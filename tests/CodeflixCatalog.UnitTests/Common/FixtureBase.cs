using Bogus;

namespace CodeflixCatalog.UnitTests.Common;

public abstract class FixtureBase
{
    public Faker Faker { get;  set; }
    protected FixtureBase()
    {
        Faker = new Faker("pt_BR");
    }

    
}