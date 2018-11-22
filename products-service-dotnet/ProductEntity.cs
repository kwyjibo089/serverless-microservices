using System;
using Microsoft.WindowsAzure.Storage.Table;

public class ProductEntity : TableEntity
{
    public string Name { get; set; }
    public Guid Id { get; set; }
}