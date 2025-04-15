<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MainForm.aspx.cs" Inherits="CIS_305_Master_Web_Project.Demos.MyFlaglerWeb.MainForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Personnel Management</h1>
    <p>
        <table>
            <tr>
                <td style="width: 200px">Person Type</td>
                <td style="width: 800px">
                    <asp:RadioButtonList ID="rblPersonType" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rblPersonType_SelectedIndexChanged">
                        <asp:ListItem>Professor</asp:ListItem>
                        <asp:ListItem>Student</asp:ListItem>
                        <asp:ListItem>Staff</asp:ListItem>
                    </asp:RadioButtonList>

                </td>
            </tr>
            <tr>
                <td>Basic Information</td>
                <td>
                    <table>
                        <tr>
                            <td style="width: 150px;">Name: </td>
                            <td style="width: 400px;">
                                <asp:TextBox runat="server" ID="txtName"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>ID: </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtID"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Email:</td>
                            <td>
                                <asp:TextBox runat="server" ID="txtEmail"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>

            <asp:Panel ID="pnlProfessor" runat="server">
                <tr>
                    <td>Professor Information</td>
                    <td>
                        <table>
                            <tr>
                                <td style="width: 150px;">Department:</td>
                                <td style="width: 400px;">
                                    <asp:TextBox runat="server" ID="TextBox1"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Research Area: </td>
                                <td>
                                    <asp:TextBox runat="server" ID="TextBox2"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Email:</td>
                                <td>
                                    <asp:TextBox runat="server" ID="TextBox3"></asp:TextBox>
                                </td>
                            </tr>
                        </table>

                    </td>
                </tr>
            </asp:Panel>

            <asp:Panel ID="pnlStudent" runat="server">
                <tr>
                    <td>Student Information</td>
                    <td>
                        <table>
                            <tr>
                                <td style="width: 150px;">Major:</td>
                                <td style="width: 400px;">
                                    <asp:TextBox runat="server" ID="TextBox4"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>GPA: </td>
                                <td>
                                    <asp:TextBox runat="server" ID="TextBox5"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Is Full Time?</td>
                                <td>
                                    <asp:TextBox runat="server" ID="TextBox6"></asp:TextBox>
                                </td>
                            </tr>
                        </table>

                    </td>
                </tr>
            </asp:Panel>

            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </p>
    <p>&nbsp;</p>



</asp:Content>
