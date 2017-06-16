using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CellarAutomatForm.Presenter
{
    public interface IViewMainForm
    {
        void PlayRecord();

        void StopRecord();

        void RewindRecord(short frame);

        void LoadRecord(string fileName);

        void SaveRecord(string fileName);
    }
}
