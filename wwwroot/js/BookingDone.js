document.addEventListener("DOMContentLoaded", function () {
    // Get the booking ID from the HTML element or wherever it's stored
    const bookingId = parseInt(document.getElementById('bookingId').textContent);

    // Make an AJAX request to fetch booking data
    fetch(`/Booking/GetBookingDetails/${bookingId}`)
        .then(response => response.json())
        .then(data => {
            // Populate the fields in the HTML with the retrieved data
            document.getElementById('bookingId').textContent = data.bookingId;
            document.getElementById('bookingDate').textContent = data.bookingDate;
            document.getElementById('name').textContent = data.name;
            // Include other properties as needed
        })
        .catch(error => {
            console.error('Error fetching booking data:', error);
        });
});
