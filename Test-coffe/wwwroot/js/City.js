//$("#formCreate").submit(function (event) {
//    event.preventDefault();
//    if (!$(this).valid()) return false;
//    user.save();
//});

//$("#formEdit").submit(function (event) {
//    event.preventDefault();
//    if (!$(this).valid()) return false;
//    user.edit();
//});

//$(document).ready(function () {
//    showList();
//});

//var user = user || {};

//user.openCreate = function () {
//    $("#showCreate").modal("show");
//    $("#name").val("");
//    $("#formCreate").valid();
//};

//user.save = function () {
//    var city = {
//        name: $("#name").val(),
//        permalink: toSlug($("#name").val())
//    }
//    createBtn(city);
//}

//function createBtn(city) {
//    $.ajax({
//        url: GetCity,
//        method: "POST",
//        dataType: "json",
//        contentType: "application/json",
//        data: JSON.stringify(city)
//    }).done(function (data) {
//        addFirst(data);
//        $("#showCreate").modal("hide");
//    }).fail(function (data) {
//        if (data.status == 400) {
//            console.log("loi 400")
//        } 
//    })
//}

//function addFirst(n) {
//    sendMessage("Đã tạo thành công city mới !");
//    $.ajax({
//        url: GetCity + "/" + n,
//        method: "GET",
//        dataType: "json",
//        contentType: "application/json"
//    }).done(function (data) {
//        drawPrepend(data);
//    });
//}

//function showList() {
//    $.ajax({
//        url: GetCity,
//        method: "GET",
//        dataType: "json",
//        contentType: "application/json"
//    }).done(function (data) {
//        $("#tbList").html("");
//        $.each(data, function (index, value) {
//            drawPrepend(value);
//        });
//    });
//}

//var idEdit;
//var rowIndex;
//var arr;
//user.openEdit = function (id, e) {
//    idEdit = id;
//    rowIndex = $(e).closest("tr").index();
//    getEditInfo(rowIndex);
//    $("#showEdit").modal("show");
//    $("#formEdit").valid();;
//};

//function getEditInfo(rowIndex) {
//    arr = [];
//    $("#tbList tr:eq(" + rowIndex + ") td").each(function () {
//        arr.push($(this).text());
//    });
//    $("#nameEdit").val(arr[0]);
//}

//user.edit = function () {
//    var city = {
//        id: idEdit,
//        name: $("#nameEdit").val(),
//        permalink: toSlug($("#nameEdit").val())
//    }
//    $("#showEdit").modal("hide");
//    editBtn(idEdit, city);
//};

//function editBtn(idEdit, city) {
//    $.ajax({
//        url: GetCity + "/" + idEdit,
//        method: "PUT",
//        dataType: "json",
//        contentType: "application/json",
//        data: JSON.stringify(city)
//    }).done(function () {
//        showSuccessbyAlert('Thông báo', 'Sửa thông tin thành công.');
//        $("#tbList tr:eq(" + rowIndex + ")").html(
//            "<td>" + city.name + "</td>" +
//            "<td class='btn-action'><a href='javascript:;' class='btn btn-block btn-primary btn-flat'  onclick='user.openEdit(" + idEdit + ",this)'><i class='fas fa-pencil-alt'></i> Edit</a></td>" +
//            "<td class='btn-action'><a href='javascript:;' class='btn btn-block btn-danger btn-flat' onclick='return deleteItem(" + idEdit + ")'><i class='fas fa-trash'></i> Delete</a></td>"
//        );
//    }).fail(function (data) {
//        if (data.status == 400) {
//            console.log("loi 400")
//        }
//    });
//}

//function deleteItem(car_id, e) {
//    rowIndex = $(e).closest("tr").index();
//    Swal.fire({
//        title: 'Bạn chắc chắn muốn xóa thông tin này?',
//        icon: 'warning',
//        showCancelButton: true,
//        confirmButtonColor: '#d33',
//        cancelButtonColor: '#3085d6',
//        confirmButtonText: 'Vâng, xóa nó đi!',
//        cancelButtonText: 'Hủy',
//    }).then((result) => {
//        if (result.value) {
//            $.ajax({
//                url: GetCity + "/" + car_id,
//                method: "DELETE",
//                dataType: "json",
//                contentType: "application/json"
//            }).done(function () {
//                $("#tbList tr:eq(" + rowIndex + ")").remove();
//            });
//        }
//    })
//}

//$("#formCreate").validate({
//    rules: {
//        name: {
//            required: true,
//            minlength: 3
//        }
//    },
//    messages: {
//        name: {
//            required: "Vui lòng nhập tên thành phố",
//            minlength: "Phải nhập 3 ký tự trở lên"
//        }
//    }
//});

//$("#formEdit").validate({
//    rules: {
//        nameEdit: {
//            required: true,
//            minlength: 3
//        }
//    },
//    messages: {
//        nameEdit: {
//            required: "Vui lòng nhập tên thành phố",
//            minlength: "Phải nhập 3 ký tự trở lên"
//        }
//    }
//});

