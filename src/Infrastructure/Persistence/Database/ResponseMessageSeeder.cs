using Application.Shared;

namespace Persistence.Database;

public static class ResponseMessageSeeder
{

    public static async Task SeedExcepionMessages(ApplicationDbContext context)
    {
        if (!context.ResponseMessages.Any())
        {
            var invalidUrl = Domain.ResponseHandler.ResponseMessage.CreateNew("Invalid url", ResponseMessageTypes.ERROR, CustomErrorCodes.INVALID_URL);
            var urlAlreadyInUse = Domain.ResponseHandler.ResponseMessage.CreateNew("Url already in use", ResponseMessageTypes.ERROR, CustomErrorCodes.URL_ALREADY_IN_USE);
            var invalidHash = Domain.ResponseHandler.ResponseMessage.CreateNew("Invalid hash", ResponseMessageTypes.ERROR, CustomErrorCodes.INVALID_HASH);
            var invalidRoute = Domain.ResponseHandler.ResponseMessage.CreateNew("Invalid route", ResponseMessageTypes.ERROR, CustomErrorCodes.INVALID_ROUTE);
            var hashAlreadyInUse = Domain.ResponseHandler.ResponseMessage.CreateNew("Hash already in use", ResponseMessageTypes.ERROR, CustomErrorCodes.HASH_ALREADY_IN_USE);
            var unhandledException = Domain.ResponseHandler.ResponseMessage.CreateNew("Unhandles exception", ResponseMessageTypes.ERROR, CustomErrorCodes.UNHANDLED_EXCEPTION);
            var recordNotFound = Domain.ResponseHandler.ResponseMessage.CreateNew("Record not found", ResponseMessageTypes.ERROR, CustomErrorCodes.RECORD_NOT_FOUND);

            context.ResponseMessages.Add(invalidUrl);
            context.ResponseMessages.Add(urlAlreadyInUse);
            context.ResponseMessages.Add(invalidHash);
            context.ResponseMessages.Add(invalidRoute);
            context.ResponseMessages.Add(hashAlreadyInUse);
            context.ResponseMessages.Add(unhandledException);
            context.ResponseMessages.Add(recordNotFound);

            await context.SaveChangesAsync();
        }
    }

}