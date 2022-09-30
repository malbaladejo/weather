class TemperatureChart {
    constructor() {
        this.temperatureChartStrategyFactory = new PeriodStrategyFactory();
        this.temperatureChart = null;
        this.temperatureCtx = document.getElementById('temperature').getContext('2d');
    }

    build(input) {
        if (this.temperatureChart) {
            this.temperatureChart.destroy();
        }

        const dataExtractStrategy = this.temperatureChartStrategyFactory.create(input);

        this.temperatureChart = new Chart(this.temperatureCtx, {
            data: {
                labels: input.data.map(x => x.d),
                datasets: dataExtractStrategy.getTemperatureData(input)
            },
            options: {
                responsive: true,
                scales: {
                    y: {
                        beginAtZero: true
                    }
                }
            }
        });
    }
}