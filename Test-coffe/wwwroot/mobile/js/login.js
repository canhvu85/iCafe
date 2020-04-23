
$(document).ready(function () {
    getCity();

}); 

var user = JSON.parse(localStorage.getItem('user'));
// console.log(user);

$('#day_open').datepicker({
    dateFormat: "dd/mm/yy",
    // maxDate: "+30d",
    minDate: "+1d",
    prevText: "Trước",
    nextText: "Sau",
    currentText: "Hôm nay",
    monthNames: ["Tháng một", "Tháng hai", "Tháng ba", "Tháng tư", "Tháng năm", "Tháng sáu", "Tháng bảy", "Tháng tám", "Tháng chín", "Tháng mười", "Tháng mười một", "Tháng mười hai"],
    monthNamesShort: ["Một", "Hai", "Ba", "Bốn", "Năm", "Sáu", "Bảy", "Tám", "Chín", "Mười", "Mười một", "Mười hai"],
    dayNames: ["Chủ nhật", "Thứ hai", "Thứ ba", "Thứ tư", "Thứ năm", "Thứ sáu", "Thứ bảy"],
    dayNamesShort: ["CN", "Hai", "Ba", "Tư", "Năm", "Sáu", "Bảy"],
    dayNamesMin: ["CN", "T2", "T3", "T4", "T5", "T6", "T7"],
    weekHeader: "Tuần",
    dateFormat: "dd/mm/yy",
    firstDay: 1,
    showMonthAfterYear: false,
});

function getCity() {
    $.ajax({
        url: "/api/mobile/CitiesAPI",
        method: "GET",
        dataType: "json",
        contentType: "application/json"
    }).done(function (data) {
        //  $("#city .custom-options").html("");
        // readTextFile(data, function (text) {
        //let data = JSON.parse(text);
        let k = data.length;
        let str = '';
        //let selected = ' selected';
        for (let i = 0; i < k; i++) {
            var st = data[i].name;
            str += `<span id=c${data[i].id} class="custom-option" data-value="${data[i].id}" onclick="changeCity(${data[i].id},'${data[i].name}')">${data[i].name}</span>`;
            // selected = '';

        }
        $("#login .city .custom-options").html(str);
        //});
    });
   
}

function changeCity(cityId, cityName) {
    cityName += "";
    $("#login .city .custom-option .selected").removeClass("selected");
    $("#login #c" + cityId).addClass("selected");
    $("#login .city .custom-select__trigger span").html(cityName);
    $("#login .shop .custom-select__trigger span").html("Chọn tên quán");
    $("#login .shop .custom-options").html("");
    //let cityId = parseInt($(this).data('value'));
    // console.log($(this).data('value'));
    $.ajax({
        url: "/api/mobile/ShopsApi/?city_id=" + cityId,
        method: "GET",
        dataType: "json",
        contentType: "application/json"
    }).done(function (data) {
        // readTextFile(data, function (text) {
        //let data = JSON.parse(text);
        let k = data.length;
        let str = '';
        let selected = '';
        for (let i = 0; i < k; i++) {
            str += `<span class="custom-option ${selected}" onclick="changeShop(this,'${data[i].name}');" data-value="${data[i].id}">${data[i].name}</span>`;
            selected = '';
        }
        $("#login .shop .custom-options").html(str);
        //});
    });
};

function changeShop(elem, name) {
    $("#login .shop .custom-option.selected").removeClass("selected");
    $("#login .shop .custom-select__trigger span").html(name);
    elem.classList.add("selected");
}


$("#login").submit(function (event) {
    event.preventDefault();
    if (!$(this).valid()) return false;
    login();
});

function login() {
    localStorage.clear();
    let un = $("#username").val();
    let pwd = $("#password").val();
    // let city_id = $("#city .selected").data("value");
    let shop_id = $("#login .shop .selected").data("value");
    // shop_id += "";
    let data_user = JSON.stringify({
        "username": un,
        "password": pwd,
        "ShopsId": shop_id
    });
    console.log(data_user);
    // console.log(data);
    $.ajax({
        url: "/api/mobile/UsersApi/mobile/userlogin", // endpoint
        type: "POST",
        dataType: "json",
        contentType: "application/json",
        data: data_user
    }).done(function (result) {
        // success
        console.log(result.name);
        showSuccessbyAlert("Thông báo!", "Đăng nhập thành công!")
        //swal({
        //    title: "Thông báo!",
        //    text: "Đăng nhập thành công!",
        //    type: "success",
        //    confirmButtonText: "OK"
        //});
        localStorage.setItem('user', data_user);
        setTimeout(function () {
            window.location.replace("/mobile/index");
        }, 2000);
    }).fail(function (errorData) {
        //onError(errorData);
        showErrorbyAlert('Cảnh báo', 'Đăng nhập không thành công!');
        //swal({
        //    icon: 'error',
        //    title: 'Cảnh báo',
        //    text: 'Đăng nhập không thành công!'
        //})
    });

}

$("#login").validate({
    rules: {
        username: {
            required: true,
            minlength: 2
        },
        password: {
            required: true
        }
    },
    messages: {
        username: {
            required: "Vui lòng nhập tài khoản",
            minlength: "Phải nhập 3 ký tự trở lên"
        },
        password: {
            required: "Vui lòng nhập mật khẩu"
        }
    }
});