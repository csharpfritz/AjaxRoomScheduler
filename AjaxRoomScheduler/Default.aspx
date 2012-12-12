<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" MasterPageFile="~/Layout.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">

</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="body">

    <telerik:RadAjaxManagerProxy ID="radProxy" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="currentGuestsGrid">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="currentGuestsGrid" />
                </UpdatedControls> 
                </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>

    <div id="left" style="float: left; border: 1px solid gray; width: 320px;">
        <div id="stubTopLeft" style="text-align: center; color: teal; font-weight: bold;">
            <span>Today's Occupancy Pct</span>
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

            <telerik:RadGrid ID="currentGuestsGrid" runat="server" AutoGenerateColumns="False" CellSpacing="0" GridLines="None" AllowSorting="True" Height="500px">

                <ClientSettings>
                    <Selecting AllowRowSelect="True" />
                    <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                </ClientSettings>

<MasterTableView>
<CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>

<RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column">
<HeaderStyle Width="20px"></HeaderStyle>
</RowIndicatorColumn>

<ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column">
<HeaderStyle Width="20px"></HeaderStyle>
</ExpandCollapseColumn>

    <Columns>
        <telerik:GridBoundColumn DataField="RoomNum" FilterControlAltText="Filter room column" HeaderText="Room" UniqueName="room">
            <FooterStyle Width="100px" />
            <HeaderStyle Width="100px" />
            <ItemStyle Width="100px" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="GuestName" FilterControlAltText="Filter column column" HeaderText="Guest" UniqueName="column">
        </telerik:GridBoundColumn>
    </Columns>

<EditFormSettings>
<EditColumn FilterControlAltText="Filter EditCommandColumn column"></EditColumn>
</EditFormSettings>
</MasterTableView>

<FilterMenu EnableImageSprites="False"></FilterMenu>

            </telerik:RadGrid>
        </div>

    <div id="stubRight">GUEST DETAILS</div>

</asp:Content>
