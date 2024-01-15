using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Common.DTOs;
using DataAccess.Services;
using WPFFleraVyer.Models;

namespace WPFFleraVyer.Views
{
    /// <summary>
    /// Interaction logic for PeopleView.xaml
    /// </summary>
    public partial class PeopleView : UserControl
    {
        private readonly PeopleRepository _repo;

        public ObservableCollection<PersonModel> PeopleList { get; set; } = new();

        public PersonModel? SelectedPerson { get; set; } = new();

        public string EditFirstName { get; set; } = string.Empty;

        public string EditLastName { get; set; } = string.Empty;

        public PeopleView()
        {
            InitializeComponent();

            DataContext = this;

            _repo = new PeopleRepository();

            var allAllPeople = _repo.GetAllPeople();

            foreach (var person in allAllPeople)
            {
                PeopleList.Add(
                    new PersonModel()
                    {
                        Id = person.Id,
                        FirstName = person.FirstName,
                        LastName = person.LastName
                    });
            }
        }

        private void UpdateBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (SelectedPerson is null)
            {
                return;
            }

            SelectedPerson.FirstName = EditFirstName;
            SelectedPerson.LastName = EditLastName;

            _repo.UpdateLastNameForPerson(SelectedPerson.Id, EditLastName);
        }

        private void AddPersonBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var newPerson = new PersonModel();
            newPerson.FirstName = EditFirstName;
            newPerson.LastName = EditLastName;
            var personRecord = new PersonRecord("", EditFirstName, EditLastName);
            PeopleList.Add(newPerson);

            _repo.AddPerson(personRecord);
        }

        private void RemovePersonBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (SelectedPerson is null)
            {
                return;
            }

            _repo.DeletePerson(SelectedPerson.Id);
            PeopleList.Remove(SelectedPerson);
        }
    }
}
