var configObj;
function getHost(){
    if (configObj == null)
        $.ajax({
            url: 'config/config.json',
            async: false,
            success: function (data){
                configObj = data
            }
        });
    
    return configObj.mainUrl;
}