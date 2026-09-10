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
Imports Domain.Security.Entities


#End Region

''' <summary>
''' Clase que expone los servicios de los repositorios
''' </summary>
Public Class SecurityServicesXpoEx
    Inherits XpoBaseService
    Implements IDisposable

#Region "Fields"

#End Region

#Region "Builders"

    ' ''' <summary>
    ' ''' Incializa una nueva instancia de la clase
    ' ''' </summary>
    ' ''' <param name="Company">Empresa que se debe consultar</param>
    ' ''' <param name="endpoint">Punto de configuración del servicio</param>
    ' ''' <param name="remoteaddress">Dirección remota del servicio</param>
    'Public Sub New(Company As String, ByVal endpoint As String, ByVal remoteaddress As String)
    '    XpoDefault.DataLayer = New SimpleDataLayer(New WCFServiceDataStoreEx(endpoint, remoteaddress, Company))
    'End Sub

#End Region

#Region "Public Methods"

    ''' <summary>
    ''' Obtiene una lista de UserXpo en estado Activo y de tipo Administrativo
    ''' </summary>
    ''' <returns></returns>
    Public Function ListActiveAdministrativeUsers() As List(Of UserXpo)
        Dim sessionNew As New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("State = ? AND ProfileType = ?", True, "1")
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(UserXpo), criteria)
        Return collect.ToEntityList(Of UserXpo)()
    End Function


    ''' <summary>
    ''' Función para obtener una lista de datos de una entidad XPO
    ''' </summary>
    ''' <typeparam name="T">Objeto XPO</typeparam>
    ''' <param name="Fun">Objeto tipo Función opcional para filtrar los datos necesarios</param>
    ''' <returns>Lista tipo T</returns>
    Public Function GetCollection(Of T)(Optional Fun As Func(Of T, Boolean) = Nothing, Optional criteria As String = Nothing) As List(Of T)
        Dim result = Me.LoadCollection(Of T)(XpoDefault.DataLayer, Fun, criteria)
        result.Sort()
        Return result
    End Function

    ''' <summary>
    ''' Consulta el listado de los roles existentes.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetRoles(ByVal pUserType As UserType, Optional TenantId As Integer? = Nothing) As XPInstantFeedbackSource
        Dim session As New Session()
        Dim criteria As CriteriaOperator = Nothing
        If pUserType = UserType.GlobalAdmin Then
            criteria = Nothing
        ElseIf pUserType = UserType.TenantAdmin AndAlso TenantId IsNot Nothing Then
            criteria = CriteriaOperator.Parse($"RollType=2 and EntityTenantRollXpo[TenantId={TenantId}] ") 'por tenant
        Else
            criteria = CriteriaOperator.Parse("Id=-1")
        End If
        Dim classEntity = session.GetClassInfo(GetType(Security_Roll))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;RollCode;Description;RolCodeName;RollTypeName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de los grupos existentes.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetGroups(ByVal pUserType As UserType, Optional TenantId As Integer? = Nothing) As XPInstantFeedbackSource
        Dim session As New Session()
        Dim criteria As CriteriaOperator = Nothing
        If pUserType = UserType.GlobalAdmin Then
            criteria = Nothing
        ElseIf pUserType = UserType.TenantAdmin Then
            criteria = CriteriaOperator.Parse($"GroupType=2 and EntityTenantRollXpo[TenantId={TenantId}]") 'por tenant
        Else
            criteria = CriteriaOperator.Parse("Id=-1")
        End If
        'Dim criteria As CriteriaOperator = CriteriaOperator.Parse("EstadoEliminado='false'")
        Dim classEntity = session.GetClassInfo(GetType(Security_Group))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Code;Description;GroupTypeName", criteria)
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

    '''' <summary>
    '''' Consulta el listado de los usuarios existentes.
    '''' </summary>
    '''' <returns></returns>
    'Public Function GetUsersSync() As XPServerCollectionSource
    '    Dim session As New Session()
    '    Dim criteria As CriteriaOperator = CriteriaOperator.Parse("EstadoEliminado='false'")
    '    Dim clase As XPClassInfo = session.GetClassInfo(GetType(Security_Users))
    '    Dim serverMode As XPServerCollectionSource = New XPServerCollectionSource(session, clase, criteria)
    '    serverMode.DisplayableProperties = "Autonumerico;Codigo;Descripcion;ContactWerknemers"
    '    Return serverMode
    'End Function

    '''' <summary>
    '''' Consulta el listado de los usuarios existentes.
    '''' </summary>
    '''' <returns></returns>
    'Public Function GetAllUser() As XPInstantFeedbackSource
    '    Dim session As New Session(XpoDefault.DataLayer)
    '    'Dim criteria As CriteriaOperator = CriteriaOperator.Parse("StateActive=true")
    '    Dim classEntity = session.GetClassInfo(GetType(UserXpo))
    '    Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;UserCode;IdPerson.Fullname;Position;CodeName", Nothing)
    '    Return serverMode
    'End Function

    ''' <summary>
    ''' lista todas las definiciones de tarifa
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllUserXpCollection(userCode As String) As XPCollection
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("UserCode='" & userCode & "'")
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(UserXpo), criteria)
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
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Tag;UserCode;UserName;RegisterId;Operation;TransactionDate", criteria)

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
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Tag;UserCode;UserName;RegisterId;Operation;TransactionDate", criteria)
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
        Dim serverMode = New XPInstantFeedbackSource(myBaseClass, "Id;Tag;UserCode;UserName;RegisterId;Operation;TransactionDate", criteria)
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

        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Accion;Usuario;Fecha;Entidad", criteria)
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

        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Accion;Usuario;Fecha;Entidad", criteria)
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

    ''' <summary>
    ''' Devuelve los tenant activos por tipo de usuario
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllTenant(ByVal UserId As Integer, ByVal pUserType As UserType) As XPServerCollectionSource
        Dim session As New Session()
        Dim collect As XPServerCollectionSource = Nothing
        Dim criteria As CriteriaOperator
        Dim classEntity As XPClassInfo

        Select Case pUserType
            Case UserType.GlobalAdmin
                criteria = CriteriaOperator.Parse("Status=2")
                classEntity = session.GetClassInfo(GetType(TenantXpo))
                collect = New XPServerCollectionSource(session, classEntity, criteria)
                collect.DisplayableProperties = "Id;Name"
            Case UserType.TenantAdmin
                criteria = CriteriaOperator.Parse("UserId=" & UserId)
                classEntity = session.GetClassInfo(GetType(TenantUsersXpo))
                collect = New XPServerCollectionSource(session, classEntity, criteria)
                collect.DisplayableProperties = "Id;Name"
            Case UserType.CompanyAdmin, UserType.StandardUser
                criteria = CriteriaOperator.Parse("UserId=" & UserId & " AND ManageCompany = 1")
                classEntity = session.GetClassInfo(GetType(TenantUsersXpo))
                collect = New XPServerCollectionSource(session, classEntity, criteria)
                collect.DisplayableProperties = "Id;Name"
        End Select

        Return collect
    End Function

    ''' <summary>
    ''' lista todos los roles por tenant
    ''' </summary>
    ''' <param name="tenantId"></param>
    ''' <returns></returns>
    Public Function ListTenantRoll(ByVal tenantId As Short) As List(Of TenantRollXpo)
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("TenantId=" & tenantId)
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(TenantRollXpo), criteria)
        Return collect.ToEntityList(Of TenantRollXpo)
    End Function
    ''' <summary>
    ''' lista roles globales
    ''' </summary>
    ''' <returns></returns>
    Public Function ListGlobalRolls() As XPServerCollectionSource
        Dim session = New Session()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("RollType = 1")
        Dim classEntity As XPClassInfo = session.GetClassInfo(GetType(Security_Roll))
        Dim collect As XPServerCollectionSource = New XPServerCollectionSource(session, classEntity, criteria)
        collect.DisplayableProperties = "Id;RollCode;Description;RolCodeName;RollType"
        Return collect
    End Function

    ''' <summary>
    ''' lista todos los grupos por tenant
    ''' </summary>
    ''' <param name="tenantId"></param>
    ''' <returns></returns>
    Public Function ListTenantGroup(ByVal tenantId As Short) As List(Of TenantGroupXpo)
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("TenantId=" & tenantId)
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(TenantGroupXpo), criteria)
        Return collect.ToEntityList(Of TenantGroupXpo)
    End Function

    ''' <summary>
    ''' lista grupos globales
    ''' </summary>
    ''' <returns></returns>
    Public Function ListGlobalGroups() As XPServerCollectionSource
        Dim session = New Session()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("GroupType = 1")
        Dim classEntity As XPClassInfo = session.GetClassInfo(GetType(Security_Group))
        Dim collect As XPServerCollectionSource = New XPServerCollectionSource(session, classEntity, criteria)
        collect.DisplayableProperties = "Id;Code;Description;State;GroupCodeName;GroupType"
        Return collect
    End Function

    ''' <summary>
    ''' Lista todos los Productos Catalogo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListProductCatalog() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ProductCatalogXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(ProductCatalogXpo)),
                                                          "Id;PlatformName;SuiteName;ProductName;State,Visible,StateName", Nothing)
    End Function

    ''' <summary>
    ''' Lista todos los Modulos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListModule() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ModuleXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(ModuleXpo)),
                                                          "Id;Name;Description;State,StateName", Nothing)
    End Function

    ''' <summary>
    ''' Devuelve los tenant activos por tipo de usuario
    ''' </summary>
    ''' <returns></returns>
    Public Function ListTitle() As XPServerCollectionSource
        Dim session As New Session()
        Dim collect As XPServerCollectionSource = Nothing
        Dim classEntity As XPClassInfo

        classEntity = session.GetClassInfo(GetType(TitleXpo))
        collect = New XPServerCollectionSource(session, classEntity, Nothing)
        collect.DisplayableProperties = "Id;Name;State"

        Return collect
    End Function


    ''' <summary>
    ''' Devuelve los tenant activos por tipo de usuario
    ''' </summary>
    ''' <returns></returns>
    Public Function ListVieForm() As XPServerCollectionSource
        Dim session As New Session()
        Dim collect As XPServerCollectionSource = Nothing
        Dim classEntity As XPClassInfo

        classEntity = session.GetClassInfo(GetType(VieFormXpo))
        collect = New XPServerCollectionSource(session, classEntity, Nothing)
        collect.DisplayableProperties = "Id;Name;ClassName;AssemblyName;State,StateName"

        Return collect
    End Function

    ''' <summary>
    ''' Lista todos los Formularios
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListForm() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of FormXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(FormXpo)), Nothing, Nothing)
    End Function

