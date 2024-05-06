(function($) {
    "use strict";

    /*****************************
     * Commons Variables
     *****************************/
    var $window = $(window),
        $body = $('body');

    /****************************
     * Sticky Menu
     *****************************/
    $(window).on('scroll', function() {
        var scroll = $(window).scrollTop();
        if (scroll < 100) {
            $(".sticky-header").removeClass("sticky");
        } else {
            $(".sticky-header").addClass("sticky");
        }
    });

    $(window).on('scroll', function() {
        var scroll = $(window).scrollTop();
        if (scroll < 100) {
            $(".seperate-sticky-bar").removeClass("sticky");
        } else {
            $(".seperate-sticky-bar").addClass("sticky");
        }
    });

    /************************************************
     * Modal Search 
     ***********************************************/
    $('a[href="#search"]').on('click', function(event) {
        event.preventDefault();
        $('#search').addClass('open');
        $('#search > form > input[type="search"]').focus();
    });

    $('#search, #search button.close').on('click', function(event) {
        if ( event.target.className == 'close' ) {
            $(this).removeClass('open');
        }
    });

    /*****************************
     * Off Canvas Function
     *****************************/
    (function() {
        var $offCanvasToggle = $('.offcanvas-toggle'),
            $offCanvas = $('.offcanvas'),
            $offCanvasOverlay = $('.offcanvas-overlay'),
            $mobileMenuToggle = $('.mobile-menu-toggle');
        $offCanvasToggle.on('click', function(e) {
            e.preventDefault();
            var $this = $(this),
                $target = $this.attr('href');
            $body.addClass('offcanvas-open');
            $($target).addClass('offcanvas-open');
            $offCanvasOverlay.fadeIn();
            if ($this.parent().hasClass('mobile-menu-toggle')) {
                $this.addClass('close');
            }
        });
        $('.offcanvas-close, .offcanvas-overlay').on('click', function(e) {
            e.preventDefault();
            $body.removeClass('offcanvas-open');
            $offCanvas.removeClass('offcanvas-open');
            $offCanvasOverlay.fadeOut();
            $mobileMenuToggle.find('a').removeClass('close');
        });
    })();


    /**************************
     * Offcanvas: Menu Content
     **************************/
    function mobileOffCanvasMenu() {
        var $offCanvasNav = $('.offcanvas-menu'),
            $offCanvasNavSubMenu = $offCanvasNav.find('.mobile-sub-menu');

        /*Add Toggle Button With Off Canvas Sub Menu*/
        $offCanvasNavSubMenu.parent().prepend('<div class="offcanvas-menu-expand"></div>');

        /*Category Sub Menu Toggle*/
        $offCanvasNav.on('click', 'li a, .offcanvas-menu-expand', function(e) {
            var $this = $(this);
            if ($this.attr('href') === '#' || $this.hasClass('offcanvas-menu-expand')) {
                e.preventDefault();
                if ($this.siblings('ul:visible').length) {
                    $this.parent('li').removeClass('active');
                    $this.siblings('ul').slideUp();
                    $this.parent('li').find('li').removeClass('active');
                    $this.parent('li').find('ul:visible').slideUp();
                } else {
                    $this.parent('li').addClass('active');
                    $this.closest('li').siblings('li').removeClass('active').find('li').removeClass('active');
                    $this.closest('li').siblings('li').find('ul:visible').slideUp();
                    $this.siblings('ul').slideDown();
                }
            }
        });
    }
    mobileOffCanvasMenu();

    /************************************************
     * Nice Select
     ***********************************************/
    $('select').niceSelect();


    /*************************
     *   Hero Slider Active
     **************************/
    var heroSlider = new Swiper('.hero-slider-active.swiper-container', {
        slidesPerView: 1,
        effect: "fade",
        speed: 1500,
        watchSlidesProgress: true,
        loop: true,
        autoplay: false,
        pagination: {
            el: '.swiper-pagination',
            clickable: true,
        },
        navigation: {
            nextEl: '.swiper-button-next',
            prevEl: '.swiper-button-prev',
        },
    });


    /****************************************
     *   Product Slider Active - 4 Grid 2 Rows
     *****************************************/
    var productSlider4grid2row = new Swiper('.product-default-slider-4grid-2row.swiper-container', {
        slidesPerView: 4,
        spaceBetween: 30,
        speed: 1500,
        slidesPerColumn: 2,
        slidesPerColumnFill: 'row',

        navigation: {
            nextEl: '.product-slider-default-2rows .swiper-button-next',
            prevEl: '.product-slider-default-2rows .swiper-button-prev',
        },

        breakpoints: {

            0: {
                slidesPerView: 1,
            },
            576: {
                slidesPerView: 2,
            },
            768: {
                slidesPerView: 2,
            },
            992: {
                slidesPerView: 3,
            },
            1200: {
                slidesPerView: 4,
            }
        }
    });


    /*********************************************
     *   Product Slider Active - 4 Grid Single Rows
     **********************************************/
    var swiper1 = new Swiper('#swiper1', {
        slidesPerView: 4,
        spaceBetween: 30,
        speed: 1500,
        navigation: {
            nextEl: '.swiper-button-next-1',
            prevEl: '.swiper-button-prev-1',
        },
        breakpoints: {
            0: { slidesPerView: 1 },
            576: { slidesPerView: 2 },
            768: { slidesPerView: 2 },
            992: { slidesPerView: 3 },
            1200: { slidesPerView: 4 },
        }
    });

    var swiper2 = new Swiper('#swiper2', {
        slidesPerView: 4,
        spaceBetween: 30,
        speed: 1500,
        navigation: {
            nextEl: '.swiper-button-next-2',  // Corrected
            prevEl: '.swiper-button-prev-2',  // Corrected
        },
        breakpoints: {
            0: { slidesPerView: 1 },
            576: { slidesPerView: 2 },
            768: { slidesPerView: 2 },
            992: { slidesPerView: 3 },
            1200: { slidesPerView: 4 },
        }
    });

    var swiper3 = new Swiper('#swiper3', {
        slidesPerView: 4,
        spaceBetween: 30,
        speed: 1500,
        navigation: {
            nextEl: '.swiper-button-next-3',
            prevEl: '.swiper-button-prev-3',
        },
        breakpoints: {
            0: { slidesPerView: 1 },
            576: { slidesPerView: 2 },
            768: { slidesPerView: 2 },
            992: { slidesPerView: 3 },
            1200: { slidesPerView: 4 },
        }
    });

    /*********************************************
     *   Product Slider Active - 4 Grid Single 3Rows
     **********************************************/
    var productSliderListview4grid3row = new Swiper('.product-listview-slider-4grid-3rows.swiper-container', {
        slidesPerView: 4,
        spaceBetween: 30,
        speed: 1500,
        slidesPerColumn: 3,
        slidesPerColumnFill: 'row',

        navigation: {
            nextEl: '.product-list-slider-3rows .swiper-button-next',
            prevEl: '.product-list-slider-3rows .swiper-button-prev',
        },

        breakpoints: {

            0: {
                slidesPerView: 1,
            },
            640: {
                slidesPerView: 2,
            },
            768: {
                slidesPerView: 2,
            },
            992: {
                slidesPerView: 3,
            },
            1200: {
                slidesPerView: 4,
            }
        }
    });


    /*********************************************
     *   Blog Slider Active - 3 Grid Single Rows
     **********************************************/
    var blogSlider = new Swiper('.blog-slider.swiper-container', {
        slidesPerView: 3,
        spaceBetween: 30,
        speed: 1500,

        navigation: {
            nextEl: '.blog-default-slider .swiper-button-next',
            prevEl: '.blog-default-slider .swiper-button-prev',
        },
        breakpoints: {

            0: {
                slidesPerView: 1,
            },
            576: {
                slidesPerView: 2,
            },
            768: {
                slidesPerView: 2,
            },
            992: {
                slidesPerView: 3,
            },
        }
    });


    /*********************************************
     *   Company Logo Slider Active - 7 Grid Single Rows
     **********************************************/
    var companyLogoSlider = new Swiper('.company-logo-slider.swiper-container', {
        slidesPerView: 7,
        speed: 1500,

        navigation: {
            nextEl: '.company-logo-slider .swiper-button-next',
            prevEl: '.company-logo-slider .swiper-button-prev',
        },
        breakpoints: {

            0: {
                slidesPerView: 1,
            },
            480: {
                slidesPerView: 2,
            },
            768: {
                slidesPerView: 3,
            },
            992: {
                slidesPerView: 3,
            },
            1200: {
                slidesPerView: 7,
            },
        }
    });

    /********************************
     *  Product Gallery - Horizontal View
     **********************************/
    var galleryThumbsHorizontal = new Swiper('.product-image-thumb-horizontal.swiper-container', {
        loop: true,
        speed: 1000,
        spaceBetween: 25,
        slidesPerView: 4,
        freeMode: true,
        watchSlidesVisibility: true,
        watchSlidesProgress: true,
        navigation: {
            nextEl: '.swiper-button-next',
            prevEl: '.swiper-button-prev',
        },

    });

    var gallerylargeHorizonatal = new Swiper('.product-large-image-horaizontal.swiper-container', {
        slidesPerView: 1,
        speed: 1500,
        thumbs: {
            swiper: galleryThumbsHorizontal
        }
    });

    /********************************
     *  Product Gallery - Vertical View
     **********************************/
    var galleryThumbsvartical = new Swiper('.product-image-thumb-vartical.swiper-container', {
        direction: 'vertical',
        centeredSlidesBounds: true,
        slidesPerView: 4,
        watchOverflow: true,
        watchSlidesVisibility: true,
        watchSlidesProgress: true,
        spaceBetween: 25,
        freeMode: true,
        navigation: {
            nextEl: '.swiper-button-next',
            prevEl: '.swiper-button-prev',
        },

    });

    var gallerylargeVartical = new Swiper('.product-large-image-vartical.swiper-container', {
        slidesPerView: 1,
        speed: 1500,
        thumbs: {
            swiper: galleryThumbsvartical
        }
    });

    /********************************
     *  Product Gallery - Single Slide View
     * *********************************/
    var singleSlide = new Swiper('.product-image-single-slide.swiper-container', {
        loop: true,
        speed: 1000,
        spaceBetween: 25,
        slidesPerView: 4,
        navigation: {
            nextEl: '.swiper-button-next',
            prevEl: '.swiper-button-prev',
        },

        breakpoints: {
            0: {
                slidesPerView: 1,
            },
            576: {
                slidesPerView: 2,
            },
            768: {
                slidesPerView: 3,
            },
            992: {
                slidesPerView: 4,
            },
            1200: {
                slidesPerView: 4,
            },
        }

    });

    /******************************************************
     * Quickview Product Gallery - Horizontal
     ******************************************************/
    var modalGalleryThumbs = new Swiper('.modal-product-image-thumb', {
        spaceBetween: 10,
        slidesPerView: 4,
        freeMode: true,
        watchSlidesVisibility: true,
        watchSlidesProgress: true,
        navigation: {
          nextEl: '.swiper-button-next',
          prevEl: '.swiper-button-prev',
        },
      });

      var modalGalleryTop = new Swiper('.modal-product-image-large', { 
        thumbs: {
          swiper: modalGalleryThumbs
        }
      });

    /********************************
     * Blog List Slider - Single Slide
     * *********************************/
    var blogListSLider = new Swiper('.blog-list-slider.swiper-container', {
        loop: true,
        speed: 1000,
        slidesPerView: 1,
        navigation: {
            nextEl: '.swiper-button-next',
            prevEl: '.swiper-button-prev',
        },

    });

    /********************************
     *  Product Gallery - Image Zoom
     **********************************/
    $('.zoom-image-hover').zoom();


    /************************************************
     * Price Slider
     ***********************************************/
    $("#slider-range").slider({
        range: true,
        min: 0,
        max: 500,
        values: [75, 300],
        slide: function(event, ui) {
            $("#amount").val("$" + ui.values[0] + " - $" + ui.values[1]);
        }
    });
    $("#amount").val("$" + $("#slider-range").slider("values", 0) +
        " - $" + $("#slider-range").slider("values", 1));



    /************************************************
     * Animate on Scroll
     ***********************************************/
    AOS.init({
       
        duration: 1000, 
        once: true, 
        easing: 'ease',
    });
    window.addEventListener('load', AOS.refresh);    

    /************************************************
     * Video  Popup
     ***********************************************/
    $('.video-play-btn').venobox(); 

    /************************************************
     * Scroll Top
     ***********************************************/
    $('body').materialScrollTop();


})(jQuery);



