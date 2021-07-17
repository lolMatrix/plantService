function getDataFromServer(url) {
    var data;
    $.ajax({
        method: "get",
        url: url,
        async: false,
        crossDomain: true,
        success: function (dataFromServer){
            if (typeof(data) != typeof(object))
                dataFromServer.sort((a, b) => b.id - a.id);
                
            data = dataFromServer
        }
    });

    return data;
}

function getType(type){
    switch (type){
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
    return type;
}

function getDetails(url, id, lambda, formTemplate, onSubmit){
    var details = getDataFromServer(url + id);

    var html = $('<div>').load("htmlTemplates/details.html", function (){
        var array = lambda(details);
        $('title').text(details.name);
        html.find('#title').text(array[0]);
        html.find('#location').text(array[1]);
        $('#content').prepend(html);
        $('#dropdownForm').load(formTemplate, function(){
            $('#parentId').val(details.id);
            $('#detailform').submit(onSubmit);
        });
    });
}

function getList(url, id, lambda, delFunc){
    var list = getDataFromServer(url + id);
    
    list.forEach(element => {
        var array = lambda(element);
        var html = $('<div>').load("htmlTemplates/data.html", function(){
            html.find(".title").text(array[0]);
            html.find(".content").text(array[1]);
            html.find(".card-body").attr("onclick", array[2]);
            html.find('button').removeClass('hidden');
            html.find('button').attr('onclick', array[3]);
        });
        $("#content").append(html);
    });
}

function getDataForMounth(id){
    var first = new Date();
    first.setMonth(first.getMonth() - 1);
    first = first.toISOString();

    var url = getHost() + 'data/get?sensorId=' + id;
    url += '&first=' + first;
    
    var second = new Date();
    url += '&second=' + second.toISOString();
    var data = getDataFromServer(url);
    if (typeof(data) != typeof(object))
        data.sort(function(a, b) {
            return Date.parse(a.timeMeasurement) - Date.parse(b.timeMeasurement);
        });
    else 
        data = null;

    return data;
}

function getDate(date) {
    return Date.parse(date);
}

function parseData(data){
    var array = [];
    data.forEach(element => {
            var object = new Object();
            object.x = Date.parse(element.timeMeasurement);
            object.y = element.value;
            array.push(object);
    });
    return array;
}

function drawChart(data, title, alias){
    var data = parseData(data);
    $("#chart").shieldChart({
            theme: "light",
            primaryHeader: {
            text: title
            },
            exportOptions: {
                    image: false,
                    print: false
            },
            tooltipSettings: {
                    chartBound: true,
                    axisMarkers: {
                            enabled: true,
                            mode: 'xy'
                    } 
            },
            axisX: {
                    axisType: 'datetime'
            },
            dataSeries: [
                    {
                            seriesType: 'line',
                            collectionAlias: alias,
                            data: data 
                    }
            ]
    });
}       

function sendJson(object, url, func){
    $.ajax({
        method: 'POST',
        async: false,
        data: JSON.stringify(object),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        url: getHost() + url,
        success: func
    })
}