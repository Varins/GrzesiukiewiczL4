<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SG_MainPage.aspx.cs" Inherits="GrzesiukiewiczL4.SG_MainPage" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron d-flex justify-content-center">
        <asp:ListView ID="SG_fileListView" runat="server" DataKeyNames="SG_filename" DataSourceID="SG_fileSource">
            <EmptyDataTemplate>
                <table runat="server" class="table">
                    <tr>
                        <td>Nie przesłano jeszcze żadnego pliku</td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <asp:Label ID="SG_filenameLabel" runat="server" Text='<%# Eval("SG_filename") %>' />
                    </td>
                    <td>
                        <asp:Label ID="SG_ownerLabel" runat="server" Text='<%# Eval("SG_owner") %>' />
                    </td>
                    <td>
                        <asp:Label ID="SG_uploadTimeLabel" runat="server" Text='<%# Eval("SG_uploadTime") %>' />
                    </td>
                    <asp:LoginView ID="LoginView1" runat="server">
                        <LoggedInTemplate>
                            <td>
                                <asp:LinkButton ID="SG_LinkButton_download" CommandName="download" CommandArgument='<%# Eval("SG_filename") %>' runat="server" Text="Pobierz"
                                    CssClass="btn btn-outline-secondary" OnCommand="SG_LinkButton_download_Command"></asp:LinkButton>
                            </td>
                        </LoggedInTemplate>
                    </asp:LoginView>
                </tr>

            </ItemTemplate>
            <LayoutTemplate>
                <table runat="server">
                    <tr runat="server">
                        <td runat="server">
                            <table id="itemPlaceholderContainer" runat="server" border="0" class="table table-bordered">
                                <tr runat="server" class="thead-dark">
                                    <th runat="server">Nazwa pliku</th>
                                    <th runat="server">Właściciel</th>
                                    <th runat="server">Data dodania</th>
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
                                    <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowLastPageButton="True" ButtonCssClass="btn btn-outline-secondary btn-sm"/>
                                </Fields>
                            </asp:DataPager>
                        </td>
                    </tr>
                </table>
            </LayoutTemplate>
            <SelectedItemTemplate>
                <tr style="">
                    <td>
                        <asp:Label ID="SG_filenameLabel" runat="server" Text='<%# Eval("SG_filename") %>' />
                    </td>
                    <td>
                        <asp:Label ID="SG_ownerLabel" runat="server" Text='<%# Eval("SG_owner") %>' />
                    </td>
                    <td>
                        <asp:Label ID="SG_uploadTimeLabel" runat="server" Text='<%# Eval("SG_uploadTime") %>' />
                    </td>
                </tr>
            </SelectedItemTemplate>
            
        </asp:ListView>
        <asp:SqlDataSource ID="SG_fileSource" runat="server" ConnectionString="<%$ ConnectionStrings:SG_DB %>" 
            SelectCommand="SELECT [SG_filename], [SG_uploadTime], [SG_owner] FROM [SG_filelist]">
        </asp:SqlDataSource>
    </div>
</asp:Content>