
let RefreshButton = document.querySelector(".refresh-view-button");
let isDragging = false;
let startY = 0;
const MAX_DRAG_PERCENT = 20; // Maximum drag distance as percentage of window height

document.addEventListener("mousedown", (e) => {
    isDragging = true;
    startY = e.clientY;
    
    document.addEventListener("mousemove", handleMouseMove);
});

function handleMouseMove(event) {
    if (!isDragging) return;
    
    const deltaY = event.clientY - startY;
    const maxDrag = (window.innerHeight * MAX_DRAG_PERCENT) / 100;
    
    // Limit the drag distance
    const newY = Math.min(deltaY, maxDrag);
    if (newY >= 0) {
        RefreshButton.style.transform = `translateY(${newY}px)`;
    }
}

document.addEventListener("mouseup", () => {
    isDragging = false;
    
    // Get current Y position before resetting
    const currentY = RefreshButton.style.transform.match(/translateY\((\d+)px\)/)?.[1] || 0;
    const maxDrag = (window.innerHeight * MAX_DRAG_PERCENT) / 100;
    
    // Trigger refresh only if threshold was reached when released
    if (currentY >= maxDrag) {
        DotNet.invokeMethodAsync('YourBlazorAppNamespace', 'HandleRefresh');
    }
    
    // Reset button position
    RefreshButton.style.transform = 'translateY(0)';
    document.removeEventListener("mousemove", handleMouseMove);
});


