window.addEventListener('load', () => {
    buildPressureChart(data);
});

function buildPressureChart(data) {
    const chartCtx = document.getElementById('chart').getContext('2d');

    const chart = new Chart(chartCtx, {
        data: {
            labels: data.map(x => x.label),
            datasets: buildPressureChartDatasets(data)
        },
        options: {
            maintainAspectRatio: false,
            plugins: {
                legend: {
                    position: 'bottom'
                }
            },
            scales: {
                y: {
                    beginAtZero: false
                }
            }
        }
    });
}

function buildPressureChartDatasets(data) {

    return [{
        type: 'line',
        label: pressureLabel,
        data: data.map(x => x.relativePressure),
        elements: {
            point: {
                radius: 0
            }
        },
        backgroundColor: [
            'rgba(255, 99, 132, 0.2)'
        ],
        borderColor: [
            'rgba(255, 99, 132, 1)'
        ],
        borderWidth: 1
    }];
}