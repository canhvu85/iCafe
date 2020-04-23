

function readTextFile(file, callback) {
    let rawFile = new XMLHttpRequest();
    rawFile.overrideMimeType("application/json");
    rawFile.open("GET", file, true);
    rawFile.onreadystatechange = function () {
        if (rawFile.readyState === 4 && rawFile.status == "200") {
            callback(rawFile.responseText);
        }
    }
    rawFile.send(null);
}

var table01 = {};
let bill_detail_id_max;
let groupItemCount = 0;
let groupItemArray = [];
var table_cur;
var table_name_to_cart;
var table_id_to_cart;
var bill_id_cur = 0;
var total = 0;
var countItem = 0;

var table_temp = JSON.parse(localStorage.getItem('table01'));
var table_cur_ls = localStorage.getItem('table_cur');
var table_name_to_cart_ls = localStorage.getItem('table_name_to_cart');
var table_id_to_cart_ls = localStorage.getItem('table_id_to_cart');

if (table_temp != null) {
	table01 = table_temp;
}
console.log("aa");
console.log(table_temp);

let user = JSON.parse(localStorage.getItem('user'));
let shop_id = user.ShopsId;

let str_name = '';
str_name += '<div class="" style="float: left; padding: 10px;">' +
	'<label>' + user.username + ' Mobile' + '</label>' +
			'</div>';
$("#nav-top .nav_tablet_mobile").html(str_name);

function TableList() {
	$.ajax({
		url: "/api/mobile/TablesApi/?shop_id=" + shop_id,
		method: "GET",
		dataType: "json",
		contentType: "application/json"
	}).done(function (data) {
		console.log(data);
		//table01 = [];
		//readTextFile(data, function (text) {
		let str = '';
		let active = ' active';
		let firstTable = data[0].id;
		table_cur = data[0].permalink;
		table_name_to_cart = data[0].name;
		table_id_to_cart = data[0].id;
		$.each(data, function (index, value) {
			var table = value.permalink.replace('"', '');
			if (table01[table] == null) {
				table01[table] = [];
			} else {
				table01[table] = table_temp[table];
			}
			//}
			str += `<div id="${value.permalink}" data-name="${value.name}" class="num-table col-md-2"  onclick="changeTable('${table}',${value.id})">
				<a>				
				<h6>${value.name}</h6>
				</a>
				</div>`;
			active = '';
			//}
		});

		if (table_temp != null) {
			table_cur = table_cur_ls;
			table_name_to_cart = table_name_to_cart_ls;
			table_id_to_cart = table_id_to_cart_ls;
			firstTable = table_id_to_cart_ls;
			//	console.log("bb: " + table_cur);
			table01[table_cur] = table_temp[table_cur];
			//	console.log(table01[table_cur])
			//ve card khi load lai
			total = 0;
			countItem = 0;
			if (table01[table_cur].length == 0) {
				$(".cart-checkout .cart-count-total").html('');
				$(".cart-checkout .cart-money-total span").html("Giỏ Hàng");
				$(".cart-checkout-fix").css("display", "flex");
			} else {
				$.each(table01[table_cur], function () {
					let q = this.item_quantity;
					q *= 1;
					let p = this.item_price;
					total += p * q;
					countItem += q;
				});

				// let str2 = countItem + " | Giỏ hàng " + addCommas(total) + ' đ' + ' ->';
				// $("#btnCheckout").html(str2);
				$(".cart-checkout .cart-count-total").html(countItem);
				$(".cart-checkout .cart-money-total span").html(addCommas(total));
				$(".cart-checkout-fix").css("display", "flex");
			}
			
		} else {
			if (table_cur_ls != null) table_cur = table_cur_ls;
			if (table_name_to_cart_ls != null) table_name_to_cart = table_name_to_cart_ls;
			if (table_id_to_cart_ls != null)
			{
				table_id_to_cart = table_id_to_cart_ls;
				firstTable = table_id_to_cart_ls;
			}
			$(".cart-checkout .cart-count-total").html('');
			$(".cart-checkout .cart-money-total span").html("Giỏ Hàng");
			$(".cart-checkout-fix").css("display", "flex");
		}
		// het ve
		localStorage.setItem('table_cur', table_cur);
		localStorage.setItem('table_name_to_cart', table_name_to_cart);
		localStorage.setItem('table_id_to_cart', table_id_to_cart);
		console.log(table_cur);
		//	localStorage.setItem('table01', JSON.stringify(table01));			
		BillDetailList(firstTable);
		BillOfTable(firstTable)
		$(".list-table-mobile").html(str);
		//changeTable();
		//});	
		$('#' + table_cur).addClass('active');
		var table_name_first = $('#' + table_cur).data('name');
		let s = '';
		s += `<div>
			 <p><b>${table_name_first}</b></p>
			 <p>${user.username}</p>
			 </div>`;
		$("#table-order-name").html(s);
	});
}

