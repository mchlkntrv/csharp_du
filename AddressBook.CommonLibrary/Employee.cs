using System.ComponentModel;

namespace AddressBook.CommonLibrary
{
    public record Employee : INotifyPropertyChanged
    {
        public required string _name;
        public required string _position;
        public string? _phone;
        public required string _email;
        public string? _room;
        public string? _mainWorkplace;
        public string? _workplace;
        public event PropertyChangedEventHandler? PropertyChanged;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            }
        }

        public string Position
        {
            get => _position;
            set
            {
                _position = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Position)));
            }
        }

        public string? Phone
        {
            get => _phone;
            set
            {
                _phone = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Phone)));
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Email)));
            }
        }

        public string? Room
        {
            get => _room;
            set
            {
                _room = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Room)));
            }
        }

        public string? MainWorkplace
        {
            get => _mainWorkplace;
            set
            {
                _mainWorkplace = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MainWorkplace)));
            }
        }

        public string? Workplace
        {
            get => _workplace;
            set
            {
                _workplace = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Workplace)));
            }
        }
    }
}
