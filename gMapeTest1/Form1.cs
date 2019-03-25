using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
            //this.gMapControl1.MapProvider = GMapProviders.GoogleChinaMap; // 设置地图源
            //GMaps.Instance.Mode = AccessMode.ServerAndCache; // GMap工作模式
            //this.gMapControl1.SetPositionByKeywords("北京"); // 地图中心位置(写经纬度好用，名字不好用)
            //this.gMapControl1.DragButton = MouseButtons.Left;

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
            String mapPath = Application.StartupPath + "\\GoogleChainMap.gmdb";
            GMap.NET.GMaps.Instance.ImportFromGMDB(mapPath);
            this.gMapControl1.MinZoom = 1;   //最小比例
            this.gMapControl1.MaxZoom = 9; //最大比例
            this.gMapControl1.Zoom = 6; //当前比例
            this.gMapControl1.ShowCenter = false; //不显示中心十字点
            this.gMapControl1.DragButton = System.Windows.Forms.MouseButtons.Left;//左键拖拽地图
            this.gMapControl1.Position = new PointLatLng(31, 104);
            #endregion

        }

        public void dataBind()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("时"));
            dt.Columns.Add(new DataColumn("分"));
            dt.Columns.Add(new DataColumn("秒"));
            dt.Columns.Add(new DataColumn("RF"));
            dt.Columns.Add(new DataColumn("PRI"));
            dt.Columns.Add(new DataColumn("PW"));
            DataRow dr;
            for (int i = 0; i < 10; i++)
            {
                dr = dt.NewRow();
                dr["时"] = "11";
                dr["分"] = "12";
                dr["秒"] = "13";
                dr["RF"] = "5986";
                dr["PRI"] = "1";
                dr["PW"] = "1";
                dt.Rows.Add(dr);
            }

            this.dataGridView1.DataSource = dt;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.dataBind();
            //2. 调用类的初始化方法，记录窗体和其控件的初始位置和大小
            asc.controllInitializeSize(this);
            //标记
            RadaMarker();
        }

        //3.为窗体添加SizeChanged事件，并在其方法Form1_SizeChanged中，调用类的自适应方法，完成自适应  
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            asc.controlAutoSize(this);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        //radar标注，添加图层
        public void RadaMarker()
        {
            GMapOverlay A =new GMapOverlay("A");
            GMapOverlay B = new GMapOverlay("B");
            string path = "F:\\VsWorkspace\\gMapeTest1\\gMapeTest1\\Image\\radar1.jpg";
            addMarker(path, A, new PointLatLng(30, 104));
            addMarker(path, A, new PointLatLng(31, 105));
        }
        //图层添加方法
        private void addMarker(string RadarPath, GMapOverlay markBrand, PointLatLng point)

        {
            Bitmap bitmap = null;
            bitmap = Bitmap.FromFile(RadarPath) as Bitmap;
            GMapMarker marker = new GMarkerGoogle(point, bitmap);
            markBrand.Markers.Add(marker);
            this.gMapControl1.Overlays.Add(markBrand);
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
