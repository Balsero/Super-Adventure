using System.ComponentModel;
namespace SuperAdventure.Models
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class QuestStatus : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Gets the player quest.
        /// </summary>
        /// <value>
        /// The player quest.
        /// </value>
        public Quest PlayerQuest { get; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is completed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is completed; otherwise, <c>false</c>.
        /// </value>
        public bool IsCompleted { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="QuestStatus"/> class.
        /// </summary>
        /// <param name="quest">The quest.</param>
        public QuestStatus(Quest quest)
        {
            PlayerQuest = quest;
            IsCompleted = false;
        }
    }
}
