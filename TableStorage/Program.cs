using Azure;
using Azure.Data.Tables;
using Microsoft.VisualBasic;

string connectionString = "DefaultEndpointsProtocol=https;AccountName=qdappstore;AccountKey=rLatxMbQZlxq71IBpc0byfktQ5V2BBuKq/j2+VknI5B9iVOO1FlHGZGt0jEj+21PvM2S8iWcNWmk+AStALXoRg==;EndpointSuffix=core.windows.net";
string tableName = "Orders";

//AddEntity("O1", "Mobile", 100);
//AddEntity("O2", "Laptop", 50);
//AddEntity("O3", "Desktop", 70);
//AddEntity("O4", "Laptop", 200);

//QueryEntity("Laptop");

//DeleteEntity("Laptop", "O4");
//DeleteEntity("O4", "Laptop");
Update("O4", "Laptop", 600);

void AddEntity(string orderId,string category,int quantity)
{
    TableClient tableClient = new TableClient(connectionString, tableName);

    TableEntity keyValuePairs = new TableEntity(category, orderId)
    {
        { "Quantity",quantity }
    };

    tableClient.AddEntity(keyValuePairs);

    Console.WriteLine("Add Entity with Order ID {0}, {1}, {2}",orderId, category, quantity);
}

void QueryEntity(string category)
{
    TableClient tableClient = new TableClient(connectionString,tableName);

    Pageable<TableEntity> result = tableClient.Query<TableEntity>(entity=>entity.PartitionKey==category);

    foreach (var item in result)
    {
        Console.WriteLine("Order Id: {0}", item.RowKey);
        Console.WriteLine("Quantity: {0}", item.GetInt32("Quantity"));
    }
}

void DeleteEntity(string category,string orderId)
{
    TableClient tableClient = new TableClient(connectionString,tableName);

    tableClient.DeleteEntity(category, orderId);
    
    QueryEntity(category);
}

void Update(string category,string orderID, int quantity)
{
    TableClient tableClient = new TableClient(connectionString,tableName);

    TableEntity tableEntity = new TableEntity(orderID, category) {
        {"Quantity",quantity } };

    tableClient.UpsertEntity(tableEntity);

    Console.WriteLine("Entity with Partition Key {0} and Row Key {1} Updated", category, orderID);
}