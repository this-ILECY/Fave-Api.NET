namespace tenetApi.Exception
{
    public static class Responses
    {
        public static string NotFound(string controllerName)
        {
            return controllerName + " not found!";
        }
        public static string OkResponse(string controllerName, string reason)
        {
            string returner = "";

            switch (controllerName)
            {
                case "token":
                    {
                        returner = controllerName + " " + reason;
                        goto returns;
                    }
            }
            switch (reason)
            {
                case "add":
                    {
                        returner = controllerName + " Added successfully!";
                        break;
                    }
                case "del":
                    {
                        returner = controllerName + " deleted successfully!";
                        break;
                    }
                case "undel":
                    {
                        returner = controllerName + " undeleted successfully!";
                        break;
                    }
                case "act":
                    {
                        returner = controllerName + " activated successfully!";
                        break;
                    }
                case "inact":
                    {
                        returner = controllerName + " inactivated successfully!";
                        break;
                    }
                case "mod":
                    {
                        returner = controllerName + " updated successfully!";
                        break;
                    }
                default:
                    {
                        returner = controllerName + " OK!";
                        break;
                    }
            }
        returns:
            return returner;
        }
        public static string BadResponse(string controllerName, string reason)
        {
            switch (reason)
            {
                case "invalid":
                    {
                        return $"Invalid {controllerName}";
                    }
                case "duplicate":
                    {
                        return $"already a {controllerName} found!";
                    }
                case "extension":
                    {
                        return $"Invalid extension!";
                    }
                case "userType":
                    {
                        return $"Invalid user type!";
                    }
                case "heavy":
                    {
                        return $"heavy file found!";
                    }
                default:
                    {
                        return $"{controllerName} BadRequest!";
                    }
            }
        }

        public static string Unathorized(string reason, string detail)
        {
            string Returner = "";
            switch (reason)
            {
                case "token":
                    Returner = "token";
                    break;
                default:
                    break;
            }
            switch (detail)
            {
                case "expired":
                    Returner = Returner + " expired!";
                    break;
                case "bad":
                    Returner = Returner + " bad detail!";
                    break;
                default:
                    break;
            }

            return Returner;
        }
    }
}

