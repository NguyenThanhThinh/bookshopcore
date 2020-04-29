$(function () {
    var placeholderElement = $('#modal-placeholder');
    debugger
    $('button[data-toggle="modal"]').click(function (event) {
        var url = $(this).data('url');
        $.get(url).done(function (data) {
            placeholderElement.html(data);
            placeholderElement.find('.modal').modal('show');
        });
    });

    placeholderElement.on('click', '[data-save="modal"]', function (event) {
        event.preventDefault();

        var form = $(this).parents('.modal').find('form');
        var actionUrl = form.attr('action');
        var dataToSend = form.serialize();
        console.log(dataToSend);
        $.post(actionUrl, dataToSend).done(function (data) {
            console.log(data);
            var newBody = $('.modal-body', data);
            placeholderElement.find('.modal-body').replaceWith(newBody);
            debugger
            var isValid = newBody.find('[name="IsValid"]').val() == 'True';
            if (!isValid) {
                placeholderElement.find('.modal').modal('hide');
            }
        });
    });
});