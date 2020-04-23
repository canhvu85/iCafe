function showErrorbyAlert(n, t) {
    swal({
        title: n,
        text: t,
        showCancelButton: !1,
        confirmButtonText: "OK",
        html: !0,
        type: 'error',
        closeOnConfirm: !0
    })
}

function showSuccessbyAlert(n, t) {
    swal({
        title: n,
        text: t,
        showCancelButton: !1,
        confirmButtonText: "OK",
        html: !0,
        type: 'success',
        closeOnConfirm: !0
    })
}