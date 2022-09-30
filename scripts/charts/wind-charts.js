class WindChart {
    constructor() {
        this.windChart = null;
        this.windCtx = document.getElementById('wind').getContext('2d');
    }

    build(input) {
        if (this.windChart) {
            this.windChart.destroy();
        }

        console.debug('WindChart.build');

        this.windChart = new Chart(this.windCtx, {
            data: {
                labels: input.data.map(x => x.d),
                datasets: [{
                    type: 'line',
                    label: 'wind',
                    data: input.data.map(x => x.w),
                    backgroundColor: [
                        'rgba(255, 99, 132, 0.2)'
                    ],
                    borderColor: [
                        'rgba(255, 99, 132, 1)'
                    ],
                    borderWidth: 1
                }]
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