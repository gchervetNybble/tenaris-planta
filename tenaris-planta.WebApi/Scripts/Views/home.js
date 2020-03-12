var $table = $('#table')

$(function () {
    var gridData = [];

    $.get("https://localhost:44398/api/email/get", function (data) {

        $(document).on("click", ".delete", function () {
            var rowId = $(this).attr("rowId");
            $(this).parents("tr").remove();
            $(".add-new").removeAttr("disabled");
        });

        if (data) {
            data.hits.forEach(email => {
                gridData.push({
                    'id': email._id,
                    'name': email._source.subject,
                    'price': email._source.to,
                    'action': "<input type=\"button\" class=\"delete btn btn-danger\" value=\"Delete\" rowId=\"" + email._id + "\" />"
                });
            });

            $table.bootstrapTable({ data: gridData })
        }
    });
})