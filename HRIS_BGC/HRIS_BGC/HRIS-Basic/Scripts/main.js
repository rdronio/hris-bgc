function togglePassword() {
  try {
    var x = document.querySelector("#txtPass");

    if (x.type === "password") {
      x.type = "text";
    } else {
      x.type = "password";
    }
  } catch (e) {
    console.log(e);
  }
}

function togglePassword2() {
  try {
    var x = document.querySelector("#txtPass2");

    if (x.type === "password") {
      x.type = "text";
    } else {
      x.type = "password";
    }
  } catch (e) {
    console.log(e);
  }
}

// navigation
$(document).ready(function() {
  try {
    $(".nav-list").on("click", "li", function() {
      $(".nav-list li.current-page").removeClass("current-page");
      $(this).addClass("current-page");
    });

    $(".btn-dropdown").on("click", function() {
      this.classList.add("hidden");
    });

    $(document).click(function(e) {
      e.stopPropagation();
      let container = $(".search-main-container");
      let showSearchOptions = document.querySelector(".showSearchOptions");

      //check if the clicked area is dropDown or not
      if (container.has(e.target).length === 0) {
        $(".search-filter-container").addClass("hidden");
        showSearchOptions.classList.remove("rotate");
        hideDataList();
      }
    });

    $(".datalist-data").on("click", "li", function() {
      $(".datalist-data li").removeClass("selected-datalist");
      $(this).addClass("selected-datalist");

      let selectedOption = document.querySelector(".selected-datalist div span")
        .innerHTML;
      let txtSearch = document.querySelector("#txtSearch");

      txtSearch.value = selectedOption;
      hideDataList();
    });

    $(document).click(function(e) {
      e.stopPropagation();
      let container = $(".btn-tools-container");

      //check if the clicked area is dropDown or not
      if (container.has(e.target).length === 0) {
        $(".btn-dropdown").addClass("hidden");
      }
    });

    $(document).click(function(e) {
      e.stopPropagation();
      let containerRec = $(".searchRec-main-container");
      const showSearchRecOptions = document.querySelector(
        ".showSearchRecOptions"
      );

      //check if the clicked area is dropDown or not
      if (containerRec.has(e.target).length === 0) {
        $(".searchRec-filter-container").addClass("hidden");
        showSearchRecOptions.classList.remove("rotate");
        hideDataList();
      }
    });
  } catch (e) {
    console.log(e);
  }
});

// function selectNav() {
//   $(".nav").on("click", "li", function() {
//     $(".nav-list li.current-page").removeClass("current-page");
//     $(this).addClass("current-page");
//   });
// }

// function selectPage() {
//   try {
//     $(".pagination-container").on("click", "a", function() {
//       $(".pagination a.active").removeClass("active");
//       $(this).addClass("active");
//     });
//   } catch (e) {
//     console.log(e);
//   }
// }

function selectTab() {
  try {
    $("#tab").on("click", "li", function() {
      $(".tab-list li.current-tab").removeClass("current-tab");
      $(this).addClass("current-tab");
    });
  } catch (e) {
    console.log(e);
  }
}

function selectTabEI() {
  try {
    $("#tabIE").on("click", "li", function() {
      $(".modal-tab-list li.current-tab").removeClass("current-tab");
      $(this).addClass("current-tab");
    });
  } catch (e) {
    console.log(e);
  }
}
function selectTabViewEI() {
  try {
    $("#tabViewIE").on("click", "li", function() {
      $(".modal-tab-list li.current-tab").removeClass("current-tab");
      $(this).addClass("current-tab");
    });
  } catch (e) {
    console.log(e);
  }
}

function toggleMenu() {
  try {
    const menu = document.querySelector("#nav");
    const content = document.querySelector(".content");
    const menuBtn = document.querySelector(".menu-btn");
    const body = document.querySelector("body");

    menu.classList.toggle("show");
    content.classList.toggle("blur");
    menuBtn.classList.toggle("hidden");
    body.classList.toggle("noscroll");
  } catch (e) {
    console.log(e);
  }
}

// Time Picker
$(".timepicker").each(function() {
  try {
    $(this).timepicker({
      timeFormat: "hh:mm p",
      interval: 1,
      minTime: "12:00 AM",
      maxTime: "11:59 PM",
      defaultTime: "null",
      startTime: "8:00 AM",
      dynamic: false,
      dropdown: true,
      scrollbar: true
    });
  } catch (e) {
    console.log(e);
  }
});

// Birthday Picker
$(".bdayPicker").each(function() {
  try {
    //Create jQueryUI datepicker
    $(this).datepicker({
      changeMonth: true,
      changeYear: true,
      defaultDate: "-18Y",
      yearRange: "-80:-18",
      onChangeMonthYear: function(year, month) {
        var $datepicker = jQuery(this);
        var date = new Date($datepicker.datepicker("getDate"));
        var lastDayOfMonth = new Date(year, month, 0).getDate();
        var preservedDay = Math.min(
          lastDayOfMonth,
          Math.max(1, date.getDate())
        );
        $datepicker.datepicker(
          "setDate",
          month + "/" + preservedDay + "/" + year
        );
      }
    });
  } catch (e) {
    console.log(e);
  }
});

// Licence Expiration Date Picker
$(".expDatePicker").each(function() {
  try {
    //Create jQueryUI datepicker
    $(this).datepicker({
      changeMonth: true,
      changeYear: true,
      yearRange: "-10:+10",
      onChangeMonthYear: function(year, month) {
        var $datepicker = jQuery(this);
        var date = new Date($datepicker.datepicker("getDate"));
        var lastDayOfMonth = new Date(year, month, 0).getDate();
        var preservedDay = Math.min(
          lastDayOfMonth,
          Math.max(1, date.getDate())
        );
        $datepicker.datepicker(
          "setDate",
          month + "/" + preservedDay + "/" + year
        );
      }
    });
  } catch (e) {
    console.log(e);
  }
});

// datePicker Payroll
// $(".datePickerPayroll").each(function() {
//   //Create jQueryUI datepicker
//   $(this).datepicker({
//     changeMonth: true,
//     changeYear: true,
//     yearRange: "-10:+10"
//   });
// });

// Date Picker
$(".datePicker").each(function() {
  try {
    //Create jQueryUI datepicker
    $(this).datepicker({
      changeMonth: true,
      changeYear: true,
      yearRange: "-10:+10",
      beforeShowDay: $.datepicker.noWeekends,
      onChangeMonthYear: function(year, month) {
        var $datepicker = jQuery(this);
        var date = new Date($datepicker.datepicker("getDate"));
        var lastDayOfMonth = new Date(year, month, 0).getDate();
        var preservedDay = Math.min(
          lastDayOfMonth,
          Math.max(1, date.getDate())
        );
        $datepicker.datepicker(
          "setDate",
          month + "/" + preservedDay + "/" + year
        );
      }
    });
  } catch (e) {
    console.log(e);
  }
});

