using System.Linq;
using UnityEngine;

namespace FairyGUI
{
    public partial class GoWrapper
    {
        public bool CloneMaterial
        {
            get { return _cloneMaterial; }
            set
            {
                _cloneMaterial = value;
                this.CacheRenderers();
            }
        }
        
        public override Material material
        {
            get
            {
                if (_materialsBackup.Count != 0)
                {
                    return _materialsBackup.First().Value;
                }

                return base.material;
            }
            set { base.material = value; }
        }
    }
}