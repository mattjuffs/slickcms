// executes when HTML-Document is loaded and DOM is ready
$(document).ready(function () {

});

// executes when complete page is fully loaded, including all frames, objects and images
$(window).load(function () {
    SetupScroll();
    SetupDropdowns();
    SetupMobileMenu();
    SetupLightBox();
    SetupResponsiveSlides();
    SetupCKEditor();
    window.LogMessage("Setup complete!");
});

function SetupScroll() {
    $(".scroll").click(function (event) {
        // prevent the default action for the click event
        event.preventDefault();

        // get the full url - like mysitecom/index.htm#home
        var full_url = this.href;

        // split the url by # and get the anchor target name - home in mysitecom/index.htm#home
        var parts = full_url.split("#");
        var trgt = parts[1];

        // get the top offset of the target anchor
        var target_offset = $("#" + trgt).offset();
        var target_top = 0;// use to determine how high to scroll, but in this use we're going to the top

        // retrieve the height of the fixed header
        var headerHeight = $('header').height();

        // hide the mobile menu, in case the scroll link was within it
        if ($('ul.mobileMenu').is(":visible")) {
            $('ul.mobileMenu').hide();
            $('#search').hide();
        }

        // goto that anchor by setting the body scroll top to anchor top
        $('html, body').animate({ scrollTop: target_offset.top - headerHeight }, 500);
    });
}

function SetupDropdowns() {
    var $menulis = $('#projectName, #menu li.root, #search');// root menu items
    var hoverIntent;

    $menulis.hover(function () {
        // close all dropdowns
        $('.dropdown').hide();

        // show this dropdown
        $("ul.dropdown", this).show();        

        // clear any other timeouts
        clearTimeout(hoverIntent);
    }, function () {
        var self = $(this);

        // setup timeout to hide this menu after 300 ms
        hoverIntent = setTimeout(function () {
            $("ul.dropdown", self).hide();
        }, 300);
    });
}

function SetupMobileMenu() {
    $('a.mobileMenu').click(function () {
        $('ul.mobileMenu').toggle();
        $('#search').toggle();
    });
}

function SetupLightBox() {
    $(function () {
        $('.lightbox').lightBox();
    });
}

function SetupResponsiveSlides() {
    $(".rslides").responsiveSlides({
        auto: true,				// Boolean: Animate automatically, true or false
        speed: 500,				// Integer: Speed of the transition, in milliseconds
        timeout: 4000,			// Integer: Time between slide transitions, in milliseconds
        pager: true,			// Boolean: Show pager, true or false
        nav: false,             // Boolean: Show navigation, true or false
        random: false,          // Boolean: Randomize the order of the slides, true or false
        pause: true,           // Boolean: Pause on hover, true or false
        pauseControls: true,   // Boolean: Pause when hovering controls, true or false
        prevText: "Previous",   // String: Text for the "previous" button
        nextText: "Next",       // String: Text for the "next" button
        maxwidth: 960,        	// Integer: Max-width of the slideshow, in pixels
        navContainer: "",       // Selector: Where controls should be appended to, default is after the 'ul'
        manualControls: "",     // Selector: Declare custom pager navigation
        namespace: "rslides",   // String: Change the default namespace used
        before: function () { },   // Function: Before callback
        after: function () { }     // Function: After callback
    });
}

function SetupCKEditor() {
    if (window.CKEDITOR) {
        if ($("#ckeditor").length > 0) {
            CKEDITOR.replace('ckeditor');
        }
    }
}

function LogMessage(msg) {
    try {
        console.log(msg)
    } catch (e) {
        alert(msg)
    }
}
