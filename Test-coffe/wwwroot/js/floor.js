

//$(document).ready(function () {
//    displayItems();
//});

//var hdnUserSession = $("#hdnUserSession").data("value");
//var shopId = hdnUserSession.ShopsId;

var UserSession = JSON.parse(sessionStorage.getItem('user'));
var shopId = UserSession.ShopsId*1;

function addItem(item) {
    if (item.trim().length > 0) {
        //arr.push(item.trim());
        let newData = {
            "name": item.trim(),
            "permalink": toSlug(item.trim()),
            'shopsId': shopId,
            "created_by": UserSession.username
        };
        axios({
            url: "/api/mobile/FloorsApi",
            method: "post",
            dataType: "json",
            headers: { 'Content-Type': "application/json"},
            data: JSON.stringify(newData)
        }).then(function (response) {
            if (response.status == 200) {
                Swal.fire({
                    icon: 'error',
                    title: 'Cảnh báo',
                    text: response.data
                });
            } else {
                $("#formCreate")[0].reset();
                Swal.fire({
                    position: 'top-end',
                    icon: 'success',
                    title: 'Tạo mới thành công',
                    showConfirmButton: false,
                    timer: 1500
                })
                displayItems(shopId);
            }
        }).catch(function () {
            Swal.fire({
                icon: 'error',
                title: 'Cảnh báo',
                text: 'Đã xảy ra lỗi!',
            });
        });
        // displayItems($("#FilterFloor").val());
    }
    else {
        $("#item").select();
    }
}

var listFloor = [];
displayItems(shopId);

function displayItems(shop_id) {
    shop_id *= 1;
    var st = "/api/mobile/FloorsApi/";
    if (shop_id != "") {
        st += "?shop_id=" + shop_id;
    }

    axios({
        url: st,
        method: "GET",
        dataType: "json",
       // async: false,
        headers: { 'Content-Type': 'application/json' }
    }).then(function (response) {
        listFloor = response.data;
        let data = response.data;
        let text = '';
        for (let i = 0; i < data.length; i++) {
            text += `<tr>`;
            text += `<th scope="row">${i + 1}</th>`;
            text += `<td id="td${data[i].id}"><span id="span${data[i].id}">${data[i].name}</span></td>`;          
            text += `<td id="tdEdit${data[i].id}"><button id="btnEdit${data[i].id}" onclick="editItem(${data[i].id},'${data[i].name}');" class="btn btnEdit"><i class="fa fa-edit edit-btn"></i>Sửa</button></td>`;
            text += `<td><button id="btnDel${data[i].id}" onclick="deleteItem(${data[i].id})" class="btn btnDel"><i class="fa fa-trash-alt delete-btn"></i>Xóa</button></td>`;
            text += `</tr>`;
        }

        //document.getElementById('countItem').innerHTML = data.length + " bàn";
        //if (data.length > 1) {
        //    document.getElementById('countItem').innerHTML += "s";
        //}
        document.getElementById("tbody").innerHTML = text;
    });
}

