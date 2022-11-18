
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

    $("#datepicker").datepicker('setDate', beginDate);
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

function getParameterByName(name, url = window.location.href) {
    name = name.replace(/[\[\]]/g, '\\$&');
    var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, ' '));
}