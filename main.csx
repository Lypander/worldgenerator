using System;
using System.Drawing;
using System.Windows.Forms;

public class Terrain : Form
{
    public enum TileType { Water, Grass, Forest, Mountain }

    private TileType[,] tiles;
    private Random rand;

    public Terrain()
    {
        Width = 500;
        Height = 500;
        rand = new Random();
        tiles = new TileType[100, 100];

        for (int i = 0; i < 100; i++)
        {
            for (int j = 0; j < 100; j++)
            {
                tiles[i, j] = (TileType)rand.Next(4);
            }
        }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        for (int i = 0; i < 100; i++)
        {
            for (int j = 0; j < 100; j++)
            {
                switch (tiles[i, j])
                {
                    case TileType.Water:
                        e.Graphics.FillRectangle(Brushes.Blue, i * 5, j * 5, 5, 5);
                        break;
                    case TileType.Grass:
                        e.Graphics.FillRectangle(Brushes.Green, i * 5, j * 5, 5, 5);
                        break;
                    case TileType.Forest:
                        e.Graphics.FillRectangle(Brushes.DarkGreen, i * 5, j * 5, 5, 5);
                        break;
                    case TileType.Mountain:
                        e.Graphics.FillRectangle(Brushes.Brown, i * 5, j * 5, 5, 5);
                        break;
                }
            }
        }
    }
}


class Program
{
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new Terrain());
    }
}