function formatNumber(number) {
    number = number.toFixed(0) + '';
    x = number.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    return x1 + x2;
}


$(document).ready(function () {
   
    $('.price').each(function () {
       
        var originalPrice = $(this).text();

     
        var numericPrice = parseFloat(originalPrice.replace(/[^0-9.-]+/g, ""));

      
        var formattedPrice = formatNumber(numericPrice);

       
        $(this).text(formattedPrice);
    });
});


$(document).ready(function () {

    $('.product-price').each(function () {

        var originalPrice = $(this).text();


        var numericPrice = parseFloat(originalPrice.replace(/[^0-9.-]+/g, ""));


        var formattedPrice = formatNumber(numericPrice);


        $(this).text(formattedPrice);
    });
});
$(document).ready(function () {

    $('.cart_amout').each(function () {

        var originalPrice = $(this).text();


        var numericPrice = parseFloat(originalPrice.replace(/[^0-9.-]+/g, ""));


        var formattedPrice = formatNumber(numericPrice) + ' VND';


        $(this).text(formattedPrice);
    });
});

$(document).ready(function () {

    $('.cart_total').each(function () {

        var originalPrice = $(this).text();


        var numericPrice = parseFloat(originalPrice.replace(/[^0-9.-]+/g, ""));


        var formattedPrice = formatNumber(numericPrice) + ' VND ';


        $(this).text(formattedPrice);
    });
});


