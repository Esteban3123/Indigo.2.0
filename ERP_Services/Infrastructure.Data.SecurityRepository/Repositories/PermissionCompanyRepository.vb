'***********************************************************************
' Assembly         : Infrastructure.Data.SecurityRepository
' Author           : Jorge Leonardo Vernaza
' Created          : 01-08-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Security.Entities
Imports Domain.Security
Imports Infrastructure.Data.Base
Imports Infrastructure.CrossCutting.Interface
Imports Domain.Base.Entities
Imports System.Data.Entity
Imports Infrastructure.CrossCutting.Security
Imports System.Configuration
Imports System.Dynamic
Imports System.Globalization
Imports Infrastructure.CrossCutting.Base

#End Region

''' <summary>
''' Esta clase Repositorio Grupo hereda del repositorio generico para poder obtener los metodos
''' comunes en todos los repositorios de manera que esta clase solo contiene los metodos y funciones no Comunes.
''' </summary>
Public Class PermissionCompanyRepository
    Inherits GenericRepository(Of PermissionCompany)
    Implements IPermissionCompanyRepository

    'Devuelve el contexto en este repositorio 
    Private _context As ISeguridadUnitOfWork

    ''' <summary>
    '''inicializa la neva instancia de <see cref="GroupRepository" /> clase.
    ''' </summary>
    ''' <param name="contex">el contexto.</param>
    Public Sub New(ByVal contex As ISeguridadUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

    ''' <summary>
    ''' metodo que develve los valores originales de la entidad {T}
    ''' </summary>
    ''' <typeparam name="TEntity">el tipo de la entidad.</typeparam>
    ''' <param name="entity">el objeto de la entidad.</param>
    ''' <returns></returns>
    Public Function GetSourceValues(Of TEntity)(ByVal entity As TEntity) As TEntity
        Return IndigoContext.GetSourceValues(entity, CType(_context, DbContext))
    End Function


    ''' <summary>
    ''' Lists the permissions companies.
    ''' </summary>
    ''' <returns></returns>
    Public Function ListPermissionCompanyAll(ByVal UserCode As String) As List(Of PermissionCompany) Implements IPermissionCompanyRepository.ListPermissionCompanyAll
        Dim PermissionCompanies = From e In _context.PermissionCompany.Include("Containers")
                                  Where e.User.UserCode = UserCode
                                  Select e
        Dim a As List(Of PermissionCompany)
        a = PermissionCompanies.ToList
        For i As Integer = 0 To a.Count - 1
            a.Item(i).StartTracking()
        Next
        Return a
    End Function

    ''' <summary>
    ''' Metodo para saber si el usuario tiene permiso para la empresa seleccionada
    ''' </summary>
    ''' <param name="UserCode"></param>
    ''' <returns></returns>
    Public Function GetPermissionUserCompany(UserCode As String, containerCode As String) As Boolean Implements IPermissionCompanyRepository.GetPermissionUserCompany

        Dim Busqueda = From e In _context.PermissionCompany
                       Where e.User.UserCode = UserCode And e.Containers.Code = containerCode
                       Select e
        Dim PermissionUserbyCompany As PermissionCompany
        If Busqueda.ToList.Count > 0 Then
            PermissionUserbyCompany = Busqueda.ToList.FirstOrDefault
            Return PermissionUserbyCompany.Permission
        Else
            Return False
        End If
    End Function

    Public Function LoginUserCompany(userCode As String, companyCode As String) As UserLogin Implements IPermissionCompanyRepository.LoginUserCompany
        Using conx As New ConectionSQL(ConfigurationManager.AppSettings(Infrastructure.CrossCutting.Base.ConfigurationFile.SECURITY_CONTAINER_PARAMETER_NAME))
            Dim res As UserLogin = New UserLogin()
            Dim _User = (From _UserAux In _context.User Where _UserAux.UserCode = userCode Select _UserAux).FirstOrDefault
            Dim dt1 As DataTable = Nothing
            If _User IsNot Nothing Then
                Select Case CType(_User.UserType, UserType)
                    Case UserType.GlobalAdmin
                        dt1 = conx.ExecuteCommand_Data("SELECT C.Id, CompanyCode = C.Code, CompanyPermission = 1, IdOperatingUnitDefault = 0, TenantId = CAST(0 AS SMALLINT), GroupId = 0, RollId = 0, " &
                                                   "RollCode = '', RollName = '', Administrator = cast(1 as bit) " &
                                                    "FROM [Security].[Containers] AS C WITH(NOLOCK) WHERE C.State = 1")
                    Case UserType.GlobalQa
                        dt1 = conx.ExecuteCommand_Data("SELECT C.Id, CompanyCode = C.Code, CompanyPermission = 1, IdOperatingUnitDefault = 0, TenantId = CAST(0 AS SMALLINT), GroupId = 0, RollId = 0, " &
                                                   "RollCode = '', RollName = '', Administrator = cast(1 as bit) " &
                                                    "FROM [Security].[Containers] AS C WITH(NOLOCK) WHERE C.State = 1 and ProductionCompany = 0")
                    Case UserType.TenantAdmin
                        dt1 = conx.ExecuteCommand_Data("select C.Id, CompanyCode = C.Code, CompanyPermission = 1, IdOperatingUnitDefault = 0, TU.TenantId, TU.GroupId, TU.RollId, R.RollCode, " &
                                                   "R.[Description] AS RollName, Administrator = cast(1 as bit) " &
                                                   "From[Security].TenantUsers TU WITH(NOLOCK) " &
                                                    "INNER JOIN[Security].TenantContainer TC WITH(NOLOCK) On TC.TenantId = TU.TenantId " &
                                                    "INNER JOIN[Security].Containers C WITH(NOLOCK) ON C.Id = TC.ContainerId " &
                                                    "And C.[State] = 1 " &
                                                    "INNER JOIN [Security].[Roll] R WITH(NOLOCK) ON TU.RollId = R.Id " &
                                                    "WHERE TU.UserId = " & _User.Id & " And TU.[State] = 1")

                    Case Else
                        dt1 = conx.ExecuteCommand_Data("SELECT C.Id, CompanyCode = C.Code, PC.Permission as CompanyPermission, PC.IdOperatingUnitDefault, TU.TenantId, TU.GroupId, TU.RollId, R.RollCode, " &
                                                   "R.[Description] AS RollName, Administrator = ISNULL(PC.Administrator, CAST(1 AS BIT))  " &
                                                   "FROM[Security].PermissionCompany PC WITH(NOLOCK)  " &
                                                   "INNER JOIN[Security].Containers C WITH(NOLOCK) ON C.Id = PC.IdContainer And C.[STATE] = 1  " &
                                                   "INNER JOIN[Security].TenantContainer TC WITH(NOLOCK) ON TC.ContainerId = C.Id  " &
                                                   "INNER JOIN[Security].TenantUsers TU WITH(NOLOCK) ON TU.UserId = PC.IdUser And TU.TenantId = TC.TenantId And TU.[State] = 1  " &
                                                   "INNER JOIN [Security].[Roll] R WITH(NOLOCK) ON TU.RollId = R.Id " &
                                                   "WHERE PC.IdUser = " & _User.Id & " And PC.Permission = 1")
                End Select
                Dim dt As DataTable = conx.ExecuteCommand_Data("SELECT U.Id, U.UserCode, U.ViewForm, U.Position, U.GroupCode, U.UserType, U.CodeInterface, P.Id As IdPerson, U.Password, " &
                                                           "U.DateExpiryAccount, U.IsLockedOut, U.FailedPasswordCount, U.State, R.RollCode, R.Description AS RollName, P.FirstName, " &
                                                           "P.FirstLastName, U.Email,  U.ProfileType " &
                                                       "FROM [Security].[User] AS U " &
                                                       "INNER JOIN [Security].[Roll] AS R ON(U.RollCode = R.Id) " &
                                                       "INNER JOIN [Security].[Person] AS P ON(U.IdPerson = P.Id) " &
                                                       "WHERE U.UserCode = '" & userCode & "'")


                res.CompanyCode = Nothing
                res.CompanyPermission = False
                res.ListCompanyPermissionCode = New List(Of CompanyPermission)
                If dt1.Rows.Count > 0 Then
                    For index As Integer = 0 To dt1.Rows.Count() - 1
                        Dim permission As New CompanyPermission()
                        permission.CompanyCode = dt1.Rows(index)("CompanyCode").ToString()
                        permission.IdOperatingUnitDefault = CInt(dt1.Rows(index)("IdOperatingUnitDefault"))
                        permission.Administrator = CBool(dt1.Rows(index)("Administrator"))
                        res.ListCompanyPermissionCode.Add(permission)
                        If dt1.Rows(index)("CompanyCode").ToString().Equals(companyCode) Then
                            res.CompanyCode = dt1.Rows(index)("CompanyCode").ToString()
                            res.CompanyPermission = CBool(dt1.Rows(index)("CompanyPermission"))
                            res.GroupCode = CInt(dt1.Rows(index)("GroupId"))
                            res.RollCode = dt1.Rows(index)("RollCode").ToString()
                            res.RollName = dt1.Rows(index)("RollName").ToString()
                            res.TenantId = CShort(dt1.Rows(index)("TenantId"))
                        End If
                    Next
                End If
                res.UserExists = False

                If dt.Rows.Count > 0 Then

                    Dim NameConvert As TextInfo = New CultureInfo("es-CO", False).TextInfo

                    res.UserExists = True
                    res.Id = CInt(dt.Rows(0)("Id"))
                    res.UserCode = dt.Rows(0)("UserCode").ToString()
                    res.Password = dt.Rows(0)("Password").ToString()
                    res.UserType = If(IsDBNull(dt.Rows(0)), String.Empty, dt.Rows(0)("UserType").ToString())
                    res.Position = If(IsDBNull(dt.Rows(0)), String.Empty, dt.Rows(0)("Position").ToString())
                    res.CodeInterface = If(IsDBNull(dt.Rows(0)), String.Empty, dt.Rows(0)("CodeInterface").ToString())
                    res.IdPerson = CInt(dt.Rows(0)("IdPerson"))
                    res.ViewForm = CBool(dt.Rows(0)("ViewForm"))
                    res.DateExpiryAccount = If(IsDBNull(dt.Rows(0)("DateExpiryAccount")), New DateTime(2035, 1, 1), CDate(dt.Rows(0)("DateExpiryAccount")))
                    res.IsLockedOut = CBool(dt.Rows(0)("IsLockedOut"))
                    res.FailedPasswordCount = CBool(dt.Rows(0)("FailedPasswordCount"))
                    res.State = CBool(dt.Rows(0)("State"))

                    res.Fullname = String.Concat(NameConvert.ToTitleCase(dt.Rows(0)("FirstName").ToString.Trim), " ", NameConvert.ToTitleCase(dt.Rows(0)("FirstLastName").ToString.Trim))
                    res.Email = dt.Rows(0)("Email").ToString()
                    res.ProfileType = dt.Rows(0)("ProfileType").ToString

                    If CType(_User.UserType, UserType) = UserType.GlobalAdmin OrElse CType(_User.UserType, UserType) = UserType.GlobalQa Then
                        res.GroupCode = CInt(dt.Rows(0)("GroupCode"))
                        res.RollCode = dt.Rows(0)("RollCode").ToString()
                        res.RollName = dt.Rows(0)("RollName").ToString()
                    End If
                End If
            End If
            Return res
        End Using
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="idUser"></param>
    ''' <param name="idCompany"></param>
    ''' <returns></returns>
    Public Function LoginUserCompany(ByVal idUser As Integer, ByVal idCompany As Integer) As UserLogin Implements IPermissionCompanyRepository.LoginUserCompany
        Using conx As New ConectionSQL(ConfigurationManager.AppSettings(Infrastructure.CrossCutting.Base.ConfigurationFile.SECURITY_CONTAINER_PARAMETER_NAME))
            Dim dt1 As DataTable = conx.ExecuteCommand_Data("SELECT DISTINCT C.Id as CompanyId, C.Code AS CompanyCode, PC.Permission AS CompanyPermission, PC.IdOperatingUnitDefault FROM [Security].[User] AS U " &
                                                        "INNER JOIN [Security].[PermissionCompany] AS PC ON(U.Id = PC.IdUser) " &
                                                        "INNER JOIN [Security].[Containers] AS C ON(PC.IdContainer = C.Id) WHERE U.Id = " & idUser & " and PC.Permission = 1")


            Dim dt As DataTable = conx.ExecuteCommand_Data("SELECT DISTINCT U.Id, U.UserCode, U.ViewForm, U.Position, U.GroupCode, U.UserType, U.CodeInterface, P.Id As IdPerson, U.Password, U.DateExpiryAccount, U.IsLockedOut, U.FailedPasswordCount, U.State, R.RollCode, R.Description AS RollName, P.FirstName, P.FirstLastName, E.Email " &
                                                       "FROM [Security].[User] AS U " &
                                                       "INNER JOIN [Security].[Roll] AS R ON(U.RollCode = R.Id) " &
                                                       "INNER JOIN [Security].[Person] AS P ON(U.IdPerson = P.Id) " &
                                                       "INNER JOIN [Security].[Email] AS E ON(P.Id = E.IdPerson) " &
                                                       "WHERE U.Id = " & idUser)

            Dim res As UserLogin = New UserLogin()
            res.CompanyCode = Nothing
            res.CompanyPermission = False
            res.ListCompanyPermissionCode = New List(Of CompanyPermission)
            If dt1.Rows.Count > 0 Then
                For index As Integer = 0 To dt1.Rows.Count() - 1
                    Dim permission As New CompanyPermission()
                    permission.CompanyCode = dt1.Rows(index)("CompanyCode").ToString()
                    permission.IdOperatingUnitDefault = CInt(dt1.Rows(index)("IdOperatingUnitDefault"))
                    res.ListCompanyPermissionCode.Add(permission)
                    If CInt(dt1.Rows(index)("CompanyId")) = idCompany Then
                        res.CompanyCode = dt1.Rows(0)("CompanyCode").ToString()
                        res.CompanyPermission = CBool(dt1.Rows(0)("CompanyPermission"))
                    End If
                Next
            End If
            res.UserExists = False

            If dt.Rows.Count > 0 Then

                Dim NameConvert As TextInfo = New CultureInfo("es-CO", False).TextInfo

                res.UserExists = True
                res.Id = CInt(dt.Rows(0)("Id"))
                res.GroupCode = CInt(dt.Rows(0)("GroupCode"))
                res.UserCode = dt.Rows(0)("UserCode").ToString()
                res.Password = dt.Rows(0)("Password").ToString()
                res.UserType = If(IsDBNull(dt.Rows(0)), String.Empty, dt.Rows(0)("UserType").ToString())
                res.Position = If(IsDBNull(dt.Rows(0)), String.Empty, dt.Rows(0)("Position").ToString())
                res.CodeInterface = If(IsDBNull(dt.Rows(0)), String.Empty, dt.Rows(0)("CodeInterface").ToString())
                res.IdPerson = CInt(dt.Rows(0)("IdPerson"))
                res.ViewForm = CBool(dt.Rows(0)("ViewForm"))
                res.DateExpiryAccount = If(IsDBNull(dt.Rows(0)("DateExpiryAccount")), New DateTime(2035, 1, 1), CDate(dt.Rows(0)("DateExpiryAccount")))
                res.IsLockedOut = CBool(dt.Rows(0)("IsLockedOut"))
                res.FailedPasswordCount = CBool(dt.Rows(0)("FailedPasswordCount"))
                res.State = CBool(dt.Rows(0)("State"))
                res.RollCode = dt.Rows(0)("RollCode").ToString()
                res.RollName = dt.Rows(0)("RollName").ToString()
                res.Fullname = String.Concat(NameConvert.ToTitleCase(dt.Rows(0)("FirstName").ToString.Trim), " ", NameConvert.ToTitleCase(dt.Rows(0)("FirstLastName").ToString.Trim))
                res.Email = dt.Rows(0)("Email").ToString()
            End If
            Return res
        End Using
    End Function

End Class


