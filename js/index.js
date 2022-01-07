function fixed_header() {
    if (jQuery(window).scrollTop() > 0) {
        jQuery('header').addClass('fixed');
    }
    else {
        jQuery('header').removeClass('fixed');
    }    
}

 jQuery(window).scroll(function () {
   fixed_header();
});