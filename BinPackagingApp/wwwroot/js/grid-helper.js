window.getGridPosition = (gridId, clientX, clientY, gridWidth, gridHeight) => {
    const gridElement = document.getElementById(gridId);
    if (!gridElement) {
        return [0, 0];
    }
    
    // Get grid bounding rectangle
    const gridRect = gridElement.getBoundingClientRect();
    
    // Calculate position relative to grid (accounting for scroll if any)
    const relativeX = clientX - gridRect.left;
    const relativeY = clientY - gridRect.top;
    
    // Get computed styles to account for padding, border, and gap
    const computedStyle = window.getComputedStyle(gridElement);
    const paddingLeft = parseFloat(computedStyle.paddingLeft) || 0;
    const paddingTop = parseFloat(computedStyle.paddingTop) || 0;
    const gap = parseFloat(computedStyle.gap) || 2;
    
    // Adjust for padding
    const x = relativeX - paddingLeft;
    const y = relativeY - paddingTop;
    
    // Get the first cell to determine cell size
    const firstCell = gridElement.querySelector('.grid-cell');
    if (!firstCell) {
        return [0, 0];
    }
    
    const cellRect = firstCell.getBoundingClientRect();
    const cellWidth = cellRect.width;
    const cellHeight = cellRect.height;
    
    // Calculate grid position
    // Account for gap between cells
    const totalCellWidth = cellWidth + gap;
    const totalCellHeight = cellHeight + gap;
    
    // Calculate which cell contains the point
    let gridX = Math.floor(x / totalCellWidth);
    let gridY = Math.floor(y / totalCellHeight);
    
    // Handle edge case: if we're in the gap area, snap to the cell before the gap
    const remainderX = x % totalCellWidth;
    const remainderY = y % totalCellHeight;
    
    if (remainderX > cellWidth) {
        // We're in a gap horizontally, use the cell before
        gridX = Math.max(0, gridX);
    }
    if (remainderY > cellHeight) {
        // We're in a gap vertically, use the cell before
        gridY = Math.max(0, gridY);
    }
    
    // Clamp to grid bounds
    gridX = Math.max(0, Math.min(gridX, gridWidth - 1));
    gridY = Math.max(0, Math.min(gridY, gridHeight - 1));
    
    return [gridX, gridY];
};

window.getDragData = (dataTransfer) => {
    try {
        return dataTransfer.getData('text/plain');
    } catch (e) {
        return '';
    }
};

window.setDragData = function (e, designation) {
    if (e && e.dataTransfer) {
        e.dataTransfer.setData("text/plain", designation);
    }
};

