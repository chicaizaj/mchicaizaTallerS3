using System.IO.Pipes;
using System.Threading.Tasks;

namespace mchicaizaTS3.Views;

public partial class Principal : ContentPage
{

	public Principal()
	{
		InitializeComponent();

        txtidentificacion.TextChanged += txtidentificacion_TextChanged;
        pickerIdentificacion.SelectedIndexChanged += pickerIdentificacion_SelectedIndexChanged;
	}

    private void pickerIdentificacion_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (pickerIdentificacion.SelectedItem == null)
            return;
        string tiposeleccionado = pickerIdentificacion.SelectedItem as string;
        txtidentificacion.Text = string.Empty;

        if (tiposeleccionado == "Cédula")
            txtidentificacion.Placeholder = "Ingrese su Cédula (10 dígitos)";
        else if (tiposeleccionado == "RUC")
            txtidentificacion.Placeholder = "Ingrese su RUC (13 dígitos)";
        else if (tiposeleccionado == "Pasaporte")
            txtidentificacion.Placeholder = "Ingrese su Pasaporte";
    }
    

    private async void txtidentificacion_TextChanged(object sender, TextChangedEventArgs e)
    {

        if (pickerIdentificacion.SelectedItem == null)
            return;

        string tiposelecionado = pickerIdentificacion.SelectedItem as string;
        string textoactual = e.NewTextValue ?? "";

        if ( tiposelecionado == "Cédula" || tiposelecionado == "RUC")
        {
            if (!textoactual.All(char.IsDigit))
            {
                await DisplayAlert("Advertencias", "Este campo solo acepta numeros", "OK");
                textoactual = new string(textoactual.Where(char.IsDigit).ToArray());
            }

            int maxlength = tiposelecionado == "Cédula" ? 10 : 13;
            if (textoactual.Length > maxlength)
                textoactual = textoactual.Substring(0, maxlength);

            txtidentificacion.Text = textoactual;
        }
     
    }

    private async void btnenviardatos_Clicked(object sender, EventArgs e)
    {
        if (pickerIdentificacion.SelectedIndex == null)
        {
            await DisplayAlert("Error", "Debe seleccionar un tipo de indentificacion:", "OK");
            return;
        }

        string tiposeleccionado = pickerIdentificacion.SelectedItem as string;
        string identificacion = txtidentificacion.Text?.Trim() ?? "";
        string nombres = txtnombre.Text?.Trim() ?? "";
        string apellidos = txtapellido.Text?.Trim() ?? "";
        string salariotexto = txtsalario.Text?.Trim()??"";

        if (string.IsNullOrEmpty(identificacion))
        {
            await DisplayAlert("Error", "Debe ingresar el numero de identificacion", "OK");
            return;
        }

        if (tiposeleccionado == "Cédula" && identificacion.Length !=10) 
        {
            await DisplayAlert("Error", "la cedula debe tener exaxtamente 10 numeros","OK");
            return;
        }

        if(tiposeleccionado == "RUC" && identificacion.Length !=13)
        {
            await DisplayAlert("Error", "El RUC debe tener exactamente 13 numeros","OK");
            return;
        }

        if(!decimal.TryParse(salariotexto, out decimal salariodecimal))
        {
            await DisplayAlert("Error", "El salario debe ser un numero valido.", "OK");
        }
        decimal aporte = salariodecimal * 0.0945m;


        await Navigation.PushAsync(new MostrarDatos(tiposeleccionado, identificacion, nombres, apellidos, aporte));
    }
}
