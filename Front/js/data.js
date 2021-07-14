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