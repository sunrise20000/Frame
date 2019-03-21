using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frame.Interface
{
    public interface IManagable<T> where T : class
    {
        Dictionary<string, T> Dic { get; set; }
        void AddInstanse(T ins);
        T FindInstanseByName(string strName);
        T FindInstanseByIndex(int Index);

    }
}
