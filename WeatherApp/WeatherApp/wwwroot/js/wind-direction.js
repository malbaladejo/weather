function buildWinDirectionChart(data) {
    const chartCtx = document.getElementById('chart').getContext('2d');

    const chart = new Chart(chartCtx, {
        type: 'radar',
        data: {
            labels: data.map(x => x.direction),
            datasets: [{
                label: 'wind directions',
                data: data.map(x => x.value),
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