<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CountDays.aspx.cs" Inherits="HRIS_Basic.CountDays" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <label for="txtDateFrom" class="label-6">From: </label>
                            <input name="txtDateFrom" type="text" id="txtDateFrom" class="leavePicker" runat="server"/>
                            <label for="txtDateTo" class="label-6">To: </label>
                            <input name="txtDateTo" type="text" id="txtDateTo" class="leavePicker" runat="server"/>
        <asp:Button ID="btnCalculate" runat="server" Text="Button" OnClick="btnCalculate_Click" />
        </br>
        </br>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>

    </div>
                <script src="Scripts/main.js"></script>
    </form>

</body>
</html>
