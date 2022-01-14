//jQuery функција која табелата ја прави Datatable и овозможува сортирање на податоците
$(document).ready(function () {
        $("#table").DataTable({
            "columns": [
                { "name": "name", "orderable": false },
                { "name": "distance", "orderable": true },
                { "name": "rating", "orderable": true },
                { "name": "municipality", "orderable": false },
                { "name": "info", "orderable": false }
            ],
            "ordering": true,
            "bLengthChange": false,
            "searching": false,
            "pageLength": 9,
            "bInfo": false,
            "dom": 'lfBrtip'
        });
    });