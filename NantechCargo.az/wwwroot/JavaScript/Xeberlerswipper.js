//  !!!!Swipper2

const sectionsWithCarousel = document.querySelectorAll(
    ".section-with-carousel"
);

createOffsets();
window.addEventListener("resize", createOffsets);

function createOffsets() {
    const sectionWithLeftOffset = document.querySelector(
        ".section-with-left-offset"
    );
    const sectionWithLeftOffsetCarouselWrapper = sectionWithLeftOffset.querySelector(
        ".carousel-wrapper"
    );
    const sectionWithRightOffset = document.querySelector(
        ".section-with-right-offset"
    );
    const sectionWithRightOffsetCarouselWrapper = sectionWithRightOffset.querySelector(
        ".carousel-wrapper"
    );
    const offset = (window.innerWidth - 1100) / 2;
    const mqLarge = window.matchMedia("(min-width: 1200px)");

    if (sectionWithLeftOffset && mqLarge.matches) {
        sectionWithLeftOffsetCarouselWrapper.style.marginLeft = offset + "px";
    } else {
        sectionWithLeftOffsetCarouselWrapper.style.marginLeft = 0;
    }

    if (sectionWithRightOffset && mqLarge.matches) {
        sectionWithRightOffsetCarouselWrapper.style.marginRight = offset + "px";
    } else {
        sectionWithRightOffsetCarouselWrapper.style.marginRight = 0;
    }
}

for (const section of sectionsWithCarousel) {
    let slidesPerView = [1.5, 2.5, 3.5];
    if (section.classList.contains("section-with-left-offset")) {
        slidesPerView = [1.5, 1.5, 2.5];
    }
    const swiper = section.querySelector(".swiper");
    new Swiper(swiper, {
        slidesPerView: slidesPerView[0],
        spaceBetween: 15,
        loop: true,
        lazyLoading: true,
        keyboard: {
            enabled: true
        },
        navigation: {
            prevEl: section.querySelector(".carousel-control-left"),
            nextEl: section.querySelector(".carousel-control-right")
        },
        pagination: {
            el: section.querySelector(".swiper-pagination"),
            clickable: true,
            renderBullet: function (index, className) {
                return `<div class=${className}>
              <span class="number">${index + 1}</span>
              <span class="line"></span>
          </div>`;
            }
        },
        breakpoints: {
            768: {
                slidesPerView: slidesPerView[1]
            },
            1200: {
                slidesPerView: slidesPerView[2]
            }
        }
    });
}



