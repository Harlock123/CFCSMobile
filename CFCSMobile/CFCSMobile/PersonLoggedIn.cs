using System;
namespace CFCSMobile
{
    public class PersonLoggedIn
    {
        public bool Success { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        public PersonLoggedIn()
        {
            Success = false;
        }
    }
}
