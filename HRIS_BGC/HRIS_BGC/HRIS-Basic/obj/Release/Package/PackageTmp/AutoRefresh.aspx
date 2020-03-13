<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AutoRefresh.aspx.cs" Inherits="HRIS_Basic.AutoRefresh" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" language="javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.4.4/jquery.min.js"></script>
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {
            var idleInterval = setInterval("reloadPage()", 10000);
        })

        function reloadPage() {

            location.reload();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    </div>
    </form>
</body>
</html>
