using System.IO.Compression;

Console.WriteLine("Введите путь к папке проекта:");
string? projectPath = Console.ReadLine()?.Trim(['"']);

if (!Directory.Exists(projectPath))
{
    Console.WriteLine("Папка не существует!");
    return;
}

try
{
    // Удаление папок bin и obj
    DeleteBinAndObjFolders(projectPath);
    Console.WriteLine("Папки bin и obj удалены.");

    // Создание имени архива
    string zipPath = Path.Combine(Path.GetDirectoryName(projectPath),
        $"{Path.GetFileName(projectPath)}_{DateTime.Now:yyyyMMdd_HHmmss}.zip");

    // Создание ZIP-архива
    ZipFile.CreateFromDirectory(projectPath, zipPath, CompressionLevel.Optimal, false);
    Console.WriteLine($"Архив создан: {zipPath}");
}
catch (Exception ex)
{
    Console.WriteLine($"Ошибка: {ex.Message}");
}

static void DeleteBinAndObjFolders(string path)
{
    // Поиск всех папок в проекте
    foreach (string dir in Directory.GetDirectories(path, "*", SearchOption.AllDirectories))
    {
        // Проверка, является ли папка bin или obj
        if (Path.GetFileName(dir).Equals("bin", StringComparison.OrdinalIgnoreCase) ||
            Path.GetFileName(dir).Equals("obj", StringComparison.OrdinalIgnoreCase))
        {
            try
            {
                Directory.Delete(dir, true);
                Console.WriteLine($"Удалена папка: {dir}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Не удалось удалить {dir}: {ex.Message}");
            }
        }
    }
}
