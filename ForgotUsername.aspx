<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ForgotUsername.aspx.cs" Inherits="Forms45OIDCExample.ForgotUsername" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <div class="container">
 <div class="row">
        <div class="col-md-2">

        </div>
        <div class="col-md-6">

            <div class="panel panel-info">
                <div class="panel-heading">
                    <h2>Forgot Username</h2>
                </div>
                <div class="panel-body">
                     <div class="col-md-6">
                         <asp:Label ID="lblMessage" runat="server" CssClass="text-primary font-bold"></asp:Label><br />
                         <asp:ValidationSummary ID="vsErrors" runat="server" HeaderText="Please review the following" CssClass="text-danger" />
                         <asp:Label ID="lblEmailAddress" runat="server" Text="Email Address" CssClass="text-primary font-bold"></asp:Label>
                        <asp:TextBox ID="tbEmailAddress" runat="server" CssClass="form-control" TextMode="SingleLine"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvEmailAddress" ControlToValidate="tbEmailAddress" runat="server" Text="*" ErrorMessage="Your email address is required to recover your username"></asp:RequiredFieldValidator>
                    </div>
            </div>
       
                <div class="panel-footer">
                    <asp:Button  ID="btnCancel" CssClass="btn btn-warning" Text="Cancel" CausesValidation="false" runat="server" OnClick="btnCancel_Click"/>&nbsp;
                    <asp:Button ID="btnSubmit" CssClass="btn btn-primary" Text="Submit" runat="server" OnClick="btnSubmit_Click" />
                </div>
            </div>
        </div>

        <div class="col-md-2"></div>
    </div>
    </div>
   


</asp:Content>
