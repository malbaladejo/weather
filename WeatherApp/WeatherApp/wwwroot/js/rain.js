function buildRainChart(data) {
    const chartCtx = document.getElementById('chart').getContext('2d');

    const chart = new Chart(chartCtx, {
        type: 'bar',
        data: {
            labels: data.map(x => x.label),
            datasets: [{
                label: 'mm rain',
                data: data.map(x => x.rain),
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
            maintainAspectRatio: false,
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });
}