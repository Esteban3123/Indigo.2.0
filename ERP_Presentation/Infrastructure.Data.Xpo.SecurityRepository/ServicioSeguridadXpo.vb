'***********************************************************************
' Assembly         : Infrastructure.Data.Xpo.SecurityRepository
' Author           : WalterSierra
' Created          : 11-03-2011
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-02-28
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports DevExpress.Xpo.DB
Imports DevExpress.Xpo
Imports DevExpress.Xpo.Metadata
Imports System.ServiceModel
Imports System.Configuration
Imports Infrastructure.CrossCutting.Xpo.Base
Imports System.IO
Imports Infrastructure.CrossCutting.Base
Imports DevExpress.Data.Filtering
Imports DevExpress.Data.Linq

#End Region

''' <summary>
''' Clase que expone los servicios de los repositorios
''' </summary>
Public Class SecurityServicesXpo

#Region "Fields"

    ''' <summary>
    ''' uri donde estan localizado los servicios xpo
    ''' </summary>
    Dim uriServiceEntitiesXpo As String

    ''' <summary>
    ''' protocolo utilizado para los servicios xpo
    ''' </summary>
    Dim protocolServicesXpo As Protocol

    ''' <summary>
    ''' nombre del archivo de configuracion
    ''' </summary>
    Const FileName As String = "Configuracion.IndigoCrystal"

    ''' <summary>
    ''' Variable de Tipo Consultas asincronas de xpo
    ''' </summary>
    Dim WithEvents serverMode As XPInstantFeedbackSource

    ''' <summary>
    ''' variable que contiene el mapeo especifo por entidad para realizar la consulta mediante xpo
    ''' </summary>
    Dim classEntity As XPClassInfo
    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Dim indigo As SessionValues = SessionValues.Instance

#End Region

