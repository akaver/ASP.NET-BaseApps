var currentCultureCode; //global variable, created in _Layout.cshtml - et, en, etc

$.when(
    $.get("/bower_components/cldr-data/supplemental/likelySubtags.json"),
    $.get("/bower_components/cldr-data/main/" + currentCultureCode + "/numbers.json"),
    $.get("/bower_components/cldr-data/supplemental/numberingSystems.json"),
    $.get("/bower_components/cldr-data/main/" + currentCultureCode + "/ca-gregorian.json"),
    $.get("/bower_components/cldr-data/main/" + currentCultureCode + "/ca-generic.json"),
    $.get("/bower_components/cldr-data/main/" + currentCultureCode + "/timeZoneNames.json"),
    $.get("/bower_components/cldr-data/supplemental/timeData.json"),
    $.get("/bower_components/cldr-data/supplemental/weekData.json")
).then(function () {
    // Normalize $.get results, we only need the JSON, not the request statuses.
    return [].slice.apply(arguments, [0]).map(function (result) {
        return result[0];
    });
}).then(Globalize.load).then(function () {
    // Initialise Globalize to the current UI culture
    Globalize.locale(currentCultureCode);
    moment.locale(currentCultureCode);
});

$(function () {
    // fix specific locale problems in moment.js
    // moment is not using cldr data yet
    moment.localeData("et")._longDateFormat.LT = "HH:mm";

    // attach bootstrap datetimepicker spinner
    $('[data-type="datetime"]').datetimepicker({ locale: currentCultureCode, format: 'L LT' });
    $('[data-type="date"]').datetimepicker({ locale: currentCultureCode, format: 'L' });
    $('[data-type="time"]').datetimepicker({ locale: currentCultureCode, format: 'LT' });

    //add placeholders, use moment.js formats - since datetimepicker uses it
    $('[data-type="datetime"]').attr('placeholder',
        moment.localeData(currentCultureCode)._longDateFormat.L + " " +
        moment.localeData(currentCultureCode)._longDateFormat.LT);
    $('[data-type="date"]').attr('placeholder', moment.localeData(currentCultureCode)._longDateFormat.L);
    $('[data-type="time"]').attr('placeholder', moment.localeData(currentCultureCode)._longDateFormat.LT);
});
