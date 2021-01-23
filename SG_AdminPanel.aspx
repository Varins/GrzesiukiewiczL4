<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SG_AdminPanel.aspx.cs" Inherits="GrzesiukiewiczL4.SG_AdminPanel" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="SG_username_label" runat="server" Text="" Visible="false"></asp:Label>
    <div class="d-inline-flex flex-column">
        <div class="pt-5">
            <asp:Label ID="SG_infoLabel" runat="server" Text="" Visible="false"></asp:Label>
        </div>
        <div class="jumbotron d-inline-flex justify-content-center">
            <asp:ListView ID="SG_userList" runat="server" DataKeyNames="UserName" DataSourceID="SG_sourceUsers">
                <EmptyDataTemplate>
                    <table runat="server" class="table">
                        <tr>
                            <td>Nie ma jeszcze użytkowników.</td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:Label ID="UserNameLabel" runat="server" Text='<%# Eval("UserName") %>' />
                        </td>
                        <td>
                            <asp:LinkButton ID="SG_LinkButton_register" CommandName="register" CommandArgument='<%# Eval("UserName") %>' runat="server" Text="Zatwierdź"
                                CssClass="btn btn-outline-secondary" OnCommand="SG_userList_RowCommand"></asp:LinkButton>
                        </td>
                        <td>
                            <asp:LinkButton ID="SG_LinkButton_deregister" CommandName="deregister" CommandArgument='<%# Eval("UserName") %>' runat="server" Text="Anuluj"
                                CssClass="btn btn-outline-secondary" OnCommand="SG_userList_RowCommand"></asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
                <LayoutTemplate>
                    <table runat="server">
                        <tr runat="server">
                            <td runat="server">
                                <table id="itemPlaceholderContainer" runat="server" border="0" class="table table-bordered">
                                    <tr runat="server" class="thead-dark">
                                        <th runat="server">Nazwa użytkownika</th>
                                        <th runat="server">Zalogowanie</th>
                                        <th runat="server">Anulowanie</th>
                                    </tr>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr runat="server">
                            <td runat="server" style="">
                                <asp:DataPager ID="DataPager1" runat="server">
                                    <Fields>
                                        <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowLastPageButton="True" ButtonCssClass="btn btn-outline-secondary btn-sm" />
                                    </Fields>
                                </asp:DataPager>
                            </td>
                        </tr>
                    </table>
                </LayoutTemplate>
                <SelectedItemTemplate>
                    <tr style="">
                        <td>
                            <asp:Label ID="UserNameLabel" runat="server" Text='<%# Eval("UserName") %>' />
                        </td>
                    </tr>
                </SelectedItemTemplate>

            </asp:ListView>
            <asp:SqlDataSource ID="SG_sourceUsers" runat="server" ConnectionString="<%$ ConnectionStrings:SG_DB %>"
                SelectCommand="SELECT [UserName] FROM [vw_aspnet_Users] WHERE ([UserName] NOT LIKE '%' + @UserName + '%')">
                <SelectParameters>
                    <asp:ControlParameter ControlID="SG_username_label" Name="UserName" PropertyName="Text" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
        </div>

    </div>
</asp:Content>
