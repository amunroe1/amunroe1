using System;
using MyEncryption;

namespace Group43
{
    public partial class AndrewTryIt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MyEncryption.Cipher cipher = new MyEncryption.Cipher();

        }

        protected void FilterButton_Click(object sender, EventArgs e)
        {
            WordCountSvc.Service1Client client = new WordCountSvc.Service1Client();


            // Encode the input to allow tags like <head>
            string input = FilterInput.Text;
            try
            {
                string response = client.FilterContent(input);
                client.Close();
                responseOutput.Text = response;

            }
            catch (Exception ex)
            {
                responseOutput.Text = ex.Message;
                client.Abort();
                return;
            }
        }

        protected void WordCountButton_Click(object sender, EventArgs e)
        {
            string input = CountInput.Text;

            WordCountSvc.Service1Client client = new WordCountSvc.Service1Client();
            try
            {
                string response = client.GetWordCount(input);
                client.Close();
                responseOutput2.Text = response;
            }
            catch (Exception ex)
            {
                responseOutput2.Text = ex.Message;
                client.Abort();
                return;
            }
        }

        protected void DecryptButton_Click(object sender, EventArgs e)
        {
            EncryptionOutput.Text=Cipher.Decrypt(DecryptionInput.Text);
        }

        protected void EncryptButton_Click(object sender, EventArgs e)
        {
            EncryptionOutput.Text=Cipher.Encrypt(EncryptionInput.Text);
        }

        protected void Error_Click(object sender, EventArgs e)
        {
            // Simulate an error for testing purposes
            throw new InvalidOperationException("This is a test exception for the error page.");
        }
    }
}