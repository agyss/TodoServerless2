using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using FromBodyAttribute = Microsoft.Azure.Functions.Worker.Http.FromBodyAttribute;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Functions.Worker.Http;

namespace ServerlessTodoApi
{
    public class TodoItems
    {
        private readonly ILogger<TodoItems> _logger;

        public TodoItems(ILogger<TodoItems> logger)
        {
            _logger = logger;
        }

        [Function("TodoItemAdd")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "todoitem")] HttpRequest req, [FromBody] TodoItem newItem)
        {
            _logger.LogInformation("Upserting item: " + newItem.ItemName);
			if (string.IsNullOrEmpty(newItem.id))
			{
				// New Item so add ID and date
				_logger.LogInformation("Item is new.");
				newItem.id = Guid.NewGuid().ToString();
				newItem.ItemCreateDate = DateTime.Now;
				newItem.ItemOwner = "Test";
			}

            return new OkObjectResult(newItem);
        }
      
		// Get all items
		[Function("TodoItemGetAll")]
		public IActionResult GetAll(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "todoitem")]HttpRequest req)
        {
			var ret = new { UserName = "Test", Items = new TodoItem[]{
				new TodoItem() {
					id = "1",
					ItemCreateDate = DateTime.Now,
					ItemName = "Test"
				}
			}};

			return new OkObjectResult(ret);
		}
        
		// // Delete item by id
		// [Function("TodoItemDelete")]
		// public async Task<HttpResponseMessage> DeleteItem(
		//    [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "todoitem/{id}")]HttpRequestMessage req,
		//    [CosmosDB("ServerlessTodo", "TodoItems", ConnectionStringSetting = "CosmosDBConnectionString")] DocumentClient client, string id,
        //    ILogger log,
        //    ClaimsPrincipal principal)
		// {
		// 	var currentUser = GetCurrentUserName(log, principal);
		// 	log.LogInformation("Deleting document with ID " + id + " for user " + currentUser.UniqueName);

		// 	Uri documentUri = UriFactory.CreateDocumentUri("ServerlessTodo", "TodoItems", id);

		// 	try
		// 	{
		// 		// Verify the user owns the document and can delete it
    	// 		await client.DeleteDocumentAsync(documentUri, new RequestOptions() { PartitionKey = new PartitionKey(currentUser.UniqueName) });
		// 	}
		// 	catch (DocumentClientException ex)
		// 	{
		// 		if (ex.StatusCode == HttpStatusCode.NotFound)
		// 		{
		// 			// Document does not exist, is not owned by the current user, or was already deleted
		// 			log.LogInformation("Document with ID: " + id + " not found.");
		// 		}
		// 		else
		// 		{
		// 			// Something else happened
		// 			throw ex;
		// 		}
		// 	}

		// 	return req.CreateResponse(HttpStatusCode.NoContent);
		// }
    }
}
