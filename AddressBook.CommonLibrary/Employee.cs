using System.ComponentModel;

namespace AddressBook.CommonLibrary
{
    public record Employee : INotifyPropertyChanged
    {
        public string _name;
        public string _position;
        public string? _phone;
        public string _email;
        public string? _room;
        public string? _mainWorkplace;
        public string? _workplace;
        public event PropertyChangedEventHandler? PropertyChanged;

        public Employee(string name, string position, string email)
        {
            _name = name;
            _position = position;
            _email = email;
        }

        public void UpdateFrom(Employee other)
        {
            Name = other.Name;
            Position = other.Position;
            Phone = other.Phone;
            Email = other.Email;
            Room = other.Room;
            MainWorkplace = other.MainWorkplace;
            Workplace = other.Workplace;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Position
        {
            get => _position;
            set
            {
                _position = value;
                OnPropertyChanged(nameof(Position));
            }
        }

        public string? Phone
        {
            get => _phone;
            set
            {
                _phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public string? Room
        {
            get => _room;
            set
            {
                _room = value;
                OnPropertyChanged(nameof(Room));
            }
        }

        public string? MainWorkplace
        {
            get => _mainWorkplace;
            set
            {
                _mainWorkplace = value;
                OnPropertyChanged(nameof(MainWorkplace));
            }
        }

        public string? Workplace
        {
            get => _workplace;
            set
            {
                _workplace = value;
                OnPropertyChanged(nameof(Workplace));
            }
        }
    }
}
