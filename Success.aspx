<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Success.aspx.cs" Inherits="Forms45OIDCExample.Success" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Success!</h1>
    <p>
        Your account has been created successfully.<br />
        You may now
        <asp:HyperLink ID="hyperlink1"
            NavigateUrl="~/LoginFlow"
            Text="Log In"
            runat="server" />
    </p>
</asp:Content>
