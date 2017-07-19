using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using AndroidMemoryAnalyzer.HeapAnalyzer.HeapStructs;

namespace AndroidMemoryAnalyzer.HeapAnalyzer
{
    public class HeapFileAnalyzer
    {
        public string HeapFilePath { get; set; }
        public HeapFileAnalyzer(string path)
        {
            HeapFilePath = path;
        }

        #region Heap结构集合
        HeapHeader HeapHeader { get; set; }
        List<HeapString> HeapStrings { get; set; }
        List<HeapClass> HeapClasses { get; set; }
        List<HeapStack> HeapStacks { get; set; }
        List<HeapDumpSegment> HeapDumpSegments { get; set; }
        List<HeapDumpSegment> HeapObjectInstanceSegments { get; set; }
        List<HeapRawData> HeapRawDatas { get; set; }
        #endregion

        #region HeapObjectInstance集合
        List<HeapDumpObject> HeapDumpObjects { get; set; }
        List<DumpClassObject> DumpClassObjects { get; set; }
        List<DumpObjectArray> DumpObjectArrays { get; set; }
        List<DumpObjectInstance> DumpObjectInstances { get; set; }
        List<DumpPrimitiveArray> DumpPrimitiveArrays { get; set; }
        List<DumpPrimitiveArrayNoData> DumpPrimitiveArrayNoDatas { get; set; }
        #endregion

        #region 常用字典集合
        public Dictionary<int, string> StringDatas { get; set; }
        public Dictionary<int, string> ClassNames { get; set; }
        public Dictionary<int, ClassObjectInfo> ClassObjectInfos { get; set; }
        public Dictionary<int, object> ObjectInfoDictionary { get; set; }//包括Object实例、两种数组等
        #endregion

        #region 分析目标集合
        public List<ObjectInstanceInfo> ObjectInstanceInfos { get; set; }
        public List<ObjectInstanceInfo> ObjectInstanceWithDataInfos { get; set; }
        public List<ObjectArrayInfo> ObjectArrayInfos { get; set; }
        public List<PrimitiveArrayInfo> PrimitiveArrayInfos { get; set; }
        #endregion

