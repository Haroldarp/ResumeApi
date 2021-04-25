using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeApi.Models
{
    public class Resume
    {
        public Basics basics { get; set; }
        public List<Work> work { get; set; }
        public List<Volunteer> volunteer { get; set; }
        public List<Education> education { get; set; }
        public List<Award> awards { get; set; }
        public List<Publication> publications { get; set; }
        public List<Skill> skills { get; set; }
        public List<Language> languages { get; set; }
        public List<Interest> interests { get; set; }
        public List<Reference> references { get; set; }
    }

    public class Location
    {
        public string address { get; set; }
        public string postalCode { get; set; }
        public string city { get; set; }
        public string countryCode { get; set; }
        public string region { get; set; }
    }

    public class Profile
    {
        public string network { get; set; }
        public string username { get; set; }
        public string url { get; set; }
    }

    public class Basics
    {
        public string name { get; set; }
        public string label { get; set; }
        public string picture { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string website { get; set; }
        public string summary { get; set; }
        public Location location { get; set; }
        public List<Profile> profiles { get; set; }
    }

    public class Work
    {
        public string company { get; set; }
        public string position { get; set; }
        public string website { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string summary { get; set; }
        public List<string> highlights { get; set; }
    }

    public class Volunteer
    {
        public string organization { get; set; }
        public string position { get; set; }
        public string website { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string summary { get; set; }
        public List<string> highlights { get; set; }
    }

    public class Education
    {
        public string institution { get; set; }
        public string area { get; set; }
        public string studyType { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string gpa { get; set; }
        public List<string> courses { get; set; }
    }

    public class Award
    {
        public string title { get; set; }
        public string date { get; set; }
        public string awarder { get; set; }
        public string summary { get; set; }
    }

    public class Publication
    {
        public string name { get; set; }
        public string publisher { get; set; }
        public string releaseDate { get; set; }
        public string website { get; set; }
        public string summary { get; set; }
    }

    public class Skill
    {
        public string name { get; set; }
        public string level { get; set; }
        public List<string> keywords { get; set; }
    }

    public class Language
    {
        public string language { get; set; }
        public string fluency { get; set; }
    }

    public class Interest
    {
        public string name { get; set; }
        public List<string> keywords { get; set; }
    }

    public class Reference
    {
        public string name { get; set; }
        public string reference { get; set; }
    }
}
