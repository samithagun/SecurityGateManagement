function AddDetailRow() {
    var Name = $("#name").val();
    var NIC = $("#nic").val();

    if (NIC == "") {
        alert('Please fill the details');
    }
    else {
        var Row = "<tr><td></td><td></td></tr>";
        $(Row).appendTo("#reqtable");
    }
}

function SubmitData() {
    var isSaved = false;
    var passReq = new Object();

    passReq.RequiredFrom = $('#datefrom').val();
    passReq.RequiredTo = $('#dateto').val();
    passReq.Comments = $('#comment').val();

    var PassRequestDet = [];
    var PassReqVehicle = [];

    $("table#reqtable > tbody > tr").each(function () {
        PassRequestDet.push({
            PersonName: $('td:eq(0) input', this).val(),
            PersonNIC: $('td:eq(1) input', this).val(),
            MobileNo: $('td:eq(4) input', this).val(),
            PassCode: $('#passtype').val(),
        })
        PassReqVehicle.push({
            VehicleCode: $('td:eq(2) input', this).val(),
            VehicleNo: $('td:eq(3) input', this).val(),
        })
    });

    passReq.PassRequestDets = PassRequestDet;
    passReq.PassReqVehicles = PassReqVehicle;

    $.ajax({
        type: "POST",
        url: '/PassRequest/JsonCreateRequest',
        data: JSON.stringify(passReq),
        contentType: "application/json; charset=utf-8",
        dataType: "json",

        success: function (msg) {
            alert('Request Sent Successfully! Reference No:' + msg.refNo)
            passReq = [];
            return isSaved;
        },
        error: function (xhr) {
            alert(xhr);
        }
    });    
}