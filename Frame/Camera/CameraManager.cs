using Frame.Class;
using Frame.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frame.Camera
{
    public class CameraManager : Singleton<CameraManager>, IManagable<CameraBase>
    {
        public Dictionary<string, CameraBase> Dic { get; set; } = new Dictionary<string, CameraBase>();

        public void AddInstanse(CameraBase ins)
        {
            if(!Dic.Keys.Contains(ins.m_Cameraname))
                Dic.Add(ins.m_Cameraname,ins);
        }

        public CameraBase FindInstanseByIndex(int Index)
        {
            if (Dic.Count > Index)
                return Dic.Values.ElementAt(Index);
            return null;
        }

        public CameraBase FindInstanseByName(string strName)
        {
            if (Dic.Keys.Contains(strName))
                return Dic[strName];
            return null;
        }
    }
}
