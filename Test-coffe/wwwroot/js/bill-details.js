function getBillDetails(tablesId) {
	getListBillDetails(tablesId).then(function (response) {
		if (response.data.length > 0) {
			var items = response.data.filter(function (rs) {
				return rs.status == 1;
			});

			var items2 = response.data.filter(function (rs) {
				return rs.status == 4;
			});

			var items3 = response.data.filter(function (rs) {
				return rs.status == 0;
			});
			let str = '';
			itemsPrinted = items.length;
			drawOrderPrinted(items, str, items2, tablesId);
			$("#table-bill-2").html("");
			if (items3.length > 0) {
				drawOrderNewWaiter(items3);
			}
		} else {
			//itemsPrinted = 0;
			$("#table-bill-1").html("");
			$("#table-bill-2").html("");
			$("#main-order-1 .checkout").removeClass("active");
			$("#main-order-1 .btn-temp-order").removeClass("active");
		}
	}).catch(function () {
		unAuthorized();
	});
}

function getListBillDetails(tablesId) {
	return axios({
		url: GetBillDetail + "/?TableId=" + tablesId,
		method: "GET",
		headers: {
			'content-type': 'application/json',
			'Authorization': hdnUserSession.remember_token
		}
	})
}

function createBillDetails(billDetails) {
	return axios({
		url: GetBillDetail,
		method: "POST",
		headers: {
			'content-type': 'application/json',
			'Authorization': hdnUserSession.remember_token
		},
		data: billDetails
	});
}

function updateBillDetail(billDetailsId, billDetails) {
	return axios({
		url: GetBillDetail + "/" + billDetailsId,
		method: "PUT",
		headers: {
			'content-type': 'application/json',
			'Authorization': hdnUserSession.remember_token
		},
		data: billDetails
	});
}

function billDetailsObj(id, quantity, total, status, username) {
	return {
		"id": id,
		"quantity": quantity,
		"total": total,
		"status": status,
		"updated_by": username
	}
}

function getGroupOrderPrinted(tableId) {
	return axios({
		url: GetBillDetail + "/TableId/" + tableId,
		method: "GET",
		headers: {
			'content-type': 'application/json',
			'Authorization': hdnUserSession.remember_token
		}
	});
}

function getOrderPrinted(tableId) {
	return axios({
		url: GetBillDetail + "/printed/" + tableId,
		method: "GET",
		headers: {
			'content-type': 'application/json',
			'Authorization': hdnUserSession.remember_token
		}
	});
}

function drawOrderPrinted(items, str, items2, tablesId) {
	if (itemsPrinted > 0) {
		for (let i = 0; i < items.length; i++) {
			let tb = new Tables(items[i].id, billsId, tablesId, items[i].productsId,
				items[i].productsName, items[i].price, items[i].quantity,
				items[i].total, items[i].status);
			tables.push(tb);
		}
		getGroupOrderPrinted(tablesId).then(function (response) {
			if (response.data.length > 0) {
				$.each(response.data, function (index, value) {
					str += drawHtml(value);
				});
				drawOrderNew(items2, str);
			}
		}).catch(function () {
			unAuthorized();
		});
	} else
		return drawOrderNew(items2, str);
}

function drawOrderNew(items2, str) {
	for (let i = 0; i < items2.length; i++) {
		let tb = new Tables(items2[i].id, billsId, tablesId, items2[i].productsId,
			items2[i].productsName, items2[i].price, items2[i].quantity,
			items2[i].total, items2[i].status);
		tables.push(tb);

		str += drawHtml(items2[i], "temp-order");
	}

	$("#table-bill-1").html(str);
	if (items2.length > 0) {
		$("#main-order-1 .btn-temp-order").addClass("active");
		$("#main-order-1 .checkout").removeClass("active");
	} else {
		$("#main-order-1 .checkout").addClass("active");
		$("#main-order-1 .btn-temp-order").removeClass("active");
	}
}

function drawHtml(value, class_css) {
	return `<div class="bill-items ${class_css}">
		<div class="col-md-5">
		<p>${value.productsName}</p>
		<p>Giá: ${addCommas(value.price)} vnđ</p>
		</div>
		<div class="col-md-3" style="text-align: center;">
		<button class="btn-minus"><i class="fa fa-minus"></i></button>
		<span>${value.quantity}</span>
		<button class="btn-plus"><i class="fa fa-plus"></i></button>
		</div>
		<div class="col-md-4" style="text-align: right;">
		<p>${addCommas(value.total)} vnđ</p>
		</div>
		</div>`;
}

function getOrderNewWaiter(tablesId) {
	axios({
		url: GetBillDetail + "/new/" + tablesId,
		method: "GET",
		headers: {
			'content-type': 'application/json',
			'Authorization': hdnUserSession.remember_token
		}
	}).then(function (response) {
		if (response.data.length > 0) {
			drawOrderNewWaiter(response.data);
		}
	}).catch(function () {
		unAuthorized();
	});
}

function drawOrderNewWaiter(data) {
	let str = '';
	$.each(data, function (index, value) {
		str += drawHtml(value, "temp-order-waiter");
	});
	$("#table-bill-2").html(str);
	$("#main-order-1 .btn-temp-order").addClass("active");
	$("#main-order-1 .checkout").removeClass("active");
}