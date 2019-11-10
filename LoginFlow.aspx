<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LoginFlow.aspx.cs" Inherits="Forms45OIDCExample.LoginFlow" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .login-panel {min-width:400px; 
        }
        .jumbotron {
  background-image: url("../images/splash.jpg");
  background-size: cover;
}
    .panel {
   background-color: rgba(245, 245, 245, 0.9);
}
    .hidden
{
  display:none;
}
    </style>
    <div class="jumbotron">
        <div class="row">
            <div class="col-md-6">

            </div>
            <div class="col-md-4">
              <div class="panel panel-default login-panel" >

                <div class="panel-heading"><h2>Sign into Site</h2></div>
                    <div class="panel-body">
                       <asp:ValidationSummary ID="vsErrorSummary" DisplayMode="BulletList" HeaderText="Please review:" CssClass="bg-danger" runat="server" />
                       <asp:Wizard ID="wizLoginFlow" runat="server" DisplaySideBar="false" DisplayCancelButton="false"  OnActiveStepChanged="wizLoginFlow_ActiveStepChanged">
                            <NavigationButtonStyle CssClass="btn btn-primary" />
                            <StartNextButtonStyle CssClass="btn btn-primary" />
                            <FinishCompleteButtonStyle CssClass="btn btn-primary" />
                            <NavigationStyle CssClass="gauze" />
                            <StepPreviousButtonStyle CssClass="hidden" />
                            <WizardSteps>
                                <asp:WizardStep ID="stepLogin" StepType="Start">
                          
                                    <form>
                                        <div class="form-group">
                                            <asp:Label ID="lblLogin" Text="Login" runat="server" CssClass="" ></asp:Label>
                                            <asp:TextBox id="tbLoginName" placeholder="Your Login Name" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfLogin" ControlToValidate="tbLoginName" Text="Required" CssClass="text-danger" ErrorMessage="Login name is required" runat="server"></asp:RequiredFieldValidator>
                                        </div>
                                    </form>
                                          

                                </asp:WizardStep>
                                <asp:WizardStep StepType="Step" ID="stepSecureImage">
                            
                                    <div class="row">
                                        <div class="col-md-6">
                                            <asp:Image ID="imgSecureImage" ImageUrl="https://dummyimage.com/128x128/000/fff.png" runat="server" />
                                        </div>
                                        <div class="col-md-6">
                                            <strong>Secure Word</strong>
                                            <br />
                                            <span class="label label-warning">
                                                <asp:Label ID="lblSecureWord" Text="SecureWord" runat="server" ></asp:Label>
                                            </span>
                                        </div>
                                    </div>
                                    <br />
                                    <asp:Button ID="btnNotYou" CssClass="btn btn-danger" OnClick="btnNotYou_Click" Text="Not You?" runat="server" />
                                                
                                         
                                </asp:WizardStep>
                                <asp:WizardStep StepType="Step" ID="stepPassword">
                             
                                    <form>
                                        <div class="form-group">
                                            <asp:Label ID="lblPassword" Text="Password" runat="server" ></asp:Label>
                                            <asp:TextBox id="tbPassword" TextMode="Password"  runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvPassword" ControlToValidate="tbPassword" Text="Required" ErrorMessage="Password is required" CssClass="text-danger" runat="server"></asp:RequiredFieldValidator>
                                            <br /><span class="text-muted">Please click next only once and wait for the screen to return.</span>
                                        </div>
                                    </form>
                                          
                                </asp:WizardStep>
                                <asp:WizardStep StepType="Step" ID="FinalStep">
                                    <asp:Label ID="lblCompletion" runat="server"></asp:Label>
                                   
                                </asp:WizardStep>
                        
                            </WizardSteps>

                        </asp:Wizard>
                        <asp:CustomValidator ID="cvGeneric" runat="server" Display="None" ></asp:CustomValidator>
                        <span class="glyphicon glyphicon-user"></span>&nbsp;<asp:HyperLink ID="lnkRegister" runat="server" Text="New user?" NavigateUrl="~/Register.aspx"></asp:HyperLink><br />
                        <asp:HyperLink ID="lnkForgotUsername" runat="server" Text="Forgot Username" NavigateUrl="~/ForgotUsername.aspx"></asp:HyperLink> - 
                        <asp:HyperLink ID="lnkForgotPassword" runat="server" Text="Forgot Password" NavigateUrl="~/ForgotPassword.aspx"></asp:HyperLink>
                </div>
            </div>
                
            </div>
        </div>
    </div>
</asp:Content>
