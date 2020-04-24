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
//    $("#avatar").change(function () {
//        readURL(this, '#blah');
//    });
//    $("#avatarEdit").change(function () {
//        readURL(this, '#avatarPreEdit');
//    });
//});

//function readURL(input, elem) {
//    if (input.files && input.files[0]) {
//        var reader = new FileReader();
//        reader.onload = function (e) {
//            $(elem).attr('src', e.target.result);
//        }
//        reader.readAsDataURL(input.files[0]);
//    }
//}


//function showList() {
//    $.ajax({
//        url: GetCataloge,
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

//function defaultImage(img, id, n) {
//    if (img == null) {
//        return "default.png";
//    } else
//        return "cataloges/" + id + n + img;
//}

//var user = user || {};

//user.openCreate = function () {
//    $("#showCreate").modal("show");
//    $("#name").val("");
//    //$("#permalink").val("");
//    $("#avatar").val("");
//    $("#blah").attr("src", "/uploads/default.png");
//    $("#formCreate").valid();
//};

//user.save = function () {
//    var name = $("#name").val();
//    var permalink = toSlug(name);
//    var shopId = $("#shop").val();
//    // init form data:
//    var formData = new FormData();
//    // append data
//    formData.append('name', name);
//    formData.append('permalink', permalink);
//    formData.append('shopId', shopId);
//    // get data
//    //formData.get('username'); // Returns "Chris"
//    var files = $("#avatar").get(0).files;
//    // Add the uploaded image content to the form data collection
//    if (files.length > 0) {
//        formData.append('avatarFile', files[0]);
//        formData.append('slugAvatar', toSlug(files[0].name.split(".")[0]) + "." + files[0].name.split(".")[1]);
//    }
//    createBtn(formData);
//}

//function createBtn(cataloges) {
//    $.ajax({
//        url: GetCataloge,
//        method: "POST",
//        contentType: false,
//        processData: false,
//        data: cataloges
//    }).done(function (data) {
//        addFirst(data);
//        $("#showCreate").modal("hide");
//    });
//}

//function addFirst(n) {
//    sendMessage("Đã tạo thành công catologe mới !");
//    $.ajax({
//        url: GetCataloge + "/" + n,
//        method: "GET",
//        dataType: "json",
//        contentType: "application/json"
//    }).done(function (data) {
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
//    $("#avatarEdit").val("");
//    $("#showEdit").modal("show");
//    $("#formEdit").valid();
//};

//function getEditInfo(rowIndex) {
//    arr = [];
//    $("#tbList tr:eq(" + rowIndex + ") td").each(function () {
//        arr.push($(this).text());
//    });
//    $("#nameEdit").val(arr[0]);
//    $("#shopEdit option:contains(" + arr[4] + ")").prop("selected", true);
//    //$("#shopEdit").val(arr[4]);
//    //$("#permalinkEdit").val(arr[1]);
//    $("#avatarPreEdit").attr('src', $("#tbList tr:eq(" + rowIndex + ") td:eq(1) img").attr("src"));
//}

//user.edit = function () {
//    var name = $("#nameEdit").val();
//    var permalink = toSlug(name);
//    var shopId = $("#shopEdit").val();

//    // init form data:
//    var formData = new FormData();
//    // append data
//    formData.append('name', name);
//    formData.append('permalink', permalink);
//    formData.append('shopId', shopId);

//    // get data
//    //formData.get('username'); // Returns "Chris"
//    var files = $("#avatarEdit").get(0).files;
//    // Add the uploaded image content to the form data collection
//    if (files.length > 0) {
//        formData.append('avatarFile', files[0]);
//        formData.append('slugAvatar', toSlug(files[0].name.split(".")[0]) + "." + files[0].name.split(".")[1]);
//    }
//    $("#showEdit").modal("hide");
//    editBtn(idEdit, formData);
//};

//function editBtn(idEdit, cataloge) {
//    $.ajax({
//        url: GetCataloge + "/" + idEdit,
//        method: "PUT",
//        contentType: false,
//        processData: false,
//        data: cataloge
//    }).done(function (data) {
//        showSuccessbyAlert('Thông báo', 'Sửa thông tin thành công.')
//        //sendMessage("Đã sửa thành công city mới !");
//        $("#tbList tr:eq(" + rowIndex + ")").html(
//            "<td>" + cataloge.get('name') + "</td>" +
//            "<td>" + "<img style='height:100px; width:100px' src='uploads/" + defaultImage(data, idEdit, "/thumb/") + "' />" + "</td>" +
//            "<td>" + $("#shopEdit option:selected").text() + "</td>" +
//            "<td class='btn-action'><a href='javascript:;' class='btn btn-block btn-primary btn-flat'  onclick='user.openEdit(" + idEdit + ",this)'><i class='fas fa-pencil-alt'></i> Edit</a></td>" +
//            "<td class='btn-action'><a href='javascript:;' class='btn btn-block btn-danger btn-flat' onclick='return deleteItem(" + idEdit + ")'><i class='fas fa-trash'></i> Delete</a></td>"
//        );
//    });
//}

