using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web;
using System.Web.SessionState;
using DTcms.Web.UI;
using DTcms.Common;
using LitJson;

namespace DTcms.Web.tools
{
    /// <summary>
    /// AJAX提交处理
    /// </summary>
    public class submit_ajax : IHttpHandler, IRequiresSessionState
    {
        Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(Utils.GetXmlMapPath(DTKeys.FILE_SITE_XML_CONFING));
        Model.userconfig userConfig = new BLL.userconfig().loadConfig(Utils.GetXmlMapPath(DTKeys.FILE_USER_XML_CONFING));
        public void ProcessRequest(HttpContext context)
        {
            //取得处事类型
            string action = DTRequest.GetQueryString("action");

            switch (action)
            {
                case "digg_add": //顶踩
                    digg_add(context);
                    break;
                case "comment_add": //提交评论
                    comment_add(context);
                    break;
                case "comment_list": //评论列表
                    comment_list(context);
                    break;
                case "validate_username": //验证用户名
                    validate_username(context);
                    break;
                case "user_register": //用户注册
                    user_register(context);
                    break;
                case "user_invite_code": //申请邀请码
                    user_invite_code(context);
                    break;
                case "user_verify_email": //发送注册验证邮件
                    user_verify_email(context);
                    break;
                case "user_login": //用户登录
                    user_login(context);
                    break;
                case "user_oauth_bind": //绑定第三方登录账户
                    user_oauth_bind(context);
                    break;
                case "user_oauth_register": //注册第三方登录账户
                    user_oauth_register(context);
                    break;
                case "user_info_edit": //修改用户信息
                    user_info_edit(context);
                    break;
                case "user_avatar_crop": //确认裁剪用户头像
                    user_avatar_crop(context);
                    break;
                case "user_password_edit": //修改密码
                    user_password_edit(context);
                    break;
                case "user_getpassword": //邮箱取回密码
                    user_getpassword(context);
                    break;
                case "user_repassword": //邮箱重设密码
                    user_repassword(context);
                    break;
                case "user_message_delete": //删除短信息
                    user_message_delete(context);
                    break;
                case "user_message_add": //发布短信息
                    user_message_add(context);
                    break;
                case "user_point_convert": //用户兑换积分
                    user_point_convert(context);
                    break;
                case "user_point_delete": //删除用户积分明细
                    user_point_delete(context);
                    break;
                case "user_amount_recharge": //用户在线充值
                    user_amount_recharge(context);
                    break;
                case "user_amount_delete": //删除用户收支明细
                    user_amount_delete(context);
                    break;
                case "cart_goods_add": //购物车加入商品
                    cart_goods_add(context);
                    break;
                case "cart_goods_update": //购物车修改商品
                    cart_goods_update(context);
                    break;
                case "cart_goods_delete": //购物车删除商品
                    cart_goods_delete(context);
                    break;
                case "order_save": //保存订单
                    order_save(context);
                    break;
                case "order_cancel": //用户取消订单
                    order_cancel(context);
                    break;

            }
        }

