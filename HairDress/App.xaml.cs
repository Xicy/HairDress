using System;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace HairDress
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomainOnAssemblyResolve;
        }

        private Assembly CurrentDomainOnAssemblyResolve(object sender, ResolveEventArgs args)
        {
            string assmbly;
            if ((assmbly = Assembly.GetEntryAssembly().GetManifestResourceNames().First(x => x.Contains(args.Name.Split(',')[0]))) != null)
            {
                var stream = Assembly.GetEntryAssembly().GetManifestResourceStream(assmbly);
                var buffer = new byte[stream.Length];
                stream.Read(buffer, 0, (int)stream.Length);
                return Assembly.Load(buffer);
            }

            throw new ArgumentNullException();
        }
    }
}