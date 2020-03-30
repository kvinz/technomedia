using Ext.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Technomedia.WebUI.Models
{
    public class AnalyticsViewModel
    {
        /// <summary>
        /// Хранилище элементов для ComboBox
        /// </summary>
        public IEnumerable<ListItem> ListItems { get; set; }

        /// <summary>
        /// Хранит заданное значение ComboBox
        /// </summary>
        public ListItem ListItemValue { get; set; }

        /// <summary>
        /// Содержит выбранное значение из ComboBox
        /// </summary>
        public string SelectedListItem { get; set; }

        public IEnumerable<CaseViewModel> CasesData { get; set; }
    }
}