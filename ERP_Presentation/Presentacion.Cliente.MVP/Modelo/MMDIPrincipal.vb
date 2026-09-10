'***********************************************************************
' Assembly         : Presentacion.Seguridad.MVP
' Author           : Andres Bonilla
' Created          : 26-03-2011
'
' Last Modified By : Jhon Tovar
' Last Modified On : 01-03-2022
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Librerias Importadas"
Imports System.Globalization
Imports Domain.Security.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.CloudAgent
#End Region

''' <summary>
''' 	Esta Clase es la encargada de administrar los servicios que se van a consumir en el funcional principal
''' </summary>
Public Class MmdiPrincipal
    Implements IDisposable

#Region "variables"
    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Dim indigo As Infrastructure.CrossCutting.Base.SessionValues = SessionValues.Instance
#End Region

#Region "Funciones"

    ''' <summary>
    ''' Funcion para cambiar el modo de visualizacion de los frontales 
    ''' True = Modo Busqueda
    ''' False = Modo Edicion
    ''' </summary>
    ''' <param name="UserCode"></param>
    ''' <param name="ViewMode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ChangeViewModeAsync(ByVal UserCode As String, ViewMode As Boolean) As Threading.Tasks.Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.ChangeUserViewModeAsync(UserCode, ViewMode)
    End Function

    ''' <summary>
    ''' Consultar los contenedores.
    ''' </summary>
    ''' <returns></returns>
    Public Shared Async Function GetContainers() As Threading.Tasks.Task(Of List(Of Containers))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.getContainersAsync(SessionValues.Instance)
    End Function

    '''<summary>
    ''' Consultar contenedor por nombre
    ''' </summary>
    ''' <returns></returns>
    Public Async Function GetContainersByName(Name As String) As Threading.Tasks.Task(Of Containers)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.getContainersByNameAsync(Name, SessionValues.Instance)
    End Function

    Public Async Function GetContainersByCode(Code As String) As Threading.Tasks.Task(Of Containers)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.getContainersByCodeAsync(Code, SessionValues.Instance)
    End Function

    ''' <summary>
    ''' Lista todos los parparametros de configuración de central de mezclas
    ''' </summary>
    ''' <returns>lista de parametros de configuración de central de mezclas</returns>
    Public Function ListAllCMConfig() As List(Of Domain.Entities.CMConfiguration)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.ListAllCMConfig(SessionValues.Instance.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene el nombre del contenedor de seguridad
    ''' </summary>
    ''' <returns>Nombre del contenedor de seguridad</returns>
    Public Async Function GetSecurityContainerNameAsync() As Threading.Tasks.Task(Of String)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.getSecurityContainerNameAsync()
    End Function

    ''' <summary>
    ''' Obtiene el nombre del contenedor de la interaccion con costos
    ''' </summary>
    ''' <returns>Nombre del contenedor de Indigo Crystal HIS</returns>
    Public Async Function GetIndigoConnectionString() As Threading.Tasks.Task(Of String)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.getIndigoConnectionStringAsync()
    End Function

    ''' <summary>
    ''' Obtiene el nombre del contenedor de seguridad
    ''' </summary>
    ''' <returns>Nombre del contenedor de seguridad</returns>
    Public Function GetSecurityContainerName() As String
        Return IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.getSecurityContainerName()
    End Function

    ''' <summary>
    ''' Funcion Consultar el nombre por codigo.
    ''' </summary>
    Public Async Function GetPermissionCompanies() As Threading.Tasks.Task(Of List(Of PermissionCompany))
        'Dim ListCompanies = Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.getContainersAsync(SessionValues.Instance) 'IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetContainersAsync(SessionValues.Instance)
        Dim ListPermission As List(Of Domain.Security.Entities.PermissionCompany) = Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.ListCompaniesPermissionAsync(SessionValues.Instance.UserIndigo, SessionValues.Instance)
        Return ListPermission.Where(Function(x) x.Permission = True).ToList()
    End Function

    Public Function GetOperatingUnitByContainerPermission(idContainer As Integer) As List(Of Domain.Entities.OperatingUnit)
        Dim listPermissionOperating As New List(Of Domain.Entities.OperatingUnit)
        Dim listOperatingUnit = IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.ListAllOperatingUnitCommand(indigo, indigo.TransactionalContainer)
        Select Case indigo.UserType
            Case UserType.GlobalAdmin, UserType.GlobalQa
                listPermissionOperating.AddRange(listOperatingUnit)
            Case UserType.TenantAdmin
                listPermissionOperating.AddRange(listOperatingUnit)
            Case UserType.CompanyAdmin
                Dim _Company = UnifiedConfiguration.Instance.ListCompanies.Where(Function(c) c.Id = idContainer).FirstOrDefault
                'si el usuario es administrador en la compañia se asignan todas las unidades operativas
                If _Company IsNot Nothing AndAlso _Company.Administrator Then
                    listPermissionOperating.AddRange(listOperatingUnit)
                Else
                    'unidades operativas que tiene permiso
                    Dim listPermissionOperatingUnit = IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.ListPermissionOperatingUnitCommand(indigo.UserIndigo, idContainer, indigo)
                    If listOperatingUnit IsNot Nothing AndAlso listPermissionOperatingUnit IsNot Nothing Then
                        Dim linq = From _OperatingUnit In listOperatingUnit
                                   Join _PermissionOperatingUnit In listPermissionOperatingUnit'.Where(Function(_PermissionOperatingUnit) _PermissionOperatingUnit.Status)
                                       On _PermissionOperatingUnit.IdOperatingUnit Equals _OperatingUnit.Id
                                   Select _OperatingUnit
                        listPermissionOperating.AddRange(linq)
                    End If
                End If
            Case UserType.StandardUser
                'unidades operativas que tiene permiso
                Dim listPermissionOperatingUnit = IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.ListPermissionOperatingUnitCommand(indigo.UserIndigo, idContainer, indigo)
                If listOperatingUnit IsNot Nothing AndAlso listPermissionOperatingUnit IsNot Nothing Then
                    Dim linq = From _OperatingUnit In listOperatingUnit
                               Join _PermissionOperatingUnit In listPermissionOperatingUnit'.Where(Function(_PermissionOperatingUnit) _PermissionOperatingUnit.Status)
                                   On _PermissionOperatingUnit.IdOperatingUnit Equals _OperatingUnit.Id
                               Select _OperatingUnit
                    listPermissionOperating.AddRange(linq)
                End If
        End Select
        Return listPermissionOperating
    End Function

    Public Async Function SavePermissionUserAsync(permission As Company) As Threading.Tasks.Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.SavePermissionUserAsync(permission, indigo)
    End Function

    ' ''' <summary>
    ' ''' Funcion Consultar el nombre por codigo.
    ' ''' </summary>
    'Public Shared Async Function getContainers() As Threading.Tasks.Task(Of List(Of Containers))
    '    Dim ListCompanies = Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetContainersAsync(SessionValues.Instance)
    '    Dim ListPermission As List(Of Domain.Security.Entities.PermissionCompany) = Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.ListCompaniesPermissionAsync(SessionValues.Instance.UserIndigo, SessionValues.Instance)
    '    Dim ListCompanyWithPermission As New List(Of CompanyIndigo)
    '    For Each Company As CompanyIndigo In ListCompanies
    '        If ListPermission.Where(Function(x) x.CompanyCode = Company.CompanyCode And x.Permission = True).ToList.Count > 0 Then
    '            ListCompanyWithPermission.Add(Company)
    '        End If
    '    Next
    '    Return ListCompanyWithPermission
    'End Function

    ''' <summary>
    ''' Funcion Consultar el nombre por codigo.
    ''' </summary>
    Public Shared Async Function ConsultarFechaServidor() As Threading.Tasks.Task(Of Date)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoComunes.GetServerDateAsync
    End Function

    '''' <summary>
    '''' Funcion que se utiliza para consultar los modulos y pintarlos en el MDI
    '''' </summary>
    '''' <returns></returns>
    'Public Shared Function ConsultarMenuModulo(CodigoModulo As Integer) As List(Of VieForm)
    '    Dim forms = BaseClass.GetXmlWithAggregates(Of VieForm)(eDataXml.XMLForms)
    '    Dim res = forms.Where(Function(f) f.HasForm AndAlso f.Modules.Any(Function(m) m.Id = CodigoModulo) AndAlso f.Permissions.Any(Function(p) p.Id = 41) AndAlso SessionValues.Instance.IdsFormVisible.Contains(f.Id.ToString())).ToList()
    '    Return res
    'End Function

    ''' <summary>
    ''' Este metodo consulta el listado de formularios con acciones.
    ''' </summary>
    Public Async Function ConsultarTodosForms() As Threading.Tasks.Task(Of List(Of VieForm))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.ListFormsAsync(Me.indigo)
    End Function

    ''' <summary>
    ''' Funcion para consultar las zonas horarias.
    ''' </summary>
    Public Async Function GetTimezone() As Threading.Tasks.Task(Of List(Of Timezone))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.getTimezoneAsync(Me.indigo)
    End Function

    ''' <summary>
    ''' Obtiene  la moneda, parametrizada en los parametros de la compañia
    ''' </summary>
    ''' <returns></returns>
    Public Async Function GetOfficialCurrencyAsync() As Threading.Tasks.Task(Of Domain.Entities.Currency)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetOfficialCurrencyAsync()
    End Function

    ''' <summary>
    ''' Funcion para consultar las zonas horarias.
    ''' </summary>
    Public Async Function GetTimezoneByIdAsync(IdTimeZone As Integer) As Threading.Tasks.Task(Of Timezone)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.getTimezoneByIdAsync(IdTimeZone)
    End Function
#End Region

#Region "FilePerson"

    ''' <summary>
    ''' Obtiene el archivo de una persona por su codigo
    ''' </summary>
    ''' <param name="codePerson">Codigo de la person</param>
    ''' <returns>Archivo de la persona</returns>
    Public Async Function GetFilePersonAsync(ByVal codePerson As String) As Threading.Tasks.Task(Of FilePerson)
        Try
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.GetFilePersonAsync(codePerson)
        Catch ex As Exception

        End Try
    End Function

    ''' <summary>
    ''' Obtiene el archivo de un usuario por su codigo
    ''' </summary>
    ''' <param name="userCode">Codigo del usuario</param>
    ''' <returns>Archivo del usuario</returns>
    Public Async Function GetFilePersonByUserCodeAsync(ByVal userCode As String) As Threading.Tasks.Task(Of FilePerson)
        Try
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.GetFilePersonByUserCodeAsync(userCode)
        Catch ex As Exception

        End Try
    End Function

    ''' <summary>
    ''' Obtiene el archivo de un usuario por su codigo
    ''' </summary>
    ''' <param name="userId">Codigo del usuario</param>
    ''' <returns>Archivo del usuario</returns>
    Public Async Function GetFilePersonByUserIdAsync(ByVal userId As Integer) As Threading.Tasks.Task(Of FilePerson)
        Try
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.GetFilePersonByUserIdAsync(userId)
        Catch ex As Exception

        End Try
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: eliminar estado administrado (objetos administrados).
            End If

            ' TODO: liberar recursos no administrados (objetos no administrados) e invalidar Finalize() below.
            ' TODO: Establecer campos grandes como Null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: invalidar Finalize() sólo si la instrucción Dispose(ByVal disposing As Boolean) anterior tiene código para liberar recursos no administrados.
    'Protected Overrides Sub Finalize()
    '    ' No cambie este código. Ponga el código de limpieza en la instrucción Dispose(ByVal disposing As Boolean) anterior.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' Visual Basic agregó este código para implementar correctamente el modelo descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en Dispose(disposing As Boolean).
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
