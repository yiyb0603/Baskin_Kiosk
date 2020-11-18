using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace Baskin_Kiosk.ViewModel
{
    public class MessageViewModel : BindableBase
    {
        private ObservableCollection<string> _messageList = new ObservableCollection<string>();

        public ObservableCollection<string> messageList
        {
            get => _messageList;
            set => SetProperty(ref _messageList, value);
        }

        public void setMessageList(string message)
        {
            this._messageList.Add(message);
        }

        public ObservableCollection<string> getMessageList()
        {
            return this.messageList;
        }
    }
}
