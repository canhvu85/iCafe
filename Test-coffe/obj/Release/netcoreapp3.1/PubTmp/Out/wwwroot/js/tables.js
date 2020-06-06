class Cities {
	constructor(cityId, cityName) {
		this.cityId = cityId;
		this.cityName = cityName;
	}
}

class Shops extends Cities {
	constructor(cityId, cityName) {
		super(cityId, cityName);
	}

	setAllProps(id, name) {
		this.id = id;
		this.name = name;
	}
}



//class Tables extends Shops {
//	constructor(shopsId) {
//		super(shopsId);
//	}

//	setAllProps(id, name) {
//		this.id = id;
//		this.name = name;
//	}
//}


class Tables {
	constructor(billDetailsId, billsId, tablesId, productsId, productsName, price, quantity, total, status) {
		this.billDetailsId = billDetailsId;
		this.billsId = billsId;
		this.tablesId = tablesId;
		this.productsId = productsId;
		this.productsName = productsName;
		this.price = price;
		this.quantity = quantity;
		this.total = total;
		this.status = status;
	}

	setBillDetailsId(billDetailsId) {
		this.billDetailsId = billDetailsId;
	}

	getBillDetailsId() {
		return this.billDetailsId;
	}

	setBillsId(billsId) {
		this.billsId = billsId;
	}

	getBillsId() {
		return this.billsId;
	}

	setTablesId(tablesId) {
		this.tablesId = tablesId;
	}

	getTablesId() {
		return this.tablesId;
	}

	setProductsId(productsId) {
		this.productsId = productsId;
	}

	getProductsId() {
		return this.productsId;
	}

	setProductsName(productsName) {
		this.productsName = productsName;
	}

	getProductsName() {
		return this.productsName;
	}

	setPrice(price) {
		this.price = price;
	}

	getPrice() {
		return this.price;
	}

	setQuantity(quantity) {
		this.quantity = quantity;
	}

	getQuantity() {
		return this.quantity;
	}

	setTotal(total) {
		this.total = total;
	}

	getTotal() {
		return this.total;
	}

	setStatus(status) {
		this.status = status;
	}

	getStatus() {
		return this.status;
	}
}
