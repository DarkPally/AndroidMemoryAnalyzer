using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AndroidMemoryAnalyzer.HeapAnalyzer;
using AndroidMemoryAnalyzer.HeapQuerier;

using System.IO;

using System.Xml.Serialization;

namespace AndroidMemoryAnalyzer
{
    public class AnalysisManager
    {
        HeapFileAnalyzer HeapAnalyzer;
        public string HeapFilePath { get; set; }
        public AnalysisManager(string heapFilePath,string QuerierFilePath)
        {
            HeapFilePath = heapFilePath;
        }
        public void Initialize()
        {
            if(HeapAnalyzer==null)
            {
                HeapAnalyzer = new HeapFileAnalyzer(HeapFilePath);
                HeapAnalyzer.DoWork();
            }
        }
        public SearchRootItem GetSearchRoot()
        {
            var RootSearchItem = new SearchRootItem()
            {
                SearchParam = new SearchParam()
                {
                    SourceType = SearchSourceType.Object.GetDescription(),
                    Filters = new List<SearchFilter>()
                   {
                       new SearchFilter()
                       {
                           Name="ClassName",
                           Operator=OperotorType.Contains.GetDescription(),
                           Value="LoginActivity"
                       }
                   },
                    TargetFormats = new List<SearchTargetFormat>()
                   {
                       new SearchTargetFormat()
                       {
                           Name="className",
                           ValueType=TargetFormatValueType.String.GetDescription(),
                           IsOutput=true,
                       }
                   },
                    Limit = 10,

                },
            };
            return RootSearchItem;
        }

        public IEnumerable<T> queryByFilter<T>(IEnumerable<T> source, SearchFilter filter)
        {
            var op = (OperotorType)EnumHelper.GetEnum<OperotorType>(filter.Operator);
            switch (op)
            {
                case OperotorType.Equal:
                    return source.
                Where(c => (c.GetType().GetProperty(filter.Name).GetValue(c, null).ToString()) == filter.Value);
                case OperotorType.GreaterEqual:
                    break;
                case OperotorType.LessEqual:
                    break;
                case OperotorType.Greater:
                    break;
                case OperotorType.Less:
                    break;
                case OperotorType.Startswith:
                    break;
                case OperotorType.Contains:
                    return source.
                Where(c => (c.GetType().GetProperty(filter.Name).GetValue(c, null).ToString()).Contains(filter.Value));
                default:
                    break;
            }
            throw new Exception("未实现");
            
        }
        public List<SearchResult> queryBySearchParam<T>(IEnumerable<T> source, SearchParam sp)
        {
            var temp=source;
            foreach(var f in sp.Filters)
            {
                temp=queryByFilter(temp, f);
            }


            foreach (var it in temp)
            {
                bool error_flag=false;
                var prop_list=new List<System.Reflection.PropertyInfo>();
                foreach(var tf in sp.TargetFormats)
                {
                   var prop=it.GetType().GetProperty(tf.Name);
                   if(prop==null)
                   {
                       error_flag=true;
                       break;
                   }
                   else
                   {
                       prop_list.Add(prop);
                   }
                }
                if(error_flag) continue;

                /*
                it.Items = new List<SearchResultItem>();
                foreach(var tf in sp.TargetFormats)
                {
                   var it.GetType().GetProperty(tf.Name)
                }*/
            }

            var tempResult= temp.Take(sp.Limit).Select(c=>new SearchResult()
                {
                    Host=c
                }).ToList();
            foreach (var it in tempResult)
            {
                it.Items = new List<SearchResultItem>();
                foreach(var tf in sp.TargetFormats)
                {
                   //var it.GetType().GetProperty(tf.Name)
                }
            }
            return tempResult;
        }
        public void DoWork()
        {
            Initialize();
            
            var sr = GetSearchRoot();
            SearchSourceType sourceType = (SearchSourceType)EnumHelper.GetEnum<SearchSourceType>(sr.SearchParam.SourceType);
            
            switch (sourceType)
            {
                case SearchSourceType.Class:
                    var list=HeapAnalyzer.ClassObjectInfos.Select(c=>c.Value);

                    break;
                case SearchSourceType.Object:
                    break;
                case SearchSourceType.Parent:
                    break;
                default:
                    break;
            }
        }
    }
}
