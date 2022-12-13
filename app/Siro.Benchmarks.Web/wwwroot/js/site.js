
var json;
$('#chapter-list-1 li').each(function () {
    json += $(this).children('a').attr('href') + ",";
});
consol.log(json);

