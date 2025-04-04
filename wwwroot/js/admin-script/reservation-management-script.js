function closeReservationForm() {
  const container = document.getElementById('reservationFormContainer');
  // Thêm lớp animation
  container.style.animation = 'fadeOut 0.3s ease-in-out forwards';

  // Chờ animation kết thúc rồi mới ẩn
  setTimeout(() => {
    container.style.display = 'none';
    container.style.animation = '';

  }, 300);
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