$.ajax({
	url: "/api/mobile/CatalogesAPI/?shop_id=" + shop_id,
	method: "GET",
	async: false,
	dataType: "json",
	contentType: "application/json",
	success: function (data) {
		//readTextFile(data, function (text) {
		groupItemCount = data.length;
		let str = '';
		let active = ' active';
		$.each(data, function (index, value) {
			//let data = JSON.parse(text);
			//let k = data.length;			
			//for (let i = 0; i < k; i++) {
			groupItemArray.push(value.id);
			str += `<div class="item-group ${active}">
				  <a href="#group-item-${value.id}">${value.name}</a>
				  </div>`;
			active = '';
			//}
		});

		$(".group-items").html(str);
		changeGroupItem();
		//});	
	}
});

function ProductList() {
	$.ajax({
		url: "/api/mobile/ProductsApi/shop/?shop_id=" + shop_id,
		method: "GET",
		dataType: "json",
		async: false,
		contentType: "application/json",
		success: function (result) {
			let data = result;
			//console.log(result);
			let items;
			let active = ' active';
				for (var i = 0; i < groupItemCount; i++) {
				items = data.filter(function (rs) {
					return rs.catalogesId == groupItemArray[i];
				});
				 
				let j = items.length;
				if (j > 0) {
					let str = `<div id="group-item-${groupItemArray[i]}" class="list-items ${active}">`;
					for (let k = 0; k < j; k++) {
						let avatar = items[k].images != null ? JSON.parse(items[k].images).avatar: "#";
						str += `<div class="item" data-id="${items[k].id}" data-name="${items[k].name}" data-price="${items[k].price}" data-avatar="${avatar}">
							<div class="item-img">
							<img width="50px" height="50px" src="../uploads/products/${items[k].id}/${avatar}" onerror="loadImageError(this)">
							</div>
							<div class="item-info">
							<p><b>${items[k].name}</b></p>
							<p>${addCommas(items[k].price)} vnđ</p>
							</div>							
							</div>`;
					}
					str += '</div>';
					active = '';
					$(".group-list-items-mobile").append(str);
				}
				else {
					let str = `<div id="group-item-${groupItemArray[i]}" class="list-items"></div>`;
					$(".group-list-items-mobile").append(str);
				}
			};
			btnPlus();
		}
	});
}


//readTextFile("json/product.json", function (text) {
//    let data = JSON.parse(text);
//    let items;
//    let active = ' active';
//    for (var i = 0; i < groupItemCount; i++) {
//    	items = data.filter(function(rs) {
//			return rs.group_id == groupItemArray[i];
//		});
//		let j = items.length;
		
//		if(j>0){
//		    let str = '<div id="group-item-'+groupItemArray[i]+'" class="list-items'+active+'">';
//		    for(let k = 0; k < j; k++) {
//		    	str += '<div class="item" data-id="'+items[k].id+'" data-name="'+items[k].name+'" data-price="'+data[k].price+'">'+
//		        			'<div class="item-img">'+
//		        				'<img src="images/'+items[k].avatar+'">'+
//		        			'</div>'+
//		        			'<div class="item-info">'+
//		        				'<p><b>'+items[k].name+'</b></p>'+
//		        				'<p>'+addCommas(items[k].price)+' vnđ</p>'+
//		        			'</div>'+
//		      //   			'<div class="item-btn">'+
//		      //   				'<button class="btn-minus" style="" data-id="'+items[k].id+'" data-name="'+items[k].name+'" data-price="'+data[k].price+'"><i class="fa fa-minus"></i></button>'+
//    				// 				'<span>0</span>'+
//								// '<button class="btn-plus" style="" data-id="'+items[k].id+'" data-name="'+items[k].name+'" data-price="'+data[k].price+'"><i class="fa fa-plus"></i></button>'+
//		      //   			'</div>'+
//		        		'</div>';
//		    }
//		    str += '</div>';
//		    active = '';
//		    $(".group-list-items-mobile").append(str);
//		}
//		else {
//			let str = '<div id="group-item-'+groupItemArray[i]+'" class="list-items"></div>';
//			$(".group-list-items-mobile").append(str);
//		}
//	};
//	btnPlus();
//});

