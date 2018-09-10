<%@ Page Title="Components" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Components.aspx.cs" Inherits="WebPCConfigTool.Pages.Components" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %> - Insert and Delete</h2>
    <asp:ObjectDataSource ID="odsRams" runat="server" TypeName="WebPCConfigTool.DAL.Repositories.RamRepository"
        OnInserting="odsRams_Inserting" OnDeleting="Comps_Deleting"
        DeleteMethod="DeleteRam" InsertMethod="InsertRam" SelectMethod="GetAllRams">
        <DeleteParameters>
            <asp:Parameter Name="id" Type="Int64" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="name" Type="String" />
            <asp:Parameter Name="price" Type="Decimal" />
            <asp:Parameter Name="compType" Type="Int32" />
        </InsertParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsRamSize" runat="server" SelectMethod="GetRamSize" TypeName="WebPCConfigTool.DAL.Repositories.RamRepository"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsHdds" runat="server" TypeName="WebPCConfigTool.DAL.Repositories.HardDiskRepository"
        OnInserting="odsHdds_Inserting" OnDeleting="Comps_Deleting"
        DeleteMethod="DeleteHardDisk" InsertMethod="InsertHardDisk" SelectMethod="GetAllDisks">
        <DeleteParameters>
            <asp:Parameter Name="id" Type="Int64" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="name" Type="String" />
            <asp:Parameter Name="price" Type="Decimal" />
            <asp:Parameter Name="compType" Type="Int32" />
        </InsertParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsHddType" runat="server" SelectMethod="GetHddType" TypeName="WebPCConfigTool.DAL.Repositories.HardDiskRepository"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsOsType" runat="server" SelectMethod="GetOsType" TypeName="WebPCConfigTool.DAL.Repositories.OperatingSystemRepository"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsOss" runat="server" TypeName="WebPCConfigTool.DAL.Repositories.OperatingSystemRepository"
        OnInserting="odsOss_Inserting" OnDeleting="Comps_Deleting"
        DeleteMethod="DeleteOs" InsertMethod="InsertOs" SelectMethod="GetAllOSs">
        <DeleteParameters>
            <asp:Parameter Name="id" Type="Int64" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="name" Type="String" />
            <asp:Parameter Name="price" Type="Decimal" />
            <asp:Parameter Name="compType" Type="Int32" />
        </InsertParameters>
    </asp:ObjectDataSource>
    &nbsp;&nbsp;
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ValidationGroup="GroupRam"
        ShowSummary="False" />
    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True" ValidationGroup="GroupHdd"
        ShowSummary="False" />
    <asp:ValidationSummary ID="ValidationSummary3" runat="server" ShowMessageBox="True" ValidationGroup="GroupOs"
        ShowSummary="False" />
    &nbsp;&nbsp;
    <asp:UpdatePanel ID="updatePanel1" runat="server">
        <ContentTemplate>

            <h2 style="font-weight: 300;">RAM </h2>
            <asp:GridView ID="gridRams" runat="server"
                ShowFooter="True" EnableViewState="False" OnDataBound="GridView_DataBound" OnRowCommand="Comps_RowCommand"
                DataSourceID="odsRams" ForeColor="#333333" GridLines="None" AllowPaging="True" AutoGenerateColumns="False">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="" InsertVisible="False" SortExpression="Id" HeaderStyle-Width="50px">
                        <ItemTemplate>
                            <asp:Label ID="lblId" runat="server" Text='<%# Bind("Id") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="50px" />
                        <ItemStyle HorizontalAlign="Left" />
                        <FooterTemplate>
                            <asp:Button ID="AddRam" runat="server" CommandName="Insert" Text="Add" ForeColor="Red" ValidationGroup="GroupRam" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Name" SortExpression="Name" HeaderStyle-Width="150px">
                        <ItemTemplate>
                            <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="150px" />
                        <ItemStyle HorizontalAlign="Left" />
                        <FooterTemplate>
                            <asp:TextBox ID="NewName" runat="server" ForeColor="#333333"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="NewName"
                                ValidationGroup="GroupRam"
                                Display="Dynamic" ErrorMessage="You must enter a name for the new RAM." ForeColor="">*</asp:RequiredFieldValidator>
                        </FooterTemplate>
                        <FooterStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Price" SortExpression="Price" HeaderStyle-Width="50px">
                        <ItemTemplate>
                            <asp:Label ID="lblPrice" runat="server" Text='<%# Bind("Price") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="100px" />
                        <ItemStyle HorizontalAlign="Right" />
                        <FooterTemplate>
                            <asp:TextBox ID="NewPrice" runat="server" ForeColor="#333333"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="NewPrice" ValidationGroup="GroupRam"
                                Display="Dynamic" ErrorMessage="You must enter a Price for the new RAM." ForeColor="">*</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator runat="server" ErrorMessage="Decimal Only for price." ControlToValidate="NewPrice"
                                Display="Dynamic" ValidationGroup="GroupRam"
                                ValidationExpression="^[1-9]\d*(\.\d+)?$">*</asp:RegularExpressionValidator>
                        </FooterTemplate>
                        <FooterStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Size" SortExpression="RamSize" HeaderStyle-Width="150px">
                        <ItemTemplate>
                            <asp:Label ID="lblRamSize" runat="server" Text='<%# GetEnumDescription(Eval("RamSize")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
                        <FooterTemplate>
                            <asp:DropDownList ID="NewType" runat="server"
                                DataSourceID="odsRamSize" ForeColor="DarkGray"
                                DataTextField="Value" DataValueField="Key">
                            </asp:DropDownList>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ButtonType="Button" HeaderText="Delete" ShowDeleteButton="True" ShowHeader="True" />
                </Columns>
                <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                <SortedAscendingCellStyle BackColor="#FDF5AC" />
                <SortedAscendingHeaderStyle BackColor="#4D0000" />
                <SortedDescendingCellStyle BackColor="#FCF6C0" />
                <SortedDescendingHeaderStyle BackColor="#820000" />
            </asp:GridView>
            <h2 style="font-weight: 300;">HDD </h2>
            <asp:GridView ID="gridHdds" runat="server"
                ShowFooter="True" EnableViewState="False" OnDataBound="GridView_DataBound" OnRowCommand="Comps_RowCommand"
                DataSourceID="odsHdds" ForeColor="#333333" GridLines="None" AllowPaging="True" AutoGenerateColumns="False">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="" InsertVisible="False" SortExpression="Id" HeaderStyle-Width="50px">
                        <ItemTemplate>
                            <asp:Label ID="lblId" runat="server" Text='<%# Bind("Id") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="50px" />
                        <ItemStyle HorizontalAlign="Left" />
                        <FooterTemplate>
                            <asp:Button ID="AddHdd" runat="server" CommandName="Insert" Text="Add" ForeColor="Red" ValidationGroup="GroupHdd" CausesValidation="true" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Name" SortExpression="Name" HeaderStyle-Width="200px">
                        <ItemTemplate>
                            <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="200px" />
                        <ItemStyle HorizontalAlign="Left" />
                        <FooterTemplate>
                            <asp:TextBox ID="NewName" runat="server" ForeColor="#333333"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="NewName" ValidationGroup="GroupHdd"
                                Display="Dynamic" ErrorMessage="You must enter a name for the new HDD." ForeColor="">*</asp:RequiredFieldValidator>
                        </FooterTemplate>
                        <FooterStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Price" SortExpression="Price" HeaderStyle-Width="50px">
                        <ItemTemplate>
                            <asp:Label ID="lblPrice" runat="server" Text='<%# Bind("Price") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="50px" />
                        <ItemStyle HorizontalAlign="Right" />
                        <FooterTemplate>
                            <asp:TextBox ID="NewPrice" runat="server" ForeColor="#333333"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="NewPrice" ValidationGroup="GroupHdd"
                                Display="Dynamic" ErrorMessage="You must enter a Price for the new HDD." ForeColor="">*</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator runat="server" ErrorMessage="Decimal Only for price." ControlToValidate="NewPrice"
                                Display="Dynamic" ValidationGroup="GroupHdd"
                                ValidationExpression="^[1-9]\d*(\.\d+)?$">*</asp:RegularExpressionValidator>
                        </FooterTemplate>
                        <FooterStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Disk Type" SortExpression="DiskType" HeaderStyle-Width="150px">
                        <ItemTemplate>
                            <asp:Label ID="lblDiskType" runat="server" Text='<%# GetEnumDescription(Eval("DiskType")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
                        <FooterTemplate>
                            <asp:DropDownList ID="NewType" runat="server"
                                DataSourceID="odsHddType" ForeColor="DarkGray"
                                DataTextField="Value" DataValueField="Key">
                            </asp:DropDownList>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ButtonType="Button" HeaderText="Delete" ShowDeleteButton="True" ShowHeader="True" />
                </Columns>
                <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                <SortedAscendingCellStyle BackColor="#FDF5AC" />
                <SortedAscendingHeaderStyle BackColor="#4D0000" />
                <SortedDescendingCellStyle BackColor="#FCF6C0" />
                <SortedDescendingHeaderStyle BackColor="#820000" />
            </asp:GridView>
            <h2 style="font-weight: 300;">OS </h2>
            <asp:GridView ID="gridOss" runat="server"
                ShowFooter="True" EnableViewState="False" OnDataBound="GridView_DataBound" OnRowCommand="Comps_RowCommand"
                DataSourceID="odsOss" ForeColor="#333333" GridLines="None" AllowPaging="True" AutoGenerateColumns="False">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="" InsertVisible="False" SortExpression="Id" HeaderStyle-Width="50px">
                        <ItemTemplate>
                            <asp:Label ID="lblId" runat="server" Text='<%# Bind("Id") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="50px" />
                        <ItemStyle HorizontalAlign="Left" />
                        <FooterTemplate>
                            <asp:Button ID="AddOs" runat="server" CommandName="Insert" Text="Add" ForeColor="Red" ValidationGroup="GroupOs" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Name" SortExpression="Name" HeaderStyle-Width="150px">
                        <ItemTemplate>
                            <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="150px" />
                        <ItemStyle HorizontalAlign="Left" />
                        <FooterTemplate>
                            <asp:TextBox ID="NewName" runat="server" ForeColor="#333333"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="NewName"
                                ValidationGroup="GroupOs"
                                Display="Dynamic" ErrorMessage="You must enter a name for the new OS." ForeColor="">*</asp:RequiredFieldValidator>
                        </FooterTemplate>
                        <FooterStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Price" SortExpression="Price" HeaderStyle-Width="50px">
                        <ItemTemplate>
                            <asp:Label ID="lblPrice" runat="server" Text='<%# Bind("Price") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="100px" />
                        <ItemStyle HorizontalAlign="Right" />
                        <FooterTemplate>
                            <asp:TextBox ID="NewPrice" runat="server" ForeColor="#333333"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="NewPrice" ValidationGroup="GroupOs"
                                Display="Dynamic" ErrorMessage="You must enter a Price for the new OS." ForeColor="">*</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator runat="server" ErrorMessage="Decimal Only for price." ControlToValidate="NewPrice"
                                Display="Dynamic" ValidationGroup="GroupOs"
                                ValidationExpression="^[1-9]\d*(\.\d+)?$">*</asp:RegularExpressionValidator>
                        </FooterTemplate>
                        <FooterStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Os Type" SortExpression="OsType" HeaderStyle-Width="100px">
                        <ItemTemplate>
                            <asp:Label ID="lblOsType" runat="server" Text='<%# GetEnumDescription(Eval("OsType")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
                        <FooterTemplate>
                            <asp:DropDownList ID="NewType" runat="server"
                                DataSourceID="odsOsType" ForeColor="DarkGray"
                                DataTextField="Value" DataValueField="Key">
                            </asp:DropDownList>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ButtonType="Button" HeaderText="Delete" ShowDeleteButton="True" ShowHeader="True" />
                </Columns>
                <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                <SortedAscendingCellStyle BackColor="#FDF5AC" />
                <SortedAscendingHeaderStyle BackColor="#4D0000" />
                <SortedDescendingCellStyle BackColor="#FCF6C0" />
                <SortedDescendingHeaderStyle BackColor="#820000" />
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
