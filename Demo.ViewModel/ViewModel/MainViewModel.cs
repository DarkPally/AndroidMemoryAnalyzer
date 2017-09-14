﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Library.ViewModel
{
    public class MainViewModel
    {
        public QQViewModel QQViewModel { get; set; }
        public MicroMsgViewModel MicroMsgViewModel { get; set; }
        public Mail163ViewModel Mail163ViewModel { get; set; }
        public Mail189ViewModel Mail189ViewModel { get; set; }
        public ECloudViewModel ECloudViewModel { get; set; }
        public Browser360ViewModel Browser360ViewModel { get; set; }
        public MicroMsgTradeViewModel MicroMsgTradeViewModel { get; set; }

        public DolphinViewModel DolphinViewModel { get; set; }
        public BrowserSogouViewModel BrowserSogouViewModel { get; set; }
        public FetionViewModel FetionViewModel { get; set; }
        public LaiWangViewModel LaiWangViewModel { get; set; }
        public MomoViewModel MomoViewModel { get; set; }
        public OuPengViewModel OuPengViewModel { get; set; }
        public OutLookViewModel OutLookViewModel { get; set; }
        public WhatsAppViewModel WhatsAppViewModel { get; set; }
        public YixinViewModel YixinViewModel { get; set; }
        public YYTalkViewModel YYTalkViewModel { get; set; }

        private static readonly MainViewModel instance = new MainViewModel();
        public static MainViewModel GetInstance() { return instance; }
        private MainViewModel()
        {
            MicroMsgViewModel = new MicroMsgViewModel();
            QQViewModel = new QQViewModel();
            Mail163ViewModel = new Mail163ViewModel();
            Mail189ViewModel = new Mail189ViewModel();
            ECloudViewModel = new ECloudViewModel();
            Browser360ViewModel = new Browser360ViewModel();
            MicroMsgTradeViewModel = new MicroMsgTradeViewModel();

            DolphinViewModel = new DolphinViewModel();
            BrowserSogouViewModel = new BrowserSogouViewModel();
            FetionViewModel = new FetionViewModel();
            LaiWangViewModel = new LaiWangViewModel();
            MomoViewModel = new MomoViewModel();
            OuPengViewModel = new OuPengViewModel();
            OutLookViewModel = new OutLookViewModel();
            WhatsAppViewModel = new WhatsAppViewModel();
            YixinViewModel = new YixinViewModel();
            YYTalkViewModel = new YYTalkViewModel();
        }

        public static void InitializeWithParams(string type,string path)
        {
            switch (type)
            {
                case "com.tencent.mobileqq":                    
                    instance.QQViewModel.FilePath = path;
                    instance.QQViewModel.ExcuteAnalyzeFile();
                    instance.startType = type;
                    break;
                case "com.tencent.mm":
                    instance.MicroMsgViewModel.FilePath = path;
                    instance.MicroMsgViewModel.ExcuteAnalyzeFile();
                    instance.startType = type;
                    break;
                case "com.netease.mobimail":
                    instance.Mail163ViewModel.FilePath = path;
                    instance.Mail163ViewModel.ExcuteAnalyzeFile();
                    instance.startType = type;
                    break;
                case "com.corp21cn.mail189":
                    instance.Mail189ViewModel.FilePath = path;
                    instance.Mail189ViewModel.ExcuteAnalyzeFile();
                    instance.startType = type;
                    break;
                case "com.cn21.ecloud":
                    instance.ECloudViewModel.FilePath = path;
                    instance.ECloudViewModel.ExcuteAnalyzeFile();
                    instance.startType = type;
                    break;
                case "com.qihoo.browser":
                    instance.Browser360ViewModel.FilePath = path;
                    instance.Browser360ViewModel.ExcuteAnalyzeFile();
                    instance.startType = type;
                    break;
                case "com.tencent.mmtools":
                    instance.MicroMsgTradeViewModel.FilePath = path;
                    instance.MicroMsgTradeViewModel.ExcuteAnalyzeFile();
                    instance.startType = type;
                    break;
                case "com.dolphin.browser.xf":
                    instance.DolphinViewModel.FilePath = path;
                    instance.DolphinViewModel.ExcuteAnalyzeFile();
                    instance.startType = type;
                    break;
                case "sogou.mobile.explorer":
                    instance.BrowserSogouViewModel.FilePath = path;
                    instance.BrowserSogouViewModel.ExcuteAnalyzeFile();
                    instance.startType = type;
                    break;
                case "cn.com.fetion":
                    instance.FetionViewModel.FilePath = path;
                    instance.FetionViewModel.ExcuteAnalyzeFile();
                    instance.startType = type;
                    break;
                case "com.alibaba.android.babylon":
                    instance.LaiWangViewModel.FilePath = path;
                    instance.LaiWangViewModel.ExcuteAnalyzeFile();
                    instance.startType = type;
                    break;
                case "com.immomo.momo":
                    instance.MomoViewModel.FilePath = path;
                    instance.MomoViewModel.ExcuteAnalyzeFile();
                    instance.startType = type;
                    break;
                case "com.oupeng.mini.android":
                    instance.OuPengViewModel.FilePath = path;
                    instance.OuPengViewModel.ExcuteAnalyzeFile();
                    instance.startType = type;
                    break;
                case "com.microsoft.office.outlook":
                    instance.OutLookViewModel.FilePath = path;
                    instance.OutLookViewModel.ExcuteAnalyzeFile();
                    instance.startType = type;
                    break;
                case "com.whatsapp":
                    instance.WhatsAppViewModel.FilePath = path;
                    instance.WhatsAppViewModel.ExcuteAnalyzeFile();
                    instance.startType = type;
                    break;
                case "im.yixin":
                    instance.YixinViewModel.FilePath = path;
                    instance.YixinViewModel.ExcuteAnalyzeFile();
                    instance.startType = type;
                    break;
                case "com.duowan.mobile":
                    instance.YYTalkViewModel.FilePath = path;
                    instance.YYTalkViewModel.ExcuteAnalyzeFile();
                    instance.startType = type;
                    break; 
                default:
                    break;
            }
        }

        public string startType=null;
    }
}
