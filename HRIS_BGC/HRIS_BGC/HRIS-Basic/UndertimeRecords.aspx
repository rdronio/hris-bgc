﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UndertimeRecords.aspx.cs"
    Inherits="HRIS_Basic.UndertimeRecords" %>

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
    <title>HRIS, Payroll & Time Keeping System</title>
</head>
<body>
    <form action="#" autocomplete="off" runat="server">
    <!-- main -->
    <div class="main">
        <!-- nav -->
        <nav id="nav">
                    <div class="logo">
                        <a href="home.html" class="logo-link">
                            <h1>LOGO HERE</h1>
                            <p>HRIS, Payroll & Time Keeping System</p>
                        </a>
                    </div>

                    <!-- profile quick info -->
                    <div class="profile">
                        <div class="profile_pic">
                            <a href="#"><img src="/images/user.png" alt="..." class="img-circle profile_img" /></a>
                        </div>
                        <div class="profile_info">
                            <span>Welcome</span>
                            <h2>HR Admin</h2>
                            <p>&nbsp;</p>
                        </div>
                    </div>

                  <!-- / menu -->
                    <ul class="nav-list">
            <li>
              <a href="DashboardAdmin.aspx"
                ><i class="fas fa-tachometer-alt"></i>Admin Dashboard</a
              >
            </li>
            <li>
              <a href="#"
                ><i class="fas fa-tasks"> </i>Approvals<span
                  class="fa fa-chevron-down"
                ></span
              ></a>
              <ul class="nav child_menu hidden">
                <li>
                  <a href="PayrollApproval.aspx">Payroll Approval</a>
                </li>
                <li>
                  <a href="LeaveRecords.aspx">Leave Records</a>
                </li>
                <li><a href="OvertimeRecords.aspx">Overtime Records</a></li>
                <li>
                  <a href="UndertimeRecords.aspx">Undertime Approval</a>
                </li>
              </ul>
            </li>
            <li>
              <a href="#"
                ><i class="fas fa-file-alt"></i> Employee Records<span
                class="fa fa-chevron-down"
              ></span
            ></a>
            <ul class="nav child_menu hidden">
              <li class="current-page">
                <a href="ManageEmployees.aspx">Employee Masterlist</a>
              </li>
              <li><a href="EmployeeLeaveRecords.aspx">Employee Leave Records</a></li>
              <li><a href="TimeRecords.aspx">Employee Time Records</a></li>
            </ul>
            </li>
            <%--<li>
              <a href="UnderConstructionAdmin.aspx"
                ><i class="fas fa-list"></i>Employee Assignment</a
              >
            </li>--%>
            <li>
              <a href="#"
                ><i class="fas fa-bullhorn"></i>Memo/Notice<span
                  class="fa fa-chevron-down"
                ></span
              ></a>
              <ul class="nav child_menu hidden">
                <li>
                  <a href="UnderConstructionAdmin.aspx">Bulk Email</a>
                </li>
              </ul>
            </li>
            <li>
              <a href="#"><i class="fas fa-chart-area"></i>Reports<span
                  class="fa fa-chevron-down"
                ></span
              ></a>
              <ul class="nav child_menu hidden">
                <li>
                  <a href="UnderConstructionAdmin.aspx">Generate DTR</a>
                </li>
              </ul>
            </li>
            <li>
              <a href="EmployeeMasterlist.aspx"
                ><i class="fas fa-download"></i>Import Time Records</a
              >
            </li>
           <li>
             <a href="#"
                ><i class="fas fa-cog"></i>System Preferences<span
                  class="fa fa-chevron-down"
                ></span
              ></a>
                            <ul class="nav child_menu hidden">
                                <li>
                                    <a href="HolidayList.aspx">Holiday Notice</a>
                                </li>

                            </ul>
                            </li>
            <li>
              <a href="LoginPage.aspx"
                ><i class="fas fa-sign-out-alt"></i>Sign Out</a
              >
            </li>
          </ul>

                    <div class="close-menu">
                        <a onclick="toggleMenu()">
                            <i class="fas fa-times"></i>
                        </a>
                    </div>
                </nav>
        <!-- undertime-->
        <section id="undertime-records" class="content">
                <!-- TOP NAV  -->
                <div class="top-nav">
                    <span class="menu-btn">
              <a href="#" onclick="toggleMenu()">
                <svg>
                  <path d="M0,5 30,5" stroke="#333" stroke-width="5" />
                  <path d="M0,14 30,14" stroke="#333" stroke-width="5" />
                  <path d="M0,23 30,23" stroke="#333" stroke-width="5" />
                </svg>
              </a>
            </span>
                </div>

                <div class="container">
                 <h2> Undertime Records</h2>
