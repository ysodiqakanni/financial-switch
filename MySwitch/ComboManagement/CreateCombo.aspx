﻿<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="CreateCombo.aspx.cs" Inherits="MySwitch.SchemeManagement.CreateCombo" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register assembly="AppZoneUI.Framework" namespace="AppZoneUI.Framework" tagprefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <ext:ResourceManager ID="ResourceManager2" runat="server"></ext:ResourceManager> 
        <cc1:EntityUIControl ID="EntityUIControl1" runat="server" UIType = "MySwitch.UI.SchemeUI.CreateComboUI, MySwitch.UI" />
   
</asp:Content>