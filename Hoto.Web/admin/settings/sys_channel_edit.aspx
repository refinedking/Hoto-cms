<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sys_channel_edit.aspx.cs" Inherits="DTcms.Web.admin.settings.sys_channel_edit" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>编辑频道信息</title>
<link href="../../scripts/ui/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
<link href="../images/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../../scripts/jquery/jquery-1.3.2.min.js"></script>
<script type="text/javascript" src="../../scripts/jquery/jquery.validate.min.js"></script> 
<script type="text/javascript" src="../../scripts/jquery/messages_cn.js"></script>
<script type="text/javascript" src="../../scripts/ui/js/ligerBuild.min.js"></script>
<script type="text/javascript" src="../js/function.js"></script>
<script type="text/javascript">
    //表单验证
    $(function () {
        $('#txtName').focus();
        $("#form1").validate({
            errorPlacement: function (lable, element) {
                element.ligerTip({ content: lable.html(), appendIdTo: lable });
            },
            rules: {
                txtName: {
                    required: true,
                    minlength: 2,
                    maxlength: 100,
                    remote: {
                        type: "post",
                        url: "../../tools/admin_ajax.ashx?action=sys_channel_validate",
                        data: {
                            channelname: function () {
                                return $("#txtName").val();
                            },
                            oldname: function () {
                                return $("#hidName").val();
                            }
                        },
                        dataType: "html",
                        dataFilter: function (data, type) {
                            if (data == "true")
                                return true;
                            else
                                return false;
                        }
                    }
                }
            },
            success: function (lable) {
                lable.ligerHideTip();
            },
            messages: {
                txtName: {
                    required: "输入(2-100)位字符",
                    minlength: "必须大于2位字符",
                    maxlength: "必须小于100位字符",
                    remote: "很抱歉，该名称已被使用"
                }
            }
        });
    });
    //菜单事件处理
    $(function () {
        //初始化按钮事件
        $("#item_box tr").each(function (i) {
            initButton(i);
        });
        //添加按钮(点击绑定)
        $("#itemAddButton").click(function () {
            showChannelDialog();
        });
    });

    //表格行的菜单内容
    function getTr() {
        var navRow = '<tr class="td_c">'
        + '<td><input type="hidden" name="item_type" /><input type="hidden" name="item_page" /><span class="item_type"></span></td>'
        + '<td><input name="item_name" type="text" class="txtInput small" style="width:98%;" readonly="readonly" /></td>'
        + '<td><input name="item_path" type="text" class="txtInput small3" style="width:98%;" readonly="readonly" /></td>'
        + '<td><input name="item_pattern" type="text" class="txtInput small3" style="width:98%;" readonly="readonly" /></td>'
        + '<td><input name="item_querystring" type="text" class="txtInput small3" style="width:98%;" readonly="readonly" /></td>'
        + '<td><input name="item_templet" type="text" class="txtInput small" style="width:98%;" readonly="readonly" /></td>'
		+ '<td align="center"><img alt="编辑" src="../images/icon_edit.gif" class="operator" /><img alt="删除" src="../images/icon_del.gif" class="operator" /></td>'
		+ '</tr>';
        return navRow;
    }

    //初始化按钮事件
    function initButton(indexValue) {
        //功能操作按钮
        $("#item_box tr:eq(" + indexValue + ") .operator").each(function (i) {
            switch (i) {
                //修改
                case 0:
                    $(this).click(function () {
                        showChannelDialog(this);
                    });
                    break;
                //删除 
                case 1:
                    $(this).click(function () {
                        var obj = $(this);
                        $.ligerDialog.confirm("确定要删除吗？", "提示信息", function (result) {
                            if (result) {
                                obj.parent().parent().remove(); //删除节点
                            }
                        });
                    });
                    break;
            }
        });
    }

    //给URL配置首页赋值
    function itemSelect(obj) {
        var value = $(obj).children("option:selected").attr("value");
        if (value.length < 1) {
            return false;
        }
        var channelName = $("#txtName").val(); //获得频道名称
        if (channelName.length < 1) {
            return false;
        }
        //咨询是否使用默认设置
        $.ligerDialog.confirm("是否使用参考配置？做为参考，请根据自己实际情况调整。", "提示信息", function (result) {
            if (result) {
                switch (value) {
                    case "list":
                        $("#urlKey").val(channelName + "_list");
                        $("#urlExp").val(channelName + "/{0}/{1}.aspx");
                        $("#var_box").empty(); //清空旧数据
                        $("#var_box").append(createVarHtml()); //插入一行
                        $("#var_box tr:last").find("input[name='varName']").val("category_id");
                        $("#var_box tr:last").find("input[name='varExp']").val("(\\d+)");
                        $("#var_box").append(createVarHtml()); //插入一行
                        $("#var_box tr:last").find("input[name='varName']").val("page");
                        $("#var_box tr:last").find("input[name='varExp']").val("(\\w+)");
                        $("#pageTemplet").val(channelName + "_list");
                        $("#urlPage").val(channelName + "_list");
                        break;
                    case "detail":
                        $("#urlKey").val(channelName + "_show");
                        $("#urlExp").val(channelName + "/show/{0}.aspx");
                        $("#var_box").empty(); //清空旧数据
                        $("#var_box").append(createVarHtml()); //插入一行
                        $("#var_box tr:last").find("input[name='varName']").val("id");
                        $("#var_box tr:last").find("input[name='varExp']").val("(\\d+)");
                        $("#pageTemplet").val(channelName + "_show");
                        $("#urlPage").val(channelName + "_show");
                        break;
                    default:
                        $("#var_box").empty(); //清空旧数据
                        $("#urlKey").val(channelName);
                        $("#urlExp").val(channelName + ".aspx");
                        $("#pageTemplet").val(channelName);
                        $("#urlPage").val(channelName);
                        break;
                }
            }
        });
    }

    //===================================================================================
    //创建窗口
    function showChannelDialog(obj) {
        var objNum = arguments.length;
        var tabHtml = createChannelHtml();
        var m = $.ligerDialog.open({
            type: "",
            title: "URL重写配置",
            content: tabHtml,
            width: 700,
            buttons: [
            { text: '确认', onclick: function () {
                if (objNum > 0) {
                    execChannelDialog(m, obj);
                } else {
                    execChannelDialog(m);
                }
            }
            },
            { text: '关闭', onclick: function () {
                m.close();
            }
            }
            ],
            isResize: true
        });
        //检查是否修改状态
        if (objNum == 1) {
            //调用赋值函数
            setChannelDialog(obj);
        }
    }

    //创建HTML
    function createChannelHtml() {
        var tableHtml = '<table class="form_table">'
        + '<col width="130px"><col>'
        + '<tbody>'
        + '<tr>'
        + '<th>频道类型：</th>'
        + '<td><select id="pageType" class="select2" onchange="itemSelect(this);"><option value="">请选择类型...</option><option value="index">首页</option><option value="list">列表页</option><option value="detail">详细页</option></select></td>'
        + '</tr><tr>'
        + '<th>URL名称(Key)：</th>'
        + '<td><input type="text" id="urlKey" class="txtInput small ime-disabled" /><label>*调用的唯一标识，英文、数字、下划线</label></td>'
        + '</tr><tr>'
        + '<th>重写表达式：</th>'
        + '<td><input type="text" id="urlExp" class="txtInput small ime-disabled" style="width:210px;" /><label>*如：article-{0}-{1}.aspx，{n}表示第N个变量</label></td>'
        + '</tr><tr>'
        + '<th>传输参数：</th>'
        + '<td><button id="btnVarAdd" type="button" class="btnSelect" onclick="addVarTr();"><span class="add">增加变量</span></button></td>'
        + '</tr><tr>'
        + '<th>&nbsp;</th>'
        + '<td><table border="0" cellspacing="0" cellpadding="0" class="border_table" width="98%">'
        + '<thead><tr><th width="40%">变量名称</th><th>正则表达式</th><th width="3%">操作</th></tr></thead>'
        + '<tbody id="var_box">'
        + '</tbody></table></td>'
        + '</tr><tr>'
        + '<th>模板文件：</th><td><input type="text" id="pageTemplet" value="" class="txtInput small ime-disabled" />.html</td>'
        + '</tr><tr>'
        + '<th>生成的文件：</th><td><input type="text" id="urlPage" value="" class="txtInput small ime-disabled" />.aspx</td>'
        + '</tr>'
        + '</tbody>'
        + '</table>'
        return tableHtml;
    }
    //创建URL变量HTML
    function createVarHtml() {
        varHtml = '<tr>'
        + '<td><select class="select2" onchange="regChannelVal(this, \'varName\');"><option value="">@参考</option><option value="category_id">类别ID</option><option value="page">分页页码</option></select>'
        + '<input type="text" name="varName" class="txtInput small ime-disabled" /></td>'
        + '<td><select class="select2" onchange="regChannelVal(this, \'varExp\');"><option value="">@参考</option><option value="(\\w+)">字符串</option><option value="(\\d+)">数字</option></select>'
        + '<input type="text" name="varExp" class="txtInput small ime-disabled" style="width:160px;" /></td>'
        + '<td><img alt="删除" src="../images/icon_del.gif" class="operator" onclick="delVarTr(this);" /></td>'
        +'</tr>'
        return varHtml;
    }

    //添加一行变量
    function addVarTr() {
        varHtml = createVarHtml();
        $("#var_box").append(varHtml);
    }
    //删除一行变量
    function delVarTr(obj) {
        $(obj).parent().parent().remove();
    }

    //赋值参考选项
    function regChannelVal(obj, objName) {
        var value = $(obj).children("option:selected").attr("value");
        if (value != "") {
            $(obj).next("input[name='" + objName + "']").val(value);
        }
    }

    //赋值表单
    function setChannelDialog(obj) {
        var pobj = $(obj).parent().parent();
        var item_type = $(pobj).find("input[name='item_type']").val();
        $("#pageType option").each(function (i) {
            if ($(this).attr("value") == item_type) {
                $(this).attr("selected", "selected");
            }
        });
        $("#urlKey").val($(pobj).find("input[name='item_name']").val());
        $("#urlExp").val($(pobj).find("input[name='item_path']").val());
        $("#pageTemplet").val($(pobj).find("input[name='item_templet']").val().replace(".html", ""));
        $("#urlPage").val($(pobj).find("input[name='item_page']").val().replace(".aspx", ""));
        //分析正则表达式
        var strPath = $(pobj).find("input[name='item_path']").val().replace(new RegExp("{\\d+}", "g"), "(.*)"); //替换成正则表达式
        var strPattern = $(pobj).find("input[name='item_pattern']").val();
        var pathArr = strPattern.match(strPath);
        //开始赋值
        if ($(pobj).find("input[name='item_querystring']").val() != "") {
            var querystrArr = $(pobj).find("input[name='item_querystring']").val().split("^");
            for (i = 0; i < querystrArr.length; i++) {
                //插入一行TR并赋值变量
                var trObj = $("#var_box").append(createVarHtml());
                var strArr = querystrArr[i].split("=");
                $(trObj).children("tr").eq(i).find("input[name='varName']").val(strArr[0]);
                //赋值正则表达式
                $(trObj).children("tr").eq(i).find("input[name='varExp']").val(pathArr[i + 1]);
            }
        }
    }

    //最终赋值结果
    function setNavRow(obj) {
        $(obj).find("input[name='item_type']").val($("#pageType").children("option:selected").attr("value"));
        $(obj).find(".item_type").html($("#pageType").children("option:selected").html());
        $(obj).find("input[name='item_name']").val($("#urlKey").val());
        $(obj).find("input[name='item_path']").val($("#urlExp").val());
        //查找变量表达式
        var patternStr = $("#urlExp").val();
        var querystringStr = "";
        $("#var_box tr").each(function (i) {
            if ($(this).find("input[name='varName']").val() != "" && $(this).find("input[name='varExp']").val() != "") {
                patternStr = patternStr.replace("{" + i + "}", $(this).find("input[name='varExp']").val());
                querystringStr += $(this).find("input[name='varName']").val() + "=$" + (i + 1);
                if (i < $("#var_box tr").length - 1) {
                    querystringStr += "^";
                }
            }
        });
        $(obj).find("input[name='item_pattern']").val(patternStr);
        $(obj).find("input[name='item_querystring']").val(querystringStr);
        $(obj).find("input[name='item_templet']").val($("#pageTemplet").val() + ".html");
        $(obj).find("input[name='item_page']").val($("#urlPage").val() + ".aspx");
    }

    //检查Dialog输入情况
    function execChannelDialog(m, obj) {
        var oldkey = "";
        var objNum = arguments.length;
        if (objNum > 1) {
            oldkey = $(obj).parent().parent().find("input[name='item_name']").val();
        }
        if ($("#pageType").children("option:selected").attr("value") == "") {
            $.ligerDialog.warn('请选择频道类型！', function () {
                $("#pageType").focus();
            });
            return false;
        }
        if ($("#urlKey").val() == "") {
            $.ligerDialog.warn('请输入URL名称(Key)！', function () {
                $("#urlKey").focus();
            });
            return false;
        }
        if ($("#urlExp").val() == "") {
            $.ligerDialog.warn('请输入URL的重写表达式！', function () {
                $("#urlExp").focus();
            });
            return false;
        }
        if ($("#pageTemplet").val() == "") {
            $.ligerDialog.warn('请输入模板文件的名称！', function () {
                $("#pageTemplet").focus();
            });
            return false;
        }
        if ($("#urlPage").val() == "") {
            $.ligerDialog.warn('请输入生成ASPX文件的名称！', function () {
                $("#urlPage").focus();
            });
            return false;
        }
        //检查本地是否重复
        var checkKey = true;
        $("#item_box tr").each(function (i) {
            if ($("#urlKey").val() == $(this).find("input[name='item_name']").val() && $("#urlKey").val() != oldkey) {
                checkKey = false;
            }
        });
        if (!checkKey) {
            $.ligerDialog.warn('对不起，URL名称(Key)已重复啦！', function(){
                $("#urlKey").focus();
            });
            return false;
        }
        //AJAX验证
        var oldItemKey = "";
        if (objNum > 1) {
            if ($(obj).parent().parent().find("input[name='old_item_name']").length > 0) {
                oldItemKey = $(obj).parent().parent().find("input[name='old_item_name']").val();
            }
        }
        $.ajax({
            type: "post",
            url: "../../tools/admin_ajax.ashx?action=sys_urlrewrite_validate",
            data: {
                rewritekey: function () {
                    return $("#urlKey").val();
                },
                oldkey: function () {
                    return oldItemKey;
                }
            },
            dataType: "html",
            success: function (data, textStatus) {
                if (data == "true") {
                    if (objNum > 1) {
                        setNavRow($(obj).parent().parent());
                    } else {
                        //创建TR
                        var navSize = $('#item_box tr').size();
                        $("#item_box").append(getTr());
                        initButton(navSize);
                        setNavRow($("#item_box tr").eq(navSize));
                    }
                    m.close();
                } else {
                    $.ligerDialog.warn('对不起，AJAX检测URL名称(Key)已存在，若旧名称已更改，请保存后方可使用该名称！', function () {
                        $("#urlKey").focus();
                    });
                    return false;
                }
            }
        });
    }
