using AksharPanchang.Config;
using AksharPanchang.Panchang;
using AksharPanchang.Viewmodel;
using FileHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AksharPanchang
{
    /// <summary>
    /// Interaction logic for EventNotes.xaml
    /// </summary>
    public partial class EventNotesWindow : Window
    {
        public EventNotesWindow()
        {
            InitializeComponent();
            var engine = new FileHelperAsyncEngine<EventNote>();
            using (engine.BeginReadFile(ApplicationManager.Instance.EVENTNOTES_FILE)) ;
            {
                engine.BeginReadFile(ApplicationManager.Instance.EVENTNOTES_FILE);
                // The engine is IEnumerable
                foreach (EventNote notes in engine)
                {
                    // your code here
                    MainWindowViewObj.Instance.EventNoteList.Add(notes);
                }
            }
            this.EventDataGrid.ItemsSource = MainWindowViewObj.Instance.EventNoteList;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            foreach(EventNote row in this.EventDataGrid.Items)
            {
                MessageBox.Show(row.Tithi);
            }
        }
    }
}
