<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Forms45OIDCExample.Register" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Src="~/Controls/UserProfileFields.ascx" TagPrefix="uc1" TagName="UserProfileFields" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Create Your Account</h1>
    <asp:ValidationSummary ID="vsMain" runat="server" DisplayMode="BulletList" />
    <uc1:userprofilefields runat="server" id="UserProfileFields" />

    <asp:Button id="btnCancel" runat="server" Text="Cancel" CausesValidation="false" OnClick="btnCancel_Click" CssClass="btn btn-danger" />
    <asp:Button ID="btnSubmit" runat="server" Text="Save" CssClass="btn btn-success" OnClick="btnSubmit_Click" />
</asp:Content>
