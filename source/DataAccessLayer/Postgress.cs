using System;
using Npgsql;
//link source/Configuration layer to postgress class
using source.ConfigurationLayer;

namespace source;

public class Postgress : IDisposable
{
    public void Dispose()
    {
        throw new NotImplementedException();
    }
}