#Region "Audit LinqInstantFeedBackSource"

    ''' <summary>
    ''' Obtiene todos los empleados
    ''' </summary>
    Public Function GetAuditListDetail(ByVal IdAuditC As String) As LinqInstantFeedbackSource
        Dim vlinqAudit As New LinqInstantFeedbackSource
        AddHandler vlinqAudit.GetQueryable, AddressOf OnGetQueryableAudit
        AddHandler vlinqAudit.DismissQueryable, AddressOf DismissQueryableAudit
        vlinqAudit.KeyExpression = "Id"
        vlinqAudit.SetAttachedProperty("IdAuditC", IdAuditC)
        Return vlinqAudit
    End Function

    Private Sub OnGetQueryableAudit(ByVal sender As Object, ByVal e As GetQueryableEventArgs)
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

    Private Sub DismissQueryableAudit(ByVal sender As Object, ByVal e As GetQueryableEventArgs)
        Try
            'Dispose of the DataContext 
            CType(e.Tag, Object).Dispose()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
#End Region

    '#Region "ListUserByPersonName"

    '    ''' <summary>
    '    ''' Lista todos los usuarios por el nombre de la persona
    '    ''' </summary>
    '    ''' <param name="personName">Nombre de la persona</param>
    '    ''' <returns>Lista de usuarios</returns>
    '    Public Function ListUserByPersonName(ByVal personName As String, ByVal UserCodeExcluid As String) As LinqInstantFeedbackSource
    '        Dim vlinq As New LinqInstantFeedbackSource
    '        AddHandler vlinq.GetQueryable, AddressOf OnGetQueryable
    '        AddHandler vlinq.DismissQueryable, AddressOf DismissQueryable
    '        vlinq.KeyExpression = "Id"
    '        vlinq.SetAttachedProperty("personName", personName)
    '        vlinq.SetAttachedProperty("UserCodeExcluid", UserCodeExcluid)
    '        Return vlinq
    '    End Function

    '    Private Sub OnGetQueryable(ByVal sender As Object, ByVal e As GetQueryableEventArgs)
    '        Dim sessionNew = New Session(XpoDefault.DataLayer)
    '        Dim aux = CType(sender, LinqInstantFeedbackSource)
    '        Dim personName = aux.GetAttachedProperty("personName").ToString
    '        Dim UserCodeExcluid = aux.GetAttachedProperty("UserCodeExcluid").ToString
    '        Dim User As XPQuery(Of UserXpo) = New XPQuery(Of UserXpo)(sessionNew)
    '        If personName IsNot Nothing AndAlso Not personName.Trim().Equals(String.Empty) Then
    '            Dim results = From T1 In User
    '                          Where T1.IdPerson.Fullname.Contains(personName) AndAlso T1.UserCode <> UserCodeExcluid
    '                          Select T1 'T1.Id, T1.UserCode, T1.IdPerson.Fullname, T1.Position, StatusName = If(T1.State = True, "Activo", "Inactivo")

    '            e.QueryableSource = results
    '            e.Tag = User
    '        Else
    '            Dim results = From T1 In User
    '                          Where T1.UserCode <> UserCodeExcluid And T1.UserType <> "3"
    '                          Select T1
    '            'T1.Id,
    '            'T1.UserCode,
    '            'T1.IdPerson.Fullname,
    '            'T1.Position,
    '            'UserCodeFullName = T1.UserCode + " - " + T1.IdPerson.Fullname,
    '            'StatusName = If(T1.State = True, "Activo", "Inactivo"),
    '            'T1.TenantName,
    '            'T1.IdTenant

    '            e.QueryableSource = results
    '            e.Tag = User
    '        End If
    '    End Sub
    '    Private Sub DismissQueryable(ByVal sender As Object, ByVal e As GetQueryableEventArgs)
    '        Try
    '            CType(e.Tag, Object).Dispose()
    '        Catch ex As Exception
    '            ex.Message.ToString()
    '        End Try
    '    End Sub

    '#End Region

