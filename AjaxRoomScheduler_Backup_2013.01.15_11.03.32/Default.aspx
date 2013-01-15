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
            <h4 style="margin: 0px 0px 5px 0px;"><asp:Literal ID="occupancyPctLabel" runat="server"></asp:Literal>
                <asp:DropDownList ID="DropDownList1" runat="server">
                </asp:DropDownList>
            </h4>
        </div>

        </div>

</asp:Content>
