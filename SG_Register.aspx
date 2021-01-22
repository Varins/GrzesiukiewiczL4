<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SG_Register.aspx.cs" Inherits="GrzesiukiewiczL4.SG_Register" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container ">
        <div class="col-md-5 col-md-offset-5">
            <div class="jumbotron">
                <asp:CreateUserWizard ID="CreateUserWizard1" runat="server" RequireEmail="False" OnCreatedUser="CreateUserWizard1_CreatedUser">
                    <WizardSteps>
                        <asp:CreateUserWizardStep ID="CreateUserWizardStep1" runat="server">
                            <ContentTemplate>
                                <h3>Stwórz nowe konto</h3>
                                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Nazwa użytkownika:</asp:Label>
                                <br />
                                <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                    ErrorMessage="Nazwa użytkownika jest wymagana." ToolTip="Nazwa użytkownika jest wymagana." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                                <br />
                                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Hasło:</asp:Label>
                                <br />
                                <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                    ErrorMessage="Hasło jest wymagane." ToolTip="Hasło jest wymagane." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                                <br />
                                <asp:Label ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="ConfirmPassword">Podaj ponownie hasło:</asp:Label>
                                <br />
                                <asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ControlToValidate="ConfirmPassword"
                                    ErrorMessage="Ponowne wpisanie hasła jest wymagane." ToolTip="Ponowne wpisanie hasła jest wymagane." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                                <br />
                                <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword"
                                    Display="Dynamic" ErrorMessage="Hasła muszą się zgadzać." ValidationGroup="CreateUserWizard1"></asp:CompareValidator>
                                <asp:Literal ID="ErrorMessage" runat="server" EnableViewState="False"></asp:Literal>
                                <br />
                            </ContentTemplate>
                           
                            <CustomNavigationTemplate>
                                <table border="0" cellspacing="5" style="width:100%;height:100%;">
                                    <tr align="left">
                                        <td align="left" colspan="0">
                                            <asp:Button CssClass="btn btn-outline-primary fixed-right" ID="StepNextButton" runat="server" CommandName="MoveNext" Text="Stwórz konto" ValidationGroup="CreateUserWizard1" />
                                        </td>
                                    </tr>
                                </table>
                            </CustomNavigationTemplate>
                           
                        </asp:CreateUserWizardStep>
                        <asp:CompleteWizardStep ID="CompleteWizardStep1" runat="server">
                        </asp:CompleteWizardStep>
                    </WizardSteps>
                </asp:CreateUserWizard>
            </div>
        </div>
    </div>
</asp:Content>
