//jQuery функција која праќа AJAX повик за да ги добие потребните информации за даденото Review и отвора Bootbox прозор каде ги прикажува резултатите
$(document).ready(function () {
        $(".reviewInfo").on("click", function (event) {
            var infoButton = $(this);
            var reviewId = parseInt(infoButton.attr("reviewId"));
            $.ajax({
                type: "GET",
                url: '/Review/GetReviewDetails',
                data: { id: reviewId },
                success: function (results) {
                    bootbox.dialog({
                        message: results,
                        title: "<h1>Review Details<h1>",
                        buttons: {
                            success: {
                                label: "OK",
                                className: "btn-success",
                            },
                        }
                    });
                },
                error: function (results) {
                    console.log("error");
                }
            }).done(function () {
            }).fail(function (xhr, status, errorThrown) {
            });
        });
    });