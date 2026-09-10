'***********************************************************************
' Assembly         : Infrastructure.Data.Xpo.AuditRepository
' Author           : Juan Diego Diaz
' Created          : 25-10-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports DevExpress.Xpo.DB
Imports DevExpress.Xpo
Imports DevExpress.Xpo.Metadata
Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Xpo.Base
Imports System.IO
Imports Infrastructure.CrossCutting.Base
Imports DevExpress.Data.Filtering
Imports DevExpress.Data.Linq
Imports System.Configuration

#End Region

''' <summary>
''' Clase que expone los servicios de los repositorios
''' </summary>
Public Class AuditServicesXpo

#Region "Fields"

    ''' <summary>
    ''' uri donde estan localizado los servicios xpo
    ''' </summary>
    Dim uriServiceEntitiesXpo As String

    ''' <summary>
    ''' Nombre del contenedor de seguridad
    ''' </summary>
    Private securityContanier As String

    ''' <summary>
    ''' protocolo utilizado para los servicios xpo
    ''' </summary>
    Dim protocolServicesXpo As Protocol

    ''' <summary>
    ''' Variable de Tipo Consultas asincronas de xpo
    ''' </summary>
    Dim serverMode As XPInstantFeedbackSource

    ''' <summary>
    ''' variable que contiene el mapeo especifo por entidad para realizar la consulta mediante xpo
    ''' </summary>
    Dim classEntity As XPClassInfo

#End Region

#Region "Builders"

    ''' <summary>
    ''' Incializa una nueva instancia de la clase <see cref="AuditServicesXpo" />.
    ''' </summary>
    Public Sub New()
        'verifico que exista el archivo
        ReadConfiguration()
        'establezclo la capa de datos para XPO
        XpoDefault.DataLayer = New SimpleDataLayer(New WCFServiceDataStore(GetEndPoint, GetRemoteAddress, securityContanier))

    End Sub

#End Region

#Region "Private Methods"

    ''' <summary>
    ''' metodo necesario para leer la configuracion xml de la aplicacion
    ''' </summary>
    Private Sub ReadConfiguration()
        securityContanier = If(ConfigurationManager.AppSettings(Infrastructure.CrossCutting.Base.ConfigurationFile.SECURITY_CONTAINER_PARAMETER_NAME) Is Nothing OrElse ConfigurationManager.AppSettings(Infrastructure.CrossCutting.Base.ConfigurationFile.SECURITY_CONTAINER_PARAMETER_NAME).Trim().Equals(String.Empty), Nothing, ConfigurationManager.AppSettings(Infrastructure.CrossCutting.Base.ConfigurationFile.SECURITY_CONTAINER_PARAMETER_NAME).Trim())
        uriServiceEntitiesXpo = ConfigurationFile.Instance.UrlXpoWebServer
        'cargo el protocolo
        protocolServicesXpo = ConfigurationFile.Instance.ProtocolUrlXpoWebServer
    End Sub

    ''' <summary>
    ''' funcion para contatenar el nombre del endpoint por cada protocolo
    ''' </summary>
    ''' <returns>El Nombre de la configuracion del Endpoint Correspondiente</returns>
    Private Function GetEndPoint() As String
        Return System.String.Format("{0}_Endpoint", [Enum].GetName(GetType(Protocol), protocolServicesXpo))
    End Function

    ''' <summary>
    ''' funcion para contatenar el remoteaddress por cada protocolo
    ''' </summary>
    ''' <returns>El Nombre del remoteaddress del Endpoint Correspondiente</returns>
    Private Function GetRemoteAddress() As String
        Return System.String.Format("{0}XpoGate.svc/{1}", uriServiceEntitiesXpo, [Enum].GetName(GetType(Protocol), protocolServicesXpo))
    End Function

#End Region

#Region "Public Methods"
    ''' <summary>
    ''' Obtiene los objetos de Auditoria Cabecera.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAuditC(ByVal IdForm As String, ByVal IdEntity As String) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = Nothing
        Dim filter As String = String.Empty
        If IdForm <> "0" Then
            filter = "Form='" & IdForm & "'"
        End If
        If IdEntity <> "0" Then
            filter = filter & " AND EntityKey=" & IdEntity & ""
        End If
        If Not String.IsNullOrEmpty(filter) Then
            criteria = CriteriaOperator.Parse(filter)
        End If
        classEntity = sessionNew.GetClassInfo(GetType(Audit_Audit))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Action;Users;Date1", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Obtiene los objetos de Auditoria Cabecera Eliminados.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAuditCDelete(ByVal IdForm As String) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = Nothing
        Dim filter As String = String.Empty
        If IdForm <> "0" Then
            filter = "Form='" & IdForm & "' AND Action=3"
        End If
        If Not String.IsNullOrEmpty(filter) Then
            criteria = CriteriaOperator.Parse(filter)
        End If
        classEntity = sessionNew.GetClassInfo(GetType(Audit_Audit))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Action;Users;Date1", criteria)
        Return serverMode
    End Function

#Region "Audit LinqInstantFeedBackSource"
    Private WithEvents vlinqAudit As New LinqInstantFeedbackSource

    ''' <summary>
    ''' Obtiene todos los empleados
    ''' </summary>
    Public Function GetAuditList(ByVal IdAuditC As String) As LinqInstantFeedbackSource
        vlinqAudit.KeyExpression = "Id"
        vlinqAudit.SetAttachedProperty("IdAuditC", IdAuditC)
        Return vlinqAudit
    End Function

    Private Sub OnGetQueryableAudit(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinqAudit.GetQueryable
        Try
            Dim sessionNew = New Session(XpoDefault.DataLayer)
            Dim dd = CType(sender, LinqInstantFeedbackSource)
            Dim AuditC = dd.GetAttachedProperty("IdAuditC").ToString
            Dim tableObjD As XPQuery(Of Audit_AuditDetail) = New XPQuery(Of Audit_AuditDetail)(sessionNew)
            Dim tableObjc As XPQuery(Of Audit_Audit) = New XPQuery(Of Audit_Audit)(sessionNew)
            Dim TmpQueryableSource = From T1 In tableObjD
                                     Where T1.IdAudit.Id = CInt(AuditC)
                                     Select T1.Id, T1.NewValue, T1.PreviousValue, T1.Property1
            'Dim prueba = TmpQueryableSource.ToList
            e.QueryableSource = TmpQueryableSource
            e.Tag = tableObjD
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DismissQueryableAudit(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinqAudit.DismissQueryable
        Try
            'Dispose of the DataContext 
            CType(e.Tag, Object).Dispose()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
#End Region

#End Region

End Class