function editItem(tdid, val) {
    tdid *= 1;
    var input, container = document.getElementById("td" + tdid);
    var form = document.createElement("form");

    form.id = 'formEdit';
    form.noValidate = 'novalidate';
    form.classList = 'card-body';
    container.appendChild(form);

    input = document.createElement("input");
    input.type = "text";
    input.id = 'editItem';
    input.name = "editItem";
    input.classList = "form-control col-sm-9 mr-3 bg-success text-white";
    input.value = val;
    form.appendChild(input);

    var tdEdit = document.getElementById("tdEdit" + tdid);
    var btnChange = document.createElement("button")
    btnChange.classList = "btn btn-primary bg-info border-0 mr-3";
    btnChange.innerHTML = 'Lưu';
    btnChange.type = "submit";

    abc();
    $("#formEdit").submit(function (event) {
        event.preventDefault();
       // jQuery.noConflict();
        if (!$("#formEdit").valid()) return false;
   // btnChange.onclick = function () {
        //arr[tdid] = input.value;
        var newData = {
            'id': tdid,
            'name': input.value.trim(),
            'permalink': toSlug(input.value.trim()),   
            'shopsId': shopId,
            'updated_by': UserSession.username
        }
        axios({
            url: "/api/mobile/FloorsApi/" + tdid,
            method: "PUT",
            dataType: "json",
           // async: false,
            headers: { 'Content-Type': "application/json" },
            data: JSON.stringify(newData)
        }).catch(function (error) {
           // $("#showEdit").modal("hide");
            Swal.fire({
                icon: 'error',
                title: 'Cảnh báo',
                text: 'Đã xảy ra lỗi!',
            });
        }).then(function (result) {
            if (result.status == 200) {
                Swal.fire({
                    icon: 'error',
                    title: 'Cảnh báo',
                    text: result.data
                });
            } else
                if (result.status == 204) {
                    Swal.fire({
                        position: 'top-end',
                        icon: 'success',
                        title: 'Sửa thông tin thành công',
                        showConfirmButton: false,
                        timer: 1500
                    })
                    $(btnChange).remove();
                    $(btnCancel).remove();
                    $(".btnEdit").css("visibility", 'visible');
                    $(".btnDel").css("visibility", 'visible');
                    $(input).remove();
                    $("#span" + tdid).show();

                    displayItems(shopId);
                }
        });
    //};
    });
    form.appendChild(btnChange);

    var btnCancel = document.createElement("button");
    btnCancel.classList = "btn btn-primary bg-secondary border-0";
    btnCancel.innerHTML = "Đóng";
    btnCancel.onclick = function () {
        $(btnChange).remove();
        $(btnCancel).remove();
        $(".btnEdit").css("visibility", 'visible');
        $(".btnDel").css("visibility", 'visible');
        $(input).remove();
        $("#span" + tdid).show("slow");

        displayItems(shopId);
    };
    form.appendChild(btnCancel);

    $("#span" + tdid).hide();   
    $(".btnEdit").css("visibility", 'hidden');
    $(".btnDel").css("visibility", 'hidden');
}

function deleteItem(id) {
    //var conf = confirm("Are you sure you want to delete \"" + arr[i] + "\" product?");
    //if (conf) {
    //    arr.splice(i, 1);
    //    displayItems();
    //}
    Swal.fire({
        title: 'Bạn chắc chắn muốn xóa tầng này?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#3085d6',
        confirmButtonText: 'Vâng, xóa nó đi!',
        cancelButtonText: 'Hủy',
    }).then((result) => {
        if (result.value) {
            axios({
                url: '/api/mobile/FloorsApi/del/' + parseInt(id) + "/?name=" + UserSession.username,
                method: 'put',
                headers: { 'Content-Type': 'application/json' }
            }).then(function (response) {
                if (response.status == 200) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Cảnh báo',
                        text: response.data
                    });
                }else
                    displayItems(1);
            }).catch(function (error) {
                        Swal.fire({
                            icon: 'error',
                            title: 'Cảnh báo',
                            text: 'Đã xảy ra lỗi'
                        });
                 });
        }
    });
}

$("#formCreate").submit(function (event) {
    event.preventDefault();
  //  jQuery.noConflict();
    if (!$(this).valid()) return false;
    addItem(item.value);
    //item.value = null;
});

$("#formCreate").validate({
    rules: {
        item: {
            required: true,
            normalizer: function (value) {
                return $.trim(value);
            },
            minlength: 3
        }
    },
    messages: {
        item: {
            required: "Vui lòng nhập tên tầng",
            minlength: "Phải nhập 3 ký tự trở lên"
        }
    }
});


function abc() {
    $("#formEdit").validate({
        rules: {
            editItem: {
                required: true,
                normalizer: function (value) {
                    return $.trim(value);
                },
                minlength: 3
            }
        },
        messages: {
            editItem: {
                required: "Vui lòng nhập tên tầng",
                minlength: "Phải nhập 3 ký tự trở lên"
            }
        },
        errorPlacement: function (error, element) {
            error.insertAfter(element);
        }
    });

}