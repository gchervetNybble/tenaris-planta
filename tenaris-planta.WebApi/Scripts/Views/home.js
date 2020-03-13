var $table = $('#table')

$(function () {
    var defaultLocale = 'es-ES';

    var gridData = [];
    var backEndData = [];
    var localeArray = [
        {
            'code': 'es-ES',
            'values': [
                { 'field': 'name', 'value': 'Nombre del equipo' },
                { 'field': 'priority', 'value': 'Prioridad' },
                { 'field': 'action', 'value': 'Acción' },
                { 'field': 'delete-button', 'value': 'Eliminar' }
            ]
        },
        {
            'code': 'en-US',
            'values': [
                { 'field': 'name', 'value': 'System name' },
                { 'field': 'priority', 'value': 'Priority' },
                { 'field': 'action', 'value': 'Action' },
                { 'field': 'delete-button', 'value': 'Delete' }
            ]
        }
    ]

    $('.dropdown-item').on('click', function () {
        var newLocaleValue = $(this).attr("value");

        defaultLocale = newLocaleValue;
        var localeFieldValues = localeArray.find(fieldEl => fieldEl.code === newLocaleValue);

        $table.bootstrapTable("changeLocale", newLocaleValue);
        $table.bootstrapTable("changeTitle", {
            name: localeFieldValues.values.find(fieldEl => fieldEl.field === 'name').value,
            priority: localeFieldValues.values.find(fieldEl => fieldEl.field === 'priority').value,
            action: localeFieldValues.values.find(fieldEl => fieldEl.field === 'action').value,
        });

    });

    $('#confirmationModal').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget);
        var eventId = button.data('whatever');

        var modal = $(this);
        modal.find('.modal-title').text('Confirmation request ' + eventId);
        modal.find('.delete-row').attr('rowId', eventId);
        modal.find('.modal-body input').val(eventId);
    })

    $(document).on("click", ".delete-row", function () {
        debugger;
        var rowId = $(this).attr("rowId");
        var rowItem = backEndData.hits.find(rowEl => rowEl._id === rowId);
        if (rowItem) {

            rowItem._source.tags.push("Done");

            var postData = {
                'doc': {
                    'tags': rowItem._source.tags
                }
            }
            $.post("api/email/update?id=" + rowId, postData, function (data) {
                if (data && data.result == "updated") {
                    var updatedGridDate = gridData.filter(function (obj) {
                        return obj.id !== rowId;
                    });
                    gridData = updatedGridDate;
                    $table.bootstrapTable({ data: updatedGridDate });
                    $table.bootstrapTable('load', updatedGridDate);
                }
            });
        }
    });

    $.get("https://localhost:44398/api/email/GetPriority", function (data) {
        if (data) {
            backEndData = data;
            data.hits.forEach(email => {

                var priority = '';
                var priorityClass = '';
                var localeFieldValues = localeArray.find(fieldEl => fieldEl.code === defaultLocale);
                email._source.tags.forEach(tagElement => {
                    if (tagElement.toLocaleLowerCase() === 'alta' || tagElement.toLocaleLowerCase() === 'media' || tagElement.toLocaleLowerCase() === 'baja') {
                        priority = tagElement;
                        if (tagElement.toLocaleLowerCase() === 'alta') priorityClass = 'text-danger';
                        if (tagElement.toLocaleLowerCase() === 'media') priorityClass = 'text-warning';
                        if (tagElement.toLocaleLowerCase() === 'baja') priorityClass = 'text-success';
                    }
                })
                gridData.push({
                    'id': email._id,
                    'name': email._id,
                    'priority': '<i class="fas fa-arrow-circle-up ' + priorityClass + ' priority"></i><p class="priority-text">' + priority + '</p>',
                    'action': '<i class="fas fa-times-circle text-danger delete-button-custom" rowId="' + email._id + '" data-whatever="' + email._id + '" data-toggle="modal" data-target="#confirmationModal"></i>'
                });
            });

            $table.bootstrapTable({ data: gridData, locale: defaultLocale })
        }
    });
})