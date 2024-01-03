using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

[ApiController]
public class TestController : ControllerBase
{
    private readonly ISum _sum;

    public TestController(ISum sum)
    {
        _sum = sum;
    }
    
    
    [HttpGet]
    [Route("/hello-world")]
    public Task<string> HelloWorld()
    {
        return Task.FromResult("Hello world!");
    }

    [HttpGet]
    [Route("/sum")]
    public Task<int> Add(int a, int b)
    {
        return Task.FromResult(_sum.Add(a,b));
    }
}