<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="WebPCConfigTool.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <h3>Your application description page.</h3>
    <p>Use this area to provide additional information.</p>
    <asp:ObjectDataSource ID="odsHDDs" runat="server" SelectMethod="GetAllDisks" TypeName="WebPCConfigTool.DAL.Repositories.HardDiskRepository"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsRams" runat="server" SelectMethod="GetAllRams" TypeName="WebPCConfigTool.DAL.Repositories.RamRepository"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsPcConfig" runat="server" TypeName="WebPCConfigTool.DAL.Repositories.ComponentRepository"
        SelectMethod="GetPCConfiguration"
        OnSelecting="odsPcConfig_Selecting"
        OnSelected="odsPcConfig_Selected">
        <SelectParameters>
            <asp:Parameter DefaultValue="0" Name="idHDD" Type="Int64" />
            <asp:Parameter DefaultValue="0" Name="idRAM" Type="Int64" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <div>
        <h2 style="font-weight: 300;">HDD </h2>
        <asp:GridView ID="gridHDD" runat="server"
            AllowPaging="True"
            AutoGenerateColumns="False"
            CellPadding="4"
            OnSelectedIndexChanged="Grid_SelectedIndexChanged"
            DataKeyNames="Id" ForeColor="#333333" GridLines="None"
            DataSourceID="odsHDDs">
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" />
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                <asp:BoundField DataField="DiskType" HeaderText="DiskType" SortExpression="DiskType" />
                <asp:BoundField DataField="Price" HeaderText="Price" SortExpression="Price" />
            </Columns>
            <AlternatingRowStyle BackColor="White" />
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
    <div>
        <h2 style="font-weight: 300;">RAM </h2>
        <asp:GridView ID="gridRAM" runat="server"
            AllowPaging="True"
            AutoGenerateColumns="False"
            CellPadding="4"
            OnSelectedIndexChanged="Grid_SelectedIndexChanged"
            DataKeyNames="Id" ForeColor="#333333" GridLines="Horizontal"
            DataSourceID="odsRams">
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" />
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                <asp:BoundField DataField="Price" HeaderText="Price" SortExpression="Price" />
            </Columns>
            <AlternatingRowStyle BackColor="White" />
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
    <hr />
    <div>
        <h2 style="font-weight: 300;"> PC Configuration </h2>
        <asp:GridView ID="gridConfiguration" runat="server" AutoGenerateColumns="False" ShowFooter="true"
            Width="509px" AllowPaging="True">
            <Columns>
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                <asp:BoundField DataField="Price" HeaderText="Price" SortExpression="Price" />
            </Columns>
            <AlternatingRowStyle BackColor="White" />
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
        <asp:Label ID="lblTotal" runat="server" Text="Total: "></asp:Label>
        <asp:Label ID="totalPrice" runat="server"></asp:Label>
    </div>
</asp:Content>