<%--                    <div id="tab">
                        <ul class="tab-list" onclick="selectTab();">
                            <li class="current-tab">
                                <a onclick="showUR()">Undertime Records</a>
                            </li>
                            <li>
                                <a onclick="showPUA()">Pending Undertime</a>
                            </li>
                        </ul>
                    </div>--%>
                    <div class="card card-1">
                        <div>
                            <a class="btn-sm btn-primary" id="btnTimeAlteration" onclick="openModal(`#btnUndertimeApplication`);">
                  File an Undertime
                </a>
                        </div>
                        <div>
                            <label class="label-6">Filter Date</label>
                            <br />
                            <label for="txtDateFrom" class="label-6">From: </label>
                            <input name="txtDateFrom" type="text" id="txtDateFrom" class="datePicker" />
                            <label for="txtDateTo" class="label-6">To: </label>
                            <input name="txtDateTo" type="text" id="txtDateTo" class="datePicker" />
                        </div>

                        <div>
                            <button class="btn btn-secondary">Clear</button>
                        </div>
                        <!-- <label class="norecords">No Records Found</label> -->
                        <!-- Undertime Records -->
                     <asp:Label ID="lblNoRecords" runat="server" CssClass="norecords" Text="No Records Available" Visible="false"></asp:Label>
                     <table class="tableLayout">
                                    <asp:DataGrid ID="dgUndertime" runat="server" AlternatingItemStyle-CssClass="AlternateMainTableRow"
                                        ItemStyle-CssClass="MainTableRow" HeaderStyle-CssClass="MainTableRow" CssClass="tableLayout"
                                        CellSpacing="1" CellPadding="3" Width="100%" BorderWidth="0" HorizontalAlign="Center"
                                        AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:BoundColumn HeaderStyle-Width="10%" HeaderText="DATE" DataField="DATE"
                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
                                            <asp:BoundColumn HeaderStyle-Width="10%" HeaderText="START TIME" DataField="TIMEIN" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
                                            <asp:BoundColumn HeaderStyle-Width="10%" HeaderText="END TIME" DataField="TIMEOUT"
                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
                                            <asp:BoundColumn HeaderStyle-Width="10%" HeaderText="UNDERTIME (HOUR/S)" DataField="total_undertime"
                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
                                                <asp:BoundColumn HeaderStyle-Width="10%" HeaderText="REASON" DataField="undertime_reason"
                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
                                                <asp:BoundColumn HeaderStyle-Width="10%" HeaderText="STATUS" DataField="status"
                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
                                                <asp:BoundColumn HeaderStyle-Width="10%" HeaderText="REMARKS" DataField="remarks"
                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </table>

                        <div class="pagination-container">
                            <div class="showmore-container">
                                <a class="btn-showmore" href="#"><i class="fas fa-chevron-down"></i>More Results</a
                  >
                </div>
               <%-- <div class="pagination" onclick="selectPage()">
                  <a href="#">«</a>
                                <a class="active" href="#">1</a>
                                <a href="#">2</a>
                                <a href="#">3</a>
                                <a href="#">4</a>
                                <a href="#">5</a>
                                <a href="#">6</a>
                                <a href="#">7</a>
                                <a href="#">8</a>
                                <a href="#">9</a>
                                <a href="#">10</a>
                                <a href="#">»</a>
                            </div>
                        </div>--%>
                    </div>

                    <div class="card card-2 hidden">
                        <div class="container-sm">
                            <!-- Pending UnderTime Records -->
                           <asp:Label ID="lblPendingNoRecords" runat="server" CssClass="norecords" Text="No Records Available" Visible="false"></asp:Label>
                     <table class="tableLayout">
                                    <asp:DataGrid ID="dgPendingUndertime" runat="server" AlternatingItemStyle-CssClass="AlternateMainTableRow"
                                        ItemStyle-CssClass="MainTableRow" HeaderStyle-CssClass="MainTableRow" CssClass="tableLayout"
                                        CellSpacing="1" CellPadding="3" Width="100%" BorderWidth="0" HorizontalAlign="Center"
                                        AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:BoundColumn HeaderStyle-Width="10%" HeaderText="DATE" DataField="DATE"
                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
                                            <asp:BoundColumn HeaderStyle-Width="10%" HeaderText="START TIME" DataField="TIMEIN" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
                                            <asp:BoundColumn HeaderStyle-Width="10%" HeaderText="END TIME" DataField="TIMEOUT"
                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
                                            <asp:BoundColumn HeaderStyle-Width="10%" HeaderText="UNDERTIME (HOUR/S)" DataField="total_undertime"
                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
                                                <asp:BoundColumn HeaderStyle-Width="10%" HeaderText="REASON" DataField="undertime_reason"
                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
                                                  <asp:TemplateColumn HeaderText="ACTION" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate><asp:LinkButton ID="btnCancel" runat="server" CssClass="btn-table btn-secondary" CommandArgument=<%# Eval("undertime_id") %>  OnCommand="itemcommand">Cancel</asp:LinkButton>
                                            </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </table>
                            <div class="pagination-container">
                                <div class="showmore-container">
                                    <a class="btn-showmore" href="#"><i class="fas fa-chevron-down"></i>Show More</a
                      >
                    </div>
                    <div class="pagination" onclick="selectPage()">
                      <a href="#">«</a>
                                    <a class="active" href="#">1</a>
                                    <a href="#">2</a>
                                    <a href="#">3</a>
                                    <a href="#">4</a>
                                    <a href="#">5</a>
                                    <a href="#">6</a>
                                    <a href="#">7</a>
                                    <a href="#">8</a>
                                    <a href="#">9</a>
                                    <a href="#">10</a>
                                    <a href="#">»</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
    </div>
    <!-- Modal Section -->
    <!-- btnUndertimeApplication -->
    <div class="modal modal-undertime-application">
        <div class="modal-content">
            <h5>
                Undertime Application Form</h5>
            <hr />
         
                <div class="">
                    <label for="txtDepartment" class="label-6">
                        Department
                    </label>
                    <asp:DropDownList ID="drpDepartment" CssClass="select" runat="server" OnSelectedIndexChanged="drpDepartment_SelectedIndexChanged"
                        AutoPostBack="true" required>
                    </asp:DropDownList>
                    <label for="txtEmployee" class="label-6">
                        Employee
                    </label>
                    <asp:DropDownList ID="drpEmployee" CssClass="select" runat="server" required>
                    </asp:DropDownList>
                    <label for="txtDateUT" class="label-6">
                        Date:
                    </label>
                    <input name="txtDateUT" type="text" id="txtDateUT" class="utPicker" runat="server" />
                    <label for="txtHours" class="label-6">
                        Hours</label>
                    <p class="text" id="txtHours" runat="server">
                        0.00</p>
                    <label for="txtAreaReasonOT" class="label-6">
                        Reason</label>
                    <textarea name="txtAreaReasonUT" id="txtAreaReasonUT" runat="server"></textarea>
                </div>
                <div class="modal-btn-container">
                    <a class="btn-sm btn-primary mx-3" id="btnRequestUndertime" runat="server" onserverclick="btnRequestUndertime_Click">
                        Request Undertime</a> <a class="btn-sm btn-secondary mx-3" onclick="closeModal(`#btnCloseUndertimeApplication`);">
                            Cancel</a>
                </div>
       
    </div>
    </div>
    <section id="footer">
            <a href="https://systemoph.com/">
              <svg
                class="systemologo"
                width="500"
                height="100"
                viewBox="0 0 260 58"
                fill="none"
                xmlns="http://www.w3.org/2000/svg"
              >
                <g clip-path="url(#clip0)">
                  <path
                    d="M0 41.4494V36.4176H6.71272V39.3857L8.36372 41.0439H18.7356L20.4383 39.3337V33.5015L18.7873 31.8433H5.51198L0.0983359 26.4062V17.1016L5.50681 11.6696H21.3337L26.7474 17.1016V22.1801H20.0346V19.1601L18.3836 17.5019H8.46724L6.81623 19.1601V24.3425L8.46724 26.0007H21.7374L27.1459 31.4327V41.3454L21.6391 46.8813H5.40847L0 41.4494Z"
                    fill="#011F4B"
                  />
                  <path
                    d="M56.1031 21.8267V52.6148L50.6947 58H37.5694L32.9114 53.3737V49.9534H39.5206V51.4608L40.4729 52.4693H47.786L49.4888 50.7071V43.1595L45.783 46.8813H37.5177L32.1558 41.4494V21.8267H38.7701V39.4845L40.4729 41.2466H44.8307L49.4888 36.2669V21.8267H56.1031Z"
                    fill="#011F4B"
                  />
                  <path
                    d="M60.8596 41.9536V38.8867H67.3705V40.2954L68.4729 41.4545H76.5882L77.6906 40.2954V38.133L76.5364 36.927H65.7661L61.0563 32.15V26.6609L65.8126 21.8318H78.8344L83.6943 26.7596V29.8265H77.1834V28.4178L76.081 27.2586H68.6696L67.5672 28.4178V30.3775L68.7213 31.5834H79.4917L84.2015 36.3604V42.0523L79.4451 46.8813H65.7195L60.8596 41.9536Z"
                    fill="#011F4B"
                  />
                  <path
                    d="M91.2142 41.7508V27.4146H86.7063V21.8267H91.3126V13.6813H97.8234V21.8319H105.437V27.4666H97.8234V39.5885L99.5262 41.2466H105.437V46.8813H96.3691L91.2142 41.7508Z"
                    fill="#011F4B"
                  />
                  <path
                    d="M108.444 41.7509V27.0611L113.604 21.8267H127.681L132.888 27.0611V36.5684H115.053V39.786L116.657 41.4442H124.824L126.325 39.8848V38.5748H132.836V41.8444L127.878 46.8762H113.5L108.444 41.7509ZM126.274 31.9421V29.026L124.571 27.2639H116.756L115.053 29.026V31.9421H126.274Z"
                    fill="#011F4B"
                  />
                  <path
                    d="M138.395 11.6696H144.704L155.122 34.4112H155.221L165.691 11.6696H172V46.8865H165.437V25.4497H165.339L157.275 41.9484H153.068L145.056 25.4497H144.957V46.8813H138.395V11.6696Z"
                    fill="#005B96"
                  />
                  <path
                    d="M177.765 41.6521V27.0611L182.925 21.8267H197.251L202.411 27.0611V41.6521L197.251 46.8865H182.925L177.765 41.6521ZM194.048 41.3506L195.802 39.5885V29.1248L194.048 27.3626H186.134L184.38 29.1248V39.5885L186.134 41.3506H194.048Z"
                    fill="#005B96"
                  />
                  <path
                    d="M205.672 38.6944H210.785L212.033 39.9575V42.8216L210.775 44.0952H207.261V46.8761H205.677V38.6944H205.672ZM210.061 42.7644L210.47 42.3538V40.4409L210.061 40.0303H207.256V42.7644H210.061Z"
                    fill="#0585B5"
                  />
                  <path
                    d="M213.197 38.6944H214.781V42.1199H218.228V38.6944H219.812V46.8813H218.228V43.4766H214.781V46.8813H213.197V38.6944Z"
                    fill="#0585B5"
                  />
                  <path
                    d="M196.605 11.9556C197.23 11.9556 197.738 11.4459 197.738 10.8172C197.738 10.1885 197.23 9.6788 196.605 9.6788C195.979 9.6788 195.471 10.1885 195.471 10.8172C195.471 11.4459 195.979 11.9556 196.605 11.9556Z"
                    fill="#6497B1"
                  />
                  <path
                    d="M201.837 12.8756C202.969 12.8756 203.886 11.954 203.886 10.8172C203.886 9.68032 202.969 8.75873 201.837 8.75873C200.705 8.75873 199.787 9.68032 199.787 10.8172C199.787 11.954 200.705 12.8756 201.837 12.8756Z"
                    fill="#005B96"
                  />
                  <path
                    d="M208.731 14.1023C210.537 14.1023 212.002 12.6315 212.002 10.8172C212.002 9.00281 210.537 7.53198 208.731 7.53198C206.924 7.53198 205.46 9.00281 205.46 10.8172C205.46 12.6315 206.924 14.1023 208.731 14.1023Z"
                    fill="#011F4B"
                  />
                  <path
                    d="M249.892 12.4701L243.599 6.77307C242.589 5.85821 242.507 4.2884 243.418 3.27477C244.328 2.26115 245.891 2.17798 246.901 3.09284L253.194 8.78992C254.203 9.70478 254.286 11.2746 253.375 12.2882C252.464 13.307 250.901 13.385 249.892 12.4701Z"
                    fill="#005B96"
                  />
                  <path
                    d="M243.764 14.9496L249.758 8.93027C250.72 7.96343 252.283 7.96343 253.246 8.93027C254.209 9.89711 254.209 11.4669 253.246 12.4338L247.253 18.4531C246.29 19.42 244.727 19.42 243.764 18.4531C242.802 17.4863 242.802 15.9217 243.764 14.9496Z"
                    fill="#005B96"
                  />
                  <path
                    d="M241.063 13.884H217.26C215.454 13.884 213.989 12.413 213.989 10.5989C213.989 8.78473 215.454 7.31367 217.26 7.31367H241.063C242.869 7.31367 244.334 8.78473 244.334 10.5989C244.334 12.413 242.869 13.884 241.063 13.884Z"
                    fill="#011F4B"
                  />
                  <path
                    d="M239.086 12.9536L230.944 5.58272C229.635 4.39756 229.531 2.37031 230.711 1.0552C231.892 -0.259907 233.91 -0.363868 235.219 0.82129L243.361 8.19215C244.67 9.3773 244.774 11.4045 243.593 12.7197C242.413 14.0348 240.395 14.1387 239.086 12.9536Z"
                    fill="#011F4B"
                  />
                  <path
                    d="M231.162 16.166L238.909 8.38447C240.157 7.13173 242.175 7.13173 243.423 8.38447C244.67 9.6372 244.67 11.6644 243.423 12.9172L235.675 20.6987C234.427 21.9514 232.409 21.9514 231.162 20.6987C229.914 19.4459 229.914 17.4187 231.162 16.166Z"
                    fill="#011F4B"
                  />
                  <path
                    d="M257.692 11.7424L254.167 8.55081C253.603 8.0362 253.556 7.16293 254.069 6.59114C254.581 6.02455 255.451 5.97777 256.02 6.49238L259.545 9.68399C260.109 10.1986 260.155 11.0719 259.643 11.6437C259.131 12.2102 258.256 12.257 257.692 11.7424Z"
                    fill="#6497B1"
                  />
                  <path
                    d="M254.26 13.1355L257.614 9.76716C258.152 9.22656 259.027 9.22656 259.565 9.76716C260.103 10.3078 260.103 11.1862 259.565 11.7268L256.211 15.0952C255.673 15.6358 254.799 15.6358 254.26 15.0952C253.722 14.5546 253.722 13.6761 254.26 13.1355Z"
                    fill="#6497B1"
                  />
                  <path
                    d="M224.066 40.1083C223.9 40.1083 223.735 40.0459 223.611 39.9159L218.927 35.2117C218.673 34.957 218.673 34.5463 218.927 34.2916L223.611 29.5874C223.864 29.3327 224.273 29.3327 224.527 29.5874L229.211 34.2916C229.464 34.5463 229.464 34.957 229.211 35.2117L224.527 39.9159C224.397 40.0407 224.232 40.1083 224.066 40.1083ZM219.931 34.7543L224.066 38.9075L228.201 34.7543L224.066 30.601L219.931 34.7543Z"
                    fill="#0585B5"
                  />
                  <path
                    d="M231.043 47.1101C230.877 47.1101 230.711 47.0477 230.587 46.9177L225.903 42.2135C225.65 41.9588 225.65 41.5481 225.903 41.2934L230.587 36.5892C230.841 36.3345 231.25 36.3345 231.503 36.5892L236.187 41.2934C236.436 41.5481 236.436 41.9588 236.187 42.2083L231.503 46.9125C231.374 47.0477 231.208 47.1101 231.043 47.1101ZM226.907 41.7612L231.043 45.9145L235.178 41.7612L231.043 37.608L226.907 41.7612Z"
                    fill="#0585B5"
                  />
                  <path
                    d="M231.043 33.1013C230.877 33.1013 230.711 33.0389 230.587 32.9089L225.903 28.2047C225.65 27.95 225.65 27.5393 225.903 27.2846L230.587 22.5804C230.841 22.3257 231.25 22.3257 231.503 22.5804L236.187 27.2846C236.436 27.5393 236.436 27.95 236.187 28.1995L231.503 32.9037C231.374 33.0389 231.208 33.1013 231.043 33.1013ZM226.907 27.7473L231.043 31.9005L235.178 27.7473L231.043 23.594L226.907 27.7473Z"
                    fill="#0585B5"
                  />
                  <path
                    d="M238.019 39.6664C237.854 39.6664 237.688 39.604 237.564 39.4741L232.88 34.7698C232.626 34.5151 232.626 34.1045 232.88 33.8498L237.564 29.1455C237.818 28.8908 238.226 28.8908 238.48 29.1455L243.164 33.8498C243.417 34.1045 243.417 34.5151 243.164 34.7698L238.48 39.4741C238.351 39.604 238.185 39.6664 238.019 39.6664ZM233.884 34.3176L238.019 38.4709L242.155 34.3176L238.019 30.1643L233.884 34.3176Z"
                    fill="#0585B5"
                  />
                </g>
                <defs>
                  <clipPath id="clip0">
                    <rect width="500" height="100" fill="white" />
                  </clipPath>
                </defs>
              </svg>
              <p>Copyright &copy; 2019 SysteMoPH - All Rights Reserved.</p>
            </a>
            </section>
    </section> </div>
    <!-- JavaScript -->
    <script src="Scripts/jquery-3.4.1.min.js"></script>
    <script src="Scripts/jquery-ui.min.js"></script>
    <script src="Scripts/main.js"></script>
    </form>
</body>
</html>
