using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using FromBodyAttribute = Microsoft.Azure.Functions.Worker.Http.FromBodyAttribute;
using Microsoft.Extensions.Logging;
using Azure.Data.Tables;
using Azure;

namespace ServerlessTodoApi
{
    public class TodoItems
    {
        private readonly ILogger<TodoItems> _logger;

        public TodoItems(ILogger<TodoItems> logger)
        {
            _logger = logger;
        }

		public class TodoItemAddOutput {
			[HttpResult]
			public IActionResult Result {get; set;}

			[TableOutput("MyTable")]
			public TodoItem tableResult {get; set;}
		}

        [Function(nameof(TodoItemAdd))]
        public TodoItemAddOutput TodoItemAdd(
			[HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "todoitem")] HttpRequest req, 
			[FromBody] TodoItem newItem)
        {
            _logger.LogInformation("Upserting item: " + newItem.ItemName);
			if (string.IsNullOrEmpty(newItem.Id))
			{
				newItem.PartitionKey = "";
				// New Item so add ID and date
				_logger.LogInformation("Item is new.");
				newItem.Id = Guid.NewGuid().ToString();
				newItem.RowKey = newItem.Id;
				newItem.Created = DateTime.UtcNow;
			}

            return new TodoItemAddOutput() {
				Result = new OkObjectResult(newItem),
				tableResult = newItem
			};
        }
      
		// Get all items
		[Function("TodoItemGetAll")]
		public IActionResult GetAll(
			[HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "todoitem")] HttpRequest req,
			[TableInput("MyTable")] IEnumerable<TodoItem> todoItems
		) => new OkObjectResult(todoItems.ToArray());
        
		// Delete item by id
		[Function(nameof(TodoItemDelete))]
		public async Task<IActionResult> TodoItemDelete(
		   [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "todoitem/{id}")]HttpRequestMessage req, string id,
		   [TableInput("MyTable")] TableClient mytable)
		{
			try {
				await mytable.DeleteEntityAsync("", id);
			} catch(Exception ex) {
				_logger.LogWarning($"An error occured during deletion: {ex.Message}");
			}
			return new NoContentResult();
		}

		[Function(nameof(UpdateTodoItem))]
		public async Task<IActionResult> UpdateTodoItem(
			[HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "todoitem/{id}")]HttpRequestMessage req, string id,
		   [TableInput("MyTable")] TableClient mytable, [FromBody] TodoItem newItem) {
				newItem.Created = DateTime.SpecifyKind(newItem.Created, DateTimeKind.Utc);
				try {
					await mytable.UpdateEntityAsync(newItem, ETag.All);
				} catch(Exception ex) {
					_logger.LogWarning($"An error occured during the update: {ex.Message}");
				}

				return new NoContentResult();
		   }

    }
}
