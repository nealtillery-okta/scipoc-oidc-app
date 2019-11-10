<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EnrollFactor.aspx.cs" Inherits="Forms45OIDCExample.EnrollFactor" %>
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
                        <h2>Activate SMS Two-Factor Authentication</h2>
                    </div>
                    <div class="panel-body">
                        <div class="col-md-6">
                            <asp:ValidationSummary ID="vsErrors" runat="server" HeaderText="Please review the following" CssClass="text-danger" />
                            <asp:Label ID="lblSmsCode" runat="server" Text="Enter your OTP" CssClass="text-primary font-bold"></asp:Label>
                            <asp:TextBox ID="tbSmsCode" runat="server" CssClass="form-control" TextMode="SingleLine"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvSmsCode" ControlToValidate="tbSmsCode" runat="server" Text="*" ErrorMessage="OTP is required"></asp:RequiredFieldValidator>

                            <asp:HiddenField ID="tbFactorID" runat="server"></asp:HiddenField>
                            <asp:Label ID="lblMessage" runat="server" CssClass="text-primary font-bold"></asp:Label>
                        </div>
                    </div>

                    <div class="panel-footer">
                        <asp:Button ID="btnDone" CssClass="btn btn-primary" Text="Done" runat="server" OnClick="btnDone_Click" />
                        <asp:Button ID="btnVerifySms" CssClass="btn btn-primary" Text="Verify" runat="server" OnClick="btnVerifySms_Click" />
                        <asp:Button ID="btnCancel" CssClass="btn btn-warning" Text="Cancel" CausesValidation="false" runat="server" OnClick="btnCancel_Click" />
                    </div>
                </div>
            </div>

            <div class="col-md-2"></div>
        </div>
    </div>



</asp:Content>
