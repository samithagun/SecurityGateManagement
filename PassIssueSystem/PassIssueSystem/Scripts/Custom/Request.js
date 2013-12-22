function AddDetailRow() {
    document.getElementById('template').style.display = "block";

    var row = $('#template').clone();                       // Clone the 1st row
    var rowCount = $('#reqtable tr').length;                // No of rows in the table 
    row.id = "reqrow" + rowCount;

    $(row).appendTo("#reqtable");                           // Append the row without any details

    document.getElementById('template').style.display = "none";
}

function SubmitData() {
    var passReq = new Object();

    passReq.RequiredFrom = $('#datefrom').val();
    passReq.RequiredTo = $('#dateto').val();
    passReq.Comments = $('#comments').val();

    var reqDets = [];
    var reqVehi = [];

    $("table#reqtable > tbody > tr").each(function () {

        reqDets.push({
            PersonName: $('td:eq(0) input', this).val(),
            PersonNIC: $('td:eq(1) input', this).val(),
            MobileNo: $('td:eq(4) input', this).val(),
            PassCode: $('#passtype').val(),
        });

        reqVehi.push({
            VehicleCode: $(this).find("#vehitype").val(),
            VehicleNo: $('td:eq(3) input', this).val(),
        });
    });

    passReq.PassRequestDets = reqDets;
    passReq.PassReqVehicles = reqVehi;

    //debugger;
    $.ajax({
        type: "POST",
        url: '/PassRequest/JsonCreateRequest',
        data: JSON.stringify(passReq),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,

        success: function (msg) {
            if (msg.refNo === 0) {
                alert('Request Sending Failed');
            }
            else {
                alert('Request Sent Successfully! Request No: ' + msg.refNo);
                passReq = [];
            }
        },

        error: function (xhr) {
            alert(xhr);
        }
    });
}