namespace WebApplication1.Services;

public class Sum : ISum
{
    private readonly IAdd _add;

    public Sum(IAdd add)
    {
        _add = add;
    }
    public int Add(int a, int b)
    {
        return _add.Add(a, b);
    }
}