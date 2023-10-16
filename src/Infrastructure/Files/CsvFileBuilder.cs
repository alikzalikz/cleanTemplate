using System.Globalization;
using CharchoobApi.Application.Common.Interfaces;
using CharchoobApi.Application.TodoLists.Queries.ExportTodos;
using CharchoobApi.Infrastructure.Files.Maps;
using CsvHelper;

namespace CharchoobApi.Infrastructure.Files;

public class CsvFileBuilder : ICsvFileBuilder
{
    public byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records)
    {
        using var memoryStream = new MemoryStream();
        using (var streamWriter = new StreamWriter(memoryStream))
        {
            using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);

            csvWriter.Context.RegisterClassMap<TodoItemRecordMap>();
            csvWriter.WriteRecords(records);
        }

        return memoryStream.ToArray();
    }
}
