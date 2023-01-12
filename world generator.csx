using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

public class Game
{

    public enum TileType
    {
        Water,
        Grassland,
        Forest,
        Plains,
        Marsh,
        Hills,
        Mountains,
        Tundra,
        Desert,
        Coast, 
       /* Orchard, 
        Cliffs, 
        WildField,
        DenseForest, 
        Jungle, 
        Bog, 
        MountainSide,*/
        // Add more terrain types here
    }
    public static Dictionary<TileType, Color> _tileTypeColorMapping = new Dictionary<TileType, Color>
{
    {TileType.Water, Color.Blue},
    {TileType.Grassland, Color.LightGreen},
    {TileType.Forest, Color.ForestGreen},
    {TileType.Plains, Color.YellowGreen},
    {TileType.Marsh, Color.CadetBlue},
    {TileType.Hills, Color.SandyBrown},
    {TileType.Mountains, Color.Gray},
    {TileType.Tundra, Color.Snow},
    {TileType.Desert, Color.PaleGoldenrod},
        {TileType.Coast, Color.DarkSeaGreen },
       /* {TileType.Orchard, Color.OliveDrab },
        {TileType.Cliffs, Color.DimGray },
        {TileType.WildField, Color.Plum },
        {TileType.DenseForest, Color.DarkGreen },
        {TileType.Jungle, Color.SeaGreen },
        {TileType.Bog, Color.LightSteelBlue},
        {TileType.MountainSide, Color.SlateGray},*/
    //Add the color of other terrain types here
};

    public static TileType GetRandomTileType()
    {
        Array values = Enum.GetValues(typeof(TileType));
        Random random = new Random(DateTime.Now.Millisecond);
        return (TileType)values.GetValue(random.Next(values.Length));
    }
 
    public static Image GetTileImageForType(TileType type)
    {
        
        var color = _tileTypeColorMapping[type];
        Bitmap bitmap = new Bitmap(12, 12);
        using (Graphics g = Graphics.FromImage(bitmap))
        {
            g.Clear(color);
        }
        return bitmap;
    }

    public class Tile
    {
        public int X { get; set; }
        public int Y { get; set; }
        public TileType Type { get; set; }
        public Image TileImage { get; set; }
        public Rectangle Bounds { get; set; }

        public Tile(int x, int y, TileType type, Image tileImage)
        {
            X = x;
            Y = y;
            Type = type;
            TileImage = tileImage;
            Bounds = new Rectangle(x * 12, y * 12, 12, 12); // assuming each tile is 64x64
        }
        public void Draw(Graphics g)
        {
            g.DrawImage(TileImage, Bounds);
        }
    }

    public class Terrain
    {
        public Tile[,] Tiles { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Terrain(int width, int height)
        {
            Width = width;
            Height = height;
            Tiles = new Tile[width, height];
        }

        public void GenerateTerrain()
        {   



            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    TileType type = GetRandomTileType();
                    Image tileImage = GetTileImageForType(type);
                    Tiles[x, y] = new Tile(x, y, type, tileImage);
                }
            }
        }

        public void Render(Graphics g)
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Tiles[x, y].Draw(g);
                }
            }
        }
    }

    public class GameWindow : Form
    {
        public readonly Terrain _terrain;

        public GameWindow()
        {
            _terrain = new Terrain(128, 128); // creating a 10x10 terrain
            _terrain.GenerateTerrain();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            _terrain.Render(e.Graphics);
        }
    }
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new GameWindow());
    }
}

