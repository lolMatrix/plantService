$(document).ready(function (){
    getGreenhoseList();
    $('#createGreenhose').submit(function(e){
        var greenhose = new Object();
        greenhose.name = $('#name').val();
        greenhose.location = $('#location').val();

        sendJson(greenhose, 'greenhose/create', function(){
            getGreenhoseList();
        })
        return false;
    })
})
function getGreenhoseList(){
    $('#content').empty();
    disableNavigation();
    setHiddenAddButtonOnHeader(false);
    var greenhose = getDataFromServer("https://localhost:44359/greenhose");
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
    setHiddenAddButtonOnHeader(true);
    enableNavigation('getGreenhoseList', '');
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
        enableNavigation('getBedsList', data.greenhoseId);
        return [data.name, 'Кол-во воды в баке: ' + data.waterVolume + ' литров.'];
    });
    getList("https://localhost:44359/sensor/get/", id, function(data){
        return [data.name, getType(data.type), 'getSensorDetails(' + data.id + ')'];
    });
    showAddButton();
}

function enableNavigation(backFunction, id){
    $('#navItem').removeClass('hidden');
    $('#navItem').attr('onclick', backFunction + '(' + id + ')');
}

function disableNavigation() {
    $('#navItem').addClass('hidden');
    $('#navItem').attr('onclick', '');
}

function setHiddenAddButtonOnHeader(isDisable){
    if(isDisable)
        $('#addOnHeader').addClass('hidden');
    else
        $('#addOnHeader').removeClass('hidden');
}

function getSensorDetails(id){
    $('#content').empty();

    var sensorType;

    getDetails(getHost() + "sensor/", id, function(data){
        enableNavigation("getSensorList", data.bedId);
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

    hideAddButton();
}

function getMeseasurementUnit(data){
    var unit = '';
    if (data.length > 0){
        unit = data[0].meseasurementUnit;
    }

    return unit;
}

function hideAddButton(){
    $('#add').addClass('hidden');
}
function showAddButton(){
    $('#add').removeClass('hidden');
}