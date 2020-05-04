let connection = new signalR.HubConnectionBuilder().withUrl("/signalServer").build()

connection.start()

//connection.on("refreshBillDetails", function () {
//	//showList();
//})

var itemId,
	itemName,
	itemPrice,
	itemCount,
	totalCount,
	totalMoney;

//vu
let user = JSON.parse(sessionStorage.getItem('user'));
var un = user.username;

var table_selected = localStorage.getItem('table_cur');
console.log("day la cart: " + table_selected);
var table_ls = JSON.parse(localStorage.getItem('table01'));
var table = [];
var data;
if (table_ls != null) {
	data = table_ls[table_selected];
	table = data;
}
console.log(table);
if (table.length != 0) {
	var table_id_seclected = table[0].table_id;
}
var table_id_ls = localStorage.getItem('table_id_to_cart');
var table_name = localStorage.getItem('table_name_to_cart');

//draw billsdetail
$.ajax({
	beforeSend: function (xhr) {
		xhr.setRequestHeader('Authorization', user.remember_token);
	},
	url: "/api/mobile/BillDetailsApi/?table_id=" + table_id_ls * 1,
	method: "GET",
	dataType: "json",
	async: false,
	contentType: "application/json"
	}).done(function (result) {
		let data1 = result;
		console.log(data1);
		let k1 = data1.length;
		let str1 = '';
		for (let i = 0; i < k1; i++) {
			let avatar = data1[i].images != null ? JSON.parse(data1[i].images).avatar : "#";
			str1 += `<div class="item">
				<div class="col-md-12" style="padding: 0;">
				<div class="item-img col-md-2">
				<img width="50px" height="50px" src="../uploads/products/${data1[i].productsId}/${avatar}" onerror="loadImageError(this)">
				</div>
				<div class="item-info col-md-6">
				<p><b>${data1[i].productsName}</b></p>
				<p>${addCommas(data1[i].price)} vnđ</p>
				</div>
				<div class="item-count col-md-1">
				<span>${data1[i].quantity}</span>
				</div>
				<div class="item-money col-md-23">
				<p>
				<span>${addCommas(data1[i].total)} vnđ</span>
				<span>&nbsp;</span>		
				</p>
				</div>
				</div>
				</div>`;
	}
	$("#group-item-billsdetail").html(str1);
	});

//draw bills
$.ajax({
	beforeSend: function (xhr) {
		xhr.setRequestHeader('Authorization', user.remember_token);
	},
	url: "/api/mobile/BillsApi/?table_id=" + table_id_ls * 1,
	method: "GET",
	dataType: "json",
	contentType: "application/json"
	}).done(function (result) {
		//readTextFile(data, function (text) {
			let data = result;
			data = data.filter(function (rs) {
				return rs.tablesId == table_id_ls * 1;
		});
		bill_id_cur = data[0].id;

		let str = '';
		str += `<p><b>${addCommas(data[0].sub_total)} vnđ</b></p>
			<p>${addCommas(data[0].fee_service)} vnđ</p>`;
		$("#sub-total-money-1 .col-md-4").html(str);
		//});	
		str = '';
		str += `<p><b>${addCommas(data[0].total_money)} vnđ</b></p>`;
		$("#total-money-1 .col-md-4").html(str);

		let s = '';
		s += `<div>
				<p><b>${table_name}</b></p>
				<p>${data[0].created_by}</p>
			  </div>`;
		$("#table-order-name").html(s);
});
//draw new products
if (data != null) {
	let k = data.length;
	let str = '';
	for (let i = 0; i < k; i++) {
		let table = new Tables();
		Object.assign(table, data[i]);
		console.log(table);
		str += `<div id="item-${table.getProductsId()}" class="item temp-order" data-item-id="${table.getProductsId()}" data-item-name="${table.getProductsName()}" data-item-price="${table.getPrice()}" data-item-count="${table.getQuantity()}">
					<div class="col-md-12" style="padding: 0;">
						<div class="item-img col-md-2">
							<img width="50px" height="50px" src="../uploads/products/${table.getProductsId()}/${table.getAvatar()}" onerror="loadImageError(this)">
						</div>
						<div class="item-info col-md-6">
							<p><b>${table.getProductsName()}</b></p>
							<p>${addCommas(table.getPrice())} vnđ</p>
						</div>
						<div class="item-count col-md-1">
							<span>${table.getQuantity()}</span>
						</div>
						<div class="item-money col-md-23">
							<p>
								<span>${addCommas(table.getTotal())} vnđ</span>
								<span>&nbsp;</span>			
							</p>
						</div>
					</div>
					<div class="item-delete" data-item-id="${table.getProductsId()}">
						Xóa
					</div>
				</div>`;
	}
	$("#group-item").append(str);
	//draw card flex
	cardDraw(table);
}

