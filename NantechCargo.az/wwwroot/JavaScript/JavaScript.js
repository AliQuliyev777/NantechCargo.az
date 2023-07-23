
//window.addEventListener("scroll", function () {
//     console.log(window.scrollY);
//    if (window.scrollY >= 100) {
//        // document.querySelector(".layout-header ul li a").style.color = "white"
//        $(".navbarMain").css("background-color", "rgb(78,0,142)");

//    }
//    else {
//        $(".navbarMain").css("background-color", "rgb(78,0,142)");

//    }
//});


// HERO SLIDER
var menu = [];
jQuery('.swiper-slide').each(function (index) {
    menu.push(jQuery(this).find('.slide-inner').attr("data-text"));

});
var interleaveOffset = 0.5;
var swiperOptions = {
    loop: true,
    speed: 1000,
    parallax: true,
    autoplay: {
        delay: 6500,
        disableOnInteraction: false,

    },
    watchSlidesProgress: true,
    pagination: {
        el: '.swiper-pagination',
        clickable: true,
    },

    navigation: {

        nextEl: '.swiper-button-next',
        prevEl: '.swiper-button-prev',

    },

    on: {
        progress: function () {

            var swiper = this;
            for (var i = 0; i < swiper.slides.length; i++) {
                var slideProgress = swiper.slides[i].progress;
                var innerOffset = swiper.width * interleaveOffset;

                var innerTranslate = slideProgress * innerOffset;
                swiper.slides[i].querySelector(".slide-inner").style.transform =
                    "translate3d(" + innerTranslate + "px, 0, 0)";

            }
        },

        touchStart: function () {

            var swiper = this;
            for (var i = 0; i < swiper.slides.length; i++) {
                swiper.slides[i].style.transition = "";

            }
        },

        setTransition: function (speed) {
            var swiper = this;

            for (var i = 0; i < swiper.slides.length; i++) {

                swiper.slides[i].style.transition = speed + "ms";
                swiper.slides[i].querySelector(".slide-inner").style.transition =
                    speed + "ms";

            }
        }
    }
};

var swiper = new Swiper(".swiper-container", swiperOptions);

// DATA BACKGROUND IMAGE
var sliderBgSetting = $(".slide-bg-image");
sliderBgSetting.each(function (indx) {

    if ($(this).attr("data-background")) {

        $(this).css("background-image", "url(" + $(this).data("background") + ")");

    }
});


// let btn = $(".swiper-slide-next")






$(".slide-title h2").css("animation", "tracking-in-contract-bck 1s cubic-bezier(0.215, 0.61, 0.355, 1) both");



// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
var swiper = new Swiper(".mySwiper", {
    spaceBetween: 30,
    centeredSlides: true,
    autoplay: {
        delay: 2400,
        disableOnInteraction: false,
    },
    pagination: {
        el: ".swiper-pagination",
        clickable: true,
    },
    navigation: {
        nextEl: ".swiper-button-next",
        prevEl: ".swiper-button-prev",
    },
});




let value = 1;
const price = 2.24;
$(".hesabla").keyup(function () {
    for (let i = 1; i <= 9; i++) {
        console.log(`Value: ${value}, Price: $${price}`);
        value++;
        price += 2.24;
    }
})


// !Calculater


function calculatePrice() {
    const country = document.getElementById("country").value;
    const input = document.getElementById("input");
    const result = document.getElementById("result");
    let price;
    if (input.value == "") {
        result.innerHTML = `Mohtesem Endirimle : $0`
    }
    if (country === "USA") {
        price = 7.24;
    } else if (country === "TURK") {
        price = 3.39;
    } else {
        result.textContent = "";
        return;
    }


    if (!isNaN(input.value) && input.value !== "") {
        const value = parseInt(input.value);
        const totalPrice = value * price;
        const roundedPrice = Number.parseFloat(totalPrice).toFixed(2);

        if (country === "USA") {
            result.textContent = `Mohtesem Endirimle : $${roundedPrice}`;


        }
        else if (country === "TURK") {
            result.textContent = `Mohtesem Endirimle : ₺${roundedPrice}`;

        }
        if (value > 30) {
            result.textContent = "Max çəki: 30kq!"
        }



    } else {
        result.textContent = "";
    }




}



// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!! tariflerrrrrrrrrrrrrrr

$(".ABSSSS").hide()
$(".DaxiliKarqo").hide()
$(".daxilikargooo").click(function () {
    $(".Turkiyeactive").hide()
    $(".ABSSSS").hide()
    $(".DaxiliKarqo").show()
})
$(".usakargo").click(function () {
    $(".Turkiyeactive").hide()
    $(".ABSSSS").show()
    $(".DaxiliKarqo").hide()
})
$(".turkeykargo").click(function () {
    $(".Turkiyeactive").show()
    $(".ABSSSS").hide()
    $(".DaxiliKarqo").hide()
})

