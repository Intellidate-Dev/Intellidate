<%@ Page Title="" Language="C#" MasterPageFile="~/Secured/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AdminModule.Secured.ErrorList.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="objHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="objContentPlaceHolder" runat="server">
    <div style="font-size: large; font-weight: bold; padding: 15px; font-variant: small-caps;">Error List : </div>


    <div style="padding: 15px; height: 500px; overflow-y: auto; overflow-x: hidden; text-align: center; border: 1px solid #000; border-radius: 6px;">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AllowSorting="True" ShowFooter="true" OnSorting="GridView1_Sorting" CellPadding="4" HeaderStyle-Font-Underline="true" ForeColor="#333333" GridLines="None" OnRowCommand="GridView1_RowCommand">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField HeaderText="S.No" HeaderStyle-Width="30px" SortExpression="SNo">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblIndex" Text='<%#Eval("SNo") %>'></asp:Label>
                    </ItemTemplate>

                    <HeaderStyle Width="30px"></HeaderStyle>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Class Name" HeaderStyle-Width="200px" SortExpression="ClassName">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblSource" Text='<%#Eval("ClassName") %>'></asp:Label>
                    </ItemTemplate>

                    <HeaderStyle Width="200px"></HeaderStyle>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Method Name" HeaderStyle-Width="150px" SortExpression="MethodName">
                    <ItemTemplate>

                        <label><%#Eval("MethodName") %></label>

                    </ItemTemplate>

                    <HeaderStyle Width="150px"></HeaderStyle>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Error Message" HeaderStyle-Width="150px" SortExpression="Ex">
                    <ItemTemplate>
                        <div style="text-align: left;">
                            <label><%#Eval("Ex") %></label>
                        </div>
                    </ItemTemplate>

                    <HeaderStyle Width="150px"></HeaderStyle>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Inner Exception" HeaderStyle-Width="200px" SortExpression="InnerException">
                    <ItemTemplate>
                        <div style="text-align: left; height: 50px; overflow-y: auto; width: 300px; overflow-x: hidden;">
                            <%#Eval("InnerException") %>
                        </div>
                    </ItemTemplate>

                    <HeaderStyle Width="200px"></HeaderStyle>
                </asp:TemplateField>


                <asp:TemplateField HeaderText="Created Date" HeaderStyle-Width="200px" SortExpression="TimeStamp">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblTimeStamp" Text='<%#Eval("TimeStamp") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="200px"></HeaderStyle>
                    <FooterTemplate>
                        <asp:Button ID="btnDelete" runat="server" Text="Clear All" CssClass="Button" CommandName="Clear" />
                    </FooterTemplate>
                </asp:TemplateField>
            </Columns>
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>

    </div>
</asp:Content>