function cardDraw(table_data){
	if (table_data != null) {
		//table_cur = table_cur_ls;
		//	console.log("bb: " + table_cur);
		//table01[table_cur] = table_temp[table_cur];
		//	console.log(table01[table_cur])
		//ve card khi load lai
		var total = 0;
		var countItem = 0;
		if (table_data.length == 0) {
			$(".cart-checkout-fix").css("display", "none");
		} else {
			$.each(table, function () {
				let q = this.item_quantity;
				q *= 1;
				let p = this.item_price;
				total += p * q;
				countItem += q;
			});

			// let str2 = countItem + " | Giỏ hàng " + addCommas(total) + ' đ' + ' ->';
			// $("#btnCheckout").html(str2);
			$(".cart-checkout .cart-count-total").html(countItem);
			$(".cart-checkout .cart-money-total").html("Gọi món " + table_name);
			$(".cart-checkout-fix").css("display", "flex");
		}
		// het ve
	} else
		$(".cart-checkout-fix").css("display", "none");
}
//
//het vu

$("#group-item .item").draggable({
	axis: "x",
	drag: function () {
		itemId = $(this).data("item-id");
		$("#group-item .item").each(function(index) {
			if($(this).data("item-id") != itemId && $(this).offset().left == -80) {
				// $(this).css("animation-name", "revertItem");left: -80px;
				$(this).animate({ left: "+=80px"}, 'slow');
				$(this).css("left", "0px");		
				$(this).find(".item-delete").animate({ width: "-=80px", right: "+=80px" }, 'slow', function() {
					$(this).parent().css("animation-name", "");
				});
			}
		});

		var ol = $(this).offset().left;
        var w = -ol + "px";
        var r = ol + "px";
        $(this).find(".item-delete").css("width", w);
        $(this).find(".item-delete").css("right", r);
		
    },
	revert: function(){
		itemId = $(this).data("item-id");
		if($(this).offset().left < -30) {
			$(this).css("left", "-80px");
			$(this).find(".item-delete").css("width", "80px");
			$(this).find(".item-delete").css("right", "-80px");
		}
		else {
			$(this).css("left", "0px");		
			$(this).find(".item-delete").css("width", "0px");
			$(this).find(".item-delete").css("right", "0px");
		}
	}
});

$("#group-item .item").on("click", function() {
	itemId = $(this).data("item-id");
	if($(this).offset().left == 0) {
		$(".cart_nav_overlay_mobile").css("display", "flex");
		$("#edit-item").animate({ height: "+=350px", opacity: 1, display: "block" }, 400 );
		itemId = $(this).data("item-id");
		itemName = $(this).data("item-name");
		itemPrice = $(this).data("item-price");
		itemCount = $(this).data("item-count");
		$("#edit-item .item-name-selected span").html(itemName);
		$("#edit-item .item-count-selected input").val(itemCount);

		$("#group-item .item").each(function(index) {
			if($(this).offset().left < 0) {
				$(this).animate({ left: "+=80px" }, 400 );
				$(this).find(".item-delete").animate({ right: '+=80px', width: "-=80px"}, 400 );
			}
		});		
	}
	else {
		$(this).animate({ left: "+=80px" }, 400 );
		$(this).find(".item-delete").animate({ right: '+=80px', width: "-=80px"}, 400 );
	}
});

function itemDeleteNone() {
	$("#group-item .item-delete").css("display", "none");
}

$("#group-item .item-delete").on("click", function() {
	itemId = $(this).data("item-id");
	$("#item-" + itemId).remove();
	$.each(table, function () {
		if (this.item_id == parseInt(itemId)) {
			var index = table.indexOf(this);
			table.splice(index, 1);
		}
	});

	//var table_temp = JSON.stringify(table);
	var table_all = JSON.parse(localStorage.getItem('table01'));
	//console.log(table_temp);
	console.log(table_all);
	table_all[table_selected] = table;
	console.log(table_all);
	localStorage.setItem('table01', JSON.stringify(table_all));
	cardDraw(table);
});

