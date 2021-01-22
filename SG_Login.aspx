<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SG_Login.aspx.cs" Inherits="GrzesiukiewiczL4.SG_Login" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container vertical-center">
        <div class="col-md-5 col-md-offset-3">
            <div class="jumbotron">
                <asp:LoginView ID="LoginView1" runat="server">
                        <AnonymousTemplate>
                            <asp:Login ID="SG_LoginPanel" runat="server">
                                <LayoutTemplate>
                                        <h3>Logowanie</h3>
                                        <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Nazwa użytkownika:</asp:Label>
                                        <br />
                                        <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                            ErrorMessage="Nazwa użytkownika jest wymagana." ToolTip="Nazwa użytkownika jest wymagana." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                        <br />
                                        <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Hasło:	</asp:Label>
                                        <br />
                                        <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                            ErrorMessage="Hasło jest wymagane." ToolTip="Hasło jest wymagane." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                        <br />
                                        <asp:Label ID="RememberMeLabel" runat="server" AssociatedControlID="RememberMe">Zapamiętaj mnie.</asp:Label>
                                        <asp:CheckBox ID="RememberMe" runat="server"/>
                                        <br />
                                        <span style="color: red">
                                            <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                        </span>
                                        <br />
                                        <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Zaloguj" ValidationGroup="Login1" CssClass="btn btn-primary" />
                                </LayoutTemplate>
                            </asp:Login>
                        </AnonymousTemplate>
                        <LoggedInTemplate>
                            <h2>Już zalogowany!</h2>
                        </LoggedInTemplate>
                    </asp:LoginView>
            </div>
        </div>

    </div>


</asp:Content>
