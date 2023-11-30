﻿namespace BusinessLogic
{
    public static class LogMessages
    {
        public static string OnEntityCreatingLog(int id, string entityName) => $"New {entityName} with id: {id} was creared.";

        public static string OnEntityUpdatingLog(int id, string entityName) => $"{entityName} with id: {id} was updated.";

        public static string OnEntityDeletingLog(int id, string entityName) => $"{entityName} with id: {id} was deleted.";

        public static string OnEntityCreatingErrorLog(string entityName) => $"Exception while creating new {entityName}.";

        public static string OnEntityUpdatingErrorLog(int id, string entityName) => $"Exception while updating {entityName} with id: {id}.";

        public static string OnEntityDeletingErrorLog(int id, string entityName) => $"Exception while deleting {entityName} with id: {id}.";




        public static string OnUpdatingClosedOrderLog(int id) => $"Order with id: {id} is closed and cannot be updated.";
    }
}
