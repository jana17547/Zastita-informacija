using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        ServiceReference1.IService1 client = new ServiceReference1.Service1Client();
        public Form1()
        {
            InitializeComponent();
        }

        byte[] fInfo1, fInfo2;

       ////////////BIFID////////////

        //Encrypt input Bifid
        private void btnEncrBF_Click(object sender, EventArgs e)
        {
            outputBF.Text = client.CryptBifid(inputBF.Text, inputKeyBF.Text);
        }

        //Decrypt input Bifid 
        private void btnDecrBF_Click(object sender, EventArgs e)
        {
            
            outputBF.Text = client.DecryptBifid(inputBF.Text, inputKeyBF.Text);
        }


        //Input key file Bifid
        private void keyFileBF_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            d.Filter = "Text files (*.txt)|*.txt|Binary files (*.bin)|*.bin";
            if (d.ShowDialog() == DialogResult.OK)
            {
                string putanja = d.FileName;
                keyPathBF.Text = putanja;
            }
        }

        //Input file Bifid
        private void btnFileBF_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            d.Filter = "Text files (*.txt)|*.txt|Binary files (*.bin)|*.bin";
            if (d.ShowDialog() == DialogResult.OK)
            {
                string putanja = d.FileName;
                pathInputBF.Text = putanja;
            }
        }

        //Output file Bifid
        private void outFileBF_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            d.Filter = "Text files (*.txt)|*.txt|Binary files (*.bin)|*.bin";
            if (d.ShowDialog() == DialogResult.OK)
            {
                string putanja = d.FileName;
                otputPathBF.Text = putanja;
            }
        }

        //Encrypt file Bifid
        private void btnfileEncBF_Click(object sender, EventArgs e)
        {
            if (pathInputBF.Text != "" && otputPathBF.Text != "" && keyPathBF.Text != "")
            {
                if (pathInputBF != null)
                {
                    fInfo1 = client.TH(pathInputBF.Text);
                }
                if (pathInputBF.Text.EndsWith(".txt") && otputPathBF.Text.EndsWith(".txt") && keyPathBF.Text.EndsWith(".txt"))
                {
                    //fajlovi su .txt
                    string readInput = File.ReadAllText(pathInputBF.Text);
                    string readKey = File.ReadAllText(keyPathBF.Text);

                    //povratna vrednost enkripcije
                    string res = client.CryptBifid(readInput, readKey);

                    //upis
                    File.WriteAllText(otputPathBF.Text, res);

                }
                if (pathInputBF.Text.EndsWith(".bin") && otputPathBF.Text.EndsWith(".bin") && keyPathBF.Text.EndsWith(".bin"))
                {
                    //fajlovi su .bin
                    string readInput = File.ReadAllText(pathInputBF.Text);
                    string readkey = File.ReadAllText(keyPathBF.Text);
                    string res = client.CryptBifid(readInput, readkey);
                    File.WriteAllBytes(otputPathBF.Text, Encoding.UTF8.GetBytes(res));
                }
            }
        }

        //Decrypt file Bifid
        private void btnfileDecBF_Click(object sender, EventArgs e)
        {
            if (pathInputBF.Text != "" && otputPathBF.Text != "" && keyPathBF.Text != "")
            {
                if (pathInputBF.Text.EndsWith(".txt") && otputPathBF.Text.EndsWith(".txt") && keyPathBF.Text.EndsWith(".txt"))
                {
                    //fajlovi su .txt
                    string readInput = File.ReadAllText(pathInputBF.Text);
                    string readKey = File.ReadAllText(keyPathBF.Text);

                    //povratna vrednost enkripcije
                    string res = client.DecryptBifid(readInput, readKey);

                    //upis
                    File.WriteAllText(otputPathBF.Text, res);

                }
                if (pathInputBF.Text.EndsWith(".bin") && otputPathBF.Text.EndsWith(".bin") && keyPathBF.Text.EndsWith(".bin"))
                {
                    //fajlovi su .bin
                    string readInput = File.ReadAllText(pathInputBF.Text);
                    string readkey= File.ReadAllText(keyPathBF.Text);
                    string res = client.DecryptBifid(readInput,readkey);
                    File.WriteAllBytes(otputPathBF.Text, Encoding.UTF8.GetBytes(res));
                }
            }
                if (otputPathBF != null)
                {
                    fInfo2 = client.TH(otputPathBF.Text);
                    if (fInfo1.SequenceEqual(fInfo2))
                    {
                        MessageBox.Show("Pocetni i dekriptovani fajl su isti");
                    }
                    else
                    {
                        MessageBox.Show("Pocetni i dekriptovani fajl nisu isti");
                    }
                }
            
        }
        ///////////END BIFID///////////


        ////////////RC6////////////////
        //Encrypt inout RC6
        private void encryptRC6_Click(object sender, EventArgs e)
        {


            string plaintext = inputRC6.Text;
            // byte[] key = Encoding.ASCII.GetBytes(keyRC6.Text);
            //int keyround = Int32.Parse(inputRoundRC6.Text);
            byte[] ciphertext = client.CryptRC6(plaintext);

            //outputRC6.Text = Encoding.UTF8.GetString(ciphertext);
            outputRC6.Text = Convert.ToBase64String(ciphertext);
        }

        //Decrypt input RC6
        private void decryptRC6_Click(object sender, EventArgs e)
        {

            string readInput = outputRC6.Text;
            byte[] input = Convert.FromBase64String(readInput);
            byte[] res = client.DecryptRC6(input);
            string res1 = Encoding.UTF8.GetString(res);
            inputRC6.Text = res1;


        }

        //Encrypt file RC6
        private void btnEncrFileRC6_Click(object sender, EventArgs e)
        {
            if (inputfile.Text != "" && outputfile.Text != "")
            {
               // byte[] readInputBytes;
                if (inputfile != null)
                {
                    fInfo1 = client.TH(inputfile.Text);
                }
                if (inputfile.Text.EndsWith(".txt") && outputfile.Text.EndsWith(".txt"))
                {
                    //fajlovi su .txt
                    string readInput = File.ReadAllText(inputfile.Text);
                   // string readKey = File.ReadAllText(keyfile.Text);

                    // readInputBytes = Encoding.UTF8.GetBytes(readInput);

                    byte[] res = client.CryptRC6(readInput);
                    File.WriteAllText(outputfile.Text, Convert.ToBase64String(res));

                }

                ////ZA BINARNE FAJLOVE
                if (inputfile.Text.EndsWith(".bin") && outputfile.Text.EndsWith(".bin"))
                {
                    //fajlovi su .bin
                    string readInput = File.ReadAllText(inputfile.Text);
     
                    byte[] res = client.CryptRC6(readInput);
                    File.WriteAllBytes(otputPathBF.Text, res);
                }

            }
        }

        //DEcrypt file RC6
        private void btnDecrFileRC6_Click(object sender, EventArgs e)
        {
            if (inputfile.Text != "" && outputfile.Text != "")
            {
                if (inputfile.Text.EndsWith(".txt") && outputfile.Text.EndsWith(".txt"))
                {
                    //fajlovi su .txt
                    string readInput = File.ReadAllText(inputfile.Text);
                    // string readKey = File.ReadAllText(keyPathBF.Text);
                    byte[] input = Convert.FromBase64String(readInput);
                    //povratna vrednost enkripcije
                    byte[] res = client.DecryptRC6(input);
                    string res1 = Encoding.UTF8.GetString(res);
                    //upis
                    File.WriteAllText(outputfile.Text, res1);

                }
                if (inputfile.Text.EndsWith(".bin") && outputfile.Text.EndsWith(".bin"))
                {
                    //fajlovi su .bin
                    string readInput = File.ReadAllText(inputfile.Text);
                    byte[] read = Encoding.UTF8.GetBytes(readInput);
                    byte[] res = client.DecryptRC6(read);
                    File.WriteAllBytes(otputPathBF.Text, res);
                }
            }
                if (outputfile != null)
                {
                    fInfo2 = client.TH(outputfile.Text);
                    if (fInfo1.SequenceEqual(fInfo2))
                    {
                        MessageBox.Show("Pocetni i dekriptovani fajl su isti");
                    }
                    else
                    {
                        MessageBox.Show("Pocetni i dekriptovani fajl nisu isti");
                    }
                }
            
        }

        //input file RC6
        private void inputfileRC6_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            d.Filter = "Text files (*.txt)|*.txt|Binary files (*.bin)|*.bin";
            if (d.ShowDialog() == DialogResult.OK)
            {
                string putanja = d.FileName;
                inputfile.Text = putanja;
            }

        }

        //key file RC6
      /*  private void keyfileRC6_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            d.Filter = "Text files (*.txt)|*.txt|Binary files (*.bin)|*.bin";
            if (d.ShowDialog() == DialogResult.OK)
            {
                string putanja = d.FileName;
                keyfile.Text = putanja;
            }
        }*/

        //output file RC6
        private void outputfileRC6_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            d.Filter = "Text files (*.txt)|*.txt|Binary files (*.bin)|*.bin";
            if (d.ShowDialog() == DialogResult.OK)
            {
                string putanja = d.FileName;
                outputfile.Text = putanja;
            }
        }

        //////////END RC6/////////////////

        //////////KNAPSACK////////////////
        //Encrypt input KnapSack
        private void btnEncKS_Click(object sender, EventArgs e)
        {
            string message = inputKS.Text;

            outputKS.Text= client.EncryptKS(message);
           
        }

        //Decrypt input KnapSack
        private void btnDecrKS_Click(object sender, EventArgs e)
        {
            inputKS.Text= client.DecryptKS(outputKS.Text);

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        //input file KnapSack
        private void btnInputFileKS_Click(object sender, EventArgs e)
        {
           OpenFileDialog d = new OpenFileDialog();
            d.Filter = "Text files (*.txt)|*.txt|Binary files (*.bin)|*.bin";
            if (d.ShowDialog() == DialogResult.OK)
            {
                string putanja = d.FileName;
                inputPathKS.Text = putanja;
            }
        }

        //output file KnapSack
        private void btnOutputFileKS_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            d.Filter = "Text files (*.txt)|*.txt|Binary files (*.bin)|*.bin";
            if (d.ShowDialog() == DialogResult.OK)
            {
                string putanja = d.FileName;
                outputPathKS.Text = putanja;
            }
        }

        //Encrypt file KnapSack
        private void encrFileKS_Click(object sender, EventArgs e)
        {
            
            if (inputPathKS.Text != "" && outputPathKS.Text != "")
            {
                if (inputPathKS != null)
                {
                    fInfo1 = client.TH(inputPathKS.Text);
                }
                if (inputPathKS.Text.EndsWith(".txt") && outputPathKS.Text.EndsWith(".txt"))
                {
                    //fajlovi su .txt
                    string readInput = File.ReadAllText(inputPathKS.Text);

                    string res = client.EncryptKS(readInput);

                    //upis
                   File.WriteAllText(outputPathKS.Text, res);

                }
                if (inputPathKS.Text.EndsWith(".bin") && outputPathKS.Text.EndsWith(".bin"))
                {
                    //fajlovi su .bin
                    string readInput = File.ReadAllText(inputPathKS.Text);

                    string res = client.EncryptKS(readInput);
                    
                    File.WriteAllBytes(outputPathKS.Text, Encoding.UTF8.GetBytes(res));
                }

            }

        }

        //Decrypt file KnapSack
        private void decryptFileKS_Click(object sender, EventArgs e)
        {

            if (inputPathKS.Text != "" && outputPathKS.Text != "")
            {
                if (inputPathKS.Text.EndsWith(".txt") && outputPathKS.Text.EndsWith(".txt"))
                {
                    //fajlovi su .txt
                    string readInput = File.ReadAllText(inputPathKS.Text);

                    string res = client.DecryptKS(readInput);

                    //upis
                    File.WriteAllText(outputPathKS.Text, res);

                }
                if (inputPathKS.Text.EndsWith(".bin") && outputPathKS.Text.EndsWith(".bin"))
                {
                    //fajlovi su .bin
                    string readInput = File.ReadAllText(inputPathKS.Text);
                    
                    string res = client.DecryptKS(readInput);

                    File.WriteAllBytes(outputPathKS.Text, Encoding.UTF8.GetBytes(res));
                }
            }
                if (outputPathKS != null)
                {
                    fInfo2 = client.TH(outputPathKS.Text);
                    if (fInfo1.SequenceEqual(fInfo2))
                    {
                        MessageBox.Show("Pocetni i dekriptovani fajl su isti");
                    }
                    else
                    {
                        MessageBox.Show("Pocetni i dekriptovani fajl nisu isti");
                    }
                }

            
        }

        ////////END KNAPSACK////////////////

        private void inputBF_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        //Encrypt input CTR
        private void encrCTR_Click(object sender, EventArgs e)
        {
            var input = Encoding.UTF8.GetBytes(inputCTR.Text);
            outputCTR.Text = Convert.ToBase64String(client.EncryptCTR(input));
        }

        private void decrCTR_Click(object sender, EventArgs e)
        {
            var input = Convert.FromBase64String(inputCTR.Text);
            outputCTR.Text = Encoding.UTF8.GetString(client.DecryptCTR(input)).Trim();
        }

         //Input file CTR//
        private void btnfileInputCTR_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            d.Filter = "Text files (*.txt)|*.txt|Binary files (*.bin)|*.bin";
            if (d.ShowDialog() == DialogResult.OK)
            {
                string putanja = d.FileName;
                inputFileCTR.Text = putanja;
            }
        }

        //Output file CTR//
        private void btnOutFileCTR_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            d.Filter = "Text files (*.txt)|*.txt|Binary files (*.bin)|*.bin";
            if (d.ShowDialog() == DialogResult.OK)
            {
                string putanja = d.FileName;
                outputFileCTR.Text = putanja;
            }

        }

        //Encrypt file CTR//
        private void encrFileCTR_Click(object sender, EventArgs e)
        {

            if (inputFileCTR.Text != "" && outputFileCTR.Text != "")
            {
                byte[] readInputBytes;
                if (inputFileCTR != null)
                {
                    fInfo1 = client.TH(inputFileCTR.Text);
                }
                if (inputFileCTR.Text.EndsWith(".txt") && outputFileCTR.Text.EndsWith(".txt"))
                {
                    //fajlovi su .txt
                    string readInput = File.ReadAllText(inputFileCTR.Text);
                    readInputBytes = Encoding.UTF8.GetBytes(readInput);
                    byte[] res = client.EncryptCTR(readInputBytes);
                    File.WriteAllText(outputFileCTR.Text, Convert.ToBase64String(res));

                }
                if (inputFileCTR.Text.EndsWith(".bin") && outputFileCTR.Text.EndsWith(".bin"))
                {
                    //fajlovi su .bin
                    readInputBytes = File.ReadAllBytes(inputFileCTR.Text);
                    byte[] res = client.EncryptCTR(readInputBytes);
                    File.WriteAllBytes(outputFileCTR.Text, res);

                }
            }
        }

        //Decrypt file CTR//
        private void decrFileCTR_Click(object sender, EventArgs e)
        {
            if (inputFileCTR.Text != "" && outputFileCTR.Text != "")
            {
                
                byte[] readInputBytes;
                if (inputFileCTR.Text.EndsWith(".txt") && outputFileCTR.Text.EndsWith(".txt"))
                {
                    //fajlovi su .txt
                    string readInput = File.ReadAllText(inputFileCTR.Text);
                    readInputBytes = Convert.FromBase64String(readInput);
                    byte[] res = client.DecryptCTR(readInputBytes);
                    File.WriteAllText(outputFileCTR.Text, Encoding.UTF8.GetString(res).Trim());

                }
                if (inputFileCTR.Text.EndsWith(".bin") && outputFileCTR.Text.EndsWith(".bin"))
                {
                    //fajlovi su .bin
                    readInputBytes = File.ReadAllBytes(inputFileCTR.Text);
                    byte[] res = client.DecryptCTR(readInputBytes);
                    File.WriteAllBytes(outputFileCTR.Text, res);
                }
                }
                if (outputFileCTR != null)
                {
                    fInfo2 = client.TH(outputFileCTR.Text);
                    if (fInfo1.SequenceEqual(fInfo2))
                    {
                        MessageBox.Show("Pocetni i dekriptovani fajl su isti");
                    }
                    else
                    {
                        MessageBox.Show("Pocetni i dekriptovani fajl nisu isti");
                    }
                }
            
        }


        //////////////BMP///////////
        //Encrypt BMP
        private void btnEncBMP_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            d.Title = "Open Image";
            d.Filter = "bmp files (*.bmp)|*.bmp";
            PictureBox PictureBox1 = new PictureBox();
            string putanja = string.Empty;
            if (d.ShowDialog() == DialogResult.OK)
            {
                PictureBox1.Image = new Bitmap(d.FileName);
                putanja = Path.GetDirectoryName(d.FileName);
            }

            var image = PictureBox1.Image;
            var ms = new MemoryStream();
            image.Save(ms, image.RawFormat);
            var imgArray = ms.ToArray();

            // 56 bajta je header za bitmapu, cuvamo ga da bi kasnije mogli da napravimo kriptovanu bitmapu i njega ne kriptujemo
            byte[] headerArr = new byte[56];
            for (int i = 0; i < 56; i++)
            {
                headerArr[i] = imgArray[i];
            }

            imgArray = imgArray.Skip(56).ToArray();
            byte[] res = client.EncryptCTR(imgArray);

            var encrByte = headerArr.Concat(res);
            using (MemoryStream ms1 = new MemoryStream(encrByte.ToArray()))
            {
                Bitmap bitmap = new Bitmap(ms1);
                pictureBMP.Image = bitmap;

                pictureBMP.Image.Save(putanja + "/encryptedImage.bmp", ImageFormat.Bmp);
            }
        }

        //DEcrypt BMP
        private void btnDecBMP_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            d.Title = "Open Image";
            d.Filter = "bmp files (*.bmp)|*.bmp";
            PictureBox PictureBox1 = new PictureBox();
            string putanja = string.Empty;
            if (d.ShowDialog() == DialogResult.OK)
            {
                PictureBox1.Image = new Bitmap(d.FileName);
                putanja = Path.GetDirectoryName(d.FileName);
            }
            else
            {
                return;
            }

            var image = PictureBox1.Image;
            var ms = new MemoryStream();
            image.Save(ms, image.RawFormat);
            var imgArray = ms.ToArray();
            // 56 bajta je header za bitmapu, cuvamo ga da bi kasnije mogli da napravimo dekriptovanu bitmapu i njega ne kriptujemo
            byte[] headerArr = new byte[56];
            for (int i = 0; i < 56; i++)
            {
                headerArr[i] = imgArray[i];
            }

            imgArray = imgArray.Skip(56).ToArray();
            byte[] res = client.DecryptCTR(imgArray);

            var encrByte = headerArr.Concat(res);
            using (MemoryStream ms1 = new MemoryStream(encrByte.ToArray()))
            {
                Bitmap bitmap = new Bitmap(ms1);
                pictureBMP.Image = bitmap;

                pictureBMP.Image.Save(putanja + "/decryptedImage.bmp", ImageFormat.Bmp);
            }

        }




        //PARALELNI POZIV //

       private async Task BifidReadAndEncryptFile_Async()
        {
            if (pathInputBF.Text != null)
            {
                fInfo1 = client.TH(pathInputBF.Text);
            }

            if (pathInputBF.Text != "" && otputPathBF.Text != "" && keyPathBF.Text != "")
            {
                byte[] readInputBytes;
                if (pathInputBF.Text.EndsWith(".txt") && otputPathBF.Text.EndsWith(".txt") && keyPathBF.Text.EndsWith(".txt"))
                {
                    //fajlovi su .txt
                    string readInput = File.ReadAllText(pathInputBF.Text);
                    string redkey = File.ReadAllText(keyPathBF.Text);
                    string res = await client.CryptBifidAsync(readInput, redkey);
                    File.WriteAllText(otputPathBF.Text, res);

                }
                if (otputPathBF.Text.EndsWith(".bin") && bifiddfnal.Text.EndsWith(".bin") && keyPathBF.Text.EndsWith(".bin"))
                {
                    //fajlovi su .bin
                    readInputBytes = File.ReadAllBytes(pathInputBF.Text);
                    string read = Encoding.UTF8.GetString(readInputBytes);
                    byte[] key = File.ReadAllBytes(keyPathBF.Text);
                    string key1 = Encoding.UTF8.GetString(key);

                    string res = await client.CryptBifidAsync(read, key1);
                    byte[] res1 = Encoding.UTF8.GetBytes(res);
                    File.WriteAllBytes(otputPathBF.Text, res1);
                }
            }
        }

      

        private async Task BifidDecryptFileAndWrite_Async()
        {
            if (otputPathBF.Text != "" && bifiddfnal.Text != "" && keyPathBF.Text != "")
            {
                byte[] readInputBytes;
                if (otputPathBF.Text.EndsWith(".txt") && bifiddfnal.Text.EndsWith(".txt") && keyPathBF.Text.EndsWith(".txt"))
                {
                    //fajlovi su .txt

                    string readInput = File.ReadAllText(otputPathBF.Text);
                    string key = File.ReadAllText(keyPathBF.Text);

                    string res = await client.DecryptBifidAsync(readInput,key);

                    File.WriteAllText(bifiddfnal.Text, res);
                  



                }
                if (otputPathBF.Text.EndsWith(".bin") && bifiddfnal.Text.EndsWith(".bin") && keyPathBF.Text.EndsWith(".bin"))
                {
                    //fajlovi su .bin
                    /*  readInputBytes = File.ReadAllBytes(otputPathBF.Text);
                      string read = Encoding.UTF8.GetString(readInputBytes);
                      byte[] key = File.ReadAllBytes(keyPathBF.Text);
                      string key1 = Encoding.UTF8.GetString(key);
                      string res = await client.DecryptBifidAsync(read, key1);
                      byte[] resbyte = Encoding.UTF8.GetBytes(res);
                      File.WriteAllBytes(bifiddfnal.Text, resbyte);*/
                  
                        //fajlovi su .bin
                        string readInput = File.ReadAllText(pathInputBF.Text);
                        string readkey = File.ReadAllText(keyPathBF.Text);
                        string res = await client.DecryptBifidAsync(readInput, readkey);
                        File.WriteAllBytes(otputPathBF.Text, Encoding.UTF8.GetBytes(res));
                    
                }
            }
            if (bifiddfnal != null)
            {
                fInfo2 = client.TH(bifiddfnal.Text);
                if (fInfo1.SequenceEqual(fInfo2))
                {
                    MessageBox.Show("Pocetni i dekriptovani fajl su isti");
                }
                else
                {
                    MessageBox.Show("Pocetni i dekriptovani fajl nisu isti");
                }
            }
        }

       

       

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            d.Filter = "Text files (*.txt)|*.txt|Binary files (*.bin)|*.bin";
            if (d.ShowDialog() == DialogResult.OK)
            {
                string putanja = d.FileName;

                bifiddfnal.Text = putanja;
            }
        }

        private void btnPoziv_Click(object sender, EventArgs e)
        {
            Thread thread1 = new Thread(() => CallKS(0));
            thread1.Start();
            thread1.Join();
            Thread thread2 = new Thread(() => CallKS(1));
            thread2.Start();
        }

        private async void CallKS(int threadNum)
        {
            if (threadNum == 0)
            {
                await BifidReadAndEncryptFile_Async();
            }
            else if (threadNum == 1)
            {
                await BifidDecryptFileAndWrite_Async();
            }
        }

       

    }
}
