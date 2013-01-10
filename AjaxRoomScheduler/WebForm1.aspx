<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="AjaxRoomScheduler.WebForm1" MasterPageFile="~/Layout.Master" %>

<asp:Content ID="bodycontent" runat="server" ContentPlaceHolderID="body">

    <style>
        .leftPanel, .detailsPanel {
            font-family: Arial;
            height: 100px;
            padding-top: 20px;
            text-align: center;
        }
    </style>

    <div style="">

    <div class="leftPanel" style="background-color: lightblue; ">

        CURRENT 
        OCCUPANTS
        INFORMATION

    </div>

    <div class="detailsPanel" style="background-color: lightgreen; margin-left: 335px; width: 300px;">

        SELECTED OCCUPANTS <br />RESERVATION INFORMATION

    </div>

    </div>

</asp:Content>