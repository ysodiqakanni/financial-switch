<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TestWeb.Default" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register assembly="AppZoneUI.Framework" namespace="AppZoneUI.Framework" tagprefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
         <ext:ResourceManager ID="ResourceManager1" runat="server"></ext:ResourceManager>
        <%--<cc1:EntityUIControl ID="Lesson1UI" runat="server" UIType = "MySwitch.UI.Lesson1UI, MySwitch.UI" />--%>
        <cc1:EntityUIControl ID="SchemeUI" runat="server" UIType = "MySwitch.UI.SchemeUI.AddUI, MySwitch.UI" />
    </div>
    </form>
</body>
</html>
