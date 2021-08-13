var htmlDiv = document.getElementById("rs-plugin-settings-inline-css");
var htmlDivCss = ".tp-caption.Gym-SmallText,.Gym-SmallText{color:rgba(255,255,255,1.00);font-size:17px;line-height:22px;font-weight:300;font-style:normal;padding:0px 0px 0px 0px;text-decoration:none;background-color:transparent;border-color:transparent;border-style:none;border-width:0px;border-radius:0px 0px 0px 0px;text-shadow:none}.tp-caption.NotGeneric-Title,.NotGeneric-Title{color:rgba(255,255,255,1.00);font-size:70px;line-height:70px;font-weight:800;font-style:normal;padding:10px 0px 10px 0;text-decoration:none;background-color:transparent;border-color:transparent;border-style:none;border-width:0px;border-radius:0 0 0 0px}.tp-caption.NotGeneric-SubTitle,.NotGeneric-SubTitle{color:rgba(255,255,255,1.00);font-size:13px;line-height:20px;font-weight:500;font-style:normal;padding:0 0 0 0px;text-decoration:none;background-color:transparent;border-color:transparent;border-style:none;border-width:0px;border-radius:0 0 0 0px;text-align:left;letter-spacing:1px;text-align:left}.tp-caption.NotGeneric-CallToAction,.NotGeneric-CallToAction{color:rgba(255,255,255,1.00);font-size:14px;line-height:14px;font-weight:500;font-style:normal;padding:10px 30px 10px 30px;text-decoration:none;background-color:rgba(0,0,0,0);border-color:rgba(255,255,255,0.50);border-style:solid;border-width:1px;border-radius:0px 0px 0px 0px;text-align:left;letter-spacing:3px;text-align:left}.tp-caption.NotGeneric-CallToAction:hover,.NotGeneric-CallToAction:hover{color:rgba(255,255,255,1.00);text-decoration:none;background-color:transparent;border-color:rgba(255,255,255,1.00);border-style:solid;border-width:1px;border-radius:0px 0px 0px 0px;cursor:pointer}.tp-caption.NotGeneric-Icon,.NotGeneric-Icon{color:rgba(255,255,255,1.00);font-size:30px;line-height:30px;font-weight:400;font-style:normal;padding:0px 0px 0px 0px;text-decoration:none;background-color:rgba(0,0,0,0);border-color:rgba(255,255,255,0);border-style:solid;border-width:0px;border-radius:0px 0px 0px 0px;text-align:left;letter-spacing:3px;text-align:left}";
if (htmlDiv) {
    htmlDiv.innerHTML = htmlDiv.innerHTML + htmlDivCss;
} else {
    var htmlDiv = document.createElement("div");
    htmlDiv.innerHTML = "<style>" + htmlDivCss + "</style>";
    document.getElementsByTagName("head")[0].appendChild(htmlDiv.childNodes[0]);
}

var setREVStartSize = function () {
    try {
        var e = new Object, i = jQuery(window).width(), t = 9999, r = 0, n = 0, l = 0, f = 0, s = 0, h = 0;
        e.c = jQuery('#rev_slider_12_1');
        e.responsiveLevels = [1240, 1024, 778, 480];
        e.gridwidth = [1240, 1024, 778, 480];
        e.gridheight = [700, 600, 500, 400];

        e.sliderLayout = "fullwidth";
        if (e.responsiveLevels && (jQuery.each(e.responsiveLevels, function (e, f) {
                    f > i && (t = r = f, l = e), i > f && f > r && (r = f, n = e)
        }), t > r && (l = n)), f = e.gridheight[l] || e.gridheight[0] || e.gridheight, s = e.gridwidth[l] || e.gridwidth[0] || e.gridwidth, h = i / s, h = h > 1 ? 1 : h, f = Math.round(h * f), "fullscreen" == e.sliderLayout) {
            var u = (e.c.width(), jQuery(window).height());
            if (void 0 != e.fullScreenOffsetContainer) {
                var c = e.fullScreenOffsetContainer.split(",");
                if (c) jQuery.each(c, function (e, i) {
                    u = jQuery(i).length > 0 ? u - jQuery(i).outerHeight(!0) : u
                }), e.fullScreenOffset.split("%").length > 1 && void 0 != e.fullScreenOffset && e.fullScreenOffset.length > 0 ? u -= jQuery(window).height() * parseInt(e.fullScreenOffset, 0) / 100 : void 0 != e.fullScreenOffset && e.fullScreenOffset.length > 0 && (u -= parseInt(e.fullScreenOffset, 0))
            }
            f = u
        } else void 0 != e.minHeight && f < e.minHeight && (f = e.minHeight);
        e.c.closest(".rev_slider_wrapper").css({ height: f })

    } catch (d) {
        console.log("Failure at Presize of Slider:" + d)
    }
};

setREVStartSize();

