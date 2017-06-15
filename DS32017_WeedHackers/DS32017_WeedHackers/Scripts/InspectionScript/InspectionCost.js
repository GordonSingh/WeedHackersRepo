var inspectionTotal = document.querySelector("#inspectionTotal");
var inspectionTotalIncVat = document.querySelector("#inspectionTotalIncVat");
var txtQty = document.querySelector("#ServiceRequest_ServiceRequest_UnitQuantity");

var roundTo2nd = function (amount) {
    amount = amount.toString();

    if (amount.indexOf(".") === -1) return amount;

    return amount.split(".")[0] + "." + (amount.split(".")[1] + "00").substring(0, 2);
}

txtQty.addEventListener("keyup", function() {
    try {
        var parsedQty = parseFloat(txtQty.value);
        var totalExclVat = parsedQty * parseFloat(unitPrice);

        if (totalExclVat.toString().toLowerCase() === "nan") {
            throw new Error("Invalid input");
        }

        inspectionTotal.innerHTML = "R"+roundTo2nd(totalExclVat);
        inspectionTotalIncVat.innerHTML = "R"+roundTo2nd(totalExclVat * 1.14);
    } catch (e) {
        inspectionTotal.innerHTML = "Enter a valid qualtity to calculate.";
        inspectionTotalIncVat.innerHTML = "Enter a valid qualtity to calculate.";
    } 
});