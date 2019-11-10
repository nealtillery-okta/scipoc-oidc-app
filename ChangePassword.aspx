<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="Forms45OIDCExample.ChangePassword" %>
<%@ MasterType VirtualPath="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <div class="container">
 <div class="row">
        <div class="col-md-2">

        </div>
        <div class="col-md-6">

            <div class="panel panel-info">
                <div class="panel-heading">
                    <h2>Change Password</h2>
                </div>
                <div class="panel-body">
                     <div class="col-md-6">
                         <asp:ValidationSummary ID="vsErrors" runat="server" HeaderText="Please review the following" CssClass="text-danger" />
                         <asp:Label ID="lblCurrentPassword" runat="server" Text="Current Password" CssClass="text-primary font-bold"></asp:Label>
                        <asp:TextBox ID="tbCurrentPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvCurrentPassword" ControlToValidate="tbCurrentPassword" runat="server" Text="*" ErrorMessage="Current password is required"></asp:RequiredFieldValidator>
                        <br />
                        <asp:Label ID="lblPassword" runat="server" Text="Password" CssClass="text-primary font-bold"></asp:Label>
                        <asp:TextBox ID="tbPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvPassword" ControlToValidate="tbPassword" runat="server" Text="*" ErrorMessage="Password is required"></asp:RequiredFieldValidator>
                        <br />
                        <asp:Label ID="lblConfirm" runat="server" Text="Confirm Password" CssClass="text-primary font-bold"></asp:Label>
                        <asp:TextBox ID="tbConfirmPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvConfirmPassword" ControlToValidate="tbConfirmPassword" runat="server" Text="*" ErrorMessage="Confirm Password is required"></asp:RequiredFieldValidator>
                        <asp:CompareValidator runat="server" ID="comvPasswords" Text="!" ErrorMessage="Passwords do no match" ControlToValidate="tbPassword" ControlToCompare="tbConfirmPassword"></asp:CompareValidator>
                    </div>
            </div>
       
                <div class="panel-footer">
                    <asp:Button  ID="btnCancel" CssClass="btn btn-warning" Text="Cancel" CausesValidation="false" runat="server" OnClick="btnCancel_Click"/>&nbsp;
                    <asp:Button ID="btnSubmit" CssClass="btn btn-primary" Text="Change Password" runat="server" OnClick="btnSubmit_Click" />
                </div>
            </div>
        </div>

        <div class="col-md-2"></div>
    </div>
    </div>
   


</asp:Content>
