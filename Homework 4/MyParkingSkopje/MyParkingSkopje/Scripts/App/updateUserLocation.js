//jQuery функција која користи AJAX за да ги испрати координатите на корисникот на серверска страна
    $(document).ready(function () {
        if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
            var lattitude = position.coords.latitude;
            var longitude = position.coords.longitude;
            $.ajax({
                url: "/Profile/UpdateUserLocation",
                data: { lattitude, longitude },
            });
        });
        }
    });