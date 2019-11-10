<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserProfileFields.ascx.cs" Inherits="Forms45OIDCExample.Controls.UserProfileFields" %>
<script type="text/javascript">
       
    function changeSecureImage(source) {
          var img = document.getElementById("imgSecure");
        img.src = "/images/secure/" + source.value + ".png";
        } 

    </script>
<style>
    .font-bold {
        font-weight: bold;
    }
</style>
<div class="row">
    <div class="col-md-6">
        <asp:Label ID="lblFirstName" runat="server" Text="First Name" CssClass="text-primary font-bold" ></asp:Label>
        <asp:TextBox ID="tbFirstName" runat="server" CssClass="form-control"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvFirstName" ControlToValidate="tbFirstName" runat="server" Text="*" ErrorMessage="First name is required"></asp:RequiredFieldValidator>
    </div>
    <div class="col-md-6">
        <asp:Label ID="lblLastName" runat="server" Text="Last Name" CssClass="text-primary font-bold"></asp:Label>
        <asp:TextBox ID="tbLastName" runat="server" CssClass="form-control"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvLastName" ControlToValidate="tbLastName" runat="server" Text="*" ErrorMessage="Last name is required"></asp:RequiredFieldValidator>

    </div>
</div>
<div class="row">
    <div class="col-md-6">
        <asp:Label ID="lblLogin" runat="server" Text="Login (email format)" CssClass="text-primary font-bold "></asp:Label>
        <asp:TextBox ID="tbLoginEmail" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvLogin" ControlToValidate="tbLoginEmail" runat="server" Text="*" ErrorMessage="Login is required"></asp:RequiredFieldValidator>
        <br />
 
        <asp:Label ID="lblPrimaryEmail" runat="server" Text="Primary Email" CssClass="text-primary font-bold "></asp:Label>
        <asp:TextBox ID="tbPrimaryEmail" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvPrimaryEmail" ControlToValidate="tbPrimaryEmail" runat="server" Text="*" ErrorMessage="Primary email is required"></asp:RequiredFieldValidator>
         <br />
        <asp:Label ID="lblSecondary" runat="server" Text="SecondaryEmail"></asp:Label>
        <asp:TextBox ID="tbSecondaryEmail" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
         <br />
        <asp:Label ID="lblMobilePhone" runat="server" Text="Mobile Phone" CssClass="text-primary font-bold"></asp:Label>
        <asp:TextBox ID="tbMobilePhone" runat="server" CssClass="form-control" TextMode="Phone"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvMobilePhone" ControlToValidate="tbMobilePhone" runat="server" Text="*" ErrorMessage="Mobile Phone is required"></asp:RequiredFieldValidator>
        <%--<asp:HyperLink ID="lnkEnrollSMS" NavigateUrl="~/EnrollFactor.aspx" Text="Enroll in SMS Verification" runat="server"></asp:HyperLink>--%>

    </div>
    <div class="col-md-6">
        <asp:Panel ID="pnlPasswordOps" runat="server">
            <asp:Label ID="lblPassword" runat="server" Text="Password" CssClass="text-primary font-bold"></asp:Label>
            <asp:TextBox ID="tbPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvPassword" ControlToValidate="tbPassword" runat="server" Text="*" ErrorMessage="Password is required"></asp:RequiredFieldValidator>
            <br />
            <asp:Label ID="lblConfirm" runat="server" Text="Confirm Password" CssClass="text-primary font-bold"></asp:Label>
            <asp:TextBox ID="tbConfirmPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvConfirmPassword" ControlToValidate="tbConfirmPassword" runat="server" Text="*" ErrorMessage="Confirm Password is required"></asp:RequiredFieldValidator>
            <asp:CompareValidator runat="server" ID="comvPasswords" Text="!" ErrorMessage="Passwords do no match" ControlToValidate="tbPassword" ControlToCompare="tbConfirmPassword"></asp:CompareValidator>
            <!-- <span class="text-muted">To change your password, complete these fields</span> -->
        </asp:Panel>
        
        <asp:HyperLink ID="hyChangePassword" runat="server" Text="Change Password" NavigateUrl="~/ChangePassword.aspx"></asp:HyperLink>
    </div>
</div>
<div class="row">
    <div class="col-md-6">
 
    </div>
    <div class="col-md-6">

    </div>
</div>
<h3>Additional Attributes</h3>

<div class="row">
    <asp:HiddenField ID="hidId" runat="server" />

    <div class="col-md-6">
        <asp:Label ID="lblSecureWord" runat="server" Text="Secure Word" CssClass="text-primary font-bold"></asp:Label>
        <asp:TextBox ID="rbSecureWord" runat="server" CssClass="form-control"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvSecureWord" ControlToValidate="rbSecureWord" runat="server" Text="*" ErrorMessage="Secure word is required"></asp:RequiredFieldValidator>

    </div>

    <div class="col-md-6">

    </div>
</div>
<div class="row">
    <div class="col-md-6">
        <asp:Label ID="lblSecureImage" runat="server" Text="Secure Image" CssClass="text-primary font-bold"></asp:Label>

        <asp:DropDownList ID="ddlSecureImages" runat="server" CssClass="form-control" OnChange="changeSecureImage(this)" >
            <asp:ListItem Text="Duck" Enabled="true" Value="duck" Selected="True"></asp:ListItem>
            <asp:ListItem Text="Truck" Enabled="true" Value="truck" ></asp:ListItem>
            <asp:ListItem Text="Ball" Enabled="true" Value="ball" ></asp:ListItem>
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rbSecureWord" runat="server" Text="*" ErrorMessage="Secure image is required"></asp:RequiredFieldValidator>

    </div>
    <div class="col-md-6">
        <img src="/images/secure/<%= this.ddlSecureImages.SelectedValue%>.png" alt=""  id="imgSecure"/>

    </div>

</div>
 