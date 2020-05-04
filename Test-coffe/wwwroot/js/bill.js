//var hdnUserSession = $("#hdnUserSession").data("value");
let user = JSON.parse(sessionStorage.getItem('user'));

function getBill(tablesId, tablesName) {
	checkBill(tablesId).then(function (response) {
		if (response.data.length > 0) {
			console.log(response.data[0]);
			billsId = response.data[0].id;
			let txt = `<div>
				<p><b>${response.data[0].tablesName}</b></p>
				<p>${response.data[0].created_by}</p>
				</div >`;
			$(".table-name").html(txt);
			let str = '';
			str += `<p><b>${addCommas(response.data[0].sub_total)} vnđ</b></p>
				<p>${addCommas(response.data[0].fee_service)} vnđ</p>`;
			$("#sub-total-money-1 .col-md-4").html(str);
			$("#total-money-1 .col-md-7").html(`<p><b>${addCommas(response.data[0].total_money)} vnđ</b></p>`);
			getBillDetails(tablesId);
		}
		else {
			let txt = `<div>
				<p><b>${tablesName}</b></p>
				<p>${user.username}</p>
				</div >`;
			$(".table-name").html(txt);
			let str = '';
			str += '<p><b>0 vnđ</b></p>' +
				'<p>0 vnđ</p>';
			$("#sub-total-money-1 .col-md-4").html(str);
			$("#table-bill-1").html("");
			$("#table-bill-2").html("");
			$("#total-money-1 .col-md-7").html("<p><b>0 vnđ</b></p>");

			$("#main-order-1 .checkout").removeClass("active");
			$("#main-order-1 .btn-temp-order").removeClass("active");
		}
	}).catch(function () {
		unAuthorized();
	})
}

function createBill(bills) {
	return axios({
		url: GetBill,
		method: "POST",
		headers: {
			'content-type': 'application/json',
			'Authorization': user.remember_token
		},
		data: bills
	});
}

function updateBill(billsId, bills) {
	return axios({
		url: GetBill + "/" + billsId,
		method: "PUT",
		headers: {
			'content-type': 'application/json',
			'Authorization': user.remember_token
		},
		data: bills
	});
}

function checkBill(tablesId) {
	return axios({
		url: GetBill + "/?TableId=" + tablesId,
		method: "GET",
		headers: {
			'content-type': 'application/json',
			'Authorization': user.remember_token
		}
	})
}

