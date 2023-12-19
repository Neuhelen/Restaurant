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
            calendarDays.innerHTML += '<div></div>';
        }

        // Add days of the current month
        for (let i = 1; i <= lastDay.getDate(); i++) {
            calendarDays.innerHTML += `<div>${i}</div>`;
        }
    }

    prevButton.addEventListener('click', function () {
        currentDate.setMonth(currentDate.getMonth() - 1);
        renderCalendar();
    });

    nextButton.addEventListener('click', function () {
        currentDate.setMonth(currentDate.getMonth() + 1);
        renderCalendar();
    });

    renderCalendar();
});