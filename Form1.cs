namespace firstporgram;

using System;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

public partial class Form1 : Form
{
    private List<Node> nodes = new List<Node>();
    private bool[,] edges = new bool[1000, 1000];
    private int selected = 0;
    private int indexOfFirstSelected = -1;
    public Form1()
    {
        InitializeComponent();

        Panel settingsPanel = new()
        {
            Width = 200,   // fixed width for settings
            Dock = DockStyle.Left, // sticks to the left side
            BackColor = Color.LightGray // just to see it
        };
        Controls.Add(settingsPanel);

        RadioButton option1 = new RadioButton();
        option1.Text = "Add verticies";
        option1.Left = 20;
        option1.Top = 100;
        option1.Checked = true; // initially selected

        // Create the second radio button
        RadioButton option2 = new RadioButton();
        option2.Text = "Add edges";
        option2.Left = 20;
        option2.Top = 130;

        settingsPanel.Controls.Add(option1);
        settingsPanel.Controls.Add(option2);

        Panel playground = new()
        {
            Dock = DockStyle.Fill, // takes all remaining space
            BackColor = Color.AliceBlue
        };


        playground.MouseClick += (s, e) =>
        {
            if (option1.Checked)
            {
                Point clickPoint = e.Location;
                Node node = new(clickPoint.X, clickPoint.Y, 50, nodes.Count.ToString());
                nodes.Add(node);
                playground.Invalidate();
            }
            if (option2.Checked)
            {
                Point clickPoint = e.Location;
                int clickedNodeIndex = getClickedNodeIndex(clickPoint);
                if (clickedNodeIndex != -1)
                {
                    int value = nodes[clickedNodeIndex].TriggerClick();
                    if (value == 1)
                    {
                        if (selected + 1 > 2)
                        {
                            nodes[clickedNodeIndex].TriggerClick();
                        }
                        else
                        {
                            selected++;
                        }

                        if (selected == 2)
                        {
                            edges[clickedNodeIndex, indexOfFirstSelected] = true;
                            edges[indexOfFirstSelected, clickedNodeIndex] = true;

                            nodes[clickedNodeIndex].TriggerClick();
                            nodes[indexOfFirstSelected].TriggerClick();
                            selected = 0;

                        }
                        indexOfFirstSelected = clickedNodeIndex;


                    }
                    else
                    {
                        selected--;
                    }


                    playground.Invalidate();

                }

            }
        };


        playground.Paint += Playground_Paint;

        Controls.Add(playground);

    }

    private void Playground_Paint(object sender, PaintEventArgs e)
    {
        Graphics g = e.Graphics;

        for (int i = 0; i < nodes.Count; i++)
        {
            Node node = nodes[i];
            Rectangle rect = node.Bounds;
            g.FillEllipse(node.brush, rect);
            g.DrawEllipse(Pens.Black, rect);
            g.DrawString(node.Text, Font, Brushes.Black, node.Center);
        }


        for (int i = 0; i < edges.GetLength(0); i++){
            for (int j = 0; j < edges.GetLength(1); j++)
            {
                if (edges[i, j])
                {
                    g.DrawLine(Pens.Black, nodes[i].Center, nodes[j].Center);
                }
            }
        }
        
      
    }

    private int getClickedNodeIndex(Point point)
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            Node node = nodes[i];
            if (node.Bounds.Contains(point))
            {
                return i;
            }
        }
        return -1;
    }
}
