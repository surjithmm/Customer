using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CustomerGeneral.GeneralErrors
{
    public class CustomNoRowAffectedException : Exception
    {
        public CustomNoRowAffectedException(string message) : base(message) { }
    }
    public class CustomUnAuthorizeException : Exception
    {
        public CustomUnAuthorizeException(string message) : base(message) { }
    }
    public enum ErrorCode
    {
        Success = 200,
        Created = 201,
        Updated = 204,
        NoRowAffected = 205,
        Unauthorized = 401,
        NotFound = 404,
        InternalServerError = 500,
        Duplicate = 601,
        known = 998,
        Unknown = 999,
    }
    public static class ServerError
    {
        public static int Code(this HttpStatusCode errorCode)
        {
            return (int)errorCode;
        }
        public static string Message(this HttpStatusCode errorCode)
        {
            return errorCode.ToString();
        }
        public static string External(this string Message)
        {
            var sb = new StringBuilder(100);
            sb.Append("External: ").Append(Message);
            return sb.ToString();
        }
        public static string ExternalMessage(this HttpStatusCode errorCode)
        {
            var sb = new StringBuilder(100);
            sb.Append("External ").Append(errorCode.ToString());
            return sb.ToString();
        }
        public static string Message(this ErrorCode errorCode)
        {
            switch (errorCode)
            {
                case ErrorCode.Success:
                    return "Request Succeeded.";
                case ErrorCode.Created:
                    return "Created successfully.";
                case ErrorCode.Updated:
                    return "Updated successfully.";
                case ErrorCode.NoRowAffected:
                    return "Success but no row affected";
                case ErrorCode.Unauthorized:
                    return "Unauthorized access.";
                case ErrorCode.NotFound:
                    return "Resource not found.";
                case ErrorCode.InternalServerError:
                    return "Internal Server Error";
                case ErrorCode.Duplicate:
                    return "Duplicate resource.";
                default:
                    return "Unknown error.";
            }
        }
        public static string Message(this ErrorCode errorCode, string description)
        {
            description = string.IsNullOrEmpty(description) ? "Unknown error" : description;
            switch (errorCode)
            {
                case ErrorCode.Success:
                    return "Request Succeeded.";
                case ErrorCode.Created:
                    return "Created successfully.";
                case ErrorCode.Updated:
                    return "Updated successfully.";
                case ErrorCode.NoRowAffected:
                    return "Success but no row affected";
                case ErrorCode.Unauthorized:
                    return "Unauthorized access.";
                case ErrorCode.NotFound:
                    return "Resource not found.";
                case ErrorCode.InternalServerError:
                    return "Internal Server Error";
                case ErrorCode.Duplicate:
                    return "Duplicate resource.";
                default:
                    return description;
            }
        }
        public static string Message(this IdentityError identityError)
        {
            return identityError.Description;
        }
        public static int Code(this IdentityError identityError)
        {
            switch (identityError.Code)
            {
                case "DuplicateUserName":
                    return (int)ErrorCode.Duplicate;
                case "InvalidUserName":
                    return (int)ErrorCode.InternalServerError; // Define your custom error codes                   // Add more mappings as needed
                default:
                    return (int)ErrorCode.Unknown;
            }
        }
        public static int Code(this ErrorCode errorCode)
        {
            return (int)errorCode;
        }
        public static string Status(this ErrorCode errorCode)
        {
            switch (errorCode)
            {
                case ErrorCode.Success:
                case ErrorCode.Created:
                case ErrorCode.Updated:
                case ErrorCode.NoRowAffected:
                    return "Success";
                default:
                    return "Failed";
            }
        }
    }
}