</script>
</head>
<body class="mainbody">
<form id="form1" runat="server">
<div class="navigation"><a href="javascript:history.go(-1);" class="back">后退</a>首页 &gt; 控制面板 &gt; 系统频道</div>
<div id="navtips" class="navtips">
    编辑频道信息需具备基本的正则表达式知识，注意URL配置的名称不要重复，以下正则表达式可供参考：<br />
    约定参数：category_id为频道ID，page为分页页码；<br />
    <a href="javascript:CloseTip('navtips');" class="close">关闭</a>
</div>
<div id="contentTab">
    <ul class="tab_nav">
        <li class="selected"><a onclick="tabs('#contentTab',0);" href="javascript:;">编辑频道信息</a></li>
    </ul>
    <div class="tab_con" style="display:block;">
        <table class="form_table">
            <col width="180px"><col>
            <tbody>
            <tr>
                <th>频道名称：</th>
                <td>
                    <asp:HiddenField ID="hidName" runat="server" Value="" />
                    <asp:TextBox ID="txtName" runat="server" CssClass="txtInput normal ime-disabled"></asp:TextBox><label>*只允许英文字母、下划线，不可重复</label></td>
            </tr>
            <tr>
                <th>频道标题：</th>
                <td><asp:TextBox ID="txtTitle" runat="server" CssClass="txtInput normal required" minlength="2" maxlength="100"></asp:TextBox><label>*</label></td>
            </tr>
            <tr>
                <th>所属模型：</th>
                <td><asp:DropDownList id="ddlModelId" CssClass="select2 required" runat="server"></asp:DropDownList><label>*更改模型请手动删除旧内容</label></td>
            </tr>
            <tr>
                <th>排 序：</th>
                <td><asp:TextBox ID="txtSortId" runat="server" CssClass="txtInput normal small required digits"></asp:TextBox><label>*</label></td>
            </tr>
            <tr>
                <th>URL重写配置：</th>
                <td><button id="itemAddButton" type="button" class="btnSelect"><span class="add">添 加</span></button></td>
            </tr>
            <tr>
                <th></th>
                <td>
                    <table border="0" cellspacing="0" cellpadding="0" class="border_table" width="99%">
                        <thead>
                        <tr>
                            <th width="9%">类型</th>
                            <th width="15%">名称</th>
                            <th width="18%">URL重写</th>
                            <th width="18%">正则表达式</th>
                            <th width="18%">参数配置</th>
                            <th width="15%">模板文件</th>
                            <th width="5%">操作</th>
                        </tr>
                        </thead>
                        <tbody id="item_box">
                        <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                        <tr class="td_c">
                            <td>
                                <input type="hidden" name="item_type" value="<%#Eval("type")%>" />
                                <input type="hidden" name="item_page" value="<%#Eval("page")%>" />
                                <span class="item_type"><%#GetPageTypeName(Eval("type").ToString())%></span>
                            </td>
                            <td><input type="hidden" name="old_item_name" value="<%#Eval("name")%>" /><input name="item_name" type="text" value="<%#Eval("name")%>" class="txtInput small" style="width:98%;" readonly="readonly" /></td>
                            <td><input name="item_path" type="text" value="<%#Eval("path")%>" class="txtInput small3" style="width:98%;" readonly="readonly" /></td>
                            <td><input name="item_pattern" type="text" value="<%#Eval("pattern")%>" class="txtInput small3" style="width:98%;" readonly="readonly" /></td>
                            <td><input name="item_querystring" type="text" value="<%#Eval("querystring")%>" class="txtInput small3" style="width:98%;" readonly="readonly" /></td>
                            <td><input name="item_templet" type="text" value="<%#Eval("templet")%>" class="txtInput small" style="width:98%;" readonly="readonly" /></td>
                            <td><img alt="编辑" src="../images/icon_edit.gif" class="operator" /><img alt="删除" src="../images/icon_del.gif" class="operator" /></td>
                        </tr>
                        </ItemTemplate>
                        </asp:Repeater>
                        </tbody>
                    </table>
                </td>
            </tr>
            <tr>
                <th></th>
                <td><asp:Button ID="btnSubmit" runat="server" Text="提交保存" CssClass="btnSubmit" onclick="btnSubmit_Click" /></td>
            </tr>
            </tbody>
        </table>
    </div>
    
</div>
</form>
</body>
</html>