#Region "Builders"

    ''' <summary>
    ''' Incializa una nueva instancia de la clase <see cref="SecurityServicesXpo" />.
    ''' </summary>
    Public Sub New(ByVal container As String)
        'verifico que exista el archivo
        ReadConfiguration()
        'establezclo la capa de datos para XPO
        XpoDefault.DataLayer = New SimpleDataLayer(New WCFServiceDataStore(GetEndPoint, GetRemoteAddress, container))
    End Sub

    Public Sub New(ByVal container As String, ByVal Segurity As Boolean)
        'verifico que exista el archivo
        ReadConfiguration()
        'establezclo la capa de datos para XPO
        If Segurity = True Then
            XpoDefault.DataLayer = New SimpleDataLayer(New WCFServiceDataStore(GetEndPoint, GetRemoteAddress, container))
        Else
            XpoDefault.DataLayer = New SimpleDataLayer(New WCFServiceDataStore(GetEndPoint, GetRemoteAddress, container))
        End If
    End Sub



#End Region

#Region "Private Methods"

    ''' <summary>
    ''' metodo necesario para leer la configuracion xml de la aplicacion
    ''' </summary>
    Private Sub ReadConfiguration()
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

    '''' <summary>
    '''' Consulta el listado de usuarios existentes
    '''' </summary>
    '''' <returns></returns>
    'Public Function GetUsers() As XPInstantFeedbackSource
    '    'Dim session As New Session
    '    'Dim criteria As CriteriaOperator = CriteriaOperator.Parse("State='False'")
    '    classEntity = XpoDefault.Session.GetClassInfo(GetType(Security_Users))
    '    serverMode = New XPInstantFeedbackSource(classEntity, "UserCode;Position", Nothing)
    '    Return serverMode
    'End Function

    ''' <summary>
    ''' Consulta el listado de los roles existentes.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetRoles() As XPInstantFeedbackSource
        Dim session As New Session()
        ' Dim criteria As CriteriaOperator = CriteriaOperator.Parse("EstadoEliminado='false'")
        classEntity = session.GetClassInfo(GetType(Security_Roll))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;RollCode;Description;RolCodeName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de los grupos existentes.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetGroups() As XPInstantFeedbackSource
        Dim session As New Session()
        'Dim criteria As CriteriaOperator = CriteriaOperator.Parse("EstadoEliminado='false'")
        classEntity = session.GetClassInfo(GetType(Security_Group))
        serverMode = New XPInstantFeedbackSource(classEntity, "Code;Description", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de los grupos existentes.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetGroupsSync() As XPServerCollectionSource
        Dim session As New Session()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("EstadoEliminado='false'")
        Dim clase As XPClassInfo = session.GetClassInfo(GetType(Security_Group))
        Dim serverMode As XPServerCollectionSource = New XPServerCollectionSource(session, clase, criteria)
        serverMode.DisplayableProperties = "Autonumerico;Codigo;Descripcion"
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de los roles existentes.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetRolesSync() As XPServerCollectionSource
        Dim session As New Session()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("EstadoEliminado='false'")
        Dim clase As XPClassInfo = session.GetClassInfo(GetType(Security_Roll))
        Dim serverMode As XPServerCollectionSource = New XPServerCollectionSource(session, clase, criteria)
        serverMode.DisplayableProperties = "Autonumerico;Codigo;Descripcion"
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de los usuarios existentes.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetUsersSync() As XPServerCollectionSource
        Dim session As New Session()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("EstadoEliminado='false'")
        Dim clase As XPClassInfo = session.GetClassInfo(GetType(Security_Users))
        Dim serverMode As XPServerCollectionSource = New XPServerCollectionSource(session, clase, criteria)
        serverMode.DisplayableProperties = "Autonumerico;Codigo;Descripcion;ContactWerknemers"
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de los usuarios existentes.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllUser() As XPInstantFeedbackSource
        Dim session As New Session(XpoDefault.DataLayer)
        'Dim criteria As CriteriaOperator = CriteriaOperator.Parse("StateActive=true")
        classEntity = session.GetClassInfo(GetType(UserXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;UserCode;IdPerson.Fullname;Position;CodeName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista todas las definiciones de tarifa
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllUserXpCollection() As XPCollection
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(UserXpo))
        Return collect
    End Function

    ''' <summary>
    ''' Lista Auditoria Basica
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListBasicAudit(Month As String, Year As String, IdForm As String, IdEntity As String, Company As String) As XPInstantFeedbackSource
        Dim dictionary As XPDictionary = XpoDefault.DataLayer.Dictionary
        Dim classEntity As XPClassInfo = dictionary.GetClassInfo(GetType(BasicAuditXpo))
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Tag=" & IdForm & " AND RegisterId=" & IdEntity & " AND Not([Operation] In (5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17))")
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Tag;UserCode;UserName;RegisterId;Operation;TransactionDate", criteria)

        Return serverMode
    End Function

    ''' <summary>
    ''' Lista Auditoria Basica
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListBasicAuditReports(Month As String, Year As String, IdForm As String, IdEntity As String, Company As String) As XPInstantFeedbackSource
        Dim dictionary As XPDictionary = XpoDefault.DataLayer.Dictionary
        Dim classEntity As XPClassInfo = dictionary.GetClassInfo(GetType(BasicAuditXpo))
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Tag=" & IdForm & " AND RegisterId=" & IdEntity & " AND Not([Operation] In (1,2,3,4))")
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Tag;UserCode;UserName;RegisterId;Operation;TransactionDate", criteria)
        Return serverMode
    End Function


    ''' <summary>
    ''' Lista Auditoria Basica
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListBasicAuditDelete(Month As String, Year As String, IdForm As String, Company As String) As XPInstantFeedbackSource
        Dim dictionary As XPDictionary = XpoDefault.DataLayer.Dictionary
        Dim myBaseClass As XPClassInfo = dictionary.GetClassInfo(GetType(BasicAuditXpo))
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Tag=" & IdForm & " AND Operation=5")
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Tag;UserCode;UserName;RegisterId;Operation;TransactionDate", criteria)
        Return serverMode
    End Function


    ''' <summary>
    ''' Obtiene los objetos de Auditoria Cabecera.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAuditC(ByVal entityName As String, ByVal Company As String, ByVal IdForm As String, ByVal IdEntity As String) As XPInstantFeedbackSource
        Dim criteria As CriteriaOperator = Nothing
        Dim filter As String = String.Empty
        If IdForm <> "0" Then
            filter = "Form='" & IdForm & "' and Padre.Id is null and IsParent = 1"
        End If
        If IdEntity <> "0" Then
            filter = filter & " AND EntityKey='" & IdEntity & "'"
        End If
        If entityName IsNot Nothing And entityName <> "" Then
            filter &= " AND Entidad = '" & entityName & "'"
        End If
        If Not String.IsNullOrEmpty(filter) Then
            criteria = CriteriaOperator.Parse(filter)
        End If
        Dim dictionary As XPDictionary = XpoDefault.DataLayer.Dictionary
        Dim classEntity As XPClassInfo = dictionary.GetClassInfo(GetType(AuditXpo))

        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Accion;Usuario;Fecha;Entidad", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Obtiene los objetos de Auditoria Cabecera Eliminados.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAuditCDelete(entityName As String, ByVal Company As String, ByVal IdForm As String) As XPInstantFeedbackSource
        Dim criteria As CriteriaOperator = Nothing
        Dim filter As String = String.Empty
        If IdForm <> "0" Then
            filter = "Form='" & IdForm & "' AND Accion = 3  and Padre.Id is null and IsParent = 1"
        End If
        If entityName IsNot Nothing And entityName <> "" Then
            filter &= " AND Entidad = " & entityName
        End If
        If Not String.IsNullOrEmpty(filter) Then
            criteria = CriteriaOperator.Parse(filter)
        End If
        Dim dictionary As XPDictionary = XpoDefault.DataLayer.Dictionary
        Dim classEntity As XPClassInfo = dictionary.GetClassInfo(GetType(AuditXpo))

        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Accion;Usuario;Fecha;Entidad", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Obtiene los objetos de Auditoria Detalle
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAuditList(ByVal IdAuditC As String, ByVal entityName As String, ByVal Company As String) As XPServerCollectionSource
        Dim criteria As CriteriaOperator = Nothing
        'criteria = CriteriaOperator.Parse("IdAudit =" & IdAuditC.ToString)
        Dim criteriaString As String = "Id ='" & IdAuditC.ToString & "' and Padre.Id is null and IsParent = 1"
        If entityName IsNot Nothing And entityName <> "" Then
            criteriaString &= " AND Entidad = '" & entityName & "'"
        End If
        criteria = CriteriaOperator.Parse(criteriaString)
        Dim dictionary As XPDictionary = XpoDefault.DataLayer.Dictionary
        'Dim classEntity As XPClassInfo = dictionary.GetClassInfo(GetType(AuditDetailXpo))
        Dim classEntity As XPClassInfo = dictionary.GetClassInfo(GetType(AuditXpo))
        Dim collection As XPServerCollectionSource = New XPServerCollectionSource(New Session(dictionary), classEntity, criteria)
        'collection.DisplayableProperties = "Id;NewValue;PreviousValue;Property1"
        collection.DisplayableProperties = "Accion;Entidad;Usuario;UsuarioWindows;Propiedades;Listas"
        Return collection
    End Function

#Region "Audit LinqInstantFeedBackSource"
    Private WithEvents vlinqAudit As New LinqInstantFeedbackSource

    ''' <summary>
    ''' Obtiene todos los empleados
    ''' </summary>
    Public Function GetAuditListDetail(ByVal IdAuditC As String) As LinqInstantFeedbackSource
        vlinqAudit.KeyExpression = "Id"
        vlinqAudit.SetAttachedProperty("IdAuditC", IdAuditC)
        Return vlinqAudit
    End Function

    Private Sub OnGetQueryableAudit(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinqAudit.GetQueryable
        Try
            Dim sessionNew = New Session(XpoDefault.DataLayer)
            Dim dd = CType(sender, LinqInstantFeedbackSource)
            Dim AuditC = dd.GetAttachedProperty("IdAuditC").ToString
            Dim tableObjD As XPQuery(Of AuditDetailXpo) = New XPQuery(Of AuditDetailXpo)(sessionNew)
            Dim tableObjc As XPQuery(Of AuditXpo) = New XPQuery(Of AuditXpo)(sessionNew)
            Dim TmpQueryableSource = From T1 In tableObjD
            'Where T1.IdAudit.Id = CInt(AuditC)
            'Select T1.Id, T1.NewValue, T1.PreviousValue, T1.Property1
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

#Region "ListUserByPersonName"

    Private WithEvents vlinq As New LinqInstantFeedbackSource

    ''' <summary>
    ''' Lista todos los usuarios por el nombre de la persona
    ''' </summary>
    ''' <param name="personName">Nombre de la persona</param>
    ''' <returns>Lista de usuarios</returns>
    Public Function ListUserByPersonName(ByVal personName As String, ByVal UserCodeExcluid As String) As LinqInstantFeedbackSource
        vlinq.KeyExpression = "Id"
        vlinq.SetAttachedProperty("personName", personName)
        vlinq.SetAttachedProperty("UserCodeExcluid", UserCodeExcluid)
        Return vlinq
    End Function

    Private Sub OnGetQueryable(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinq.GetQueryable
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim aux = CType(sender, LinqInstantFeedbackSource)
        Dim personName = aux.GetAttachedProperty("personName").ToString
        Dim UserCodeExcluid = aux.GetAttachedProperty("UserCodeExcluid").ToString
        Dim User As XPQuery(Of UserXpo) = New XPQuery(Of UserXpo)(sessionNew)
        If personName IsNot Nothing AndAlso Not personName.Trim().Equals(String.Empty) Then
            Dim results = From T1 In User
                                 Where T1.IdPerson.Fullname.Contains(personName) AndAlso T1.UserCode <> UserCodeExcluid
                                 Select T1.Id, T1.UserCode, T1.IdPerson.Fullname, T1.Position, StatusName = If(T1.State = True, "Activo", "Inactivo")

            e.QueryableSource = results
            e.Tag = User
        Else
            Dim results = From T1 In User
                                 Where T1.UserCode <> UserCodeExcluid
                                 Select T1.Id, T1.UserCode, T1.IdPerson.Fullname, T1.Position, StatusName = If(T1.State = True, "Activo", "Inactivo")

            e.QueryableSource = results
            e.Tag = User
        End If
    End Sub
    Private Sub DismissQueryable(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinq.DismissQueryable
        Try
            CType(e.Tag, Object).Dispose()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub

#End Region

#End Region

End Class
