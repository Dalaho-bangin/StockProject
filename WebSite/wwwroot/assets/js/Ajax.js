

var placeholder = $("#modal-placeholder");
$(document).on('click', 'a[data-toggle="modal"]', function () {
    var url = $(this).data('url');
    $.ajax({
        url: url,
        
    }).done(function (result) {
        placeholder.html(result);
        placeholder.find('.modal').modal('show');
    });
});


function ShowMessage(title, text, theme) {
    window.createNotification({
        closeOnClick: true,
        displayCloseButton: false,
        positionClass: 'nfc-bottom-right',
        showDuration: 4000,
        theme: theme !== '' ? theme : 'success'
    })({
        title: title !== '' ? title : 'اعلان',
        message: decodeURI(text)
    });
}

