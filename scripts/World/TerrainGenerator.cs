using Godot;

namespace Pasyal.World;

public partial class TerrainGenerator : GridMap
{
    [Export] public int WorldSize = 128; // Size of the GridMap area
    [Export] public int BaseHeight = 5;
    
    public override void _Ready()
    {
        Generate();
    }
    
    public void Generate()
    {
        var noise = new FastNoiseLite();
        noise.Seed = (int)GD.Randi();
        noise.NoiseType = FastNoiseLite.NoiseTypeEnum.Simplex;
        noise.Frequency = 0.02f; // Smooth rolling hills
        
        Clear();
        
        // This relies on a MeshLibrary being assigned to the GridMap.
        // Assuming ID 0 = Dirt, 1 = Grass, 2 = Nipa Hut Planks
        for (int x = -WorldSize/2; x < WorldSize/2; x++)
        {
            for (int z = -WorldSize/2; z < WorldSize/2; z++)
            {
                float noiseVal = noise.GetNoise2D(x, z);
                
                // Scale noise [-1, 1] up to a nice terrain height variance
                int height = BaseHeight + Mathf.FloorToInt((noiseVal + 1f) * 6f);
                
                for (int y = 0; y < height; y++)
                {
                    // Everything beneath the top layer is dirt, top layer is grass
                    int blockId = (y == height - 1) ? 1 : 0; 
                    SetCellItem(new Vector3I(x, y, z), blockId);
                }
            }
        }
        
        GD.Print("Voxel Terrain Generation Complete.");
    }
}
