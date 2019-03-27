using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace gMapeTest1
{
    public partial class Form1 : Form
    {
        //声明自适应窗口实例
        AutoSizeFormClass asc = new AutoSizeFormClass();
        public Form1()
        {
            InitializeComponent();
        }

        private void gMapControl1_Load(object sender, EventArgs e)
        {
            #region 加载在线地图
            //this.gMapControl1.CacheLocation = System.Windows.Forms.Application.StartupPath;//指定地图缓存存放路径
            //this.gMapControl1.MapProvider = GMapProviders.GoogleChinaMap;//指定地图源
            ////this.gMapControl1.MapProvider = GMapProviders.BingHybridMap;//指定地图源
            //this.gMapControl1.Manager.Mode = AccessMode.ServerAndCache;//地图加载模式
            //this.gMapControl1.MinZoom = 1;   //最小比例
            //this.gMapControl1.MaxZoom = 23; //最大比例
            //this.gMapControl1.Zoom = 9; //当前比例
            //this.gMapControl1.ShowCenter = false; //不显示中心十字点
            //this.gMapControl1.DragButton = System.Windows.Forms.MouseButtons.Left;//左键拖拽地图
            //this.gMapControl1.Position = new PointLatLng(31, 104);
            #endregion

            #region 加载离线地图
            this.gMapControl1.MapProvider = GMapProviders.GoogleChinaMap;
            this.gMapControl1.Manager.Mode = AccessMode.CacheOnly;
            String mapPath = "F:\\VsWorkspace\\GoogleChainMap.gmdb";
            GMap.NET.GMaps.Instance.ImportFromGMDB(mapPath);
            this.gMapControl1.MinZoom = 1;   //最小比例
            this.gMapControl1.MaxZoom = 9; //最大比例
            this.gMapControl1.Zoom = 6; //当前比例
            this.gMapControl1.ShowCenter = false; //不显示中心十字点
            this.gMapControl1.DragButton = System.Windows.Forms.MouseButtons.Left;//左键拖拽地图
            this.gMapControl1.Position = new PointLatLng(31, 104);
            #endregion
        }

        //public void dataBind()
        //{
        //    DataTable dt = new DataTable();
        //    dt.Columns.Add(new DataColumn("时"));
        //    dt.Columns.Add(new DataColumn("分"));
        //    dt.Columns.Add(new DataColumn("秒"));
        //    dt.Columns.Add(new DataColumn("RF"));
        //    dt.Columns.Add(new DataColumn("PRI"));
        //    dt.Columns.Add(new DataColumn("PW"));
        //    DataRow dr;
        //    for (int i = 0; i < 10; i++)
        //    {
        //        dr = dt.NewRow();
        //        dr["时"] = "11";
        //        dr["分"] = "12";
        //        dr["秒"] = "13";
        //        dr["RF"] = "5986";
        //        dr["PRI"] = "1";
        //        dr["PW"] = "1";
        //        dt.Rows.Add(dr);
        //    }

        //    this.dataGridView1.DataSource = dt;

        //}

        private void Form1_Load(object sender, EventArgs e)
        {
            this.dataGridView1.Show();
            //2. 调用类的初始化方法，记录窗体和其控件的初始位置和大小
            asc.controllInitializeSize(this);
            //标记
            RadarMarker();
        }

        //3.为窗体添加SizeChanged事件，并在其方法Form1_SizeChanged中，调用类的自适应方法，完成自适应  
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            asc.controlAutoSize(this);
        }

        //radar标注，添加图层
        public void RadarMarker()
        {
            GMapOverlay RadarOverlay =new GMapOverlay("RadarOverlay");
            string path = "F:\\VsWorkspace\\gMapeTest1\\gMapeTest1\\Image\\radar1.jpg";
            addMarker(path, RadarOverlay, new PointLatLng(30, 104));
            addMarker(path, RadarOverlay, new PointLatLng(31, 105));
        }
       
        //标注图层添加方法
        private void addMarker(string RadarPath, GMapOverlay markBrand, PointLatLng point)
        {
            Bitmap bitmap = null;
            bitmap = Bitmap.FromFile(RadarPath) as Bitmap;
            GMapMarker marker = new GMarkerGoogle(point, bitmap);
            markBrand.Markers.Add(marker);
            this.gMapControl1.Overlays.Add(markBrand);
        }
       
        //划线
        public void Line()
        {
            GMapOverlay DirecLine = new GMapOverlay("DirecLine");
            List<double> StartListLat = new List<double>();
            StartListLat.Add(30);
            StartListLat.Add(30);
            StartListLat.Add(30);
            StartListLat.Add(30);
            List<double> StartListLng = new List<double>();
            StartListLng.Add(104);
            StartListLng.Add(104);
            StartListLng.Add(104);
            StartListLng.Add(104);
            List<double> TerminalListLat = new List<double>();
            TerminalListLat.Add(33);
            TerminalListLat.Add(33);
            TerminalListLat.Add(33);
            TerminalListLat.Add(33);
            List<double> TerminalListLng = new List<double>();
            TerminalListLng.Add(105);
            TerminalListLng.Add(115);
            TerminalListLng.Add(125);
            TerminalListLng.Add(130);
            for (int i = 0; i < StartListLat.Count; i++)
            {
                PointLatLng StartP = new PointLatLng(StartListLat[i], StartListLng[i]);
                PointLatLng TerminalP = new PointLatLng(TerminalListLat[i], TerminalListLng[i]);
                DrawDirectionLine(DirecLine, StartP, TerminalP);
            }

        }
        
        //划线方法
        public void DrawDirectionLine(GMapOverlay DirecLine, PointLatLng StartP, PointLatLng TerminalP)
        {
            List<PointLatLng> list = new List<PointLatLng>();
            list.Add(StartP);
            list.Add(TerminalP);
            GMapPolygon line = new GMapPolygon(list, "line");
            DirecLine.Polygons.Add(line);
            this.gMapControl1.Overlays.Add(DirecLine);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Line();
        }

        //从excel导入数据
        public static DataTable ReadExcelToTable(string path)//excel存放的路径
        {
            try
            {
                string strExtension = System.IO.Path.GetExtension(path);
                string strFileName = System.IO.Path.GetFileName(path);
                String connstring;//数据库连接字符串
                switch (strExtension)
                {
                    case ".xls":
                        connstring = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";" + "Extended Properties=\"Excel 8.0;HDR=yes;IMEX=1;\"";
                        break;
                    case ".xlsx":
                        connstring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";" + "Extended Properties=\"Excel 12.0;HDR=yes;IMEX=1;\"";//此连接可以操作.xls与.xlsx文件 (支持Excel2003 和 Excel2007 的连接字符串)  备注： "HDR=yes;"是说Excel文件的第一行是列名而不是数，"HDR=No;"正好与前面的相反。"IMEX=1 "如果列中的数据类型不一致，使用"IMEX=1"可必免数据类型冲突。 
                        break;
                    default:
                        connstring = null;
                        break;
                }

                using (OleDbConnection conn = new OleDbConnection(connstring))
                {
                    conn.Open();
                    DataTable sheetsName = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "Table" }); //得到所有sheet的名字
                    string firstSheetName = sheetsName.Rows[0][2].ToString(); //得到第一个sheet的名字
                    string sql = string.Format("SELECT * FROM [{0}]", firstSheetName); //查询字符串                    //string sql = string.Format("SELECT * FROM [{0}] WHERE [日期] is not null", firstSheetName); //查询字符串
                    OleDbDataAdapter ada = new OleDbDataAdapter(sql, connstring);
                    DataSet set = new DataSet();
                    ada.Fill(set);
                    conn.Close();
                    return set.Tables[0];
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //批号信息表
            DataTable dt1 = new DataTable();
            //详细定位
            DataTable dt2 = new DataTable();
            
            OpenFileDialog file = new OpenFileDialog();
            file.Multiselect = true;
            file.ShowDialog();
            String[] path = file.FileNames;
            string strFileName;
            for (int i = 0; i < path.Length; i++)
            {
                strFileName = System.IO.Path.GetFileNameWithoutExtension(path[i]);
                switch (strFileName)
                {
                    case "批号信息":
                        {
                            dt1= ReadExcelToTable(path[i]);
                        }
                        break;
                    case "详细定位":
                        {
                            dt2 = ReadExcelToTable(path[i]);
                        }
                        break;
                    default:
                        break;
                } 
            }
            //DataTable dt = ReadExcelToTable(path);
            #region 提取表中第一行作为表头
            //DataTable dt2 = new DataTable();            
            ////提取第一列作为表头
            //for (int i = 0; i < dt.Columns.Count; i++)
            //{
            //    dt2.Columns.Add(new DataColumn(dt.Rows[0][i].ToString()));
            //}
            ////填充dt2
            //for (int i = 1; i < dt.Rows.Count; i++)
            //{
            //    //dt2.Rows.Add(dt.Rows[i]) ;
            //    DataRow dr = dt2.NewRow();
            //    for (int j = 0; j < dt.Columns.Count; j++)
            //    {
            //        dr[dt2.Columns[j].ColumnName] = dt.Rows[i][j];
            //    }
            //    dt2.Rows.Add(dr);
            //    //DataRow dr = dt.Rows[i];
            //    //dt2.ImportRow(dt.Rows[i]);
            //}
            #endregion
            this.dataGridView1.DataSource = dt1;
        }


        //class GMapMarkerImage : GMapMarker
        //{
        //    private Image image;
        //    public Image Image
        //    {
        //        get
        //        {
        //            return image;
        //        }
        //        set
        //        {
        //            image = value;
        //            if (image != null)
        //            {
        //                this.Size = new Size(image.Width, image.Height);
        //            }
        //        }
        //    }
        //    public Pen Pen
        //    {
        //        get;
        //        set;
        //    }
        //    public Pen OutPen
        //    {
        //        get;
        //        set;
        //    }
        //    public GMapMarkerImage(GMap.NET.PointLatLng p, Image image) : base(p)
        //    {
        //        Size = new System.Drawing.Size(image.Width, image.Height);
        //        Offset = new System.Drawing.Point(-Size.Width / 2, -Size.Height / 2);
        //        this.image = image;
        //        Pen = null;
        //        OutPen = null;
        //    }

        //    public override void OnRender(Graphics g)
        //    {
        //        if (image == null)
        //            return;

        //        Rectangle rect = new Rectangle(LocalPosition.X, LocalPosition.Y, Size.Width, Size.Height);
        //        g.DrawImage(image, rect);

        //        if (Pen != null)
        //        {
        //            g.DrawRectangle(Pen, rect);
        //        }

        //        if (OutPen != null)
        //        {
        //            g.DrawEllipse(OutPen, rect);
        //        }
        //    }

        //    public override void Dispose()
        //    {
        //        if (Pen != null)
        //        {
        //            Pen.Dispose();
        //            Pen = null;
        //        }

        //        if (OutPen != null)
        //        {
        //            OutPen.Dispose();
        //            OutPen = null;
        //        }

        //        base.Dispose();
        //    }
        //}
    }
}
