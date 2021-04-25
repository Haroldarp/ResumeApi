using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResumeApi.Models;
using ResumeApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ResumeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ResumeContoller : Controller
    {

        private Resume GetResumeByName(string name)
        {
            return ResumeRepository.Resumes.FirstOrDefault(resume => resume.basics.name == name);
        }
        // GET: ResumeContoller
        [HttpGet]
        public ActionResult<List<Resume>> Get()
        {
            List<Resume> resumes = ResumeRepository.Resumes;
            return Ok(resumes);
        }



        // POST: ResumeContoller
        [HttpPost]
        public ActionResult post([FromBody] Resume newResume)
        {
            ResumeRepository.Resumes.Add(newResume);

            return StatusCode((int)HttpStatusCode.NoContent);
        }

        // DELETE: ResumeContoller
        [HttpDelete("{name}")]
        public ActionResult delete(string name)
        {

            Resume resume = GetResumeByName(name);

            if (resume == null)
                return NotFound();

            ResumeRepository.Resumes.Remove(resume);

            return StatusCode((int)HttpStatusCode.NoContent);
        }

        [HttpPut("{name}")]
        public ActionResult put(string name, [FromBody] Resume newResume)
        {

            Resume resume = GetResumeByName(name);

            if (resume == null)
                return NotFound();

            ResumeRepository.Resumes.Remove(resume);
            ResumeRepository.Resumes.Add(newResume);

            return StatusCode((int)HttpStatusCode.NoContent);
        }
    }
}