$(".cart_nav_overlay_mobile, .btn-close, .btn-check").on("click", function() {
	$(".cart_nav_overlay_mobile").css("display", "none");
	$("#edit-item").animate({ height: "-=350px", opacity: 0, display: "none" }, 400 );
});


$("#edit-item .btn-plus").on("click", function() {
	totalCount = $("#edit-item .item-count-selected input").val();
	totalCount *= 1;
	totalCount += 1;
	$("#edit-item .item-count-selected input").val(totalCount);
});

$("#edit-item .btn-minus").on("click", function() {
	totalCount = $("#edit-item .item-count-selected input").val();
	totalCount *= 1;
	
	if(totalCount > 1) {
		totalCount -= 1;
		$("#edit-item .item-count-selected input").val(totalCount);	
	}
	else {
		$("#item-"+ itemId).remove();
		$(".cart_nav_overlay_mobile").click();
		//vu
		$.each(table, function () {
			if (this.item_id == parseInt(itemId)) {
				var index = table.indexOf(this);
				table.splice(index, 1);
			}
		});

		//var table_temp = JSON.stringify(table);
		var table_all = JSON.parse(localStorage.getItem('table01'));
		//console.log(table_temp);
		console.log(table_all);
		table_all[table_selected] = table;
		console.log(table_all);
		localStorage.setItem('table01', JSON.stringify(table_all));
		cardDraw(table);
	}
});


$("#edit-item .btn-check").on("click", function() {
	totalCount = $("#edit-item .item-count-selected input").val();
	totalMoney = totalCount * itemPrice;
	$("#item-"+ itemId).data("item-count", totalCount);
	$("#item-"+ itemId + " .item-count span").html(totalCount);
	$("#item-" + itemId + " .item-money p span:eq(0)").html(addCommas(totalMoney)+" vnđ");
	//vu
	$.each(table, function () {		
		if (this.item_id == parseInt(itemId)) {
			totalCount *= 1;
			let q = totalCount;
			this.item_quantity = q;
			this.money = q * itemPrice;			
		}
	});

	//var table_temp = JSON.stringify(table);
	var table_all = JSON.parse(localStorage.getItem('table01'));
	//console.log(table_temp);
	console.log(table_all);
	table_all[table_selected] = table;
	console.log(table_all);
	localStorage.setItem('table01', JSON.stringify(table_all));
	cardDraw(table);
});

$(".nav-cart").on("click", function () {
	//localStorage.removeItem('table01');
	var table_all = JSON.parse(localStorage.getItem('table01'));
	table = [];
	table_all[table_selected] = table;
	console.log(table_all);
	localStorage.setItem('table01', JSON.stringify(table_all));
	cardDraw(table);
	$("#group-item div").remove();
})

//gui thu ngan
<<<<<<< HEAD
let user = JSON.parse(sessionStorage.getItem('user'));
var un = user.username;
=======
//let user = JSON.parse(sessionStorage.getItem('user'));
//var un = user.username;
>>>>>>> 4e068ae1f6795d453ad2a311572a4126a4239ff0
var table_status;
var bill_id;

