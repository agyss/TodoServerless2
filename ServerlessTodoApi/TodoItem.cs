using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Azure;
using Azure.Data.Tables;

namespace ServerlessTodoApi
{
    public class TableEntity : ITableEntity
{
    public string PartitionKey { get; set; }
    public string RowKey { get; set; }

    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; }
}
    public interface ITodoItem {

        public string Id {get; }
        public string ItemName {get; set;}
    }

    public class TodoItem  : TableEntity, ITodoItem
    {
        public TodoItem() {}

		public string ItemName { get; set; }
        public string Id { get ;  set; }

        public DateTime Created {get; set; }

        public ItemState itemstate {get;set;} = ItemState.Created;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum ItemState
        {
            [EnumMember(Value = "Created")]
            Created,
            [EnumMember(Value = "Deleted")]
            Deleted 
        }
    }
}
