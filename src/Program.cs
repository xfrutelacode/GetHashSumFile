/*
 * Данная программа была создана на С# не спорю, что есть готовые такие программы.(плохо искал)
 * Но для меня данной программой пользоваться удобство
 * Делал xfrutelacode
 */

//Подключаем нужные либы. Остальное можно сразу же удалить
using System;
using System.IO;
using System.Security.Cryptography;


//Пространство имён
namespace GetHashFile
{
    //Класс
    class Program
    {
        //Точка входа
        static void Main(string[] args)
        {
            //Вывод сообщения
            Console.WriteLine("Привет, давай тяганем хэшку? coded by xfrutelacode\n1.Укажите путь до папки('C:\\file')\n2.Нажми Enter и радуйся жизни");
            //Читаем то, что вводит юзер и пишем в path
            string path = Console.ReadLine();

            //В качестве хэш'а юзаем SHA256
            SHA256 sha256 = new SHA256CryptoServiceProvider();
            //юзаем форич, чтобы получить все файлы во всех директ. в папке указанной
            foreach (string file in Directory.EnumerateFiles(Environment.ExpandEnvironmentVariables(path), "*", SearchOption.AllDirectories))
            {
                //юзаю FileStream т.к. он поддерживает синхронные и асинхронные операции чтения и не только тут использую в качес. чтение, а больше нам не надо
                FileStream fileStream = File.OpenRead(file);
                //Получаем массив байтов из размера файла НЕ КИЛОБАЙТ, а БАЙТЫ!!!!!
                byte[] fileBytes = new byte[fileStream.Length];
                //Читаем
                fileStream.Read(fileBytes, 0, fileBytes.Length);
                //вычисляем хэш из полученого размера файла
                byte[] compHashSum = sha256.ComputeHash(fileBytes);
                //А тута мы будем хранить хэш сам
                string hashSum = "";
                foreach (byte btH in compHashSum)
                {
                  //Получаем с массив байтов сам хэш
                   hashSum += $"{btH:x2}";
                }
                //Выводим
                Console.WriteLine($"\nФайл: {file} Хэш-Сумма: {hashSum.ToUpper()}");
                //Отсюда можно общий хэш получить, но лучше так не делать ;)
                //Давайте поместим их куда-то

                //Переменная которая будет указывать куда создать и помещать
                string pathCreatTXT = Path.Combine(Environment.GetEnvironmentVariable("SYSTEMDRIVE"), "\\hashsum.txt");
                FileStream fileSt = File.Open(pathCreatTXT, FileMode.OpenOrCreate, FileAccess.Write);
                //Не забываем закрыть, но можно было бы использовать using, но он у меня криво работает и них*я не закрывает
                fileSt.Close();

                //Записываемв текстовый файл данные
                StreamWriter streamWriter = new StreamWriter(pathCreatTXT, true);
                streamWriter.WriteLine($"\nФайл: {file} Хэш-Сумма: {hashSum.ToUpper()}");
                //Не забываем закрыть
				//Можно использовать снова using, как я ранее писал он у меня нормально не работает
                streamWriter.Close();

                //Так же не забываем закрыть ранее открытый FileStream (32 строка)
                fileStream.Close();
            }

            //Мы все сделали 
            
            //Ожидаем пока юзер нажмет на любую клавишу и закроем программу
            Console.ReadKey();
        }

    }
}

/*
 * Конец закрываем все и бла бла бла
 * МОжно аптЫмЫзировать можно много че сделать
 * у вас октрытый исходный код делайте, что хотите
 */
