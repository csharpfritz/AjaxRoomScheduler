<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" MasterPageFile="~/Layout.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">

</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="body">

    <div id="left" style="float: left; border: 1px solid gray; width: 180px;">
        <div id="stubTopLeft" style="height: 180px; background-color: lightGreen; text-align: center;">TOP LEFT PANEL - OCCUPANCY %</div>

        <div id="stubBottomLeft" style="height: 400px; background-color: skyblue;">BOTTOM LEFT PANEL - CURRENT GUESTS</div>
    </div>

    <div id="stubRight">GUEST DETAILS</div>

</asp:Content>
