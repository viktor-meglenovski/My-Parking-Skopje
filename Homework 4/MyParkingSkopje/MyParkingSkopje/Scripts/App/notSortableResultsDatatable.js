//jQuery функција која табелата ја прави Datatable и не овозможува сортирање на податоците
$(document).ready(function () {
        $("#table").DataTable({
            "ordering": false,
            "bLengthChange": false,
            "searching": false,
            "pageLength": 9,
            "bInfo": false,
            "dom": 'lfBrtip'
        });
    });