function changeTable(table_per, table_id) {
	//table_per = table_per + "";
	table_cur = table_per;
	localStorage.removeItem('table_cur');
	localStorage.setItem('table_cur', table_per);
	console.log("day la: " + table_per)
	if (table_temp != null) {
		table01[table_per] = table_temp[table_per];
	}

	total = 0;
	countItem = 0;

	if (table01[table_per].length == 0) {
		$(".cart-checkout .cart-count-total").html('');
		$(".cart-checkout .cart-money-total span").html("Giỏ Hàng");
		$(".cart-checkout-fix").css("display", "flex");		
	} else {
		$.each(table01[table_cur], function() {		    
			let q = this.item_quantity;	
				q *= 1;
			    let p = this.item_price;
				total += p * q;
				countItem += q;
		    });
		
		// let str2 = countItem + " | Giỏ hàng " + addCommas(total) + ' đ' + ' ->';
		// $("#btnCheckout").html(str2);
		$(".cart-checkout .cart-count-total").html(countItem);
		$(".cart-checkout .cart-money-total span").html(addCommas(total));
		$(".cart-checkout-fix").css("display", "flex");
	}

	var table_name = $('#'+table_cur).data('name');
	let str = '';
	str +=	`<div>
			<p><b>${table_name}</b></p>
			<p>${user.username}</p>
			</div>`;		
	$("#table-order-name").html(str);

	table_name_to_cart = table_name;
	table_id_to_cart = table_id;
	localStorage.setItem('table_name_to_cart', table_name_to_cart);
	localStorage.setItem('table_id_to_cart', table_id_to_cart);

	console.log(table01);
	BillDetailList(table_id);
	BillOfTable(table_id)
	 // Click table on Mobile
    //$(".num-table a").click(function() {
    //	$(".list-table-mobile .num-table").removeClass("active");
    //	$(this).parent().addClass("active");
    	
    //	$(".main-order").css("display", "none");
    //	$($(this).attr("href")).css("display", "block");

    //	$(".list-table-extend-mobile button").eq(0).css("display", "inline-block");
    //	$(".list-table-extend-mobile button").eq(1).css("display", "none");
    //	$(".list-table-mobile").height(70);
    //	$(".container .main-order-left").css("display", "block");
    //	$(".container .main-order-right").eq(0).css("display", "block");
    //	$(".nav_overlay_mobile").css("display", "none");
    //	return false;
    //});
	$("#list-table .num-table").on("click", function () {
		$(".list-table-mobile .num-table").removeClass("active");
		$(this).addClass("active");

		//tablesId = parseInt($(this).find("a h2").attr("id"));
		//tablesName = $(this).find("a h2").html();

		//$(".main-order").css("display", "none");
		$($(this).find("a").attr("href")).css("display", "block");

		$(".list-table-extend button").eq(0).css("display", "inline-block");
		$(".list-table-extend button").eq(1).css("display", "none");
		$(".list-table-mobile").height(70);
		//$(".container .main-order-left").css("display", "block");
		$(".container .main-order-left").css("visibility", "");
		//$(".container .main-order-right").eq(0).css("display", "block");
		$(".container .main-order-right").eq(0).css("visibility", "");
		$(".nav_overlay").css("display", "none");

		//tables = [];
		//tablesName = $(this).find("h2").html();
		//getBill(tablesId, tablesName);
		return false;
	});
}

function changeGroupItem() {
	$(".item-group a").on("click", function() {
    	$(".item-group").removeClass("active");
    	$(this).parent().addClass("active");
    	$(".group-list-items-mobile .list-items").removeClass("active");
    	$($(this).attr("href")).addClass("active");
    	return false;
    });
}