$(document).ready(function () {

    $('.product_total').each(function () {

        var originalPrice = $(this).text();


        var numericPrice = parseFloat(originalPrice.replace(/[^0-9.-]+/g, ""));


        var formattedPrice = formatNumber(numericPrice);


        $(this).text(formattedPrice);
    });
});



document.addEventListener("DOMContentLoaded", function () {
    var detailLinks = document.querySelectorAll('.detail-link');

    // Loop through each element and attach a click event
    detailLinks.forEach(function (link) {
        link.addEventListener('click', function (event) {
            // Prevent the default behavior of the link (to avoid navigating to "#")
            event.preventDefault();

            // Get the data-product-id attribute value
            var productId = this.getAttribute('data-product-id');

            // Redirect to the "/Home/ChiTietSanPham" page with the product ID
            window.location.href = '/Home/ChiTietSanPham/' + productId;
        });
    });
});


function updateTotal(inputElement) {
    // Get the row of the changed input
    var row = inputElement.closest('tr');

    // Get the price and quantity elements within the row
    var priceElement = row.querySelector('.product-price');
    var quantityElement = row.querySelector('.product_quantity input');
    var totalElement = row.querySelector('.product_total');
    var quantityDisplay = document.querySelector('.cart_quantity');
    var totalDisplay = document.querySelector('.cart_total');

    // Extract numeric values
    var price = parseFloat(priceElement.textContent.replace(/[^\d.]/g, '')); // Remove non-numeric characters
    var quantity = parseFloat(quantityElement.value);

    // Update the total based on the new quantity
    var total = price * quantity;

    // Update the total element in the row with formatted number
    totalElement.textContent = formatNumber(total);
    quantityDisplay.textContent = quantity;
    totalDisplay.textContent = formatNumber(total) + ' VND';
    document.getElementById('tongtien').value = total;
    document.getElementById('soluong').value = quantity;
}

