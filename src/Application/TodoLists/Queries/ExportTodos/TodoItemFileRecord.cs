using CharchoobApi.Application.Common.Mappings;
using CharchoobApi.Domain.Entities;

namespace CharchoobApi.Application.TodoLists.Queries.ExportTodos;

public class TodoItemRecord : IMapFrom<TodoItem>
{
    public string? Title { get; init; }

    public bool Done { get; init; }
}
