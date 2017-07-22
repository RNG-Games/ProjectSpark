using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using _ProjectSpark.actors;

namespace _ProjectSpark.util
{
    public class Memento<T> where T : IActable
    {
        private readonly T state;

        public Memento(T obj)
        {
            state = Clone(obj);
        }

        public T GetSavedState()
        {
            return state;
        }

        private static T Clone(T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;
                return (T)formatter.Deserialize(ms);
            }
        }
    }
}
