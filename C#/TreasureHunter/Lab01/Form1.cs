// Project : Treasure Hunter
// Feb 04 2025
// By Agamdeep Singh
//
// Print Format : Landscape
// ///////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using GDIDrawer;

namespace Lab01
{
    public partial class Form1 : Form
    {
        // ///////////////////////////////////////////////////////////////////////
        // Form1 class - Runs the operation of treasure hunting and manipulates maps on gdidrawer 'Form1', 
        // ///////////////////////////////////////////////////////////////////////
        public Form1()
        {
            InitializeComponent();
            Text = "Lab01 - Treasure Hunter 1.0.0.0";
            MouseWheel += sleepChange;                      // Event handler for sleep
            sw = new Stopwatch();                           // New stopwatch
        }

        public struct Treasure                  // Treasure struct
        {
            public Point location;              // Location of treasure
            public int treasureValue;           // Value of treasure
            public int steps;                   // Treasure steps
        }

        CDrawer drawer = null;                  // Cdrawer variable set to null
        public enum site { open, wall, visited, treasure};      // enum for different sites on the map

        site[,] diffSites;                                      // 2d array for storing sites

        List<Treasure> tList = new List<Treasure>();            // List of treasures

        int sleepTime = 15;                                     // Sleeptime

        Point startP = new Point(-1, -1);                       // Start point

        FileInfo file;                                          // fileinfo object

        bool solve = true;                                      // Bool variable

        int flag = 0;                                           // flag for solve button

        Stopwatch sw;                                           // Sw stopwatch

        Thread t1;                                                  // new thread

        Color solveColor = Color.Purple;                        // solve color

        Color deadColor = Color.DarkGray;                       // dead color after tracking

        int stepCounter = 0;                                    // treasure step count

        //********************************************************************************************
        //Method: public void sleepChange(object sender, MouseEventArgs e)
        //Purpose: Changes the sleep value
        //***************************************************************************
        public void sleepChange(object sender, MouseEventArgs e)
        {
            if(e.Delta < 0)
            {
                sleepTime--;
            }
            else if(e.Delta > 0)
            {
                sleepTime++;
            }
            UI_sleeptime_lbl.Text = $"{sleepTime}ms";
        }

        //********************************************************************************************
        //Method: private void UI_Label_DragEnter(object sender, DragEventArgs e)
        //Purpose: Checks the file dragged is of right format
        //***************************************************************************
        private void UI_Label_DragEnter(object sender, DragEventArgs e)
        {
            if(e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect= DragDropEffects.None;
            }
        }

        //********************************************************************************************
        //Method:  private void UI_Label_DragDrop(object sender, DragEventArgs e)
        //Purpose: Checks if the file exists and calls load map function
        //***************************************************************************
        private void UI_Label_DragDrop(object sender, DragEventArgs e)
        {
            file = new FileInfo(((string[])e.Data.GetData(DataFormats.FileDrop)).First());              // Stores the file name in variable
            if(file.Exists)
            {
                LoadMap();
            }
        }

