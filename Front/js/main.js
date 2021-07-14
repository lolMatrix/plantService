$.ajax({
    method: "get",
    url: "https://localhost:44359/greenhose",
    crossDomain: true,
    success: function (data){
        data.forEach(element => {
            var html = $('<div>').load("htmlTemplates/data.html", function(){
                html.find(".title").text(element.name);
                html.find(".content").text(element.location);
                html.find(".card").attr("onclick", "getBedsList(" + element.id + ")");
            });
            $("#content").append(html);
        });
    }
});

function getBedsList(id){
    $("#content").empty();
    $.ajax({
    method: "get",
    url: "https://localhost:44359/bed/get/" + id,
    crossDomain: true,
    success: function (data){
        data.forEach(element => {
            var html = $('<div>').load("htmlTemplates/data.html", function(){
                html.find(".title").text(element.name);
                html.find(".content").text('Кол-во воды: ' + element.waterVolume + ' литров');
                html.find(".card").attr("onclick", "fuck(" + element.id + ")");
            });
            $("#content").append(html);
        });
    }
});
}