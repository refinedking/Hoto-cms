using System;
using System.Text;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.API.OAuth;
using DTcms.Common;
using LitJson;

namespace DTcms.Web.api.oauth.qq
{
    public partial class result_json : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string oauth_access_token = string.Empty;
            string oauth_openid = string.Empty;
            string oauth_name = string.Empty;

            if (Session["oauth_name"] == null || Session["oauth_access_token"] == null || Session["oauth_openid"] == null)
            {
                Response.Write("{\"ret\":\"1\", \"msg\":\"出错啦，Access Token已过期或不存在！\"}");
                return;
            }
            oauth_name = Session["oauth_name"].ToString();
            oauth_access_token = Session["oauth_access_token"].ToString();
            oauth_openid = Session["oauth_openid"].ToString();
            JsonData jd = qq_helper.get_info(oauth_access_token, oauth_openid);
            if (jd == null)
            {
                Response.Write("{\"ret\":\"1\", \"msg\":\"出错啦，无法获取授权用户信息！\"}");
                return;
            }
            try
            {
                if (jd["ret"].ToString() != "0")
                {
                    Response.Write("{\"ret\":\"" + jd["ret"].ToString() + "\", \"msg\":\"出错信息:" + jd["msg"].ToString() + "！\"}");
                    return;
                }
                StringBuilder str = new StringBuilder();
                str.Append("{");
                str.Append("\"ret\": \"" + jd["ret"].ToString() + "\", ");
                str.Append("\"msg\": \"" + jd["msg"].ToString() + "\", ");
                str.Append("\"oauth_name\": \"" + oauth_name + "\", ");
                str.Append("\"oauth_access_token\": \"" + oauth_access_token + "\", ");
                str.Append("\"oauth_openid\": \"" + jd[0]["openid"].ToString() + "\", ");
                str.Append("\"nick\": \"" + jd[0]["nick"].ToString() + "\", ");
                str.Append("\"avatar\": \"" + jd[0]["head"].ToString() + "/100\", ");
                if (jd[0]["sex"].ToString() == "1")
                {
                    str.Append("\"sex\": \"男\", ");
                }
                else if (jd[0]["sex"].ToString() == "2")
                {
                    str.Append("\"sex\": \"女\", ");
                }
                else
                {
                    str.Append("\"sex\": \"保密\", ");
                }
                str.Append("\"birthday\": \"" + jd[0]["birth_year"].ToString() + "-" + jd[0]["birth_month"].ToString() + "-" + jd[0]["birth_day"].ToString() + "\"");
                str.Append("}");
                Response.Write(str.ToString());
            }
            catch
            {
                Response.Write("{\"ret\":\"1\", \"msg\":\"出错啦，无法获取授权用户信息！\"}");
            }
            return;
        }
    }
}