#Region "ListUserByContainer"

    ''' <summary>
    ''' Lista usuarios globales, mas usuarios administradores de tenant de la compañia, mas los usuarios con permiso a una compañia
    ''' </summary>
    ''' <param name="idContainer">id de container</param>
    ''' <returns>Lista de usuarios</returns>
    Public Function ListUserByContainer(ByVal idContainer As Integer) As LinqInstantFeedbackSource
        Dim vlinq As New LinqInstantFeedbackSource
        AddHandler vlinq.GetQueryable, AddressOf OnGetUserByContainerQueryable
        AddHandler vlinq.DismissQueryable, AddressOf DismissUserByContainerQueryable
        vlinq.KeyExpression = "Id"
        vlinq.SetAttachedProperty("idContainer", idContainer)
        Return vlinq
    End Function

    Private Sub OnGetUserByContainerQueryable(ByVal sender As Object, ByVal e As GetQueryableEventArgs)
        Dim _Session = New Session(XpoDefault.DataLayer)
        Dim _Sender = CType(sender, LinqInstantFeedbackSource)
        Dim _ContainerId As Integer = Convert.ToInt32(_Sender.GetAttachedProperty("idContainer"))
        Dim _TenantId As Short = 0
        Dim UserTypeToExclude = New List(Of Char)({"2", "3", "4"})
        'recupera el tenant de la compañia
        Dim _Company As Company = UnifiedConfiguration.Instance.ListCompanies.Where(Function(c) c.Id = _ContainerId).FirstOrDefault
        If _Company IsNot Nothing Then
            _TenantId = _Company.TenantId
        End If

        Dim _Users As XPQuery(Of UserXpo) = New XPQuery(Of UserXpo)(_Session)
        Dim _TenantUsers As XPQuery(Of TenantUsersXpo) = New XPQuery(Of TenantUsersXpo)(_Session)
        Dim _PermissionCompanyXpo As XPQuery(Of PermissionCompanyXpo) = New XPQuery(Of PermissionCompanyXpo)(_Session)

        Dim _UsersGlobal = (From _User In _Users
                            Where _User.UserType = "3" Or _User.UserType = "4" 'usuario globales y qa
                            Select _User).Distinct()

        Dim _UsersInCompany = (From _User In _Users
                               Where (Not UserTypeToExclude.Contains(_User.UserType) AndAlso _PermissionCompanyXpo.Any(Function(s) s.IdUser = _User.Id AndAlso s.IdContainer = _ContainerId And s.Permission)) 'usuario tengan permiso en la compañia a excepcion de los AdminTenat y globales
                               Select _User).Distinct()

        'lista de usuarios que hacen parte de un tenant
        'usuarios administradores de tenant, y que tienen permiso al tenant de la compañia
        Dim _UsersTenantAdmin = (From _User In _Users
                                 Where _User.UserType = "2" AndAlso _TenantUsers.Any(Function(a) a.UserId = _User.Id AndAlso a.Id = _TenantId)
                                 Select _User)

        e.QueryableSource = (_UsersGlobal.Union(_UsersTenantAdmin).Union(_UsersInCompany)).Distinct()
        e.Tag = _Users
    End Sub
    Private Sub DismissUserByContainerQueryable(ByVal sender As Object, ByVal e As GetQueryableEventArgs)
        Try
            CType(e.Tag, Object).Dispose()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub

    ''' <summary>
    ''' lista todas las definiciones de tarifa
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPermissionCompany(ByVal containerId As Integer) As List(Of PermissionCompanyXpo)
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("IdContainer=" & containerId & " AND Permission = 1")
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(PermissionCompanyXpo), criteria)
        Return collect.ToEntityList(Of PermissionCompanyXpo)
    End Function

#End Region

    '#Region "ListUserByTenant"

    '    ''' <summary>
    '    ''' Lista todos los usuarios que hacen parte de un tenant
    '    ''' se usa para un usuario que tiene permiso a la opcion y es administrador
    '    ''' no valida permisos dado que el usuario puede asignarlo en la seccion de compañias
    '    ''' </summary>
    '    ''' <param name="idTenant">id de tenant</param>
    '    ''' <returns>Lista de usuarios</returns>
    '    Public Function ListUserByTenant(ByVal idTenant As Short) As LinqInstantFeedbackSource
    '        Dim vlinq As New LinqInstantFeedbackSource
    '        AddHandler vlinq.GetQueryable, AddressOf OnGetUserByTenantQueryable
    '        AddHandler vlinq.DismissQueryable, AddressOf DismissUserByTenantQueryable
    '        vlinq.KeyExpression = "Id"
    '        vlinq.SetAttachedProperty("idTenant", idTenant)
    '        Return vlinq
    '    End Function

    '    Private Sub OnGetUserByTenantQueryable(ByVal sender As Object, ByVal e As GetQueryableEventArgs)
    '        Dim sessionNew = New Session(XpoDefault.DataLayer)
    '        Dim aux = CType(sender, LinqInstantFeedbackSource)
    '        Dim idTenantAux As Short = Convert.ToInt16(aux.GetAttachedProperty("idTenant"))
    '        Dim User As XPQuery(Of UserXpo) = New XPQuery(Of UserXpo)(sessionNew)

    '        Dim results
    '        If idTenantAux = 0 Then
    '            results = From T1 In User
    '                      Where T1.TenantId Is Nothing AndAlso T1.UserType <> "3"
    '                      Select T1
    '        Else
    '            results = From T1 In User
    '                      Where T1.TenantId IsNot Nothing AndAlso T1.TenantId.Id = idTenantAux AndAlso T1.UserType <> "3"
    '                      Select T1
    '        End If


    '        e.QueryableSource = results
    '        e.Tag = User
    '    End Sub
    '    Private Sub DismissUserByTenantQueryable(ByVal sender As Object, ByVal e As GetQueryableEventArgs)
    '        Try
    '            CType(e.Tag, Object).Dispose()
    '        Catch ex As Exception
    '            ex.Message.ToString()
    '        End Try
    '    End Sub
    '#End Region

