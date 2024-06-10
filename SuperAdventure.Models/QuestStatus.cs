using System.ComponentModel;
namespace SuperAdventure.Models
{
    public class QuestStatus : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Quest PlayerQuest { get; }
        public bool IsCompleted { get; set; }
        public QuestStatus(Quest quest)
        {
            PlayerQuest = quest;
            IsCompleted = false;
        }
    }
}
