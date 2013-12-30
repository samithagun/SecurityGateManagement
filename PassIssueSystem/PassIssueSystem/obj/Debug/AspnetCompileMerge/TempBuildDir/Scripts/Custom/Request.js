
function AddDetailRow() {
    document.getElementById('template').style.display = "table-row";

    var row = $('#template').clone();                       // Clone the 1st row
    var rowCount = $('#reqtable tr').length;                // No of rows in the table 
    row.id = "reqrow" + rowCount;

    $(row).appendTo("#reqtable");                           // Append the row without any details

    document.getElementById('template').style.display = "none";
}

function SubmitData() {

    var passReq = new Object();
    var reqDets = [];
    var reqVehi = [];

    var dFrom = new Date($('#datefrom').val());
    passReq.RequiredFrom = dFrom;
    //passReq.RequiredFrom = $('#datefrom').val();

    var dTo = new Date($('#dateto').val());
    passReq.RequiredTo = dTo;
    //passReq.RequiredTo = $('#dateto').val();

    if (passReq.RequiredTo < passReq.RequiredFrom) {
        alert('Invalid date range');
        return;
    }

    passReq.Comments = $('#comments').val();
    if (passReq.Comments == "") {
        alert('Comments are required');
        return;
    }

    //var row = $('table#reqtable > tbody > tr#reqrow1');
    var name = document.getElementById('name1');
    var nic = document.getElementById('nic1');
    if (name.value == "" || nic.value == "") {
        alert('At least 1 person details are required');
        return;
    }

    $("table#reqtable > tbody > tr").each(function () {

        reqDets.push({
            PersonName: $('td:eq(0) input', this).val(),
            PersonNIC: $('td:eq(1) input', this).val(),
            MobileNo: $('td:eq(4) input', this).val(),
            PassCode: $('#passtype').val(),
        });

        reqVehi.push({
            VehicleCode: $(this).find("#vehitype1").val(),
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
                alert('Request sending failed');
            }
            else {
                alert('Request sent successfully! Request No: ' + msg.refNo);
                passReq = [];
            }
        },

        error: function (xhr) {
            alert(xhr);
        }
    });
}