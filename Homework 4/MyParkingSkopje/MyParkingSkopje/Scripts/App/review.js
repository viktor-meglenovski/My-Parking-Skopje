//Колекција од jQuery функции кои менаџираат со додавањето, ажурирањето и бришењето на Review на клиентска страна
$(document).ready(function () {
        var starsValue = 0;
        if ($("#existingReviewStarsValue").length != 0) {
            starsValue = $("#existingReviewStarsValue").attr("starsVal");

        }


        //Функција која менаџира со кликање на ѕвездите (земање на точна вредност во опсег од 0 до 5 и прикажување на точен број на обоени и необоени ѕвезди)
        $(".star").on("click", function () {
            var ova = $(this);
            var num = parseInt(ova.attr("id").charAt(4));
            var stars = $(".star");
            for (var i = 0; i < 5; i++) {
                if (i < num) {
                    stars.get(i).setAttribute("src", "/Content/Images/star_full.png");
                }
                else {
                    stars.get(i).setAttribute("src", "/Content/Images/star_empty.png");
                }
            }
            starsValue = num;
        });
        //Функција која го отфрла незачуваното Review
        $("#discardReview").on("click", function () {
            starsValue = 0;
            $("#reviewText").val('');
            var stars = $(".star");
            for (var i = 0; i < 5; i++) {
                stars.get(i).setAttribute("src", "/Content/Images/star_empty.png");
            }
        });
        //Функција која ги испраќа податоците од формата на серверска страна преку AJAX
        $("#submitReview,#editReview").on("click", function () {
            var revText = $("#reviewText").val();
            var parkingId = parseInt($("#pId").attr("pIdAtr"));
            if (revText == "") {
                //Aко не е внесен текст се добива порака
                bootbox.alert("Please write a short comment!");
            }
            else if (starsValue == 0) {
                //Ако не е одбран број на ѕвезди се добива порака
                bootbox.alert("Please select with how many stars you would rate this parking!");
            }
            else {
                //Ако се е во ред се прави соодветниот AJAX повик
                $.ajax({
                    type: "POST",
                    url: '/Review/AddOrEditReview',
                    data: { parkingId: parkingId, stars: starsValue, reviewText: revText },
                    success: function (results) {
                        //Ако добиеме успешен одговор правиме reload на страната
                        window.location.href = "/Parking/Details?id=" + parkingId;
                    },
                    error: function (results) {
                        console.log("error");
                    }
                })
            }
        });
        //Функција која бриши веќе постоечко Review
        $("#deleteReview").on("click", function () {
            var revText = $("#reviewText").val();
            var parkingId = parseInt($("#pId").attr("pIdAtr"));
            //Се отвора Bootbox прозорец каде корисникот треба да потврди дека сака да го избриши својот Review за паркингот
            bootbox.confirm({
                message: "<h2>Are you sure you want to delete your review for this parking?</h2>",
                buttons: {
                    confirm: {
                        label: 'Yes',
                        className: 'btn-success'
                    },
                    cancel: {
                        label: 'No',
                        className: 'btn-danger'
                    }
                },
                //Ако корисникот потврдил се прави соодветниот AJAX повик
                callback: function (result) {
                    $.ajax({
                        type: "GET",
                        url: '/Review/DeleteReview',
                        data: { parkingId: parkingId },
                        //Ако добиеме успешен одговор правиме reload на страната
                        success: function (results) {
                            window.location.href = "/Parking/Details?id=" + parkingId;
                        },
                        error: function (results) {
                            console.log("error");
                        }
                    })
                }
            });
        });
    });