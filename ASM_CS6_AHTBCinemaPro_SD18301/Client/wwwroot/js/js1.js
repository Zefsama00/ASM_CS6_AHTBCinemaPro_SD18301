document.getElementById('next1').onclick = function () {
    let lists = document.querySelectorAll('.item1');
    document.getElementById('slide1').appendChild(lists[0]);
}
document.getElementById('prev1').onclick = function () {
    let lists = document.querySelectorAll('.item1');
    document.getElementById('slide1').prepend(lists[lists.length - 1]);
}
window.initOwlCarousel = () => {
    $(document).ready(function () {

        $('#top-movies-slide').owlCarousel({
            items: 2,
            dots: false,
            loop: true,
            autoplay: true,
            autoplayHoverPause: true,
            responsive: {
                500: {
                    items: 3
                },
                1280: {
                    items: 4
                },
                1600: {
                    items: 6
                }
            }
        });
        $('.movies-slide').owlCarousel({
            loop: true,
            margin: 10,
            nav: true,
            responsive: {
                0: {
                    items: 1
                },
                600: {
                    items: 2
                },
                1000: {
                    items: 4
                }
            }
        });
    });
};