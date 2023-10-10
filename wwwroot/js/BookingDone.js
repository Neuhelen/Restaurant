
// Fetch data from local storage and display it
document.addEventListener("DOMContentLoaded", function() {
	const bookingDate = localStorage.getItem('bookingDate');
	const name = localStorage.getItem('name');
	const email = localStorage.getItem('email');
	const phoneNumber = localStorage.getItem('phoneNumber');
	const partySize = localStorage.getItem('partySize');
	const additionalNotes = localStorage.getItem('additionalNotes')  || 'N/A';

	// Populate the fields in the HTML
    document.getElementById('bookingDate').textContent = bookingDate;
    document.getElementById('name').textContent = name;
    document.getElementById('email').textContent = email;
    document.getElementById('phoneNumber').textContent = phoneNumber;
    document.getElementById('partySize').textContent = partySize;
    document.getElementById('additionalNotes').textContent = additionalNotes;

});