// Leave Picker
$(".leavePicker").each(function() {
  try {
    //Create jQueryUI datepicker
    $(this).datepicker({
      changeMonth: true,
      changeYear: true,
      yearRange: "-10:+10",
      beforeShowDay: $.datepicker.noWeekends,
      onChangeMonthYear: function(year, month) {
        var $datepicker = jQuery(this);
        var date = new Date($datepicker.datepicker("getDate"));
        var lastDayOfMonth = new Date(year, month, 0).getDate();
        var preservedDay = Math.min(
          lastDayOfMonth,
          Math.max(1, date.getDate())
        );
        $datepicker.datepicker(
          "setDate",
          month + "/" + preservedDay + "/" + year
        );
      }
    });
  } catch (e) {
    console.log(e);
  }
});

// OT Picker
$(".otPicker").each(function() {
  try {
    //Create jQueryUI datepicker
    $(this).datepicker({
      changeMonth: true,
      changeYear: true,
      yearRange: "-10:+10",
      beforeShowDay: $.datepicker.noWeekends,
      onChangeMonthYear: function(year, month) {
        var $datepicker = jQuery(this);
        var date = new Date($datepicker.datepicker("getDate"));
        var lastDayOfMonth = new Date(year, month, 0).getDate();
        var preservedDay = Math.min(
          lastDayOfMonth,
          Math.max(1, date.getDate())
        );
        $datepicker.datepicker(
          "setDate",
          month + "/" + preservedDay + "/" + year
        );
      }
    });
  } catch (e) {
    console.log(e);
  }
});

// Undertime Picker
$(".utPicker").each(function() {
  try {
    //Create jQueryUI datepicker
    $(this).datepicker({
      changeMonth: true,
      changeYear: true,
      yearRange: "-10:+10",
      beforeShowDay: $.datepicker.noWeekends,
      onChangeMonthYear: function(year, month) {
        var $datepicker = jQuery(this);
        var date = new Date($datepicker.datepicker("getDate"));
        var lastDayOfMonth = new Date(year, month, 0).getDate();
        var preservedDay = Math.min(
          lastDayOfMonth,
          Math.max(1, date.getDate())
        );
        $datepicker.datepicker(
          "setDate",
          month + "/" + preservedDay + "/" + year
        );
      }
    });
  } catch (e) {
    console.log(e);
  }
});

// Termination Date Picker
$(".terminationDatePicker").each(function() {
  try {
    //Create jQueryUI datepicker
    $(this).datepicker({
      changeMonth: true,
      changeYear: true,
      yearRange: "-10:+10",
      onChangeMonthYear: function(year, month) {
        var $datepicker = jQuery(this);
        var date = new Date($datepicker.datepicker("getDate"));
        var lastDayOfMonth = new Date(year, month, 0).getDate();
        var preservedDay = Math.min(
          lastDayOfMonth,
          Math.max(1, date.getDate())
        );
        $datepicker.datepicker(
          "setDate",
          month + "/" + preservedDay + "/" + year
        );
      }
    });
  } catch (e) {
    console.log(e);
  }
});

$(".startDatePicker").each(function() {
  try {
    $(this).datepicker({
      changeMonth: true,
      changeYear: true,
      yearRange: "-10:+10",
      onChangeMonthYear: function(year, month) {
        var $datepicker = jQuery(this);
        var date = new Date($datepicker.datepicker("getDate"));
        var lastDayOfMonth = new Date(year, month, 0).getDate();
        var preservedDay = Math.min(
          lastDayOfMonth,
          Math.max(1, date.getDate())
        );
        $datepicker.datepicker(
          "setDate",
          month + "/" + preservedDay + "/" + year
        );
      }

      // onSelect: function(date) {
      //   let selectedDate = new Date(date);
      //   let payFreq = document.querySelector("#drpDownPayFreq").value;
      //   let endDate = new Date(date);
      //   console.log("this is date from onSelect:" + date);

      //   if (payFreq === "weekly") {
      //     endDate.setDate(selectedDate.getDate() + 7);
      //     $(".endDatePicker").datepicker("setDate", endDate);
      //   } else if (payFreq === "twoWeeks") {
      //     endDate.setDate(selectedDate.getDate() + 14);
      //     $(".endDatePicker").datepicker("setDate", endDate);
      //   } else if (payFreq === "monthly") {
      //     endDate.setMonth(selectedDate.getMonth() + 1);
      //     $(".endDatePicker").datepicker("setDate", endDate);
      //   } else {
      //     endDate.setDate(selectedDate.getDate());
      //     $(".endDatePicker").datepicker("option", "minDate", selectedDate);
      //   }
      // }
    });
  } catch (e) {
    console.log(e);
  }
});

$(".endDatePicker").each(function() {
  try {
    $(this).datepicker({});
  } catch (e) {
    console.log(e);
  }
});

function generatePayPeriod(
  drpDownPayFreqID,
  startDatePickerID,
  endDatePickerID,
  formID
) {
  try {
    let payFreq = document.querySelector(drpDownPayFreqID).value;
    let startDatePicker = document.querySelector(startDatePickerID).value;
    let endDatePicker = document.querySelector(endDatePickerID);

    let selectedDate = new Date(startDatePicker);
    let endDate = new Date(startDatePicker);

    if (startDatePicker != "") {
      endDatePicker.classList.add("disabled");
      if (payFreq === "weekly") {
        endDate.setDate(selectedDate.getDate() + 7);
        $(endDatePicker).datepicker("setDate", endDate);
      } else if (payFreq === "twoWeeks") {
        endDate.setDate(selectedDate.getDate() + 14);
        $(endDatePicker).datepicker("setDate", endDate);
      } else if (payFreq === "monthly") {
        endDate.setMonth(selectedDate.getMonth() + 1);
        $(endDatePicker).datepicker("setDate", endDate);
      } else if (payFreq === "custom") {
        endDatePicker.classList.remove("disabled");
        endDate.setDate(selectedDate.getDate());
        $(endDatePicker).datepicker("option", "minDate", selectedDate);
      } else {
        reset(formID);
      }
    }
  } catch (e) {
    console.log(e);
  }
}

