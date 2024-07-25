document.getElementById('next1').onclick = function () {
    let lists = document.querySelectorAll('.item1');
    document.getElementById('slide1').appendChild(lists[0]);
}
document.getElementById('prev1').onclick = function () {
    let lists = document.querySelectorAll('.item1');
    document.getElementById('slide1').prepend(lists[lists.length - 1]);
}
document.addEventListener('DOMContentLoaded', () => {
    document.querySelectorAll('.slider').forEach(slider => {
        const slides = slider.querySelectorAll('.item');
        const prevButton = slider.closest('.slider-wrapper').querySelector('.prev');
        const nextButton = slider.closest('.slider-wrapper').querySelector('.next');
        let currentIndex = 0;

        function showSlide(index) {
            slides.forEach((slide, i) => {
                // Dịch chuyển từng slide dựa trên chỉ số hiện tại
                slide.style.transform = `translateX(${(i - index) * 100}%)`;
            });
        }

        function nextSlide() {
            currentIndex = (currentIndex + 1) % slides.length;
            showSlide(currentIndex);
        }

        function prevSlide() {
            currentIndex = (currentIndex - 1 + slides.length) % slides.length;
            showSlide(currentIndex);
        }

        // Hiển thị slide đầu tiên
        showSlide(currentIndex);

        // Xử lý sự kiện nút tiếp theo và trước
        if (nextButton) {
            nextButton.addEventListener('click', nextSlide);
        }

        if (prevButton) {
            prevButton.addEventListener('click', prevSlide);
        }
    });
});



