<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetLeave.aspx.cs" Inherits="HRIS_Basic.SetLeave" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    
    <form id="form1" runat="server">
    <div>
    </br>
    </br>
    <asp:Label ID="Label3" runat="server" Text="Employee:   "></asp:Label><asp:DropDownList ID="drpEmployee" runat="server">
    </asp:DropDownList>
  </br>
  </br>
        <asp:Label ID="Label1" runat="server" Text="Vacation Leave: "></asp:Label><asp:TextBox
            ID="txtVacation" runat="server"></asp:TextBox>
            </br>
            </br>
            <asp:Label ID="Label2" runat="server" Text="Sick Leave: "></asp:Label><asp:TextBox
            ID="txtSick" runat="server"></asp:TextBox>
            </br>
            </br>
            <asp:Label ID="Label4" runat="server" Text="Year: "></asp:Label><select id="drpYear" runat="server"></select>
            </br>
            </br>
    <asp:Button ID="btnSet" runat="server" Text="Set Leave" OnClick="btnSet_Click" />
    </div>

    <script type="text/javascript">
        var min = new Date().getFullYear(),
         max = min + 10,
            select = document.getElementById('drpYear');

        for (var i = min; i <= max; i++) {
            var opt = document.createElement('option');
            opt.value = i;
            opt.innerHTML = i;
            select.appendChild(opt);
        }
    </script>
    </form>
</body>
</html>
