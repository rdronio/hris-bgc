<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayrollModal.aspx.cs" Inherits="HRIS_Basic.PayrollModal" %>

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

        <title>HRIS</title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="modal modal-create-payroll">
                <div class="modal-content">
                    <h5>Generate Payroll</h5>
                    <hr />

                    <div>
                        <label class="label-6 block">Pay Period</label>
                        <label for="txtDateFromModal" class="label-6">From: </label>
                        <input name="txtDateFromModal" type="text" id="txtDateFromModal" class="datePicker" />
                        <label for="txtDateToModal" class="label-6">To: </label>
                        <input name="txtDateToModal" type="text" id="txtDateToModal" class="datePicker" />
                        <button class="btn-sm btn-secondary">Clear</button>
                    </div>

                    <div>
                        <label for="showEmployeeByModal" class="label-6 block">Show Employee by</label>
                        <select name="showEmployeeByModal" id="showEmployeeByModal" class="select" onchange="getEmployeeByModal()">
                            <option value="">--Please choose an option--</option>
                            <option value="department">Department</option>
                            <option value="position">Position</option>
                        </select>
                        <select name="showEmployeeByCategoryModal" id="showEmployeeByCategoryModal" class="select">
                        </select>
                    </div>

                    <div>
                        <label for="drpDownEmployee" class="label-6 block">Employee</label>
                        <select name="drpDownEmployee" id="drpDownEmployee" class="select ">
                        </select>
                    </div>

                    <div class="hidden">
                        <label class="label-6">Type</label>
                        <div class="modal-rd-container">
                            <label for="opt1" class="radio">
                                <input type="radio" name="rdo" id="opt1" class="hidden" />
                                <span class="label"></span>Daily
                            </label>

                            <label for="opt2" class="radio">
                                <input type="radio" name="rdo" id="opt2" class="hidden" />
                                <span class="label"></span>Monthly
                            </label>
                        </div>
                    </div>

                    <div class="hidden">
                        <label class="label-6">Absent (days)</label>
                        <input name="txtAbsent" type="text" id="txtAbsent" />
                        <label class="label-6">Overtime (hours)</label>
                        <input name="txtOvertime" type="text" id="txtOvertime" />
                        <label class="label-6">Late (minutes)</label>
                        <input name="txtLate" type="text" id="txtLate" />
                    </div>

                    <hr />
                    <div>
                        <label class="label-6">Gross Pay</label>
                        <p class="label-6" id="lblGrossPay">20,000</p>
                        <label class="label-6">SSS/GSIS</label>
                        <p class="label-6" id="lblSssGsis">20,000</p>
                        <label class="label-6">Pag-ibig</label>
                        <p class="label-6" id="lblPagibig">20,000</p>
                        <label class="label-6">Philhealth</label>
                        <p class="label-6" id="lblPhilhealth">20,000</p>
                        <label class="label-6">Other Deductions</label>
                        <p class="label-6" id="lblOtherDeduction">20,000</p>
                    </div>

                    <hr />
                    <div>
                        <label class="label-6">Net Pay</label>
                        <p class="label-6">20,000</p>
                    </div>
                    <hr />

                    <div class="modal-btn-container">

                        <asp:Button runat="server" ID="btnCreatePayrollModal" Text="Create Payroll" CssClass="btn-sm btn-primary mx-3" OnClick="btnCreatePayrollModal_Click" UseSubmitBehavior="false" data-dismiss="modal"></asp:Button>
                        <button class="btn-sm btn-secondary mx-3" onclick="closeModal(`#btnCloseCreatePayroll`);">
                            Cancel
                        </button>
                    </div>
                </div>
            </div>
            <script src="Scripts/main.js"></script>
    </form>
</body>
</html>
