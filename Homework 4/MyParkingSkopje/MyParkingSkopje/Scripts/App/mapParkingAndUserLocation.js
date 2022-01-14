//Функција која го користи Leaflet API за да ги прикажи локациите на паркингот и корисникот преку мапирање на нивните координати на мапата
$(document).ready(function () {
        var lat = $("#map").attr('latitude');
        var lon = $("#map").attr('longitude');
        var map = L.map('map').setView([lat, lon], 13);
        var tiles = L.tileLayer('https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token=pk.eyJ1IjoibWFwYm94IiwiYSI6ImNpejY4NXVycTA2emYycXBndHRqcmZ3N3gifQ.rJcFIG214AriISLbB6B5aw', {
            maxZoom: 18,
            attribution: 'Map data &copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors, ' +
                'Imagery © <a href="https://www.mapbox.com/">Mapbox</a>',
            id: 'mapbox/streets-v11',
            tileSize: 512,
            zoomOffset: -1
        }).addTo(map);
        var parkingName = $("#pId").attr("parkingName");
        var marker = L.marker([lat, lon]).addTo(map);

        lat = $("#pId").attr('userLatitude');
        lon = $("#pId").attr('userLongitude');
        var marker = L.marker([lat, lon]).addTo(map).bindPopup("Your location");
    });