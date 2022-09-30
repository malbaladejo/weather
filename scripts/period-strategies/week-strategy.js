class WeekStrategy {
    constructor() {

    }

    getTemperatureData(input) {
        return [{
            type: 'line',
            label: 'temp in',
            data: input.data.map(x => x.it),
            backgroundColor: [
                'rgba(255, 99, 132, 0.2)'
            ],
            borderColor: [
                'rgba(255, 99, 132, 1)'
            ],
            borderWidth: 1
        },
        {
            type: 'line',
            label: 'temp out',
            data: input.data.map(x => x.ot),
            backgroundColor: [
                'rgba(255, 206, 86, 0.2)'
            ],
            borderColor: [
                'rgba(255, 206, 86, 1)'
            ],
            borderWidth: 1
        }];
    }

    getTitle(data) {
        return data.firstDay + ' - ' + data.lastDay;
    }
}