//function deleteItem(car_id, e) {
//    rowIndex = $(e).closest("tr").index();
//    //Swal.fire({
//    //    title: 'Bạn chắc chắn muốn xóa thông tin này?',
//    //    icon: 'warning',
//    //    showCancelButton: true,
//    //    confirmButtonColor: '#d33',
//    //    cancelButtonColor: '#3085d6',
//    //    confirmButtonText: 'Vâng, xóa nó đi!',
//    //    cancelButtonText: 'Hủy',
//    //})
//    function deleteAction() {
//        $.ajax({
//            url: GetCataloge + car_id,
//            method: "DELETE",
//            dataType: "json",
//            contentType: "application/json"
//        }).done(function () {
//            $("#tbList tr:eq(" + rowIndex + ")").remove();
//        });
//    }
//    showWarningbyAlert('Bạn chắc chắn muốn xóa thông tin này?', deleteAction);
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
//            required: "Vui lòng nhập tên danh mục",
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
//            required: "Vui lòng nhập tên danh mục",
//            minlength: "Phải nhập 3 ký tự trở lên"
//        }
//    }
//});

//function drawPrepend(data) {
//    $("#tbList").prepend(
//        "<tr>" +
//        "<td>" + data.name + "</td>" +
//        "<td>" + "<img style='height:100px; width:100px' src='uploads/" + defaultImage(data.thumb, data.id, "/thumb/") + "' />" + "</td>" +
//        "<td>" + data.shopsName + "</td>" +
//        "<td class='btn-action'><a href='javascript:;' class='btn btn-block btn-primary btn-flat'  onclick='user.openEdit(" + data.id + ",this)'><i class='fas fa-pencil-alt'></i> Edit</a></td>" +
//        "<td class='btn-action'><a href='javascript:;' class='btn btn-block btn-danger btn-flat' onclick='return deleteItem(" + data.id + ",this)'><i class='fas fa-trash'></i> Delete</a></td>" +
//        "</tr>"
//    );
//}







var hdnUserSession = $("#hdnUserSession").data("value");
console.log("shopsId  " + hdnUserSession.ShopsId);

//let user = JSON.parse(localStorage.getItem('user'));

$(document).ready(function () {
    displayItems(hdnUserSession.ShopsId);
});

$("#formCreate").submit(function (event) {
    event.preventDefault();
    if (!$(this).valid()) return false;
    addItem(item.value);
});

function displayItems(shopsId) {
    axios({
        url: GetCataloge + "/shop/" + shopsId,
        method: "GET",
        headers: {
            'content-type': 'application/json',
            'Authorization': hdnUserSession.remember_token
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
            "name": item.trim(),
            "shopsId": hdnUserSession.ShopsId
        };
        axios({
            url: GetCataloge,
            method: "POST",
            headers: {
                'content-type': 'application/json',
                'Authorization': hdnUserSession.remember_token
            },
            data: newData
        }).then(function () {
            $("#formCreate")[0].reset();
            showSuccessbyAlert('Tạo danh mục mới thành công.')
            displayItems(hdnUserSession.ShopsId);
        }).catch(function () {
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
        if (!$("#formEdit").valid()) return false;
        //  btnChange.onclick = function () {
        var newData = {
            'id': tdid,
            'name': input.value.trim()
        }
        axios({
            url: GetCataloge + "/" + tdid,
            method: "PUT",
            headers: {
                'content-type': 'application/json',
                'Authorization': hdnUserSession.remember_token
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

            displayItems(hdnUserSession.ShopsId);
        }).catch(function () {
            $("#showEdit").modal("hide");
            unAuthorized();
        })

        // };
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

        displayItems(hdnUserSession.ShopsId);
    };
    form.appendChild(btnCancel);
    $("#span" + tdid).hide();
    $(".btnEdit").css("visibility", 'hidden');
    $(".btnDel").css("visibility", 'hidden');
}

function deleteItem(id) {
    showWarningbyAlert('Bạn chắc chắn muốn xóa danh mục này?').then((result) => {
        if (result.value) {
            axios({
                url: GetCataloge + "/" + parseInt(id),
<<<<<<< HEAD
<<<<<<< HEAD
                method: "DELETE"
            }).then(function () {
                displayItems(hdnUserSession.ShopsId);
            }).catch(function (data) {
                showErrorbyAlert('Cảnh báo', data.responseText)
=======
=======
>>>>>>> 4facee5cff2b4d58663460bd86bf4f9b07627dba
                method: "DELETE",
                headers: {
                    'content-type': 'application/json',
                    'Authorization': hdnUserSession.remember_token
                }
            }).then(function () {
                displayItems(hdnUserSession.ShopsId);
            }).catch(function (data) {
                showErrorbyAlert(data.responseText)
                unAuthorized();
<<<<<<< HEAD
>>>>>>> 1e5fa3f4d55602f90e120414cf434886acc18128
=======
>>>>>>> 4facee5cff2b4d58663460bd86bf4f9b07627dba
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
            required: "Vui lòng nhập tên danh mục",
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
                required: "Vui lòng nhập tên danh mục",
                minlength: "Phải nhập 3 ký tự trở lên"
            }
        },
        errorPlacement: function (error, element) {
            error.insertAfter(element);
        }
    });

}

