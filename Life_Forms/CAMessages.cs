namespace CellarAutomatForm
{
    /// <summary>
    /// Сообщения КА
    /// </summary>
    public static class CAMessages
    {
        public const string Saved = "КА сохранен";
        public const string Loading = "КА загружается";
        public const string Loaded = "КА загружен";
        public const string NotLoaded = "КА не загружен!";
        public const string LoadError = "Ошибка загрузки КА из файла\nЗагрузите КА повторно";
        public const string Build = "КА строится";
        public const string Building = "КА строится, невозможно воспроизвести другой КА";
        public const string Built = "КА построен";
        public const string StopBuilding = "Построение КА было прервано";
        public const string Play = "КА воспроизводится";
        public const string PlayingEnded = "Воспроизведение КА завершено";
        public const string StopPlaying = "Воспроизведение КА было остановлено";
        public const string Hint = "Установите значения для КА и выберите правило, затем нажмите Построить.\nДля досрочного прерывания построения КА нажмите Остановить построение\nЗатем загрузите построенный КА и нажмите Воспроизвести.";
    }
}