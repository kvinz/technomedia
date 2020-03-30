using Ext.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Technomedia.Domain.Entities;

namespace Technomedia.WebUI.Models
{
    public class CreateEditCaseViewModel
    {
        public Case Case { get; set; }

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

        /// <summary>
        /// Дата закрытия заявки
        /// </summary>
        public string DateClosedCase { get; set; }

        /// <summary>
        /// Время закрытия заявки
        /// </summary>
        public string TimeClosedCase { get; set; }

    }
}