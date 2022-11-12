﻿function buildPressureChart(data) {
    const chartCtx = document.getElementById('chart').getContext('2d');

    const chart = new Chart(chartCtx, {
        data: {
            labels: data.map(x => x.label),
            datasets: buildPressureChartDatasets(data)
        },
        options: {
            maintainAspectRatio: false,
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
        label: 'pressure',
        data: data.map(x => x.relativePressure),
        backgroundColor: [
            'rgba(255, 99, 132, 0.2)'
        ],
        borderColor: [
            'rgba(255, 99, 132, 1)'
        ],
        borderWidth: 1
    }/*,
    {
        type: 'line',
        label: '% hum out',
        data: data.map(x => x.outHumidity),
        backgroundColor: [
            'rgba(255, 206, 86, 0.2)'
        ],
        borderColor: [
            'rgba(255, 206, 86, 1)'
        ],
        borderWidth: 1
    }*/];
}