document.addEventListener('DOMContentLoaded', function () {
    var orderForm = document.getElementById('orderForm');

    orderForm.addEventListener('submit', function (event) {
        // Get the name and phone number input elements
        var nameInput = document.querySelector('input[name="hoten"]');
        var phoneInput = document.querySelector('input[name="sdt"]');

        // Validate the name and phone number
        var isValid = true;

        if (!nameInput.value.trim()) {
            // Name is not filled
            isValid = false;
            nameInput.style.border = '1px solid red'; // Add red border for visual indication
            alert("Bạn chưa nhập họ và tên");
        } else {
            nameInput.style.border = ''; // Reset border if valid
        }

        if (!phoneInput.value.trim()) {
            // Phone number is not filled
            isValid = false;
            phoneInput.style.border = '1px solid red'; // Add red border for visual indication
            alert("Bạn chưa nhập số điện thoại");
        } else {
            phoneInput.style.border = ''; // Reset border if valid
        }

        // If there are validation errors, prevent the form submission
        if (!isValid) {
            event.preventDefault();
        }
    });
});


$(document).ready(function () {
    $('.hero-slider-active').slick({
        autoplay: true,
        autoplaySpeed: 4000,
        arrows: true,
        dots: true,
        prevArrow: $('.swiper-button-prev'),
        nextArrow: $('.swiper-button-next'),
        rtl: true, // Ảnh sẽ chuyển động từ phải qua trái
    });
});