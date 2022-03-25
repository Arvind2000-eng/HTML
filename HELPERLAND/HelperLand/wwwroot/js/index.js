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






//Start....... Strong Password
var myInput = document.getElementById("psw-user");
var letter = document.getElementById("letter");
var capital = document.getElementById("capital");
var number = document.getElementById("number");
var length = document.getElementById("length");
var speSymb = document.getElementById("spesymb");

$(document).ready(function () {
    $("#psw-user").focus(function () {
        $("#message").css("display", "block");
    });
});
$(document).ready(function () {
    $("#psw-user").focusout(function () {
        $("#message").css("display", "none");
    });
});

$(document).ready(function () {
    $("#psw-user").keyup(function () {
        var lowerCaseLetters = /[a-z]/g;
        if (myInput.value.match(lowerCaseLetters)) {
            letter.classList.remove("invalid1");
            letter.classList.add("valid1");
        } else {
            letter.classList.remove("valid1");
            letter.classList.add("invalid1");
        }

        var upperCaseLetters = /[A-Z]/g;
        if (myInput.value.match(upperCaseLetters)) {
            capital.classList.remove("invalid1");
            capital.classList.add("valid1");
        } else {
            capital.classList.remove("valid1");
            capital.classList.add("invalid1");
        }

        var numbers = /[0-9]/g;
        if (myInput.value.match(numbers)) {
            number.classList.remove("invalid1");
            number.classList.add("valid1");
        } else {
            number.classList.remove("valid1");
            number.classList.add("invalid1");
        }

        var specialsymbole = /[!@#$%^&*()\-_+.=<>,?/:;|~{}]/g;
        if (myInput.value.match(specialsymbole)) {
            spesymb.classList.remove("invalid1");
            spesymb.classList.add("valid1");
        } else {
            spesymb.classList.remove("valid1");
            spesymb.classList.add("invalid1");
        }

        if (myInput.value.length >= 8) {
            length.classList.remove("invalid1");
            length.classList.add("valid1");
        } else {
            length.classList.remove("valid1");
            length.classList.add("invalid1");
        }
    });
});

//End....... Strong Password













$(document).ready(function () {
    $(".scenario1-button").click(function () {
        $(this).hide();
        $(".scenario1").removeClass("hide");
    });
});


$(document).ready(function () {
    $(".invoice-checkbox ,.invoice-checkbox label").click(function () {
        $(".invoice-address-form").toggleClass("hide show");
    });
});





$(document).ready(function () {
    var tc = $("#terms-condition");
    var dp = $("#data-protection");
    var us = $("#usersubmit");
    //alert("ok");
    $("#terms-condition").click(function () {
        //alert(tc.prop('checked') == true);
        if (tc.prop('checked') == true && dp.prop('checked') == true) {
            us.removeAttr('disabled');
        }
        else {
            us.prop('disabled', true);
        }
    });
    $("#data-protection").click(function () {
        //alert(dp.prop('checked') == true);
        if (tc.prop('checked') == true && dp.prop('checked') == true) {
            us.removeAttr('disabled');
        }
        else {
            us.prop('disabled', true);
        }
    });

    
});








