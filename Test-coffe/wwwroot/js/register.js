$(document).ready(function () {
    getCityRegister();

});

function getCityRegister() {
    axios({
        url: GetCity,
        method: "GET"
    }).then(function (response) {
        if (response.data.length > 0) {
            $.each(response.data, function (index, value) {
                $("#register .city .custom-options").append(
                    "<span class='custom-option' data-value='" + value.id + "'>" + value.name + "</span>"
                );
            });

            $("#register .city .custom-option").on("click", function () {
                let city_name = $(this).html();
                let city_id = $(this).data("value");
                $("#register .city .custom-select__trigger span").html(city_name);
                $("#register .city .custom-option.selected").removeClass("selected");
                this.classList.add("selected");
                $("#register .shop .custom-select__trigger span").html("Chọn tên quán");
                $("#register .shop .custom-options").html("");
                //let cityId = $(this).data('value');
                axios({
                    url: GetShop + "/cities/" + city_id,
                    method: "GET"
                }).then(function (response) {
                    if (response.data.length > 0) {
                        let k = response.data.length;
                        let str = '<span class="custom-option selected" style="display: none;"></span>';
                        for (let i = 0; i < k; i++) {
                            str += '<span class="custom-option" onclick="changeShopRegister(this,\'' + response.data[i].name + '\');" data-value="' + response.data[i].id + '">' + response.data[i].name + '</span>';
                        }
                        $("#register .shop .custom-options").html(str);
                    }
                });
            });
        }
    });
}


$("#register").submit(function (event) {
    event.preventDefault();
    if (!$(this).valid()) return false;
    register();
});

function register() {
    let users = {
        username: $("#usernameRegister").val(),
        password: $("#passwordRegister").val(),
        ShopsId: $("#register .shop .selected").data("value")
    }
    axios({
        url: "/Login/RegisterForm",
        method: "POST",
        headers: { 'content-type': 'application/json' },
        data: JSON.stringify(users)
    }).then(function (response) {
        if (response.status == 200) {
            showErrorbyAlert(response.data)

        } else if (response.status == 201) {
            showSuccessbyAlert("Đăng ký tài khoản thành công!").then(() => {
                $("#username").val(users.username);
                $("#password").val(users.password);
                $(".login-btn").click();
                $("#login .city .custom-option").each(function () {
                    if ($(this).data("value") == $("#register .city .selected").data("value")) {
                        $(this).click();
                        $(document).ajaxStop(function () {
                            $("#login .shop .custom-option").each(function () {
                                if ($(this).data("value") == $("#register .shop .selected").data("value")) {
                                    $(this).click();
                                    $('#login .shop .custom-select-wrapper').click();
                                }
                            });
                        })

                    }
                });

                return false;
            })
        }
    })
}

$("#register").validate({
    rules: {
        usernameRegister: {
            required: true,
            minlength: 3
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
