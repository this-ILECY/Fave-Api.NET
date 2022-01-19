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
            switch (reason)
            {
                case "add":
                    {
                        return controllerName + " Added successfully!";
                    }
                case "del":
                    {
                        return controllerName + " deleted successfully!";
                    }
                case "undel":
                    {
                        return controllerName + " undeleted successfully!";
                    }
                case "act":
                    {
                        return controllerName + " activated successfully!";
                    }
                case "inact":
                    {
                        return controllerName + " inactivated successfully!";
                    }
                case "mod":
                    {
                        return controllerName + " updated successfully!";
                    }
                default:
                    {
                        return controllerName + " OK!";
                    }
            }

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
                        return  $"{controllerName} BadRequest!";
                    }
            }
        }
    }
}

