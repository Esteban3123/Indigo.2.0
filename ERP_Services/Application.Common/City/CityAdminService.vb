'***********************************************************************
' Assembly         : Application.Common
' Author           : Cristhian Mauricio Salazar
' Created          : 11-04-2013
'
' Last Modified By : Cristhian Mauricio Salazar
' Last Modified On : 16-04-2013

' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Common
Imports Domain.Base
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports System.Data.Entity.Infrastructure
Imports Domain.Entities

Public Class CityAdminService
    Implements ICityAdminService

    ' Repositoria de ciudades
    Private _CityRepository As ICityRepository

    ''' <summary>
    ''' Costructor el cual inicia el repositorio de ciudades
    ''' </summary>
    ''' <param name="repository">Repositorio de ciudades</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal repository As ICityRepository)
        If (repository Is Nothing) Then
            Throw New ArgumentNullException("CityRepository Vacio")
        End If
        _CityRepository = repository
    End Sub

    ''' <summary>
    ''' Elimina una ciudad
    ''' </summary>
    ''' <param name="city">Ciudad</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>True o False</returns>
    ''' <remarks></remarks>
    Public Function DeleteCity(city As Domain.Entities.City, audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionMessageResult(Of Domain.Entities.City) Implements ICityAdminService.DeleteCity
        Dim result As New ActionMessageResult(Of Domain.Entities.City)
        result.StateResult = True
        If (city Is Nothing) Then
            Throw New ArgumentNullException("City Vacio")
        End If
        Dim UnitOfWork As IUnitWork = _CityRepository.UnitWork
        Try
            _CityRepository.DeleteEntity(city)
            UnitOfWork.Commit()
            '/***** Auditoria Basica ********/
            IndigoAuditBasic.Execute(city.GetType.Name, audit.Functional, city.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
            '/*****Auditoria Avanzada ******/
            Dim auditObject As New IndigoAuditSimpleEntity(Of Domain.Entities.City)(city, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            auditObject.Execute()
            Return result
        Catch ex As DbUpdateException
            UnitOfWork.RollbackChanges()
            result.StateResult = False
            result.MessageResult.Add(New MessageResult("c-0000", city.Code))
            Return result
        Catch ex As Exception
            UnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            result.StateResult = False
            Return result
        End Try
    End Function

    ''' <summary>
    ''' Obtiene unaq ciudad en especifico
    ''' </summary>
    ''' <param name="code">Codigo Ciudad</param>
    ''' <returns>Ciudad</returns>
    ''' <remarks></remarks>
    Public Function GetCity(code As String) As Domain.Entities.City Implements ICityAdminService.GetCity
        If (String.IsNullOrEmpty(code) = True) Then
            Throw New ArgumentNullException("CodeCity Vacio")
        End If
        Try
            Return _CityRepository.GetCity(code)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Lista todas las ciudades
    ''' </summary>
    ''' <returns>Lista de Ciudades</returns>
    ''' <remarks></remarks>
    Public Function ListAllCity() As List(Of Domain.Entities.City) Implements ICityAdminService.ListAllCity
        Try
            Return _CityRepository.ListAllCity()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guarda o actualiza una ciudad
    ''' </summary>
    ''' <param name="city">Ciudad que se quiere salvar</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>True o false</returns>
    ''' <remarks></remarks>
    Public Function SaveCity(city As Domain.Entities.City, audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionResult(Of City) Implements ICityAdminService.SaveCity
        If (city Is Nothing) Then
            Throw New ArgumentNullException("City Vacio")
        End If
        Dim UnitOfWork As IUnitWork = _CityRepository.UnitWork
        Try
            Dim AuxCity As Domain.Entities.City = Nothing
            Dim status As Integer
            Dim auditProcess As IndigoAuditSimpleEntity(Of City)
            If city.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                city.ModificationUser = audit.CodeUser
                city.ModificationDate = Date.Now()
                status = Infrastructure.CrossCutting.Audit.Actions.Update
                AuxCity = _CityRepository.GetCityById(city.Id, False)
            Else
                city.CreationUser = audit.CodeUser
                city.CreationDate = Date.Now()
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            End If

            'Valido si se va a guardar o a eliminar
            _CityRepository.SaveEntity(city)
            UnitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of City)(city, audit, status, AuxCity)
            auditProcess.Execute()
            city.MarkAsUnchanged()
            Return New ActionResult(Of City) With {.StatusCode = eStatusResult.SUCCESS, .StateResult = True, .ObjectEmbbeded = city}

            'If city.ChangeTracker.State = ObjectState.Added Then
            '    '/***** Auditoria Basica ********/
            '    IndigoAuditBasic.Execute(city.GetType.Name, audit.Functional, city.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Crear, audit.Company, audit.ContainerSecurity)
            '    '/*****Auditoria Avanzada ******/
            '    IndigoAuditSimpleEntity(Of Domain.Common.Entities.City).Execute(city, audit, Infrastructure.CrossCutting.Audit.Actions.Insert, audit.Company)
            'ElseIf city.ChangeTracker.State = ObjectState.Modified Then
            '    '/***** Auditoria Basica ********/
            '    IndigoAuditBasic.Execute(city.GetType.Name, audit.Functional, city.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Modificar, audit.Company, audit.ContainerSecurity)
            '    '/*****Auditoria Avanzada ******/
            '    IndigoAuditSimpleEntity(Of Domain.Common.Entities.City).Execute(city, audit, Infrastructure.CrossCutting.Audit.Actions.Update, audit.Company, AuxCity)
            'End If
            'Return True
        Catch ex As Exception
            UnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return New ActionResult(Of City) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Retorna una ciudad especifica
    ''' </summary>
    ''' <param name="code">Codigo Ciudad</param>
    ''' <param name="idDepartamento">Id del departamento</param>
    ''' <returns>Ciudad</returns>
    ''' <remarks></remarks>
    Public Function GetCity(code As String, idDepartamento As Integer, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Entities.City Implements ICityAdminService.GetCity
        If (String.IsNullOrEmpty(code) = True) Then
            Throw New ArgumentNullException("CodeCity Vacio")
        End If
        Try
            Dim city = _CityRepository.GetCity(code, idDepartamento)
            If city.Id > 0 Then
                '/*****Auditoria Avanzada ******/
                Dim auditObject As New IndigoAuditSimpleEntity(Of Domain.Entities.City)(city, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
                Return city
            Else
                Return New Domain.Entities.City()
            End If
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una ciudad especifica
    ''' </summary>
    ''' <param name="idCity">Id de la ciudad</param>
    ''' <returns>Ciudad</returns>
    ''' <remarks></remarks>
    Public Function GetCityById(idCity As Integer) As Domain.Entities.City Implements ICityAdminService.GetCityById
        Try
            Return _CityRepository.GetCityById(idCity)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Metodo que lista todas las ciudades dependiendo del departanmento
    ''' </summary>
    ''' <param name="IdDepartment">Id del departamento</param>
    ''' <returns>Lista de ciudades</returns>
    ''' <remarks></remarks>
    Public Function ListAllCitiesByIdDepartment(idDepartment As Integer) As List(Of Domain.Entities.City) Implements ICityAdminService.ListAllCitiesByIdDepartment
        Try
            Return _CityRepository.ListAllCitiesByIdDepartment(idDepartment)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return Nothing
        End Try
    End Function


    Public Function ChangeStateCity(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of City) Implements ICityAdminService.ChangeStateCity
        Dim city As City = _CityRepository.GetCity(code)
        city.State = state
        Return SaveCity(city, audit)
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _CityRepository = Nothing
            IndigoGC.Execute()
        End If
        disposedValue = True
    End Sub

    ' Visual Basic agrega este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
