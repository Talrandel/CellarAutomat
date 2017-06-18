namespace CellarAutomatForm.Presenter
{
    public interface IViewMainForm
    {
        void PlayRecord();

        void StopRecord();

        void RewindRecord();

        void LoadRecord();

        void SaveRecord();

        void GetRecordParameters();
    }
}