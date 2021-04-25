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
    [Route("resume")]
    public class ResumeContoller : Controller
    {

        private Resume GetResumeByName(string name)
        {
            return ResumeRepository.Resumes.FirstOrDefault(resume => resume.basics.name == name);
        }

        private int GetResumeIndexByName(string name)
        {
            return ResumeRepository.Resumes.FindIndex(resume => resume.basics.name == name);
        }

        #region GET
        // GET: ResumeContoller
        [HttpGet]
        public ActionResult<List<Resume>> Get()
        {
            List<Resume> resumes = ResumeRepository.Resumes;
            return Ok(resumes);
        }

        [HttpGet("{name}")]
        public ActionResult<Resume> GetByName(string name)
        {
            Resume resume = GetResumeByName(name);

            if (resume == null)
                return NotFound("resume not found");

            return Ok(resume);
        }

        [HttpGet("{name}/basics")]
        public ActionResult<Basics> GetBasics(string name)
        {
            Resume resume = GetResumeByName(name);

            if (resume == null)
                return NotFound("resume not found");

            Basics basics = resume.basics;
            return Ok(basics);
        }

        [HttpGet("{name}/basics/location")]
        public ActionResult<Location> GetLocation(string name)
        {
            Resume resume = GetResumeByName(name);

            if (resume == null)
                return NotFound("resume not found");

            Location location = resume.basics.location;
            return Ok(location);
        }

        [HttpGet("{name}/basics/profiles")]
        public ActionResult<List<Profile>> GetProfiles(string name)
        {
            Resume resume = GetResumeByName(name);

            if (resume == null)
                return NotFound("resume not found");

            List<Profile> profiles = resume.basics.profiles;
            return Ok(profiles);
        }

        [HttpGet("{name}/basics/profile/{network}")]
        public ActionResult<Profile> GetProfile(string name, string network)
        {

            Resume resume = GetResumeByName(name);

            if (resume == null)
                return NotFound("resume not found");

            Profile profile = resume.basics.profiles.FirstOrDefault(profile => profile.network == network);

            if (profile == null)
                return NotFound("profile not found");

            return Ok(profile);
        }

        [HttpGet("{name}/work")]
        public ActionResult<List<Work>> GetWork(string name)
        {

            Resume resume = GetResumeByName(name);

            if (resume == null)
                return NotFound("resume not found");

            List<Work> works = resume.work;

            return Ok(works);
        }

        [HttpGet("{name}/work/{company}")]
        public ActionResult<Work> GetWork(string name, string company)
        {

            Resume resume = GetResumeByName(name);

            if (resume == null)
                return NotFound("resume not found");

            Work work = resume.work.FirstOrDefault(work => work.company == company);

            if(work == null)
                return NotFound("work not found");


            return Ok(work);
        }


        [HttpGet("{name}/volunteer")]
        public ActionResult<List<Volunteer>> GetVolunteer(string name)
        {

            Resume resume = GetResumeByName(name);

            if (resume == null)
                return NotFound("resume not found");

            List<Volunteer> volunteer = resume.volunteer;

            return Ok(volunteer);
        }

        [HttpGet("{name}/volunteer/{organization}")]
        public ActionResult<Volunteer> GetVolunteer(string name, string organization)
        {

            Resume resume = GetResumeByName(name);

            if (resume == null)
                return NotFound("resume not found");

            Volunteer volunteer = resume.volunteer.FirstOrDefault(volunteer => volunteer.organization == organization);

            if (volunteer == null)
                return NotFound("volunteer not found");

            return Ok(volunteer);
        }

        [HttpGet("{name}/education")]
        public ActionResult<List<Education>> GetEducation(string name)
        {

            Resume resume = GetResumeByName(name);

            if (resume == null)
                return NotFound("resume not found");

            List<Education> education = resume.education;

            return Ok(education);
        }

        [HttpGet("{name}/education/{institution}")]
        public ActionResult<Education> GetEducation(string name, string institution)
        {

            Resume resume = GetResumeByName(name);

            if (resume == null)
                return NotFound("resume not found");

            Education education = resume.education.FirstOrDefault(education => education.institution == institution);
            if (education == null)
                return NotFound("education not found");

            return Ok(education);
        }

        [HttpGet("{name}/awards")]
        public ActionResult<List<Award>> GetAward(string name)
        {

            Resume resume = GetResumeByName(name);

            if (resume == null)
                return NotFound("resume not found");

            List<Award> award = resume.awards;

            return Ok(award);
        }

        [HttpGet("{name}/awards/{title}")]
        public ActionResult<Award> GetAward(string name, string title)
        {

            Resume resume = GetResumeByName(name);

            if (resume == null)
                return NotFound("resume not found");

            Award award = resume.awards.FirstOrDefault(award => award.title == title);
            if (award == null)
                return NotFound("award not found");

            return Ok(award);
        }

        [HttpGet("{name}/publications")]
        public ActionResult<List<Publication>> GetPublication(string name)
        {

            Resume resume = GetResumeByName(name);

            if (resume == null)
                return NotFound("resume not found");

            List<Publication> publications = resume.publications;

            return Ok(publications);
        }

        [HttpGet("{name}/publications/{publicationName}")]
        public ActionResult<Publication> GetPublication(string name, string publicationName)
        {

            Resume resume = GetResumeByName(name);

            if (resume == null)
                return NotFound("resume not found");

            Publication publication = resume.publications.FirstOrDefault(publication => publication.name == publicationName);
            if (publication == null)
                return NotFound("publication not found");

            return Ok(publication);
        }

        [HttpGet("{name}/skills")]
        public ActionResult<List<Skill>> GetSkill(string name)
        {

            Resume resume = GetResumeByName(name);

            if (resume == null)
                return NotFound("resume not found");

            List<Skill> skills = resume.skills;

            return Ok(skills);
        }

        [HttpGet("{name}/skills/{skillName}")]
        public ActionResult<Skill> GetSkill(string name, string skillName)
        {

            Resume resume = GetResumeByName(name);

            if (resume == null)
                return NotFound("resume not found");

            Skill skill = resume.skills.FirstOrDefault(skill => skill.name == skillName);
            if (skill == null)
                return NotFound("skill not found");

            return Ok(skill);
        }

        [HttpGet("{name}/languages")]
        public ActionResult<List<Language>> GetLanguage(string name)
        {

            Resume resume = GetResumeByName(name);

            if (resume == null)
                return NotFound("resume not found");

            List<Language> languages = resume.languages;

            return Ok(languages);
        }

        [HttpGet("{name}/languages/{languageName}")]
        public ActionResult<Language> GetLanguage(string name, string languageName)
        {

            Resume resume = GetResumeByName(name);

            if (resume == null)
                return NotFound("resume not found");

            Language language = resume.languages.FirstOrDefault(item => item.language == languageName);
            if (language == null)
                return NotFound("language not found");

            return Ok(language);
        }

        [HttpGet("{name}/interests")]
        public ActionResult<List<Interest>> GetInterest(string name)
        {

            Resume resume = GetResumeByName(name);

            if (resume == null)
                return NotFound("resume not found");

            List<Interest> interests = resume.interests;

            return Ok(interests);
        }

        [HttpGet("{name}/interests/{interestName}")]
        public ActionResult<Interest> GetInterest(string name, string interestName)
        {

            Resume resume = GetResumeByName(name);

            if (resume == null)
                return NotFound("resume not found");

            Interest interest = resume.interests.FirstOrDefault(interest => interest.name == interestName);
            if (interest == null)
                return NotFound("interest not found");

            return Ok(interest);
        }

        [HttpGet("{name}/references")]
        public ActionResult<List<Reference>> GetReference(string name)
        {

            Resume resume = GetResumeByName(name);

            if (resume == null)
                return NotFound("resume not found");

            List<Reference> references = resume.references;

            return Ok(references);
        }

        [HttpGet("{name}/references/{referenceName}")]
        public ActionResult<Reference> GetReference(string name, string referenceName)
        {

            Resume resume = GetResumeByName(name);

            if (resume == null)
                return NotFound("resume not found");

            Reference reference = resume.references.FirstOrDefault(reference => reference.name == referenceName);
            if (reference == null)
                return NotFound("reference not found");

            return Ok(reference);
        }

        #endregion


        #region POST
        // POST: ResumeContoller
        [HttpPost]
        public ActionResult post([FromBody] Resume newResume)
        {
            ResumeRepository.Resumes.Add(newResume);

            if (ResumeRepository.Resumes.FirstOrDefault(resume => resume.basics.name == newResume.basics.name) != null)
                Conflict("Already exist");

            return StatusCode((int)HttpStatusCode.NoContent);
        }

        [HttpPost("{name}/basics/profiles")]
        public ActionResult postProfiel(string name, [FromBody] Profile newProfile)
        {
            Resume resume = GetResumeByName(name);

            if (resume.basics.profiles.FirstOrDefault(item => item.network == newProfile.network) != null)
                Conflict("Already exist");

            resume.basics.profiles.Add(newProfile);
            return NoContent();
        }

        [HttpPost("{name}/work")]
        public ActionResult postwork(string name, [FromBody] Work newWork)
        {
            Resume resume = GetResumeByName(name);

            if (resume.work.FirstOrDefault(item => item.company == newWork.company) != null)
                Conflict("Already exist");

            resume.work.Add(newWork);
            return NoContent();
        }

        [HttpPost("{name}/volunteer")]
        public ActionResult postVolunteer(string name, [FromBody] Volunteer newVolunteer)
        {
            Resume resume = GetResumeByName(name);

            if (resume.volunteer.FirstOrDefault(item => item.organization == newVolunteer.organization) != null)
                Conflict("Already exist");

            resume.volunteer.Add(newVolunteer);
            return NoContent();
        }

        [HttpPost("{name}/education")]
        public ActionResult postEducation(string name, [FromBody] Education newEducation)
        {
            Resume resume = GetResumeByName(name);


            if (resume.education.FirstOrDefault(item => item.institution == newEducation.institution) != null)
                Conflict("Already exist");

            resume.education.Add(newEducation);
            return NoContent();
        }

        [HttpPost("{name}/awards")]
        public ActionResult postAward(string name, [FromBody] Award newAward)
        {
            Resume resume = GetResumeByName(name);

            if (resume.awards.FirstOrDefault(item => item.title == newAward.title) != null)
                Conflict("Already exist");

            resume.awards.Add(newAward);
            return NoContent();
        }

        [HttpPost("{name}/publications")]
        public ActionResult postPublications(string name, [FromBody] Publication newPublication)
        {
            Resume resume = GetResumeByName(name);

            if (resume.publications.FirstOrDefault(item => item.name == newPublication.name) != null)
                Conflict("Already exist");

            resume.publications.Add(newPublication);
            return NoContent();
        }


        [HttpPost("{name}/skills")]
        public ActionResult postSkills(string name, [FromBody] Skill newSkill)
        {
            Resume resume = GetResumeByName(name);

            if (resume.skills.FirstOrDefault(item => item.name == newSkill.name) != null)
                Conflict("Already exist");

            resume.skills.Add(newSkill);
            return NoContent();
        }

        [HttpPost("{name}/languages")]
        public ActionResult postLanguages(string name, [FromBody] Language newLanguage)
        {
            Resume resume = GetResumeByName(name);

            if (resume.languages.FirstOrDefault(item => item.language == newLanguage.language) != null)
                Conflict("Already exist");

            resume.languages.Add(newLanguage);
            return NoContent();
        }

        [HttpPost("{name}/interests")]
        public ActionResult postInterests(string name, [FromBody] Interest newInterest)
        {
            Resume resume = GetResumeByName(name);

            if (resume.interests.FirstOrDefault(item => item.name == newInterest.name) != null)
                Conflict("Already exist");

            resume.interests.Add(newInterest);
            return NoContent();
        }

        [HttpPost("{name}/references")]
        public ActionResult postReferences(string name, [FromBody] Reference newReference)
        {
            Resume resume = GetResumeByName(name);

            if (resume.references.FirstOrDefault(item => item.name == newReference.name) != null)
                Conflict("Already exist");

            resume.references.Add(newReference);
            return NoContent();
        }


        #endregion

        // DELETE: ResumeContoller
        [HttpDelete("{name}")]
        public ActionResult delete(string name)
        {

            Resume resume = GetResumeByName(name);

            if (resume == null)
                return NotFound("resume not found");

            ResumeRepository.Resumes.Remove(resume);

            return StatusCode((int)HttpStatusCode.NoContent);
        }

        [HttpPut("{name}")]
        public ActionResult put(string name, [FromBody] Resume newResume)
        {

            int resumeIndex = GetResumeIndexByName(name);

            if (resumeIndex == -1)
                return NotFound("resume not found");

            ResumeRepository.Resumes[resumeIndex] = newResume;

            return StatusCode((int)HttpStatusCode.NoContent);
        }
    }
}
