function makeStatistic() {
    var urlFullStr = $("#input_url_data").val();
    var urlArray = urlFullStr.split("\n");
    $("#rezalt tbody").empty();
    var url_base = window.location.origin;
    urlArray.forEach(function (entry) {
    $.ajax({
        url: url_base +'/api/URLCheker/GetStatisticSingleAsync' + entry,
        beforeSend: function (xhr) {
            xhr.overrideMimeType("text/plain; charset=x-user-defined");
        }
    })
        .done(function (data) {
            var rezalt = $.parseJSON(data);
            rezalt.forEach(function (rezalt_entry) {
                console.log(rezalt_entry);
                $('#rezalt tbody').append('<tr><td>' + rezalt_entry.RequestDate + '</td><td>' + rezalt_entry.Url.UrlName + '</td><td>' + rezalt_entry.ResponseCode + '</td><td>' + rezalt_entry.Title +'</td></tr>');
            });
        });
    });
    return false;
}