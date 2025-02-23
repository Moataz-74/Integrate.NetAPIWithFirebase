using APIWithFireBase.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class FirebaseController : ControllerBase
{
    private readonly FirebaseService _firebaseService;
   

    public FirebaseController(FirebaseService firebaseService )
    {
        _firebaseService = firebaseService;
       
    }

    [HttpPost("post")]
    public async Task<IActionResult> PostData([FromBody] double temp)
    {
        var data = new BabyTemperature()
        {
            Temperature = temp,
            IsNormal = temp < 38 ? true : false,
            
        };
        
        var result = await _firebaseService.PostDataAsync<BabyTemperature>("BabyTemp" , data);
        return Ok();
    }

    [HttpGet("get")]
    public async Task<IActionResult> GetData()
    {
       

        var result = await _firebaseService.GetAllDataAsync("BabyTemp");
        List<BabyTemperature> datalist = new List<BabyTemperature>();
        foreach (var d in result)
        {
          
            datalist.Add(d.Value);

        }
        foreach (var d in datalist)
        {
            Console.WriteLine(d);
        }



        return Ok(result);
    }

}
