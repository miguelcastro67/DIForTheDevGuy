<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Avengers.aspx.cs" Inherits="WebFormClient.Avengers" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>AllAvengers</title>
    </head>
    <body>
        <form id="form1" runat="server">
            <div>
                <asp:GridView ID="grdAvengers" runat="server" />
            </div>
            <br />
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Default.aspx" Text="Home" />
        </form>
    </body>
</html>