var tpj = jQuery;

var revapi12;
tpj(document).ready(function () {
    if (tpj("#rev_slider_12_1").revolution == undefined) {
        revslider_showDoubleJqueryError("#rev_slider_12_1");
    } else {
        revapi12 = tpj("#rev_slider_12_1").show().revolution({
            sliderType: "standard",
            jsFileLocation: "/Content/plugins/revslider/public/assets/js/",
            sliderLayout: "fullwidth",
            dottedOverlay: "none",
            delay: 9000,
            navigation: {
                keyboardNavigation: "off",
                keyboard_direction: "horizontal",
                mouseScrollNavigation: "off",
                mouseScrollReverse: "default",
                onHoverStop: "off",
                touch: {
                    touchenabled: "on",
                    swipe_threshold: 75,
                    swipe_min_touches: 50,
                    swipe_direction: "vertical",
                    drag_block_vertical: false
                }
                ,
                arrows: {
                    style: "custom",
                    enable: true,
                    hide_onmobile: false,
                    hide_onleave: false,
                    tmp: '',
                    left: {
                        h_align: "right",
                        v_align: "bottom",
                        h_offset: 46,
                        v_offset: 0
                    },
                    right: {
                        h_align: "right",
                        v_align: "bottom",
                        h_offset: 5,
                        v_offset: 0
                    }
                }
            },
            responsiveLevels: [1240, 1024, 778, 480],
            visibilityLevels: [1240, 1024, 778, 480],
            gridwidth: [1240, 1024, 778, 480],
            gridheight: [700, 600, 500, 400],
            lazyType: "none",
            parallax: {
                type: "mouse",
                origo: "slidercenter",
                speed: 2000,
                levels: [2, 3, 4, 5, 6, 7, 12, 16, 10, 50, 47, 48, 49, 50, 51, 55],
                type: "mouse",
            },
            shadow: 0,
            spinner: "spinner2",
            stopLoop: "off",
            stopAfterLoops: -1,
            stopAtSlide: -1,
            shuffle: "off",
            autoHeight: "off",
            disableProgressBar: "on",
            hideThumbsOnMobile: "off",
            hideSliderAtLimit: 0,
            hideCaptionAtLimit: 0,
            hideAllCaptionAtLilmit: 0,
            startWithSlide: 0,
            debugMode: false,
            fallbacks: {
                simplifyAll: "off",
                nextSlideOnWindowFocus: "off",
                disableFocusListener: false,
            }
        });
    }
});
jQuery(document).ready(function ($) {
    $(".post-slider").owlCarousel({
        singleItem: true,
        autoPlay: true,
        autoHeight: true,
        pagination: false,
        navigation: true,
        navigationText: ['<i class="fa fa-angle-left">', '<i class="fa fa-angle-right">']
    });

    $(".st-portfolio-slider > div").owlCarousel({
        items: 4,
        pagination: false,
        navigation: true,
        navigationText: ['<i class="fa fa-angle-left">', '<i class="fa fa-angle-right">']
    });

    $(".st-testimonial-slider").owlCarousel({
        singleItem: true,
        autoPlay: true,
        pagination: true,
        navigation: false
    });
    //$(".st-client-slider").owlCarousel({
    //    items: 5,
    //    autoPlay: true,
    //    pagination: false,
    //    navigation: false
    //});
    $('.counter-number').waypoint({
        offset: '100%',
        triggerOnce: true,
        handler: function () {
            var el = $(this);
            var duration = Math.floor((Math.random() * 1000) + 1000);
            var to = el.attr('data-to');

            $({ property: 0 }).animate({ property: to }, {
                duration: duration,
                easing: 'linear',
                step: function () {
                    el.text(Math.floor(this.property));
                },
                complete: function () {
                    el.text(this.property);
                }
            });
        }
    });
    /*
     Isotope Filter
     */
    $('#load-filter li a').on('click', function (e) {
        $('#load-filter li').removeClass('active');
        $(this).parent().addClass('active');
        var selector = $(this).attr('data-filter');
        var holder = $(this).closest('div').next();
        holder.isotope({
            filter: selector
        });
        return false;
    });
    /*
     Shortcode Icon Box
     */
    if ($('.st-iconbox').length) {
        $('.st-iconbox').each(function () {
            if ($(this).data('hover-background') !== 'undefined' && $(this).data('hover-background') !== false) {
                var hover_background = $(this).data('hover-background');
                $(this).hover(function () {
                    $(this).css('background-color', hover_background);
                },
                        function () {
                            $(this).css('background-color', '');
                        });
            }
            if ($(this).data('hover-color') !== 'undefined' && $(this).data('hover-color') !== false) {
                var hover_color = $(this).data('hover-color');
                $(this).hover(function () {
                    $(this).css('color', hover_color);
                },
                        function () {
                            $(this).css('color', '');
                        });
            }
        });
    }
});