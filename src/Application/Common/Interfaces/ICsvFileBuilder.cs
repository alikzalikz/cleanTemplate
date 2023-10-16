using CharchoobApi.Application.TodoLists.Queries.ExportTodos;

namespace CharchoobApi.Application.Common.Interfaces;

public interface ICsvFileBuilder
{
    byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
}
