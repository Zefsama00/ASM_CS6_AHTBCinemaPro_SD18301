
window.MuaVe = function () {
    $(document).ready(function () {
        $('#cinema-info').click(function () {
            $('#showtimes').toggle();
        });

        $('.time-slot').click(function (event) {
            event.preventDefault();
            var gioChieuId = $(this).data('id');
            var phimId = $(this).data('phim-id');
            loadSeats(gioChieuId, phimId);
        });

        function loadSeats(gioChieuId, phimId) {
            $.ajax({
                url: '/Multimodel/LoadSeats', // Đường dẫn tuyệt đối hoặc tương đối phù hợp với ứng dụng của bạn
                type: 'GET',
                data: { id: gioChieuId, gioChieuId: gioChieuId }, // Truyền thêm gioChieuId vào data
                success: function (data) {
                    console.log('AJAX success:', data); // Ghi lại phản hồi
                    var seatContainer = $("#seat-container");
                    seatContainer.empty();
                    if (data && data.length > 0) {
                        $.each(data, function (index, seat) {
                            var seatHtml = '<div class="col-1 m-3 d-flex align-items-center justify-content-center">' +
                                '<a class="btn btn-outline-primary" href="/Multimodel/ThanhToan/' + seat.id + '?idphim=' + phimId + '&gioChieuId=' + gioChieuId + '&username=' + 'nn' + '">' + seat.name + '</a>' +
                                '</div>';

                            seatContainer.append(seatHtml);
                        });
                    } else {
                        seatContainer.append('<p>Không có ghế nào.</p>');
                    }
                },
                error: function (xhr, status, error) {
                    console.error('Lỗi AJAX:', xhr.responseText, status, error);
                    alert('Không tải được ghế. Vui lòng kiểm tra console để biết thêm chi tiết.');
                }
            });
        }
    });
};