        void initialize()
        {
            initHeapStruct();
            initDumpObject();
            initDictionary();
            initObjectInfo();
            initClear();
        }
        void initHeapStruct()
        {

            Stream st = new FileStream(HeapFilePath, FileMode.Open);

            BinaryReader br = new BinaryReader(st);

            HeapHeader = new HeapHeader().Deserialize(br);

            HeapStrings = new List<HeapString>();
            HeapClasses = new List<HeapClass>();
            HeapStacks = new List<HeapStack>();
            HeapDumpSegments = new List<HeapDumpSegment>();
            HeapRawDatas = new List<HeapRawData>();

            while (br.PeekChar() != -1)
            {
                var nextflag = (HeapTag)br.ReadByte();
                switch (nextflag)
                {
                    case HeapTag.STRING:
                        HeapStrings.Add(new HeapString().Deserialize(br));
                        break;
                    case HeapTag.LOAD_CLASS:
                        HeapClasses.Add(new HeapClass().Deserialize(br));
                        break;
                    case HeapTag.STACK_TRACE:
                        HeapStacks.Add(new HeapStack().Deserialize(br));
                        break;
                    case HeapTag.HEAP_DUMP_SEGMENT:
                        HeapDumpSegments.Add(new HeapDumpSegment().Deserialize(br));
                        break;
                    default:
                        HeapRawDatas.Add(new HeapRawData().Deserialize(br));
                        break;
                }
            }

            br.Close();
        }       
        void initDumpObject()
        {
            HeapObjectInstanceSegments = HeapDumpSegments.Where(c => c.SegmentType == DumpSegmentType.ObjectInstance).ToList();
            HeapDumpObjects = new List<HeapDumpObject>();

            foreach(var it in HeapObjectInstanceSegments)
            {
                HeapDumpObjects.AddRange(it.HeapDumpObjects);
            }
            DumpClassObjects = HeapDumpObjects.Where(c => c.Tag == DumpObjectTag.CLASS_OBJECT).Select(c=>c as DumpClassObject).ToList();
            DumpObjectArrays = HeapDumpObjects.Where(c => c.Tag == DumpObjectTag.OBJECT_ARRAY).Select(c => c as DumpObjectArray).ToList();
            DumpObjectInstances = HeapDumpObjects.Where(c => c.Tag == DumpObjectTag.OBJECT_INSTANCE).Select(c => c as DumpObjectInstance).ToList();
            DumpPrimitiveArrays = HeapDumpObjects.Where(c => c.Tag == DumpObjectTag.PRIMITIVE_ARRAY_WITH_DATA).Select(c => c as DumpPrimitiveArray).ToList();
            DumpPrimitiveArrayNoDatas = HeapDumpObjects.Where(c => c.Tag == DumpObjectTag.PRIMITIVE_ARRAY_WITHOUT_DATA).Select(c => c as DumpPrimitiveArrayNoData).ToList();

           //var s= DumpClassObjects.Where(i => i.StaticFieldContent.Count_Plused != 0).ToList();
        }
        void initDictionary()
        {
            StringDatas = HeapStrings.ToDictionary(k => k.StringID, v => v.StringData);
            ClassNames = HeapClasses.ToDictionary(k => k.ObjectID, v => StringDatas[v.NameID]);

            ClassObjectInfos = DumpClassObjects.ToDictionary(k => k.ClassObjectID, v => new ClassObjectInfo(v, this));
        }
        void initObjectInfo()
        {
            PrimitiveArrayInfos = DumpPrimitiveArrays.Select(c => new PrimitiveArrayInfo(c)).ToList();
            ObjectArrayInfos = DumpObjectArrays.Select(c => new ObjectArrayInfo(c, this)).ToList();

            ObjectInstanceInfos = DumpObjectInstances
                .Select(c => new ObjectInstanceInfo(c, this)).ToList();
            ObjectInstanceWithDataInfos = ObjectInstanceInfos.Where(c => c.InstanceFields != null && c.InstanceFields.Count > 0).ToList();

            ObjectInfoDictionary = ObjectInstanceInfos.ToDictionary(k => k.ObjectID, v => (object)v);
            foreach (var it in PrimitiveArrayInfos)
            {
                ObjectInfoDictionary.Add(it.PrimitiveArrayID, (object)it);
            }
            foreach (var it in ObjectArrayInfos)
            {
                ObjectInfoDictionary.Add(it.ObjectArrayID, (object)it);
            }

            Parallel.ForEach(ObjectInstanceWithDataInfos,item=>
                {
                    foreach(var it in item.InstanceFields)
                    {
                        if(it.Type==PrimitiveType.HPROF_BASIC_OBJECT)
                        {
                            if(ObjectInfoDictionary.ContainsKey((int)it.Value))
                            {
                                (it as ReferenceObjectInfo).ReferenceTarget= ObjectInfoDictionary[(int)it.Value];
                            }
                        }
                    }
                });
            Parallel.ForEach(ObjectArrayInfos, item =>
            {
                item.Elements = item.ElementIDs.Select(c => ObjectInfoDictionary.ContainsKey(c)
                ? ObjectInfoDictionary[c] : null).ToList();  
            });

        }
        void initClear()
        {
            HeapStrings.Clear();
            HeapClasses.Clear();
            HeapStacks.Clear();
            HeapDumpSegments.Clear();
            HeapRawDatas.Clear();

            DumpClassObjects.Clear();
            DumpObjectArrays.Clear();
            DumpObjectInstances.Clear();
            DumpPrimitiveArrays.Clear();
            DumpPrimitiveArrayNoDatas.Clear(); 
        }
        void lookForSomething_Demo()
        {
            //获取 【类名】 中含有 "LoginActivity" 的 【实例对象】
            var objectInstance = ObjectInstanceInfos.Where(c => c.ClassName.Contains("LoginActivity")).First();

            //获取 【实例对象】 中含有 名为"className" 的 【属性】
            var objectProperty = objectInstance.InstanceFields.Where(c => c.Name == "className").FirstOrDefault();

            //该属性是字符串，内存放一个指针，获取该指针指向的 【实例对象】
            var StringObject = (ObjectInstanceInfo)(objectProperty as ReferenceObjectInfo).ReferenceTarget;

            //该【实例对象】是一个String类型对象，只存放了一个指针并指向char型数组，获取该【数组实例】
            var StringRef = (StringObject.InstanceFields[0] as ReferenceObjectInfo).ReferenceTarget;

            //获取该【char数组】存储的字符串值
            var StringData = (StringRef as PrimitiveArrayInfo).StringData;

        }
        public void DoWork()
        {
            initialize();
            //lookForSomething_Demo();
        }


    }
}
