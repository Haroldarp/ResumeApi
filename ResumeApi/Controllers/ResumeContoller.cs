using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ResumeApi.Models;
using ResumeApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
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

        private string GenerateEtag(string model)
        {
            string randomNumber = new Random().Next(1, 10000).ToString();
            string key = model + randomNumber;
            byte[] data = Encoding.UTF8.GetBytes(key);
            using (var md5 = MD5.Create())
            {
                var hash = md5.ComputeHash(data);
                string hex = BitConverter.ToString(hash);
                return hex.Replace("-", "");
            }
        }

        private void SetEtag(Resume resume)
        {
            string etag = GenerateEtag(JsonConvert.SerializeObject(resume));
            if(ResumeRepository.etags.ContainsKey(resume.basics.name.Replace(" ", String.Empty)))
            {
                ResumeRepository.etags[resume.basics.name.Replace(" ", String.Empty)] = etag;

            }
            else
            {
                ResumeRepository.etags.Add(resume.basics.name.Replace(" ", String.Empty), etag);
            }

            string etagAll = GenerateEtag(JsonConvert.SerializeObject(ResumeRepository.Resumes));
            ResumeRepository.etag = etagAll;
        }

        #region GET

        [ETagFilter]
        [HttpGet]
        public ActionResult<List<Resume>> Get()
        {
            List<Resume> resumes = ResumeRepository.Resumes;
            return Ok(resumes);
        }

        [ETagFilter]
        [HttpGet("{name}")]
        public ActionResult<Resume> GetByName(string name)
        {
            Resume resume = GetResumeByName(name);

            if (resume == null)
                return NotFound("resume not found");

            return Ok(resume);
        }

        [ETagFilter]
        [HttpGet("{name}/basics")]
        public ActionResult<Basics> GetBasics(string name)
        {
            Resume resume = GetResumeByName(name);

            if (resume == null)
                return NotFound("resume not found");

            Basics basics = resume.basics;
            return Ok(basics);
        }

        [ETagFilter]
        [HttpGet("{name}/basics/location")]
        public ActionResult<Location> GetLocation(string name)
        {
            Resume resume = GetResumeByName(name);

            if (resume == null)
                return NotFound("resume not found");

            Location location = resume.basics.location;
            return Ok(location);
        }

        [ETagFilter]
        [HttpGet("{name}/basics/profiles")]
        public ActionResult<List<Profile>> GetProfiles(string name)
        {
            Resume resume = GetResumeByName(name);

            if (resume == null)
                return NotFound("resume not found");

            List<Profile> profiles = resume.basics.profiles;
            return Ok(profiles);
        }

        [ETagFilter]
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

        [ETagFilter]
        [HttpGet("{name}/work")]
        public ActionResult<List<Work>> GetWork(string name)
        {

            Resume resume = GetResumeByName(name);

            if (resume == null)
                return NotFound("resume not found");

            List<Work> works = resume.work;

            return Ok(works);
        }

        [ETagFilter]
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

        [ETagFilter]
        [HttpGet("{name}/volunteer")]
        public ActionResult<List<Volunteer>> GetVolunteer(string name)
        {

            Resume resume = GetResumeByName(name);

            if (resume == null)
                return NotFound("resume not found");

            List<Volunteer> volunteer = resume.volunteer;

            return Ok(volunteer);
        }

        [ETagFilter]
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

        [ETagFilter]
        [HttpGet("{name}/education")]
        public ActionResult<List<Education>> GetEducation(string name)
        {

            Resume resume = GetResumeByName(name);

            if (resume == null)
                return NotFound("resume not found");

            List<Education> education = resume.education;

            return Ok(education);
        }

        [ETagFilter]
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

        [ETagFilter]
        [HttpGet("{name}/awards")]
        public ActionResult<List<Award>> GetAward(string name)
        {

            Resume resume = GetResumeByName(name);

            if (resume == null)
                return NotFound("resume not found");

            List<Award> award = resume.awards;

            return Ok(award);
        }

        [ETagFilter]
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

        [ETagFilter]
        [HttpGet("{name}/publications")]
        public ActionResult<List<Publication>> GetPublication(string name)
        {

            Resume resume = GetResumeByName(name);

            if (resume == null)
                return NotFound("resume not found");

            List<Publication> publications = resume.publications;

            return Ok(publications);
        }

        [ETagFilter]
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

        [ETagFilter]
        [HttpGet("{name}/skills")]
        public ActionResult<List<Skill>> GetSkill(string name)
        {

            Resume resume = GetResumeByName(name);

            if (resume == null)
                return NotFound("resume not found");

            List<Skill> skills = resume.skills;

            return Ok(skills);
        }

        [ETagFilter]
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

        [ETagFilter]
        [HttpGet("{name}/languages")]
        public ActionResult<List<Language>> GetLanguage(string name)
        {

            Resume resume = GetResumeByName(name);

            if (resume == null)
                return NotFound("resume not found");

            List<Language> languages = resume.languages;

            return Ok(languages);
        }

        [ETagFilter]
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

        [ETagFilter]
        [HttpGet("{name}/interests")]
        public ActionResult<List<Interest>> GetInterest(string name)
        {

            Resume resume = GetResumeByName(name);

            if (resume == null)
                return NotFound("resume not found");

            List<Interest> interests = resume.interests;

            return Ok(interests);
        }

        [ETagFilter]
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

        [ETagFilter]
        [HttpGet("{name}/references")]
        public ActionResult<List<Reference>> GetReference(string name)
        {

            Resume resume = GetResumeByName(name);

            if (resume == null)
                return NotFound("resume not found");

            List<Reference> references = resume.references;

            return Ok(references);
        }

        [ETagFilter]
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

        [BasicAuthentication]
        [HttpPost]
        public ActionResult post([FromBody] Resume newResume)
        {
            if (ResumeRepository.Resumes.FirstOrDefault(resume => resume.basics.name == newResume.basics.name) != null)
                return Conflict("Already exist");

            ResumeRepository.Resumes.Add(newResume);

            SetEtag(newResume);
            return StatusCode((int)HttpStatusCode.NoContent);
        }

        [BasicAuthentication]
        [HttpPost("{name}/basics/profiles")]
        public ActionResult postProfiel(string name, [FromBody] Profile newProfile)
        {
            Resume resume = GetResumeByName(name);

            if (resume.basics.profiles.FirstOrDefault(item => item.network == newProfile.network) != null)
                return Conflict("Already exist");

            resume.basics.profiles.Add(newProfile);
            SetEtag(resume);
            return NoContent();
        }

        [BasicAuthentication]
        [HttpPost("{name}/work")]
        public ActionResult postwork(string name, [FromBody] Work newWork)
        {
            Resume resume = GetResumeByName(name);

            if (resume.work.FirstOrDefault(item => item.company == newWork.company) != null)
                return Conflict("Already exist");

            resume.work.Add(newWork);
            SetEtag(resume);
            return NoContent();
        }

        [BasicAuthentication]
        [HttpPost("{name}/volunteer")]
        public ActionResult postVolunteer(string name, [FromBody] Volunteer newVolunteer)
        {
            Resume resume = GetResumeByName(name);

            if (resume.volunteer.FirstOrDefault(item => item.organization == newVolunteer.organization) != null)
                return Conflict("Already exist");

            resume.volunteer.Add(newVolunteer);
            SetEtag(resume);
            return NoContent();
        }

        [HttpPost("{name}/education")]
        public ActionResult postEducation(string name, [FromBody] Education newEducation)
        {
            Resume resume = GetResumeByName(name);


            if (resume.education.FirstOrDefault(item => item.institution == newEducation.institution) != null)
                return Conflict("Already exist");

            resume.education.Add(newEducation);
            SetEtag(resume);
            return NoContent();
        }

        [BasicAuthentication]
        [HttpPost("{name}/awards")]
        public ActionResult postAward(string name, [FromBody] Award newAward)
        {
            Resume resume = GetResumeByName(name);

            if (resume.awards.FirstOrDefault(item => item.title == newAward.title) != null)
                return  Conflict("Already exist");

            resume.awards.Add(newAward);
            SetEtag(resume);
            return NoContent();
        }

        [HttpPost("{name}/publications")]
        public ActionResult postPublications(string name, [FromBody] Publication newPublication)
        {
            Resume resume = GetResumeByName(name);

            if (resume.publications.FirstOrDefault(item => item.name == newPublication.name) != null)
                return Conflict("Already exist");

            resume.publications.Add(newPublication);
            SetEtag(resume);
            return NoContent();
        }

        [BasicAuthentication]
        [HttpPost("{name}/skills")]
        public ActionResult postSkills(string name, [FromBody] Skill newSkill)
        {
            Resume resume = GetResumeByName(name);

            if (resume.skills.FirstOrDefault(item => item.name == newSkill.name) != null)
                return Conflict("Already exist");

            resume.skills.Add(newSkill);
            SetEtag(resume);
            return NoContent();
        }

        [BasicAuthentication]
        [HttpPost("{name}/languages")]
        public ActionResult postLanguages(string name, [FromBody] Language newLanguage)
        {
            Resume resume = GetResumeByName(name);

            if (resume.languages.FirstOrDefault(item => item.language == newLanguage.language) != null)
                return Conflict("Already exist");

            resume.languages.Add(newLanguage);
            SetEtag(resume);
            return NoContent();
        }

        [HttpPost("{name}/interests")]
        public ActionResult postInterests(string name, [FromBody] Interest newInterest)
        {
            Resume resume = GetResumeByName(name);

            if (resume.interests.FirstOrDefault(item => item.name == newInterest.name) != null)
                return Conflict("Already exist");

            resume.interests.Add(newInterest);
            SetEtag(resume);
            return NoContent();
        }

        [BasicAuthentication]
        [HttpPost("{name}/references")]
        public ActionResult postReferences(string name, [FromBody] Reference newReference)
        {
            Resume resume = GetResumeByName(name);

            if (resume.references.FirstOrDefault(item => item.name == newReference.name) != null)
                return Conflict("Already exist");

            resume.references.Add(newReference);
            SetEtag(resume);
            return NoContent();
        }


        #endregion


        #region DELETE

        [BasicAuthentication]
        [HttpDelete("{name}")]
        public ActionResult delete(string name)
        {

            Resume resume = GetResumeByName(name);

            if (resume == null)
                return NotFound("resume not found");

            ResumeRepository.Resumes.Remove(resume);
            ResumeRepository.etags.Remove(resume.basics.name.Replace(" ", String.Empty));
            string etagAll = GenerateEtag(JsonConvert.SerializeObject(ResumeRepository.Resumes));
            ResumeRepository.etag = etagAll;

            return StatusCode((int)HttpStatusCode.NoContent);
        }

        [BasicAuthentication]
        [HttpDelete("{name}/basics/profiles/{network}")]
        public ActionResult DeleteProfiles(string name, string network)
        {

            Resume resume = GetResumeByName(name);

            if (resume == null)
                return NotFound("resume not found");

            Profile profile = resume.basics.profiles.FirstOrDefault(profile => profile.network == network);

            if (profile == null)
                return NotFound("profile not found");

            resume.basics.profiles.Remove(profile);
            SetEtag(resume);
            return NoContent();
        }

        [BasicAuthentication]
        [HttpDelete("{name}/work/{company}")]
        public ActionResult<Work> DeleteWork(string name, string company)
        {

            Resume resume = GetResumeByName(name);

            if (resume == null)
                return NotFound("resume not found");

            Work work = resume.work.FirstOrDefault(work => work.company == company);

            if (work == null)
                return NotFound("work not found");

            resume.work.Remove(work);
            SetEtag(resume);
            return NoContent();
        }

        [BasicAuthentication]
        [HttpDelete("{name}/volunteer/{organization}")]
        public ActionResult<Volunteer> DeleteVolunteer(string name, string organization)
        {

            Resume resume = GetResumeByName(name);

            if (resume == null)
                return NotFound("resume not found");

            Volunteer volunteer = resume.volunteer.FirstOrDefault(volunteer => volunteer.organization == organization);

            if (volunteer == null)
                return NotFound("volunteer not found");

            resume.volunteer.Remove(volunteer);
            SetEtag(resume);
            return NoContent();

        }

        [BasicAuthentication]
        [HttpDelete("{name}/education/{institution}")]
        public ActionResult<Education> DeleteEducation(string name, string institution)
        {

            Resume resume = GetResumeByName(name);

            if (resume == null)
                return NotFound("resume not found");

            Education education = resume.education.FirstOrDefault(education => education.institution == institution);
            if (education == null)
                return NotFound("education not found");

            resume.education.Remove(education);
            SetEtag(resume);
            return NoContent();
        }

        [BasicAuthentication]
        [HttpDelete("{name}/awards/{title}")]
        public ActionResult<Award> DeleteAward(string name, string title)
        {

            Resume resume = GetResumeByName(name);

            if (resume == null)
                return NotFound("resume not found");

            Award award = resume.awards.FirstOrDefault(award => award.title == title);
            if (award == null)
                return NotFound("award not found");

            resume.awards.Remove(award);
            SetEtag(resume);
            return NoContent();
        }

        [BasicAuthentication]
        [HttpDelete("{name}/publications/{publicationName}")]
        public ActionResult<Publication> DeletePublication(string name, string publicationName)
        {

            Resume resume = GetResumeByName(name);

            if (resume == null)
                return NotFound("resume not found");

            Publication publication = resume.publications.FirstOrDefault(publication => publication.name == publicationName);
            if (publication == null)
                return NotFound("publication not found");

            resume.publications.Remove(publication);
            SetEtag(resume);
            return NoContent();
        }

        [BasicAuthentication]
        [HttpDelete("{name}/skills/{skillName}")]
        public ActionResult<Skill> DeleteSkill(string name, string skillName)
        {

            Resume resume = GetResumeByName(name);

            if (resume == null)
                return NotFound("resume not found");

            Skill skill = resume.skills.FirstOrDefault(skill => skill.name == skillName);
            if (skill == null)
                return NotFound("skill not found");

            resume.skills.Remove(skill);
            SetEtag(resume);
            return NoContent();
        }

        [BasicAuthentication]
        [HttpDelete("{name}/languages/{languageName}")]
        public ActionResult<Language> DeleteLanguage(string name, string languageName)
        {

            Resume resume = GetResumeByName(name);

            if (resume == null)
                return NotFound("resume not found");

            Language language = resume.languages.FirstOrDefault(item => item.language == languageName);
            if (language == null)
                return NotFound("language not found");

            resume.languages.Remove(language);
            SetEtag(resume);
            return NoContent();
        }

        [BasicAuthentication]
        [HttpDelete("{name}/references/{referenceName}")]
        public ActionResult<Reference> DeleteReference(string name, string referenceName)
        {

            Resume resume = GetResumeByName(name);

            if (resume == null)
                return NotFound("resume not found");

            Reference reference = resume.references.FirstOrDefault(reference => reference.name == referenceName);
            if (reference == null)
                return NotFound("reference not found");

            resume.references.Remove(reference);
            SetEtag(resume);
            return NoContent();
        }


        #endregion


        #region PUT

        [ETagFilter]
        [BasicAuthentication]
        [HttpPut("{name}")]
        public ActionResult put(string name, [FromBody] Resume newResume)
        {

            int resumeIndex = GetResumeIndexByName(name);


            if (resumeIndex == -1)
                return NotFound("resume not found");

            string nameBefore = ResumeRepository.Resumes[resumeIndex].basics.name;
            ResumeRepository.Resumes[resumeIndex] = newResume;

            if (nameBefore != newResume.basics.name)
                ResumeRepository.etags.Remove(nameBefore.Replace(" ", String.Empty));

            SetEtag(ResumeRepository.Resumes[resumeIndex]);
            return StatusCode((int)HttpStatusCode.NoContent);
        }

        [ETagFilter]
        [BasicAuthentication]
        [HttpPut("{name}/basics")]
        public ActionResult PutBasic(string name, [FromBody] Basics newBasic)
        {

            int resumeIndex = GetResumeIndexByName(name);

            if (resumeIndex == -1)
                return NotFound("resume not found");

            ResumeRepository.Resumes[resumeIndex].basics = newBasic;
            SetEtag(ResumeRepository.Resumes[resumeIndex]);
            return NoContent();

        }

        [ETagFilter]
        [BasicAuthentication]
        [HttpPut("{name}/basics/location")]
        public ActionResult PutLocation(string name, [FromBody] Location newLocation)
        {

            int resumeIndex = GetResumeIndexByName(name);

            if (resumeIndex == -1)
                return NotFound("resume not found");

            ResumeRepository.Resumes[resumeIndex].basics.location = newLocation;
            SetEtag(ResumeRepository.Resumes[resumeIndex]);
            return NoContent();
        }

        [ETagFilter]
        [BasicAuthentication]
        [HttpPut("{name}/basics/profiles/{network}")]
        public ActionResult PutLocation(string name, string network, [FromBody] Profile newProfile)
        {

            int resumeIndex = GetResumeIndexByName(name);

            if (resumeIndex == -1)
                return NotFound("resume not found");

            int profileIndex = ResumeRepository.Resumes[resumeIndex].basics.profiles.FindIndex(profile => profile.network == network);

            if (profileIndex == -1)
                return NotFound("profile not found");

            ResumeRepository.Resumes[resumeIndex].basics.profiles[profileIndex] = newProfile;
            SetEtag(ResumeRepository.Resumes[resumeIndex]);
            return NoContent();
        }

        [ETagFilter]
        [BasicAuthentication]
        [HttpPut("{name}/work/{company}")]
        public ActionResult PutWork(string name, string company, [FromBody] Work newWork)
        {

            int resumeIndex = GetResumeIndexByName(name);

            if (resumeIndex == -1)
                return NotFound("resume not found");

            int workIndex = ResumeRepository.Resumes[resumeIndex].work.FindIndex(work => work.company == company);

            if (workIndex == -1)
                return NotFound("work not found");

            ResumeRepository.Resumes[resumeIndex].work[workIndex] = newWork;
            SetEtag(ResumeRepository.Resumes[resumeIndex]);
            return NoContent();
        }

        [ETagFilter]
        [BasicAuthentication]
        [HttpPut("{name}/volunteer/{organization}")]
        public ActionResult PutVolunteer(string name, string organization, [FromBody] Volunteer newVolunteer)
        {

            int resumeIndex = GetResumeIndexByName(name);

            if (resumeIndex == -1)
                return NotFound("resume not found");

            int volunteerIndex = ResumeRepository.Resumes[resumeIndex].volunteer.FindIndex(volunteer => volunteer.organization == organization);

            if (volunteerIndex == -1)
                return NotFound("volunteer not found");

            ResumeRepository.Resumes[resumeIndex].volunteer[volunteerIndex] = newVolunteer;
            SetEtag(ResumeRepository.Resumes[resumeIndex]);
            return NoContent();
        }

        [ETagFilter]
        [BasicAuthentication]
        [HttpPut("{name}/education/{institution}")]
        public ActionResult PutEducation(string name, string institution, [FromBody] Education newEducation)
        {

            int resumeIndex = GetResumeIndexByName(name);

            if (resumeIndex == -1)
                return NotFound("resume not found");

            int educationIndex = ResumeRepository.Resumes[resumeIndex].education.FindIndex(education => education.institution == institution);

            if (educationIndex == -1)
                return NotFound("education not found");

            ResumeRepository.Resumes[resumeIndex].education[educationIndex] = newEducation;
            SetEtag(ResumeRepository.Resumes[resumeIndex]);
            return NoContent();
        }

        [ETagFilter]
        [BasicAuthentication]
        [HttpPut("{name}/awards/{title}")]
        public ActionResult PutAwards(string name, string title, [FromBody] Award newAward)
        {

            int resumeIndex = GetResumeIndexByName(name);

            if (resumeIndex == -1)
                return NotFound("resume not found");

            int awardIndex = ResumeRepository.Resumes[resumeIndex].awards.FindIndex(award => award.title == title);

            if (awardIndex == -1)
                return NotFound("award not found");

            ResumeRepository.Resumes[resumeIndex].awards[awardIndex] = newAward;
            SetEtag(ResumeRepository.Resumes[resumeIndex]);
            return NoContent();
        }

        [ETagFilter]
        [BasicAuthentication]
        [HttpPut("{name}/publications/{publicationName}")]
        public ActionResult PutPublications(string name, string publicationName, [FromBody] Publication newPublication)
        {

            int resumeIndex = GetResumeIndexByName(name);

            if (resumeIndex == -1)
                return NotFound("resume not found");

            int publicationIndex = ResumeRepository.Resumes[resumeIndex].publications.FindIndex(publication => publication.name == publicationName);

            if (publicationIndex == -1)
                return NotFound("publication not found");

            ResumeRepository.Resumes[resumeIndex].publications[publicationIndex] = newPublication;
            SetEtag(ResumeRepository.Resumes[resumeIndex]);
            return NoContent();
        }

        [ETagFilter]
        [BasicAuthentication]
        [HttpPut("{name}/skills/{skillName}")]
        public ActionResult PutSkill(string name, string skillName, [FromBody] Skill newSkill)
        {

            int resumeIndex = GetResumeIndexByName(name);

            if (resumeIndex == -1)
                return NotFound("resume not found");

            int skillIndex = ResumeRepository.Resumes[resumeIndex].skills.FindIndex(skill => skill.name == skillName);

            if (skillIndex == -1)
                return NotFound("skill not found");

            ResumeRepository.Resumes[resumeIndex].skills[skillIndex] = newSkill;
            SetEtag(ResumeRepository.Resumes[resumeIndex]);
            return NoContent();
        }

        [ETagFilter]
        [BasicAuthentication]
        [HttpPut("{name}/languages/{language}")]
        public ActionResult PutSkill(string name, string language, [FromBody] Language newLanguage)
        {

            int resumeIndex = GetResumeIndexByName(name);

            if (resumeIndex == -1)
                return NotFound("resume not found");

            int languageIndex = ResumeRepository.Resumes[resumeIndex].languages.FindIndex(item => item.language == language);

            if (languageIndex == -1)
                return NotFound("language not found");

            ResumeRepository.Resumes[resumeIndex].languages[languageIndex] = newLanguage;
            SetEtag(ResumeRepository.Resumes[resumeIndex]);
            return NoContent();
        }

        [ETagFilter]
        [BasicAuthentication]
        [HttpPut("{name}/interests/{interestName}")]
        public ActionResult PutInterest(string name, string interestName, [FromBody] Interest newInteres)
        {

            int resumeIndex = GetResumeIndexByName(name);

            if (resumeIndex == -1)
                return NotFound("resume not found");

            int interesIndex = ResumeRepository.Resumes[resumeIndex].interests.FindIndex(interest => interest.name == interestName);

            if (interesIndex == -1)
                return NotFound("interes not found");

            ResumeRepository.Resumes[resumeIndex].interests[interesIndex] = newInteres;
            SetEtag(ResumeRepository.Resumes[resumeIndex]);
            return NoContent();
        }

        [ETagFilter]
        [BasicAuthentication]
        [HttpPut("{name}/references/{referenceName}")]
        public ActionResult PutReference(string name, string referenceName, [FromBody] Reference newReference)
        {

            int resumeIndex = GetResumeIndexByName(name);

            if (resumeIndex == -1)
                return NotFound("resume not found");

            int referenceIndex = ResumeRepository.Resumes[resumeIndex].references.FindIndex(reference => reference.name == referenceName);

            if (referenceIndex == -1)
                return NotFound("interes not found");

            ResumeRepository.Resumes[resumeIndex].references[referenceIndex] = newReference;
            SetEtag(ResumeRepository.Resumes[resumeIndex]);
            return NoContent();
        }
    }
    #endregion
}
