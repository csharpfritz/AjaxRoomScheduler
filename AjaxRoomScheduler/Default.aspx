﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="AjaxRoomScheduler.Default" MasterPageFile="~/Layout.master" %>

<asp:Content runat="server" ContentPlaceHolderID="afterScripts">

    <script type="text/javascript" id="telerikClientEvents1">
//<![CDATA[

	function currentGuestsGrid_OnRowSelected(sender,args)
	{

	    var rowIndex = parseInt(args._itemIndexHierarchical,10);
	    var dataItem = args._tableView._dataItems[rowIndex];

	    alert("Reservation Id: " + dataItem._element.cells[0].innerHTML);

	}
//]]>
</script>

</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="body">

    <telerik:RadAjaxManagerProxy ID="radProxy" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="currentGuestsGrid">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="currentGuestsGrid" />
                    <telerik:AjaxUpdatedControl ControlID="guestDetailsPanel" />
                </UpdatedControls> 
                </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>

    <div class="leftPanel">
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

        <telerik:RadAjaxPanel runat="server" ID="currentGuestsPanel">
            <telerik:RadGrid ID="currentGuestsGrid" runat="server" AutoGenerateColumns="False" CellSpacing="0" GridLines="None" AllowSorting="True" OnNeedDataSource="currentGuestsGrid_NeedDataSource" OnSelectedIndexChanged="currentGuestsGrid_SelectedIndexChanged">

                <ClientSettings>
                    <Selecting EnableDragToSelectRows="False" AllowRowSelect="True" />
                    <ClientEvents OnRowSelected="currentGuestsGrid_OnRowSelected" />
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

    <telerik:RadAjaxLoadingPanel ID="guestDetailsLoadingPanel" runat="server"></telerik:RadAjaxLoadingPanel>

    <telerik:RadAjaxPanel ID="guestDetailsPanel" LoadingPanelID="guestDetailsLoadingPanel" EnableAJAX="true" runat="server" CssClass="detailsPanel" Visible="false" OnLoad="guestDetailsPanel_Load">

        <asp:Literal runat="server" ID="lReservationDetails"><h3 style="text-decoration: underline;">Reservation Details</h3></asp:Literal>

        <asp:Literal runat="server" ID="lDetails"><h4>Guest Details</h4></asp:Literal>

        <table>
            <tr>
                <td>
                    <asp:Label runat="server" ID="lblLastName" Width="150" AccessKey="l" AssociatedControlID="txtLastName"><u>L</u>ast Name:</asp:Label>
                </td>
                <td>
                    <telerik:RadTextBox runat="server" ID="txtLastName" Width="150" Enabled="false"></telerik:RadTextBox>
                </td>
                <td>
                    <asp:Label runat="server" ID="lblFirstName" Width="150" AccessKey="f" AssociatedControlID="txtFirstName"><u>F</u>irst Name:</asp:Label>
                </td>
                <td>
                    <telerik:RadTextBox runat="server" ID="txtFirstName" Width="150" Enabled="false"></telerik:RadTextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server" ID="lblAssignedRoom" AccessKey="o">Assigned R<u>o</u>om</asp:Label>
                </td>
                <td colspan="3">
                    <asp:label runat="server" ID="assignedRoom"></asp:label>
                </td>
            </tr>
        </table>

        <asp:Literal ID="lCharges" runat="server"><h4>Charges:</h4></asp:Literal>
        <telerik:RadGrid ID="chargesGrid" runat="server"></telerik:RadGrid>

        <br />
        <asp:Literal ID="lNotes" runat="server"><h4>Notes:</h4></asp:Literal>
        <telerik:AccessibleRadEditor runat="server" ID="notesEditor">
            <Content>
</Content>
            <ImageManager EnableImageEditor="False" />
            <TrackChangesSettings CanAcceptTrackChanges="False" />
        </telerik:AccessibleRadEditor>

        </telerik:RadAjaxPanel>

</asp:Content>
