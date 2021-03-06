﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Threading;
using System.Globalization;
using Microsoft.Phone.UserData;
using System.Windows.Media.Imaging;
using System.IO;

namespace PayMe
{
    public partial class App : Application
    {
        private static PayMeListViewModel _PayMeList = null;

        private static ContactPicturesDictionaryModel _ContactPictures = null;

        /// <summary>
        /// ViewModel estático que utilizan las vistas para enlazarse.
        /// </summary>
        /// <returns>Objeto MainViewModel.</returns>
        public static PayMeListViewModel PayMeList
        {
            get
            {
                // Retrasar la creación del modelo de vista hasta que sea necesario
                if (_PayMeList == null)
                    _PayMeList = new PayMeListViewModel();

                return _PayMeList;
            }
        }

        public static ContactPicturesDictionaryModel ContactPictures
        {
            get
            {
                // Retrasar la creación del modelo de vista hasta que sea necesario
                if (_ContactPictures == null)
                    _ContactPictures = new ContactPicturesDictionaryModel();

                return _ContactPictures;
            }
        }

        /// <summary>
        /// Proporciona un fácil acceso al marco raíz de la aplicación de Windows Phone.
        /// </summary>
        /// <returns>Marco raíz de la aplicación de Windows Phone.</returns>
        public PhoneApplicationFrame RootFrame { get; private set; }

        /// <summary>
        /// Constructor para el objeto de aplicación.
        /// </summary>
        public App()
        {
            // Controlador global para excepciones no detectadas. 
            // Recuerde que las excepciones que devuelva ApplicationBarItem.Click no se detectarán aquí.
            UnhandledException += Application_UnhandledException;

            // Mostrar información de perfiles de gráficos al depurar.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // Mostrar los contadores de velocidad de fotogramas actuales.
                Application.Current.Host.Settings.EnableFrameRateCounter = true;

                // Mostrar las áreas de la aplicación que se están redibujando en cada fotograma.
                //Application.Current.Host.Settings.EnableRedrawRegions = true;

                // Habilitar el modo de visualización del análisis de no producción, 
                // que muestra las áreas de una página que reciben aceleración por GPU con una superposición de color.
                //Application.Current.Host.Settings.EnableCacheVisualization = true;
            }

            // Inicialización de Silverlight estándar
            InitializeComponent();

            // Inicialización específica del teléfono
            InitializePhoneApplication();
        }

        // Código que se ejecuta al iniciar la aplicación (p. ej., desde Inicio)
        // Este código no se ejecutará cuando se vuelva a activar la aplicación
        private void Application_Launching(object sender, LaunchingEventArgs e) {
            ContactPictures.LoadData();
        }

        // Código que se ejecuta al activar la aplicación (pasa a primer plano)
        // Este código no se ejecutará cuando se inicia la aplicación por primera vez
        private void Application_Activated(object sender, ActivatedEventArgs e)
        {
            if (!_PayMeList.IsDataLoaded)
            {
                _PayMeList.LoadData();
            }
        }

        // Código que se ejecuta al desactivar la aplicación (se envía a un segundo plano)
        // Este código no se ejecutará cuando se cierre la aplicación
        private void Application_Deactivated(object sender, DeactivatedEventArgs e)
        {
            _PayMeList.SaveToDisk();
        }

        // Código que se ejecuta al cerrar la aplicación (p. ej., cuando el usuario pulsa Atrás)
        // Este código no se ejecutará cuando se desactive la aplicación
        private void Application_Closing(object sender, ClosingEventArgs e) {
            _PayMeList.SaveToDisk();
            _ContactPictures.SaveToDisk();
        }

        // Código que se ejecuta si se produce un error en una navegación
        private void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // Error en la navegación; interrumpir depurador
                System.Diagnostics.Debugger.Break();
            }
        }

        // Código que se ejecuta ante excepciones no controladas
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // Se ha producido una excepción no controlada; interrumpir depurador
                Console.WriteLine(e.ToString());
                System.Diagnostics.Debugger.Break();
            }
        }

        #region Inicialización de la aplicación del teléfono

        // Evite la doble inicialización
        private bool phoneApplicationInitialized = false;

        // No agregue ningún código adicional a este método
        private void InitializePhoneApplication()
        {
            if (phoneApplicationInitialized)
                return;

            // Cree el marco pero no lo establezca todavía como RootVisual; esto permite que la pantalla
            // de presentación permanezca activa hasta que la aplicación esté preparada para representarse.
            RootFrame = new PhoneApplicationFrame();
            RootFrame.Navigated += CompleteInitializePhoneApplication;

            // Controlar errores de navegación
            RootFrame.NavigationFailed += RootFrame_NavigationFailed;

            // Asegúrese de que no volvemos a inicializar
            phoneApplicationInitialized = true;
        }

        // No agregue ningún código adicional a este método
        private void CompleteInitializePhoneApplication(object sender, NavigationEventArgs e)
        {
            // Establezca el visual raíz para permitir que se represente la aplicación
            if (RootVisual != RootFrame)
                RootVisual = RootFrame;

            // Quite este controlador porque ya no es necesario
            RootFrame.Navigated -= CompleteInitializePhoneApplication;
        }

        #endregion
		
		private void newButton_Click(object sender, EventArgs e) {
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/NewPayMePage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/Configuration.xaml", UriKind.RelativeOrAbsolute));
            //Do work for your application here.
        }

        private void aboutMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Button 3 works!");
            //Do work for your application here.
        }
		
		public void closeApplication() {
			this.Application_Closing(new Object(), new ClosingEventArgs());
		}
    }
}