<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pagination.aspx.cs" Inherits="HRIS_Basic.Pagination" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <!-- Meta, title, CSS, favicons, etc. -->
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <!-- CSS -->
    <link rel="stylesheet" href="Styles/css/all.css" />
    <link rel="stylesheet" href="Styles/style.css" />
    <link rel="stylesheet" media="screen and (max-width: 767px)" href="Styles/mobile.css" />
    <link rel="stylesheet" href="Styles/jquery/jquery-ui-custom.css" />
    <!-- JavaScript -->
    <script src="Scripts/jquery-3.4.1.min.js"></script>
    <script src="Scripts/jquery-ui.min.js"></script>
    <title>HRIS & Payroll w/ Time Keeping System</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="tableLayout">
            <asp:DataGrid ID="dgTimeLogs" runat="server" AlternatingItemStyle-CssClass="AlternateMainTableRow"
                ItemStyle-CssClass="MainTableRow" HeaderStyle-CssClass="MainTableRow" CssClass="tableLayout"
                CellSpacing="1" CellPadding="3" Width="100%" BorderWidth="0" HorizontalAlign="Center"
                AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundColumn HeaderStyle-Width="10%" HeaderText="EMPLOYEE NAME" DataField="Emp_ID"
                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-Width="10%" HeaderText="DATE" DataField="timelogs_date" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-Width="10%" HeaderText="TIME" DataField="timelogs_time"
                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
                </Columns>
            </asp:DataGrid>
        </table>
        <div class="pagination">
            <asp:Repeater ID="rptPaging" runat="server" OnItemCommand="rptPaging_ItemCommand">
                <ItemTemplate>
                <asp:LinkButton ID="lnkPageNumber" CommandName="Page" CommandArgument="<%# Container.DataItem %>"
                        runat="server" ForeColor="Black" Font-Bold="True"><%# Container.DataItem %></asp:LinkButton>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
    </form>
</body>
</html>
