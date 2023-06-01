var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url:'/admin/eventWeb/getall'},
        "columns": [
            { "data": "eventId", "width": "5%" },
            { "data": "eventName", "width": "15%" },
            { "data": "eventDescription", "width": "15%" },
            { "data": "eventDate", "width": "15%" },
            { "data": "location", "width": "10%" },
            { "data": "availableSeats", "width": "8%" },
            { "data": "ticketPrice", "width": "7%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                     <a href="/admin/eventWeb/UpdateEvent?EventId=${data}" class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i> Edit</a>               
                     <a href='/admin/eventWeb/deleteEvent?EventId=${data}' class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"></i> Delete</a>
                    </div>`
                },
                "width": "25%"
            }
        ]
    });
}

