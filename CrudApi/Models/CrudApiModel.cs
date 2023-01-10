using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace CrudApi.Models
{

    public class CrudApiModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string EmployeeName { get; set; }
        public string BirthDate { get; set; }

        public string Gender { get; set; }

        public long PassportNo { get; set; }

        public string Mobilenumber { get; set; }

        public string PresentAddress { get; set; }

        public string Photo { get; set; }

        public string Signature { get; set; }

        public string CandidatePhoto { get; set; }

        public string CandidateSignature { get; set; }

        public string FatherName { get; set; }

        public string BloodGroup { get; set; }

        public string MaritalStatus { get; set; }
        public long AadharNumber { get; set; }

        public string Status { get; set; }
        public string CardNo { get; set; }

        public string Role { get; set; }

        public string Email { get; set; }

        public string PermanentAaddress { get; set; }

        public long BankAccountNo { get; set; }

        public string AccountHolderName { get; set; }

        public string PanNo { get; set; }

        public string BankName { get; set; }

        public string IFCSCode { get; set; }

        public string BankAddress { get; set; }

        public double salary { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string Team { get; set; }

    }

}
