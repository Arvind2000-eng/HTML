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
            letter.classList.remove("invalid");
            letter.classList.add("valid");
        } else {
            letter.classList.remove("valid");
            letter.classList.add("invalid");
        }

        var upperCaseLetters = /[A-Z]/g;
        if (myInput.value.match(upperCaseLetters)) {
            capital.classList.remove("invalid");
            capital.classList.add("valid");
        } else {
            capital.classList.remove("valid");
            capital.classList.add("invalid");
        }

        var numbers = /[0-9]/g;
        if (myInput.value.match(numbers)) {
            number.classList.remove("invalid");
            number.classList.add("valid");
        } else {
            number.classList.remove("valid");
            number.classList.add("invalid");
        }

        var specialsymbole = /[!@#$%^&*()\-_+.=<>,?/:;|~{}]/g;
        if (myInput.value.match(specialsymbole)) {
            spesymb.classList.remove("invalid");
            spesymb.classList.add("valid");
        } else {
            spesymb.classList.remove("valid");
            spesymb.classList.add("invalid");
        }

        if (myInput.value.length >= 8) {
            length.classList.remove("invalid");
            length.classList.add("valid");
        } else {
            length.classList.remove("valid");
            length.classList.add("invalid");
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


//for boarder changing
$(document).ready(function () {
    var service1 = $(".extra-services .service1 .img-container");
    var service2 = $(".extra-services .service2 .img-container");
    var service3 = $(".extra-services .service3 .img-container");
    var service4 = $(".extra-services .service4 .img-container");
    var service5 = $(".extra-services .service5 .img-container");

    var servimg1 = $(".extra-services .service1");
    var servimg2 = $(".extra-services .service2");
    var servimg3 = $(".extra-services .service3");
    var servimg4 = $(".extra-services .service4");
    var servimg5 = $(".extra-services .service5");

    var setborder = "3px solid #1d7a8c";

    var servdtl1 = $(".extra-service-detail .service1");
    var servdtl2 = $(".extra-service-detail .service2");
    var servdtl3 = $(".extra-service-detail .service3");
    var servdtl4 = $(".extra-service-detail .service4");
    var servdtl5 = $(".extra-service-detail .service5");

    var srvedit = Number($(".total-service-time td p #tsrvedt").text());
    var perCleaningPrice = Number($("#perclprc").text());
    var effectivePrice = Number($("#effeprc").text());

    function cleanprcedt() {
        $("#perclprc").text(perCleaningPrice.toFixed(2));
        $("#totalprc").text(perCleaningPrice.toFixed(2));
        $("#effeprc").text(effectivePrice.toFixed(2));
    }

    function grt3check(number) {
        if (number >= 3.0) {
            $(".total-service-time").removeClass("d-none");
            $(".total-service-time td p #tsrvedt").text(srvedit);
        }
        else {
            $(".total-service-time").addClass("d-none");
        }
    }

    servimg1.click(function () {
        
        if (service1.css("border") == "1px solid rgb(200, 200, 200)") {
            service1.css("border", setborder);
            servdtl1.removeClass("d-none");
            srvedit += 0.5;
            perCleaningPrice += 9.0;
            effectivePrice = perCleaningPrice * 0.8;
        }
        else {
            service1.css("border", "1px solid #c8c8c8");
            servdtl1.addClass("d-none");
            srvedit -= 0.5;
            perCleaningPrice -= 9.0;
            effectivePrice = perCleaningPrice * 0.8;
        }
        grt3check(srvedit);
        cleanprcedt();
    });

    servimg2.click(function () {
        if (service2.css("border") == "1px solid rgb(200, 200, 200)") {
            service2.css("border", setborder);
            servdtl2.removeClass("d-none");
            srvedit += 0.5;
            perCleaningPrice += 9.0;
            effectivePrice = perCleaningPrice * 0.8;
        }
        else {
            service2.css("border", "1px solid #c8c8c8");
            servdtl2.addClass("d-none");
            srvedit -= 0.5;
            perCleaningPrice -= 9.0;
            effectivePrice = perCleaningPrice * 0.8;
        }
        grt3check(srvedit);
        cleanprcedt();
    });

    servimg3.click(function () {
        if (service3.css("border") == "1px solid rgb(200, 200, 200)") {
            service3.css("border", setborder);
            servdtl3.removeClass("d-none");
            srvedit += 0.5;
            perCleaningPrice += 9.0;
            effectivePrice = perCleaningPrice * 0.8;
        }
        else {
            service3.css("border", "1px solid #c8c8c8");
            servdtl3.addClass("d-none");
            srvedit -= 0.5;
            perCleaningPrice -= 9.0;
            effectivePrice = perCleaningPrice * 0.8;
        }
        grt3check(srvedit);
        cleanprcedt();
    });

    servimg4.click(function () {
        if (service4.css("border") == "1px solid rgb(200, 200, 200)") {
            service4.css("border", setborder);
            servdtl4.removeClass("d-none");
            srvedit += 0.5;
            perCleaningPrice += 9.0;
            effectivePrice = perCleaningPrice * 0.8;
        }
        else {
            service4.css("border", "1px solid #c8c8c8");
            servdtl4.addClass("d-none");
            srvedit -= 0.5;
            perCleaningPrice -= 9.0;
            effectivePrice = perCleaningPrice * 0.8;
        }
        grt3check(srvedit);
        cleanprcedt();
    });

    servimg5.click(function () {
        if (service5.css("border") == "1px solid rgb(200, 200, 200)") {
            service5.css("border", setborder);
            servdtl5.removeClass("d-none");
            srvedit += 0.5;
            perCleaningPrice += 9.0;
            effectivePrice = perCleaningPrice * 0.8;
        }
        else {
            service5.css("border", "1px solid #c8c8c8");
            servdtl5.addClass("d-none");
            srvedit -= 0.5;
            perCleaningPrice -= 9.0;
            effectivePrice = perCleaningPrice * 0.8;
        }
        grt3check(srvedit);
        cleanprcedt();
    });
    
});





//registration page checkbox to button validate
var tc = document.getElementById('terms-condition');
var dp = document.getElementById('data-protection');
var us = document.getElementById('usersubmit');

function termcondition() {
    if (tc.checked && dp.checked) {
        us.disabled = false;
    }
    else {
        us.disabled = true;
    }
};
function dataprotection() {
    if (dp.checked && tc.checked) {
        us.disabled = false;
    }
    else {
        us.disabled = true;
    }
};




//HelperRegistration page checkbox to button validate

function tchelper() {
    var checker = document.getElementById('accept');
    var sendbtn = document.getElementById('Helper_Registration');
    if (checker.checked) {
        sendbtn.disabled = false;
    }
    else {
        sendbtn.disabled = true;
    }
};






