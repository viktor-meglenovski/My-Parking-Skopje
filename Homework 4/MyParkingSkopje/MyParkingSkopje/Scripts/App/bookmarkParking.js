//jQuery функција која испраќа AJAX повик за Bookmarking на паркинг при клик на срцето прикажано на страната
$(document).ready(function () {
    $("#bookmark").on("click", function (event) {
        var heart = $(this)
        $.ajax({
            url: "/Parking/BookmarkParking/" + heart.attr("projectId"),
            success: function (results) {
                if (results.newState == true) {
                    //Ако паркингот e Bookmarked да се прикажи обоено срце
                    heart.attr("src", "/Content/Images/heart.png")
                }
                else {
                    //Ако паркингот не е Bookmarked да се прикажи необоено срце
                    heart.attr("src", "/Content/Images/heart_empty.png")
                }
            },
            error: function (results) {
                console.log("error");
            }
        });
    });
});