jQuery(function ($) {

    "use strict";

    /*
     PrettyPhoto
     */
    $("a[rel^='prettyPhoto']").prettyPhoto({
        show_title: false,
        social_tools: false
    });


    /*
     FitVids
     */
    $("body").fitVids();


    /*
     Remove Empty
     */
    $('p:empty').remove();


    /*
     Video Background
     */
    $('.video-wrap video').mediaelementplayer();


    /*
     Smooth Scroll
     */
    $('nav a[href*="#"]:not([href="#"])').click(function () {
        if (location.pathname.replace(/^\//, '') == this.pathname.replace(/^\//, '') && location.hostname == this.hostname) {
            var target = $(this.hash);
            //target = target.length ? target : $('[data-sectionid=' + this.hash.slice(1) +']');
            target = target.length ? target : $('[id=' + this.hash.slice(1) + ']');
            if (target.length) {
                $('html,body').animate({
                    scrollTop: target.offset().top
                }, 1500);
                return false;
            }
            else {
                var section = $('[data-sectionid=' + this.hash.slice(1) + ']');
                if (section.length) {
                    $('html,body').animate({
                        scrollTop: section.offset().top - 60
                    }, 1500);
                    return false;
                }
            }
        }
    });


    /*
     Back to Top
     */
    $(window).on('scroll', function () {
        if ($(this).scrollTop() > 200) {
            $('.scrollTop').fadeIn();
        } else {
            $('.scrollTop').fadeOut();
        }
    });

    $('.scrollTop').click(function (e) {
        e.stopPropagation();
        $('body,html').animate({
            scrollTop: 0
        }, 800);
        return false;
    });


    /*
     Header Sticky
     */
    if ($('body').hasClass('header-sticky')) {
        var affixHeader = $('.header-stick');
        var windowSize = $(window).width();
        var headerHeight = $('#header').innerHeight() - affixHeader.innerHeight();
        if (windowSize > 991) {
            if ($('body').hasClass('header-style-v1') && !$('body').hasClass('header-transparent')) {
                var smoothHeight = $('.header-stick').outerHeight(true);
                affixHeader.wrap('<div id="header-smooth"></div>').parent().css({height: smoothHeight}); //wrap header for smooth stick and unstick
            }
            $(window).on('load resize scroll', function () {
                $(affixHeader).affix({
                    offset: {
                        top: headerHeight,
                        bottom: 0
                    },
                });
            });
        }
    }


    /*
     Header Search
     */
    if ($('.header-search').length) {
        $('.search-icon').on('click', function (e) {
            $(this).parents().find('.search-form').fadeToggle();
            return false;
        });
        $('body').on('click', function (e) {
            var $searchform = $('.search-form');
            if (!($searchform.has(e.target).length || $(e.target).is('.search-form input'))) {
                $searchform.fadeOut('fast', 'swing');
            }
        });
    }


    /*
     Menu Nav
     */
    $('ul#primary-menu li, ul#sticky-menu li').on({
        mouseenter: function () {
            $(this).children('ul').stop(true, true).fadeIn(300);
        },
        mouseleave: function () {
            $(this).children('ul').fadeOut(100);
        }
    });

    var desktopmenu = $('ul#primary-menu');
    $('<div id="mobile-container"><ul id="mobile-menu" class="mobile-menu"></ul></div>').insertAfter($('#toggle-mobile-menu'));
    $('ul#mobile-menu').html(desktopmenu.children().clone());

    $('#toggle-mobile-menu').on('click', function (e) {
        e.preventDefault();
        $('#mobile-menu').slideToggle();
        $('#mobile-menu .sub-menu').slideUp();
    });

    $('ul#mobile-menu li.menu-item-has-children > a').on('click', function (e) {
        e.preventDefault();
        $(this).toggleClass('active');
        $(this).next().slideToggle();
        $(this).parent().siblings().find('a.active').removeClass('active');
        $(this).parent().siblings().find('.sub-menu').slideUp();
    });


    /*
     Parallax Title
     */
    if ($('.title-parallax #title-wrapper').length) {
        $('.title-parallax #title-wrapper').each(function () {
            $(this).parallax("50%", 0.4);
        });
    }


    /*
     Parallax Section
     */
    if ($('.wpb_parallax').length) {
        $('.wpb_parallax').each(function () {
            var speed = $(this).data('speed') * 0.4;
            $(this).parallax("50%", speed);
        });
    }

    /*
     Isotope fitRows
     */
    var $gridBlog = $('.st-blog > .row').imagesLoaded(function () {
        // init Isotope after all images have loaded
        $gridBlog.isotope({
            layoutMode: 'fitRows'
        });
    });
    var $gridPort = $('.st-portfolio > .row').imagesLoaded(function () {
        // init Isotope after all images have loaded
        $gridPort.isotope({
            layoutMode: 'fitRows'
        });
    });
    var $gridServ = $('.st-service > .row').imagesLoaded(function () {
        // init Isotope after all images have loaded
        $gridServ.isotope({
            layoutMode: 'fitRows'
        });
    });
    var $gridTeam = $('.st-team > .row').imagesLoaded(function () {
        // init Isotope after all images have loaded
        $gridTeam.isotope({
            layoutMode: 'fitRows'
        });
    });
    var $gridTesti = $('.st-testimonial > .row').imagesLoaded(function () {
        // init Isotope after all images have loaded
        $gridTesti.isotope({
            layoutMode: 'fitRows'
        });
    });


    /*
     Load More
     */
    $('.load-more a').live('click', function (e) {
        e.preventDefault();
        $(this).addClass('loading').text('Loading...');
        var holder = $(this).closest('div').prev();
        var classNames = holder.attr('class');
        var classNames = classNames.split(/\s+/);
        var uniqueHolder = '.' + classNames[1] + ' .post-item';
        var uniqueNext = '.load-' + classNames[1] + ' a';
        $.ajax({
            type: 'GET',
            url: $(this).attr('href'),
            dataType: 'html',
            success: function (out) {
                var result = $(out).find(uniqueHolder);
                var nextlink = $(out).find(uniqueNext).attr('href');
                holder.append(result).isotope('reloadItems').isotope();
                $(uniqueNext).removeClass('loading').text('Show More');
                if (nextlink != undefined) {
                    $(uniqueNext).attr('href', nextlink);
                } else {
                    $(uniqueNext).parent().remove();
                }

                $("a[rel^='prettyPhoto']").prettyPhoto({
                    show_title: false,
                    social_tools: false
                });

                // Isotope Chrome Fix
                setTimeout(function () {
                    $container.isotope('layout');
                }, 1000);
            }
        });
    });
    /*
     Shortcode Countdown
     */
    $('.countdown').each(function () {
        var launch = $(this).data('date');
        $(this).countdown({until: new Date(launch)});
    });

});

jQuery(document).ready(function ($) {

    wow = new WOW(
        {
            boxClass: 'wow',      // default
            animateClass: 'animated', // default
            offset: 0,          // default
            mobile: false,       // default
            live: true        // default
        }
    )
    wow.init();

});