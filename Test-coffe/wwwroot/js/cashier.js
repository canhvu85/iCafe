let tables = [];
let groupItemCount = 0;
let groupItemArray = [];
//let user = JSON.parse(localStorage.getItem('user'));

let tablesId;
let tablesName;
let itemsPrinted = 0;
let billDetailsId;
let billsId;
getTables();
getCataloges();
getProducts();
printOrder();
cancelOrder();
checkout();

let connection = new signalR.HubConnectionBuilder().withUrl("/signalServer").withAutomaticReconnect().build()

connection.start()

connection.on("refreshBillDetails", function () {
	console.log("sent")
	getOrderNewWaiter(tablesId);
})

let connection2 = new signalR.HubConnectionBuilder().withUrl("/Cancel").withAutomaticReconnect().build()

connection2.start()

connection2.on(hdnUserSession.username, function () {
	console.log("OK2");
})

function table_click() {
	$(".list-table .num-table").on("click", function () {
		$(".list-table .num-table").removeClass("active");
		$(this).addClass("active");

		tablesId = parseInt($(this).find("a h2").attr("id"));
		tablesName = $(this).find("a h2").html();

		$($(this).find("a").attr("href")).css("display", "block");

		$(".list-table-extend button").eq(0).css("display", "inline-block");
		$(".list-table-extend button").eq(1).css("display", "none");
		$(".list-table").height(70);
		$(".container .main-order-left").css("visibility", "");
		$(".container .main-order-right").eq(0).css("visibility", "");
		$(".nav_overlay").css("display", "none");

		tables = [];
		getBill(tablesId, tablesName);
		return false;
	});
}

function getTables() {
	//$.ajax({
	//	beforeSend: function (xhr) {
	//		xhr.setRequestHeader('Authorization', hdnUserSession.token);
	//	},
	//	url: GetTable + "/?shop_id=" + hdnUserSession.ShopsId,
	//	method: "GET",
	//	dataType: "json",
	//	contentType: "application/json"
	//}).done(function (data) {
	//	console.log(data)
	//});

	axios({
		url: GetTable + "/?shop_id=" + hdnUserSession.ShopsId,
		method: "GET",
		headers: {
			'content-type': 'application/json',
			'Authorization': hdnUserSession.remember_token
		}
	}).then(function (response) {
		console.log(response);
		let str = '';
		let active = ' active';
		$.each(response.data, function (index, value) {
			str += `<div class="num-table col-md-2 ${active}">
				<a href="#main-order-${value.id}">
				<h2 id="${value.id}">${value.name}</h2>
				</a>
				</div>`;
			active = '';
		});
		$(".list-table").html(str);
		table_click();
		getBill(response.data[0].id, response.data[0].name);
		tablesId = response.data[0].id;
	}).catch(function () {
		unAuthorized();
	});
}

function changeCategory() {
	$(".group-item a").on("click", function () {
		$(".group-item").removeClass("active");
		$(this).parent().addClass("active");
		$(".group-list-items .list-items").removeClass("active");
		$($(this).attr("href")).addClass("active");
		return false;
	});
}

function getCataloges() {
	axios({
		url: GetCataloge + "/shop/" + hdnUserSession.ShopsId,
		method: "GET"
	}).then(function (response) {
		groupItemCount = response.data.length;
		let str = '';
		let active = ' active';
		for (let i = 0; i < groupItemCount; i++) {
			groupItemArray.push(response.data[i].id);
			str += `<div class="group-item ${active}">
				<a href="#group-item-${response.data[i].id}">${response.data[i].name}</a>
				</div>`;
			active = '';
		}
		$(".group-items").html(str);

		changeCategory();
	});
}

