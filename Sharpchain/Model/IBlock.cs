using System;

namespace Sharpchain.Model
{
    public interface IBlock
    {
        Int64 Index { get; set; }
        string PrevHash { get; set; }
        DateTime TimeStamp { get; set; }
        string Data { get; set; }
        string Hash { get; set; }
        Int64 Nonce { get; set; }
    }
}