document.addEventListener('DOMContentLoaded', function () {
    const prevButton = document.getElementById('prev-month');
    const nextButton = document.getElementById('next-month');
    const monthName = document.getElementById('month-name');
    const calendarDays = document.getElementById('calendar-days');

    let currentDate = new Date();

    function renderCalendar() {
        calendarDays.innerHTML = '';

        // Set month name
        monthName.textContent = currentDate.toLocaleDateString('default', { month: 'long', year: 'numeric' });

        // Get the first day of the month
        let firstDay = new Date(currentDate.getFullYear(), currentDate.getMonth(), 1);
        let lastDay = new Date(currentDate.getFullYear(), currentDate.getMonth() + 1, 0);

        // Add empty days for previous month
        for (let i = 0; i < firstDay.getDay(); i++) {
            calendarDays.innerHTML += '<div class="empty-day"></div>';
        }

        // Add days of the current month
        for (let i = 1; i <= lastDay.getDate(); i++) {
            calendarDays.innerHTML += `<div class="day">${i}</div>`;
        }

        renderOrders(); // Call to update bookings on the calendar
    }

    function renderOrders() {
        fetch(`/Booking/GetBookingCounts?year=${currentDate.getFullYear()}&month=${currentDate.getMonth() + 1}`)
            .then(response => response.json())
            .then(bookingCounts => {
                const days = document.querySelectorAll('#calendar-days .day');
                days.forEach(day => {
                    const dayNumber = parseInt(day.textContent);
                    const bookingCount = bookingCounts.find(count => new Date(count.date).getDate() === dayNumber);
                    if (bookingCount && bookingCount.count > 0) {
                        const ordersText = document.createElement('span');
                        ordersText.textContent = `Orders: ${bookingCount.count}`;
                        ordersText.classList.add('orders-count');
                        day.appendChild(ordersText);
                        day.classList.add('has-bookings');
                    }
                });
            })
            .catch(error => console.error('Error fetching bookings:', error));
    }

    prevButton.addEventListener('click', function () {
        currentDate.setMonth(currentDate.getMonth() - 1);
        renderCalendar();
    });

    nextButton.addEventListener('click', function () {
        currentDate.setMonth(currentDate.getMonth() + 1);
        renderCalendar();
    });

    renderCalendar(); // Initial render
});
