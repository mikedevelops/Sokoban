namespace Level.Tile
{
    public enum TileType
    {
        None,
        Default,
        PlayerGoal,
        BlockGoal,
        BlockWall
    }
    
    public class Tile: AbstractTile
    {
        public override TileType GetTileType()
        {
            return TileType.Default;
        }
    }
}