$(".cart-checkout").on("click", function () {	
	checkStatusTable(table_id_seclected);
	console.log(table_id_seclected);
	console.log(table_status);
	
	if (table_status == 0 || table_status == 1 || table_status == 2 || table_status == 3) {
		if (table_status == 0) {
			//create bill
			console.log("tao bill");
			var newData = {
				"status": 0,
				"sub_total": 0,// cartTotalMoney(table),
				"fee_service": 0,
				"total_money": 0,//cartTotalMoney(table),
				"created_by": un,
				"TablesId": table_id_seclected * 1
			}
			$.ajax({
				beforeSend: function (xhr) {
					xhr.setRequestHeader('Authorization', user.remember_token);
				},
				url: "/api/mobile/BillsApi",
				method: "POST",
				dataType: "json",
				async: false,
				data: JSON.stringify(newData),
				contentType: "application/json"

			}).done(function (data) {
				bill_id = data.id;
				console.log(bill_id);
				localStorage.setItem('bill_id_ls' + table_id_seclected, bill_id);
				

			}).fail(function (jqXHR, textStatus, errorThrown) {

				console.log(textStatus + ': ' + errorThrown);
			});
		}

		if (table_status == 1 || table_status == 2 || table_status == 3) {
			console.log("lay bill id");
			$.ajax({
				beforeSend: function (xhr) {
					xhr.setRequestHeader('Authorization', user.remember_token);
				},
				url: "/api/mobile/BillsApi/?table_id=" + table_id_ls * 1,
				method: "GET",
				dataType: "json",
				async: false,
				contentType: "application/json"
			}).done(function (result) {
				//readTextFile(data, function (text) {
				let data = result;
				data = data.filter(function (rs) {
					return rs.tablesId == table_id_ls * 1;
				});
				bill_id_cur = data[0].id;
				localStorage.setItem('bill_id_ls' + table_id_seclected, bill_id_cur);
				console.log("data");
				console.log(data);
			});
		}

		//change table status to 2(have new product)
		var newData = {
			"id": table_id_seclected*1,
			"status": 2
		}
		$.ajax({
			beforeSend: function (xhr) {
				xhr.setRequestHeader('Authorization', user.remember_token);
			},
			url: "/api/mobile/TablesApi/" + table_id_seclected,
			method: "PUT",
			dataType: "json",
			async: false,
			data: JSON.stringify(newData),
			contentType: "application/json"

		}).done(function (data) {
			table_status = 2;
			console.log(table_status);
			console.log("doi tt ban");
		}).fail(function (jqXHR, textStatus, errorThrown) {

			console.log(textStatus + ': ' + errorThrown);
		});		
	} 

	//create billdetail
	for (let i = 0; i < table.length; i++) {
		console.log("tao bill-detail");
		if (table[i].item_quantity > 0) {
			table[i].item_price *= 1;
			table[i].item_quantity *= 1;
			let total = table[i].item_quantity * table[i].item_price;
			let bId = localStorage.getItem('bill_id_ls' + table_id_seclected)*1;
			var newData = {
				"price": table[i].item_price,
				"quantity": table[i].item_quantity,
				"total": total,
				"status": 0,
				"ProductsId": table[i].item_id,
				"created_by": un,
				"BillsId": bId
			}
			$.ajax({
				beforeSend: function (xhr) {
					xhr.setRequestHeader('Authorization', user.remember_token);
				},
				url: "/api/mobile/BillDetailsApi",
				method: "POST",
				dataType: "json",
				async: false,
				data: JSON.stringify(newData),
				contentType: "application/json"

			}).done(function (data) {
				//signalR
				connection.invoke("SendMessage").catch(function (err) {
					return console.error(err.toString());
				});
			//
				

			}).fail(function (jqXHR, textStatus, errorThrown) {

				console.log(textStatus + ': ' + errorThrown);
			});
		}
	}

	var table_all = JSON.parse(localStorage.getItem('table01'));
	table = [];
	table_all[table_selected] = table;
	console.log(table_all);
	localStorage.setItem('table01', JSON.stringify(table_all));
	$("#group-item").html('');
	cardDraw(table);

	return false	
})

function checkStatusTable(table_id) {
	table_id *= 1;
	$.ajax({
		beforeSend: function (xhr) {
			xhr.setRequestHeader('Authorization', user.remember_token);
		},
		url: "/api/mobile/TablesApi/" + table_id ,
		method: "GET",
		dataType: "json",
		async:false,
		contentType: "application/json"
			
	}).done(function (data) {
		// If successful
		//console.log(data);
		table_status = data.status;		
		
	}).fail(function (jqXHR, textStatus, errorThrown) {
		// If fail
		console.log(textStatus + ': ' + errorThrown);		
	});
}

function cartTotalMoney(table) {
	var total = 0;
	$.each(table, function () {
		if (this.item_id == parseInt(itemId)) {
			totalCount *= 1;
			let q = totalCount;
			this.item_quantity = q;
			this.money = q * itemPrice;
		}
		total += this.money;
	});
	return total;
}

$(".nav_mobile-link").on("click", function () {
	//localStorage.removeItem('bill_id_ls' + table_id_seclected);
	//localStorage.removeItem('table01');
})