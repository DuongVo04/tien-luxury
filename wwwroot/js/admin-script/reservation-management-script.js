﻿function closeReservationForm() {
  const container = document.getElementById('reservationFormContainer');
  // Thêm lớp animation
  container.style.animation = 'fadeOut 0.3s ease-in-out forwards';

  // Chờ animation kết thúc rồi mới ẩn
  setTimeout(() => {
    container.style.display = 'none';
    container.style.animation = '';

  }, 300);
}

function filterByStatus(status) {
  const rows = document.querySelectorAll('.order-row');

  rows.forEach(row => {
    const statusCell = row.querySelector('.status-text').dataset.status;
    if (status === "all" || statusCell === status) {
      row.style.display = "";
    } else {
      row.style.display = "none";
    }
  });
}

function extractDateOnly(datetimeStr) {

  const datePart = datetimeStr.trim().split(' ')[0];

  if (datePart.includes('/')) {

    const [day, month, year] = datePart.split('/');
    return `${year}-${month.padStart(2, '0')}-${day.padStart(2, '0')}`;
  } else if (datePart.includes('-')) {

    return datePart;
  }

  return '';
}

function filterByDate(dateInput) {
  const rows = document.querySelectorAll('.order-row');

  rows.forEach(row => {
    const datetimeStr = row.querySelector('.date-text').textContent;
    const dateOnly = extractDateOnly(datetimeStr);

    if (!dateInput || dateOnly === dateInput) {
      row.style.display = "";
    } else {
      row.style.display = "none";
    }
  });
}


$(document).ready(function () {

  // Gắn sự kiện click cho nút "Chỉnh Sửa"
  $(document).on('click', '#updateReservationBtn', function (e) {
    e.preventDefault();

    var reservationId = $(this).closest('tr').data('reservation-id');

    $.ajax({
      url: "/Admin/ReservationsManagement/UpdateReservationStatus",
      type: "GET",
      data: { id: reservationId },


      success: function (data) {
        $("#reservationFormContainer").html(data);
        $("#reservationFormContainer").show();
      },
      error: function (xhr, status, error) {
        console.error('Error loading update form:', error);
      }
    });

  });

  // $(document).on('click', '#deleteServiceBtn', function (e) {
  //     e.preventDefault();
  //     var serviceId = $(this).closest('tr').data('service-id');
  //     $.ajax({
  //         url: "/Admin/ServicesManagement/DeleteService",
  //         type: "GET",
  //         data: { id: serviceId },
  //         success: function (data) {
  //             $("#serviceFormContainer").html(data);
  //             $("#serviceFormContainer").show();
  //         },
  //         error: function (xhr, status, error) {
  //             console.error('Error loading update form:', error);
  //         }
  //     });
  // });
});