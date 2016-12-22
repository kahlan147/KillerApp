function InsertIntoInventory(item) {
    var Item = item;
    var InventoryList = document.getElementById("InventoryList");
    var Option = document.createElement("option");
    Option.text = Item.Name;
    InventoryList.add(Option);
}

function CreateInventory(MyInventory) {
    var Inventory = MyInventory
    var length = Inventory.length;
    var InventoryList = document.getElementById("InventoryList");
    for (var i = 0; i < length; i++) {
        var option = document.createElement("option");
        option.text = Inventory[i].Name;
        InventoryList.add(option);
    }
}