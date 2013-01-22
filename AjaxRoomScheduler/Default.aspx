<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="AjaxRoomScheduler.Default" MasterPageFile="~/Layout.master" %>

<asp:Content runat="server" ContentPlaceHolderID="body">

    <telerik:RadAjaxManagerProxy runat="server" id="ajaxProxy">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="currentGuestsGrid" >
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="currentGuestsGrid" LoadingPanelID="loadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>

    <asp:Label ID="testLabel" runat="server"></asp:Label>

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

        <%-- Show the cool animations when we reference the server --%>
        <telerik:RadAjaxLoadingPanel runat="server" ID="loadingPanel"></telerik:RadAjaxLoadingPanel>

        <telerik:RadGrid ID="currentGuestsGrid" runat="server" AutoGenerateColumns="False" 
            CellSpacing="0" GridLines="None" AllowSorting="True" 
            OnNeedDataSource="currentGuestsGrid_NeedDataSource">

            <ClientSettings EnableRowHoverStyle="true">
                <ClientEvents OnRowClick="currentGuestsGrid_RowClick" />
                <Selecting EnableDragToSelectRows="False" AllowRowSelect="true" />
                <Scrolling AllowScroll="True" UseStaticHeaders="True" />
            </ClientSettings>

            <MasterTableView DataKeyNames="ResId" ClientDataKeyNames="ResId">

                <Columns>
                    <telerik:GridBoundColumn DataField="RoomNum" HeaderText="Room" UniqueName="room">
                        <FooterStyle Width="100px" />
                        <HeaderStyle Width="100px" />
                        <ItemStyle Width="100px" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="GuestName" SortExpression="SortName" 
                        HeaderText="Guest" UniqueName="column">
                    </telerik:GridBoundColumn>
                </Columns>

            </MasterTableView>

        </telerik:RadGrid>


        </div>

    <asp:Panel ID="reservationDetailsPanel" runat="server" CssClass="detailsPanel" Visible="true" ClientIDMode="Static" >

        <asp:Label runat="server" ID="lNoReservationSelected" CssClass="NoReservationSelected" Text="No Reservation Currently Selected"></asp:Label>

        <asp:Literal runat="server" ID="lReservationDetails"><h3>Reservation Details</h3></asp:Literal>

        <asp:Literal runat="server" ID="lDetails"><h4>Guest Details:</h4></asp:Literal>

        <table>
            <tr>
                <td>
                    <asp:Label runat="server" ID="lblLastName" Width="150" AssociatedControlID="txtLastName">Last Name:</asp:Label>
                </td>
                <td>
                    <telerik:RadTextBox runat="server" ID="txtLastName" Width="150" Enabled="false"></telerik:RadTextBox>
                </td>
                <td>
                    <asp:Label runat="server" ID="lblFirstName" Width="150" AssociatedControlID="txtFirstName">First Name:</asp:Label>
                </td>
                <td>
                    <telerik:RadTextBox runat="server" ID="txtFirstName" Width="150" Enabled="false"></telerik:RadTextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server" ID="lblAssignedRoom" AssociatedControlID="assignedRoom">Assigned Room:</asp:Label>
                </td>
                <td colspan="3">
                    <asp:label runat="server" ID="assignedRoom"></asp:label>
                </td>
            </tr>
        </table>

        <div id="chargesHeader">
            <asp:Literal ID="lCharges" runat="server"><h4>Charges:</h4> </asp:Literal>
            <asp:Literal ID="lTotalCharges" runat="server"><h4>Total: </h4></asp:Literal>
        </div>
        <br />
        <%-- Editor for Notes goes here --%>
        <telerik:RadGrid ID="chargesGrid" runat="server" Width="500px" Height="200px" Visible="false" AutoGenerateColumns="False" CellSpacing="0" GridLines="None">

            <MasterTableView>
                <Columns>
                    <telerik:GridBoundColumn DataField="Description" UniqueName="description" HeaderText="Description"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Value" UniqueName="value" HeaderText="Charged" DataFormatString="{0:$0.00}">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                </Columns>

            </MasterTableView>

        </telerik:RadGrid>

        <br />
        <asp:Literal ID="lNotes" runat="server"><h4>Notes:</h4></asp:Literal>
        <telerik:AccessibleRadEditor runat="server" ID="notesEditor" Height="200" Width="500" />

        </asp:Panel>

</asp:Content>

<asp:Content ContentPlaceHolderID="afterScripts" runat="server">
    <script><!-- 

        var controls = {
            ajaxMgr: {
                ClientID: "<%= RadAjaxManager.GetCurrent(this).ClientID %>"
        	}
        };

        function currentGuestsGrid_RowClick(sender, args) {
        	
            var key = args.getDataKeyValue("ResId");
            $find("<%= RadAjaxManager.GetCurrent(this).ClientID %>").ajaxRequest(key);

        }

    --></script>
</asp:Content>