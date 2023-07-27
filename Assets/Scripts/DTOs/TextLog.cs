using Microsoft.WindowsAzure.Storage.Table;

public class TextLog : TableEntity
{
    public string Name { get; set; }
    public string Description { get; set; }

    public TextLog() { }

    public TextLog(string name)
    {
        Name = name;
        RowKey = name;
    }
}
