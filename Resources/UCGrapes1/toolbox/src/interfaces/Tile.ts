export interface Tile {
    TileId: string;
    TileName?: string;
    TileText?: string;
    TileColor?: string;
    TileAlign?: string;
    TileIcon?: string;
    TileBGColor?: string;
    TileBGImageUrl?: string;
    TileBGImageOpacity?: number;
    Permissions?: [];
    Action?: {
        ObjectType?: string;
        ObjectId?: string;
        ObjectUrl?: string;
    };
}