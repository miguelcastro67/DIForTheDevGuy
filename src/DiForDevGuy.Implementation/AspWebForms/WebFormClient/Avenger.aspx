<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Avenger.aspx.cs" Inherits="WebFormClient.Avenger" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Single Avenger</title>
    </head>
    <body>
        <form id="form1" runat="server">
            <div>
                <asp:DetailsView ID="detAvenger" runat="server" />
            </div>
            <br />
            <asp:HyperLink runat="server" NavigateUrl="~/Default.aspx" Text="Home" />
        </form>
    </body>
</html>