function hidePayslipInfo() {
  try {
    const drpEmployee = document.querySelector("#drpDownEmployee");
    const emp = drpEmployee.options[drpEmployee.selectedIndex].value;

    const child7 = document.querySelector(
      "#payroll .modal-content > *:nth-child(7)"
    );
    const child11 = document.querySelector(
      "#payroll .modal-content > *:nth-child(11)"
    );
    const child12 = document.querySelector(
      "#payroll .modal-content > *:nth-child(12)"
    );
    const child13 = document.querySelector(
      "#payroll .modal-content > *:nth-child(13)"
    );
    const child14 = document.querySelector(
      "#payroll .modal-content > *:nth-child(14)"
    );
    const child15 = document.querySelector(
      "#payroll .modal-content > *:nth-child(15)"
    );
    const child16 = document.querySelector(
      "#payroll .modal-content > *:nth-child(16)"
    );
    const child17 = document.querySelector(
      "#payroll .modal-content > *:nth-child(17)"
    );

    console.log();
    if (emp === "All") {
      child7.classList.add("hidden");
      child11.classList.add("hidden");
      child12.classList.add("hidden");
      child13.classList.add("hidden");
      child14.classList.add("hidden");
      child15.classList.add("hidden");
      child16.classList.add("hidden");
      child17.classList.add("hidden");
    } else {
      child7.classList.remove("hidden");
      child11.classList.remove("hidden");
      child12.classList.remove("hidden");
      child13.classList.remove("hidden");
      child14.classList.remove("hidden");
      child15.classList.remove("hidden");
      child16.classList.remove("hidden");
      child17.classList.remove("hidden");
    }
  } catch (e) {
    console.log(e);
  }
}

function calcBusinessDays(dDate1, dDate2) {
  try {
    // input given as Date objects
    var iWeeks,
      iDateDiff,
      iAdjust = 0;
    if (dDate2 < dDate1) return -1; // error code if dates transposed
    var iWeekday1 = dDate1.getDay(); // day of week
    var iWeekday2 = dDate2.getDay();
    iWeekday1 = iWeekday1 == 0 ? 7 : iWeekday1; // change Sunday from 0 to 7
    iWeekday2 = iWeekday2 == 0 ? 7 : iWeekday2;
    if (iWeekday1 > 5 && iWeekday2 > 5) iAdjust = 1; // adjustment if both days on weekend
    iWeekday1 = iWeekday1 > 5 ? 5 : iWeekday1; // only count weekdays
    iWeekday2 = iWeekday2 > 5 ? 5 : iWeekday2;

    // calculate differnece in weeks (1000mS * 60sec * 60min * 24hrs * 7 days = 604800000)
    iWeeks = Math.floor((dDate2.getTime() - dDate1.getTime()) / 604800000);

    if (iWeekday1 < iWeekday2) {
      //Equal to makes it reduce 5 days
      iDateDiff = iWeeks * 5 + (iWeekday2 - iWeekday1);
    } else {
      iDateDiff = (iWeeks + 1) * 5 - (iWeekday1 - iWeekday2);
    }

    iDateDiff -= iAdjust; // take into account both days on weekend

    return iDateDiff + 1; // add 1 because dates are inclusive
  } catch (e) {
    console.log(e);
  }
}

// leave records From To Counter
function getBusinessDateCount(startDate, endDate) {
  try {
    var elapsed, daysBeforeFirstSaturday, daysAfterLastSunday;
    var ifThen = function(a, b, c) {
      return a == b ? c : a;
    };

    elapsed = endDate - startDate;
    elapsed /= 86400000;

    daysBeforeFirstSunday = (7 - startDate.getDay()) % 7;
    daysAfterLastSunday = endDate.getDay();

    elapsed -= daysBeforeFirstSunday + daysAfterLastSunday;
    elapsed = (elapsed / 7) * 5;
    elapsed +=
      ifThen(daysBeforeFirstSunday - 1, -1, 0) +
      ifThen(daysAfterLastSunday, 6, 5);

    return Math.ceil(elapsed);
  } catch (e) {
    console.log(e);
  }
}

function calc() {
  try {
    let start = document.querySelector("#txtDateFrom").value,
      end = document.querySelector("#txtDateTo").value,
      result = getBusinessDateCount(new Date(start), new Date(end));
    document.querySelector("#result").innerHTML = result;
  } catch (e) {
    console.log(e);
  }
}

// Show Employee based on selected option
function getEmployeeBy() {
  try {
    const select = document.querySelector("#showEmployeeBy");
    const showEmployeeByCategory = document.querySelector(
      "#showEmployeeByCategory"
    );
    removeOptions(showEmployeeByCategory);
    const departments = ["Programming", "Marketing", "Sales", "Human Resource"];
    const positions = ["Manager", "Programmer", "Sales Assistant", "Technical"];

    if (select.options[select.selectedIndex].value == "department") {
      for (dept in departments) {
        showEmployeeByCategory.appendChild(new Option(departments[dept]));
      }
    } else if (select.options[select.selectedIndex].value == "position") {
      for (pos in positions) {
        showEmployeeByCategory.appendChild(new Option(positions[pos]));
      }
    } else {
    }
  } catch (e) {
    console.log(e);
  }
}

// Show Employee based on selected option
function getEmployeeByModal() {
  try {
    const select = document.querySelector("#showEmployeeByModal");
    const showEmployeeByCategory = document.querySelector(
      "#showEmployeeByCategoryModal"
    );
    removeOptions(showEmployeeByCategory);
    const departments = ["Programming", "Marketing", "Sales", "Human Resource"];
    const positions = ["Manager", "Programmer", "Sales Assistant", "Technical"];

    if (select.options[select.selectedIndex].value == "department") {
      for (dept in departments) {
        showEmployeeByCategory.appendChild(new Option(departments[dept]));
      }
    } else if (select.options[select.selectedIndex].value == "position") {
      for (pos in positions) {
        showEmployeeByCategory.appendChild(new Option(positions[pos]));
      }
    } else {
    }
  } catch (e) {
    console.log(e);
  }
}

// Clear Options
function removeOptions(select_box) {
  try {
    let i;
    for (i = select_box.options.length - 1; i >= 0; i--) {
      select_box.remove(i);
    }
  } catch (e) {
    console.log(e);
  }
}

function scrollTop() {
  try {
    document.body.scrollTop = 0; // For Safari
    document.documentElement.scrollTop = 0; // For Chrome, Firefox, IE and Opera
  } catch (e) {
    console.log(e);
  }
}

function scrollTopModal() {
  try {
    document.querySelector(".modal-content").scrollTop = 0;
  } catch (e) {
    console.log(e);
  }
}

function scrollTopDataList() {
  try {
    document.querySelector(".datalist-data").scrollTop = 0;
  } catch (e) {
    console.log(e);
  }
}

