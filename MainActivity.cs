using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;

namespace CarService
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {

        EditText TxtUser;
        EditText TxtPassword;
        Button BtnLogin;
        Button BtnRegister;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            TxtUser = FindViewById<EditText>(Resource.Id.TxtUser);
            TxtPassword = FindViewById<EditText>(Resource.Id.TxtPassword);
            BtnLogin = FindViewById<Button>(Resource.Id.BtnLogin);
            BtnRegister = FindViewById<Button>(Resource.Id.BtnRegister);

            BtnLogin.Click += BtnLogin_Click;
            BtnRegister.Click += BtnRegister_Click;

        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            try 
            {

                Login resultado = null;
                if (!string.IsNullOrEmpty(TxtUser.Text.Trim()) && !string.IsNullOrEmpty(TxtPassword.Text.Trim()))
                {
                    resultado = new Auxiliar().SeleccionarUno(TxtUser.Text.Trim(), TxtPassword.Text.Trim());
                    if (resultado != null)
                    {
                        TxtUser.Text = resultado.Usuario.ToString();
                        Toast.MakeText(this, "Entraste con exito!", ToastLength.Short).Show();
                        var bienvenido = new Intent(this, typeof(LoginActivity));
                        bienvenido.PutExtra("Usuario", FindViewById<EditText>(Resource.Id.TxtUser).Text);
                        StartActivity(bienvenido);
                        Finish();
                    }
                    else
                    {
                        Toast.MakeText(this, "Usuario o clave incorrectas", ToastLength.Long).Show();

                    }


                }
                else 
                {
                    Toast.MakeText(this, "Nombre o clave incorrectas", ToastLength.Long).Show();

                }

            
            }
            catch(Exception ex) 
            {
            Toast.MakeText(this, ex.ToString(), ToastLength.Long).Show();
            }
            throw new NotImplementedException();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}