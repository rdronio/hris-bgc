<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreatePDF.aspx.cs" Inherits="HRIS_Basic.CreatePDF" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="btnCreate" runat="server" Text="Generate Payslip" OnClick="btnCreate_Click" />
        <asp:Button ID="btnCreateHTML" runat="server" Text="Generate HTML Payslip" OnClick="btnCreateHTML_Click" />
    </div>
    </form>
</body>
</html>
