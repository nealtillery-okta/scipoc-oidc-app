<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="Forms45OIDCExample.ForgotPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <div class="container">
        <div class="row">
            <div class="col-md-2">
            </div>
            <div class="col-md-6">

                <div class="panel panel-info">
                    <div class="panel-heading">
                        <h2>Recover Your Password</h2>
                    </div>
                    <div class="panel-body">
                        <div class="col-md-6">
                            <asp:ValidationSummary ID="vsErrors" runat="server" HeaderText="Please review the following" CssClass="text-danger" />
                            <asp:Label ID="lblUsername" runat="server" Text="Username" CssClass="text-primary font-bold"></asp:Label>
                            <asp:TextBox ID="tbUsername" runat="server" CssClass="form-control" TextMode="SingleLine"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvUsername" ControlToValidate="tbUsername" runat="server" Text="*" ErrorMessage="Your username is required to reset your password"></asp:RequiredFieldValidator>

                            <asp:Label ID="lblSmsCode" runat="server" Text="Enter your OTP" CssClass="text-primary font-bold"></asp:Label>
                            <asp:TextBox ID="tbSmsCode" runat="server" CssClass="form-control" TextMode="SingleLine"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator ID="rfvSmsCode" ControlToValidate="tbSmsCode" runat="server" Text="*" ErrorMessage="OTP is required"></asp:RequiredFieldValidator>--%>

                            <asp:Label ID="lblMessage" runat="server" CssClass="text-primary font-bold"></asp:Label>
                        </div>
                    </div>

                    <div class="panel-footer">
                        <asp:Button ID="btnVerifySms" CssClass="btn btn-primary" Text="Verify" runat="server" OnClick="btnVerifySms_Click" />
                        <asp:Button ID="btnResetEmail" CssClass="btn btn-primary" Text="Reset via Email" runat="server" OnClick="btnResetEmail_Click" />
                        <asp:Button ID="btnResetSms" CssClass="btn btn-primary" Text="Reset via SMS" runat="server" OnClick="btnResetSms_Click" />
                        <asp:Button ID="btnCancel" CssClass="btn btn-warning" Text="Cancel" CausesValidation="false" runat="server" OnClick="btnCancel_Click" />
                    </div>
                </div>
            </div>

            <div class="col-md-2"></div>
        </div>
    </div>



</asp:Content>
