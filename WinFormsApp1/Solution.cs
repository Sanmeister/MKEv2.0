using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Security.Permissions;
using System.Reflection.Metadata;
using System.Diagnostics.Metrics;
using System.Reflection;
using System.Security.Policy;
using static System.Net.WebRequestMethods;

namespace MKE
{
    public struct Triangle
    {
        public PointF vertex1;
        public PointF vertex2;
        public PointF vertex3;
    
    }
    internal class Solution
    {      
        Graphics graphics;

        decimal[][] ParametrsV;

        double S;
        double[] Side;

        int PanelWidth;
        int PanelHeight;
        int NumberVertex, Qheat, method;
        decimal Kxx, Kyy;

        decimal[] b;
        decimal[] c;
        decimal[,] Ih;
        decimal[,] K_e;
        public List<PointF> NFound = new List<PointF>();
        List<PointF> PointsArea;       
        public List<Triangle> Triangles;
        List<Triangle> NextStepTriangulation;
        Triangle[] TempTriangles;

        PointF[] Vector;
        PointF[] Points;
        PointF[] MiddleSide;
        PointF TempPoint;
        PointF[] NewPoint;

        public Solution(int NumberOfVertex, int Q, int MethodGrid, params decimal[][] ParametrsValue)
        {
            NumberVertex = NumberOfVertex;
            Qheat = Q;
            method = MethodGrid;


            b = new decimal[3];
            c = new decimal[3];
            Ih = new decimal[3,3];
            Side = new double[3];
            K_e = new decimal[3,3];
            TempPoint = new PointF();
            MiddleSide = new PointF[3];
            Points = new PointF[NumberVertex];
            Vector = new PointF[4];
            NewPoint = new PointF[2];

            PointsArea = new List<PointF>();
            Triangles = new List<Triangle>();     
            TempTriangles = new Triangle[100000];         
            NextStepTriangulation = new List<Triangle>();

            ParametrsV = new decimal[5][];
            ParametrsV[0] = new decimal[NumberVertex];
            ParametrsV[1] = new decimal[NumberVertex];
            ParametrsV[2] = new decimal[NumberVertex];
            ParametrsV[3] = new decimal[NumberVertex];
            ParametrsV[4] = new decimal[NumberVertex];           

            for (int i = 0; i < NumberVertex;i++) 
            {
                ParametrsV[0][i] = ParametrsValue[0][i];
                ParametrsV[1][i] = ParametrsValue[1][i];
                ParametrsV[2][i] = ParametrsValue[2][i];
                ParametrsV[3][i] = ParametrsValue[3][i];
                ParametrsV[4][i] = ParametrsValue[4][i];
            }           
        }
        public void BuildGrid()
        {
            double Angle = 0;
            decimal MaxLeftX = 0, MaxDownY = 0;

            if( method == 1)
            {
                for(int i = 0; i < NumberVertex; i++)
                {
                    Points[i].X = (float)ParametrsV[0][i];
                    Points[i].Y = (float)ParametrsV[1][i];

                    if ((decimal)Points[i].X <= MaxLeftX)
                    {
                        MaxLeftX = (decimal)Points[i].X;
                    }

                    if ((decimal)Points[i].Y <= MaxDownY)
                    {
                        MaxDownY = (decimal)Points[i].Y;
                    }
                }
            }
            else
            {
                for (int i = 0; i < NumberVertex; i++)
                {
                    if(i == 0 )
                    {
                        Points[i].X = 0;
                        Points[i].Y = 0;
                    }
                    else if( i == 1 )
                    {
                        Points[i].X = (float)ParametrsV[0][i - 1];
                        Points[i].Y = 0;
                    }
                    else
                    {
                        Angle += ((double)ParametrsV[1][i - 1] * Math.PI) / 180;
                        Points[i].X = (float)(Math.Cos(Angle) * (double)ParametrsV[0][i-1] + Points[i - 1].X);
                        Points[i].Y = (float)(Math.Sin(Angle) * (double)ParametrsV[0][i-1] + Points[i - 1].Y);
                    }

                    if ((decimal)Points[i].X <= MaxLeftX)
                    {
                        MaxLeftX = (decimal)Points[i].X;
                    }

                    if ((decimal)Points[i].Y <= MaxDownY)
                    {
                        MaxDownY = (decimal)Points[i].Y;
                    }
                }
            }

            for (int i = 0; i < NumberVertex; i++)
            {
                Points[i].X = Points[i].X + (float)Math.Abs(MaxLeftX);
                Points[i].Y = Points[i].Y + (float)Math.Abs(MaxDownY);
            }
        }

