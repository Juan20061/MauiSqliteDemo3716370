namespace MauiSqliteDemo3716370
{
    public partial class MainPage : ContentPage
    {
        private readonly LocalDbService _dbService;
        private int _editClientesId;

        public MainPage(LocalDbService dbService)
        {
            InitializeComponent();
            _dbService = dbService;
            Task.Run(async () => listview.ItemsSource = await _dbService.GetClientes());
        }

        private async void saveButton_Clicked(object sender, EventArgs e)
        {
            if (_editClientesId==0)
            {
                //agrega cliente
                await _dbService.Create(new Cliente
                {
                    NombreCliente = nombreEntryField.Text,
                    Email = emailEntryField.Text,
                    Movil = movilEntryField.Text

                });

            }
            else
            {
                //edita cliente
                await _dbService.Update(new Cliente
                {
                    id=_editClientesId,
                    NombreCliente=nombreEntryField.Text,
                    Email=emailEntryField.Text,
                    Movil=movilEntryField.Text
                });
                _editClientesId = 0;
            }
            nombreEntryField.Text = string.Empty;
            emailEntryField.Text = string.Empty;
            movilEntryField.Text = string.Empty;

            listview.ItemsSource = await _dbService.GetClientes();
        }

        private async void listview_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var clientes = (Cliente)e.Item;
            var action = await DisplayActionSheet("Action", "Cancel", null, "Edit", "Delete");

            switch (action)
            {
                case "Edit":
                    _editClientesId = clientes.id;
                    nombreEntryField.Text = clientes.NombreCliente;
                    emailEntryField.Text = clientes.Email;
                    movilEntryField.Text = clientes.Movil;
                    break;

                case "Delete":
                    await _dbService.Delete(clientes);
                    listview.ItemsSource = await _dbService.GetClientes();
                    break;
                    
            }
        }
    }

}
