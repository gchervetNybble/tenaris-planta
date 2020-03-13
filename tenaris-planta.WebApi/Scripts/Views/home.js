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
                { 'field': 'date', 'value': 'Fecha' },
                { 'field': 'delete-modal-header', 'value': 'Se necesita confirmación' },
                { 'field': 'delete-modal-body', 'value': 'Para dar de baja el evento {0} es necesario confirmar.' },
                { 'field': 'delete-modal-confirm', 'value': 'Confirmar' },
                { 'field': 'delete-modal-cancel', 'value': 'Cancelar' }
            ]
        },
        {
            'code': 'en-US',
            'values': [
                { 'field': 'name', 'value': 'System name' },
                { 'field': 'priority', 'value': 'Priority' },
                { 'field': 'action', 'value': 'Action' },
                { 'field': 'date', 'value': 'Date' },
                { 'field': 'delete-modal-header', 'value': 'Confirmation needed' },
                { 'field': 'delete-modal-body', 'value': 'Confirmation needed to close event {0}.' },
                { 'field': 'delete-modal-confirm', 'value': 'Confirm' },
                { 'field': 'delete-modal-cancel', 'value': 'Cancel' }
            ]
        }
    ]

    $('.dropdown-item').on('click', (event) => {
        var newLocaleValue = $(event.target).attr("value");

        defaultLocale = newLocaleValue;
        var localeFieldValues = localeArray.find(fieldEl => fieldEl.code === newLocaleValue);

        $table.bootstrapTable("changeLocale", newLocaleValue);
        $table.bootstrapTable("changeTitle", {
            name: localeFieldValues.values.find(fieldEl => fieldEl.field === 'name').value,
            priority: localeFieldValues.values.find(fieldEl => fieldEl.field === 'priority').value,
            action: localeFieldValues.values.find(fieldEl => fieldEl.field === 'action').value,
            date: localeFieldValues.values.find(fieldEl => fieldEl.field === 'date').value,
        });

    });

    $('#confirmationModal').on('show.bs.modal', (event) => {
        var button = $(event.relatedTarget);
        var eventId = button.data('whatever');
        var localeFieldValues = localeArray.find(fieldEl => fieldEl.code === defaultLocale);

        var modal = $(this);
        modal.find('.modal-title').text(localeFieldValues.values.find(fieldEl => fieldEl.field === 'delete-modal-header').value.replace('{0}', eventId));
        modal.find('.modal-body').text(localeFieldValues.values.find(fieldEl => fieldEl.field === 'delete-modal-body').value.replace('{0}', eventId));
        modal.find('.delete-confirm').text(localeFieldValues.values.find(fieldEl => fieldEl.field === 'delete-modal-confirm').value);
        modal.find('.delete-cancel').text(localeFieldValues.values.find(fieldEl => fieldEl.field === 'delete-modal-cancel').value);
        modal.find('.delete-confirm').attr('rowId', eventId);
        modal.find('.modal-body input').val(eventId);
    })

    $(document).on("click", ".delete-confirm", () => {
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
            $.post("api/email/update?id=" + rowId, postData, (data) => {
                if (data && data.result == "updated") {
                    var updatedGridDate = gridData.filter((obj) => {
                        return obj.id !== rowId;
                    });
                    gridData = updatedGridDate;
                    $table.bootstrapTable({ data: updatedGridDate });
                    $table.bootstrapTable('load', updatedGridDate);
                }
            });
        }
    });

    getData = () => {
        $.get("https://localhost:44398/api/email/GetPriority", (data) => {
            if (data) {
                backEndData = data;
                data.hits.forEach(email => {

                    var priority = '';
                    var priorityClass = '';
                    var timestamp = undefined;

                    email._source.tags.forEach(tagElement => {
                        if (tagElement.toLocaleLowerCase() === 'alta' || tagElement.toLocaleLowerCase() === 'media' || tagElement.toLocaleLowerCase() === 'baja') {
                            priority = tagElement;
                            if (tagElement.toLocaleLowerCase() === 'alta') priorityClass = 'text-danger';
                            if (tagElement.toLocaleLowerCase() === 'media') priorityClass = 'text-warning';
                            if (tagElement.toLocaleLowerCase() === 'baja') priorityClass = 'text-success';
                        }
                        timestamp = new Date(email._source["@timestamp"])
                    })
                    gridData.push({
                        'id': email._id,
                        'name': getSystemName(email._source.subject, email._source.message),
                        'timestamp': timestamp,
                        'date':
                            (('0' + timestamp.getDate()).slice(-2)) + '/' +
                            (('0' + Number(timestamp.getMonth() + 1)).slice(-2)) + '/' +
                            (('0' + timestamp.getFullYear()).slice(-2)) + ' ' +
                            (('0' + timestamp.getHours()).slice(-2)) + ':' +
                            (('0' + timestamp.getMinutes()).slice(-2)) + ':' +
                            (('0' + timestamp.getSeconds()).slice(-2)),
                        'priority': '<i class="fas fa-arrow-circle-up ' + priorityClass + ' priority"></i><p class="priority-text">' + priority + '</p>',
                        'action': '<i class="fas fa-times-circle text-danger delete-button-custom" rowId="' + email._id + '" data-whatever="' + email._id + '" data-toggle="modal" data-target="#confirmationModal"></i>'
                    });
                });

                $table.bootstrapTable({ data: gridData, locale: defaultLocale })
            }
        });
    }

    getSystemName = (subject, body) => {
        if (subject) {
            if (subject.endsWith('is Down') && subject.indexOf('Critical: ') != -1) {
                return (subject.split('Critical: ')[1]).split(' is Down')[0];
            } else if (body) {
                if (body.indexOf('Critical') != -1 && body.indexOf('Node: ') != -1) {
                    return (body.split('Node: ')[1]).split('\n')[0];
                }

            }
            else
                return '';
        }
    }

    refresh = () => {
        debugger;
        getData();
    }

    getData();
})