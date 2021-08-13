//  js page loading
$(window).on('load', function (event) {
    $('body').removeClass('preloading');
    // $('.load').delay(1000).fadeOut('fast');
    $('.loader').delay(1000).fadeOut('fast');
});

//jquery active
$(function () {
    jQuery(function ($) {
        var path = window.location.href; // because the 'href' property of the DOM element is the absolute path
        $('ul li a').each(function () {
            if (this.href === path) {
                $(this).addClass('active');
            }
        });
    });
})
