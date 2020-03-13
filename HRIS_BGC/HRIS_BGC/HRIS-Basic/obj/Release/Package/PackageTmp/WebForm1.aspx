<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication1.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    Upload CSV: <asp:FileUpload ID="FileUploader" runat="server" /><br />
    <asp:Button ID="UploadButton" runat="server" Text="Upload" OnClick="UploadButton_Click" /><br />
    <asp:Label ID="Label1" runat="server"></asp:Label><br /><br />
     <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />

    <asp:DataGrid ID="dgPayroll" runat="server" AlternatingItemStyle-CssClass="AlternateMainTableRow"
        ItemStyle-CssClass="MainTableRow" HeaderStyle-CssClass="MainTableRow" CssClass="tableLayout"
        CellSpacing="1" CellPadding="3" Width="100%" BorderWidth="0" HorizontalAlign="Center"
        AutoGenerateColumns="False">
        <Columns>
            <asp:BoundColumn HeaderStyle-Width="10%" HeaderText="Employee ID" DataField="EmployeeID"
                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
            <asp:BoundColumn HeaderStyle-Width="10%" HeaderText="Employee Name" DataField="EmployeeName" HeaderStyle-HorizontalAlign="Center"
                ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
            <asp:BoundColumn HeaderStyle-Width="10%" HeaderText="Date" DataField="Date"
                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
            <asp:BoundColumn HeaderStyle-Width="10%" HeaderText="Timekeeping" DataField="Timekeeping"
                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
                <asp:BoundColumn HeaderStyle-Width="10%" HeaderText="Type" DataField="Type"
                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
        </Columns>
    </asp:DataGrid>

        
    </div>
    </form>
</body>
</html>
