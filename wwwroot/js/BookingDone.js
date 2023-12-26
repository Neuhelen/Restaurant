document.addEventListener("DOMContentLoaded", function () {
    // Get the booking ID from the HTML element or wherever it's stored
    const bookingId = parseInt(document.getElementById('bookingId').textContent);

    // Get the booking details from the API
fetch(`/api/booking/${bookingId}`)
        .then(response => response.json())
        .then(booking => {
            // Update the HTML elements with the booking details
            document.getElementById('bookingId').textContent = booking.id;
            document.getElementById('bookingDate').textContent = booking.bookingDate;
            document.getElementById('bookingTime').textContent = booking.bookingTime;
            document.getElementById('bookingDuration').textContent = booking.bookingDuration;
            document.getElementById('bookingPrice').textContent = booking.bookingPrice;
            document.getElementById('bookingStatus').textContent = booking.bookingStatus;
            document.getElementById('bookingNotes').textContent = booking.bookingNotes;
            document.getElementById('bookingCustomer').textContent = booking.customerName;
            document.getElementById('bookingEmployee').textContent = booking.employeeName;
            document.getElementById('bookingService').textContent = booking.serviceName;
        })
        .catch(error => console.error(error));