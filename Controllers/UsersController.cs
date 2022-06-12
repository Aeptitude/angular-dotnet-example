using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using angular_dotnet_example.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace angular_dotnet_example.Controllers
{
    [ApiController]
    public class UsersController : ControllerBase
    {
        private ApplicationDetails _appDetails;

        private readonly ILogger<UsersController> _logger;

        static readonly Models.IUserRepository repository = new Models.UserRepository();

        public UsersController(ILogger<UsersController> logger, IOptions<ApplicationDetails> appDetails)
        {
            _logger = logger;
            _appDetails = appDetails.Value;
        }

        [HttpGet]
        [Route("api/users")]
        public IEnumerable<Models.UserModel> GetAllUsers()
        {
            var allfiles = Directory.GetFiles(_appDetails.DownloadPath, "*.*", SearchOption.AllDirectories).ToList().Select(x => new UserModel(){firstName = x});
            return allfiles;
        }

        [HttpPost]
        [Route("api/user")]
        [Consumes("application/json")]
        public Models.UserModel PostUser(Models.UserModel item)
        {
            return repository.Add(item);
        }
        

    }
}