function openModal(btn_id) {
  try {
    // List modal
    const modalTimeAlteration = document.querySelector(
      ".modal-time-alteration"
    );
    const modalOvertimeRecord = document.querySelector(
      ".modal-overtime-application"
    );
    const modalUndertimeRecord = document.querySelector(
      ".modal-undertime-application"
    );
    const modalExpenseRecord = document.querySelector(
      ".modal-expense-application"
    );

    const modalAddHoliday = document.querySelector(".modal-add-holiday");
    const modalEditHoliday = document.querySelector(".modal-edit-holiday");

    const modalFileLeave = document.querySelector(".modal-file-leave");
    const modalCreatePayroll = document.querySelector(".modal-create-payroll");
    const modalMassPayroll = document.querySelector(".modal-mass-payroll");
    const modalPayrollMemo = document.querySelector(".modal-payrollmemo");
    const modalAddEmployee = document.querySelector(".modal-add-employee");
    const modalAddEmployeeHeader = document.querySelector(
      "#add-employee-header"
    );
    const modalAddEmployeeBtn = document.querySelector("#btnModalAddEmployee");
    const modalUpdateEmployeeBtn = document.querySelector(
      "#btnModalUpdateEmployee"
    );
    const selectDept = document.querySelector("#drpDepartment");
    const selectDeptUpdate = document.querySelector("#drpDepartmentUpdate");
    // const modalUpdateEmployee = document.querySelector(
    //   ".modal-update-employee"
    // );
    const modalViewEmployee = document.querySelector(".modal-view-employee");
    const modalAddEmpTask = document.querySelector(".modal-add-inprogresstask");
    const modalViewPayslip = document.querySelector(".modal-view-payslip");
    const modalViewMemo = document.querySelector(".modal-viewpayrollmemo");

    // get open modal button
    const modalBtn = btn_id;

    if (modalBtn === "#btnTimeAlteration") {
      modalTimeAlteration.style.display = "flex";
    } else if (modalBtn === "#btnOvertimeApplication") {
      modalOvertimeRecord.style.display = "flex";
    } else if (modalBtn === "#btnUndertimeApplication") {
      modalUndertimeRecord.style.display = "flex";
    } else if (modalBtn === "#btnFileLeave") {
      modalFileLeave.style.display = "flex";
    } else if (modalBtn === "#btnAddExpense") {
      modalExpenseRecord.style.display = "flex";
    } else if (modalBtn === "#btnCreatePayroll") {
      modalCreatePayroll.style.display = "flex";
    } else if (modalBtn === "#btnMassGeneratePayroll") {
      modalMassPayroll.style.display = "flex";
    } else if (modalBtn === "#btnAddEmployee") {
      modalAddEmployee.style.display = "flex";
      modalAddEmployeeHeader.innerHTML = "Add Employment Information";
      modalAddEmployeeBtn.classList.remove("hidden");
      modalUpdateEmployeeBtn.classList.add("hidden");
      selectDept.classList.remove("hidden");
      selectDeptUpdate.classList.add("hidden");
    } else if (modalBtn === "#btnUpdateEmployee") {
      modalAddEmployee.style.display = "flex";
      modalAddEmployeeHeader.innerHTML = "Update Employment Information";
      modalAddEmployeeBtn.classList.add("hidden");
      modalUpdateEmployeeBtn.classList.remove("hidden");
      selectDept.classList.add("hidden");
      selectDeptUpdate.classList.remove("hidden");
      // defaultTabUpdate("primary-info-update");
    } else if (modalBtn === "#btnViewEmployee") {
      modalViewEmployee.style.display = "flex";
      hideBasicInfo("view-primary-info");
    } else if (modalBtn === "#btnAddEmpTask") {
      modalAddEmpTask.style.display = "flex";
    } else if (modalBtn === "#btnViewPayslip") {
      modalViewPayslip.style.display = "flex";
    } else if (modalBtn === "#btnAddHoliday") {
      modalAddHoliday.style.display = "flex";
    } else if (modalBtn === "#btnEditHoliday") {
      modalEditHoliday.style.display = "flex";
    } else if (modalBtn === "#btnAddPayrollMemo") {
      modalPayrollMemo.style.display = "flex";
    } else if (modalBtn === "#btnViewMemoNotice") {
      modalViewMemo.style.display = "flex";
    }

    //set tab back to primary-info
    defaultTab("primary-info");
    scrollTopModal();
  } catch (e) {
    console.log(e);
  }
}

function closeModal(btn_id) {
  try {
    // List modal
    const modalTimeAlteration = document.querySelector(
      ".modal-time-alteration"
    );
    const modalOvertimeRecord = document.querySelector(
      ".modal-overtime-application"
    );
    const modalUndertimeRecord = document.querySelector(
      ".modal-undertime-application"
    );
    const modalExpenseRecord = document.querySelector(
      ".modal-expense-application"
    );
    const modalAddHoliday = document.querySelector(".modal-add-holiday");
    const modalEditHoliday = document.querySelector(".modal-edit-holiday");
    const modalFileLeave = document.querySelector(".modal-file-leave");
    const modalCreatePayroll = document.querySelector(".modal-create-payroll");
    const modalMassPayroll = document.querySelector(".modal-mass-payroll");
    const modalAddEmployee = document.querySelector(".modal-add-employee");
    // const modalUpdateEmployee = document.querySelector(
    //   ".modal-update-employee"
    // );
    const modalViewEmployee = document.querySelector(".modal-view-employee");
    const modalAddEmpTask = document.querySelector(".modal-add-inprogresstask");
    const modalViewPayslip = document.querySelector(".modal-view-payslip");
    const modalPayrollMemo = document.querySelector(".modal-payrollmemo");
    const modalViewMemo = document.querySelector(".modal-viewpayrollmemo");

    // get open modal button
    const modalBtn = btn_id;

    if (modalBtn === "#btnCloseTimeAlteration") {
      modalTimeAlteration.style.display = "none";
    } else if (modalBtn === "#btnCloseOvertimeApplication") {
      modalOvertimeRecord.style.display = "none";
    } else if (modalBtn === "#btnCloseUndertimeApplication") {
      modalUndertimeRecord.style.display = "none";
    } else if (modalBtn === "#btnCloseFileLeave") {
      modalFileLeave.style.display = "none";
    } else if (modalBtn === "#btnCloseAddExpense") {
      modalExpenseRecord.style.display = "none";
    } else if (modalBtn === "#btnCloseCreatePayroll") {
      modalCreatePayroll.style.display = "none";
      reset(`payroll`);
    } else if (modalBtn === "#btnCloseMassPayroll") {
      modalMassPayroll.style.display = "none";
      reset(`payroll`);
    } else if (modalBtn === "#btnCloseAddEmployee") {
      modalAddEmployee.style.display = "none";
    } else if (modalBtn === "#btnCloseAddHoliday") {
      modalAddHoliday.style.display = "none";
    } else if (modalBtn === "#btnCloseEditHoliday") {
      modalEditHoliday.style.display = "none";
    }
    //  else if (modalBtn === "#btnCloseUpdateEmployee") {
    //   modalUpdateEmployee.style.display = "none";
    // }
    else if (modalBtn === "#btnCloseViewEmployee") {
      modalViewEmployee.style.display = "none";
    } else if (modalBtn === "#btnCloseAddEmpTask") {
      modalAddEmpTask.style.display = "none";
      resetAddEmpProj();
    } else if (modalBtn === "#btnCloseViewPayslip") {
      modalViewPayslip.style.display = "none";
    } else if (modalBtn === "#btnClosePayrollMemo") {
      modalPayrollMemo.style.display = "none";
    } else if (modalBtn === "#btnCloseViewMemo") {
      modalViewMemo.style.display = "none";
    }

    //set tab back to primary-info
    defaultTab("primary-info");
    // defaultTabUpdate("primary-info-update");
    defaultTabViewIE("view-primary-info");
  } catch (e) {
    console.log(e);
  }
}

