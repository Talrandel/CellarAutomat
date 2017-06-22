using System;

using CellularAutomaton.Components.Player;
using CellularAutomaton.Core.Rules;
using CellularAutomaton.Core;

namespace CellarAutomatForm.Presenter
{
    /// <summary>
    /// Интерфейс для представления КА
    /// </summary>
    public class PresenterMainForm
    {
        private IViewMainForm _view;
        private IPlayer _player;
        private CellularAutomaton.Core.CellularAutomaton _automat;
        private Field _field;
        private Record _record;

        public void PlayRecord()
        {
            _player.Play();
            _view.PlayRecord();
        }

        public void StopRecord()
        {
            _player.Stop();
            _view.StopRecord();
        }

        public void RewindRecord(short frame)
        {
            _player.Rewind(frame);
            _view.RewindRecord();
        }

        public void LoadRecord(string fileName)
        {
            _record.Load(fileName);
            _view.LoadRecord();
        }

        public void SaveRecord(string fileName)
        {
            _record.Save(fileName);
            _view.SaveRecord();
        }

        void GetRecordParameters(IRule rule, int fieldWidth, int fieldHeight, int statesCount, byte dencity)
        {
            _automat = new CellularAutomaton.Core.CellularAutomaton(rule, fieldWidth, fieldHeight, statesCount);
            _automat.SetDensityForField(dencity);
            _view.GetRecordParameters();
        }

        public bool CheckCAParameters()
        {
#warning NotImplementedException CheckCAParameters
            throw new NotImplementedException(nameof(CheckCAParameters));
            return true;
        }

        public PresenterMainForm(IViewMainForm view)
        {
#warning Player initialization
            _view = view;
        }
    }
}