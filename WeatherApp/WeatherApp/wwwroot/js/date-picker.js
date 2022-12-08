
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

function buildDatePickerUrl(controler, action, date) {
    const dateArray = date.split('/');
    const year = 2;
    const month = 0;
    const day = 1;
    const culture = getParameterByName('culture');
    let url = '/' + controler + '/' + action + '/?date=' + dateArray[year] + '-' + dateArray[month] + '-' + dateArray[day];

    if (culture) {
        url = url + "&culture=" + culture;
    }

    return url;
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