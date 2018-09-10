<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Configurations.aspx.cs" Inherits="WebPCConfigTool.Pages.Configurations" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <h3>Select a configuration to view details.</h3>
    <asp:ObjectDataSource ID="odsConfigurations" runat="server" SelectMethod="GetAllConfigurations" TypeName="WebPCConfigTool.DAL.Repositories.PcConfigurationRepository"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsComponents" runat="server" SelectMethod="GetComponents" TypeName="WebPCConfigTool.DAL.Repositories.ComponentRepository">
        <SelectParameters>
            <asp:ControlParameter ControlID="gridConfigurations" DefaultValue="0" Name="idPcConfiguration" PropertyName="SelectedValue" Type="Int64" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <div class="row">
        <h2 style="font-weight: 300;">PC Configurations </h2>
        <asp:GridView ID="gridConfigurations" runat="server" AllowPaging="True" DataKeyNames="Id"
            ForeColor="#333333" GridLines="Horizontal"
            AutoGenerateColumns="False" DataSourceID="odsConfigurations" Width="600px">
            <Columns>
                <asp:CommandField ShowSelectButton="True" ItemStyle-Width="50px" />
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                <asp:BoundField DataField="Price" HeaderText="Price" SortExpression="Price" ItemStyle-Width="100px">
                    <ItemStyle HorizontalAlign="Right" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundField>
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
        <hr />
        <h2 style="font-weight: 300;">PC Components details</h2>
        <asp:GridView ID="gridComponents" runat="server" 
            ForeColor="#555555" GridLines="Horizontal"
            AutoGenerateColumns="False" Width="600px" DataSourceID="odsComponents">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                <asp:BoundField DataField="Price" HeaderText="Price" SortExpression="Price" ItemStyle-Width="100px">
                    <ItemStyle HorizontalAlign="Right" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundField>
            </Columns>
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