        #region 顶和踩的处理方法OK==============================
        private void digg_add(HttpContext context)
        {
            string digg_type = DTRequest.GetFormString("digg_type");
            int article_id = DTRequest.GetFormInt("article_id");
            //检查是否重复点击
            if (article_id > 0)
            {
                string cookie = Utils.GetCookie(DTKeys.COOKIE_DIGG_KEY, "diggs_" + article_id.ToString());
                if (cookie == article_id.ToString())
                {
                    context.Response.Write("{\"msg\":0, \"msgbox\":\"您刚刚提交过，体息一会吧！\"}");
                    return;
                }
            }
            BLL.article_diggs bll = new BLL.article_diggs();
            if (!bll.Exists(article_id))
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"信息不存在或已删除！\"}");
                return;
            }
            //自动+1
            if (digg_type == "good")
            {
                bll.UpdateField(article_id, "digg_good=digg_good+1");
            }
            else
            {
                bll.UpdateField(article_id, "digg_bad=digg_bad+1");
            }
            //返回成功
            Model.article_diggs model = bll.GetModel(article_id);
            context.Response.Write("{\"msg\":1, \"digggood\":" + model.digg_good + ", \"diggbad\":" + model.digg_bad + ", \"msgbox\":\"成功顶或踩了一下！\"}");
            Utils.WriteCookie(DTKeys.COOKIE_DIGG_KEY, "diggs_" + article_id.ToString(), article_id.ToString(), 8640);
            return;

        }
        #endregion

        #region 提交评论的处理方法OK============================
        private void comment_add(HttpContext context)
        {
            StringBuilder strTxt = new StringBuilder();
            BLL.article_comment bll = new BLL.article_comment();
            Model.article_comment model = new Model.article_comment();

            string code = DTRequest.GetFormString("txtCode");
            int article_id = DTRequest.GetQueryInt("article_id");
            string _content = DTRequest.GetFormString("txtContent");
            //校检验证码
            string result =verify_code(context, code);
            if (result != "success")
            {
                context.Response.Write(result);
                return;
            }
            if (article_id == 0)
            {
                context.Response.Write("{\"msg\": 0, \"msgbox\": \"对不起，参数传输有误！\"}");
                return;
            }
            if (string.IsNullOrEmpty(_content))
            {
                context.Response.Write("{\"msg\": 0, \"msgbox\": \"对不起，请输入评论的内容！\"}");
                return;
            }
            //检查用户是否登录
            int user_id = 0;
            string user_name = "匿名用户";
            Model.users userModel = new Web.UI.BasePage().GetUserInfo();
            if (userModel != null)
            {
                user_id = userModel.id;
                user_name = userModel.user_name;
            }
            model.article_id = article_id;
            model.content = Utils.ToHtml(_content);
            model.user_id = user_id;
            model.user_name = user_name;
            model.user_ip = DTRequest.GetIP();
            model.is_lock = siteConfig.commentstatus; //审核开关
            model.add_time = DateTime.Now;
            model.is_reply = 0;
            if (bll.Add(model) > 0)
            {
                context.Response.Write("{\"msg\": 1, \"msgbox\": \"恭喜您，留言提交成功啦！\"}");
                return;
            }
            context.Response.Write("{\"msg\": 0, \"msgbox\": \"对不起，保存过程中发生错误！\"}");
            return;
        }
        #endregion

        #region 取得评论列表方法OK==============================
        private void comment_list(HttpContext context)
        {
            int article_id = DTRequest.GetQueryInt("article_id");
            int page_index = DTRequest.GetQueryInt("page_index");
            int page_size = DTRequest.GetQueryInt("page_size");
            int totalcount;
            StringBuilder strTxt = new StringBuilder();

            if (article_id == 0 || page_size == 0)
            {
                context.Response.Write("获取失败，传输参数有误！");
                return;
            }

            BLL.article_comment bll = new BLL.article_comment();
            DataSet ds = bll.GetList(page_size, page_index, string.Format("is_lock=0 and article_id={0}", article_id.ToString()), "add_time asc", out totalcount);
            //如果记录存在
            if (ds.Tables[0].Rows.Count > 0)
            {
                strTxt.Append("[");
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];
                    //strTxt.Append("<li>\n");
                    //strTxt.Append("<div class=\"title\"><span>" + dr["add_time"] + "</span>" + dr["user_name"] + "</div>");
                    //strTxt.Append("<div class=\"box\">" + dr["content"] + "</div>");
                    //if (Convert.ToInt32(dr["is_reply"]) == 1)
                    //{
                    //    strTxt.Append("<div class=\"reply\">");
                    //    strTxt.Append("<strong>管理员回复：</strong>" + dr["reply_content"].ToString());
                    //    strTxt.Append("<span class=\"time\">" + dr["reply_time"].ToString() + "</span>");
                    //    strTxt.Append("</div>");
                    //}
                    //strTxt.Append("</li>\n");

                    strTxt.Append("{");
                    strTxt.Append("\"user_id\":" + dr["user_id"]);
                    strTxt.Append(",\"user_name\":\"" + dr["user_name"] + "\"");
                    if (Convert.ToInt32(dr["user_id"]) > 0)
                    {
                        Model.users userModel = new BLL.users().GetModel(Convert.ToInt32(dr["user_id"]));
                        if (userModel != null)
                        {
                            strTxt.Append(",\"avatar\":\"" + userModel.avatar + "\"");
                        }
                    }
                    strTxt.Append("");
                    strTxt.Append(",\"content\":\"" + Microsoft.JScript.GlobalObject.escape(dr["content"]) + "\"");
                    strTxt.Append(",\"add_time\":\"" + dr["add_time"] + "\"");
                    strTxt.Append(",\"is_reply\":" + dr["is_reply"]);
                    if (Convert.ToInt32(dr["is_reply"]) == 1)
                    {
                        strTxt.Append(",\"reply_content\":\"" + Microsoft.JScript.GlobalObject.escape(dr["reply_content"]) + "\"");
                        strTxt.Append(",\"reply_time\":\"" + dr["reply_time"] + "\"");
                    }
                    strTxt.Append("}");
                    //是否加逗号
                    if (i < ds.Tables[0].Rows.Count - 1)
                    {
                        strTxt.Append(",");
                    }
                   
                }
                strTxt.Append("]");
            }
            //else
            //{
            //    strTxt.Append("<p>暂无评论，快来抢沙发吧！</p>");
            //}
            context.Response.Write(strTxt.ToString());
        }
        #endregion

        #region 验证用户名是否可用OK============================
        private void validate_username(HttpContext context)
        {
            string username = DTRequest.GetString("username");
            //如果为Null，退出
            if (string.IsNullOrEmpty(username))
            {
                context.Response.Write("null");
                return;
            }
            //过滤注册用户名字符
            string[] strArray = userConfig.regkeywords.Split(',');
            foreach (string s in strArray)
            {
                if (s.ToLower() == username.ToLower())
                {
                    context.Response.Write("lock");
                    return;
                }
            }
            BLL.users bll = new BLL.users();
            //查询数据库
            if (!bll.Exists(username.Trim()))
            {
                context.Response.Write("true");
                return;
            }
            context.Response.Write("false");
            return;
        }
        #endregion

        #region 用户注册OK=====================================
        private void user_register(HttpContext context)
        {
            string code = DTRequest.GetFormString("txtCode").Trim();
            string invitecode = DTRequest.GetFormString("txtInviteCode").Trim();
            string username = DTRequest.GetFormString("txtUserName").Trim();
            string password = DTRequest.GetFormString("txtPassword").Trim();
            string email = DTRequest.GetFormString("txtEmail").Trim();
            string userip = DTRequest.GetIP();

            #region 检查各项并提示
            //检查是否开启会员功能
            if (siteConfig.memberstatus == 0)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"对不起，会员功能已被关闭，无法注册新会员！\"}");
                return;
            }
            if (userConfig.regstatus == 0)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"对不起，系统暂不允许注册新用户！\"}");
                return;
            }
            //校检验证码
            string result = verify_code(context, code);
            if (result != "success")
            {
                context.Response.Write(result);
                return;
            }
            //检查用户输入信息是否为空
            if (username == "" || password == "")
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"用户名和密码不能为空！\"}");
                return;
            }
            if (email == "")
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"电子邮箱不能为空！\"}");
                return;
            }

            //检查用户名
            BLL.users bll = new BLL.users();
            Model.users model = new Model.users();
            if (bll.Exists(username))
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"该用户名已经存在！\"}");
                return;
            }
            //检查同一IP注册时隔
            if (userConfig.regctrl > 0)
            {
                if (bll.Exists(userip, userConfig.regctrl))
                {
                    context.Response.Write("{\"msg\":0, \"msgbox\":\"对不起，同一IP在" + userConfig.regctrl + "小时内不能注册多个用户！\"}");
                    return;
                }
            }
            //不允许同一Email注册不同用户
            if (userConfig.regemailditto == 0)
            {
                if (bll.ExistsEmail(email))
                {
                    context.Response.Write("{\"msg\":0, \"msgbox\":\"Email不允许重复注册，如果你忘记用户名，请找回密码！\"}");
                    return;
                }
            }
            //检查默认组别是否存在
            Model.user_groups modelGroup = new BLL.user_groups().GetDefault();
            if (modelGroup == null)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"系统尚未分组，请联系管理员设置会员分组！\"}");
                return;
            }
            //检查是否通过邀请码注册
            if (userConfig.regstatus == 2)
            {
                string result1 = verify_invite_reg(username, invitecode);
                if (result1 != "success")
                {
                    context.Response.Write(result1);
                    return;
                }
            }
            #endregion

            //保存注册信息
            model.group_id = modelGroup.id;
            model.user_name = username;
            model.password = DESEncrypt.Encrypt(password);
            model.email = email;
            model.reg_ip = userip;
            model.reg_time = DateTime.Now;
            model.is_lock = userConfig.regverify; //设置为对应状态
            int newId = bll.Add(model);
            if (newId < 1)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"系统故障，注册失败，请联系网站管理员！\"}");
                return;
            }
            model = bll.GetModel(newId);
            //赠送积分金额
            if (modelGroup.point > 0)
            {
                new BLL.point_log().Add(model.id, model.user_name, modelGroup.point, "注册赠送积分");
            }
            if (modelGroup.amount > 0)
            {
                new BLL.amount_log().Add(model.id, model.user_name, DTEnums.AmountTypeEnum.SysGive.ToString(), modelGroup.amount, "注册赠送金额", 1);
            }
            //判断是否发送站内短消息
            if (userConfig.regmsgstatus == 1)
            {
                new BLL.user_message().Add(1, "", model.user_name, "欢迎您成为本站会员", userConfig.regmsgtxt);
            }
            //需要Email验证
            if (userConfig.regverify == 1)
            {
                string result2 = verify_email(model);
                if (result2 != "success")
                {
                    context.Response.Write(result2);
                    return;
                }
                context.Response.Write("{\"msg\":1, \"url\":\"" + new Web.UI.BasePage().linkurl("register") + "?action=sendmail&username=" + Utils.UrlEncode(model.user_name) + "\", \"msgbox\":\"注册成功，请进入邮箱验证激活账户！\"}");
            }
            //需要人工审核
            else if (userConfig.regverify == 2)
            {
                context.Response.Write("{\"msg\":1, \"url\":\"" + new Web.UI.BasePage().linkurl("register") + "?action=verify&username=" + Utils.UrlEncode(model.user_name) + "\", \"msgbox\":\"注册成功，请等待审核通过！\"}");
            }
            else
            {
                context.Response.Write("{\"msg\":1, \"url\":\"" + new Web.UI.BasePage().linkurl("register") + "?action=succeed&username=" + Utils.UrlEncode(model.user_name) + "\", \"msgbox\":\"恭喜您，注册成功啦！\"}");
            }
            return;
        }

        #region 邀请注册处理方法OK==============================
        private string verify_invite_reg(string user_name, string invite_code)
        {
            if (string.IsNullOrEmpty(invite_code))
            {
                return "{\"msg\":0, \"msgbox\":\"邀请码不能为空！\"}";
            }
            BLL.user_code codeBll = new BLL.user_code();
            Model.user_code codeModel = codeBll.GetModel(invite_code);
            if (codeModel == null)
            {
                return "{\"msg\":0, \"msgbox\":\"邀请码不正确或已过期啦！\"}";
            }
            if (userConfig.invitecodecount > 0)
            {
                if (codeModel.count >= userConfig.invitecodecount)
                {
                    codeModel.status = 1;
                    return "{\"msg\":0, \"msgbox\":\"该邀请码已经被使用啦！\"}";
                }
            }
            //检查是否给邀请人增加积分
            if (userConfig.pointinvitenum > 0)
            {
                new BLL.point_log().Add(codeModel.user_id, codeModel.user_name, userConfig.pointinvitenum, "邀请用户【" + user_name + "】注册获得积分");
            }
            //更改邀请码状态
            codeModel.count += 1;
            codeBll.Update(codeModel);
            return "success";
        }
        #endregion

        #region Email验证发送邮件OK=============================
        private string verify_email(Model.users userModel)
        {
            //生成随机码
            string strcode = Utils.GetCheckCode(20);
            BLL.user_code codeBll = new BLL.user_code();
            Model.user_code codeModel;
            //检查是否重复提交
            codeModel = codeBll.GetModel(userModel.user_name, DTEnums.CodeEnum.RegVerify.ToString());
            if (codeModel == null)
            {
                codeModel = new Model.user_code();
                codeModel.user_id = userModel.id;
                codeModel.user_name = userModel.user_name;
                codeModel.type = DTEnums.CodeEnum.RegVerify.ToString();
                codeModel.str_code = strcode;
                codeModel.eff_time = DateTime.Now.AddDays(userConfig.regemailexpired);
                codeModel.add_time = DateTime.Now;
                new BLL.user_code().Add(codeModel);
            }
            //获得邮件内容
            Model.mail_template mailModel = new BLL.mail_template().GetModel("regverify");
            if (mailModel == null)
            {
                return "{\"msg\":0, \"msgbox\":\"邮件发送失败，邮件模板内容不存在！\"}";
            }
            //替换模板内容
            string titletxt = mailModel.maill_title;
            string bodytxt = mailModel.content;
            titletxt = titletxt.Replace("{webname}", siteConfig.webname);
            titletxt = titletxt.Replace("{username}", userModel.user_name);
            bodytxt = bodytxt.Replace("{webname}", siteConfig.webname);
            bodytxt = bodytxt.Replace("{username}", userModel.user_name);
            bodytxt = bodytxt.Replace("{linkurl}", Utils.DelLastChar(siteConfig.weburl, "/") + new Web.UI.BasePage().linkurl("register")+"?action=checkmail&strcode=" + codeModel.str_code);
            //发送邮件
            try
            {
                DTMail.sendMail(siteConfig.emailstmp, 
                    siteConfig.emailusername,
                    DESEncrypt.Decrypt(siteConfig.emailpassword), 
                    siteConfig.emailnickname, 
                    siteConfig.emailfrom, 
                    userModel.email, 
                    titletxt, bodytxt);
            }
            catch
            {
                return "{\"msg\":0, \"msgbox\":\"邮件发送失败，请联系本站管理员！\"}";
            }
            return "success";
        }

        #endregion

        #endregion

        #region 申请邀请码OK===================================
        private void user_invite_code(HttpContext context)
        {
            //检查用户是否登录
            Model.users model = new BasePage().GetUserInfo();
            if (model == null)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"对不起，用户没有登录或登录超时啦！\"}");
                return;
            }
            //检查是否开启邀请注册
            if (userConfig.regstatus != 2)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"对不起，系统不允许通过邀请注册！\"}");
                return;
            }
            BLL.user_code codeBll = new BLL.user_code();
            //检查申请是否超过限制
            if (userConfig.invitecodenum > 0)
            {
                int result = codeBll.GetCount("user_name='" + model.user_name + "' and type='" + DTEnums.CodeEnum.Register.ToString() + "' and datediff(d,add_time,getdate())=0");
                if (result >= userConfig.invitecodenum)
                {
                    context.Response.Write("{\"msg\":0, \"msgbox\":\"对不起，您申请的邀请码数量已超过每天的限制！\"}");
                    return;
                }
            }
            //删除过期的邀请码
            codeBll.Delete("type='" + DTEnums.CodeEnum.Register.ToString() + "' and status=1 or datediff(d,eff_time,getdate())>0");
            //随机取得邀请码
            string str_code = Utils.GetCheckCode(8);
            Model.user_code codeModel = new Model.user_code();
            codeModel.user_id = model.id;
            codeModel.user_name = model.user_name;
            codeModel.type = DTEnums.CodeEnum.Register.ToString();
            codeModel.str_code = str_code;
            if (userConfig.invitecodeexpired > 0)
            {
                codeModel.eff_time = DateTime.Now.AddDays(userConfig.invitecodeexpired);
            }
            codeBll.Add(codeModel);
            context.Response.Write("{\"msg\":1, \"msgbox\":\"恭喜您，申请邀请码已成功！\"}");
            return;
        }
        #endregion

        #region 发送注册验证邮件OK=============================
        private void user_verify_email(HttpContext context)
        {
            string username = DTRequest.GetFormString("username");
            //检查是否过快
            string cookie = Utils.GetCookie("user_reg_email");
            if (cookie == username)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"发送邮件间隔为20分钟，您刚才已经提交过啦，休息一下再来吧！\"}");
                return;
            }
            Model.users model = new BLL.users().GetModel(username);
            if (model == null)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"该用户不存在或已删除！\"}");
                return;
            }
            if (model.is_lock != 1)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"该用户无法进行邮箱验证！\"}");
                return;
            }
            string result = verify_email(model);
            if (result != "success")
            {
                context.Response.Write(result);
                return;
            }
            context.Response.Write("{\"msg\":1, \"msgbox\":\"邮件已经发送成功啦！\"}");
            Utils.WriteCookie("user_reg_email", username, 20); //20分钟内无重复发送
            return;
        }
        #endregion

        #region 用户登录OK=====================================
        private void user_login(HttpContext context)
        {
            string username = DTRequest.GetFormString("txtUserName");
            string password = DTRequest.GetFormString("txtPassword");
            string remember = DTRequest.GetFormString("chkRemember");
            //检查用户名密码
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                context.Response.Write("{\"msg\": 0, \"msgbox\": \"温馨提示：请输入用户名或密码！\"}");
                return;
            }

            BLL.users bll = new BLL.users();
            Model.users model = bll.GetModel(username, DESEncrypt.Encrypt(password), userConfig.emaillogin);
            if (model == null)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"错误提示：用户名或密码错误，请重试！\"}");
                return;
            }
            //检查用户是否通过验证
            if (model.is_lock == 1) //待验证
            {
                context.Response.Write("{\"msg\":1, \"url\":\"" + new Web.UI.BasePage().linkurl("register") + "?action=sendmail&username=" + Utils.UrlEncode(model.user_name) + "\", \"msgbox\":\"会员尚未通过验证！\"}");
                return;
            }
            else if (model.is_lock == 2) //待审核
            {
                context.Response.Write("{\"msg\":1, \"url\":\"" + new Web.UI.BasePage().linkurl("register") + "?action=verify&username=" + Utils.UrlEncode(model.user_name) + "\", \"msgbox\":\"会员尚未通过审核！\"}");
                return;
            }
            context.Session[DTKeys.SESSION_USER_INFO] = model;
            context.Session.Timeout = 45;
            //记住登录状态下次自动登录
            if (remember.ToLower() == "true")
            {
                Utils.WriteCookie(DTKeys.COOKIE_USER_NAME_REMEMBER, "DTcms", model.user_name, 43200);
                Utils.WriteCookie(DTKeys.COOKIE_USER_PWD_REMEMBER, "DTcms", model.password, 43200);
            }
            else
            {
                //防止Session提前过期
                Utils.WriteCookie(DTKeys.COOKIE_USER_NAME_REMEMBER, "DTcms", model.user_name);
                Utils.WriteCookie(DTKeys.COOKIE_USER_PWD_REMEMBER, "DTcms", model.password);
            }

            //写入登录日志
            new BLL.user_login_log().Add(model.id, model.user_name, "会员登录", DTRequest.GetIP());
            //返回URL
            context.Response.Write("{\"msg\":1, \"msgbox\":\"会员登录成功！\"}");
            return;
        }
        #endregion

        #region 绑定第三方登录账户OK============================
        private void user_oauth_bind(HttpContext context)
        {
            //检查URL参数
            if (context.Session["oauth_name"] == null)
            {
                context.Response.Write("{\"msg\": 0, \"msgbox\": \"错误提示：授权参数不正确！\"}");
                return;
            }
            //获取授权信息
            string result = Utils.UrlExecute(siteConfig.webpath + "api/oauth/" + context.Session["oauth_name"].ToString() + "/result_json.aspx");
            if (result.Contains("error"))
            {
                context.Response.Write("{\"msg\": 0, \"msgbox\": \"错误提示：请检查URL是否正确！\"}");
                return;
            }
            //反序列化JSON
            Dictionary<string, object> dic = JsonMapper.ToObject<Dictionary<string, object>>(result);
            if (dic["ret"].ToString() != "0")
            {
                context.Response.Write("{\"msg\": 0, \"msgbox\": \"错误代码：" + dic["ret"] + "，描述：" + dic["msg"] + "\"}");
                return;
            }

            //检查用户名密码
            string username = DTRequest.GetString("txtUserName");
            string password = DTRequest.GetString("txtPassword");
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                context.Response.Write("{\"msg\": 0, \"msgbox\": \"温馨提示：请输入用户名或密码！\"}");
                return;
            }
            BLL.users bll = new BLL.users();
            Model.users model = bll.GetModel(username, DESEncrypt.Encrypt(password), userConfig.emaillogin);
            if (model == null)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"错误提示：用户名或密码错误，请重试！\"}");
                return;
            }
            //开始绑定
            Model.user_oauth oauthModel = new Model.user_oauth();
            oauthModel.oauth_name = dic["oauth_name"].ToString();
            oauthModel.user_id = model.id;
            oauthModel.user_name = model.user_name;
            oauthModel.oauth_access_token = dic["oauth_access_token"].ToString();
            oauthModel.oauth_openid = dic["oauth_openid"].ToString();
            int newId = new BLL.user_oauth().Add(oauthModel);
            if (newId < 1)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"错误提示：绑定过程中出现错误，请重新登录授权！\"}");
                return;
            }
            context.Session[DTKeys.SESSION_USER_INFO] = model;
            context.Session.Timeout = 45;
            //记住登录状态，防止Session提前过期
            Utils.WriteCookie(DTKeys.COOKIE_USER_NAME_REMEMBER, "DTcms", model.user_name);
            Utils.WriteCookie(DTKeys.COOKIE_USER_PWD_REMEMBER, "DTcms", model.password);
            //写入登录日志
            new BLL.user_login_log().Add(model.id, model.user_name, "会员登录", DTRequest.GetIP());
            //返回URL
            context.Response.Write("{\"msg\":1, \"msgbox\":\"会员登录成功！\"}");
            return;
        }
        #endregion

        #region 注册第三方登录账户OK============================
        private void user_oauth_register(HttpContext context)
        {
            //检查URL参数
            if (context.Session["oauth_name"] == null)
            {
                context.Response.Write("{\"msg\": 0, \"msgbox\": \"错误提示：授权参数不正确！\"}");
                return;
            }
            //获取授权信息
            string result = Utils.UrlExecute(siteConfig.webpath + "api/oauth/" + context.Session["oauth_name"].ToString() + "/result_json.aspx");
            if (result.Contains("error"))
            {
                context.Response.Write("{\"msg\": 0, \"msgbox\": \"错误提示：请检查URL是否正确！\"}");
                return;
            }
            //反序列化JSON
            Dictionary<string, object> dic = JsonMapper.ToObject<Dictionary<string, object>>(result);
            if (dic["ret"].ToString() != "0")
            {
                context.Response.Write("{\"msg\": 0, \"msgbox\": \"错误代码：" + dic["ret"] + "，" + dic["msg"] + "\"}");
                return;
            }

            string password = DTRequest.GetFormString("txtPassword").Trim();
            string email = DTRequest.GetFormString("txtEmail").Trim();
            string userip = DTRequest.GetIP();
            //检查用户名
            BLL.users bll = new BLL.users();
            Model.users model = new Model.users();
            //检查默认组别是否存在
            Model.user_groups modelGroup = new BLL.user_groups().GetDefault();
            if (modelGroup == null)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"系统尚未分组，请联系管理员设置会员分组！\"}");
                return;
            }
            //保存注册信息
            model.group_id = modelGroup.id;
            model.user_name = bll.GetRandomName(10);
            model.password = DESEncrypt.Encrypt(password);
            model.email = email;
            if (!string.IsNullOrEmpty(dic["nick"].ToString()))
            {
                model.nick_name = dic["nick"].ToString();
            }
            if (dic["avatar"].ToString().StartsWith("http://"))
            {
                model.avatar = dic["avatar"].ToString();
            }
            if (!string.IsNullOrEmpty(dic["sex"].ToString()))
            {
                model.sex = dic["sex"].ToString();
            }
            if (!string.IsNullOrEmpty(dic["birthday"].ToString()))
            {
                model.birthday = DateTime.Parse(dic["birthday"].ToString());
            }
            model.reg_ip = userip;
            model.reg_time = DateTime.Now;
            model.is_lock = 0; //设置为对应状态
            int newId = bll.Add(model);
            if (newId < 1)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"系统故障，注册失败，请联系网站管理员！\"}");
                return;
            }
            model = bll.GetModel(newId);
            //赠送积分金额
            if (modelGroup.point > 0)
            {
                new BLL.point_log().Add(model.id, model.user_name, modelGroup.point, "注册赠送积分");
            }
            if (modelGroup.amount > 0)
            {
                new BLL.amount_log().Add(model.id, model.user_name, DTEnums.AmountTypeEnum.SysGive.ToString(), modelGroup.amount, "注册赠送金额", 1);
            }
            //判断是否发送站内短消息
            if (userConfig.regmsgstatus == 1)
            {
                new BLL.user_message().Add(1, "", model.user_name, "欢迎您成为本站会员", userConfig.regmsgtxt);
            }
            //绑定到对应的授权类型
            Model.user_oauth oauthModel = new Model.user_oauth();
            oauthModel.oauth_name = dic["oauth_name"].ToString();
            oauthModel.user_id = model.id;
            oauthModel.user_name = model.user_name;
            oauthModel.oauth_access_token = dic["oauth_access_token"].ToString();
            oauthModel.oauth_openid = dic["oauth_openid"].ToString();
            new BLL.user_oauth().Add(oauthModel);

            context.Session[DTKeys.SESSION_USER_INFO] = model;
            context.Session.Timeout = 45;
            //记住登录状态，防止Session提前过期
            Utils.WriteCookie(DTKeys.COOKIE_USER_NAME_REMEMBER, "DTcms", model.user_name);
            Utils.WriteCookie(DTKeys.COOKIE_USER_PWD_REMEMBER, "DTcms", model.password);
            //写入登录日志
            new BLL.user_login_log().Add(model.id, model.user_name, "会员登录", DTRequest.GetIP());
            //返回URL
            context.Response.Write("{\"msg\":1, \"msgbox\":\"会员登录成功！\"}");
            return;
        }
        #endregion

        #region 修改用户信息OK=================================
        private void user_info_edit(HttpContext context)
        {
            //检查用户是否登录
            Model.users model = new BasePage().GetUserInfo();
            if (model == null)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"对不起，用户没有登录或登录超时啦！\"}");
                return;
            }
            string email = DTRequest.GetFormString("txtEmail");
            string nick_name = DTRequest.GetFormString("txtNickName");
            string sex = DTRequest.GetFormString("rblSex");
            string birthday = DTRequest.GetFormString("txtBirthday");
            string telphone = DTRequest.GetFormString("txtTelphone");
            string mobile = DTRequest.GetFormString("txtMobile");
            string qq = DTRequest.GetFormString("txtQQ");
            string address = context.Request.Form["txtAddress"];
            string safe_question = context.Request.Form["txtSafeQuestion"];
            string safe_answer = context.Request.Form["txtSafeAnswer"];
            //检查邮箱
            if (nick_name == "")
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"对不起，请输入您的姓名昵称！\"}");
                return;
            }
            //检查邮箱
            if (email == "")
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"对不起，请输入您邮箱帐号！\"}");
                return;
            }
            
            //开始写入数据库
            model.email = email;
            model.nick_name = nick_name;
            model.sex = sex;
            DateTime _birthday;
            if (DateTime.TryParse(birthday, out _birthday))
            {
                model.birthday = _birthday;
            }
            model.telphone = telphone;
            model.mobile = mobile;
            model.qq = qq;
            model.address = address;
            model.safe_question = safe_question;
            model.safe_answer = safe_answer;

            
            new BLL.users().Update(model);
            context.Response.Write("{\"msg\":1, \"msgbox\":\"您的账户资料已修改成功啦！\"}");
            return;
        }
        #endregion

        #region 确认裁剪用户头像OK=============================
        private void user_avatar_crop(HttpContext context)
        {
            //检查用户是否登录
            Model.users model = new BasePage().GetUserInfo();
            if (model == null)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"对不起，用户没有登录或登录超时啦！\"}");
                return;
            }
            string fileName = DTRequest.GetFormString("hideFileName");
            int x1 = DTRequest.GetFormInt("hideX1");
            int y1 = DTRequest.GetFormInt("hideY1");
            int w = DTRequest.GetFormInt("hideWidth");
            int h = DTRequest.GetFormInt("hideHeight");
            //检查是否图片

            //检查参数
            if (!Utils.FileExists(fileName) || w == 0 || h == 0)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"对不起，请先上传一张图片！\"}");
                return;
            }
            //取得保存的新文件名
            UpLoad upFiles = new UpLoad();
            bool result = upFiles.cropSaveAs(fileName, fileName, 180, 180, w, h, x1, y1);
            if (!result)
            {
                context.Response.Write("{\"msg\": 0, \"msgbox\": \"图片裁剪过程中发生意外错误！\"}");
                return;
            }
            //删除原用户头像
            Utils.DeleteFile(model.avatar);
            model.avatar = fileName;
            //修改用户头像
            new BLL.users().UpdateField(model.id, "avatar='" + model.avatar + "'");
            context.Response.Write("{\"msg\": 1, \"msgbox\": \"" + model.avatar + "\"}");
            return;
        }
        #endregion

        #region 修改登录密码OK=================================
        private void user_password_edit(HttpContext context)
        {
            //检查用户是否登录
            Model.users model = new BasePage().GetUserInfo();
            if (model == null)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"对不起，用户没有登录或登录超时啦！\"}");
                return;
            }
            int user_id = model.id;
            string oldpassword = DTRequest.GetFormString("txtOldPassword");
            string password = DTRequest.GetFormString("txtPassword");
            //检查输入的旧密码
            if (string.IsNullOrEmpty(oldpassword))
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"请输入您的旧登录密码！\"}");
                return;
            }
            //检查输入的新密码
            if (string.IsNullOrEmpty(password))
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"请输入您的新登录密码！\"}");
                return;
            }
            //旧密码是否正确
            if (model.password != DESEncrypt.Encrypt(oldpassword))
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"对不起，您输入的旧密码不正确！\"}");
                return;
            }
            //执行修改操作
            model.password = DESEncrypt.Encrypt(password);
            new BLL.users().Update(model);
            context.Response.Write("{\"msg\":1, \"msgbox\":\"您的密码已修改成功，请记住新密码！\"}");
            return;
        }
        #endregion

        #region 邮箱取回密码OK=================================
        private void user_getpassword(HttpContext context)
        {
            string code = DTRequest.GetFormString("txtCode");
            string username = DTRequest.GetFormString("txtUserName").Trim();
            //检查用户名是否正确
            if (string.IsNullOrEmpty(username))
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"对不起，用户名不可为空！\"}");
                return;
            }
            //校检验证码
            string result = verify_code(context, code);
            if (result != "success")
            {
                context.Response.Write(result);
                return;
            }
            //检查用户信息
            BLL.users bll = new BLL.users();
            Model.users model = bll.GetModel(username);
            if (model == null)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"对不起，您输入的用户名不存在！\"}");
                return;
            }
            if (string.IsNullOrEmpty(model.email))
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"您尚未设置邮箱地址，无法使用取回密码功能！\"}");
                return;
            }
            //生成随机码
            string strcode = Utils.GetCheckCode(20);
            //获得邮件内容
            Model.mail_template mailModel = new BLL.mail_template().GetModel("getpassword");
            if (mailModel == null)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"邮件发送失败，邮件模板内容不存在！\"}");
                return;
            }
            //检查是否重复提交
            BLL.user_code codeBll = new BLL.user_code();
            Model.user_code codeModel;
            codeModel = codeBll.GetModel(username, DTEnums.CodeEnum.RegVerify.ToString());
            if (codeModel == null)
            {
                codeModel = new Model.user_code();
                //写入数据库
                codeModel.user_id = model.id;
                codeModel.user_name = model.user_name;
                codeModel.type = DTEnums.CodeEnum.Password.ToString();
                codeModel.str_code = strcode;
                codeModel.eff_time = DateTime.Now.AddDays(1);
                codeModel.add_time = DateTime.Now;
                codeBll.Add(codeModel);
            }
            //替换模板内容
            string titletxt = mailModel.maill_title;
            string bodytxt = mailModel.content;
            titletxt = titletxt.Replace("{webname}", siteConfig.webname);
            titletxt = titletxt.Replace("{username}", model.user_name);
            bodytxt = bodytxt.Replace("{webname}", siteConfig.webname);
            bodytxt = bodytxt.Replace("{username}", model.user_name);
            bodytxt = bodytxt.Replace("{linkurl}", Utils.DelLastChar(siteConfig.weburl, "/") + new BasePage().linkurl("repassword1", "reset", strcode)); //此处需要修改
            //发送邮件
            try
            {
                DTMail.sendMail(siteConfig.emailstmp,
                    siteConfig.emailusername,
                    DESEncrypt.Decrypt(siteConfig.emailpassword),
                    siteConfig.emailnickname,
                    siteConfig.emailfrom,
                    model.email,
                    titletxt, bodytxt);
            }
            catch
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"邮件发送失败，请联系本站管理员！\"}");
                return;
            }
            context.Response.Write("{\"msg\":1, \"msgbox\":\"邮件发送成功，请登录您的邮箱找回登录密码！\"}");
            return;
        }
        #endregion

        #region 邮箱重设密码OK=================================
        private void user_repassword(HttpContext context)
        {
            string code = context.Request.Form["txtCode"];
            string strcode = context.Request.Form["hideCode"];
            string password = context.Request.Form["txtPassword"];

            //校检验证码
            string result = verify_code(context, code);
            if (result != "success")
            {
                context.Response.Write(result);
                return;
            }
            //检查验证字符串
            if (string.IsNullOrEmpty(strcode))
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"系统找不到邮件验证的字符串！\"}");
                return;
            }
            //检查输入的新密码
            if (string.IsNullOrEmpty(password))
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"请输入您的新密码！\"}");
                return;
            }

            BLL.user_code codeBll = new BLL.user_code();
            Model.user_code codeModel = codeBll.GetModel(strcode);
            if (codeModel == null)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"邮件验证的字符串不存在或已过期！\"}");
                return;
            }
            //验证用户是否存在
            BLL.users userBll = new BLL.users();
            if (!userBll.Exists(codeModel.user_id))
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"该用户不存在或已被删除！\"}");
                return;
            }
            Model.users userModel = userBll.GetModel(codeModel.user_id);
            //执行修改操作
            userModel.password = DESEncrypt.Encrypt(password);
            userBll.Update(userModel);
            //更改验证字符串状态
            codeModel.count = 1;
            codeModel.status = 1;
            codeBll.Update(codeModel);
            context.Response.Write("{\"msg\":1, \"msgbox\":\"修改密码成功，请记住您的新密码！\"}");
            return;
        }
        #endregion

        #region 删除短信息OK===================================
        private void user_message_delete(HttpContext context)
        {
            //检查用户是否登录
            Model.users model = new BasePage().GetUserInfo();
            if (model == null)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"对不起，用户没有登录或登录超时啦！\"}");
                return;
            }
            string check_id = DTRequest.GetFormString("checkId");
            if (check_id == "")
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"删除失败，请检查传输参数！\"}");
                return;
            }
            string[] arrId = check_id.Split(',');
            for (int i = 0; i < arrId.Length; i++)
            {
                new BLL.user_message().Delete(int.Parse(arrId[i]), model.user_name);
            }
            context.Response.Write("{\"msg\":1, \"msgbox\":\"删除短信息成功啦！\"}");
            return;
        }
        #endregion

        #region 发布短信息OK===================================
        private void user_message_add(HttpContext context)
        {
            //检查用户是否登录
            Model.users model = new BasePage().GetUserInfo();
            if (model == null)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"对不起，用户没有登录或登录超时啦！\"}");
                return;
            }
            string code = context.Request.Form["txtCode"];
            string send_save = DTRequest.GetFormString("sendSave");
            string user_name = DTRequest.GetFormString("txtUserName");
            string title = DTRequest.GetFormString("txtTitle");
            string content = DTRequest.GetFormString("txtContent");
            //校检验证码
            string result = verify_code(context, code);
            if (result != "success")
            {
                context.Response.Write(result);
                return;
            }
            //检查用户名
            if (user_name == "" || !new BLL.users().Exists(user_name))
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"对不起，该用户名不存在或已经被删除啦！\"}");
                return;
            }
            //检查标题
            if (title == "")
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"对不起，请输入短消息标题！\"}");
                return;
            }
            //检查内容
            if (content == "")
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"对不起，请输入短消息内容！\"}");
                return;
            }
            //保存数据
            Model.user_message modelMessage = new Model.user_message();
            modelMessage.type = 2;
            modelMessage.post_user_name = model.user_name;
            modelMessage.accept_user_name = user_name;
            modelMessage.title = title;
            modelMessage.content = Utils.ToHtml(content);
            new BLL.user_message().Add(modelMessage);
            if (send_save == "true") //保存到收件箱
            {
                modelMessage.type = 3;
                new BLL.user_message().Add(modelMessage);
            }
            context.Response.Write("{\"msg\":1, \"msgbox\":\"发布短信息成功啦！\"}");
            return;
        }
        #endregion

        #region 用户兑换积分OK=================================
        private void user_point_convert(HttpContext context)
        {
            //检查系统是否启用兑换积分功能
            if (userConfig.pointcashrate == 0)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"对不起，网站已关闭兑换积分功能！\"}");
                return;
            }
            //检查用户是否登录
            Model.users model = new BasePage().GetUserInfo();
            if (model == null)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"对不起，用户没有登录或登录超时啦！\"}");
                return;
            }
            int amout = DTRequest.GetFormInt("txtAmount");
            string password = DTRequest.GetFormString("txtPassword");
            if (model.amount < 1)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"对不起，您账户上的余额不足！\"}");
                return;
            }
            if (amout < 1)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"对不起，最小兑换金额为1元！\"}");
                return;
            }
            if (amout > model.amount)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"对不起，您兑换的金额大于账户余额！\"}");
                return;
            }
            if (password == "")
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"对不起，请输入您账户的密码！\"}");
                return;
            }
            //验证密码
            if (DESEncrypt.Encrypt(password) != model.password)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"对不起，您输入的密码不正确！\"}");
                return;
            }
            //计算兑换后的积分值
            int convertPoint = (int)(Convert.ToDecimal(amout) * userConfig.pointcashrate);
            //扣除金额
            int amountNewId = new BLL.amount_log().Add(model.id, model.user_name, DTEnums.AmountTypeEnum.Convert.ToString(), amout * -1, "用户兑换积分", 1);
            //增加积分
            if (amountNewId < 1)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"转换过程中发生错误，请重新提交！\"}");
                return;
            }
            int pointNewId = new BLL.point_log().Add(model.id, model.user_name, convertPoint, "用户兑换积分");
            if (pointNewId < 1)
            {
                //返还金额
                new BLL.amount_log().Add(model.id, model.user_name, DTEnums.AmountTypeEnum.Convert.ToString(), amout, "用户兑换积分失败，返还金额", 1);
                context.Response.Write("{\"msg\":0, \"msgbox\":\"转换过程中发生错误，请重新提交！\"}");
                return;
            }

            context.Response.Write("{\"msg\":1, \"msgbox\":\"恭喜您，积分兑换成功啦！\"}");
            return;
        }
        #endregion

        #region 删除用户积分明细OK=============================
        private void user_point_delete(HttpContext context)
        {
            //检查用户是否登录
            Model.users model = new BasePage().GetUserInfo();
            if (model == null)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"对不起，用户没有登录或登录超时啦！\"}");
                return;
            }
            string check_id = DTRequest.GetFormString("checkId");
            if (check_id == "")
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"删除失败，请检查传输参数！\"}");
                return;
            }
            string[] arrId = check_id.Split(',');
            for (int i = 0; i < arrId.Length; i++)
            {
                new BLL.point_log().Delete(int.Parse(arrId[i]), model.user_name);
            }
            context.Response.Write("{\"msg\":1, \"msgbox\":\"积分明细删除成功啦！\"}");
            return;
        }
        #endregion

        #region 用户在线充值OK=================================
        private void user_amount_recharge(HttpContext context)
        {
            //检查用户是否登录
            Model.users model = new BasePage().GetUserInfo();
            if (model == null)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"对不起，用户没有登录或登录超时啦！\"}");
                return;
            }
            decimal amount = DTRequest.GetFormDecimal("order_amount", 0);
            int payment_id = DTRequest.GetFormInt("payment_id");
            if (amount == 0)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"对不起，请输入正确的充值金额！\"}");
                return;
            }
            if (payment_id == 0)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"对不起，请选择正确的支付方式！\"}");
                return;
            }
            if (!new BLL.payment().Exists(payment_id))
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"对不起，您选择的支付方式不存在或已删除！\"}");
                return;
            }
            //生成订单号
            string order_no = Utils.GetOrderNumber(); //订单号
            new BLL.amount_log().Add(model.id, model.user_name, DTEnums.AmountTypeEnum.Recharge.ToString(), order_no, payment_id, amount,
                "账户充值(" + new BLL.payment().GetModel(payment_id).title + ")", 0);
            //保存成功后返回订单号
            context.Response.Write("{\"msg\":1, \"msgbox\":\"订单保存成功！\", \"url\":\"" + new Web.UI.BasePage().linkurl("payment1", "confirm", DTEnums.AmountTypeEnum.Recharge.ToString(), order_no) + "\"}");
            return;

        }
        #endregion

        #region 删除用户收支明细OK=============================
        private void user_amount_delete(HttpContext context)
        {
            //检查用户是否登录
            Model.users model = new BasePage().GetUserInfo();
            if (model == null)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"对不起，用户没有登录或登录超时啦！\"}");
                return;
            }
            string check_id = DTRequest.GetFormString("checkId");
            if (check_id == "")
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"删除失败，请检查传输参数！\"}");
                return;
            }
            string[] arrId = check_id.Split(',');
            for (int i = 0; i < arrId.Length; i++)
            {
                new BLL.amount_log().Delete(int.Parse(arrId[i]), model.user_name);
            }
            context.Response.Write("{\"msg\":1, \"msgbox\":\"收支明细删除成功啦！\"}");
            return;
        }
        #endregion

        #region 购物车加入商品OK===============================
        private void cart_goods_add(HttpContext context)
        {
            string goods_id = DTRequest.GetFormString("goods_id");
            int goods_quantity = DTRequest.GetFormInt("goods_quantity", 1);
            if (goods_id == "")
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"您提交的商品参数有误！\"}");
                return;
            }
            //查找会员组
            int group_id = 0;
            Model.users groupModel = new Web.UI.BasePage().GetUserInfo();
            if (groupModel != null)
            {
                group_id = groupModel.group_id;
            }
            //统计购物车
            Web.UI.ShopCart.Add(goods_id, goods_quantity);
            Model.cart_total cartModel = Web.UI.ShopCart.GetTotal(group_id);
            context.Response.Write("{\"msg\":1, \"msgbox\":\"商品已成功添加到购物车！\", \"quantity\":" + cartModel.total_quantity + ", \"amount\":" + cartModel.real_amount + "}");
            return;
        }
        #endregion

        #region 修改购物车商品OK===============================
        private void cart_goods_update(HttpContext context)
        {
            string goods_id = DTRequest.GetFormString("goods_id");
            int goods_quantity = DTRequest.GetFormInt("goods_quantity");
            if (goods_id == "")
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"您提交的商品参数有误！\"}");
                return;
            }

            if (Web.UI.ShopCart.Update(goods_id, goods_quantity))
            {
                context.Response.Write("{\"msg\":1, \"msgbox\":\"商品数量修改成功！\"}");
            }
            else
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"商品数量更改失败，请检查操作是否有误！\"}");
            }
            return;
        }
        #endregion

        #region 删除购物车商品OK===============================
        private void cart_goods_delete(HttpContext context)
        {
            string goods_id = DTRequest.GetFormString("goods_id");
            if (goods_id == "")
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"您提交的商品参数有误！\"}");
                return;
            }
            Web.UI.ShopCart.Clear(goods_id);
            context.Response.Write("{\"msg\":1, \"msgbox\":\"商品移除成功！\"}");
            return;
        }
        #endregion

        #region 保存用户订单OK=================================
        private void order_save(HttpContext context)
        {
            int payment_id = DTRequest.GetFormInt("payment_id");
            int distribution_id = DTRequest.GetFormInt("distribution_id");
            string accept_name = DTRequest.GetFormString("accept_name");
            string post_code = DTRequest.GetFormString("post_code");
            string telphone = DTRequest.GetFormString("telphone");
            string mobile = DTRequest.GetFormString("mobile");
            string address = DTRequest.GetFormString("address");
            string message = DTRequest.GetFormString("message");
            //检查配送方式
            if (distribution_id == 0)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"对不起，请选择配送方式！\"}");
                return;
            }
            Model.distribution disModel = new BLL.distribution().GetModel(distribution_id);
            if (disModel == null)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"对不起，您选择的配送方式不存在或已删除！\"}");
                return;
            }
            //检查支付方式
            if (payment_id == 0)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"对不起，请选择支付方式！\"}");
                return;
            }
            Model.payment payModel = new BLL.payment().GetModel(payment_id);
            if (payModel == null)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"对不起，您选择的支付方式不存在或已删除！\"}");
                return;
            }
            //检查收货人
            if (string.IsNullOrEmpty(accept_name))
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"对不起，请输入收货人姓名！\"}");
                return;
            }
            //检查手机和电话
            if (string.IsNullOrEmpty(telphone) && string.IsNullOrEmpty(mobile))
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"对不起，请输入收货人联系电话或手机！\"}");
                return;
            }
            //检查地址
            if (string.IsNullOrEmpty(address))
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"对不起，请输入详细的收货地址！\"}");
                return;
            }
            //检查用户是否登录
            Model.users userModel = new BasePage().GetUserInfo();
            if (userModel == null)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"对不起，用户没有登录或登录超时啦！\"}");
                return;
            }
            //检查购物车商品
            IList<Model.cart_items> iList = DTcms.Web.UI.ShopCart.GetList(userModel.group_id);
            if (iList == null)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"对不起，购物车为空，无法结算！\"}");
                return;
            }
            //统计购物车
            Model.cart_total cartModel = DTcms.Web.UI.ShopCart.GetTotal(userModel.group_id);
            //保存订单=======================================================================
            Model.orders model = new Model.orders();
            model.order_no = Utils.GetOrderNumber(); //订单号
            model.user_id = userModel.id;
            model.user_name = userModel.user_name;
            model.payment_id = payment_id;
            model.distribution_id = distribution_id;
            model.accept_name = accept_name;
            model.post_code = post_code;
            model.telphone = telphone;
            model.mobile = mobile;
            model.address = address;
            model.message = message;
            model.payable_amount = cartModel.payable_amount;
            model.real_amount = cartModel.real_amount;
            model.payable_freight = disModel.amount; //应付运费
            model.real_freight = disModel.amount; //实付运费
            //如果是先款后货的话
            if (payModel.type == 1)
            {
                if (payModel.poundage_type == 1) //百分比
                {
                    model.payment_fee = model.real_amount * payModel.poundage_amount / 100;
                }
                else //固定金额
                {
                    model.payment_fee = payModel.poundage_amount;
                }
            }
            //订单总金额=实付商品金额+运费+支付手续费
            model.order_amount = model.real_amount + model.real_freight + model.payment_fee;
            //购物积分,可为负数
            model.point = cartModel.total_point;
            model.add_time = DateTime.Now;
            //商品详细列表
            List<Model.order_goods> gls = new List<Model.order_goods>();
            foreach (Model.cart_items item in iList)
            {
                gls.Add(new Model.order_goods { goods_id = item.id, goods_name = item.title, goods_price = item.price, real_price = item.user_price, quantity = item.quantity, point = item.point });
            }
            model.order_goods = gls;
            int result = new BLL.orders().Add(model);
            if (result < 1)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"订单保存过程中发生错误，请重新提交！\"}");
                return;
            }
            //扣除积分
            if (model.point < 0)
            {
                new BLL.point_log().Add(model.user_id, model.user_name, model.point, "积分换购，订单号：" + model.order_no);
            }
            //清空购物车
            DTcms.Web.UI.ShopCart.Clear("0");
            //提交成功，返回URL
            context.Response.Write("{\"msg\":1, \"url\":\"" + new Web.UI.BasePage().linkurl("payment1", "confirm", DTEnums.AmountTypeEnum.BuyGoods.ToString(), model.order_no) + "\", \"msgbox\":\"恭喜您，订单已成功提交！\"}");
            return;
        }
        #endregion

        #region 用户取消订单OK=================================
        private void order_cancel(HttpContext context)
        {
            //检查用户是否登录
            Model.users userModel = new BasePage().GetUserInfo();
            if (userModel == null)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"对不起，用户没有登录或登录超时啦！\"}");
                return;
            }
            //检查订单是否存在
            string order_no = DTRequest.GetQueryString("order_no");
            Model.orders orderModel = new BLL.orders().GetModel(order_no);
            if (order_no == "" || orderModel == null)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"对不起，该订单号不存在啦！\"}");
                return;
            }
            //检查是否自己的订单
            if (userModel.id != orderModel.user_id)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"对不起，不能取消别人的订单状态！\"}");
                return;
            }
            //检查订单状态
            if (orderModel.status > 1)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"对不起，该订单不是生成状态，不能取消！\"}");
                return;
            }
            bool result = new BLL.orders().UpdateField(order_no, "status=4");
            if (!result)
            {
                context.Response.Write("{\"msg\":0, \"msgbox\":\"对不起，操作过程中发生不可遇知的错误！\"}");
                return;
            }
            //如果是积分换购则返还积分
            if (orderModel.point < 0)
            {
                new BLL.point_log().Add(orderModel.user_id, orderModel.user_name, -1 * orderModel.point, "取消订单，返还换购积分，订单号：" + orderModel.order_no);
            }
            context.Response.Write("{\"msg\":1, \"msgbox\":\"取消订单成功啦！\"}");
            return;
        }
        #endregion

        #region 通用外理方法OK=================================
        //校检验证码
        private string verify_code(HttpContext context,string strcode)
        {
            if (string.IsNullOrEmpty(strcode))
            {
                return "{\"msg\":0, \"msgbox\":\"对不起，请输入验证码！\"}";
            }
            if (context.Session[DTKeys.SESSION_CODE] == null)
            {
                return "{\"msg\":0, \"msgbox\":\"对不起，验证码超时或已过期！\"}";
            }
            if (strcode.ToLower() != (context.Session[DTKeys.SESSION_CODE].ToString()).ToLower())
            {
                return "{\"msg\":0, \"msgbox\":\"您输入的验证码与系统的不一致！\"}";
            }
            context.Session[DTKeys.SESSION_CODE] = null;
            return "success";
        }
        #endregion END通用方法=================================================

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}