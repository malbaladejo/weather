class RainChart {
    constructor() {
        this.rainChart = null;
        this.rainCtx = document.getElementById('rain').getContext('2d');
    }

    build(input) {
        if (this.rainChart) {
            this.rainChart.destroy();
        }

        console.debug('RainChart.build ');

        this.rainChart = new Chart(this.rainCtx, {
            type: 'bar',
            data: {
                labels: input.data.map(x => x.d),
                datasets: [{
                    label: 'mm rain',
                    data: input.data.map(x => x.r),
                    backgroundColor: [
                        'rgba(54, 162, 235, 0.2)'
                    ],
                    borderColor: [
                        'rgba(54, 162, 235, 1)'
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