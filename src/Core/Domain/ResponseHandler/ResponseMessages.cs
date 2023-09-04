using System.ComponentModel.DataAnnotations;

namespace Domain.ResponseHandler;

public class ResponseMessage
{
    public int Id { get; private set; }
    public int ReasonCode { get; private set; }
    [MaxLength(20)]
    public string Type { get; private set; }
    [MaxLength(100)]
    public string Message { get; private set; } = string.Empty;


    public static ResponseMessage CreateNew(string message, string type, int reasonCode)
    {
        return new ResponseMessage(message, type, reasonCode);
    }


    private ResponseMessage(string message, string type, int reasonCode)
    {
        this.Message = message;
        this.Type = type;
        this.ReasonCode = reasonCode;
    }

}