//function showErrorbyAlert(n, t) {
//    swal({
//        title: n,
//        text: t,
//        showCancelButton: !1,
//        confirmButtonText: "OK",
//        html: !0,
//        type: 'error',
//        closeOnConfirm: !0
//    })
//}

//function showSuccessbyAlert(n, t) {
//    swal({
//        title: n,
//        text: t,
//        showCancelButton: !1,
//        confirmButtonText: "OK",
//        html: !0,
//        type: 'success',
//        closeOnConfirm: !0
//    });
//}

//function showSuccessbyAlertThen(n, t, f) {
//    swal({
//        title: n,
//        text: t,
//        showCancelButton: !1,
//        confirmButtonText: "OK",
//        html: !0,
//        type: 'success',
//        closeOnConfirm: !0
//    }, function () {
//        f();
//    });
//}

//function showWarningbyAlert(n, f) {
//    swal({
//        title: n,
//        type: "warning",
//        showCancelButton: true,
//        confirmButtonColor: '#d33',
//        cancelButtonColor: '#3085d6',
//        confirmButtonText: 'Vâng, xóa nó đi!',
//        cancelButtonText: 'Hủy'
//    }, function () {
//        f();
//    });
//}

function showSuccessbyAlert(t) {
    return Swal.fire({
        position: 'center-middle',
        icon: 'success',
        title: t,
        showConfirmButton: false,
        timer: 1500
    })
}

function showEditSuccessbyAlert(t) {
    Swal.fire({
        position: 'top-end',
        icon: 'success',
        title: t,
        showConfirmButton: false,
        timer: 1500
    })
}

function showErrorbyAlert(t) {
    return Swal.fire({
        icon: 'error',
        text: t
    })
}

function showWarningbyAlert(t) {
    return Swal.fire({
        text: t,
        icon: 'question',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Vâng, xóa nó đi!',
        cancelButtonText: 'Hủy'
    })
}

function loadImageError(elem) {
    $(elem).attr("src", "/images/product-not-available-96x96.png");
}

function loadImageErrorCp(elem) {
    $(elem).attr("src", "/images/no-image-available.png");
}

function toSlug(str) {
    // Chuyển hết sang chữ thường
    str = str.toLowerCase();

    // xóa dấu
    str = str
        .normalize('NFD') // chuyển chuỗi sang unicode tổ hợp
        .replace(/[\u0300-\u036f]/g, ''); // xóa các ký tự dấu sau khi tách tổ hợp

    // Thay ký tự đĐ
    str = str.replace(/[đĐ]/g, 'd');

    // Xóa ký tự đặc biệt
    str = str.replace(/([^0-9a-z-\s])/g, '');

    // Xóa khoảng trắng thay bằng ký tự -
    str = str.replace(/(\s+)/g, '-');

    // Xóa ký tự - liên tiếp
    str = str.replace(/-+/g, '-');

    // xóa phần dư - ở đầu & cuối
    str = str.replace(/^-+|-+$/g, '');

    // return
    return str;
}

function sendMessage(message) {
    $(".alert, .alert-success").show();
    $(".alert, .alert-success").html(message);
    $(".alert, .alert-success").fadeTo(2000, 500).slideUp(500, function () {
        $(".alert, .alert-success").slideUp(2000);
    });
}

function unAuthorized() {
    console.log("Unauthorized");
    axios({
        url: LogOut,
        method: "POST",
        headers: { 'content-type': 'application/json' },
        data: JSON.stringify({
            id: parseInt(user.id),
            updated_by: user.username
        })
    }).then(function () {
        sessionStorage.clear();
        showErrorbyAlert("Bạn không có quyền thực hiện hành động này").then((result) => {
            if (result.value) {
                window.location.replace("/");
            }
        });
    }).catch(function () {
        alert("loi");
    })
}