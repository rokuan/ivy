using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ivy.Activity
{
    public sealed class Bundle
    {
        private Dictionary<string, int> intMap = new Dictionary<string, int>();
        private Dictionary<string, bool> boolMap = new Dictionary<string, bool>();
        private Dictionary<string, double> doubleMap = new Dictionary<string, double>();
        private Dictionary<string, string> stringMap = new Dictionary<string, string>();
        private Dictionary<string, object> objMap = new Dictionary<string, object>();

        public Bundle()
        {

        }

        public void putInt(string name, int i)
        {
            intMap[name] = i;
        }

        public void putBoolean(string name, bool b)
        {
            boolMap[name] = b;
        }

        public void putDouble(string name, double d)
        {
            doubleMap[name] = d;
        }

        public void putString(string name, string s)
        {
            stringMap[name] = s;
        }

        public void putObject(string name, object o)
        {
            objMap[name] = o;
        }

        public int getInt(string name)
        {
            return intMap[name];
        }

        public bool getBool(string name)
        {
            return boolMap[name];
        }

        public double getDouble(string name)
        {
            return doubleMap[name];
        }

        public string getString(string name)
        {
            return stringMap[name];
        }

        public object getObject(string name)
        {
            return objMap[name];
        }
    }
}
