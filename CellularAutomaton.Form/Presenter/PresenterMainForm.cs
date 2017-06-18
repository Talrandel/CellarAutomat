using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CellularAutomaton.Core.Rules;
using CellularAutomaton.Core.Helpers.Player;
using CellularAutomaton.Core.Helpers.Recorder;

namespace CellarAutomatForm.Presenter
{
    /// <summary>
    /// Интерфейс для представления КА
    /// </summary>
    public class PresenterMainForm
    {
        private IViewMainForm _view;
        private IPlayer _player;

        public void PlayRecord()
        {
            _player.Play();
            _view.PlayRecord();
        }

        public void StopRecord()
        { }

        public void RewindRecord(short frame)
        { }

        public void LoadRecord(string fileName)
        { }

        public void SaveRecord(string fileName)
        { }

        void GetRecordParameters(IRule rule, int fieldWidth, int fieldHeight, int statesCount, int dencity)
        { }

        public bool CheckCAParameters()
        {
            return true;
        }

        public PresenterMainForm(IViewMainForm view)
        {
            _view = view;
        }
    }
}