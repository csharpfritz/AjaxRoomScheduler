<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Rooms_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">

<telerik:RadTextBox ID="txtFirstName" runat="server"></telerik:RadTextBox>
<telerik:RadTextBox ID="txtLastName" runat="server"></telerik:RadTextBox>

<script>
    var controls = new function () {
        return {
            txtFirstName: {
                UniqueID: "<%= txtFirstName.UniqueID %>",
                ClientID: "<%= txtFirstName.ClientID %>"
            },
            txtLastName: {
                UniqueID: "<%= txtLastName.UniqueID %>",
                ClientID: "<%= txtLastName.ClientID %>"
            }
        };
    }();
    
    controls.txtFirstName.ClientID;
</script>
    </asp:Content>

