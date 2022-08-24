using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiDotnetEntityFramework.Data;

namespace ApiDotnetEntityFramework.Controllers{    
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase{
        private readonly DataContext _context;
        public AccountController(DataContext context){
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Account>>> Get(){
            return Ok(await _context.accounts.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> Get(int id){
            Account? account = await _context.accounts.FindAsync(id);
            if(account == null){
                return BadRequest("Account Not found.");
            }
            return Ok(account);
        }
        
        //[FromBody] usar em parametros post para int e string e valores primitivos
        [HttpPost]
        public async Task<ActionResult<List<Account>>> AddAccount(Account account){
            if(account == null){
                return BadRequest("Account Not found.");
            }
            _context.accounts.Add(account);
            await _context.SaveChangesAsync();
            return Ok(_context.accounts.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Account>>> UpdateAccount(Account request){
            Account? account = await _context.accounts.FindAsync(request.Id);
            if(account == null){
                return BadRequest("Account Not found.");
            }
            account.name = request.name;
            account.email = request.email;
            account.password = request.password;
            await _context.SaveChangesAsync();
            return Ok(await _context.accounts.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Account>>> DeleteAccount(int id){
            Account? account = await _context.accounts.FindAsync(id);
            if(account == null){
                return BadRequest("Account Not found.");
            }
            _context.accounts.Remove(account);
            await _context.SaveChangesAsync();
            return Ok(await _context.accounts.ToListAsync());
        }
    }
}