document.addEventListener('DOMContentLoaded', function () {
    const scrollHolder = document.querySelector('.scroll-holder');
    let isDown = false;
    let startX;
    let scrollLeft;

    scrollHolder.addEventListener('mousedown', (e) => {
        isDown = true;
        scrollHolder.classList.add('active');
        startX = e.pageX - scrollHolder.offsetLeft;
        scrollLeft = scrollHolder.scrollLeft;
    });

    scrollHolder.addEventListener('mouseleave', () => {
        isDown = false;
        scrollHolder.classList.remove('active');
    });

    scrollHolder.addEventListener('mouseup', () => {
        isDown = false;
        scrollHolder.classList.remove('active');
    });

    scrollHolder.addEventListener('mousemove', (e) => {
        if (!isDown) return;
        e.preventDefault();
        const x = e.pageX - scrollHolder.offsetLeft;
        const walk = (x - startX) * 2; //scroll-fast
        scrollHolder.scrollLeft = scrollLeft - walk;
    });
});