function openTabHrInfo() {
  try {
    const modalAddEmployee = document.querySelector(".modal-add-employee");
    const modalAddEmployeeBtn = document.querySelector("#btnModalAddEmployee");
    const modalUpdateEmployeeBtn = document.querySelector(
      "#btnModalUpdateEmployee"
    );

    modalAddEmployee.style.display = "flex";
    modalAddEmployeeBtn.classList.remove("hidden");
    modalUpdateEmployeeBtn.classList.add("hidden");

    const x = document.getElementsByClassName("tabNameHrModal");
    for (i = 0; i < x.length; i++) {
      x[i].style.display = "none";
    }
    document.getElementById("hr-info").style.display = "flex";

    $(".modal-tab-list li.current-tab").removeClass("current-tab");
    $(".modal-tab-list li:nth-child(2)").addClass("current-tab");

    scrollTopModal();
  } catch (e) {
    console.log(e);
  }
}

function openTabHrInfoUpdate() {
  try {
    const modalAddEmployee = document.querySelector(".modal-add-employee");
    const modalAddEmployeeHeader = document.querySelector(
      "#add-employee-header"
    );
    const modalAddEmployeeBtn = document.querySelector("#btnModalAddEmployee");
    const modalUpdateEmployeeBtn = document.querySelector(
      "#btnModalUpdateEmployee"
    );
    const selectDept = document.querySelector("#drpDepartment");
    const selectDeptUpdate = document.querySelector("#drpDepartmentUpdate");

    modalAddEmployee.style.display = "flex";
    modalAddEmployeeHeader.innerHTML = "Update Employment Information";
    modalAddEmployeeBtn.classList.add("hidden");
    modalUpdateEmployeeBtn.classList.remove("hidden");
    selectDept.classList.add("hidden");
    selectDeptUpdate.classList.remove("hidden");

    const x = document.getElementsByClassName("tabNameHrModal");
    for (i = 0; i < x.length; i++) {
      x[i].style.display = "none";
    }
    document.getElementById("hr-info").style.display = "flex";

    $(".modal-tab-list li.current-tab").removeClass("current-tab");
    $(".modal-tab-list li:nth-child(2)").addClass("current-tab");

    scrollTopModal();
  } catch (e) {
    console.log(e);
  }
}

function reset(frm_id) {
  try {
    document.getElementById(frm_id).reset();
  } catch (e) {
    console.log(e);
  }
}

function openModalWithPay() {
  try {
    const modalWithPay = document.querySelector(".modal-with-pay");
    modalWithPay.style.display = "flex";
  } catch (e) {
    console.log(e);
  }
}

function closeModalWithPay() {
  try {
    const modalWithPay = document.querySelector(".modal-with-pay");
    modalWithPay.style.display = "none";
  } catch (e) {
    console.log(e);
  }
}

function openModalConfirmation() {
  try {
    const modalConfirmation = document.querySelector(".modal-confirmation");
    modalConfirmation.style.display = "flex";
  } catch (e) {
    console.log(e);
  }
}

function closeModalConfirmation() {
  try {
    const modalConfirmation = document.querySelector(".modal-confirmation");
    modalConfirmation.style.display = "none";
  } catch (e) {
    console.log(e);
  }
}

function loadModal() {
  try {
    document.querySelector(".modal").innerHTML = "confirmation.html";
    openModalConfirmation();
  } catch (e) {
    console.log(e);
  }
}

function openModalSetLeave() {
  try {
    const modal = document.querySelector(".modal-set-leave");
    modal.style.display = "flex";
  } catch (e) {
    console.log(e);
  }
}

function closeModalSetLeave() {
  try {
    const modal = document.querySelector(".modal-set-leave");
    modal.style.display = "none";
  } catch (e) {
    console.log(e);
  }
}

function openTab(tabName) {
  try {
    var x = document.getElementsByClassName("tabNameHrModal");
    for (i = 0; i < x.length; i++) {
      x[i].style.display = "none";
    }
    document.getElementById(tabName).style.display = "flex";

    $(".tab-list").on("click", "li", function() {
      $(".tab-list li.current-tab").removeClass("current-tab");
      $(this).addClass("current-tab");
    });
  } catch (e) {
    console.log(e);
  }
}

function openViewEmpInfoTab(tabName) {
  try {
    var x = document.getElementsByClassName("tabViewIEList");
    for (i = 0; i < x.length; i++) {
      x[i].style.display = "none";
    }
    document.getElementById(tabName).style.display = "flex";

    hideBasicInfo(tabName);

    $(".tab-list").on("click", "li", function() {
      $(".tab-list li.current-tab").removeClass("current-tab");
      $(this).addClass("current-tab");
    });
  } catch (e) {
    console.log(e);
  }
}

function hideBasicInfo(tabName) {
  try {
    const basicInfo = document.querySelector(
      ".summary-employee-info-container"
    );
    if (tabName === "view-primary-info") {
      basicInfo.classList.add("hidden");
    } else {
      basicInfo.classList.remove("hidden");
    }
  } catch (e) {
    console.log(e);
  }
}

function redirectModalToUpdateEmployee() {
  try {
    const modalViewEmployee = document.querySelector(".modal-view-employee");
    modalViewEmployee.style.display = "none";
    openModal(`#btnUpdateEmployee`);
  } catch (e) {
    console.log(e);
  }
}

function defaultTab(tabName) {
  try {
    var x = document.getElementsByClassName("tabNameHrModal");
    for (i = 0; i < x.length; i++) {
      x[i].style.display = "none";
    }
    document.getElementById(tabName).style.display = "flex";

    $(".modal-tab-list li.current-tab").removeClass("current-tab");
    $(".modal-tab-list li:first-child").addClass("current-tab");
  } catch (e) {
    console.log(e);
  }
}

function defaultTabUpdate(tabName) {
  try {
    var x = document.getElementsByClassName("tabNameHrModal");
    for (i = 0; i < x.length; i++) {
      x[i].style.display = "none";
    }
    document.getElementById(tabName).style.display = "flex";

    $(".modal-tab-list li.current-tab").removeClass("current-tab");
    $(".modal-tab-list li:nth-child(1)").addClass("current-tab");
  } catch (e) {
    console.log(e);
  }
}