#Region "ListUserByTenant"

    Public Function ListUserByUser(ByVal userId As Integer, ByVal pUserType As UserType) As LinqInstantFeedbackSource
        Dim vlinq As New LinqInstantFeedbackSource
        AddHandler vlinq.GetQueryable, AddressOf OnListUserByUserQueryable
        AddHandler vlinq.DismissQueryable, AddressOf DismissListUserByUserQueryable
        vlinq.KeyExpression = "Id"
        vlinq.SetAttachedProperty("userId", userId)
        vlinq.SetAttachedProperty("pUserType", pUserType)
        Return vlinq
    End Function

    Private Sub OnListUserByUserQueryable(ByVal sender As Object, ByVal e As GetQueryableEventArgs)
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim aux = CType(sender, LinqInstantFeedbackSource)
        Dim _userId As Integer = Convert.ToInt32(aux.GetAttachedProperty("userId"))
        Dim _pUserType As UserType = Convert.ToInt32(aux.GetAttachedProperty("pUserType"))
        Dim _Users As XPQuery(Of UserXpo) = New XPQuery(Of UserXpo)(sessionNew)


        Dim results = Nothing

        Select Case _pUserType
            Case UserType.GlobalAdmin, UserType.GlobalQa
                results = From _User In _Users
            Case UserType.TenantAdmin
                Dim _TenantUsersXpos As XPQuery(Of TenantUsersXpo) = New XPQuery(Of TenantUsersXpo)(sessionNew)
                'lista de tenant del usuario logeado
                Dim _listTenants = _TenantUsersXpos.Where(Function(tu) tu.UserId = _userId).Select(Function(tu) tu.Id).ToList
                'lista de usuarios que estan asociados a los tenant del usuario logeado
                'Dim _listUsers = From _TenantUser In _TenantUsersXpos.Where(Function(tu) _listTenants.Contains(tu.Id)).Select(Function(tu) tu.UserId).ToList
                results = From _User In _Users.Where(Function(u) u.UserType = "1" OrElse u.UserType = "0") 'usuario administrador de compañia o usuario estandar
                          Join tu In _TenantUsersXpos On tu.UserId Equals _User.Id
                          Where _listTenants.Contains(tu.Id)
                          Select _User
            Case UserType.CompanyAdmin
                Dim _PermissionCompanyXpos As XPQuery(Of PermissionCompanyXpo) = New XPQuery(Of PermissionCompanyXpo)(sessionNew)
                'lista de compañias del usuario logeado donde es administrador
                Dim _listCompanies = _PermissionCompanyXpos.Where(Function(pc) pc.IdUser = _userId AndAlso pc.Permission AndAlso pc.Administrator).Select(Function(pc) pc.IdContainer).ToList
                'lista de usuarios que estan asociados a las compañias del usuario logeado
                'Dim _listUsers = From _PermissionCompanyXpo In _PermissionCompanyXpos.Where(Function(pc) _listCompanies.Contains(pc.IdContainer)).Select(Function(pc) pc.IdUser).ToList
                results = (From _User In _Users.Where(Function(u) u.UserType = "0") 'usuario estandar  'Join pc In _PermissionCompanyXpos On pc.IdUser Equals _User.Id 'Where _listCompanies.Contains(pc.IdContainer)
                           Where _PermissionCompanyXpos.Any(Function(s) s.IdUser = _User.Id AndAlso _listCompanies.Contains(s.IdContainer))
                           Select _User)
            Case UserType.StandardUser
                results = Nothing
        End Select



        e.QueryableSource = results
        e.Tag = _Users
    End Sub
    Private Sub DismissListUserByUserQueryable(ByVal sender As Object, ByVal e As GetQueryableEventArgs)
        Try
            CType(e.Tag, Object).Dispose()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
#End Region

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
