using Application.Events.Repository;
using Infrastructure.CrossCutting.Base;
using Infrastructure.CrossCutting.Interface;
using System.Configuration;
using System.Data;

namespace Application.Events.Models
{
    /// <summary>
    /// Clase para guardar los detalles de errores presentados.
    /// </summary>
    public class ErrorWrapper
    {
        public int MessageType { get; set; }
        public string Message { get; set; }
        public string SourceCode { get; set; }
        public string Source { get; set; }
        public string Action { get; set; }
        public string SourceDataBase { get; set; }
        public string TargetDataBase { get; set; }
        public string Exception { get; set; }

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="messageType"></param>
        /// <param name="message"></param>
        /// <param name="sourceCode"></param>
        /// <param name="source"></param>
        /// <param name="action"></param>
        /// <param name="sourceDataBase"></param>
        /// <param name="targetDataBase"></param>
        /// <param name="exception"></param>
        public ErrorWrapper(int messageType, string message, string sourceCode, string source, string action, string sourceDataBase, string targetDataBase, string exception)
        {
            MessageType = messageType;
            Message = message;
            SourceCode = sourceCode;
            Source = source;
            Action = action;
            SourceDataBase = sourceDataBase;
            TargetDataBase = targetDataBase;
            Exception = exception;
        }

        /// <summary>
        /// Método que se encarga de guardar el objeto ErrorWrapper con los detalles del error.
        /// </summary>
        public void SaveErrorLog()

        {
            ConectionSQL conectionSQL = new ConectionSQL(ConfigurationManager.AppSettings.Get(ConfigurationFile.SECURITY_CONTAINER_PARAMETER_NAME).ToString());

            string query = "INSERT INTO Security.SyncLog " +
                    "(MessageType, " +
                    "Message, " +
                    "SourceCode, " +
                    "Source, " +
                    "Action, " +
                    "SourceDataBase, " +
                    "TargetDataBase, " +
                    "Data" +
                    ") " +
                "SELECT TOP 1 " +
                    "@MessageType, " +
                    "@Message, " +
                    "@SourceCode, " +
                    "@Source, " +
                    "@Action, " +
                    "@SourceDataBase, " +
                    "@TargetDataBase, " +
                    "@Data " +
                    "FROM Security.Containers c WITH(NOLOCK)" +
                    "where c.TransactionalContainer = @SourceDataBase AND c.PublishEvent = 1 ";

            conectionSQL.AddParam("MessageType", SqlDbType.TinyInt, MessageType);
            conectionSQL.AddParam("Message", SqlDbType.VarChar, Message);
            conectionSQL.AddParam("SourceCode", SqlDbType.VarChar, SourceCode);
            conectionSQL.AddParam("Source", SqlDbType.VarChar, Source);
            conectionSQL.AddParam("Action", SqlDbType.VarChar, Action);
            conectionSQL.AddParam("SourceDataBase", SqlDbType.VarChar, SourceDataBase);
            conectionSQL.AddParam("TargetDataBase", SqlDbType.VarChar, TargetDataBase);
            conectionSQL.AddParam("Data", SqlDbType.Text, Exception);

            conectionSQL.ExecuteCommandParams(query);

        }
    }
}
