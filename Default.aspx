<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Forms45OIDCExample._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Demonstration</h1>
        <p class="lead">OpenID Connect extends OAuth 2.0. The OAuth 2.0 protocol provides API security via scoped access tokens, and OpenID Connect provides user authentication and single sign-on (SSO) functionality.</p>
        <p><a href="https://developer.okta.com/docs/reference/api/oidc/" class="btn btn-primary btn-lg">Learn more &raquo;</a></p>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>OAuth 2.0 and OpenID Connect</h2>
            <p>
                This page will give you an overview of OAuth 2.0 and OpenID Connect and their Okta implementations. It will explain the different flows, and help you decide which flow is best for you based on the type of application that you are building. If you already know what kind of flow you want, you can jump directly to:
            </p>
            <p>
                <a class="btn btn-default" href="https://developer.okta.com/docs/concepts/auth-overview/">Learn more &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Choosing an OAuth 2.0 Flow </h2>
            <p>
                Depending on your use case, you will need to use a different OAuth flow. Below you will find a table that maps application types to our recommended OAuth 2.0 flows. If you'd like more information, keep reading for help choosing an OAuth flow based on (1) the type of token you need, and/or (2) the type of client application that you are building.
            </p>
            <p>
                <a class="btn btn-default" href="https://developer.okta.com/docs/concepts/auth-overview/#choosing-an-oauth-2-0-flow">Learn more &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Authorization Code Flow</h2>
            <p>
                The Authorization Code flow is best used by server-side apps where the source code is not publicly exposed. The apps should be server-side because the request that exchanges the authorization code for a token requires a client secret, which will have to be stored in your client. The server-side app requires an end-user, however, because it relies on interaction with the end-user's web browser which will redirect the user and then receive the authorization code.


            </p>
            <p>
                <a class="btn btn-default" href="https://developer.okta.com/docs/concepts/auth-overview/#authorization-code-flow">Learn more &raquo;</a>
            </p>
        </div>
    </div>

</asp:Content>
