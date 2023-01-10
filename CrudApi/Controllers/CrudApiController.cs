using CrudApi.Models;
using CrudApi.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;


namespace CrudApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrudApiController : ControllerBase
    {
        private readonly CrudApiService _crudApiService;
        private readonly TeamService _Teamservice;


        public CrudApiController(CrudApiService crudApiService ,TeamService Teamservice)
        {
            _crudApiService = crudApiService;
            _Teamservice = Teamservice;
        }

        [HttpGet]
        public async Task<List<CrudApiModel>> Get() =>
            await _crudApiService.GetAsync();


        [HttpGet("Team")]
        public async Task<List<IdNameModel>> GetTeam() =>
            await _Teamservice.GetKAsync();
        

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<CrudApiModel>> Get(string id)
        {
            var book = await _crudApiService.GetAsync(id);

            if (book is null)
            {
                return NotFound();
            }

            return book;
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromForm] Picture newBook)
        {

            newBook.Id = ObjectId.GenerateNewId().ToString();
            try
            {
                string imgpath = Path.Combine(Directory.GetCurrentDirectory(), "uploadimage", newBook.Id);
                string imgext = Path.GetExtension(newBook.CandidatePhoto.FileName);
                imgpath = imgpath + imgext;

                string fileName = newBook.Id + imgext;

                using (Stream vc = new FileStream(imgpath, FileMode.Create))
                {
                    newBook.CandidatePhoto.CopyTo(vc);
                }


                string signpath = Path.Combine(Directory.GetCurrentDirectory(), "signatureimages", newBook.Id);
                string signext = Path.GetExtension(newBook.CandidateSignature.FileName);
                signpath = signpath + signext;

                string signfileName = newBook.Id + signext;

                using (Stream vc = new FileStream(signpath, FileMode.Create))
                {
                    newBook.CandidateSignature.CopyTo(vc);
                }

              CrudApiModel upload = new CrudApiModel();
                upload.Id = newBook.Id;
                upload.EmployeeName = newBook.EmployeeName;
                upload.BirthDate = newBook.BirthDate;
                upload.Gender = newBook.Gender;
                upload.PassportNo = newBook.PassportNo;
                upload.Mobilenumber = newBook.Mobilenumber;
                upload.PresentAddress = newBook.PresentAddress;
                upload.CandidatePhoto = imgpath;
                upload.FatherName = newBook.FatherName;
                upload.BloodGroup = newBook.BloodGroup;
                upload.MaritalStatus = newBook.MaritalStatus;
                upload.AadharNumber = newBook.AadharNumber;
                upload.Status = newBook.Status;
                upload.CardNo = newBook.CardNo;
                upload.Role = newBook.Role;
                upload.Email = newBook.Email;
                upload.PermanentAaddress = newBook.PermanentAaddress;
                upload.CandidateSignature = signpath;
                upload.BankAccountNo = newBook. BankAccountNo;
                upload.AccountHolderName = newBook.AccountHolderName;
                upload.PanNo = newBook.PanNo;
                upload.BankName =newBook.BankName;
                upload.IFCSCode = newBook.IFCSCode;
                upload.BankAddress = newBook.BankAddress;
                upload.Team = newBook.Team;
                upload.Photo = fileName;
                upload.Signature = signfileName;
                await _crudApiService.CreateAsync(upload);
                return StatusCode(StatusCodes.Status201Created);

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id,[FromForm] Picture newBook)
        {
            var book = await _crudApiService.GetAsync(id);

             if (book is null)
             {
                 return NotFound();
             }

            newBook.Id = ObjectId.GenerateNewId().ToString();
            try
            {
                string imgpath = Path.Combine(Directory.GetCurrentDirectory(), "uploadimage", newBook.Id);
                string imgext = Path.GetExtension(newBook.CandidatePhoto.FileName);
                imgpath = imgpath + imgext;

                string fileName =  newBook.Id + imgext;

                using (Stream vc = new FileStream(imgpath, FileMode.Create))
                {
                    newBook.CandidatePhoto.CopyTo(vc);
                }


                string signpath = Path.Combine(Directory.GetCurrentDirectory(), "signatureimages", newBook.Id);
                string signext = Path.GetExtension(newBook.CandidateSignature.FileName);
                signpath = signpath + signext;

                string signfileName = newBook.Id + signext;

                using (Stream vc = new FileStream(signpath, FileMode.Create))
                {
                    newBook.CandidateSignature.CopyTo(vc);
                }


                CrudApiModel upload = new CrudApiModel();



                upload.Id = book.Id;
                upload.EmployeeName = newBook.EmployeeName;
                upload.BirthDate = newBook.BirthDate;
                upload.Gender = newBook.Gender;
                upload.PassportNo = newBook.PassportNo;
                upload.Mobilenumber = newBook.Mobilenumber;
                upload.PresentAddress = newBook.PresentAddress;
                upload.CandidatePhoto = imgpath;
                upload.FatherName = newBook.FatherName;
                upload.BloodGroup = newBook.BloodGroup;
                upload.MaritalStatus = newBook.MaritalStatus;
                upload.AadharNumber = newBook.AadharNumber;
                upload.Status = newBook.Status;
                upload.CardNo = newBook.CardNo;
                upload.Role = newBook.Role;
                upload.Email = newBook.Email;
                upload.PermanentAaddress = newBook.PermanentAaddress;
                upload.CandidateSignature = signpath;
                upload.BankAccountNo = newBook.BankAccountNo;
                upload.AccountHolderName = newBook.AccountHolderName;
                upload.PanNo = newBook.PanNo;
                upload.BankName = newBook.BankName;
                upload.IFCSCode = newBook.IFCSCode;
                upload.BankAddress = newBook.BankAddress;
                upload.Team = newBook.Team;
                upload.Photo = fileName;
                upload.Signature = signfileName;
               

                await _crudApiService.UpdateAsync(id, upload);
                return StatusCode(StatusCodes.Status201Created);

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }


        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var book = await _crudApiService.GetAsync(id);

            if (book is null)
            {
                return NotFound();
            }

            await _crudApiService.RemoveAsync(id);

            return NoContent();
        }

        

    }

}