//function drawPrepend(data) {
//    $("#tbList").prepend(
//        "<tr>" +
//        "<td>" + data.name + "</td>" +
//        "<td class='btn-action'><a href='javascript:;' class='btn btn-block btn-primary btn-flat'  onclick='user.openEdit(" + data.id + ",this)'><i class='fas fa-pencil-alt'></i> Edit</a></td>" +
//        "<td class='btn-action'><a href='javascript:;' class='btn btn-block btn-danger btn-flat' onclick='return deleteItem(" + data.id + ",this)'><i class='fas fa-trash'></i> Delete</a></td>" +
//        "</tr>"
//    );
//}


































//var hdnUserSession = $("#user").data("value");
let user = JSON.parse(localStorage.getItem('user'));

$(document).ready(function () {
    displayItems();
});

$("#formCreate").submit(function (event) {
    event.preventDefault();
    if (!$(this).valid()) return false;
    addItem(item.value);
    //item.value = null;
});

//axios.defaults.baseURL = 'https://localhost:5001/';

function displayItems() {
    axios({
        url: GetCity + "/withtoken",
        method: "GET",
        headers: {
            'content-type': 'application/json',
            'Authorization': user.remember_token
        }
    }).then(function (response) {
        $("#tbList").html("");
        let text = '';
        for (let i = 0; i < response.data.length; i++) {
            text += `<tr>
            <th scope="row" class="stt">${(i + 1)}</th>
            <td id="td${response.data[i].id}"><span id="span${response.data[i].id}">${response.data[i].name}</span></td>
            <td id="tdEdit${response.data[i].id}" class="btn-action2"><button id="btnEdit${response.data[i].id}" onclick="editItem(${response.data[i].id},'${response.data[i].name}');" class="btn btnEdit"><i class="fa fa-edit edit-btn"></i>Sửa</button></td>
            <td class="btn-action"><button id="btnDel${response.data[i].id}" onclick="deleteItem(${response.data[i].id})" class="btn btnDel"><i class="fa fa-trash-alt delete-btn"></i>Xóa</button></td>
            </tr>`;
        }
        $("#tbList").html(text);
    }).catch(function () {
        unAuthorized();
    });
}


function addItem(item) {
    if (item.trim().length > 0) {
        let newData = {
            "name": item.trim().replace(/([^0-9a-z-\s])/g, ''),
            "permalink": toSlug(item.trim())
        };
        axios({
            method: 'POST',
            url: GetCity,
            headers: {
                'content-type': 'application/json',
                'Authorization': user.remember_token
            },
            data: newData
        }).then(function () {
            $("#formCreate")[0].reset();
            showSuccessbyAlert('Tạo thành phố mới thành công.')
            displayItems();
        }).catch(function () {
            showErrorbyAlert('Đã xảy ra lỗi!')
            unAuthorized();
        });
    }
    else {
        $("#item").select();
    }
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
    input.id = 'editItem';
    input.name = "editItem";
    input.type = "text";
    input.classList = "form-control col-sm-9 mr-3 bg-success text-white";
    input.value = val;
    form.appendChild(input);

    var tdEdit = document.getElementById("tdEdit" + tdid);
    var btnChange = document.createElement("button")
    btnChange.classList = "btn btn-primary bg-info border-0 mr-3";
    btnChange.type = "submit";
    btnChange.innerHTML = 'Lưu';

    abc();
    $("#formEdit").submit(function (event) {
        event.preventDefault();
        if (!$("#formEdit").valid()) return false;
        // btnChange.onclick = function () {
        var newData = {
            'id': tdid,
            'name': input.value.trim().replace(/([^0-9a-z-\s])/g, ''),
            "permalink": toSlug(input.value.trim())
        }
        axios({
            method: 'PUT',
            url: GetCity + "/" + tdid,
            headers: {
                'content-type': 'application/json',
                'Authorization': user.remember_token
            },
            data: newData
        }).then(function () {
            showEditSuccessbyAlert('Sửa thông tin thành công.')
            $(btnChange).remove();
            $(btnCancel).remove();
            $(".btnEdit").css("visibility", 'visible');
            $(".btnDel").css("visibility", 'visible');
            $(input).remove();
            $("#span" + tdid).show();

            displayItems();
        }).catch(function () {
            $("#showEdit").modal("hide");
            showErrorbyAlert('Đã xảy ra lỗi!')
            unAuthorized();
        })
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

        displayItems();
    };
    form.appendChild(btnCancel);

    $("#span" + tdid).hide();
    $(".btnEdit").css("visibility", 'hidden');
    $(".btnDel").css("visibility", 'hidden');
}

function deleteItem(id) {
    showWarningbyAlert('Bạn chắc chắn muốn xóa thành phố này?').then((result) => {
        if (result.value) {
            axios({
                method: 'DELETE',
                url: GetCity + "/" + parseInt(id),
                headers: {
                    'content-type': 'application/json',
                    'Authorization': user.remember_token
                }
            }).then(function () {
                displayItems();
            }).catch(function (data) {
                showErrorbyAlert(data.responseText)
                unAuthorized();
            });
        }
    })
}


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
            required: "Vui lòng nhập tên thành phố",
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
                required: "Vui lòng nhập tên thành phố",
                minlength: "Phải nhập 3 ký tự trở lên"
            }
        },
        errorPlacement: function (error, element) {
            error.insertAfter(element);
        }
    });

}