        //********************************************************************************************
        //Method: public void LoadMap()
        //Purpose: Converts the file imported into bitmap, check the width and height, and fills in the diffsites array
        //***************************************************************************
        public void LoadMap()
        {
            Bitmap bm = new Bitmap(file.FullName);                                  // Bitmap for the file
            if(bm.Width > 190 || bm.Height > 100)                                   // If witdh and height exceed restrictions
            {
                throw new ArgumentException("Bitmap WxH exceeds allowed area");
            }
            else
            {
                UI_listbox.Items.Add($"Load:Map Loaded :{file.Name}");
                UI_solve_btn.Text = $"Solve:{file.Name}";
                if (drawer != null)                                                         // If already a drawer exits
                {
                    drawer.Close();                                                         // Close the drawer
       
                    CDrawer canvas = new CDrawer(bm.Width * 10, bm.Height * 10, false);     // Initialise a new one
                    drawer = canvas;
                }
                else
                {
                    drawer = new CDrawer(bm.Width * 10, bm.Height * 10, false);
                }

                // Setting drawer properties
                drawer.Scale = 10;
                drawer.ContinuousUpdate = false;
                drawer.BBColour = Color.White;

                diffSites = new site[drawer.ScaledWidth, drawer.ScaledHeight];

                // Filling the diffSites array based on the Colors provided by the bitmap
                for(int i = 0;i < bm.Height ;i++)
                {
                    for(int j = 0;j < bm.Width ;j++)
                    {
                        Color pixelColor = bm.GetPixel(j, i);
                        drawer.SetBBScaledPixel(j ,i , pixelColor);                 // Coloring the pixel in gdidrawer
                        if(pixelColor == Color.FromArgb(0, 0, 0))                   // IF color is black
                        {
                            diffSites[j, i] = site.wall;
                        }
                        else if(pixelColor == Color.FromArgb(255, 255, 255))    // IF color is white
                        {
                            diffSites[j, i] = site.open;
                        }
                        else if (pixelColor == Color.FromArgb(0, 255, 0))       // IF color is green
                        {
                            startP.X = j; startP.Y = i;
                        }
                        else if(pixelColor.R == 255 && pixelColor.G == 255 && pixelColor.B < 255)   // if treasure found
                        {
                            diffSites[j, i] = site.treasure;
                            Treasure tr = new Treasure();
                            tr.location = new Point(j, i);
                            tr.treasureValue = pixelColor.B;
                            tList.Add(tr);                      
                        }
                    }
                }

                drawer.Render();  

                if(startP.X == -1 || startP.Y == -1)
                {
                    throw new ArgumentException("No Start point detected!!!!");         // Exception if no start point is found
                }
            }
        }

        //********************************************************************************************
        //Method:  private void UI_solve_btn_Click(object sender, EventArgs e)
        //Purpose: Checks if the map needs to solved in a thread or simply
        //***************************************************************************
        private void UI_solve_btn_Click(object sender, EventArgs e)
        {
            if(flag == 0)
            {
                if (diffSites.Length > 4000 || sleepTime > 15)                                              // If area greater than 4000 or sleeptime greater than 15
                {
                    sw.Start();                                                                                 // Starting the stopwatch
                    flag = 1;
                    t1 = new Thread(() => SolveCall(), 20 * 1024 * 1024);      // aSSIGNING METHOD and setting the stack size
                    t1.Start();                                                 // Starting the thread
                    UI_solve_btn.Text = $"Cancel";
                }
                else
                {
                    Solve(drawer, startP, startP, solveColor, deadColor);                                           // Normal start of function
                    solveComplete();
                }
            }
            else if (flag == 1)
            {
                flag = 0;
                solve = false;
            }
        }
                                                                                                         
        public void SolveCall()
        {
            Solve(drawer, startP, startP, solveColor, deadColor);                                           // Normal start of function
            Invoke(new Action (() => solveComplete()));
        }

        //********************************************************************************************
        //Method:private void UI_Solvecol_btn_Click(object sender, EventArgs e)
        //Purpose:Displays the color dialog box to choose solve color
        //***************************************************************************
        private void UI_Solvecol_btn_Click(object sender, EventArgs e)
        {
            if(UI_solvecolor_dialog.ShowDialog() == DialogResult.OK)
            {
                solveColor = UI_solvecolor_dialog.Color;
                UI_Solvecol_btn.BackColor = UI_solvecolor_dialog.Color;
            }
        }

        //********************************************************************************************
        //Method: private void UI_deadcol_btn_Click(object sender, EventArgs e)
        //Purpose:Displays the color dialog box to choose dead color
        //***************************************************************************
        private void UI_deadcol_btn_Click(object sender, EventArgs e)
        {
            if (UI_deadcolor_dialog.ShowDialog() == DialogResult.OK)
            {
                deadColor = UI_deadcolor_dialog.Color;
                UI_deadcol_btn.BackColor = UI_deadcolor_dialog.Color;
            }
        }