function getProducts() {
	axios({
		url: "api/ProductsAPI/shop/" + hdnUserSession.ShopsId,
		method: "GET"
	}).then(function (response) {
		let items;
		let active = ' active';
		for (var i = 0; i < groupItemCount; i++) {
			items = response.data.filter(function (rs) {
				return rs.catalogesId == groupItemArray[i];
			});
			let j = items.length;
			if (j > 0) {
				let str = `<div id="group-item-${groupItemArray[i]}" class="list-items ${active}">`;
				var img;
				for (let k = 0; k < j; k++) {
					img = items[k].images != null ? JSON.parse(items[k].images).thumb : "#";
					str += `<div class="item">
						<div class="item-img">
						<img src="uploads/products/${items[k].id}/${img}" onerror="loadImageError(this);">
						</div>
						<div class="item-btn">
						<input type="button" class="btn btn-success btn-add" value="Thêm" data-id="${items[k].id}" data-name="${items[k].name}" data-price="${items[k].price}" />
						</div>
						<div class="item-info">
						<p><b>${items[k].name}</b></p>
						<p>${addCommas(items[k].price)} vnđ</p>
						</div>
						</div>`;
				}
				str += '</div>';
				active = '';
				$(".group-list-items").append(str);

			}
			else {
				let str = `<div id="group-item-${groupItemArray[i]}" class="list-items"></div>`;
				$(".group-list-items").append(str);
				$(".group-list-items-mobile").append(str);
			}
		};
		addItemProduct();
	});
}

function addItemProduct() {
	$(".group-list-items .btn-add").on("click", function () {
		let itemId = $(this).data("id");
		let name = $(this).data("name");
		let price = $(this).data("price");
		let billDetails;
		let k = false;
		checkBill(tablesId).then(function (rs) {
			if (rs.data.length == 0) {
				let bills = {
					TablesId: tablesId,
					created_by: hdnUserSession.username
				}

				createBill(bills).then(function (rs) {
					billsId = rs.data.id;
					createDrawNewOrder(price, hdnUserSession.username, itemId, billsId, tablesId, name);
					itemsPrinted = 0;
				})

				updateTable(tablesId, 2);
			} else if (rs.data.length == 1) {
				if (rs.data.status != 2) {
					updateTable(tablesId, 2);
				}

				for (i = itemsPrinted; i < tables.length; i++) {
					if (tables[i].productsId == itemId) {
						let q = tables[i].quantity + 1;
						tables[i].quantity = q;
						tables[i].total = q * price;
						k = true;
						billDetailsId = tables[i].billDetailsId;
						billDetails = billDetailsObj(billDetailsId, tables[i].quantity, tables[i].total, 4, hdnUserSession.username);
					}
				}

				if (!k) {
					createDrawNewOrder(price, hdnUserSession.username, itemId, rs.data[0].id, tablesId, name);
				} else {
					updateBillDetail(billDetailsId, billDetails);

					let str = '';
					if (itemsPrinted > 0) {
						getGroupOrderPrinted(tablesId).then(function (rs) {
							if (rs.data.length > 0) {
								$.each(rs.data, function (index, value) {
									str += drawHtml(value);
								});
								for (var i = itemsPrinted; i < tables.length; i++) {
									str += drawHtml(tables[i], "temp-order");
								}
								$("#table-bill-1").html(str);
							}
						});
					} else {
						for (var i = itemsPrinted; i < tables.length; i++) {
							str += drawHtml(tables[i], "temp-order");
						}
						$("#table-bill-1").html(str);

					}
				}
			} else
				console.log("Nhiều Bills")
		})

		$("#main-order-1 .btn-temp-order").addClass("active");
		$("#main-order-1 .checkout").removeClass("active");
	});
}

function updateTable(tablesId, status) {
	return axios({
		url: GetTable + "/" + tablesId,
		method: "PUT",
		headers: { 'content-type': 'application/json' },
		data: JSON.stringify({
			id: tablesId,
			status: status,
			updated_by: hdnUserSession.username
		})
	});
}

