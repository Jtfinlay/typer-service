using Microsoft.WindowsAzure.Storage.Table;

namespace TyperService.V1.Models
{
    public class Story : TableEntity
    {
        public string Phrase { get; set; }
    }
}
