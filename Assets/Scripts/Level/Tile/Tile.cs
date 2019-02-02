namespace Level.Tile
{
    public enum TileType
    {
        None,
        Default,
        PlayerGoal,
        BlockGoal
    }
    
    public class Tile: AbstractTile
    {
        public override TileType GetTileType()
        {
            return TileType.Default;
        }
    }
}