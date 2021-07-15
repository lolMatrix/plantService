$(document).ready(function (){
    getGreenhoseList();
});

function getGreenhoseList(){
    $('title').text("Список теплиц");
    var greenhose = getDataFromServer(getHost() + "greenhose");
    greenhose.forEach(element => {
        var html = $('<div>').load("htmlTemplates/data.html", function(){
            html.find(".title").text(element.name);
            html.find(".content").text(element.location);
            html.find(".card").attr("onclick", "getBedsList(" + element.id + ")");
            html.find(".card").hover(function (){
                console.log("hovered")
            });
        });

        $("#content").append(html);
    });
}

function getBedsList(id){
    $("#content").empty();
    getDetails(getHost() + "greenhose/", id, function(data){
        return [data.name, data.location];
    });
    getList(getHost() + "bed/get/", id, function(data){
        return [data.name, 'Кол-во воды в баке: ' + data.waterVolume + ' литров.', 'getSensorList(' + data.id + ')'];
    })
}

function getSensorList(id){
    $("#content").empty();
    getDetails(getHost() + "bed/", id, function (data){
        return [data.name, 'Кол-во воды в баке: ' + data.waterVolume + ' литров.'];
    });
    getList(getHost() + "sensor/get/", id, function(data){
        return [data.name, getType(data.type), 'getSensorDetails(' + data.id + ')'];
    });
}

function getSensorDetails(id){
    $('#content').empty();

    var sensorType;

    getDetails(getHost() + "sensor/", id, function(data){
        sensorType = getType(data.type);
        return [data.name, sensorType];
    });

    var data = getDataForMounth(id);

    var html;
    if (data != null){
        html = $('<div>').load("htmlTemplates/graph.html", function(){
            drawChart(data, sensorType, getMeseasurementUnit(data));
        });
    } else {
        html = $('<div>').load("htmlTemplates/data.html", function(){
            html.find(".title").text("Пока нет данных для этого датчика");
            html.find(".content").text("Но, его можно настроить. Индентификатор датчика: " + id);
        });
    }

    $("#content").append(html);

}

function getMeseasurementUnit(data){
    var unit = '';
    if (data.length > 0){
        unit = data[0].meseasurementUnit;
    }

    return unit;
}