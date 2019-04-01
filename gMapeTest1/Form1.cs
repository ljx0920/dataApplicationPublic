using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace gMapeTest1
{
    
    public delegate void moveCenter(float lat, float lng);
    public partial class Form1 : Form
    {
        //变量定义
        


        //声明自适应窗口实例
        AutoSizeFormClass asc = new AutoSizeFormClass();
        public Form1()
        {
            InitializeComponent();
            positonForm = new positionInForm();
            excelata = new excelDeal();

            /*
             #region 加载在线地图
             this.gMapControl1.CacheLocation = System.Windows.Forms.Application.StartupPath;//指定地图缓存存放路径
            this.gMapControl1.MapProvider = GMapProviders.GoogleChinaMap;//指定地图源
            //this.gMapControl1.MapProvider = GMapProviders.BingHybridMap;//指定地图源
            //this.gMapControl1.Manager.Mode = AccessMode.ServerAndCache;//地图加载模式
            this.gMapControl1.Manager.Mode = AccessMode.CacheOnly;
            this.gMapControl1.MinZoom = 1;   //最小比例
            this.gMapControl1.MaxZoom = 23; //最大比例
            this.gMapControl1.Zoom = 9; //当前比例
            this.gMapControl1.ShowCenter = false; //不显示中心十字点
            this.gMapControl1.DragButton = System.Windows.Forms.MouseButtons.Left;//左键拖拽地图
            this.gMapControl1.Position = new PointLatLng(31, 104);

            #endregion
            */
            #region 加载离线地图

            this.gMapControl1.MapProvider = GMapProviders.GoogleChinaMap;
            this.gMapControl1.Manager.Mode = AccessMode.CacheOnly;
            String mapPath = "F:\\VsWorkspace\\GoogleChainMap1.gmdb";
            GMap.NET.GMaps.Instance.ImportFromGMDB(mapPath);
            this.gMapControl1.MinZoom = 1;   //最小比例
            this.gMapControl1.MaxZoom = 9; //最大比例
            this.gMapControl1.Zoom = 6; //当前比例
            this.gMapControl1.ShowCenter = false; //不显示中心十字点
            this.gMapControl1.DragButton = System.Windows.Forms.MouseButtons.Left;//左键拖拽地图
            this.gMapControl1.Position = new PointLatLng(31, 104);
            positonForm.gMapControl1 = this.gMapControl1;


            // 调用类的初始化方法，记录窗体和其控件的初始位置和大小，窗口及控件的自适应
            asc.controllInitializeSize(this);
            //标会雷达
            RadarMarker();

        }

       
        /*
         * @atuhor jy
         * @description:窗口大小改变时，触发控件自适应改变
         * @input:
         * @return:void
         * @tip:
         */
        //3.为窗体添加SizeChanged事件，并在其方法Form1_SizeChanged中，调用类的自适应方法，完成自适应  
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            asc.controlAutoSize(this);
        }

        //radar标注，添加图层
        /*
         * @author:jy
         * @descript:地图中添加雷达库的标会,转化坐标后，调用addMarker
         * @input:map(经度坐标，纬度坐标)
         * @return:
         * @tip:
         */
        public void RadarMarker()
        {
            GMapOverlay A =new GMapOverlay("A");
            GMapOverlay B = new GMapOverlay("B");

            string path = "C:\\Users\\Administrator\\Source\\Repos\\dataApplicationPublic\\gMapeTest1\\Image\\radar1.jpg";
            List<PointLatLng> points = new List<PointLatLng>();
            List<PointLatLng> points1 = new List<PointLatLng>();
            PointLatLng test = new PointLatLng(39.92244, 100.3922);
            PointLatLng testnew = transitionToLatPoint(test, 20, 16, true);
            points.Add(new PointLatLng(39.92244, 100.3922));
            points.Add(new PointLatLng(39.92280, 116.4015));
            points1.Add(new PointLatLng(39.92244, 100.3922));
            points1.Add(testnew);
            GMapPolygon polygon = new GMapPolygon(points, "故宫");
            polygon.Fill = new SolidBrush(Color.FromArgb(50, Color.Red));
            polygon.Stroke = new Pen(Color.Red, 1);

            GMapPolygon polygon1 = new GMapPolygon(points1, "故宫1");
            polygon1.Fill= new SolidBrush(Color.FromArgb(50, Color.Red)); ;
            polygon1.Stroke = new Pen(Color.Red, 1);

            A.Polygons.Add(polygon);
            A.Polygons.Add(polygon1);
            addMarker(path, A, new PointLatLng(30, 104));
            addMarker(path, A, new PointLatLng(31, 105));
            addMarker(path, A, new PointLatLng(39.92244, 100.3922));
            
        }

        //标注图层添加方法
        /*
         * @author:jy
         * @descript:地图中添加图层，在地图上画点
         * @input:
         *      RadarPath:图标路径
         *      markBrand:目标图层
         *      point:标会的经纬坐标点
         * @return:
         * @tip:
         */
        private void addMarker(string RadarPath, GMapOverlay markBrand, PointLatLng point)
        {
            Bitmap bitmap = null;
            bitmap = Bitmap.FromFile(RadarPath) as Bitmap;
            GMapMarker marker = new GMarkerGoogle(point, bitmap);
            markBrand.Markers.Add(marker);
            this.gMapControl1.Overlays.Add(markBrand);
        }

        //划线测试函数
        /*
         * @author:jy
         * @descript:地图中添加图层，在地图上画线
         * @input:
         * @return:
         * @tip:
         */
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
        /*
         * @author:jy
         * @descript:地图中添加图层，在地图上画线（两点划线）
         * @input:
         *         DirecLine:图层
         *         StartP：起始点
         *         TerminalP：终止点
         * @return:
         * @tip:
         */
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
        /*
         * @author:jy
         * @descript:选中文件夹路径，导入路径下的excel（excel名称写死）
         * @input:
         *         path:文件夹路径
         * @return:
         * @tip:
         */
        public static DataTable ReadExcelToTable(string path)//excel存放的路径
        {
            try
            {
                string strExtension = System.IO.Path.GetExtension(path);
                string strFileName = System.IO.Path.GetFileName(path);
                string csvpath=System.IO.Path.GetDirectoryName(path);
                String connstring;//数据库连接字符串
                switch (strExtension)
                {
                    case ".xls":
                        connstring = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";" + "Extended Properties=\"Excel 8.0;HDR=yes;IMEX=1;\"";
                        break;
                    case ".xlsx":
                        connstring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";" + "Extended Properties=\"Excel 12.0;HDR=yes;IMEX=1;\"";//此连接可以操作.xls与.xlsx文件 (支持Excel2003 和 Excel2007 的连接字符串)  备注： "HDR=yes;"是说Excel文件的第一行是列名而不是数，"HDR=No;"正好与前面的相反。"IMEX=1 "如果列中的数据类型不一致，使用"IMEX=1"可必免数据类型冲突。 
                        break;
                    case ".csv":
                        connstring= "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + csvpath +"\\"+ ";Extended Properties='Text;FMT=Delimited;HDR=YES;'";
                        
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
            catch (Exception e)
            {
                System.Console.WriteLine(e);
                return null;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //批号信息表
            DataTable dt1 = new DataTable();
            //详细定位
            DataTable dt2 = new DataTable();

            //OpenFileDialog file = new OpenFileDialog();
            //file.Multiselect = true;
            //file.ShowDialog();
            //String[] path = file.FileNames;

            FolderBrowserDialog folder = new FolderBrowserDialog();
            folder.ShowDialog();
            string folderPath = folder.SelectedPath;
            DirectoryInfo files = new DirectoryInfo(folderPath);
            List<string> listFilePath = new List<string>();
            foreach (FileInfo file in files.GetFiles())
            {
                listFilePath.Add(file.FullName);
            }
            string strFileName;
            for (int i = 0; i < listFilePath.Count; i++)
            {
                strFileName = System.IO.Path.GetFileNameWithoutExtension(listFilePath[i]);
                switch (strFileName)
                {
                    case "批号信息":
                        {
                            dt1 = ReadExcelToTable(listFilePath[i]);
                        }
                        break;
                    case "详细定位":
                        {
                            dt2 = ReadExcelToTable(listFilePath[i]);
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


        /*
         * @author:ljx
         * @descript:坐标点加方向角，转化为两个点,支撑划线
         * @input:
         *      point:坐标点
         *      float:方向角
         *      float:方向角上延伸的距离
         *      sgin:标志位,true代表输入angle为角度，否则为弧度
         * @return:
         *      point:坐标点按方向角延伸一定距离的点
         * @tip:
         * 
         */
        PointLatLng transitionToLatPoint(PointLatLng inputPoint, float angle, float distance,bool sgin) {
            PointLatLng result = new PointLatLng();
            if (true.Equals(sgin))
            {
                double angleRad = (angle * Math.PI)/ 180;
                result.Lat = inputPoint.Lat + Math.Cos(angleRad) * distance;
                result.Lng = inputPoint.Lng + Math.Sin(angleRad) * distance;
            }
            else {
                result.Lat = inputPoint.Lat + Math.Cos(angle) * distance;
                result.Lng = inputPoint.Lng + Math.Sin(angle) * distance;
            }
            return result;
        }
        /*
         * @author:ljx
         * @descript:显示坐标跳转对话框
         * @input:
         * @return:
         * @tip:
         * 
         */
        private void button3_Click(object sender, EventArgs e)
        {
 

            positonForm.ShowDialog();
            
        }
    }
}
#endregion