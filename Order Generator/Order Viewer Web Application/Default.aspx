<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Order_Viewer_Web_Application._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Orders</h1>
    </div>

    <div class="row">
        <div class="col-md-4">
            <asp:GridView ID="GridView1" runat="server">
            </asp:GridView>
        </div>
    </div>

</asp:Content>
