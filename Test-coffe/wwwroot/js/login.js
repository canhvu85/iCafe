$(document).ready(function () {
    getCity();

});

function getCity() {
    axios({
        url: GetCity,
        method: "GET"
    }).then(function (response) {
        if (response.data.length > 0) {
            $.each(response.data, function (index, value) {
                $("#login .city .custom-options").append(
                    `<span class='custom-option' data-value='${value.id}'>${value.name}</span>`
                );
            });

            $("#login .city .custom-option").on("click", function () {
                let city_name = $(this).html();
                let city_id = $(this).data("value");
                $("#login .city .custom-select__trigger span").html(city_name);
                $("#login .city .custom-option.selected").removeClass("selected");
                this.classList.add("selected");
                $("#login .shop .custom-select__trigger span").html("Chọn tên quán");
                $("#login .shop .custom-options").html("");
                //let cityId = $(this).data('value');
                axios({
                    url: GetShop + "/cities/" + city_id,
                    method: "GET"
                }).then(function (response) {
                    if (response.data.length > 0) {
                        let k = response.data.length;
                        let str = '<span class="custom-option selected" style="display: none;"></span>';
                        for (let i = 0; i < k; i++) {
                            str += `<span class="custom-option" onclick="changeShop(this,'${response.data[i].name}');" data-value="${response.data[i].id}">${response.data[i].name}</span>`;
                        }
                        $("#login .shop .custom-options").html(str);
                    }
                });
            });
        }
    });
}

$("#login").submit(function (event) {
    event.preventDefault();
    if (!$(this).valid()) return false;
    login();
});

function login() {
    let users = {
        username: $("#username").val(),
        password: $("#password").val(),
        ShopsId: $("#login .shop .selected").data("value")
    }
    axios({
        url: LoginForm,
        method: "POST",
        headers: { 'content-type': 'application/json' },
        data: JSON.stringify(users)
    }).then(function (response) {
        console.log(response);
        console.log(response.data);
        if (response.status == 200 || response.status == 202) {
            showErrorbyAlert(response.data);
        } else if (response.status == 201) {
            showSuccessbyAlert('Đăng nhập hệ thống thành công.')
            //localStorage.setItem('user', JSON.stringify(response.data[0]));
            var user = jwt_decode(response.data);
            user.remember_token = response.data;
            localStorage.setItem('user', JSON.stringify(user));
            setTimeout(function () {
                window.location.replace("/cashier");
            }, 2000);
        }
    }).catch(function (response) {
        console.log(response);
        showErrorbyAlert('kiem tra mang!')
    });
}

$("#login").validate({
    rules: {
        username: {
            required: true,
            minlength: 3
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