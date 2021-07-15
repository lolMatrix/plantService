function getDataFromServer(url) {
    var data;
    $.ajax({
        method: "get",
        url: url,
        async: false,
        crossDomain: true,
        success: function (dataFromServer){
            data = dataFromServer
        }
    });

    return data;
}

function getDetails(url, id, lambda){
    var details = getDataFromServer(url + id);

    var html = $('<div>').load("htmlTemplates/details.html", function (){
        var array = lambda(details);
        html.find('#title').text(array[0]);
        html.find('#location').text(array[1]);
        $('#content').prepend(html);
    });
}

function getList(url, id, lambda){
    var list = getDataFromServer(url + id);
    
    list.forEach(element => {
        var array = lambda(element);
        var html = $('<div>').load("htmlTemplates/data.html", function(){
            html.find(".title").text(array[0]);
            html.find(".content").text(array[1]);
            html.find(".card").attr("onclick", array[2]);
        });
        $("#content").append(html);
    });
}

function getGraph(url, id, lambda){
    var graph = getDataFromServer(url + id);
    
    graph.forEach(element => {
        var array = lambda(element);
        var html = $('<div>').load("htmlTemplates/graph.html", function(){
            html.find(".title").text(array[0]);
            html.find(".content").text(array[1]);
            html.find(".card").attr("onclick", array[2]);
        });
        
        $("#content").append(html);
    });

   
    
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

function drawChart(data){
    var data = parseData(data);
    $("#chart").shieldChart({
            theme: "light",
            primaryHeader: {
            text: "Температура воздуха"
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
                            collectionAlias: "Температура воздуха (в градусах по Цельсию)",
                            data: data 
                    }
            ]
    });
}       

