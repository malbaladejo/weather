function buildWindChart(data) {
    const chartCtx = document.getElementById('chart').getContext('2d');

    const chart = new Chart(chartCtx, {
        data: {
            labels: data.map(x => x.label),
            datasets: buildWindChartDatasets(data)
        },
        options: {
            maintainAspectRatio: false,
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });
}

function buildWindChartDatasets(data) {

    return [{
        type: 'line',
        label: 'wind',
        data: data.map(x => x.wind),
        backgroundColor: [
            'rgba(255, 99, 132, 0.2)'
        ],
        borderColor: [
            'rgba(255, 99, 132, 1)'
        ],
        borderWidth: 1
    }];
}