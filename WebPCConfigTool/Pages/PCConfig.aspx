<%@ Page Title="PC Configuration" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="PCConfig.aspx.cs" Inherits="WebPCConfigTool.Pages.PCConfig" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <h3>Your application description page.</h3>
    <p>Use this area to provide additional information.</p>
    <asp:ObjectDataSource ID="odsHDDs" runat="server" SelectMethod="GetAllDisks" TypeName="WebPCConfigTool.DAL.Repositories.HardDiskRepository"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsRams" runat="server" SelectMethod="GetAllRams" TypeName="WebPCConfigTool.DAL.Repositories.RamRepository"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsOSs" runat="server" SelectMethod="GetAllOSs" TypeName="WebPCConfigTool.DAL.Repositories.OperatingSystemRepository"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsCpus" runat="server" SelectMethod="GetAllCpus" TypeName="WebPCConfigTool.DAL.Repositories.CpuRepository"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsVcs" runat="server" SelectMethod="GetAllCards" TypeName="WebPCConfigTool.DAL.Repositories.VideoCardRepository"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsPcConfig" runat="server" TypeName="WebPCConfigTool.DAL.Repositories.ComponentRepository"
        SelectMethod="GetPCConfiguration" InsertMethod="InsertComponents"
        OnSelecting="odsPcConfig_Selecting"
        OnInserting="odsPcConfig_Inserting"
        OnInserted="odsPcConfig_Inserted"
        OnSelected="odsPcConfig_Selected">
        <InsertParameters>
            <asp:Parameter Name="idHDD" Type="Int64" />
            <asp:Parameter Name="idRAM" Type="Int64" />
            <asp:Parameter Name="idOS" Type="Int64" />
            <asp:Parameter Name="idCPU" Type="Int64" />
            <asp:Parameter Name="idVC" Type="Int64" />
        </InsertParameters>
        <SelectParameters>
            <asp:Parameter DefaultValue="0" Name="idHDD" Type="Int64" />
            <asp:Parameter DefaultValue="0" Name="idRAM" Type="Int64" />
            <asp:Parameter DefaultValue="0" Name="idOS" Type="Int64" />
            <asp:Parameter DefaultValue="0" Name="idCPU" Type="Int64" />
            <asp:Parameter DefaultValue="0" Name="idVC" Type="Int64" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <div class="row">
        <div class="col-md-6">
            <h2 style="font-weight: 300;">HDD </h2>
            <asp:GridView ID="gridHDD" runat="server"
                AllowPaging="True"
                AutoGenerateColumns="False"
                OnSelectedIndexChanged="Grid_SelectedIndexChanged"
                DataKeyNames="Id" ForeColor="#333333" GridLines="Horizontal"
                DataSourceID="odsHDDs" Width="597px">
                <Columns>
                    <asp:CommandField ShowSelectButton="True" ItemStyle-Width="50px" />
                    <asp:BoundField DataField="Id" HeaderText="" SortExpression="Id" />
                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" ItemStyle-Width="300px">
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="DiskType" HeaderText="Type" SortExpression="DiskType" />
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
        </div>
        <div class="col-md-6">
            <h2 style="font-weight: 300;">RAM </h2>
            <asp:GridView ID="gridRAM" runat="server"
                AllowPaging="True"
                AutoGenerateColumns="False"
                OnSelectedIndexChanged="Grid_SelectedIndexChanged"
                DataKeyNames="Id" ForeColor="#333333" GridLines="Horizontal"
                DataSourceID="odsRams" Width="597px">
                <Columns>
                    <asp:CommandField ShowSelectButton="True" ItemStyle-Width="50px" />
                    <asp:BoundField DataField="Id" HeaderText="" SortExpression="Id" />
                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" ItemStyle-Width="300px">
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="RamSize" HeaderText="Size" SortExpression="RamSize" />
                    <asp:BoundField DataField="Price" HeaderText="Price" SortExpression="Price" ItemStyle-Width="100px">
                        <ItemStyle HorizontalAlign="Right" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                </Columns>
                <AlternatingRowStyle BackColor="White" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>
        </div>
    </div>
    <div class="row">
        <div class="col-md-6">
            <h2 style="font-weight: 300;">OS </h2>
            <asp:GridView ID="gridOS" runat="server"
                AllowPaging="True"
                AutoGenerateColumns="False"
                OnSelectedIndexChanged="Grid_SelectedIndexChanged"
                DataKeyNames="Id" ForeColor="#333333" GridLines="Horizontal"
                DataSourceID="odsOSs" Width="597px">
                <Columns>
                    <asp:CommandField ShowSelectButton="True" ItemStyle-Width="50px" />
                    <asp:BoundField DataField="Id" HeaderText="" SortExpression="Id" />
                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" ItemStyle-Width="300px">
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="OsType" HeaderText="Type" SortExpression="OsType" />
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
        </div>
        <div class="col-md-6">
            <h2 style="font-weight: 300;">CPU </h2>
            <asp:GridView ID="gridCpus" runat="server"
                AllowPaging="True"
                AutoGenerateColumns="False"
                OnSelectedIndexChanged="Grid_SelectedIndexChanged"
                DataKeyNames="Id" ForeColor="#333333" GridLines="Horizontal"
                DataSourceID="odsCpus" Width="597px">
                <Columns>
                    <asp:CommandField ShowSelectButton="True" ItemStyle-Width="50px" />
                    <asp:BoundField DataField="Id" HeaderText="" SortExpression="Id" />
                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" ItemStyle-Width="300px">
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:BoundField>
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
        </div>
    </div>
    <div class="row">
        <div class="col-md-6">
            <h2 style="font-weight: 300;">Video Cards</h2>
            <asp:GridView ID="gridCards" runat="server"
                AllowPaging="True"
                AutoGenerateColumns="False"
                OnSelectedIndexChanged="Grid_SelectedIndexChanged"
                DataKeyNames="Id" ForeColor="#333333" GridLines="Horizontal"
                DataSourceID="odsVcs" Width="597px">
                <Columns>
                    <asp:CommandField ShowSelectButton="True" ItemStyle-Width="50px" />
                    <asp:BoundField DataField="Id" HeaderText="" SortExpression="Id" />
                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" ItemStyle-Width="300px">
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:BoundField>
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
        </div>
    </div>
    <hr />
    <div class="col-md-12">
        <h2 style="font-weight: 300;">PC Configuration </h2>
        <asp:GridView ID="gridConfiguration" runat="server" AutoGenerateColumns="False" GridLines="Horizontal"
            Width="600px">
            <Columns>
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" HeaderStyle-Width="400px" HeaderStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center" Width="400px"></HeaderStyle>
                </asp:BoundField>
                <asp:BoundField DataField="Price" HeaderText="Price" SortExpression="Price" ItemStyle-Width="100px">
                    <ItemStyle HorizontalAlign="Right" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundField>
            </Columns>
            <AlternatingRowStyle BackColor="White" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#CC0000" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
        <div class="row">
            <div class="col-md-5" style="align-content: flex-start">
                <asp:Button ID="btnConfirm" runat="server" Enabled="false" Text="Confirm" OnClick="btnConfirm_Click" ForeColor="#FFCC99" />
            </div>
            <div class="row col-md-2" style="align-content: flex-end">
                <asp:Label ID="lblTotal" runat="server" Text="Total: " Font-Bold="True" Width="75px" ></asp:Label>
                <asp:Label ID="totalPrice" runat="server" ForeColor="#CC0000" />
            </div>
        </div>
    </div>
</asp:Content>
