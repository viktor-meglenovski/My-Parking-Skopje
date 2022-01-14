//Функција која не дозволува пребарување на паркинзи по име доколку името е празен стринг
$("#nameButton").on("click", function () {
        var text = $("#name").val();
        console.log(text);
    if (text == "") {
        //Доколку името е празен стринг добиваме порака дека треба да внесиме текст
            bootbox.alert("Please fill in the search box with some text!");
        }
        else {
            //Доколку името не е празен стринг прави соодветна редирекција
            window.location.href = "/SearchParkings/ByName?name=" + text;
        }
    });