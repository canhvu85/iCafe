class Tables {
	constructor(billDetailsId, billsId, tablesId, productsId, productsName, price, quantity, total, avatar) {
		this.id = billDetailsId;
		this.bill_id = billsId;
		this.table_id = tablesId;
		this.item_id = productsId;
		this.item_name = productsName;
		this.item_price = price;
		this.item_quantity = quantity;
		this.money = total;
		this.avatar = avatar;
	}

	setId(billDetailsId) {
		this.id = billDetailsId;
	}

	getId() {
		return this.id;
	}

	setBillsId(billsId) {
		this.bill_id = billsId;
	}

	getBillsId() {
		return this.bill_id;
	}

	setTablesId(tablesId) {
		this.table_id = tablesId;
	}

	getTablesId() {
		return this.table_id;
	}

	setProductsId(productsId) {
		this.item_id = productsId;
	}

	getProductsId() {
		return this.item_id;
	}

	setProductsName(productsName) {
		this.item_name = productsName;
	}

	getProductsName() {
		return this.item_name;
	}

	setPrice(price) {
		this.item_price = price;
	}

	getPrice() {
		return this.item_price;
	}

	setQuantity(quantity) {
		this.item_quantity = quantity;
	}

	getQuantity() {
		return this.item_quantity;
	}

	setTotal(total) {
		this.money = total;
	}

	getTotal() {
		return this.money;
	}

	setAvatar(avatar) {
		this.avatar = avatar;
	}

	getAvatar() {
		return this.avatar;
	}
}