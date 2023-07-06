using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestWebAPI.Contexts;
using Dapper;
namespace TestWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private Test2Context _context2;
        private TestContext _context1;
        public TestController(Test2Context context2, TestContext context)
        {
            _context2 = context2;
            _context1 = context;
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> Test()
        {
            List<string> someData = await _context2.RunQueryAsync<List<string>>(async (con) =>
            {
                return (await con.QueryAsync<string>("select * from \"Campaigns\"")).ToList();
            });
            List<string> someData2 = await _context1.RunQueryAsync<List<string>>(async (con) =>
            {
                return (await con.QueryAsync<string>("select * from \"Campaign\"")).ToList();
            });
            return Ok();
        }
    }
}
