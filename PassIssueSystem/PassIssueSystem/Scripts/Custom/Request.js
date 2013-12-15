function AddDetailRow() {
    var Name = $("#Name").val();
    var NIC = $("#NIC").val();

    if (NIC == "") {
        alert('Please fill the details');
    }
    else {
        var Row = "<tr>" + "<td>" + Name + "</td></tr>";
        $(Row).appendTo("#tblRequest");
    }
}

function SubmitData() {
    var isSaved = false;
    var passHed = new Object();

    passHed.PassReqNo = 0;
    passHed.BookTaken = $('#BookTaken').val();
    passHed.ReturnDate = $('#ReturnDate').val();

    var reqCollection = [];   
    $('#BorrowbookTable>tbody>tr>td:nth-child(1)').each(function () {
        reqCollection.push({
            BookId: $(this).text(),
            BorrowBookDetModelID: 0,
            RefferenceID:0
        });
    });

    debugger;
    hedObj.books = booksCollection;
    debugger;
    $.ajax({
        type: "POST",
        url: '/BorrowBook/JsonCreateBorrowBook',
        data: JSON.stringify(hedObj),
        contentType: "application/json; charset=utf-8",
        dataType: "json",      
      
        success: function (msg) {
            debugger;
            alert('Saved!')
            isSaved = msg.isSaved;
        },
        error: function (xhr) {
            debugger;
            alert(xhr);
        }
    });

    return isSaved;
}