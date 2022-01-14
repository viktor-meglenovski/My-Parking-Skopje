//jQuery функција која табелата со Reviews ја претвора во Datatable
$(document).ready(function () {
    $("#reviewsTable").DataTable({
        "columns": [
            { "name": "name", "orderable": false },
            { "name": "stars", "orderable": true },
            { "name": "date", "orderable": true },
            { "name": "info", "orderable": false }
        ],
        "ordering": true,
        "bLengthChange": false,
        "searching": false,
        "pageLength": 3,
        "bInfo": false,
        "dom": 'lfBrtip'
    });
});