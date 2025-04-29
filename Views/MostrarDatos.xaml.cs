namespace mchicaizaTS3.Views;

public partial class MostrarDatos : ContentPage
{
	public MostrarDatos(string tiposeleccionados, string identificacion , string nombres, string apellidos, decimal aportes)
	{
		InitializeComponent();

		lblTipoIdentificacion.Text = tiposeleccionados;
		lblNumeroIdentificacion.Text = identificacion;
		lblnombres.Text = nombres;
		lblapellidos.Text = apellidos;
		lbLaporteiess.Text = aportes.ToString("F2");
	}

    private async void btnexportar_Clicked(object sender, EventArgs e)
    {
		try
		{
			string contenido = $"Tipo de identificacion: {lblTipoIdentificacion.Text}\n" +
							   $"Numero de identificacion: {lblNumeroIdentificacion.Text}\n" +
							   $"Fecha de exportacion: {DateTime.Now:dd/MM/yyyy HH:mm:ss}";
			string filename = "Reporte.txt";

			string rutaCadena = FileSystem.AppDataDirectory;
			string rutaCompleta = Path.Combine(rutaCadena, filename);

			File.WriteAllText(rutaCompleta, contenido);

			await DisplayAlert("Exito", $"Reporte guardado en:\n {rutaCompleta}", "OK");

		}
		catch (Exception ex)
		{
			await DisplayAlert("Error", $"No se pudo exportar el reporte.\n Error:{ex}", "OK");
		}
    }
}