function btnPlus() {
    // $(".item-btn .btn-plus").click(function() {
    $(".group-list-items-mobile .item").click(function() {
    	$("#btnCheckout").css("display", "block");
        let itemId = $(this).data("id");
    	let name = $(this).data("name");
		let price = $(this).data("price");
		let avatar = $(this).data("avatar");
		//if (avatar == "no-image.png") {
		//	avatar = "#";
  //      }
		let k = false;
		console.log(table_cur);
		$.each(table01[table_cur], function () {
			if (this.item_id == parseInt(itemId)) {
		    	let q = this.item_quantity + 1;
		        this.item_quantity = q;
				this.money = q * price;
		        k = true;
		    }
		});

		if (!k) {			
			let table = new Tables(0, 0,
				parseInt(table_id_to_cart), itemId,
								   name, price,
								   1, price,
								   avatar
			);
			table01[table_cur].push(table);

			//table01[table_cur].push({
   //           	"id": bill_detail_id_max + 1,
			//	"bill_id": 1,
			//	"table_id": table_id_to_cart,
			//	"item_id": itemId,
			//	"item_name": name,
			//	"item_price": price,
			//	"item_quantity": 1,
			//	"money": price,
			//	"avatar": avatar
   //         });
			
	    // 	let str = '' +
		   //  	'<div class="bill-items">' +
			  //   '<div class="col-md-5">'+
			  //   '<p>'+name+'</p>'+
			  //   '<p>Giá: '+addCommas(price)+' vnđ</p>'+
			  //   '</div>'+
			  //   '<div class="col-md-3" style="text-align: center;">'+
			  //   	'<button class="btn-minus"><i class="fa fa-minus"></i></button>'+
					// '<span>'+
					// '1' +
					// '</span>'+
					// '<button class="btn-plus"><i class="fa fa-plus"></i></button>'+
			  //   '</div>'+
			  //   '<div class="col-md-4" style="text-align: right;">'+
			  //   '<p>'+addCommas(price)+' vnđ</p>'+
			  //   '</div>'+
			  //   '</div>';

		   //  $("#table-bill-1").append(str);

			total += price;
			countItem ++;
			// let str2 = countItem + " | Giỏ hàng " + addCommas(total) + ' đ' + ' ->';
			// $("#btnCheckout").html(str2);
			$(".cart-checkout .cart-count-total").html(countItem);
			$(".cart-checkout .cart-money-total span").html(addCommas(total));
	    	
		}
		else {
			// let str = '';
			// for (var i = 0; i < table01.length; i++) {
			//     str += '<div class="bill-items">' +
			// 	    '<div class="col-md-5">'+
			// 	    '<p>'+table01[i].item_name+'</p>'+
			// 	    '<p>Giá: '+addCommas(table01[i].item_price)+' vnđ</p>'+
			// 	    '</div>'+
			// 	    '<div class="col-md-3" style="text-align: center;">'+
			// 	    '<button class="btn-minus"><i class="fa fa-minus"></i></button>'+
			// 		'<span>'+
			// 		table01[i].item_quantity +
			// 		'</span>'+
			// 		'<button class="btn-plus"><i class="fa fa-plus"></i></button>'+
			// 	    '</div>'+
			// 	    '<div class="col-md-4" style="text-align: right;">'+
			// 	    '<p>'+addCommas(table01[i].money)+' vnđ</p>'+
			// 	    '</div>'+
			// 	    '</div>';
			// }
			// $("#table-bill-1").html(str);

			total += price;
			countItem ++;
			// let str2 = countItem + " | Giỏ hàng " + addCommas(total) + ' đ' + ' ->';
	    	// $("#btnCheckout").html(str2);

	    	$(".cart-checkout .cart-count-total").html(countItem);
			$(".cart-checkout .cart-money-total span").html(addCommas(total));

		}
		$(".cart-checkout-fix").css("display", "flex");

		localStorage.setItem('table01', JSON.stringify(table01));
    });
}


function BillOfTable(table_id) {
	$.ajax({
		url: "/api/mobile/BillsApi/?table_id=" + table_id,
		method: "GET",
		dataType: "json",
		contentType: "application/json",
		success: function (result) {
			//readTextFile(data, function (text) {
			let data = result;
			data = data.filter(function (rs) {
				return rs.tablesId == table_id;
			});
			bill_id_cur = data[0].id;

			let str = '';
			str += `<p><b>${addCommas(data[0].sub_total)} vnđ</b></p>
					<p>${addCommas(data[0].fee_service)} vnđ</p>`;
			$("#sub-total-money-1 .col-md-4").html(str);
			//});	
			str = '';
			str += `<p><b>${addCommas(data[0].total_money)}</b></p>`;
				$("#total-money-1 .col-md-4").html(str);
		}
	});
}

function BillDetailList(table_id) {
	$.ajax({
		url: "/api/mobile/BillDetailsApi/?table_id=" + table_id,
		method: "GET",
		dataType: "json",
		contentType: "application/json",
		success: function (result) {
			let data = result;
			bill_detail_id_max = data.length;
			let k = data.length;
			let str1 = '';
			for (let i = 0; i < k; i++) {
				str1 += '<div class="bill-items">' +
					'<div class="col-md-5">' +
					'<p>' + data[i].productsName + '</p>' +
					'<p>Giá: ' + addCommas(data[i].price) + ' vnđ</p>' +
					'</div>' +
					'<div class="col-md-3" style="text-align: center;">' +
					'<button class="btn-minus"><i class="fa fa-minus"></i></button>' +
					'<span>' +
					data[i].quantity +
					'</span>' +
					'<button class="btn-plus"><i class="fa fa-plus"></i></button>' +
					'</div>' +
					'<div class="col-md-4" style="text-align: right;">' +
					'<p>' + addCommas(data[i].price) + ' vnđ</p>' +
					'</div>' +
					'</div>';
			}
			$("#table-bill-1").html(str1);
		}
	});
}

$(document).ready(function () {
	TableList();
	ProductList();
});

