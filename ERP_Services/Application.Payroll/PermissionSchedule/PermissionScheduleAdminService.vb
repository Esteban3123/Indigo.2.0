'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Daniel Eduardo Arévalo
' Created          : 03-12-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll
Imports Domain.Payroll.Entities
Imports Domain.Security
Imports Domain.Security.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports System.Data.Entity.Infrastructure
Imports System.Transactions
Imports Infrastructure.CrossCutting.Resources
Imports System.Data.Entity.Core


Public Class PermissionScheduleAdminService

    Implements IPermissionScheduleAdminService

    ''' <summary>
    ''' Repositorio de la unidad funcional
    ''' </summary>
    ''' <remarks></remarks>
    Private _PermissionRollRepository As IPositionRollRepository

    ''' <summary>
    ''' Repositorio de la unidad funcional
    ''' </summary>
    ''' <remarks></remarks>
    Private _PermissionUserRepository As IPositionUserRepository

    ''' <summary>
    ''' Repositorio de la unidad funcional
    ''' </summary>
    ''' <remarks></remarks>
    Private _FunctionalUnitResponsibleRepository As IFunctionalUnitResponsibleRepository

    ''' <summary>
    ''' Repositorio de usuarios
    ''' </summary>
    ''' <remarks></remarks>
    Private _RoleRepository As IRoleRepository

    ''' <summary>
    ''' Repositorio de Usuarios
    ''' </summary>
    Private _userRepository As IUserRepository

    ''' <summary>
    ''' incia el repositorio de educationLevels
    ''' </summary>
    ''' <param name="repository">Repositorio de educations levels</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal repository As IPositionRollRepository, RoleRepository As IRoleRepository, PermissionUserRepository As IPositionUserRepository,
                   FunctionalUnitResponsible As IFunctionalUnitResponsibleRepository, userRepository As IUserRepository)
        If (repository Is Nothing = True) Then
            Throw New ArgumentNullException("Repositorio de unidad funcional Vacio")
        End If
        _PermissionRollRepository = repository
        _RoleRepository = RoleRepository
        _FunctionalUnitResponsibleRepository = FunctionalUnitResponsible
        _PermissionUserRepository = PermissionUserRepository
        _userRepository = userRepository
    End Sub

    Public Function GetPermissionRoll(CodeRoll As String, RolId As Integer) As List(Of PositionRoll) Implements IPermissionScheduleAdminService.GetPermissionRoll
        If String.IsNullOrEmpty(CodeRoll) Then
            Throw New ArgumentNullException("RolId vacio")
        End If
        Try

            If RolId > 0 Then
                Return _PermissionRollRepository.GetListPositionRollByRole(RolId)
            End If

            If RolId <= 0 And CodeRoll <> String.Empty Then
                Return _PermissionRollRepository.GetListPositionRollByRoleCode(CodeRoll)
            End If

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function SavePermissionRol(ListPermissionRol As List(Of PositionRoll), ListPermissionRolDelete As List(Of PositionRoll), audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionResult(Of List(Of PositionRoll)) Implements IPermissionScheduleAdminService.SavePermissionRol

        If ListPermissionRol Is Nothing Then
            Throw New ArgumentNullException("ListPermissionRol")
        End If
        Dim unitOfWork As IUnitWork = Me._PermissionRollRepository.UnitWork

        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                For Each objPermissionRol As PositionRoll In ListPermissionRol
                    Me._PermissionRollRepository.SaveEntity(objPermissionRol)
                    unitOfWork.Commit()
                Next

                For Each objPermissionRolDelete As PositionRoll In ListPermissionRolDelete
                    If objPermissionRolDelete.Id > 0 Then
                        objPermissionRolDelete.MarkAsDeleted()
                        Me._PermissionRollRepository.SaveEntity(objPermissionRolDelete)
                        unitOfWork.Commit()
                    End If
                Next

                'Se marca la entidad como sin cambios
                scope.Complete()
                Return New ActionResult(Of List(Of PositionRoll)) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = ListPermissionRol, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of List(Of PositionRoll)) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of PositionRoll)) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try

    End Function

    Public Function SavePermissionUser(ListPermissionUser As List(Of PositionUser), ListDeletePermissionUser As List(Of PositionUser), ListPermissionFunctionalUnit As List(Of FunctionalUnitResponsible), ListDeletePermissionFunctionalUnit As List(Of FunctionalUnitResponsible), audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionResult(Of String) Implements IPermissionScheduleAdminService.SavePermissionUser


        Dim unitOfWork As IUnitWork = Me._PermissionUserRepository.UnitWork
        Dim unitOfWorkFunctionalUnit As IUnitWork = Me._FunctionalUnitResponsibleRepository.UnitWork
        Dim Message As String

        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                Dim ListInteger As New List(Of Integer)
                If ListPermissionUser IsNot Nothing AndAlso ListPermissionUser.Count > 0 Then
                    ListInteger.Add(ListPermissionUser.FirstOrDefault().IdUser)

                    For Each objPermissionUser As PositionUser In ListPermissionUser
                        Me._PermissionUserRepository.SaveEntity(objPermissionUser)
                        unitOfWork.Commit()
                    Next
                End If

                For Each objPermissionUserDelete As PositionUser In ListDeletePermissionUser
                    If objPermissionUserDelete.Id > 0 Then
                        objPermissionUserDelete.MarkAsDeleted()
                        Me._PermissionUserRepository.SaveEntity(objPermissionUserDelete)
                        unitOfWork.Commit()
                    End If

                Next

                For Each objFunctionalUnit As FunctionalUnitResponsible In ListPermissionFunctionalUnit
                    Me._FunctionalUnitResponsibleRepository.SaveEntity(objFunctionalUnit)
                    unitOfWorkFunctionalUnit.Commit()
                Next

                For Each objFunctionalUnitDelete As FunctionalUnitResponsible In ListDeletePermissionFunctionalUnit
                    If objFunctionalUnitDelete.Id > 0 Then
                        objFunctionalUnitDelete.MarkAsDeleted()
                        Me._FunctionalUnitResponsibleRepository.SaveEntity(objFunctionalUnitDelete)
                        unitOfWorkFunctionalUnit.Commit()
                    End If

                Next

                'Se marca la entidad como sin cambios
                scope.Complete()
                Return New ActionResult(Of String) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = Message, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of String) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of String) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try

    End Function

    Public Function GetPositionUser(UserId As Integer) As List(Of PositionUser) Implements IPermissionScheduleAdminService.GetPositionUser
        If String.IsNullOrEmpty(UserId) Then
            Throw New ArgumentNullException("UserId vacio")
        End If
        Try
            Return _PermissionUserRepository.GetListPositionUserByUserId(UserId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function GetFunctionalUnitResponsible(UserId As Integer) As List(Of FunctionalUnitResponsible) Implements IPermissionScheduleAdminService.GetFunctionalUnitResponsible
        If String.IsNullOrEmpty(UserId) Then
            Throw New ArgumentNullException("UserId vacio")
        End If
        Try
            Return _FunctionalUnitResponsibleRepository.GetListFunctionalUnitResponibleByUserId(UserId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function


#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: elimine el estado administrado (objetos administrados).
            End If

            ' TODO: libere los recursos no administrados (objetos no administrados) y reemplace Finalize() a continuación.
            ' TODO: configure los campos grandes en nulos.
        End If
        disposedValue = True
    End Sub

    ' TODO: reemplace Finalize() solo si el anterior Dispose(disposing As Boolean) tiene código para liberar recursos no administrados.
    'Protected Overrides Sub Finalize()
    '    ' No cambie este código. Coloque el código de limpieza en el anterior Dispose(disposing As Boolean).
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' Visual Basic agrega este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en el anterior Dispose(disposing As Boolean).
        Dispose(True)
        ' TODO: quite la marca de comentario de la siguiente línea si Finalize() se ha reemplazado antes.
        ' GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