function defaultTabViewIE(tabName) {
  try {
    var x = document.getElementsByClassName("tabViewIEList");
    for (i = 0; i < x.length; i++) {
      x[i].style.display = "none";
    }
    document.getElementById(tabName).style.display = "flex";

    $(".modal-tab-list li.current-tab").removeClass("current-tab");
    $(".modal-tab-list li:first-child").addClass("current-tab");
  } catch (e) {
    console.log(e);
  }
}

function toggleEndo() {
  try {
    const selectEmploymentStatus = $("#selectEmploymentStatus")
      .find(":selected")
      .text();
    const lblLeave = document.querySelector("#txtLeaveLabel");
    const lblSickLeave = document.querySelector("#txtSickLeaveLabel");
    const txtSickLeave = document.querySelector("#txtSickLeave");
    const lblVELeave = document.querySelector("#txtVELeaveLabel");
    const txtVELeave = document.querySelector("#txtVELeave");
    const dtTermDateLabel = document.querySelector("#dtTermDateLabel");
    const dtTermDate = document.querySelector("#dtTermDate");

    lblLeave.classList.add("disabled");
    lblSickLeave.classList.add("disabled");
    txtSickLeave.classList.add("disabled");
    lblVELeave.classList.add("disabled");
    txtVELeave.classList.add("disabled");
    dtTermDateLabel.classList.add("disabled");
    dtTermDate.classList.add("disabled");

    if (selectEmploymentStatus === "Regular") {
      lblLeave.classList.remove("disabled");
      lblSickLeave.classList.remove("disabled");
      txtSickLeave.classList.remove("disabled");
      lblVELeave.classList.remove("disabled");
      txtVELeave.classList.remove("disabled");
    } else if (selectEmploymentStatus === "Terminated") {
      dtTermDateLabel.classList.remove("disabled");
      dtTermDate.classList.remove("disabled");
    }
  } catch (e) {
    console.log(e);
  }
}
function toggleEndo2() {
  try {
    const selectEmploymentStatus = $("#selectEmploymentStatus2")
      .find(":selected")
      .text();
    const lblLeave = document.querySelector("#txtLeaveLabel2");
    const lblSickLeave = document.querySelector("#txtSickLeaveLabel2");
    const txtSickLeave = document.querySelector("#txtSickLeave2");
    const lblVELeave = document.querySelector("#txtVELeaveLabel2");
    const txtVELeave = document.querySelector("#txtVELeave2");
    const dtTermDateLabel = document.querySelector("#dtTermDateLabel2");
    const dtTermDate = document.querySelector("#dtTermDate2");

    lblLeave.classList.add("disabled");
    lblSickLeave.classList.add("disabled");
    txtSickLeave.classList.add("disabled");
    lblVELeave.classList.add("disabled");
    txtVELeave.classList.add("disabled");
    dtTermDateLabel.classList.add("disabled");
    dtTermDate.classList.add("disabled");

    if (selectEmploymentStatus === "Regular") {
      lblLeave.classList.remove("disabled");
      lblSickLeave.classList.remove("disabled");
      txtSickLeave.classList.remove("disabled");
      lblVELeave.classList.remove("disabled");
      txtVELeave.classList.remove("disabled");
    } else if (selectEmploymentStatus === "End of Contract") {
      dtTermDateLabel.classList.remove("disabled");
      dtTermDate.classList.remove("disabled");
    }
  } catch (e) {
    console.log(e);
  }
}

function enableReq() {
  try {
    const req1 = document.querySelector("#req1");
    const req2 = document.querySelector("#req2");
    const req3 = document.querySelector("#req3");
    req1.classList.remove("disabled");
    req2.classList.remove("disabled");
    req3.classList.remove("disabled");
  } catch (e) {
    console.log(e);
  }
}

function disableReq() {
  try {
    const req1 = document.querySelector("#req1");
    const req2 = document.querySelector("#req2");
    const req3 = document.querySelector("#req3");
    req1.classList.add("disabled");
    req2.classList.add("disabled");
    req3.classList.add("disabled");
  } catch (e) {
    console.log(e);
  }
}

function enableReq2() {
  try {
    const req1 = document.querySelector("#req1_2");
    const req2 = document.querySelector("#req2_2");
    const req3 = document.querySelector("#req3_2");
    req1.classList.remove("disabled");
    req2.classList.remove("disabled");
    req3.classList.remove("disabled");
  } catch (e) {
    console.log(e);
  }
}

function disableReq2() {
  try {
    const req1 = document.querySelector("#req1_2");
    const req2 = document.querySelector("#req2_2");
    const req3 = document.querySelector("#req3_2");
    req1.classList.add("disabled");
    req2.classList.add("disabled");
    req3.classList.add("disabled");
  } catch (e) {
    console.log(e);
  }
}

function enableAccessRole() {
  try {
    const selectRoleLabel = document.querySelector("#selectRoleLabel");
    const selectRole = document.querySelector("#selectRole");
    const txtPasswordLabel = document.querySelector("#txtPasswordLabel");
    const txtPassword = document.querySelector("#txtPassword");
    selectRoleLabel.classList.remove("disabled");
    selectRole.classList.remove("disabled");
    txtPasswordLabel.classList.remove("disabled");
    txtPassword.classList.remove("disabled");
  } catch (e) {
    console.log(e);
  }
}

function disableAccessRole() {
  try {
    const selectRoleLabel = document.querySelector("#selectRoleLabel");
    const selectRole = document.querySelector("#selectRole");
    const txtPasswordLabel = document.querySelector("#txtPasswordLabel");
    const txtPassword = document.querySelector("#txtPassword");
    selectRoleLabel.classList.add("disabled");
    selectRole.classList.add("disabled");
    txtPasswordLabel.classList.add("disabled");
    txtPassword.classList.add("disabled");
  } catch (e) {
    console.log(e);
  }
}

function enableAccessRole2() {
  try {
    const selectRoleLabel = document.querySelector("#selectRoleLabel2");
    const selectRole = document.querySelector("#selectRole2");
    const txtPasswordLabel = document.querySelector("#txtPasswordLabel2");
    const txtPassword = document.querySelector("#txtPassword2");
    selectRoleLabel.classList.remove("disabled");
    selectRole.classList.remove("disabled");
    txtPasswordLabel.classList.remove("disabled");
    txtPassword.classList.remove("disabled");
  } catch (e) {
    console.log(e);
  }
}

function disableAccessRole2() {
  try {
    const selectRoleLabel = document.querySelector("#selectRoleLabel2");
    const selectRole = document.querySelector("#selectRole2");
    const txtPasswordLabel = document.querySelector("#txtPasswordLabel2");
    const txtPassword = document.querySelector("#txtPassword2");
    selectRoleLabel.classList.add("disabled");
    selectRole.classList.add("disabled");
    txtPasswordLabel.classList.add("disabled");
    txtPassword.classList.add("disabled");
  } catch (e) {
    console.log(e);
  }
}

