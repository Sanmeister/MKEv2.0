using MKE;
using System.Data;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        Solution solution;
        Graphics graphics;

        public decimal[][]? ParametrsValue;
        public int Count = 0;
        public int method;
        int PanelWidth;
        int PanelHeight;

        readonly Label heading = new Label();
        Label NumVertexText = new Label();
        Label CoordinatesVertexText = new Label();
        Label XText = new();
        Label QDis = new Label();
        Label QText = new Label();
        Label YText = new Label();
        Label Ttext = new();
        Label htext = new();
        Label qtext = new();
        Label Tdis = new();
        Label hdis = new();
        Label qdis = new();
        Label Lendis = new();
        Label Angledis = new();

        TextBox NumVertex = new TextBox();     
        TextBox X = new TextBox();     
        TextBox Y = new TextBox();        
        TextBox Q = new TextBox();
        TextBox T = new TextBox();
        TextBox h = new TextBox();
        TextBox q = new TextBox();
        TextBox Len = new();
        TextBox Angle = new();

        Button Enterdata = new Button();

        DialogResult result;
    
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {           
            result = MessageBox.Show("Нажмите да, для того чтобы задать область по координатам точек, или нажмите нет, для задания области по длинам сторон и углам между ними", "Сообщение", MessageBoxButtons.YesNo,
            MessageBoxIcon.Information,
            MessageBoxDefaultButton.Button1,
            MessageBoxOptions.DefaultDesktopOnly);

            if(result == DialogResult.Yes)
            {
                method = 1;
                MethodCoordinatesfirstpart();
            }
            else
            {
                method = 2;
                MethodLengthAnglefirstpart();
            }
        }
        public void MethodCoordinatesfirstpart()
        {
            NumNodes.Visible = false;
            NumNodesText.Visible = false;
            EnableNodes.Visible = false;
            DataText.Visible = false;
            Qt.Visible = false;
            Qv.Visible = false;
            Grantext.Visible = false;
            Numtext.Visible = false;
            NumG.Visible = false;
            htxt.Visible = false;
            hval.Visible = false;
            Ttxt.Visible = false;
            Tval.Visible = false;
            qtxt.Visible = false;
            qval.Visible = false;
            Show.Visible = false; 
            Change.Visible = false;
            GetSolution.Visible = false;
            Reset.Visible = false;

            Parametrs.Controls.Add(heading);

            heading.Text = "Введите данные:";
            heading.Location = new Point(130, 15);
            heading.Font = new Font("Times New Roman", 15.0f);
            heading.Size = new Size(300, 30);
           
            Parametrs.Controls.Add(NumVertexText);

            NumVertexText.Text = "Количество вершин:";
            NumVertexText.Location = new Point(120, 50);
            NumVertexText.Font = new Font("Times New Roman", 15.0f);
            NumVertexText.Size = new Size(300, 30);
            
            Parametrs.Controls.Add(NumVertex);

            NumVertex.Location = new Point(180, 90);
            NumVertex.Font = new Font("Times New Roman", 12.0f);
            NumVertex.Size = new Size(70, 10);
            NumVertex.TextAlign = HorizontalAlignment.Center;
            
            Parametrs.Controls.Add(CoordinatesVertexText);

            CoordinatesVertexText.Text = "Координаты первой вершины:";
            CoordinatesVertexText.Location = new Point(80, 130);
            CoordinatesVertexText.Font = new Font("Times New Roman", 15.0f);
            CoordinatesVertexText.Size = new Size(400, 30);
           
            Parametrs.Controls.Add(XText);

            XText.Text = "X:";
            XText.Location = new Point(90, 170);
            XText.Font = new Font("Times New Roman", 15.0f);
            XText.Size = new Size(30, 30);
            
            Parametrs.Controls.Add(X);

            X.Location = new Point(120, 167);
            X.Font = new Font("Times New Roman", 12.0f);
            X.Size = new Size(70, 10);
            X.TextAlign = HorizontalAlignment.Center;
            
            Parametrs.Controls.Add(YText);

            YText.Text = "Y:";
            YText.Location = new Point(220, 170);
            YText.Font = new Font("Times New Roman", 15.0f);
            YText.Size = new Size(30, 30);
            
            Parametrs.Controls.Add(Y);

            Y.Location = new Point(250, 167);
            Y.Font = new Font("Times New Roman", 12.0f);
            Y.Size = new Size(70, 10);
            Y.TextAlign = HorizontalAlignment.Center;
            
            Parametrs.Controls.Add(QDis);

            QDis.Text = "Источник тепла внутри тела:";
            QDis.Location = new Point(80, 220);
            QDis.Font = new Font("Times New Roman", 15.0f);
            QDis.Size = new Size(300, 20);
            
            Parametrs.Controls.Add(QText);

            QText.Text = "Q:";
            QText.Location = new Point(145, 260);
            QText.Font = new Font("Times New Roman", 15.0f);
            QText.Size = new Size(30, 30);
            
            Parametrs.Controls.Add(Q);

            Q.Location = new Point(180, 260);
            Q.Font = new Font("Times New Roman", 12.0f);
            Q.Size = new Size(70, 10);
            Q.TextAlign = HorizontalAlignment.Center;


            hdis.Text = "Коэффициент теплообмена:";
            hdis.Location = new Point(100, 290);
            hdis.Font = new Font("Times New Roman", 15.0f);
            hdis.Size = new Size(300, 30);
            Parametrs.Controls.Add(hdis);

            htext.Text = "h:";
            htext.Location = new Point(150, 335);
            htext.Font = new Font("Times New Roman", 15.0f);
            htext.Size = new Size(30, 30);
            Parametrs.Controls.Add(htext);

            h.Location = new Point(180, 330);
            h.Font = new Font("Times New Roman", 15.0f);
            h.Size = new Size(70, 10);
            Parametrs.Controls.Add(h);

            Tdis.Text = "Температура окружающей среды:";
            Tdis.Location = new Point(65, 370);
            Tdis.Font = new Font("Times New Roman", 15.0f);
            Tdis.Size = new Size(300, 30);
            Parametrs.Controls.Add(Tdis);

            Ttext.Text = "T:";
            Ttext.Location = new Point(150, 415);
            Ttext.Font = new Font("Times New Roman", 15.0f);
            Ttext.Size = new Size(30, 30);
            Parametrs.Controls.Add(Ttext);

            T.Location = new Point(180, 410);
            T.Font = new Font("Times New Roman", 15.0f);
            T.Size = new Size(70, 10);
            Parametrs.Controls.Add(T);

            qdis.Text = "Поток тепла:";
            qdis.Location = new Point(150, 450);
            qdis.Font = new Font("Times New Roman", 15.0f);
            qdis.Size = new Size(300, 30);
            Parametrs.Controls.Add(qdis);

            qtext.Text = "q:";
            qtext.Location = new Point(150, 490);
            qtext.Font = new Font("Times New Roman", 15.0f);
            qtext.Size = new Size(30, 30);
            Parametrs.Controls.Add(qtext);

            q.Location = new Point(180, 485);
            q.Font = new Font("Times New Roman", 15.0f);
            q.Size = new Size(70, 10);
            Parametrs.Controls.Add(q);

            Parametrs.Controls.Add(Enterdata);
            Enterdata.Text = "Далее";
            Enterdata.Location = new Point(130, 535);
            Enterdata.Font = new Font("Times New Roman", 15.0f);
            Enterdata.Size = new Size(170, 50);
            Enterdata.BackColor = Color.FromArgb(157,160,163);
           
            Enterdata.Click += Enterdata_Click;
        }
        public void MethodCoordinateslastpart()
        {
            NumVertexText.Visible = false;
            NumVertexText.Visible = false;
            NumVertex.Visible = false;
            QDis.Visible = false;
            QText.Visible = false;
            Q.Visible = false;

            heading.Text = "Введите данные:";
            heading.Location = new Point(130, 15);
            heading.Font = new Font("Times New Roman", 15.0f);
            heading.Size = new Size(300, 30);

            CoordinatesVertexText.Text = "Координаты текущей вершины:";
            CoordinatesVertexText.Location = new Point(70, 50);
            CoordinatesVertexText.Font = new Font("Times New Roman", 15.0f);
            CoordinatesVertexText.Size = new Size(400, 30);

            XText.Text = "X:";
            XText.Location = new Point(90, 90);
            XText.Font = new Font("Times New Roman", 15.0f);
            XText.Size = new Size(30, 30);

            X.Location = new Point(120, 87);
            X.Font = new Font("Times New Roman", 12.0f);
            X.Size = new Size(70, 10);
            X.TextAlign = HorizontalAlignment.Center;

            YText.Text = "Y:";
            YText.Location = new Point(220, 90);
            YText.Font = new Font("Times New Roman", 15.0f);
            YText.Size = new Size(30, 30);

            Y.Location = new Point(250, 87);
            Y.Font = new Font("Times New Roman", 12.0f);
            Y.Size = new Size(70, 10);
            Y.TextAlign = HorizontalAlignment.Center;

            hdis.Text = "Коэффициент теплообмена:";
            hdis.Location = new Point(80, 130);
            hdis.Font = new Font("Times New Roman", 15.0f);
            hdis.Size = new Size(300, 30);   

            htext.Text = "h:";
            htext.Location = new Point(130, 175);
            htext.Font = new Font("Times New Roman", 15.0f);
            htext.Size = new Size(30, 30);

            h.Location = new Point(160, 170);
            h.Font = new Font("Times New Roman", 15.0f);
            h.Size = new Size(90, 10);

            Tdis.Text = "Температура окружающей среды:";
            Tdis.Location = new Point(65, 220);
            Tdis.Font = new Font("Times New Roman", 15.0f);
            Tdis.Size = new Size(300, 30);

            Ttext.Text = "T:";
            Ttext.Location = new Point(130, 265);
            Ttext.Font = new Font("Times New Roman", 15.0f);
            Ttext.Size = new Size(30, 30);

            T.Location = new Point(160, 260);
            T.Font = new Font("Times New Roman", 15.0f);
            T.Size = new Size(90, 10);

            qdis.Text = "Поток тепла:";
            qdis.Location = new Point(150, 310);
            qdis.Font = new Font("Times New Roman", 15.0f);
            qdis.Size = new Size(300, 30);

            qtext.Text = "q:";
            qtext.Location = new Point(130, 355);
            qtext.Font = new Font("Times New Roman", 15.0f);
            qtext.Size = new Size(30, 30);

            q.Location = new Point(160, 350);
            q.Font = new Font("Times New Roman", 15.0f);
            q.Size = new Size(90, 10);

            Enterdata.Location = new Point(135,410);
            Enterdata.Size = new Size(150, 60);
        }
        public void MethodLengthAnglefirstpart()
        {
            NumNodes.Visible = false;
            NumNodesText.Visible = false;
            EnableNodes.Visible = false;
            DataText.Visible = false;
            Qt.Visible = false;
            Qv.Visible = false;
            Grantext.Visible = false;
            Numtext.Visible = false;
            NumG.Visible = false;
            htxt.Visible = false;
            hval.Visible = false;
            Ttxt.Visible = false;
            Tval.Visible = false;
            qtxt.Visible = false;
            qval.Visible = false;
            Show.Visible = false;
            Change.Visible = false;
            GetSolution.Visible = false;
            Reset.Visible = false;

            Parametrs.Controls.Add(heading);

            heading.Text = "Введите данные:";
            heading.Location = new Point(130, 15);
            heading.Font = new Font("Times New Roman", 15.0f);
            heading.Size = new Size(300, 30);

            Parametrs.Controls.Add(NumVertexText);

            NumVertexText.Text = "Количество вершин:";
            NumVertexText.Location = new Point(120, 50);
            NumVertexText.Font = new Font("Times New Roman", 15.0f);
            NumVertexText.Size = new Size(300, 30);

            Parametrs.Controls.Add(NumVertex);

            NumVertex.Location = new Point(180, 90);
            NumVertex.Font = new Font("Times New Roman", 12.0f);
            NumVertex.Size = new Size(70, 10);
            NumVertex.TextAlign = HorizontalAlignment.Center;

            Lendis.Text = "Длину первой стороны:";
            Lendis.Location = new Point(110, 130);
            Lendis.Font = new Font("Times New Roman", 15.0f);
            Lendis.Size = new Size(300, 30);
            Parametrs.Controls.Add(Lendis);

            Len.Location = new Point(180, 170);
            Len.Font = new Font("Times New Roman", 12.0f);
            Len.Size = new Size(70, 10);
            Parametrs.Controls.Add(Len);

            Parametrs.Controls.Add(QDis);

            QDis.Text = "Источник тепла внутри тела:";
            QDis.Location = new Point(80, 210);
            QDis.Font = new Font("Times New Roman", 15.0f);
            QDis.Size = new Size(300, 20);

            Parametrs.Controls.Add(QText);

            QText.Text = "Q:";
            QText.Location = new Point(145, 250);
            QText.Font = new Font("Times New Roman", 15.0f);
            QText.Size = new Size(30, 30);

            Parametrs.Controls.Add(Q);

            Q.Location = new Point(180, 250);
            Q.Font = new Font("Times New Roman", 12.0f);
            Q.Size = new Size(70, 10);
            Q.TextAlign = HorizontalAlignment.Center;

            hdis.Text = "Коэффициент теплообмена:";
            hdis.Location = new Point(100, 290);
            hdis.Font = new Font("Times New Roman", 15.0f);
            hdis.Size = new Size(300, 30);
            Parametrs.Controls.Add(hdis);

            htext.Text = "h:";
            htext.Location = new Point(150, 335);
            htext.Font = new Font("Times New Roman", 15.0f);
            htext.Size = new Size(30, 30);
            Parametrs.Controls.Add(htext);

            h.Location = new Point(180, 330);
            h.Font = new Font("Times New Roman", 15.0f);
            h.Size = new Size(70, 10);
            Parametrs.Controls.Add(h);

            Tdis.Text = "Температура окружающей среды:";
            Tdis.Location = new Point(65, 370);
            Tdis.Font = new Font("Times New Roman", 15.0f);
            Tdis.Size = new Size(300, 30);
            Parametrs.Controls.Add(Tdis);

            Ttext.Text = "T:";
            Ttext.Location = new Point(150, 415);
            Ttext.Font = new Font("Times New Roman", 15.0f);
            Ttext.Size = new Size(30, 30);
            Parametrs.Controls.Add(Ttext);

            T.Location = new Point(180, 410);
            T.Font = new Font("Times New Roman", 15.0f);
            T.Size = new Size(70, 10);
            Parametrs.Controls.Add(T);

            qdis.Text = "Поток тепла:";
            qdis.Location = new Point(150, 450);
            qdis.Font = new Font("Times New Roman", 15.0f);
            qdis.Size = new Size(300, 30);
            Parametrs.Controls.Add(qdis);

            qtext.Text = "q:";
            qtext.Location = new Point(150, 490);
            qtext.Font = new Font("Times New Roman", 15.0f);
            qtext.Size = new Size(30, 30);
            Parametrs.Controls.Add(qtext);

            q.Location = new Point(180, 485);
            q.Font = new Font("Times New Roman", 15.0f);
            q.Size = new Size(70, 10);
            Parametrs.Controls.Add(q);

            Enterdata.Text = "Далее";
            Enterdata.Location = new Point(130, 535);
            Enterdata.Font = new Font("Times New Roman", 15.0f);
            Enterdata.Size = new Size(170, 50);
            Enterdata.BackColor = Color.FromArgb(157, 160, 163);

            Parametrs.Controls.Add(Enterdata);
            Enterdata.Click += Enterdata_Click;
        }
        public void MethodLengthAnglelastpart()
        {
            NumVertexText.Visible = false;
            NumVertexText.Visible = false;
            NumVertex.Visible = false;
            QDis.Visible = false;
            QText.Visible = false;
            Q.Visible = false;

            heading.Text = "Введите данные:";
            heading.Location = new Point(130, 15);
            heading.Font = new Font("Times New Roman", 15.0f);
            heading.Size = new Size(300, 30);

            Lendis.Text = "Длину текущей стороны:";
            Lendis.Location = new Point(110, 50);
            Lendis.Font = new Font("Times New Roman", 15.0f);
            Lendis.Size = new Size(300, 30);

            Len.Location = new Point(180, 90);
            Len.Font = new Font("Times New Roman", 12.0f);
            Len.Size = new Size(70, 10);

            Angledis.Text = "Угол между предыдущей и текущей стороной:";
            Angledis.Location = new Point(75, 130);
            Angledis.Font = new Font("Times New Roman", 15.0f);
            Angledis.Size = new Size(300, 50);
            Angledis.TextAlign = ContentAlignment.MiddleCenter;
            Parametrs.Controls.Add(Angledis);

            Angle.Location = new Point(180, 190);
            Angle.Font = new Font("Times New Roman", 12.0f);
            Angle.Size = new Size(70, 10);
            Parametrs.Controls.Add(Angle);

            hdis.Text = "Коэффициент теплообмена:";
            hdis.Location = new Point(100, 230);
            hdis.Font = new Font("Times New Roman", 15.0f);
            hdis.Size = new Size(300, 30);


            htext.Text = "h:";
            htext.Location = new Point(150, 27);
            htext.Font = new Font("Times New Roman", 15.0f);
            htext.Size = new Size(30, 30);


            h.Location = new Point(180, 265);
            h.Font = new Font("Times New Roman", 15.0f);
            h.Size = new Size(70, 10);


            Tdis.Text = "Температура окружающей среды:";
            Tdis.Location = new Point(65, 310);
            Tdis.Font = new Font("Times New Roman", 15.0f);
            Tdis.Size = new Size(300, 30);


            Ttext.Text = "T:";
            Ttext.Location = new Point(150, 350);
            Ttext.Font = new Font("Times New Roman", 15.0f);
            Ttext.Size = new Size(30, 30);


            T.Location = new Point(180, 350);
            T.Font = new Font("Times New Roman", 15.0f);
            T.Size = new Size(70, 10);
     
            qdis.Text = "Поток тепла:";
            qdis.Location = new Point(150, 390);
            qdis.Font = new Font("Times New Roman", 15.0f);
            qdis.Size = new Size(300, 30);

            qtext.Text = "q:";
            qtext.Location = new Point(150, 425);
            qtext.Font = new Font("Times New Roman", 15.0f);
            qtext.Size = new Size(30, 30);

            q.Location = new Point(180, 425);
            q.Font = new Font("Times New Roman", 15.0f);
            q.Size = new Size(70, 10);

            Enterdata.Location = new Point(140, 470);
            Enterdata.Size = new Size(150, 60);
        }
        public void FinalView()
        {          
            NumVertexText.Visible = false;
            NumVertex.Visible = false;
            CoordinatesVertexText.Visible = false;
            XText.Visible = false;
            X.Visible = false;
            YText.Visible = false;
            Y.Visible = false;
            QDis.Visible = false;
            QText.Visible = false;
            heading.Visible= false;
            T.Visible = false;
            h.Visible = false;
            q.Visible = false;
            Ttext.Visible = false;
            htext.Visible = false;
            qtext.Visible = false;
            Tdis.Visible = false;
            hdis.Visible = false;
            qdis.Visible = false;
            Lendis.Visible = false;
            Len.Visible = false;
            Angledis.Visible = false;
            Angle.Visible = false;
            Enterdata.Visible = false;

            NumNodes.Visible = true;
            NumNodesText.Visible = true;
            EnableNodes.Visible = true;

            DataText.Visible = true;
            Qt.Visible = true;
            Qv.Visible = true;
            Qv.Enabled = false;

            Grantext.Visible = true;
            Numtext.Visible = true;
            NumG.Visible = true;

            htxt.Visible = true;
            hval.Visible = true;
            hval.Enabled= false;

            Ttxt.Visible = true;
            Tval.Visible = true;
            Tval.Enabled= false;

            qtxt.Visible = true;
            qval.Visible = true;
            qval.Enabled=false;

            Show.Visible = true;
            Change.Visible = true;

            GetSolution.Visible = true;
            Reset.Visible = true;

            Qv.Text = Q.Text;

            hval.Text = ParametrsValue[2][0].ToString();
            Tval.Text = ParametrsValue[3][0].ToString();
            qval.Text = ParametrsValue[4][0].ToString();
            NumG.Text = "1";
        }
        public void Enterdata_Click(object? sender, EventArgs e)
        {
            
            if ( method == 1)
            {
                try
                {
                    if (Count == 0)
                    {
                        NumVertex.Text = 11.ToString();
                        ParametrsValue = new decimal[5][];
                        ParametrsValue[0] = new decimal[int.Parse(NumVertex.Text.ToString())];//координаты х 
                        ParametrsValue[1] = new decimal[int.Parse(NumVertex.Text.ToString())];//координаты у 
                        ParametrsValue[2] = new decimal[int.Parse(NumVertex.Text.ToString())];// h
                        ParametrsValue[3] = new decimal[int.Parse(NumVertex.Text.ToString())];// T
                        ParametrsValue[4] = new decimal[int.Parse(NumVertex.Text.ToString())];// q

                        MethodCoordinateslastpart();
                    }

                        ParametrsValue[0][Count] = decimal.Parse(X.Text);
                        ParametrsValue[1][Count] = decimal.Parse(Y.Text);

                        ParametrsValue[2][Count] = decimal.Parse(h.Text);
                        ParametrsValue[3][Count] = decimal.Parse(T.Text);
                        ParametrsValue[4][Count] = decimal.Parse(q.Text);                 
                }
                catch
                {
                    /* 
                     * MessageBox.Show("Проверьте введенные данные. \nЗначения параметров должны являться вещественными числами");

                     if (i == 0)
                     {
                         X.Text = null;
                         Y.Text = null;
                         Q.Text = null;
                         NumVertex.Text = null;
                     }
                     else
                     {
                         X.Text = null;
                         Y.Text = null;

                         h.Text = null;
                         T.Text = null;
                         q.Text = null;
                     }
                     i--;*/


                    ParametrsValue[0][0] = -5;
                    ParametrsValue[1][0] = -2;

                    ParametrsValue[0][1] = 9;
                    ParametrsValue[1][1] = 0;

                    ParametrsValue[0][2] = 10;
                    ParametrsValue[1][2] = 1;

                    ParametrsValue[0][3] = 10;
                    ParametrsValue[1][3] = 5;

                    ParametrsValue[0][4] = 9;
                    ParametrsValue[1][4] = 5;

                    ParametrsValue[0][5] = 6;
                    ParametrsValue[1][5] = 2;

                    ParametrsValue[0][6] = 4;
                    ParametrsValue[1][6] = 2;

                    ParametrsValue[0][7] = 7;
                    ParametrsValue[1][7] = 6;

                    ParametrsValue[0][8] = 5;
                    ParametrsValue[1][8] = 6;

                    ParametrsValue[0][9] = 0;
                    ParametrsValue[1][9] = 2;

                    ParametrsValue[0][10] = -7;
                    ParametrsValue[1][10] = 0;

                    Count = int.Parse(NumVertex.Text);
                    Q.Text = "0";
                }

                if (Count > 0 && Count == int.Parse(NumVertex.Text))
                {
                    solution = new Solution(int.Parse(NumVertex.Text), int.Parse(Q.Text), method, ParametrsValue);

                    FinalView();
                   
                    XOY.CreateGraphics().Clear(XOY.BackColor);

                    solution.BuildGrid();
                    solution.ShowGrid(XOY);
                    solution.Triangulation(XOY);
                }

                if (Count > 0 && Count == int.Parse(NumVertex.Text) - 2)
                {
                    Enterdata.Text = "Построить сетку";
                }

                X.Text = null;
                Y.Text = null;
                h.Text = null;
                T.Text = null;
                q.Text = null;

                Count++;
            }
            else
            {
                try
                {
                    if (Count == 0)
                    {
                        ParametrsValue = new decimal[5][];
                        ParametrsValue[0] = new decimal[int.Parse(NumVertex.Text.ToString())];//длины сторон 
                        ParametrsValue[1] = new decimal[int.Parse(NumVertex.Text.ToString())];//углы
                        ParametrsValue[2] = new decimal[int.Parse(NumVertex.Text.ToString())];// h
                        ParametrsValue[3] = new decimal[int.Parse(NumVertex.Text.ToString())];// T
                        ParametrsValue[4] = new decimal[int.Parse(NumVertex.Text.ToString())];// q
;
                        ParametrsValue[0][Count] = decimal.Parse(Len.Text);
                        ParametrsValue[1][Count] = 0;

                        ParametrsValue[2][Count] = decimal.Parse(h.Text);
                        ParametrsValue[3][Count] = decimal.Parse(T.Text);
                        ParametrsValue[4][Count] = decimal.Parse(q.Text);

                        MethodLengthAnglelastpart();
                    }
                    else
                    {
                        ParametrsValue[0][Count] = decimal.Parse(Len.Text);
                        ParametrsValue[1][Count] = decimal.Parse(Angle.Text);

                        ParametrsValue[2][Count] = decimal.Parse(h.Text);
                        ParametrsValue[3][Count] = decimal.Parse(T.Text);
                        ParametrsValue[4][Count] = decimal.Parse(q.Text);
                    }
                }
                catch
                {
                    MessageBox.Show("Проверьте введенные данные. \nЗначения параметров должны являться вещественными числами");

                    if (Count == 0)
                    {
                        Len.Text = null;
                        Q.Text = null;
                        NumVertex.Text = null;
                    }
                    else
                    {
                        Len.Text = null;
                        Angle.Text = null;

                        h.Text = null;
                        T.Text = null;
                        q.Text = null;
                    }
                    Count--;
                }

                if (Count == int.Parse(NumVertex.Text) - 1)
                {
                    solution = new Solution(int.Parse(NumVertex.Text), int.Parse(Q.Text), method, ParametrsValue);

                    FinalView();

                    XOY.CreateGraphics().Clear(XOY.BackColor);

                    solution.BuildGrid();                  
                    solution.ShowGrid(XOY);
                    solution.Triangulation(XOY);
                }

                if(Count == int.Parse(NumVertex.Text) - 2)
                {
                    Enterdata.Text = "Построить сетку";
                }

                Len.Text = null;
                Angle.Text = null;
                h.Text = null;
                T.Text = null;
                q.Text = null;
                Count++;
            }           
        }
        private void Show_Click(object sender, EventArgs e)
        {
            solution.ShowBorder(XOY, int.Parse(NumG.Text));

            hval.Text = ParametrsValue[2][int.Parse(NumG.Text) - 1].ToString();
            Tval.Text = ParametrsValue[3][int.Parse(NumG.Text) - 1].ToString();
            qval.Text = ParametrsValue[4][int.Parse(NumG.Text) - 1].ToString();
        }
        private void Change_Click(object sender, EventArgs e)
        {
            if(Change.Text == "Изменить")
            {
                Change.Text = "Применить";
                hval.Enabled = true;
                Tval.Enabled = true;
                qval.Enabled = true;
                Qv.Enabled = true;
            }

            if (ParametrsValue[2][int.Parse(NumG.Text) - 1] != decimal.Parse(hval.Text) || ParametrsValue[3][int.Parse(NumG.Text) - 1] != decimal.Parse(Tval.Text) || ParametrsValue[4][int.Parse(NumG.Text) - 1] != decimal.Parse(qval.Text) || Q.Text != Qv.Text)
            {
                ParametrsValue[2][int.Parse(NumG.Text) - 1] = decimal.Parse(hval.Text);
                ParametrsValue[3][int.Parse(NumG.Text) - 1] = decimal.Parse(Tval.Text);
                ParametrsValue[4][int.Parse(NumG.Text) - 1] = decimal.Parse(qval.Text);
                Q.Text = Qv.Text;

                Change.Text = "Изменить";

                hval.Enabled = false;
                Tval.Enabled = false;
                qval.Enabled = false;
                Qv.Enabled = false;
            }           
        }
        private void GetSolution_Click(object sender, EventArgs e)
        {
            
        }
        private void XOY_MouseClick(object sender, MouseEventArgs e)
        {
            if(Count != 0)
            {
                PointF PointLine = new();

                PointLine.X = e.Location.X;
                PointLine.Y = e.Location.Y;

                
                int NumBorder = solution.PointOnLine(XOY, PointLine);

                if (NumBorder != 0)
                {
                    solution.ShowBorder(XOY, NumBorder);

                    NumG.Text = NumBorder.ToString();
                    hval.Text = ParametrsValue[2][NumBorder - 1].ToString();
                    Tval.Text = ParametrsValue[3][NumBorder - 1].ToString();
                    qval.Text = ParametrsValue[4][NumBorder - 1].ToString();
                }

                if(solution.PointCloseNode(XOY, PointLine) != 0)
                {
                    NumNodes.Text = (solution.PointCloseNode(XOY, PointLine) + 1).ToString();
                }
            }                           
        }
        private void EnableNodes_Click(object sender, EventArgs e)
        {
            string NodesFirstPart = "", NodesLastPart = "";

            int NodesIndex = NumNodes.Text.Length;
            
            PanelWidth = XOY.Width;
            PanelHeight = XOY.Height;

            graphics = XOY.CreateGraphics();
            graphics.Clear(XOY.BackColor);

            solution.GetNumberNodes();
            solution.ShowGrid(XOY);

            for (int i = 0; i < solution.Triangles.Count; i++)
            {
                solution.DisplayTriangle(XOY, solution.Triangles[i]);
            }

            for (int i = 0; i < NumNodes.Text.Length; i++)
            {
                if (NumNodes.Text[i] == '-')
                {
                    NodesIndex = i;
                    break;
                }
            }
            for (int i = 0; i < NumNodes.Text.Length; i++)
            {                
                if(i < NodesIndex)
                {
                    NodesFirstPart += NumNodes.Text[i];
                }
                else if(i > NodesIndex)
                {
                    NodesLastPart += NumNodes.Text[i];
                }
            }

            if(NodesLastPart != "")
            {
                for (int i = int.Parse(NodesFirstPart); i <= int.Parse(NodesLastPart); i++)
                {
                    graphics.DrawString(i.ToString(), new Font(FontFamily.GenericSansSerif, 12f), new SolidBrush(Color.Red), PanelWidth / 10 + 38 * solution.NFound[i - 1].X, 4 * PanelHeight / 5 - 38 * solution.NFound[i - 1].Y);
                    graphics.DrawEllipse(new Pen(Color.FromArgb(27, 42, 207), 2.0f), PanelWidth / 10 + 38 * solution.NFound[i - 1].X, 4 * PanelHeight / 5 - 38 * solution.NFound[i - 1].Y, 3, 3);
                }
            }
            else
            {
                graphics.DrawString(NumNodes.Text, new Font(FontFamily.GenericSansSerif, 12f), new SolidBrush(Color.Red), PanelWidth / 10 + 38 * solution.NFound[int.Parse(NodesFirstPart) - 1].X, 4 * PanelHeight / 5 - 38 * solution.NFound[int.Parse(NodesFirstPart) - 1].Y);
                graphics.DrawEllipse(new Pen(Color.FromArgb(27, 42, 207), 2.0f), PanelWidth / 10 + 38 * solution.NFound[int.Parse(NodesFirstPart) - 1].X, 4 * PanelHeight / 5 - 38 * solution.NFound[int.Parse(NodesFirstPart) - 1].Y, 3, 3);
            }
        }
    }
}