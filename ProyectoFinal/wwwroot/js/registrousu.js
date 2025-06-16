 document.addEventListener('DOMContentLoaded', () => {
        let currentSlideInsertar = 0;
        const slidesInsertar = document.querySelectorAll('#formInsertar .form-slide');
        const prevBtnInsertar = document.querySelector('#formInsertar .prev');
        const nextBtnInsertar = document.querySelector('#formInsertar .next');

        function showSlideInsertar(index) {
            slidesInsertar.forEach((slide, i) => {
                slide.classList.toggle('active', i === index);
            });
        }

        prevBtnInsertar.addEventListener('click', () => {
            if (currentSlideInsertar > 0) {
                currentSlideInsertar--;
                showSlideInsertar(currentSlideInsertar);
            }
        });

        nextBtnInsertar.addEventListener('click', () => {
            if (currentSlideInsertar < slidesInsertar.length - 1) {
                currentSlideInsertar++;
                showSlideInsertar(currentSlideInsertar);
            }
        });

        showSlideInsertar(currentSlideInsertar);
    });
