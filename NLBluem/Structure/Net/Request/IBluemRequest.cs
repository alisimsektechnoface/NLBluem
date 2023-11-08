using NLBluem.Net.RequestTypes;
using NLBluem.ValueObjects;

namespace NLBluem.Structure.Net.Request
{
    public interface IBluemRequest
    {
        BluemRequestType GetBluemRequestType();

        string BuildSignaturePayload();

        void AddFile(ValueObjects.BluemFile file);

        StringContent GetFileContent();

        string GetFileContentAsString();

        string GetFileMimeType();

        string GetFileName();
    }
}