$(document).ready(function () {
    getCityRegister();

});

function getCityRegister() {
    $.ajax({
        url: "/api/mobile/CitiesAPI",
        method: "GET",
        dataType: "json",
        contentType: "application/json"
    }).done(function (data) {
        let k1 = data.length;
        let str1 = '';
        let selected1 = ' selected';
        for (let i = 0; i < k1; i++) {
            var st = data[i].name;
            str1 += `<span id=c${data[i].id} class="custom-option ${selected1}" data-value="${data[i].id}" onclick="changeCityRegister(${data[i].id},'${data[i].name}')">${data[i].name}</span>`;
            selected1 = '';
        }
        $("#register .city .custom-options").html(str1);
    });
}

function changeCityRegister(cityId, cityName) {
    cityName += "";
    $("#register .city .custom-option.selected").removeClass("selected");
    $("#register #c" + cityId).addClass("selected");
    $("#register .city .custom-select__trigger span").html(cityName);
    $("#register .shop .custom-select__trigger span").html("Chọn tên quán");
    $("#register .shop .custom-options").html("");
    //let cityId = parseInt($(this).data('value'));
    // console.log($(this).data('value'));
    $.ajax({
        url: "/api/ShopsApi/?city_id=" + cityId,
        method: "GET",
        dataType: "json",
        contentType: "application/json"
    }).done(function (data) {
        // readTextFile(data, function (text) {
        //let data = JSON.parse(text);
        let k = data.length;
        let str = '';
        // let selected = '';
        for (let i = 0; i < k; i++) {
            str += `<span class="custom-option" onclick="changeShopRegister(this,'${data[i].name}');" data-value="${data[i].id}">${data[i].name}</span>`;
            // selected = '';
        }
        $("#register .shop .custom-options").html(str);
        //});
    });

};

function changeShopRegister(elem, name) {
    $("#register .shop .custom-option .selected").removeClass("selected");
    $("#register .shop .custom-select__trigger span").html(name);
    elem.classList.add("selected");
}

$("#register").submit(function (event) {
    event.preventDefault();
    if (!$(this).valid()) return false;
    register();
});

function register() {
    let un = $("#usernameRegister").val();
    let pwd = $("#passwordRegister").val();
    // let city_id = $("#city .selected").data("value");
    let shop_id = $("#register .shop .selected").data("value");
    // shop_id += "";
    let data_user = {
        "username": un,
        "password": pwd,
        "ShopsId": shop_id
    };
    console.log(data_user);
    // console.log(data);
    $.ajax({
        url: "/api/mobile/UsersApi/mobile/userregister", // endpoint
        type: "POST",
        dataType: "json",
        contentType: "application/json",
        data: JSON.stringify(data_user)
    }).done(function (data) {
        swal({
            title: "Thông báo!",
            text: "Đăng ký tài khoản thành công!",
            type: "success",
            confirmButtonText: "OK"
        }, function () {
            $("#username").val(data_user.username);
            $("#password").val(data_user.password);
            $(".login-btn").click();
            $("#login .city .custom-option").each(function () {
                if ($(this).data("value") == $("#register .city .selected").data("value")) {
                    $(this).click();
                    $("#login .city .selected").click();
                    //changeCity($(this).data("value"), $("#register .city .custom-select__trigger span").html());
                    //$("#login .city .custom-option .selected").removeClass("selected");
                   // $(this).addClass("selected");
                    //$("#login .city .custom-select__trigger span").html($("#register .city .custom-select__trigger span").html());
                    $(document).ajaxStop(function () {
                        $("#login .shop .custom-option").each(function () {
                            if ($(this).data("value") == $("#register .shop .selected").data("value")) {
                                $(this).click();
                                //changeShop($(this), $("#register .shop .custom-select__trigger span").html());
                                $('#login .shop .custom-select-wrapper').click();
                            }
                        });
                    });

                }
            });
        });
        return false;
    }).fail(function (xhr, status, error) {
        showErrorbyAlert('Cảnh báo', xhr.responseText);
        //swal({
        //    type: 'error',
        //    title: 'Cảnh báo',
        //    text: xhr.responseText
        //});
    });
}

$("#register").validate({
    rules: {
        usernameRegister: {
            required: true,
            minlength: 2
        },
        passwordRegister: {
            required: true
        }
    },
    messages: {
        usernameRegister: {
            required: "Vui lòng nhập tài khoản",
            minlength: "Phải nhập 3 ký tự trở lên"
        },
        passwordRegister: {
            required: "Vui lòng nhập mật khẩu"
        }
    }
});