        //********************************************************************************************
        //Method: public void Solve(CDrawer canvas, Point currentP, Point StartP, Color stepColor, Color deadColor)
        //Purpose:Recursive method to solve the map and find treasure
        //***************************************************************************
        public void Solve(CDrawer canvas, Point currentP, Point StartP, Color stepColor, Color deadColor)
        {
            if(!solve)                                                                          // if solve is set to false
            {
                return;
            }
            if(currentP.X >= diffSites.GetLength(0) || currentP.X < 0)                          // if path goes out bounds
            {
                return;
            }
            if (currentP.Y >= diffSites.GetLength(1) || currentP.Y < 0)
            {
                return;
            }
            if (diffSites[currentP.X, currentP.Y] == site.wall || diffSites[currentP.X, currentP.Y] == site.visited)                // If it hits wall or already visited site
            {
                return;
            }

            Thread.Sleep(sleepTime);                                                                        // Introduce sleep time

            if (diffSites[currentP.X, currentP.Y] == site.treasure)                                         // If found a treasure
            {
                UpdateTreasure(new Point(currentP.X, currentP.Y));
                return;
            }

            diffSites[currentP.X, currentP.Y] = site.visited;                                                   // Setting the current location to visited

            // Setting the stepcolor and incrementing counter
            canvas.SetBBScaledPixel(currentP.X, currentP.Y, stepColor);
            stepCounter++;
            drawer.Render();

            // Going in all directions
            Solve(drawer, new Point(currentP.X + 1, currentP.Y), StartP, stepColor, deadColor);
            Solve(drawer, new Point(currentP.X , currentP.Y + 1), StartP, stepColor, deadColor);
            Solve(drawer, new Point(currentP.X - 1, currentP.Y), StartP, stepColor, deadColor);
            Solve(drawer, new Point(currentP.X, currentP.Y - 1), StartP, stepColor, deadColor);

            // Setting the daedcolor and decrementing counter

            drawer.SetBBScaledPixel(currentP.X, currentP.Y, deadColor);
            stepCounter--; 
            drawer.Render();
        }

        //********************************************************************************************
        //Method: public void solveComplete()
        //Purpose:Runs after the solve gets completed or cancelled for thread
        //***************************************************************************
        public void solveComplete()
        {
            if(!solve)                                                              // If solve cancelled
            {
                UI_listbox.Items.Add("Solver stopped before completion.");
                UI_listbox.Items.Add(sw.ElapsedMilliseconds);
                UI_listbox.Items.Add(stepCounter);
                flag = 0;
                solve = true;
            }
            else
            {
                UI_listbox.Items.Add($"Treasure hunt completed in {sw.ElapsedMilliseconds}ms.");
                sw.Stop();
                sw.Reset();
                for(int i = 0; i < tList.Count; i++)                                                                                                // Displaying all the treasures in current map
                {
                    UI_listbox.Items.Add($"Treasure (X={tList[i].location.X}, Y={tList[i].location.Y}) of value {tList[i].treasureValue} found in {tList[i].steps}");
                }
                for (int i = 0; i < tList.Count; i++)
                {
                    UI_listbox.Items.Add($"Found {tList[i].treasureValue} gold @[X={tList[i].location.X},Y={tList[i].location.Y}]");
                }

                //Resetting the variables
                stepCounter = 0;
                startP.X = -1;
                startP.Y = -1;
                tList.Clear();
            }
            UI_solve_btn.Text = $"Solve";
        }

        //********************************************************************************************
        //Method:public void UpdateTreasure(Point p)
        //Purpose:Updates the treasure list whenever it is encountered in solve
        //***************************************************************************
        public void UpdateTreasure(Point p)
        {
            for(int i = 0;i < tList.Count;i++)
            {
                if (tList[i].location == p)
                {
                    Treasure treasure = tList[i];
                    treasure.steps = stepCounter;
                    tList[i] = treasure;
                }
            }
        }

    }
}
