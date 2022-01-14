//jQuery функција која при hover на соодветните елементи курсорот го претвора во поинтер
$(document).ready(function () {
    $("#bookmark, .star, .reviewInfo, a, #back-button").hover(function () {
        $(this).css('cursor', 'pointer');
    });
});