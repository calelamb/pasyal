export default function PixelArt({ grid, cellSize = 4, className = '' }) {
  if (!grid || !Array.isArray(grid) || grid.length === 0 || !grid[0]) return null;

  const cols = grid[0].length;

  return (
    <div className={`inline-flex ${className}`}>
      <div
        className="pixel-art-grid"
        style={{
          display: 'grid',
          gridTemplateColumns: `repeat(${cols}, ${cellSize}px)`,
          imageRendering: 'pixelated',
        }}
      >
        {grid.flat().map((color, i) => (
          <div
            key={i}
            style={{
              width: cellSize,
              height: cellSize,
              backgroundColor: color || 'transparent',
            }}
          />
        ))}
      </div>
    </div>
  );
}