function createDrawNewOrder(price, username, itemId, billsId, tablesId, name) {
	billDetails = {
		"price": price,
		"quantity": 1,
		"total": price,
		"status": 4,
		"created_by": username,
		"productsId": itemId,
		"billsId": billsId
	}
	createBillDetails(billDetails).then(function (rs) {
		billDetailsId = rs.data.id;
		let tb = new Tables(billDetailsId, billsId, tablesId, itemId,
			name, price, 1, price, 0);
		tables.push(tb);
	})
	let str = `<div class="bill-items temp-order">
		<div class="col-md-5">
		<p>${name}</p>
		<p>Giá: ${addCommas(price)} vnđ</p>
		</div>
		<div class="col-md-3" style="text-align: center;">
		<button class="btn-minus"><i class="fa fa-minus"></i></button>
		<span>1</span>
		<button class="btn-plus"><i class="fa fa-plus"></i></button>
		</div>
		<div class="col-md-4" style="text-align: right;">
		<p>${addCommas(price)} vnđ</p>
		</div>
		</div>`;

	$("#table-bill-1").append(str);
}

function printOrder() {
	$("#main-order-1 .btn-temp-order .btn-primary").on("click", function (data) {
		alert("da in bep");
		updateTable(tablesId, 1);

		getListBillDetails(tablesId).then(function (rs) {
			var items = rs.data.filter(function (rs) {
				return rs.status == 0 || rs.status == 4;
			});

			var items2 = rs.data.filter(function (rs) {
				return rs.status == 1;
			});

			let billDetails;
			let sub_total = 0;

			let billsId = items[0].billsId;

			items2.forEach(function (item) {
				sub_total += item.total;
			});

			function awaitAll(list, asyncFn) {
				const promises = [];

				list.forEach(x => {
					promises.push(asyncFn(x));
				});

				return Promise.all(promises);
			}

			function editBillDetails(x) {
				billDetails = billDetailsObj(x.id, x.quantity, x.total, 1, hdnUserSession.username);
				updateBillDetail(x.id, billDetails)
				sub_total += x.total;
			}

			awaitAll(items, editBillDetails).then(function () {
				let bills = {
					id: billsId,
					sub_total: sub_total,
					updated_by: hdnUserSession.username
				};
				updateBill(billsId, bills).then(function () {
					tables = [];
					getBill(tablesId, tablesName);
				})
			})
		})
	});
}

function cancelOrder() {
	$("#main-order-1 .btn-temp-order .btn-warning").on("click", function () {
		alert("da huy don");
		billDetails = billDetailsObj(235, 1, 0, 2, hdnUserSession.username);
		updateBillDetail(235, billDetails).then(function (rs) {
			if (rs.data != null && rs.data.message == "Không tìm thấy") {
				console.log("Không tìm thấy");
			} else {
				connection2.invoke("CancelOrder").catch(function (err) {
					return console.error(err.toString());
				});
				//connection2.invoke("CancelOrder", "vu35").catch(function (err) {
				//	return console.error(err.toString());
				//});
			}
		}).catch(function () {
			console.log("lỗi");
		})
	});
}

function checkout() {
	$("#main-order-1 .checkout .btn-success").on("click", function () {
		alert("da thanh toan");
		getOrderPrinted(tablesId).then(function (rs) {
			$.each(rs.data, function (index, value) {
				billDetails = billDetailsObj(value.id, value.quantity, value.total, 3, hdnUserSession.username);
				updateBillDetail(value.id, billDetails).then(function () {
					let bills = {
						id: billsId,
						status: 1,
						updated_by: hdnUserSession.username
					};
					updateBill(billsId, bills).then(function () {
						updateTable(tablesId, 0).then(function () {
							tables = [];
							let str = '';
							str += '<p><b>0 vnđ</b></p>' +
								'<p>0 vnđ</p>';
							$("#sub-total-money-1 .col-md-4").html(str);
							$("#table-bill-1").html("");
							$("#table-bill-2").html("");
							$("#total-money-1 .col-md-7").html("<p><b>0 vnđ</b></p>");

							$("#main-order-1 .checkout").removeClass("active");
							$("#main-order-1 .btn-temp-order").removeClass("active");
						})
					})
				});
			});
		});
	});
}
