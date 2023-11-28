$(document).ready(function () {
    $("#darkModeButton").click(function () {
        toggleDarkMode();
    });

    // Function to toggle dark mode
    function toggleDarkMode() {
        if (isDarkModeEnabled()) {
            // If dark mode is enabled, disable it
            disableDarkMode();
            window.location.reload(true);
        } else {
            // If dark mode is disabled, enable it
            enableDarkMode();
            window.location.reload(true);
        }
    }

    // Function to check if dark mode is enabled
    function isDarkModeEnabled() {
        return document.cookie.includes("DarkMode=true");
    }

    // Function to enable dark mode
    function enableDarkMode() {
        var cookie = new CookieHeaderValue("DarkMode", "true");
        cookie.Expires = DateTimeOffset.Now.AddDays(30);
        cookie.Domain = window.location.hostname;
        cookie.Path = "/";

        document.cookie = cookie.toString();
        applyDarkModeStyles();
    }

    // Function to disable dark mode
    function disableDarkMode() {
        // Set a cookie with an expiration date in the past to remove the existing cookie
        document.cookie = "DarkMode=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
        // Add logic to remove dark mode styles from your page
    }

    // Function to apply dark mode styles
    function applyDarkModeStyles() {
        // Add logic to apply dark mode styles to your page
    }
});