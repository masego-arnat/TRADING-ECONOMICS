using Microsoft.AspNetCore.Mvc.Rendering;

namespace BCity.Models
{
    public class LinkClientContactViewModel
    {
        public SelectList? Clients { get; set; }
        public SelectList? Contacts { get; set; }
        public int SelectedClientId { get; set; }
        public int SelectedContactId { get; set; }
    }
}
