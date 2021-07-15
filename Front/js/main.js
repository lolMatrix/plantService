$(document).ready(function (){
    getGreenhoseList();
});

function getGreenhoseList(){
    var greenhose = getDataFromServer(getHost() + "greenhose");
    greenhose.forEach(element => {
        var html = $('<div>').load("htmlTemplates/data.html", function(){
            html.find(".title").text(element.name);
            html.find(".content").text(element.location);
            html.find(".card").attr("onclick", "getBedsList(" + element.id + ")");
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

    var html = $('<div>').load("htmlTemplates/graph.html", function(){
        drawChart(data, sensorType, getMeseasurementUnit(data));
    });
    $("#content").append(html);

}

function getMeseasurementUnit(data){
    var unit = '';
    if (data.length > 0){
        unit = data[0].meseasurementUnit;
    }

    return unit;
}