using MauiAppTempoAgora.Models;
using MauiAppTempoAgora.Services;

namespace MauiAppTempoAgora
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txt_cidade.Text))
                {
                    // Verificar conexão antes de fazer a requisição
                    if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
                    {
                        await DisplayAlert("Sem Conexão",
                            "Você está sem conexão com a internet. Por favor, conecte-se e tente novamente.",
                            "OK");
                        return;
                    }
                    Tempo? t = await DataService.GetPrevisao(txt_cidade.Text);

                    if (t != null)
                    {
                        string dados_previsao = "";

                        dados_previsao = $"Latitude: {t.lat} \n" +
                                         $"Longitude: {t.lon} \n" +
                                         $"Nascer do Sol: {t.sunrise} \n" +
                                         $"Por do Sol: {t.sunset} \n" +
                                         $"Temp Máx: {t.temp_max} \n" +
                                         $"Temp Min: {t.temp_min} \n" +
                                         $"Velocidade do Vento: {t.speed} \n" +
                                         $"Clima do Tempo: {t.main} \n"+
                                         $"Visibilidade: {t.visibility} \n";

                        lbl_res.Text = dados_previsao;

                    }
                    else
                    {
                        await DisplayAlert("Cidade não encontrada",
                            "Não foi possível encontrar a cidade especificada. Verifique o nome e tente novamente.",
                            "OK");
                        lbl_res.Text = "Cidade não encontrada";
                    }

                }
                else
                {
                    await DisplayAlert("Campo Em Branco", "Preencha a Cidade","Ok");
                    lbl_res.Text = "Preencha a cidade.";
                }

            }
            catch (Exception ex)
            {
                // Mensagens específicas para diferentes tipos de erro
                if (ex.Message.Contains("Sem conexão com a internet"))
                {
                    await DisplayAlert("Sem Conexão",
                        "Você está sem conexão com a internet. Por favor, conecte-se e tente novamente.",
                        "OK");
                    lbl_res.Text = "Sem conexão com a internet";
                }
                else
                {
                    await DisplayAlert("Erro", ex.Message, "OK");
                    lbl_res.Text = "Erro ao obter previsão";
                }
            }
        }
    }

}
