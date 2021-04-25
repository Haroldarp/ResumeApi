using ResumeApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeApi.Repository
{
    public static class ResumeRepository
    {
        public static List<Resume> Resumes = new List<Resume>();
        public static Dictionary<string, string> etags = new Dictionary<string, string>();
        public static string etag = null;
    }
}
