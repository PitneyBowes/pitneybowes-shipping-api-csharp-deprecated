using System.Text;
using System;

namespace PitneyBowes.Developer.Drawing
{
    static public class Binding
    {

        private enum BoundObjectRefType
        {
            None,
            Obj,
            Dict,
            Enum
        }
        static private Tuple<BoundObjectRefType, int, string, int> getNextReference(string s, int i)
        {
            if (s == null) return null;
            if (i == s.Length) return null;
            var sb = new StringBuilder();
            int index = 0;
            BoundObjectRefType refType = BoundObjectRefType.None;
            for (; i < s.Length; i++)
            {
                switch (s[i])
                {
                    case '.':
                        if (refType != BoundObjectRefType.None) return new Tuple<BoundObjectRefType, int, string, int>(refType, index, sb.ToString(), i);
                        refType = BoundObjectRefType.Obj;
                        break;
                    case '#':
                        if (refType != BoundObjectRefType.None) return new Tuple<BoundObjectRefType, int, string, int>(refType, index, sb.ToString(), i);
                        refType = BoundObjectRefType.Dict;
                        break;
                    case '[':
                        if (refType != BoundObjectRefType.None) return new Tuple<BoundObjectRefType, int, string, int>(refType, index, sb.ToString(), i);
                        refType = BoundObjectRefType.Dict;
                        i++;
                        while (s[i] != ']')
                        {
                            index = index * 10 + (int)char.GetNumericValue(s[i++]);
                        }
                        break;
                    default:
                        sb.Append(s[i]);
                        break;
                }
            }
            return new Tuple<BoundObjectRefType, int, string, int>(refType, index, sb.ToString(), i);
        }

        static public object BoundObject(object o, string path)
        { 
            if (o == null ) return o;
            int i = 0;
            for (var t = getNextReference(path, i); t != null;t = getNextReference(path, i))
            {
                switch(t.Item1)
                {
                    case BoundObjectRefType.Dict:
                        o = ((dynamic)o)[t.Item3];
                        break;
                    case BoundObjectRefType.Enum:
                        dynamic iter = ((dynamic)o).GetEnumerator();
                        int counter = 0;
                        while(iter = iter.MoveNext() && counter < t.Item2 )
                        {
                            counter++;
                        }
                        o = iter.Current;
                        break;
                    default:
                    case BoundObjectRefType.Obj:
                        o = o.GetType().GetProperty(t.Item3);
                        break;
                }
                i = t.Item4;
            }
            return o;
        }

    }
}
