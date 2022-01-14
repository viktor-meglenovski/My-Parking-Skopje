//jQuery функција која преку AJAX испраќа слика за зачувување на серверска страна - се користи за прикажување на профилни слики пред да се зачуваат комплетно на сервер и во база
    $(document).ready(function () {
        $("#ImageUpload").change(function () {
            var formData = new FormData();
            var totalFiles = document.getElementById("ImageUpload").files.length;
            if (totalFiles == 0)
                return;
            var file = document.getElementById("ImageUpload").files[0];
            formData.append("ImageUpload", file);
            $.ajax({
                type: "POST",
                url: '/Profile/ProfileImageUpload',
                data: formData,
                dataType: 'json',
                contentType: false,
                processData: false,
                success: function (response) {
                    if (response.success) {
                        $("#ProfilePicture").attr("src", response.newImagePath);
                    }
                },
                error: function (response) {
                    console.log("error");
                }
            }).done(function () {
            }).fail(function (xhr, status, errorThrown) {
            });
        });
                });