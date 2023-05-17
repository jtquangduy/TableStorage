using Azure.Data.Tables;

string connectionString = "DefaultEndpointsProtocol=https;AccountName=qdappstore;AccountKey=rLatxMbQZlxq71IBpc0byfktQ5V2BBuKq/j2+VknI5B9iVOO1FlHGZGt0jEj+21PvM2S8iWcNWmk+AStALXoRg==;EndpointSuffix=core.windows.net";
string tableName = "Orders";

AddEntity("O1", "Mobile", 100);
AddEntity("O2", "Laptop", 50);
AddEntity("O3", "Desktop", 70);
AddEntity("O4", "Laptop", 200);

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