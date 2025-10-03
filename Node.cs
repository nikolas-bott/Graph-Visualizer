using System.ComponentModel;

public class Node
{
    public Rectangle Bounds;
    public String Text;
    public Brush brush;
    public bool isClicked;

    public Node(int x, int y, int size, String text)
    {
        Bounds = new Rectangle(x, y, size, size);
        Text = text;
        brush = Brushes.Cyan;
        isClicked = false;
    }

    public Point Center => new Point(Bounds.X + Bounds.Width / 2, Bounds.Y + Bounds.Height / 2);
    public int TriggerClick()
    {
        if (isClicked)
        {
            brush = new SolidBrush(Color.Cyan); // create a new brush
            isClicked = false;
            return -1;
        }
        else
        {
            brush = new SolidBrush(Color.Red); // create a new brush
            isClicked = true;
            return 1;
        }
    }
    
}
