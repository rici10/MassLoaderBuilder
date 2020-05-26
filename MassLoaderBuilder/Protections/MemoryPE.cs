namespace MassLoaderBuilder.Protections
{
    #region References

    using System;
    using System.Runtime.InteropServices;

    #endregion

    internal static class MemoryPE
    {
        public static unsafe void Initialize()
        {
            byte* ptr = (byte*)(void*)Marshal.GetHINSTANCE(typeof(MemoryPE).Module); //получаем базовый адрес.
            byte* ptr2 = ptr + 60; // смещаемся до e_lfanew
            ptr2 = ptr + *(uint*)ptr2; //топаем в File Header
            ptr2 += 6; // Смещаемся до NumberOfSection
            ushort num = *(ushort*)ptr2; //получаем кол-во секций
            ptr2 += 14; //смещаемся до поля с размером опционального заголовка
            ushort num2 = *(ushort*)ptr2; // получаем размер опционального заголовка
            ptr2 = ptr2 + 4 + num2; // огибаем опциональный заголовок
            SafeNativeMethods.VirtualProtect(ptr2 - 16, (UIntPtr)8, 64U, out uint num3); //ставим защиту PAGE_EXECUTEREADWRITE чтобы иметь возможность писать в участок памяти
            *(int*)(ptr2 - 12) = 0; //обнуляем размер .NET MetaData
            byte* ptr3 = ptr + *(uint*)(ptr2 - 16); // указатель на .Net Directory
            *(int*)(ptr2 - 16) = 0; //обнуляем указатель на .NET Directory
            SafeNativeMethods.VirtualProtect(ptr3, (UIntPtr)72, 64U, out num3); //set read/write/execute protect
            byte* ptr4 = ptr + *(uint*)(ptr3 + 8); //получаем указатель на MetaData
            *(int*)ptr3 = 0; //обнуляем MajorRuntimeVersion
            *(int*)(ptr3 + 4) = 0; //обнуляем MinorRuntimeVersion
            *(int*)(ptr3 + 8) = 0; //Обнуляем указатель на MetaData
            *(int*)(ptr3 + 12) = 0; //Обнуляем размер MetaData
            SafeNativeMethods.VirtualProtect(ptr4, (UIntPtr)4, 64U, out num3); //ставим доступную защиту на сигнатуру в MetaData Header
            *(int*)ptr4 = 0; //обнуляем эту сигнатуру
            for (int i = 0; i < num; i++) //перебираем все записи в таблице секций.
            {
                SafeNativeMethods.VirtualProtect(ptr2, (UIntPtr)8, 64U, out num3); //ставим новую защиту, чтобы мы могли писать в память
                Marshal.Copy(new byte[8], 0, (IntPtr)(void*)ptr2, 8); //Обнуляем запись о секции в таблице секций
                ptr2 += 40; //переходим к следующей секции в таблице секций.
            }
        }
    }
}