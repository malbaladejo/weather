
function buildDatePicker(controler, action, beginDate) {
    console.log("buildDatePicker " + controler + " " + action + " " + beginDate);
    window.addEventListener('load', () => {
        $(function () {
            initializeDatePicker(controler, action, beginDate);
            stopClickPropagationOnDatePicker();
        })
    });
}

function initializeDatePicker(controler, action, beginDate) {
    $("#datepicker").datepicker(
        {
            onSelect: function (date) {
                if (date) {
                    const url = buildDatePickerUrl(controler, action, date);
                    window.location.href = url;
                }
            }
        });

    $("#datepicker").datepicker('setDate', Date.parse(beginDate));

    if (getCulture()) {
        $("#datepicker").datepicker('option', $.datepicker.regional[getCulture()]);
    }
}

function buildDatePickerUrl(controler, action, dateAsStringInCurrentCultureFormat) {
    const dateArray = dateAsStringInCurrentCultureFormat.split('/');
    const year = 2;
    const culture = getParameterByName('culture');
    let url = '/' + controler + '/' + action + '/?date=' + dateArray[year] + '-' + dateArray[getMonthIndex()] + '-' + dateArray[getDayIndex()];

    if (culture) {
        url = url + "&culture=" + culture;
    }

    return url;
}

function getMonthIndex() {
    if (getCulture() === 'fr') {
        return 1;
    }

    return 0;
}

function getDayIndex() {
    if (getCulture() === 'fr') {
        return 0;
    }

    return 1;
}

function stopClickPropagationOnDatePicker() {
    $(".ui-datepicker").on("click", function (e) {
        e.stopPropagation();
    });
}

function getCulture() {
    const culture = getParameterByName('culture');
    if (!culture) {
        return 'fr';
    }

    return null;
}