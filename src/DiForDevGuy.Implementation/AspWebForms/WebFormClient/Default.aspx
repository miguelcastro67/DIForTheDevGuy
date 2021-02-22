<%@ Page Title="Home Page" Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebFormClient._Default" %>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Autofac WebForms Integration Demo</title>
    </head>
    <body>
        <form id="form1" runat="server">
            <div>
                <asp:HyperLink NavigateUrl="~/Avengers.aspx" Text="Avengers" runat="server" />
                <br /><br />
                <asp:TextBox ID="txtName" runat="server" Width="200px" />
                <asp:LinkButton ID="lnkAvenger" runat="server" OnClick="lnkAvenger_Click" Text="Avenger" />
            </div>
        </form>
    </body>
</html>
