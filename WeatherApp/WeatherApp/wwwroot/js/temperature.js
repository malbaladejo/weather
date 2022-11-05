function buildTemperatureChart(data) {
    const chartCtx = document.getElementById('chart').getContext('2d');

    const chart = new Chart(chartCtx, {
        data: {
            labels: data.map(x => x.label),
            datasets: buildTemperatureDatasets(data)
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

function buildTemperatureDatasets(data) {

    return [{
        type: 'line',
        label: 'temp in',
        data: data.map(x => x.inTemperature),
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
        data: data.map(x => x.outTemperature),
        backgroundColor: [
            'rgba(255, 206, 86, 0.2)'
        ],
        borderColor: [
            'rgba(255, 206, 86, 1)'
        ],
        borderWidth: 1
    }];
}