using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace gMapeTest1
{
    class excelDeal
    {
        //初始化界面的表数据，目标信息
        private DataTable targetInfo;
        //详细日志表数据
        private DataTable targetLog;
        //标会库表数据
        private DataTable Reposity;
        //设备行航迹航姿数据
        private DataTable navData;
        //结果数据
        private DataTable resultData;



        public DataTable TargetInfo { get => targetInfo; set => targetInfo = value; }
        public DataTable TargetLog { get => targetLog; set => targetLog = value; }
        public DataTable Reposity1 { get => Reposity; set => Reposity = value; }
        public DataTable NavData { get => navData; set => navData = value; }
        public DataTable ResultData { get => resultData; set => resultData = value; }

        //初始化表数据
        public void readData(String path) {
            
        }
        //存储表数据
        public void saveData(String Path) {
        }
        public void contonlTargetView(int col,String value) {

        }
    }
}
