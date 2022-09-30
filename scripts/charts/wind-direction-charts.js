
class WindDirectionsChart {
    constructor() {
        this.windDirectionsChart = null;
        this.windDirectionsCtx = document.getElementById('windDirection').getContext('2d');
    }

    build(input) {
        if (this.windDirectionsChart) {
            this.windDirectionsChart.destroy();
        }
        console.debug('WindDirectionsChart.build ');

        this.windDirectionsChart = new Chart(this.windDirectionsCtx, {
            type: 'radar',
            data: {
                labels: input.wd.map(x => x.d),
                datasets: [{
                    label: 'wind directions',
                    data: input.wd.map(x => x.p),
                    fill: true,
                    backgroundColor: 'rgba(255, 99, 132, 0.2)',
                    borderColor: 'rgb(255, 99, 132)',
                    pointBackgroundColor: 'rgb(255, 99, 132)',
                    pointBorderColor: '#fff',
                    pointHoverBackgroundColor: '#fff',
                    pointHoverBorderColor: 'rgb(255, 99, 132)'
                }]
            },
            options: {
                responsive: true,
                elements: {
                    line: {
                        borderWidth: 3
                    }
                }
            }
        });
    }
}