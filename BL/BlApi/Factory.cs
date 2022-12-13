using BlApi;
using BlImplementation;

namespace BlApi
{
    public static class Factory
    {
        public static IBl Get() => new Bl();
    }
}