        public void ShowGrid(Panel XOY)
        {
            PanelWidth = XOY.Width;
            PanelHeight = XOY.Height;

            graphics = XOY.CreateGraphics();
            graphics.Clear(XOY.BackColor);

            graphics.DrawLine(new Pen(Color.Black, 2.5f), 0, 4 * PanelHeight / 5, PanelWidth, 4 * PanelHeight / 5);
            graphics.DrawLine(new Pen(Color.Black, 2.5f), PanelWidth / 10, 0, PanelWidth / 10, PanelHeight);

            for (int i = 0; i < NumberVertex; i++)
            {
                if (i != NumberVertex - 1)
                {
                    graphics.DrawLine(new Pen(Color.Green, 3f), PanelWidth / 10 + 38 * Points[i].X, 4 * PanelHeight / 5 - 38 * Points[i].Y, PanelWidth / 10 + 38 * Points[i + 1].X, 4 * PanelHeight / 5 - 38 * Points[i + 1].Y);
                }
                else
                {
                    graphics.DrawLine(new Pen(Color.Green, 3f), PanelWidth / 10 + 38 * Points[i].X, 4 * PanelHeight / 5 - 38 * Points[i].Y, PanelWidth / 10 + 38 * Points[0].X, 4 * PanelHeight / 5 - 38 * Points[0].Y);
                }

                graphics.DrawEllipse(new Pen(Color.FromArgb(255, 36, 0), 2.0f), PanelWidth / 10 + 38 * Points[i].X, 4 * PanelHeight / 5 - 38 * Points[i].Y, 2, 2);
            }
        }
        public void GetVector(int Point1, int Point2, int Point3)
        {
            Vector[0].X = PointsArea[Point1].X - PointsArea[Point2].X;
            Vector[0].Y = PointsArea[Point1].Y - PointsArea[Point2].Y;

            Vector[1].X = PointsArea[Point3].X - PointsArea[Point2].X;
            Vector[1].Y = PointsArea[Point3].Y - PointsArea[Point2].Y;
        }
        public void GetVectorTriangle(PointF A, PointF B, PointF X)
        {
            Vector[0].X = B.X - A.X;
            Vector[0].Y = B.Y - A.Y;

            Vector[1].X = X.X - A.X;
            Vector[1].Y = X.Y - A.Y;
        }
        public bool RemovePoint(PointF CurrentPoint, List<Triangle> Triangles)
        {
            bool flagO = false, flagN = false;
            int Count;

            for (int i = 0; i < Points.Length; i++)
            {
                if (CurrentPoint == Points[i])
                {
                    for (int j = 0; j < Triangles.Count; j++)
                    {
                        if (i == 0)
                        {
                            Count = Points.Length - 1;

                            if (Points[Count] == Triangles[j].vertex1 || Points[Count] == Triangles[j].vertex2 || Points[Count] == Triangles[j].vertex3)
                            {
                                flagO = true;
                            }
                        }
                        else
                        {
                            if (Points[i - 1] == Triangles[j].vertex1 || Points[i - 1] == Triangles[j].vertex2 || Points[i - 1] == Triangles[j].vertex3)
                            {
                                flagO = true;
                            }
                        }

                        if (i == Points.Length - 1)
                        {
                            Count = 0;
                            if (Points[Count] == Triangles[j].vertex1 || Points[Count] == Triangles[j].vertex2 || Points[Count] == Triangles[j].vertex3)
                            {
                                flagN = true;
                            }
                        }
                        else
                        {
                            if (Points[i + 1] == Triangles[j].vertex1 || Points[i + 1] == Triangles[j].vertex2 || Points[i + 1] == Triangles[j].vertex3)
                            {
                                flagN = true;
                            }
                        }
                    }
                }
            }

            if (flagN && flagO)
            {
                flagO = false;
                flagN = false;

                return true;
            }
            else
            {
                flagO = false;
                flagN = false;

                return false;
            }


        }
        public double MaxLengthVector(PointF Vector1, PointF Vector2)
        {
            if (Math.Sqrt(Vector1.X * Vector1.X + Vector1.Y * Vector1.Y) > Math.Sqrt(Vector2.X * Vector2.X + Vector2.Y * Vector2.Y))
            {
                return Math.Sqrt(Vector1.X * Vector1.X + Vector1.Y * Vector1.Y);
            }
            else
            {
                return Math.Sqrt(Vector2.X * Vector2.X + Vector2.Y * Vector2.Y);
            }
        }
        public double MaxLengthVector(List<PointF> Point)
        {
            double max = 0;
            for (int j = 0; j < PointsArea.Count; j++)
            {
                for (int i = 0; i < PointsArea.Count; i++)
                {
                    if (Math.Sqrt((Point[i].X - Point[j].X) * (Point[i].X - Point[j].X) + (Point[i].Y - Point[j].Y) * (Point[i].Y - Point[j].Y)) > max)
                    {
                        max = Math.Sqrt((Point[i].X - Point[j].X) * (Point[i].X - Point[j].X) + (Point[i].Y - Point[j].Y) * (Point[i].Y - Point[j].Y));
                    }
                }
            }
            return max;
        }
        public int EqualSide(PointF Pointfirst, PointF Pointlast, Triangle Triangle)
        {
            // равна первой
            if ((Pointfirst == Triangle.vertex1) && (Pointlast == Triangle.vertex2) || (Pointfirst == Triangle.vertex2) && (Pointlast == Triangle.vertex1))
            {              
                return 1;
            }
            // равна второй
            else if ((Pointfirst == Triangle.vertex2) && (Pointlast == Triangle.vertex3) || (Pointfirst == Triangle.vertex3) && (Pointlast == Triangle.vertex2))
            {
                return 2;
            }
            // равна третей
            else if ((Pointfirst == Triangle.vertex3) && (Pointlast == Triangle.vertex1) || (Pointfirst == Triangle.vertex1) && (Pointlast == Triangle.vertex3))
            {
                return 3;
            }
            return 0;
        }
        public int OneLine(PointF Vertexfirst, PointF Vertexlast, PointF Vertex1, PointF Vertex2)
        {
            double s;
            
            s = (Vertexfirst.X * Vertexlast.Y + Vertexlast.X * Vertex1.Y + Vertex1.X * Vertexfirst.Y) - 
                (Vertexlast.X * Vertexfirst.Y + Vertex1.X * Vertexlast.Y + Vertexfirst.X * Vertex1.Y);
            
            if (s == 0)
            {
                return 1;
            }

            s = (Vertexfirst.X * Vertexlast.Y + Vertexlast.X * Vertex2.Y + Vertex2.X * Vertexfirst.Y) - 
                (Vertexlast.X * Vertexfirst.Y + Vertex2.X * Vertexlast.Y + Vertexfirst.X * Vertex2.Y);
            
            if (s == 0)
            {
                return 2;
            }
            return 0;
        }      
        public PointF FoundPoint(PointF Vertex1, PointF Vertex2, PointF Point1, PointF Point2)
        {

            TempPoint.X = -1000;
            TempPoint.Y = -1000;

            if (Point1 == Vertex1)
            {
                NewPoint[0] = Vertex2;
                NewPoint[1] = Point2;

                return Point2;
            }
            else if (Point1 == Vertex2)
            {
                NewPoint[0] = Vertex1;
                NewPoint[1] = Point2;

                return Point2;
            }


            if (Point2 == Vertex1)
            {
                NewPoint[0] = Vertex2;
                NewPoint[1] = Point1;

                return Point1;
            }
            else if (Point2 == Vertex2)
            {
                NewPoint[0] = Vertex1;
                NewPoint[1] = Point1;

                return Point1;
            }
            return TempPoint;
        }
        public void JoinNewTriangle(PointF Vertex1, PointF Vertex2, PointF Vertex3, int Indexfirst, int Indexlast)
        {
            TempTriangles[0].vertex1 = Vertex1;
            TempTriangles[0].vertex2 = Vertex2;
            TempTriangles[0].vertex3 = Vertex3;

            Triangles.Insert(Indexfirst, TempTriangles[0]);
            Triangles.RemoveAt(Indexfirst + 1);
            Triangles.RemoveAt(Indexlast);
        }
        public void CombiningTriangles()
        {
            for (int i = 0; i < Triangles.Count; i++)
            {
                for (int j = 0; j < Triangles.Count; j++)
                {
                    if (i != j)
                    {
                        //первая сторона равна первой
                        if (EqualSide(Triangles[i].vertex1, Triangles[i].vertex2, Triangles[j]) == 1)
                        {
                            //вторая сторона
                            if (OneLine(Triangles[i].vertex2, Triangles[i].vertex3, (PointF)FoundPoint(Triangles[i].vertex2, Triangles[i].vertex3, Triangles[j].vertex2, Triangles[j].vertex3), (PointF)FoundPoint(Triangles[i].vertex2, Triangles[i].vertex3, Triangles[j].vertex3, Triangles[j].vertex1)) == 1)
                            {
                                JoinNewTriangle(NewPoint[0], Triangles[i].vertex1, NewPoint[1], i, j);
                            }
                            else if (OneLine(Triangles[i].vertex2, Triangles[i].vertex3, (PointF)FoundPoint(Triangles[i].vertex2, Triangles[i].vertex3, Triangles[j].vertex2, Triangles[j].vertex3), (PointF)FoundPoint(Triangles[i].vertex2, Triangles[i].vertex3, Triangles[j].vertex3, Triangles[j].vertex1)) == 2)
                            {
                                JoinNewTriangle(NewPoint[0], Triangles[i].vertex1, NewPoint[1], i, j);
                            }
                            //третья сторона
                            if (OneLine(Triangles[i].vertex3, Triangles[i].vertex1, (PointF)FoundPoint(Triangles[i].vertex3, Triangles[i].vertex1, Triangles[j].vertex2, Triangles[j].vertex3), (PointF)FoundPoint(Triangles[i].vertex3, Triangles[i].vertex1, Triangles[j].vertex3, Triangles[j].vertex1)) == 1)
                            {
                                JoinNewTriangle(NewPoint[0], Triangles[i].vertex2, NewPoint[1], i, j);
                            }
                            else if (OneLine(Triangles[i].vertex3, Triangles[i].vertex1, (PointF)FoundPoint(Triangles[i].vertex3, Triangles[i].vertex1, Triangles[j].vertex2, Triangles[j].vertex3), (PointF)FoundPoint(Triangles[i].vertex3, Triangles[i].vertex1, Triangles[j].vertex3, Triangles[j].vertex1)) == 2)
                            {
                                JoinNewTriangle(NewPoint[0], Triangles[i].vertex2, NewPoint[1], i, j);
                            }
                        }
                        //первая сторона равна второй
                        else if (EqualSide(Triangles[i].vertex1, Triangles[i].vertex2, Triangles[j]) == 2)
                        {
                            //вторая сторона
                            if (OneLine(Triangles[i].vertex2, Triangles[i].vertex3, (PointF)FoundPoint(Triangles[i].vertex2, Triangles[i].vertex3, Triangles[j].vertex1, Triangles[j].vertex2), (PointF)FoundPoint(Triangles[i].vertex2, Triangles[i].vertex3, Triangles[j].vertex3, Triangles[j].vertex1)) == 1)
                            {
                                JoinNewTriangle(NewPoint[0], Triangles[i].vertex1, NewPoint[1], i, j);
                            }
                            else if (OneLine(Triangles[i].vertex2, Triangles[i].vertex3, (PointF)FoundPoint(Triangles[i].vertex2, Triangles[i].vertex3, Triangles[j].vertex1, Triangles[j].vertex2), (PointF)FoundPoint(Triangles[i].vertex2, Triangles[i].vertex3, Triangles[j].vertex3, Triangles[j].vertex1)) == 2)
                            {
                                JoinNewTriangle(NewPoint[0], Triangles[i].vertex1, NewPoint[1], i, j);
                            }
                            //третья сторона
                            if (OneLine(Triangles[i].vertex3, Triangles[i].vertex1, (PointF)FoundPoint(Triangles[i].vertex3, Triangles[i].vertex1, Triangles[j].vertex1, Triangles[j].vertex2), (PointF)FoundPoint(Triangles[i].vertex3, Triangles[i].vertex1, Triangles[j].vertex3, Triangles[j].vertex1)) == 1)
                            {
                                JoinNewTriangle(NewPoint[0], Triangles[i].vertex2, NewPoint[1], i, j);
                            }
                            else if (OneLine(Triangles[i].vertex3, Triangles[i].vertex1, (PointF)FoundPoint(Triangles[i].vertex3, Triangles[i].vertex1, Triangles[j].vertex1, Triangles[j].vertex2), (PointF)FoundPoint(Triangles[i].vertex3, Triangles[i].vertex1, Triangles[j].vertex3, Triangles[j].vertex1)) == 2)
                            {
                                JoinNewTriangle(NewPoint[0], Triangles[i].vertex2, NewPoint[1], i, j);
                            }
                        }
                        //первая сторона равна третьей
                        else if (EqualSide(Triangles[i].vertex1, Triangles[i].vertex2, Triangles[j]) == 3)
                        {
                            //вторая сторона
                            if (OneLine(Triangles[i].vertex2, Triangles[i].vertex3, (PointF)FoundPoint(Triangles[i].vertex2, Triangles[i].vertex3, Triangles[j].vertex1, Triangles[j].vertex2), (PointF)FoundPoint(Triangles[i].vertex2, Triangles[i].vertex3, Triangles[j].vertex2, Triangles[j].vertex3)) == 1)
                            {
                                JoinNewTriangle(NewPoint[0], Triangles[i].vertex1, NewPoint[1], i, j);
                            }
                            else if (OneLine(Triangles[i].vertex2, Triangles[i].vertex3, (PointF)FoundPoint(Triangles[i].vertex2, Triangles[i].vertex3, Triangles[j].vertex1, Triangles[j].vertex2), (PointF)FoundPoint(Triangles[i].vertex2, Triangles[i].vertex3, Triangles[j].vertex2, Triangles[j].vertex3)) == 2)
                            {
                                JoinNewTriangle(NewPoint[0], Triangles[i].vertex1, NewPoint[1], i, j);
                            }
                            //третья сторона
                            if (OneLine(Triangles[i].vertex3, Triangles[i].vertex1, (PointF)FoundPoint(Triangles[i].vertex3, Triangles[i].vertex1, Triangles[j].vertex1, Triangles[j].vertex2), (PointF)FoundPoint(Triangles[i].vertex3, Triangles[i].vertex1, Triangles[j].vertex2, Triangles[j].vertex3)) == 1)
                            {
                                JoinNewTriangle(NewPoint[0], Triangles[i].vertex2, NewPoint[1], i, j);
                            }
                            else if (OneLine(Triangles[i].vertex3, Triangles[i].vertex1, (PointF)FoundPoint(Triangles[i].vertex3, Triangles[i].vertex1, Triangles[j].vertex1, Triangles[j].vertex2), (PointF)FoundPoint(Triangles[i].vertex3, Triangles[i].vertex1, Triangles[j].vertex2, Triangles[j].vertex3)) == 2)
                            {
                                JoinNewTriangle(NewPoint[0], Triangles[i].vertex2, NewPoint[1], i, j);
                            }

                        }
                        //вторая сторона равна первой
                        if (EqualSide(Triangles[i].vertex2, Triangles[i].vertex3, Triangles[j]) == 1)
                        {
                            MessageBox.Show("da");
                            //первая сторона
                            if (OneLine(Triangles[i].vertex1, Triangles[i].vertex2, (PointF)FoundPoint(Triangles[i].vertex1, Triangles[i].vertex2, Triangles[j].vertex2, Triangles[j].vertex3), (PointF)FoundPoint(Triangles[i].vertex1, Triangles[i].vertex2, Triangles[j].vertex3, Triangles[j].vertex1)) == 1)
                            {
                                JoinNewTriangle(NewPoint[0], Triangles[i].vertex3, NewPoint[1], i, j);
                            }
                            else if (OneLine(Triangles[i].vertex1, Triangles[i].vertex2, (PointF)FoundPoint(Triangles[i].vertex1, Triangles[i].vertex2, Triangles[j].vertex2, Triangles[j].vertex3), (PointF)FoundPoint(Triangles[i].vertex1, Triangles[i].vertex2, Triangles[j].vertex3, Triangles[j].vertex1)) == 2)
                            {
                                JoinNewTriangle(NewPoint[0], Triangles[i].vertex3, NewPoint[1], i, j);
                            }

                            //третья сторона
                            if (OneLine(Triangles[i].vertex3, Triangles[i].vertex1, (PointF)FoundPoint(Triangles[i].vertex3, Triangles[i].vertex1, Triangles[j].vertex2, Triangles[j].vertex3), (PointF)FoundPoint(Triangles[i].vertex3, Triangles[i].vertex1, Triangles[j].vertex3, Triangles[j].vertex1)) == 1)
                            {
                                JoinNewTriangle(NewPoint[0], Triangles[i].vertex2, NewPoint[1], i, j);
                            }
                            else if (OneLine(Triangles[i].vertex3, Triangles[i].vertex1, (PointF)FoundPoint(Triangles[i].vertex3, Triangles[i].vertex1, Triangles[j].vertex2, Triangles[j].vertex3), (PointF)FoundPoint(Triangles[i].vertex3, Triangles[i].vertex1, Triangles[j].vertex3, Triangles[j].vertex1)) == 2)
                            {
                                JoinNewTriangle(NewPoint[0], Triangles[i].vertex2, NewPoint[1], i, j);
                            }
                        }
                        //вторая сторона равна второй
                        else if (EqualSide(Triangles[i].vertex2, Triangles[i].vertex3, Triangles[j]) == 2)
                        {
                            //первая сторона
                            if (OneLine(Triangles[i].vertex1, Triangles[i].vertex2, (PointF)FoundPoint(Triangles[i].vertex1, Triangles[i].vertex2, Triangles[j].vertex1, Triangles[j].vertex2), (PointF)FoundPoint(Triangles[i].vertex1, Triangles[i].vertex2, Triangles[j].vertex3, Triangles[j].vertex1)) == 1)
                            {
                                JoinNewTriangle(NewPoint[0], Triangles[i].vertex3, NewPoint[1], i, j);
                            }
                            else if (OneLine(Triangles[i].vertex1, Triangles[i].vertex2, (PointF)FoundPoint(Triangles[i].vertex1, Triangles[i].vertex2, Triangles[j].vertex1, Triangles[j].vertex2), (PointF)FoundPoint(Triangles[i].vertex1, Triangles[i].vertex2, Triangles[j].vertex3, Triangles[j].vertex1)) == 2)
                            {
                                JoinNewTriangle(NewPoint[0], Triangles[i].vertex3, NewPoint[1], i, j);
                            }

                            //третья сторона
                            if (OneLine(Triangles[i].vertex3, Triangles[i].vertex1, (PointF)FoundPoint(Triangles[i].vertex3, Triangles[i].vertex1, Triangles[j].vertex1, Triangles[j].vertex2), (PointF)FoundPoint(Triangles[i].vertex3, Triangles[i].vertex1, Triangles[j].vertex3, Triangles[j].vertex1)) == 1)
                            {
                                JoinNewTriangle(NewPoint[0], Triangles[i].vertex2, NewPoint[1], i, j);
                            }
                            else if (OneLine(Triangles[i].vertex3, Triangles[i].vertex1, (PointF)FoundPoint(Triangles[i].vertex3, Triangles[i].vertex1, Triangles[j].vertex1, Triangles[j].vertex2), (PointF)FoundPoint(Triangles[i].vertex3, Triangles[i].vertex1, Triangles[j].vertex3, Triangles[j].vertex1)) == 2)
                            {
                                JoinNewTriangle(NewPoint[0], Triangles[i].vertex2, NewPoint[1], i, j);
                            }
                        }
                        //вторая сторона равна третьей
                        else if (EqualSide(Triangles[i].vertex2, Triangles[i].vertex3, Triangles[j]) == 3)
                        {
                            //первая сторона
                            if (OneLine(Triangles[i].vertex1, Triangles[i].vertex2, (PointF)FoundPoint(Triangles[i].vertex1, Triangles[i].vertex2, Triangles[j].vertex1, Triangles[j].vertex2), (PointF)FoundPoint(Triangles[i].vertex1, Triangles[i].vertex2, Triangles[j].vertex2, Triangles[j].vertex3)) == 1)
                            {
                                JoinNewTriangle(NewPoint[0], Triangles[i].vertex3, NewPoint[1], i, j);
                            }
                            else if (OneLine(Triangles[i].vertex1, Triangles[i].vertex2, (PointF)FoundPoint(Triangles[i].vertex1, Triangles[i].vertex2, Triangles[j].vertex1, Triangles[j].vertex2), (PointF)FoundPoint(Triangles[i].vertex1, Triangles[i].vertex2, Triangles[j].vertex2, Triangles[j].vertex3)) == 2)
                            {
                                JoinNewTriangle(NewPoint[0], Triangles[i].vertex3, NewPoint[1], i, j);
                            }

                            //третья сторона
                            if (OneLine(Triangles[i].vertex3, Triangles[i].vertex1, (PointF)FoundPoint(Triangles[i].vertex3, Triangles[i].vertex1, Triangles[j].vertex1, Triangles[j].vertex2), (PointF)FoundPoint(Triangles[i].vertex3, Triangles[i].vertex1, Triangles[j].vertex2, Triangles[j].vertex3)) == 1)
                            {
                                JoinNewTriangle(NewPoint[0], Triangles[i].vertex2, NewPoint[1], i, j);
                            }
                            else if (OneLine(Triangles[i].vertex3, Triangles[i].vertex1, (PointF)FoundPoint(Triangles[i].vertex3, Triangles[i].vertex1, Triangles[j].vertex1, Triangles[j].vertex2), (PointF)FoundPoint(Triangles[i].vertex3, Triangles[i].vertex1, Triangles[j].vertex2, Triangles[j].vertex3)) == 2)
                            {
                                JoinNewTriangle(NewPoint[0], Triangles[i].vertex2, NewPoint[1], i, j);
                            }
                        }
                        //третья сторона равна первой
                        if (EqualSide(Triangles[i].vertex3, Triangles[i].vertex1, Triangles[j]) == 1)
                        {
                            //первая сторона
                            if (OneLine(Triangles[i].vertex1, Triangles[i].vertex2, (PointF)FoundPoint(Triangles[i].vertex1, Triangles[i].vertex2, Triangles[j].vertex2, Triangles[j].vertex3), (PointF)FoundPoint(Triangles[i].vertex1, Triangles[i].vertex2, Triangles[j].vertex3, Triangles[j].vertex1)) == 1)
                            {
                                JoinNewTriangle(NewPoint[0], Triangles[i].vertex3, NewPoint[1], i, j);
                            }
                            else if (OneLine(Triangles[i].vertex1, Triangles[i].vertex2, (PointF)FoundPoint(Triangles[i].vertex1, Triangles[i].vertex2, Triangles[j].vertex2, Triangles[j].vertex3), (PointF)FoundPoint(Triangles[i].vertex1, Triangles[i].vertex2, Triangles[j].vertex3, Triangles[j].vertex1)) == 2)
                            {
                                JoinNewTriangle(NewPoint[0], Triangles[i].vertex3, NewPoint[1], i, j);
                            }
                            //третья сторона
                            if (OneLine(Triangles[i].vertex3, Triangles[i].vertex1, (PointF)FoundPoint(Triangles[i].vertex3, Triangles[i].vertex1, Triangles[j].vertex2, Triangles[j].vertex3), (PointF)FoundPoint(Triangles[i].vertex3, Triangles[i].vertex1, Triangles[j].vertex3, Triangles[j].vertex1)) == 1)
                            {
                                JoinNewTriangle(NewPoint[0], Triangles[i].vertex2, NewPoint[1], i, j);
                            }
                            else if (OneLine(Triangles[i].vertex3, Triangles[i].vertex1, (PointF)FoundPoint(Triangles[i].vertex3, Triangles[i].vertex1, Triangles[j].vertex2, Triangles[j].vertex3), (PointF)FoundPoint(Triangles[i].vertex3, Triangles[i].vertex1, Triangles[j].vertex3, Triangles[j].vertex1)) == 2)
                            {
                                JoinNewTriangle(NewPoint[0], Triangles[i].vertex2, NewPoint[1], i, j);
                            }
                        }
                        //третья сторона равна второй
                        else if (EqualSide(Triangles[i].vertex3, Triangles[i].vertex1, Triangles[j]) == 2)
                        {

                            //первая сторона
                            if (OneLine(Triangles[i].vertex1, Triangles[i].vertex2, (PointF)FoundPoint(Triangles[i].vertex1, Triangles[i].vertex2, Triangles[j].vertex1, Triangles[j].vertex2), (PointF)FoundPoint(Triangles[i].vertex1, Triangles[i].vertex2, Triangles[j].vertex3, Triangles[j].vertex1)) == 1)
                            {

                                JoinNewTriangle(NewPoint[0], Triangles[i].vertex3, NewPoint[1], i, j);



                            }
                            else if (OneLine(Triangles[i].vertex1, Triangles[i].vertex2, (PointF)FoundPoint(Triangles[i].vertex1, Triangles[i].vertex2, Triangles[j].vertex1, Triangles[j].vertex2), (PointF)FoundPoint(Triangles[i].vertex1, Triangles[i].vertex2, Triangles[j].vertex3, Triangles[j].vertex1)) == 2)
                            {

                                JoinNewTriangle(NewPoint[0], Triangles[i].vertex3, NewPoint[1], i, j);
                            }

                            //вторая сторона
                            if (OneLine(Triangles[i].vertex2, Triangles[i].vertex3, (PointF)FoundPoint(Triangles[i].vertex2, Triangles[i].vertex3, Triangles[j].vertex1, Triangles[j].vertex2), (PointF)FoundPoint(Triangles[i].vertex2, Triangles[i].vertex3, Triangles[j].vertex3, Triangles[j].vertex1)) == 1)
                            {

                                JoinNewTriangle(NewPoint[0], Triangles[i].vertex1, NewPoint[1], i, j);
                            }
                            else if (OneLine(Triangles[i].vertex2, Triangles[i].vertex3, (PointF)FoundPoint(Triangles[i].vertex2, Triangles[i].vertex3, Triangles[j].vertex1, Triangles[j].vertex2), (PointF)FoundPoint(Triangles[i].vertex2, Triangles[i].vertex3, Triangles[j].vertex3, Triangles[j].vertex1)) == 2)
                            {

                                JoinNewTriangle(NewPoint[0], Triangles[i].vertex1, NewPoint[1], i, j);
                            }

                        }
                        //третья сторона равна третьей
                        else if (EqualSide(Triangles[i].vertex3, Triangles[i].vertex1, Triangles[j]) == 3)
                        {
                            //первая сторона
                            if (OneLine(Triangles[i].vertex1, Triangles[i].vertex2, (PointF)FoundPoint(Triangles[i].vertex1, Triangles[i].vertex2, Triangles[j].vertex1, Triangles[j].vertex2), (PointF)FoundPoint(Triangles[i].vertex1, Triangles[i].vertex2, Triangles[j].vertex2, Triangles[j].vertex3)) == 1)
                            {
                                JoinNewTriangle(NewPoint[0], Triangles[i].vertex3, NewPoint[1], i, j);
                            }
                            else if (OneLine(Triangles[i].vertex1, Triangles[i].vertex2, (PointF)FoundPoint(Triangles[i].vertex1, Triangles[i].vertex2, Triangles[j].vertex1, Triangles[j].vertex2), (PointF)FoundPoint(Triangles[i].vertex1, Triangles[i].vertex2, Triangles[j].vertex2, Triangles[j].vertex3)) == 2)
                            {
                                JoinNewTriangle(NewPoint[0], Triangles[i].vertex3, NewPoint[1], i, j);
                            }

                            //вторая сторона
                            if (OneLine(Triangles[i].vertex2, Triangles[i].vertex3, (PointF)FoundPoint(Triangles[i].vertex2, Triangles[i].vertex3, Triangles[j].vertex1, Triangles[j].vertex2), (PointF)FoundPoint(Triangles[i].vertex2, Triangles[i].vertex3, Triangles[j].vertex2, Triangles[j].vertex3)) == 1)
                            {
                                JoinNewTriangle(NewPoint[0], Triangles[i].vertex1, NewPoint[1], i, j);
                            }
                            else if (OneLine(Triangles[i].vertex2, Triangles[i].vertex3, (PointF)FoundPoint(Triangles[i].vertex2, Triangles[i].vertex3, Triangles[j].vertex1, Triangles[j].vertex2), (PointF)FoundPoint(Triangles[i].vertex2, Triangles[i].vertex3, Triangles[j].vertex2, Triangles[j].vertex3)) == 2)
                            {
                                JoinNewTriangle(NewPoint[0], Triangles[i].vertex1, NewPoint[1], i, j);
                            }
                        }
                    }
                }
            }
        }
        public void DisplayTriangle(Panel XOY, Triangle Triangle)
        {
            PanelWidth = XOY.Width;
            PanelHeight = XOY.Height;

           graphics = XOY.CreateGraphics();

            graphics.DrawLine(new Pen(Color.FromArgb(0, 0, 0), 1f), PanelWidth / 10 + 38 * Triangle.vertex1.X, 4 * PanelHeight / 5 - 38 * Triangle.vertex1.Y, PanelWidth / 10 + 38 * Triangle.vertex2.X, 4 * PanelHeight / 5 - 38 * Triangle.vertex2.Y);
            graphics.DrawLine(new Pen(Color.FromArgb(0, 0, 0), 1f), PanelWidth / 10 + 38 * Triangle.vertex1.X, 4 * PanelHeight / 5 - 38 * Triangle.vertex1.Y, PanelWidth / 10 + 38 * Triangle.vertex3.X, 4 * PanelHeight / 5 - 38 * Triangle.vertex3.Y);
            graphics.DrawLine(new Pen(Color.FromArgb(0, 0, 0), 1f), PanelWidth / 10 + 38 * Triangle.vertex2.X, 4 * PanelHeight / 5 - 38 * Triangle.vertex2.Y, PanelWidth / 10 + 38 * Triangle.vertex3.X, 4 * PanelHeight / 5 - 38 * Triangle.vertex3.Y);
        }
        public void MiddleSides(Triangle CurrentTriangle)
        {          
            MiddleSide[0].X = (CurrentTriangle.vertex2.X + CurrentTriangle.vertex1.X) / 2;
            MiddleSide[0].Y = (CurrentTriangle.vertex2.Y + CurrentTriangle.vertex1.Y) / 2;

            MiddleSide[1].X = (CurrentTriangle.vertex2.X + CurrentTriangle.vertex3.X) / 2;
            MiddleSide[1].Y = (CurrentTriangle.vertex2.Y + CurrentTriangle.vertex3.Y) / 2;

            MiddleSide[2].X = (CurrentTriangle.vertex3.X + CurrentTriangle.vertex1.X) / 2;
            MiddleSide[2].Y = (CurrentTriangle.vertex3.Y + CurrentTriangle.vertex1.Y) / 2;
        }
        public double VesTriangle(Triangle CurrentTriangle)
        {
            Side[0] = Math.Sqrt((CurrentTriangle.vertex1.X - CurrentTriangle.vertex2.X) * (CurrentTriangle.vertex1.X - CurrentTriangle.vertex2.X) + (CurrentTriangle.vertex1.Y - CurrentTriangle.vertex2.Y) * (CurrentTriangle.vertex1.Y - CurrentTriangle.vertex2.Y));
            Side[1] = Math.Sqrt((CurrentTriangle.vertex2.X - CurrentTriangle.vertex3.X) * (CurrentTriangle.vertex2.X - CurrentTriangle.vertex3.X) + (CurrentTriangle.vertex2.Y - CurrentTriangle.vertex3.Y) * (CurrentTriangle.vertex2.Y - CurrentTriangle.vertex3.Y));
            Side[2] = Math.Sqrt((CurrentTriangle.vertex3.X - CurrentTriangle.vertex1.X) * (CurrentTriangle.vertex3.X - CurrentTriangle.vertex1.X) + (CurrentTriangle.vertex3.Y - CurrentTriangle.vertex1.Y) * (CurrentTriangle.vertex3.Y - CurrentTriangle.vertex1.Y));

            double p = (Side[0] + Side[1] + Side[2]) / 2;
            S = Math.Sqrt(p * (p - Side[0]) * (p - Side[1]) * (p - Side[2]));

            return Side[0] + Side[1] + Side[2];
        }
        public void TriangulationTriangle(Triangle Triangle)
        {
            if (VesTriangle(Triangle) > 2.5 && S > 0.43)
            {
                MiddleSides(Triangle);

                TempTriangles[0].vertex1 = Triangle.vertex1;
                TempTriangles[0].vertex2 = MiddleSide[0];
                TempTriangles[0].vertex3 = MiddleSide[2];

                NextStepTriangulation.Add(TempTriangles[0]);

                TempTriangles[1].vertex1 = MiddleSide[0];
                TempTriangles[1].vertex2 = Triangle.vertex2;
                TempTriangles[1].vertex3 = MiddleSide[1];

                NextStepTriangulation.Add(TempTriangles[1]);

                TempTriangles[2].vertex1 = MiddleSide[1];
                TempTriangles[2].vertex2 = Triangle.vertex3;
                TempTriangles[2].vertex3 = MiddleSide[2];

                NextStepTriangulation.Add(TempTriangles[2]);

                TempTriangles[3].vertex1 = MiddleSide[0];
                TempTriangles[3].vertex2 = MiddleSide[1];
                TempTriangles[3].vertex3 = MiddleSide[2];

                NextStepTriangulation.Add(TempTriangles[3]);
            }
            else
            {
                NextStepTriangulation.Add(Triangle);
            }
        }
        public void UpdateTriangulation(List<Triangle> CurrentTriangles)
        {
            for (int i = 0; i < CurrentTriangles.Count; i++)
            {
                    TriangulationTriangle(CurrentTriangles[i]);
            }

            CurrentTriangles.Clear();

            for (int i = 0; i < NextStepTriangulation.Count; i++)
            {
                CurrentTriangles.Add(NextStepTriangulation[i]);
            }

            NextStepTriangulation.Clear();
        }
        public bool PossibleTriangulation(List<Triangle> Triangles)
        {
            for(int i = 0; i < Triangles.Count; i++)
            {
                if (VesTriangle(Triangles[i]) > 2.5 && S > 0.43)
                {
                    return true;
                }
            }
            return false;
        }
        public void ShowBorder(Panel XOY, int NumBorder)
        {
            PanelWidth = XOY.Width;
            PanelHeight = XOY.Height;

            for (int i = 0; i < NumberVertex; i++)
            {
                if(i == NumBorder - 1)
                {
                    if(i != NumberVertex - 1)
                    {
                        graphics.DrawLine(new Pen(Color.FromArgb(237, 41, 57), 3f), PanelWidth / 10 + 38 * Points[NumBorder - 1].X, 4 * PanelHeight / 5 - 38 * Points[NumBorder - 1].Y, PanelWidth / 10 + 38 * Points[NumBorder].X, 4 * PanelHeight / 5 - 38 * Points[NumBorder].Y);
                    }
                    else
                    {
                        graphics.DrawLine(new Pen(Color.FromArgb(237, 41, 57), 3f), PanelWidth / 10 + 38 * Points[NumBorder - 1].X, 4 * PanelHeight / 5 - 38 * Points[NumBorder - 1].Y, PanelWidth / 10 + 38 * Points[0].X, 4 * PanelHeight / 5 - 38 * Points[0].Y);
                    }                  
                }
                else
                {
                    if (i != NumberVertex - 1)
                    {
                        graphics.DrawLine(new Pen(Color.Green, 3f), PanelWidth / 10 + 38 * Points[i].X, 4 * PanelHeight / 5 - 38 * Points[i].Y, PanelWidth / 10 + 38 * Points[i + 1].X, 4 * PanelHeight / 5 - 38 * Points[i + 1].Y);
                    }
                    else
                    {
                        graphics.DrawLine(new Pen(Color.Green, 3f), PanelWidth / 10 + 38 * Points[i].X, 4 * PanelHeight / 5 - 38 * Points[i].Y, PanelWidth / 10 + 38 * Points[0].X, 4 * PanelHeight / 5 - 38 * Points[0].Y);
                    }
                }               
                graphics.DrawEllipse(new Pen(Color.FromArgb(255, 36, 0), 2.0f), PanelWidth / 10 + 38 * Points[i].X, 4 * PanelHeight / 5 - 38 * Points[i].Y, 2, 2);
            }            
        }
        public bool ObtuseAngle(PointF PointA, PointF PointB, PointF PointX)
        {
            double distance = 0;

            GetVectorTriangle(PointA, PointB, PointX);

            distance = Math.Acos((Vector[0].X * Vector[1].X + Vector[0].Y * Vector[1].Y) / Math.Sqrt( (Vector[0].X * Vector[0].X + Vector[0].Y * Vector[0].Y) * (Vector[1].X * Vector[1].X + Vector[1].Y * Vector[1].Y)));
            distance = distance * 180 / Math.PI;

            if (distance > 90)
            {
                return true;
            }

            GetVectorTriangle(PointB, PointA, PointX);

            distance = Math.Acos((Vector[0].X * Vector[1].X + Vector[0].Y * Vector[1].Y) / Math.Sqrt((Vector[0].X * Vector[0].X + Vector[0].Y * Vector[0].Y) * (Vector[1].X * Vector[1].X + Vector[1].Y * Vector[1].Y)));
            distance = distance * 180 / Math.PI;

            if (distance > 90)
            {
                return true;
            }
            return false;
        }
        public double MinDistanceToPoint(PointF A, PointF B, PointF X)
        {
            double distance = 10000;

            if (Math.Sqrt((X.X - A.X) * (X.X - A.X) + (X.Y - A.Y) * (X.Y - A.Y)) < Math.Sqrt((X.X - B.X) * (X.X - B.X) + (X.Y - B.Y) * (X.Y - B.Y)))
            {
                distance = Math.Sqrt((X.X - A.X) * (X.X - A.X) + (X.Y - A.Y) * (X.Y - A.Y));
            }
            else
            {
                distance = Math.Sqrt((X.X - B.X) * (X.X - B.X) + (X.Y - B.Y) * (X.Y - B.Y));
            }

            return distance;
        }
        public int PointOnLine(Panel XOY, PointF Point)
        {
            PanelWidth = XOY.Width;
            PanelHeight = XOY.Height;

            int NumberBorder = 0;

            double distance;
            double MinDistance = 100000;
            
            Point.X = (Point.X - PanelWidth / 10) / 38;
            Point.Y = (-Point.Y + 4 * PanelHeight / 5) / 38;

            for (int i = 0; i < Points.Length; i++)
            {
               if (i == Points.Length - 1)
               {
                    if(ObtuseAngle(Points[i], Points[0], Point))
                    {
                        distance = MinDistanceToPoint(Points[i], Points[0], Point);
                    }
                    else
                    {
                        distance = Math.Abs((Points[0].Y - Points[i].Y) * Point.X - (Points[0].X - Points[i].X) * Point.Y + Points[0].X * Points[i].Y - Points[0].Y * Points[i].X) / Math.Sqrt( (Points[0].Y - Points[i].Y) * (Points[0].Y - Points[i].Y) + (Points[0].X - Points[i].X) * (Points[0].X - Points[i].X));
                    }

                    if (distance < MinDistance)
                    {
                        MinDistance = distance;
                        NumberBorder = i + 1;
                    }
               }
               else
               {
                    if (ObtuseAngle(Points[i], Points[i + 1], Point))
                    {
                        distance = MinDistanceToPoint(Points[i], Points[i + 1], Point);
                    }
                    else
                    {
                        distance = Math.Abs((Points[i + 1].Y - Points[i].Y) * Point.X - (Points[i + 1].X - Points[i].X) * Point.Y + Points[i + 1].X * Points[i].Y - Points[i + 1].Y * Points[i].X) / Math.Sqrt((Points[i + 1].Y - Points[i].Y) * (Points[i + 1].Y - Points[i].Y) + (Points[i + 1].X - Points[i].X) * (Points[i + 1].X - Points[i].X));
                    }

                    if(distance < MinDistance)
                    {
                        MinDistance = distance;
                        NumberBorder = i + 1;                     
                    }      
               }                
            }

            if (MinDistance <= 0.1)
            {
                return NumberBorder;
            }
            else
            {
                return 0;
            }           
        }
        public void Triangulation(Panel XOY)
        {
            bool FlagAngle = false, FlagA = false;           

            int FoundIndex = 0;
            int CountTriangles = 0;

            int ICurrentPoint1 = 0, ICurrentPoint2 = 0;
            int JCurrentPoint1 = 0, JCurrentPoint2 = 0;

            double AngleNew, CurrentAngle, MinAngle = 360;
            double LenVector = MaxLengthVector(PointsArea);

            for (int i=0; i < Points.Length; i++)
            {
                PointsArea.Add(Points[i]);
            }
           
            int CountPoints = PointsArea.Count;

            for (int j=0; j < CountPoints - 2; j++)
            {
                for(int i = 0; i < PointsArea.Count; i++)
                {
                    ICurrentPoint1 = i + 1;
                    ICurrentPoint2 = i + 2;

                    if (ICurrentPoint1 >= PointsArea.Count)
                    {
                        ICurrentPoint1 = Math.Abs(PointsArea.Count - i - 1);
                        ICurrentPoint2 = Math.Abs(PointsArea.Count - i - 2);
                    }
                    else if (ICurrentPoint2 >= PointsArea.Count)
                    {

                        ICurrentPoint2 = Math.Abs(PointsArea.Count - i - 2);
                    }

                    GetVector(i, ICurrentPoint1, ICurrentPoint2);

                    double Angle = Math.Acos((Vector[0].X * Vector[1].X + Vector[0].Y * Vector[1].Y) /
                        (Math.Sqrt((Vector[0].X * Vector[0].X + Vector[0].Y * Vector[0].Y) *
                        (Vector[1].X * Vector[1].X + Vector[1].Y * Vector[1].Y)))); ;

                    Angle = (Angle * 180) / Math.PI;

                    if ((PointsArea[i].X * PointsArea[ICurrentPoint1].Y + PointsArea[ICurrentPoint2].X * PointsArea[i].Y + PointsArea[ICurrentPoint2].Y * PointsArea[ICurrentPoint1].X
                         - PointsArea[ICurrentPoint2].X * PointsArea[ICurrentPoint1].Y - PointsArea[i].X * PointsArea[ICurrentPoint2].Y - PointsArea[ICurrentPoint1].X * PointsArea[i].Y) <= 0)
                    {
                        Angle = 360 - Angle;
                        FlagA = true;
                    }
                    else
                    {
                        FlagA = false;
                    }

                    if (Angle < MinAngle && FlagA)
                    {
                        MinAngle = Angle;
                        FoundIndex = i;
                        FlagAngle = true;
                    }
                    else if (Angle < MinAngle)
                    {
                        MinAngle = Angle;
                        FoundIndex = i;
                        FlagAngle = false;
                    }
                }

                if (MinAngle <= 90)
                {
                    JCurrentPoint1 = FoundIndex + 1;
                    JCurrentPoint2 = FoundIndex + 2;

                    if (JCurrentPoint1 >= PointsArea.Count)
                    {
                        JCurrentPoint1 = Math.Abs(PointsArea.Count - FoundIndex - 1);
                        JCurrentPoint2 = Math.Abs(PointsArea.Count - FoundIndex - 2);
                    }
                    else if (JCurrentPoint2 >= PointsArea.Count)
                    {
                        JCurrentPoint2 = Math.Abs(PointsArea.Count - FoundIndex - 2);
                    }

                    TempTriangles[CountTriangles].vertex1 = PointsArea[FoundIndex];
                    TempTriangles[CountTriangles].vertex2 = PointsArea[JCurrentPoint1];
                    TempTriangles[CountTriangles].vertex3 = PointsArea[JCurrentPoint2];

                    Triangles.Add(TempTriangles[CountTriangles]);

                    CountTriangles++;

                    if (RemovePoint(PointsArea[JCurrentPoint1], Triangles))
                    {
                        PointsArea.RemoveAt(JCurrentPoint1);
                    }
                }
                else
                {
                    JCurrentPoint1 = FoundIndex + 1;
                    JCurrentPoint2 = FoundIndex + 2;

                    if (JCurrentPoint1 >= PointsArea.Count)
                    {
                        JCurrentPoint1 = Math.Abs(PointsArea.Count - FoundIndex - 1);
                        JCurrentPoint2 = Math.Abs(PointsArea.Count - FoundIndex - 2);
                    }
                    else if (JCurrentPoint2 >= PointsArea.Count)
                    {
                        JCurrentPoint2 = Math.Abs(PointsArea.Count - FoundIndex - 2);
                    }

                    GetVector(FoundIndex, JCurrentPoint1, JCurrentPoint2);

                    Vector[2].X = (float)(Vector[0].X * MaxLengthVector(Vector[0], Vector[1]) / Math.Sqrt(Vector[0].X * Vector[0].X + Vector[0].Y * Vector[0].Y));
                    Vector[2].Y = (float)(Vector[0].Y * MaxLengthVector(Vector[0], Vector[1]) / Math.Sqrt(Vector[0].X * Vector[0].X + Vector[0].Y * Vector[0].Y));

                    LenVector = LenVector / Math.Sqrt(Vector[0].X * Vector[0].X + Vector[0].Y * Vector[0].Y);

                    CurrentAngle = Math.Acos(Vector[2].X / (Math.Sqrt(Vector[2].X * Vector[2].X + Vector[2].Y * Vector[2].Y)));

                    CurrentAngle = (CurrentAngle * 180) / Math.PI;
                   
                    if (FlagAngle)
                    {
                        AngleNew = (((MinAngle / 2) - 180 + CurrentAngle) * Math.PI) / 180;
                    }
                    else
                    {
                        AngleNew = ((-(MinAngle / 2) - 180 + CurrentAngle) * Math.PI) / 180;
                    }

                    Vector[3].X = (float)(Vector[2].X * Math.Cos(AngleNew) - Vector[2].Y * Math.Sin(AngleNew));
                    Vector[3].Y = (float)(Vector[2].X * Math.Sin(AngleNew) + Vector[2].Y * Math.Cos(AngleNew));

                    /* Первый треугольник */
                    TempTriangles[CountTriangles].vertex1 = PointsArea[FoundIndex];
                    TempTriangles[CountTriangles].vertex2 = PointsArea[JCurrentPoint1];

                    TempTriangles[CountTriangles].vertex3.X = Vector[3].X + PointsArea[JCurrentPoint1].X;
                    TempTriangles[CountTriangles].vertex3.Y = Vector[3].Y + PointsArea[JCurrentPoint1].Y;

                    Triangles.Add(TempTriangles[CountTriangles]);

                    CountTriangles++;

                    /* Второй треугольник */
                    TempTriangles[CountTriangles].vertex1 = PointsArea[JCurrentPoint1];
                    TempTriangles[CountTriangles].vertex2 = PointsArea[JCurrentPoint2];

                    TempTriangles[CountTriangles].vertex3.X = Vector[3].X + PointsArea[JCurrentPoint1].X;
                    TempTriangles[CountTriangles].vertex3.Y = Vector[3].Y + PointsArea[JCurrentPoint1].Y;

                    Triangles.Add(TempTriangles[CountTriangles]);
                   
                    TempPoint.X = Vector[3].X + PointsArea[JCurrentPoint1].X;
                    TempPoint.Y = Vector[3].Y + PointsArea[JCurrentPoint1].Y;

                    PointsArea.Insert(JCurrentPoint1, TempPoint);
                    PointsArea.RemoveAt(JCurrentPoint2);

                    CountTriangles++;
                    j--;
                }
                MinAngle = 360;
            }

            CombiningTriangles();

            while (PossibleTriangulation(Triangles))
            {
                UpdateTriangulation(Triangles);
            }
            
            for(int i = 0; i < Triangles.Count; i++)
            {
                DisplayTriangle(XOY, Triangles[i]);
            }
        }       
        public bool NodesFound(PointF Vertex)
        {
            for(int i = 0; i < NFound.Count;i++)
            {
                if( Vertex == NFound[i])
                {
                    return true;
                }
            }
            return false;
        }
        public void GetNumberNodes()
        {  
            int CountNodes = 1;
            for(int i = 0; i < Triangles.Count;i++)
            {
                if (!NodesFound(Triangles[i].vertex1))
                {                  
                    NFound.Add(Triangles[i].vertex1);
                    CountNodes++;
                }
                if (!NodesFound(Triangles[i].vertex2))
                {
                    NFound.Add(Triangles[i].vertex2);
                    CountNodes++;
                }
                if (!NodesFound(Triangles[i].vertex3))
                {
                    NFound.Add(Triangles[i].vertex3);
                    CountNodes++;
                }
            }
        }
        public double GetDistancePoints(PointF Point1, PointF Point2)
        {
            return Math.Sqrt((Point2.X - Point1.X) * (Point2.X - Point1.X) + (Point2.Y - Point1.Y) * (Point2.Y - Point1.Y));
        }
        public decimal GetSquare(Triangle CurrentTriangle)
        {
            decimal s = (decimal)((CurrentTriangle.vertex1.X * CurrentTriangle.vertex2.Y + CurrentTriangle.vertex2.X * CurrentTriangle.vertex3.Y + CurrentTriangle.vertex3.X * CurrentTriangle.vertex1.Y) -
                (CurrentTriangle.vertex2.X * CurrentTriangle.vertex1.Y + CurrentTriangle.vertex3.X * CurrentTriangle.vertex2.Y + CurrentTriangle.vertex1.X * CurrentTriangle.vertex3.Y));

            return s / 2 ;
        }
        public int SideTriangleOnBorder(PointF Vertex1, PointF Vertex2)
        {
           for(int i = 0; i < Points.Length; i++)
           {
                if (i == Points.Length - 1)
                {
                    if (((GetDistancePoints(Points[i], Vertex1) + GetDistancePoints(Vertex1, Points[0])) == GetDistancePoints(Points[i], Points[0])) &&
                    ((GetDistancePoints(Points[i], Vertex2) + GetDistancePoints(Vertex2, Points[0])) == GetDistancePoints(Points[i], Points[0])))
                    {
                        return i;
                    }
                }
                else
                {
                    if(((GetDistancePoints(Points[i], Vertex1) + GetDistancePoints(Vertex1, Points[i + 1])) == GetDistancePoints(Points[i], Points[i + 1])) && 
                    ((GetDistancePoints(Points[i], Vertex2) + GetDistancePoints(Vertex2, Points[i + 1])) == GetDistancePoints(Points[i], Points[i + 1])))
                    {
                      return i;
                    }
                 }
           }
            return -1;
        }
        public int PointCloseNode(Panel XOY, PointF CurrentPoint)
        {
            double MinDistance = 1000;
            int NodesIndex = 0;
            PanelWidth = XOY.Width;
            PanelHeight = XOY.Height;
            
            CurrentPoint.X = (CurrentPoint.X - PanelWidth / 10) / 38;
            CurrentPoint.Y = (-CurrentPoint.Y + 4 * PanelHeight / 5) / 38;

            GetNumberNodes();
            for (int i = 0; i < NFound.Count; i++)
            {
                if(GetDistancePoints(NFound[i], CurrentPoint) < MinDistance)
                {
                    MinDistance = GetDistancePoints(NFound[i], CurrentPoint);
                    NodesIndex = i;
                }               
            }

            if(MinDistance < 0.1)
            {
                graphics.DrawEllipse(new Pen(Color.FromArgb(199, 21, 133), 3f), PanelWidth / 10 + 38 * NFound[NodesIndex].X, 4 * PanelHeight / 5 - 38 * NFound[NodesIndex].Y, 2, 2);
                graphics.DrawString((NodesIndex + 1).ToString(), new Font(FontFamily.GenericSansSerif, 12f), new SolidBrush(Color.Red), PanelWidth / 10 + 38 * NFound[NodesIndex].X, 4 * PanelHeight / 5 - 38 * NFound[NodesIndex].Y);
                return NodesIndex;
            }
            return 0;
        }
        public void FindTemperature()
        {
            decimal SquareTriangle;
            int NumberBorder;
            for(int count = 0; count < Triangles.Count; count++)
            {
                /* Вычисление b[i] и  c[i], для узла i */
                b[0] = (decimal)(Triangles[count].vertex2.Y - Triangles[count].vertex3.Y);
                c[0] = (decimal)(Triangles[count].vertex3.X - Triangles[count].vertex2.X);

                /* Вычисление b[j] и  c[j], для узла j */
                b[1] = (decimal)(Triangles[count].vertex3.Y - Triangles[count].vertex1.Y);
                c[1] = (decimal)(Triangles[count].vertex1.X - Triangles[count].vertex3.X);

                /* Вычисление b[k] и  c[k], для узла k */
                b[2] = (decimal)(Triangles[count].vertex1.Y - Triangles[count].vertex2.Y);
                c[2] = (decimal)(Triangles[count].vertex2.X - Triangles[count].vertex1.X);

                /* Вычисление матрицы теплопроводности элементов с теплообменом на границе, или его отсутствием, если треугольник не попадает на границу */

                SquareTriangle = GetSquare(Triangles[count]);

                NumberBorder = SideTriangleOnBorder(Triangles[count].vertex1, Triangles[count].vertex2);

                if (NumberBorder != -1)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if(i == 2 || j == 2)
                            {
                                Ih[i, j] = 0;
                            }
                            else
                            {
                                if (i == j)
                                {
                                    Ih[i, j] = (ParametrsV[2][NumberBorder] * (decimal)GetDistancePoints(Triangles[count].vertex1, Triangles[count].vertex2)) / 3;
                                }
                                else
                                {
                                    Ih[i, j] = (ParametrsV[2][NumberBorder] * (decimal)GetDistancePoints(Triangles[count].vertex1, Triangles[count].vertex2)) / 6;

                                }
                            }
                            K_e[i, j] = K_e[i, j] + (Kxx / 4 * SquareTriangle) * (b[i] * b[j]) + (Kyy / 4 * SquareTriangle) * (c[i] * c[j]) + Ih[i, j];
                        }
                    }  
                }
                else
                {
                    for(int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            K_e[i, j] = K_e[i, j] + (Kxx / 4 * SquareTriangle) * (b[i] * b[j]) + (Kyy / 4 * SquareTriangle) * (c[i] * c[j]);
                        }
                    }
                }


                NumberBorder = SideTriangleOnBorder(Triangles[count].vertex1, Triangles[count].vertex2);
            }
        }
    }
}
