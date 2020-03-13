// Show Date
function showDate() {
  try {
    let today = new Date(),
      date = today.getDate(),
      month = today.getMonth(),
      year = today.getFullYear();

    // Output Date
    lblDate.innerHTML = `${convertDate(
      month
    )}<span class="label-3"> </span>${addZero(
      date
    )}<span class="label-3">, </span>${year}`;
    lblDate2.innerHTML = `${convertDate(
      month
    )}<span class="label-3"> </span>${addZero(
      date
    )}<span class="label-3">, </span>${year}`;
  } catch {}
}

// Show Time
function showTime() {
  try {
    let today = new Date(),
      hour = today.getHours(),
      min = today.getMinutes();
    // sec = today.getSeconds();

    // Set AM or PM
    const amPm = hour >= 12 ? "PM" : "AM";

    // 12hr Format
    hour = hour % 12 || 12;

    // Output Time
    lblTime.innerHTML = `${addZero(
      hour
    )}<span class="label-0">:</span>${addZero(
      min
    )}<span class="label-0"> </span>${amPm}`;

    setTimeout(showTime, 1000);
  } catch {}
}

// Add Zeros
function addZero(n) {
  try {
    return (parseInt(n, 10) < 10 ? "0" : "") + n;
  } catch {}
}

// Convert month to in words
function convertDate(temp_date) {
  try {
    let months = [
      "January",
      "February",
      "March",
      "April",
      "May",
      "June",
      "July",
      "August",
      "September",
      "October",
      "November",
      "December"
    ];

    return months[Number(temp_date)];
  } catch {}
}

// Run
showDate();
showTime();
