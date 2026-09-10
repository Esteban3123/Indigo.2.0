'***********************************************************************
' Assembly         : Insfrastructure.CrossCutting.Exceptions
' Author           : Oscar Sierra
' Created          : 11-11-2011
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-02-26
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.Collections.Generic
Imports System.Configuration
Imports System.Net.Http
Imports System.Reflection
Imports System.Text
Imports System.Threading.Tasks
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Base.Window
Imports NewRelic.Api.Agent
Imports Newtonsoft.Json
#End Region

''' <summary>
''' clase para el gestionamiento de exepciones de la aplicacion
''' </summary>
Public NotInheritable Class IndigoManagementExceptions

    ''' <summary>
    ''' Handles the exception UI.	
    ''' </summary>
    ''' <param name="ex">The ex.</param>
    ''' <param name="policy">The policy.</param>
    ''' <remarks></remarks>
    <Transaction>
    Public Shared Async Function HandleExceptionUI(ByVal ex As System.Exception, ByVal policy As String) As Task
        Try
            Dim _apiKey As String = ConfigurationManager.AppSettings("Api-key")
            Dim _endpoint As String = ConfigurationManager.AppSettings("UrlNewRelic")

            Try
                Dim session = SessionValues.Instance
                Dim container As String = If(String.IsNullOrWhiteSpace(session?.HisContainer), session?.TransactionalContainer, session?.HisContainer).Trim()
                If (container.StartsWith("INDIGO", StringComparison.OrdinalIgnoreCase)) Then
                    container = container.Substring(6).Trim()
                End If
                Dim origin = GetExceptionOrigin(ex)
                Using httpClient As New HttpClient()
                    httpClient.DefaultRequestHeaders.Add("Api-Key", _apiKey)

                    Dim logData = New With {
                        .timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                        .message = $"Container: {container}, Message: {ex.Message}",
                        .logType = "error",
                        .level = "ERROR",
                        .appSuite = "pruebas",
                        .hostname = session?.HostName,
                        .userId = session?.UserIndigoId,
                        .clientName = session?.IndigoCompanyName,
                        .version = session?.IndigoVersion,
                        .clientIp = session?.NetworkIP,
                        .callingMethod = origin.MethodName,
                        .callingClass = origin.ClassName,
                        .callingLine = origin.Line,
                        .container = container,
                        .clientId = session?.ClientId,
                        .stackTrace = ex.StackTrace,
                        .innerException = ex.InnerException?.ToString()
                    }
                    Dim jsonData = JsonConvert.SerializeObject(logData)
                    Dim Content = New StringContent(jsonData, Encoding.UTF8, "application/json")

                    Dim response = Await httpClient.PostAsync(_endpoint, Content)
                    Dim text = Await response.Content.ReadAsStringAsync()
                    httpClient.Dispose()
                End Using

            Catch
            End Try
            If ex.GetType.Name = NameOf(IndigoTokenException) Then
                Using pop As New FrmTransparentPopUp(New FrmFlyoutIndigoError(ex.Message, Botones.Cancelar, MessageType.Errores, Nothing))
                    pop.ShowDialog()
                End Using
            Else
                Dim Formulario = New FrmTransparentPopUp(New FrmFlyoutIndigoError("", Botones.Cancelar, MessageType.Errores, Nothing, ex)).ShowDialog(MDISingleInstance.Instance.InstanceMDI)
            End If
        Catch innerEx As System.Exception
            Dim Formulario = New FrmTransparentPopUp(New FrmFlyoutIndigoError("", Botones.Cancelar, MessageType.Errores, Nothing, innerEx)).ShowDialog(MDISingleInstance.Instance.InstanceMDI)
        End Try
    End Function

    ''' <summary>
    ''' Devuelve la clase, método y línea del primer frame útil del base exception.
    ''' Filtra frames de System/Microsoft y de tus namespaces de utilidades de logging.
    ''' </summary>
    Public Shared Function GetExceptionOrigin(ex As Exception) As (ClassName As String, MethodName As String, Line As Integer)

        If ex Is Nothing Then Return ("", "", 0)

        Dim baseEx = ex.GetBaseException()
        Dim st As New StackTrace(baseEx, True)
        Dim frames = st.GetFrames()

        Dim defaultSkips As String() = {"System.", "Microsoft.", "mscorlib.", "Microsoft.VisualBasic."}
        Dim skipList As IEnumerable(Of String) = defaultSkips

        If frames IsNot Nothing Then
            For Each f In frames
                Dim m As MethodBase = f.GetMethod()
                If m Is Nothing Then Continue For

                Dim t = m.DeclaringType
                Dim ns = If(t?.FullName, "")
                Dim shouldSkip = skipList.Any(Function(s) ns.StartsWith(s, StringComparison.Ordinal))

                If Not shouldSkip Then
                    Dim className = If(t IsNot Nothing, t.FullName, "")
                    Dim methodName = m.Name
                    Dim line = f.GetFileLineNumber()
                    If line = 0 Then line = f.GetILOffset()
                    Return (className, methodName, line)
                End If
            Next
        End If

        Dim ts = baseEx.TargetSite
        Dim cls = If(ts?.DeclaringType?.FullName, "")
        Dim met = If(ts?.Name, "")
        Return (cls, met, 0)
    End Function


    ''' <summary>
    ''' Handles the exception.	
    ''' </summary>
    ''' <param name="ex">The ex.</param>
    ''' <param name="policy">The policy.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function HandleException(ByVal ex As System.Exception, ByVal policy As String) As Boolean
        Return HandleException(ex, policy, Nothing)
    End Function

    ''' <summary>
    ''' Handles the exception.	
    ''' </summary>
    ''' <param name="ex">The ex.</param>
    ''' <param name="policy">The policy.</param>
    ''' <param name="sessionValues">Valores de Session</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Transaction>
    Public Shared Function HandleException(ByVal ex As System.Exception, ByVal policy As String, Optional ByVal sessionValues As SessionValues = Nothing) As Boolean
        Try
            Dim apm As ApmHandler = New NewRelicAPM()
            If sessionValues IsNot Nothing Then
                apm.AddCustomAttribute("Client", sessionValues?.IndigoCompanyName)
                apm.AddCustomAttribute("ContainerName", sessionValues?.FoundationalContainer)
                apm.AddCustomAttribute("UserId", sessionValues?.UserIndigoId)
                apm.AddCustomAttribute("UserName", sessionValues?.UserIndigoName)
                apm.AddCustomAttribute("Version", sessionValues?.IndigoVersion)
                apm.AddCustomAttribute("HostName", sessionValues?.HostName)
                apm.AddCustomAttribute("NetworkIp", sessionValues?.NetworkIP)
            Else
                apm.AddCustomAttribute("ContainerName", ServerSessionValues.Current?.CurrentContainer)
                apm.AddCustomAttribute("Version", ServerSessionValues.Current?.IndigoVersion)
            End If
            apm.AddCustomAttribute("AppName", Base.Utils.GetAppName())
            apm.AddCustomAttribute("Policy", policy)
            apm.NoticeError(ex)
            Return True
        Catch innerEx As System.Exception
            Dim errorMsg As String = System.String.Format("An unexpected exception occured " & "while calling HandleException with policy '{0}.{1}{2}'", policy, Environment.NewLine, innerEx.ToString())
            Return False
        End Try
    End Function

    Public Shared Function GetExceptionDetails(exception As Exception) As String
        If exception.GetType().Name.Equals("DbEntityValidationException") Then
            Dim errors As New System.Text.StringBuilder()
            For Each eve In CType(exception, System.Data.Entity.Validation.DbEntityValidationException).EntityValidationErrors
                errors.AppendLine(String.Format("Entity of type {0} in state {1} has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State))
                For Each ve In eve.ValidationErrors
                    errors.AppendLine(String.Format("- Property: {0}, Error: {1}", ve.PropertyName, ve.ErrorMessage))
                Next
            Next
            Return errors.ToString()
        Else
            Dim properties = exception.[GetType]().GetProperties()
            Dim fields = properties.[Select](Function([property]) New With {
                Key .Name = [property].Name,
                Key .Value = [property].GetValue(exception, Nothing)
            }).[Select](Function(x) [String].Format("{0} : {1}", x.Name, If(x.Value IsNot Nothing, x.Value.ToString(), [String].Empty)))
            Return [String].Join(vbLf, fields)
        End If
    End Function

End Class
