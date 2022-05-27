using System;
using System.Collections.Generic;


namespace VisitCounter
{
    public class TagsOfOutTag
    {
        public Dictionary<String, List<String>> Tags {get; set;}
        public bool DoesNotContainTagsFromLists { get; private set; }
        public bool ContainsTagsFromLists { get; private set; }
        public String ContainsTagsPrefix { get; private set; }
        public String DoesNotContainTagsPrefix { get; private set; }
        public TagsOfOutTag(Dictionary<String, List<String>> tags, 
            bool containsTagsFromLists, bool doesNotContainTagsFromLists,
            String containsTagsPrefix, String doesNotContainTagsPrefix)
        {
            this.Tags = tags;
            this.ContainsTagsFromLists = containsTagsFromLists;
            this.DoesNotContainTagsFromLists = doesNotContainTagsFromLists;
            this.ContainsTagsPrefix = containsTagsPrefix;
            this.DoesNotContainTagsPrefix = doesNotContainTagsPrefix;
        }
    }

    public delegate double DoubleDelegate(Dictionary<String,double> list);
    public enum CustomizerType
    {
        TargetDestination = 0,//SaddleBrown
        Passage = 1,//Blue
        Building = 2, //Navy
        Barrier = 3, //Black
        Water = 4,//DodgerBlue
        StrongTransportWay = 5,//Red
        WeakTransportWay = 6,//Fuchsia
        WalkWay = 7,//BlueViole
        Walkable = 8,//Green
        None = 9, //DarkKhaki

        LeisurePoint,//Green
        WorkPoint,//Red
        HomePoint,//Blue
        FoodPoint,//OrangeRed

        HumanPoint//BlueViole
    }
}
