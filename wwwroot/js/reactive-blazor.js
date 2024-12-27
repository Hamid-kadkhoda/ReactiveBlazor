

async function RenderNewCanvasChart(identifier, labels, dataset, label, type) {

    await setTimeout(() => { }, 0);

    const ctx = document.getElementById(`${identifier}`);

    let chart = new Chart(ctx, {
        type: type,
        data: {
            labels: labels,
            datasets: [{
                label: label,
                data: dataset,
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });

    console.log(chart)
} 