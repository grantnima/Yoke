script.type = "text/javascript";
script.src = "jq/jquery-3.1.0.js";
function cartsjia() {
    var valus1 = $('#carttext').val();
    var valus2 = parseInt(valus1) + 1;
    $('#carttext').val(valus2);
    $('#carttext1').val(valus2);
}
function cartsjian() {
    var valus1 = $('#carttext').val();
    var valus2 = parseInt(valus1) - 1;
    $('#carttext').val(valus2);
    $('#carttext1').val(valus2);
}
