var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url:'/admin/eventWeb/getall'},
        "columns": [
            { "data": "eventId", "width": "15%" },
            { "data": "eventName", "width": "15%" },
            { "data": "eventDescription", "width": "15%" },
            { "data": "eventDate", "width": "15%" },
            { "data": "location", "width": "10%" },
            { "data": "availableSeats", "width": "8%" },
            { "data": "ticketPrice", "width": "7%" },
            {
                "data": "eventId",
                "render": function (data, type, row) {
                    console.log(data);
                    console.log(type);
                    console.log('row is '+row);
                    return `<div class="w-75 btn-group" role="group">
                     <a href="/admin/eventWeb/UpdateEvent?eventId=${row.eventId}" class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i> Edit</a>               
                   
                     <a onClick=Delete('/admin/eventWeb/deleteEvent/${row.eventId}') class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"></i> Delete</a>
                  
                     </div>`
                },
                "width": "25%"
            }
        ]
    });
   
}


function deleteEvent(eventId) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: `/admin/eventWeb/deleteEvent?eventId=${eventId}`,
                type: 'DELETE',
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                },
                error: function (xhr, status, error) {
                    toastr.error("Error deleting event");
                    console.log(error);
                }
            })
        }
    })
}


