<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="AjaxRoomScheduler.Default" MasterPageFile="~/Layout.master" %>

<asp:Content runat="server" ContentPlaceHolderID="body">

    <div class="leftPanel">
        <div id="topLeft" style="text-align: center; color: teal; font-weight: bold;">
            <asp:label runat="server" ID="occupancyLabel" Text="Today's Occupancy Pct" />
            <telerik:RadRadialGauge ID="occupancyGuage" runat="server" Height="130px" Skin="MetroTouch" Width="320px">
                <Pointer Color="Teal" >
                    <Cap Color="Black" Size="0.10"  />
                </Pointer>
                <Scale>
                    <Labels Color="Teal" Font="8pt arial bold" Format="{0}%" Position="Inside" />
                    <Ranges>
                        <telerik:GaugeRange Color="Green" To="60" />
                        <telerik:GaugeRange Color="Yellow" From="61" To="90" />
                        <telerik:GaugeRange Color="Red" From="91" To="100" />
                    </Ranges>
                </Scale>
            </telerik:RadRadialGauge>
            <h4 style="margin: 0px 0px 5px 0px;"><asp:Literal ID="occupancyPctLabel" runat="server"></asp:Literal></h4>
        </div>

        <telerik:RadAjaxPanel runat="server" ID="currentGuestsPanel">
            <telerik:RadGrid ID="currentGuestsGrid" runat="server" AutoGenerateColumns="False" CellSpacing="0" GridLines="None" AllowSorting="True" OnNeedDataSource="currentGuestsGrid_NeedDataSource">

                <ClientSettings>
                    <Selecting EnableDragToSelectRows="False" AllowRowSelect="True" />
                    <ClientEvents OnRowSelected="currentGuestsGrid_OnRowSelected" />
                    <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                </ClientSettings>

<MasterTableView>

<RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column">
<HeaderStyle Width="20px"></HeaderStyle>
</RowIndicatorColumn>

<ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column">
<HeaderStyle Width="20px"></HeaderStyle>
</ExpandCollapseColumn>

    <Columns>
        <telerik:GridBoundColumn DataField="ResId" DataType="System.Int32" FilterControlAltText="Filter resID column" UniqueName="resID" Visible="True" HeaderStyle-Width="0" ItemStyle-Width="0">
            <HeaderStyle Width="0px" />
            <ItemStyle Width="0px" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="RoomNum" FilterControlAltText="Filter room column" HeaderText="Room" UniqueName="room">
            <FooterStyle Width="100px" />
            <HeaderStyle Width="100px" />
            <ItemStyle Width="100px" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="GuestName" FilterControlAltText="Filter column column" SortExpression="SortName" HeaderText="Guest" UniqueName="column">
        </telerik:GridBoundColumn>
        <telerik:GridClientSelectColumn FilterControlAltText="Filter select column" UniqueName="select">
            <HeaderStyle Width="0px" />
            <ItemStyle Width="0px" />
        </telerik:GridClientSelectColumn>
    </Columns>

<EditFormSettings>
<EditColumn FilterControlAltText="Filter EditCommandColumn column"></EditColumn>
</EditFormSettings>
</MasterTableView>

<FilterMenu EnableImageSprites="False"></FilterMenu>

            </telerik:RadGrid>
        </telerik:RadAjaxPanel>

        </div>

</asp:Content>
