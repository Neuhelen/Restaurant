$(document).ready(function () {
    if (isDarkModeEnabled()) {
        applyDarkModeStyles();
    }

    //If the dark mode button is clicked, toggle between light mode and dark mode. 
    $("#darkModeButton").click(function () {
        toggleDarkMode();
        console.log("Button click.");
    });

    //This function toggles between light mode and dark mode
    function toggleDarkMode() {
        if (isDarkModeEnabled()) {
            //If dark mode is enabled, disable it
            disableDarkMode();
        } else {
            //If dark mode is disabled, enable it
            enableDarkMode();
        }
    }

    function isDarkModeEnabled() {
        return document.cookie.includes("DarkMode=true");
    }

    //This function enables dark mode by creating a Cookie
    function enableDarkMode() {
        document.cookie = "DarkMode=true; expires=" + new Date(Date.now() + 30 * 24 * 60 * 60 * 1000).toUTCString() + "; path=/";

        applyDarkModeStyles();
    }

    //This function disables dark mode by setting the Cookie's expiration date in the past
    function disableDarkMode() {
        document.cookie = "DarkMode=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";

        removeDarkModeStyles();
    }

    //This function applies the dark mode styles
    function applyDarkModeStyles() {
        $('body').addClass('dark-mode');
        $('.grid-container, .alt-grid-container').addClass('dark-mode-container');
    }

    function removeDarkModeStyles() {

        //This part removes the dark mode styles: 
        $('body').removeClass('dark-mode');
        $('.grid-container, .alt-grid-container').removeClass('dark-mode-container');
    }
});
