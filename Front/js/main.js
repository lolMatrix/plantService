var greenhose = getDataFromServer("https://localhost:44359/greenhose");
greenhose.forEach(element => {
    var html = $('<div>').load("htmlTemplates/data.html", function(){
        html.find(".title").text(element.name);
        html.find(".content").text(element.location);
        html.find(".card").attr("onclick", "getBedsList(" + element.id + ")");
    });
    $("#content").append(html);
});

function getBedsList(id){
    $("#content").empty();
    getDetails("https://localhost:44359/greenhose/", id, function(data){
       return [data.name, data.location];
    });
    getList("https://localhost:44359/bed/get/", id, function(data){
        return [data.name, 'Кол-во воды в баке: ' + data.waterVolume + ' литров.', 'getSensorList(' + data.id + ')'];
    })
}

function getSensorList(id){
    $("#content").empty();
    getDetails("https://localhost:44359/bed/", id, function (data){
        return [data.name, 'Кол-во воды в баке: ' + data.waterVolume + ' литров.'];
    });
    getList("https://localhost:44359/sensor/get/", id, function(data){
        var type = "";
        switch (data.type){
            case "WaterSensor":
                type = "Сенсор влажности почвы";
                break;
            case "AirSensor":
                type = "Сенсор воздуха";
                break;
            case "TemperatureSensor":
                type = "Сенсор температуры";
                break;
        }
        return [data.name, type, 'getCharts(' + data.id + ')'];
    });
}