function removeHidden() {
  try {
    const inputProjectTask = document.querySelector(
      "input.txtTaskField.hidden"
    );
    inputProjectTask.classList.remove("hidden");

    const lastTaskField = document.querySelector("#txtTaskField_15");
    const btnProjectTask = document.querySelector(".btnAddTask");

    if (lastTaskField.classList.contains("hidden") == false) {
      btnProjectTask.classList.add("hidden");
    }
  } catch (e) {
    console.log(e);
  }
}

function resetAddEmpProj() {
  try {
    const resetEmpProj = document.querySelectorAll(
      ".txtTaskName-container .txtTaskField"
    );

    resetEmpProj.forEach(element => {
      element.classList.add("hidden");
    });

    const firstTaskField = document.querySelector("#txtTaskField_1");
    firstTaskField.classList.remove("hidden");

    const lastTaskField = document.querySelector("#txtTaskField_15");
    const btnProjectTask = document.querySelector(".btnAddTask");

    if (lastTaskField.classList.contains("hidden") == true) {
      btnProjectTask.classList.remove("hidden");
    }
  } catch (e) {
    console.log(e);
  }
}

function showEM() {
  try {
    const card1 = document.querySelector(
      "#employee-masterlist .container .card-1"
    );
    const card2 = document.querySelector(
      "#employee-masterlist .container .card-2"
    );

    card2.classList.add("hidden");
    card1.classList.remove("hidden");
  } catch (e) {
    console.log(e);
  }
}

function showPayrollIM() {
  try {
    const card1 = document.querySelector(
      "#payroll-add-employee .container .card-1"
    );
    const card2 = document.querySelector(
      "#payroll-add-employee .container .card-2"
    );

    card2.classList.remove("hidden");
    card1.classList.add("hidden");
  } catch (e) {
    console.log(e);
  }
}
function showPayrollEM() {
  try {
    const card1 = document.querySelector(
      "#payroll-add-employee .container .card-1"
    );
    const card2 = document.querySelector(
      "#payroll-add-employee .container .card-2"
    );

    card2.classList.add("hidden");
    card1.classList.remove("hidden");
  } catch (e) {
    console.log(e);
  }
}

function showIM() {
  try {
    const card1 = document.querySelector(
      "#employee-masterlist .container .card-1"
    );
    const card2 = document.querySelector(
      "#employee-masterlist .container .card-2"
    );

    card2.classList.remove("hidden");
    card1.classList.add("hidden");
  } catch (e) {
    console.log(e);
  }
}

function showER() {
  try {
    const card1 = document.querySelector("#expense-records .container .card-1");
    const card2 = document.querySelector("#expense-records .container .card-2");

    card2.classList.add("hidden");
    card1.classList.remove("hidden");
  } catch (e) {
    console.log(e);
  }
}

function showPE() {
  try {
    const card1 = document.querySelector("#expense-records .container .card-1");
    const card2 = document.querySelector("#expense-records .container .card-2");

    card2.classList.remove("hidden");
    card1.classList.add("hidden");
  } catch (e) {
    console.log(e);
  }
}

function showTR() {
  try {
    const card1 = document.querySelector("#time-records .container .card-1");
    const card2 = document.querySelector("#time-records .container .card-2");

    card2.classList.add("hidden");
    card1.classList.remove("hidden");
  } catch (e) {
    console.log(e);
  }
}
function showPTA() {
  try {
    const card1 = document.querySelector("#time-records .container .card-1");
    const card2 = document.querySelector("#time-records .container .card-2");

    card2.classList.remove("hidden");
    card1.classList.add("hidden");
  } catch (e) {
    console.log(e);
  }
}

function showLR() {
  try {
    const card1 = document.querySelector("#leaverecords .container .card-1");
    const card2 = document.querySelector("#leaverecords .container .card-2");

    card2.classList.add("hidden");
    card1.classList.remove("hidden");
  } catch (e) {
    console.log(e);
  }
}

function showPLA() {
  try {
    const card1 = document.querySelector("#leaverecords .container .card-1");
    const card2 = document.querySelector("#leaverecords .container .card-2");

    card2.classList.remove("hidden");
    card1.classList.add("hidden");
  } catch (e) {
    console.log(e);
  }
}

function showOR() {
  try {
    const card1 = document.querySelector(
      "#overtime-records .container .card-1"
    );
    const card2 = document.querySelector(
      "#overtime-records .container .card-2"
    );

    card2.classList.add("hidden");
    card1.classList.remove("hidden");
  } catch (e) {
    console.log(e);
  }
}

function showPOA() {
  try {
    const card1 = document.querySelector(
      "#overtime-records .container .card-1"
    );
    const card2 = document.querySelector(
      "#overtime-records .container .card-2"
    );

    card2.classList.remove("hidden");
    card1.classList.add("hidden");
  } catch (e) {
    console.log(e);
  }
}

function showUR() {
  try {
    const card1 = document.querySelector(
      "#undertime-records .container .card-1"
    );
    const card2 = document.querySelector(
      "#undertime-records .container .card-2"
    );

    card2.classList.add("hidden");
    card1.classList.remove("hidden");
  } catch (e) {
    console.log(e);
  }
}

function showPUA() {
  try {
    const card1 = document.querySelector(
      "#undertime-records .container .card-1"
    );
    const card2 = document.querySelector(
      "#undertime-records .container .card-2"
    );

    card2.classList.remove("hidden");
    card1.classList.add("hidden");
  } catch (e) {
    console.log(e);
  }
}

function showIP() {
  try {
    const card1 = document.querySelector("#project-task .container .card-1");
    const card2 = document.querySelector("#project-task .container .card-2");

    card2.classList.add("hidden");
    card1.classList.remove("hidden");
  } catch (e) {
    console.log(e);
  }
}

function showCT() {
  try {
    const card1 = document.querySelector("#project-task .container .card-1");
    const card2 = document.querySelector("#project-task .container .card-2");

    card2.classList.remove("hidden");
    card1.classList.add("hidden");
  } catch (e) {
    console.log(e);
  }
}
function showP() {
  try {
    const card1 = document.querySelector("#payroll .container .card-1");
    const card2 = document.querySelector("#payroll .container .card-2");

    card2.classList.add("hidden");
    card1.classList.remove("hidden");
  } catch (e) {
    console.log(e);
  }
}

function showPPA() {
  try {
    const card1 = document.querySelector("#payroll .container .card-1");
    const card2 = document.querySelector("#payroll .container .card-2");

    card2.classList.remove("hidden");
    card1.classList.add("hidden");
  } catch (e) {
    console.log(e);
  }
}

function openModalLeaveRemarks() {
  try {
    const modalRemarks = document.querySelector(".modal-leave-remarks");
    modalRemarks.style.display = "flex";
  } catch (e) {
    console.log(e);
  }
}

