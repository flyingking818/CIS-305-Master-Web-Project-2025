<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="WebApplication_Test2025.Demos.MyFlagler.Main" %>



<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Person Management</h1>
    <asp:Panel ID="PanelForm" runat="server">
    <table>
        <tr>
            <td style="width: 200px">Person Type</td>
            <td style="width: 800px">
                <asp:RadioButtonList runat="server" ID="rblPerson" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblPerson_SelectedIndexChanged" AutoPostBack="true">
                    <asp:ListItem>Professor</asp:ListItem>
                    <asp:ListItem>Student</asp:ListItem>
                    <asp:ListItem>Staff</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td>Basic Information</td>
            <td>&nbsp;<table>
                <tr>
                    <td style="width: 150px">&nbsp;Name: </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtName"></asp:TextBox>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;ID:</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtID"></asp:TextBox>&nbsp;</td>
                </tr>
                <tr>
                    <td>Email:&nbsp;</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtEmail"></asp:TextBox></td>
                </tr>

            </table>
            </td>
        </tr>
       
        <asp:Panel ID="pnlProfessor" runat="server">
            <tr>

                <td>Professor Information:</td>
                <td>
                    <table>
                        <tr>
                            <td style="width: 150px">Department: </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlDepartment">
                                    <asp:ListItem>---Please Select---</asp:ListItem>
                                    <asp:ListItem Value="MAT">Math &amp; Technology</asp:ListItem>
                                    <asp:ListItem Value="BA">Business Adminstration</asp:ListItem>
                                    <asp:ListItem Value="FA">Fine Arts</asp:ListItem>
                                </asp:DropDownList>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>Research Area:</td>
                            <td>
                                <asp:TextBox runat="server" ID="txtResearchArea"></asp:TextBox>&nbsp;</td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:CheckBox runat="server" ID="chkTerminalDegree" Text="Terminal Degree?"></asp:CheckBox>
                            </td>
                        </tr>

                    </table>


                </td>
            </tr>
        </asp:Panel>
        <asp:Panel ID="pnlStudent" runat="server">
            <tr>

                <td>Student Information:</td>
                <td>
                    <table>
                        <tr>
                            <td style="width: 150px">Major: </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlMajor">
                                    <asp:ListItem>---Please Select---</asp:ListItem>
                                    <asp:ListItem Value="CIS">Computer Information Systems</asp:ListItem>
                                    <asp:ListItem Value="BA">Business Adminstration</asp:ListItem>
                                    <asp:ListItem Value="FA">Fine Arts</asp:ListItem>
                                    <asp:ListItem Value="Acc">Accounting</asp:ListItem>
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>GPA:</td>
                            <td>
                                <asp:TextBox runat="server" ID="txtGPA"></asp:TextBox>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>Enrollment Date:</td>
                            <td>
                                <asp:TextBox runat="server" ID="txtEnrollmentDate"></asp:TextBox>&nbsp;</td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:CheckBox ID="chkFullTime" Text="Full Time?" runat="server" />
                            </td>
                        </tr>

                    </table>


                </td>
            </tr>
        </asp:Panel>
        <asp:Panel ID="pnlStaff" runat="server">
            <tr>

                <td>Staff Information:</td>
                <td>
                    <table>
                        <tr>
                            <td style="width: 150px">Position: </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtPosition"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Division:</td>
                            <td>
                                <asp:TextBox runat="server" ID="txtDivision"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:CheckBox ID="chkAdministrative" Text="Is administrative?" runat="server" />
                            </td>
                        </tr>

                    </table>


                </td>
            </tr>
        </asp:Panel>
     
       
    </table>
    </asp:Panel>
    <table>
         <tr>
     <td>
         <asp:Button ID="btnDisplayProfile" runat="server" Text="Display Profile" OnClick="btnDisplayProfile_Click" /></td>
     <td>
         <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>&nbsp;</td>
 </tr>
 <tr>
     <td>&nbsp;</td>
     <td>
         <asp:Button ID="btnAddProfile" runat="server" Text="Add Profile" OnClick="btnAddProfile_Click"></asp:Button></td>
 </tr>
 <tr>
     <td>&nbsp;</td>
     <td>&nbsp;</td>
 </tr>
    </table>
  
</asp:Content>
