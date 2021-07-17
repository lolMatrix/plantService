$(document).ready(function (){
    getGreenhoseList();
    $('#createGreenhose').submit(function(e){
        var greenhose = new Object();
        greenhose.name = $('#name').val();
        greenhose.location = $('#location').val();

        sendJson(greenhose, 'greenhose/create', function(){
            getGreenhoseList();
        });
        return false;
    })
});

function deleteGreenhose(id){
    $.ajax({
        method: 'DELETE',
        url: getHost() + 'greenhose/delete/' + id,
        success: function(){
            getGreenhoseList();
            showAlert()
        }
    })
}

function deleteBed(id, greengoseId){
    $.ajax({
        method: 'DELETE',
        url: getHost() + 'bed/delete/' + id,
        success: function(){
            getBedsList(greengoseId);
            showAlert();
        }
    })
}

function deleteSensor(id, bedId){
    $.ajax({
        method: 'DELETE',
        url: getHost() + 'sensor/delete/' + id,
        success: function(){
            getSensorList(bedId);
            showAlert();
        }
    })
}

function getGreenhoseList(){
    $('title').text('Список теплиц');
    $('#content').empty();
    disableNavigation();
    setHiddenAddButtonOnHeader(false);
    var greenhose = getDataFromServer("https://localhost:44359/greenhose");
    greenhose.forEach(element => {
        var html = $('<div>').load("htmlTemplates/data.html", function(){
            html.find(".title").text(element.name);
            html.find(".content").text(element.location);
            html.find(".card-body").attr("onclick", "getBedsList(" + element.id + ")");
            html.find('button').removeClass('hidden');
            html.find('button').attr('onclick', 'deleteGreenhose(' + element.id + ')');
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
    }, 'htmlTemplates/adding/addBed.html', createBed);

    getList("https://localhost:44359/bed/get/", id, function(data){
        return [data.name, 'Кол-во воды в баке: ' + data.waterVolume + ' литров.', 'getSensorList(' + data.id + ')', 'deleteBed(' + data.id + ',' + id + ')'];
    });
}

function createBed(e) {
    var bed = new Object();
        bed.name = $('#internalName').val();
        bed.waterVolume = Number($('#water').val());
        bed.greenhoseId = Number($('#parentId').val());

        sendJson(bed, 'bed/register', function(){
            getBedsList(bed.greenhoseId);
        });
    return false;
}

function createSensor(e) {
    var sensor = new Object();
        sensor.name = $('#internalName').val();
        sensor.type = $('#type').val();
        sensor.bedId = Number($('#parentId').val());

        sendJson(sensor, 'sensor/register', function(){
            getSensorList(sensor.bedId);
        });
    return false;
}

function getSensorList(id){
    $("#content").empty();
    getDetails("https://localhost:44359/bed/", id, function (data){
        enableNavigation('getBedsList', data.greenhoseId);
        return [data.name, 'Кол-во воды в баке: ' + data.waterVolume + ' литров.'];
    }, 'htmlTemplates/adding/addSensor.html', createSensor);
    getList("https://localhost:44359/sensor/get/", id, function(data){
        return [data.name, getType(data.type), 'getSensorDetails(' + data.id + ')', 'deleteSensor(' + data.id + ',' + id + ')'];
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

function showAlert(){
    $('.alert').removeClass('hidden');
    setTimeout(() => {
        $('.alert').addClass('hidden');
    }, 1000);
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