function closeModalLeaveRemarks() {
  try {
    const modalRemarks = document.querySelector(".modal-leave-remarks");
    modalRemarks.style.display = "none";
  } catch (e) {
    console.log(e);
  }
}

function openModalOvertimeRemarks() {
  try {
    const modalRemarks = document.querySelector(".modal-overtime-remarks");
    modalRemarks.style.display = "flex";
  } catch (e) {
    console.log(e);
  }
}

function closeModalOvertimeRemarks() {
  try {
    const modalRemarks = document.querySelector(".modal-overtime-remarks");
    modalRemarks.style.display = "none";
  } catch (e) {
    console.log(e);
  }
}
function openModalTimeAlterationRemarks() {
  try {
    const modalRemarks = document.querySelector(
      ".modal-timealteration-remarks"
    );
    modalRemarks.style.display = "flex";
  } catch (e) {
    console.log(e);
  }
}

function closeModalTimeAlterationRemarks() {
  try {
    const modalRemarks = document.querySelector(
      ".modal-timealteration-remarks"
    );
    modalRemarks.style.display = "none";
  } catch (e) {
    console.log(e);
  }
}
function openModalExpenseRemarks() {
  try {
    const modalRemarks = document.querySelector(".modal-expense-remarks");
    modalRemarks.style.display = "flex";
  } catch (e) {
    console.log(e);
  }
}

function closeModalExpenseRemarks() {
  try {
    const modalRemarks = document.querySelector(".modal-expense-remarks");
    modalRemarks.style.display = "none";
  } catch (e) {
    console.log(e);
  }
}
function openModalUndertimeRemarks() {
  try {
    const modalRemarks = document.querySelector(".modal-undertime-remarks");
    modalRemarks.style.display = "flex";
  } catch (e) {
    console.log(e);
  }
}

function closeModalUndertimeRemarks() {
  try {
    const modalRemarks = document.querySelector(".modal-undertime-remarks");
    modalRemarks.style.display = "none";
  } catch (e) {
    console.log(e);
  }
}

function openModalViewDTR() {
  try {
    const modal = document.querySelector(".modal-view-dtr");
    modal.style.display = "flex";
  } catch (e) {
    console.log(e);
  }
}

function closeModalViewDTR() {
  try {
    const modal = document.querySelector(".modal-view-dtr");
    modal.style.display = "none";
  } catch (e) {
    console.log(e);
  }
}

function toggleDropDown() {
  try {
    const drpdown = document.querySelector(".btn-dropdown");
    drpdown.classList.toggle("hidden");
  } catch (e) {
    console.log(e);
  }
}

function toggleEmployeeSelection() {
  try {
    const div1 = document.querySelector(".div1");
    const div2 = document.querySelector(".div2");
    const div3 = document.querySelector(".div3");
    let radioValue = $("input[name='employeeSelection']:checked").val();

    if (radioValue === "All") {
      div1.classList.remove("hidden");
      div2.classList.add("hidden");
      div3.classList.add("hidden");
    } else if (radioValue === "Custom") {
      div1.classList.add("hidden");
      div2.classList.remove("hidden");
      div3.classList.remove("hidden");
    }
  } catch (e) {
    console.log(e);
  }
}

function toggleEmployeeSelection3() {
  try {
    const div7 = document.querySelector(".div7");
    const div8 = document.querySelector(".div8");
    const div9 = document.querySelector(".div9");
    let radioValue = $("input[name='employeeSelection3']:checked").val();

    if (radioValue === "All") {
      div7.classList.remove("hidden");
      div8.classList.add("hidden");
      div9.classList.add("hidden");
    } else if (radioValue === "Custom") {
      div7.classList.add("hidden");
      div8.classList.remove("hidden");
      div9.classList.remove("hidden");
    }
  } catch (e) {
    console.log(e);
  }
}

function checkSearchInput() {
  try {
    const txtSearch = document.querySelector(".txtSearch").value;
    const btnClearSearch = document.querySelector(".btn-clear-search");

    if (txtSearch === "") {
      btnClearSearch.classList.add("hidden");
      hideDataList();
    } else {
      btnClearSearch.classList.remove("hidden");
      showDataList();
      scrollTopDataList();
    }
  } catch (e) {
    console.log(e);
  }
}

function checkSearchInput2() {
  try {
    const txtSearch = document.querySelector(".txtSearch2").value;
    const btnClearSearch = document.querySelector(".btn-clear-search2");

    if (txtSearch === "") {
      btnClearSearch.classList.add("hidden");
    } else {
      btnClearSearch.classList.remove("hidden");
    }
  } catch (e) {
    console.log(e);
  }
}

function clearSearch(formID) {
  try {
    const btnClearSearch = document.querySelector(".btn-clear-search");

    btnClearSearch.classList.add("hidden");
    reset(formID);
  } catch (e) {
    console.log(e);
  }
}

function clearSearch2(formID) {
  try {
    const btnClearSearch = document.querySelector(".btn-clear-search2");

    btnClearSearch.classList.add("hidden");
    reset(formID);
  } catch (e) {
    console.log(e);
  }
}

function toggleSearchFilter() {
  try {
    const container = document.querySelector(".search-filter-container");
    const showSearchOptions = document.querySelector(".showSearchOptions svg");

    container.classList.toggle("hidden");
    showSearchOptions.classList.toggle("rotate");
  } catch (e) {
    console.log(e);
  }
}

function toggleSearchFilter2() {
  try {
    const container = document.querySelector(".sr-f-2");
    const showSearchOptions = document.querySelector(".showSearchOptions2");

    container.classList.toggle("hidden");
    showSearchOptions.classList.toggle("rotate");
  } catch (e) {
    console.log(e);
  }
}

function toggleSearchRecFilter() {
  try {
    const container = document.querySelector(".searchRec-filter-container");
    const showSearchRecOptions = document.querySelector(
      ".showSearchRecOptions"
    );

    container.classList.toggle("hidden");
    showSearchRecOptions.classList.toggle("rotate");
  } catch (e) {
    console.log(e);
  }
}

function toggleSearchRecFilter3() {
  try {
    const container3 = document.querySelector(".sr-f-3");
    const showSearchRecOptions3 = document.querySelector(".sr-o-3");

    container3.classList.toggle("hidden");
    showSearchRecOptions3.classList.toggle("rotate");
  } catch (e) {
    console.log(e);
  }
}

function showDataList() {
  try {
    const datalist = document.querySelector(".datalist-data");
    scrollTopDataList();
    datalist.classList.remove("hidden");
  } catch (e) {
    console.log(e);
  }
}

function hideDataList() {
  try {
    const datalist = document.querySelector(".datalist-data");

    datalist.classList.add("hidden");
  } catch (e) {
    console.log(e);
  }
}
