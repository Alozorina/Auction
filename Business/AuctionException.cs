using System;

namespace Business
{
    public class AuctionException : Exception
    {
        public AuctionException() { }
        public AuctionException(string message) : base(message) { }
        public AuctionException(string message, Exception innerException) : base(message, innerException) { }
    }
}
