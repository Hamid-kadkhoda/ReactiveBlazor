

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
} 

window.blazorDialog = {
    addVisibleClass: (element) => {
        element.classList.add('dialog-visible');
    },
    removeVisibleClass: (element) => {
        element.classList.remove('dialog-visible');
    },
    focusFirstInteractiveElement: (container) => {
        const focusable = container.querySelector('button, [href], input, select, textarea, [tabindex]:not([tabindex="-1"])');
        focusable?.focus();
    }
};