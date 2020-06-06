//var hdnUserSession = $("#hdnUserSession").data("value");
//var shopId = hdnUserSession.ShopsId;
var UserSession = JSON.parse(sessionStorage.getItem('user'));
var shopId = UserSession.ShopsId * 1;

var floorList = [];
getFloors();

function getFloors() {
    axios({
        url: "api/mobile/FloorsApi/?shop_id=" + shopId,
        method: "get",
        // async: false,
        headers: {
            'Content-Type': 'application/json',
            'Authorization': UserSession.remember_token
        }
    }).then(function (response) {
        floorList = response.data;
        drawFloors();
        // console.log("aaa");
    });
}

function drawFloors() {
    for (let i = 0; i < floorList.length; i++) {
        var z = document.createElement("option");
        z.setAttribute("value", floorList[i].id);
        var t = document.createTextNode(floorList[i].name);
        z.appendChild(t);
        document.getElementById("cFilterFloor").appendChild(z);
        //console.log("bb");
    }

    for (let i = 0; i < floorList.length; i++) {
        var z = document.createElement("option");
        z.setAttribute("value", floorList[i].id);
        var t = document.createTextNode(floorList[i].name);
        z.appendChild(t);
        document.getElementById("FilterFloor").appendChild(z);
        //console.log("cc");
    }
}

function addItem(item) {
    if (item.trim().length > 0) {
        //arr.push(item.trim());
        let newData = {
            "name": item.trim().replace(/([^0-9a-z-\s])/g, ''),
            "permalink": toSlug(item.trim() + " " + $('#cFilterFloor').val()),
            "floorsId": $('#cFilterFloor').val() * 1
        };
        axios({
            url: "/api/mobile/TablesApi",
            method: "post",
            dataType: "json",
            headers: {
                'Content-Type': "application/json",
                'Authorization': UserSession.remember_token
            },
            data: JSON.stringify(newData)
        }).then(function (data) {
            if (data.status == 200) {
                Swal.fire({
                    icon: 'error',
                    title: 'Cảnh báo',
                    text: data.data
                });
            } else {
                Swal.fire(
                    'Thông báo',
                    'Tạo bàn mới thành công.',
                    'success'
                );
                displayItems($("#cFilterFloor").val());
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

var listTable = [];
displayItems("");

function displayItems(floor_id) {
    floor_id *= 1;
    var st = "/api/mobile/TablesApi/";
    if (floor_id != "") {
        st += "floor?floor_id=" + floor_id;
    } else {
        st += "?shop_id=" + shopId;
    }

    axios({
        url: st,
        method: "GET",
        dataType: "json",
        async: false,
        headers: {
            'Content-Type': 'application/json',
            'Authorization': UserSession.remember_token
        }
    }).then(function (response) {
        listTable = response.data;
        let data = response.data;
        let text = '';
        for (let i = 0; i < data.length; i++) {
            text += `<tr>`;
            text += `<th scope="row">${i + 1}</th>`;
            text += `<td id="td${data[i].id}"><span id="span${data[i].id}">${data[i].name}</span></td>`;
            text += `<td id="tdf${data[i].id}"><span id="spanf${data[i].id}">${data[i].floors.name}</span></td>`;
            text += `<td id="tdEdit${data[i].id}"><button id="btnEdit${data[i].id}" onclick="editItem(${data[i].id},'${data[i].name}','${data[i].floorsId}');" class="btn btnEdit"><i class="fa fa-edit edit-btn"></i>Sửa</button></td>`;
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

function editItem(tdid, val, flid) {
    flid *= 1;
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

    var div = document.createElement("div");
    div.id = 'divInput';
    div.appendChild(input);

    form.appendChild(div);

    var container1 = container = document.getElementById("tdf" + tdid);
    var floor = document.createElement("SELECT");
    floor.classList = "form-control col-sm-9 mr-3 bg-white text-black";
    //floor.setAttribute("id", "floorSelect");
    floor.id = 'floorSelect';
    form.appendChild(floor);

    for (let i = 0; i < floorList.length; i++) {
        var z = document.createElement("option");
        z.setAttribute("value", floorList[i].id);
        var t = document.createTextNode(floorList[i].name);
        z.appendChild(t);
        document.getElementById("floorSelect").appendChild(z);
    }
    floor.value = flid;

    var tdEdit = document.getElementById("tdEdit" + tdid);
    var btnChange = document.createElement("button")
    btnChange.classList = "btn btn-primary bg-info border-0 mr-3";
    btnChange.innerHTML = "Lưu";
    btnChange.type = "submit";

    abc();
    $("#formEdit").submit(function (event) {
        event.preventDefault();
        if (!$("#formEdit").valid()) return false;
        //btnChange.onclick = function () {
        //arr[tdid] = input.value;
        var newData = {
            'id': tdid,
            'name': input.value.trim().replace(/([^0-9a-z-\s])/g, ''),
            'permalink': toSlug(input.value.trim() + " " + floor.value),
            'floorsId': floor.value * 1
        }
        axios({
            url: "/api/mobile/TablesApi/" + tdid,
            method: "PUT",
            dataType: "json",
            async: false,
            headers: {
                'Content-Type': "application/json",
                'Authorization': UserSession.remember_token
            },
            data: JSON.stringify(newData)
        }).catch(function () {
            $("#showEdit").modal("hide");
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
            } else {
                Swal.fire(
                    'Thông báo',
                    'Sửa thông tin thành công.',
                    'success'
                );
                $(btnChange).remove();
                $(btnCancel).remove();
                $(".btnEdit").css("visibility", 'visible');
                $(".btnDel").css("visibility", 'visible');
                $(input).remove();
                $("#span" + tdid).show();
                $("#spanf" + tdid).show();
                $("#FilterFloor").val(floor.value);
                displayItems($("#FilterFloor").val());
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
        $("#spanf" + tdid).show("slow");
        $("#FilterFloor").val(floor.value);
        displayItems($("#FilterFloor").val());
    };
    form.appendChild(btnCancel);

    $("#span" + tdid).hide();
    $("#spanf" + tdid).hide();
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
        title: 'Bạn chắc chắn muốn xóa bàn này?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#3085d6',
        confirmButtonText: 'Vâng, xóa nó đi!',
        cancelButtonText: 'Hủy',
    }).then((result) => {
        if (result.value) {
            axios({
                url: '/api/mobile/TablesApi/del/' + parseInt(id) + "/?name=" + UserSession.username,
                method: 'put',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': UserSession.remember_token
                }
            }).then(function (response) {
                if (response.status == 200) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Cảnh báo',
                        text: response.data
                    });
                }else
                    displayItems($("#FilterFloor").val());
            }).catch(function (error) {
                Swal.fire({
                    icon: 'error',
                    title: 'Cảnh báo',
                    text: 'Đã xảy ra lỗi'
                });
            });
        }
    })
}

$("#FilterFloor").change(function () {
    displayItems(this.value);
});


$("#formCreate").submit(function (event) {
    event.preventDefault();
    // jQuery.noConflict();
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
            required: "Vui lòng nhập tên bàn",
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
                required: "Vui lòng nhập tên bàn",
                minlength: "Phải nhập 3 ký tự trở lên"
            }
        },
        errorPlacement: function (error, element) {
            error.insertAfter(element);
        }
    });

}