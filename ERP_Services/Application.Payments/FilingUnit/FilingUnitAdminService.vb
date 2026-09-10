'***********************************************************************
' Assembly         : Application.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 02/03/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Security
Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Core
Imports Infrastructure.CrossCutting.Resources

Public Class FilingUnitAdminService
    Implements IFilingUnitAdminService

    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _filingUnitRepository As IFilingUnitRepository
    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDRepository As ISequensePaymentsDRepository

    ''' <summary>
    ''' Repositorio a usuarios
    ''' </summary>
    Private _userAdminService As IUserAdminService

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal filingUnitRepository As IFilingUnitRepository, ByVal secuenseDRepository As ISequensePaymentsDRepository, ByVal userAdminService As IUserAdminService)
        If filingUnitRepository Is Nothing Then
            Throw New ArgumentNullException("filingUnitRepository Vacio")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        _filingUnitRepository = filingUnitRepository
        Me._secuenseDRepository = secuenseDRepository
        Me._userAdminService = userAdminService
    End Sub

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChangeState(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of FilingUnit) Implements IFilingUnitAdminService.ChangeState
        Dim filingUnit As FilingUnit = _filingUnitRepository.GetFilingUnit(code)
        filingUnit.Status = state
        Return SaveFilingUnit(filingUnit, audit)
    End Function

    ''' <summary>
    ''' Elimina una unidad de radicacion
    ''' </summary>
    ''' <param name="FilingUnit"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteFilingUnit(FilingUnit As FilingUnit, audit As AuditMessage) As ActionResult Implements IFilingUnitAdminService.DeleteFilingUnit
        If FilingUnit Is Nothing Then
            Throw New ArgumentNullException("FilingUnit")
        End If
        Dim unitOfWork As IUnitWork = Me._filingUnitRepository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of FilingUnit)
            auditProcess = New IndigoAuditSimpleEntity(Of FilingUnit)(FilingUnit, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            Me._filingUnitRepository.DeleteEntity(FilingUnit)
            unitOfWork.Commit()
            auditProcess.Execute()
            Return New ActionResult With {.StateResult = True}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"}), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una unidad de radicacion por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFilingUnit(code As String, audit As AuditMessage) As ActionResult(Of FilingUnit) Implements IFilingUnitAdminService.GetFilingUnit
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim filingUnit As FilingUnit = Me._filingUnitRepository.GetFilingUnit(code.Trim())
            If filingUnit IsNot Nothing AndAlso filingUnit.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of FilingUnit)(filingUnit, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            If filingUnit.FilingUnitUser IsNot Nothing AndAlso filingUnit.FilingUnitUser.Count > 0 Then
                Dim listUserId As List(Of Integer) = New List(Of Integer)
                For Each user In filingUnit.FilingUnitUser
                    listUserId.Add(user.UserId)
                Next
                Dim listUsers = _userAdminService.ListUsersByIds(listUserId)
                For Each fu In filingUnit.FilingUnitUser
                    Dim user = (From e In listUsers Where e.Id = fu.UserId Select e).FirstOrDefault
                    If user IsNot Nothing AndAlso user.Id > 0 AndAlso user.Person IsNot Nothing Then
                        fu.FullNameUser = user.Person.Fullname
                    End If
                Next
            End If
            Return New ActionResult(Of FilingUnit) With {.StateResult = True, .ObjectEmbbeded = filingUnit}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of FilingUnit) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una unidad de radicacion por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFilingUnitById(id As String, audit As AuditMessage) As ActionResult(Of FilingUnit) Implements IFilingUnitAdminService.GetFilingUnitById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim filingUnit As FilingUnit = Me._filingUnitRepository.GetFilingUnitById(id)
            If filingUnit IsNot Nothing AndAlso filingUnit.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of FilingUnit)(filingUnit, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of FilingUnit) With {.StateResult = True, .ObjectEmbbeded = filingUnit}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of FilingUnit) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Guarda o actualiza la unidad de radicacion
    ''' </summary>
    ''' <param name="FilingUnit"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveFilingUnit(FilingUnit As FilingUnit, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of FilingUnit) Implements IFilingUnitAdminService.SaveFilingUnit
        If FilingUnit Is Nothing Then
            Throw New ArgumentNullException("FilingUnit")
        End If
        Dim unitOfWork As IUnitWork = Me._filingUnitRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Dim seq As PaymentsSecuenceDetail = Nothing
            If FilingUnit.Code Is Nothing OrElse FilingUnit.Code.Trim().Equals(String.Empty) Then
                seq = Me._secuenseDRepository.GetSequenseDById(idSequense)
                If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.PaymentsSecuence.Sequential Then
                    Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                    If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                        FilingUnit.Code = res
                        seq.Next += 1
                        Me._secuenseDRepository.SaveEntity(seq)
                    Else
                        Return New ActionResult(Of FilingUnit) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
                    End If
                Else
                    Return New ActionResult(Of FilingUnit) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
                End If
            End If

            Dim auxFilingUnit As FilingUnit = Nothing
            Dim auditProcess As IndigoAuditSimpleEntity(Of FilingUnit)
            Dim status As Integer

            If FilingUnit.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                FilingUnit.CreationUser = audit.CodeUser
                FilingUnit.CreationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            Else
                auxFilingUnit = FilingUnit.OriginalValue
                FilingUnit.ModificationUser = audit.CodeUser
                FilingUnit.ModificationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Update
            End If

            Me._filingUnitRepository.SaveEntity(FilingUnit)
            unitOfWork.Commit()
            sequenseUnitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of FilingUnit)(FilingUnit, audit, status, auxFilingUnit)
            auditProcess.Execute()

            'Se marca la entidad como sin cambios
            FilingUnit.MarkAsUnchanged()

            Return New ActionResult(Of FilingUnit) With {.StateResult = True, .ObjectEmbbeded = FilingUnit}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of FilingUnit) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of FilingUnit) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene las unidades de radicacion
    ''' </summary>
    ''' <param name="userCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFilingUnitByUser(userCode As String) As ActionResult(Of List(Of FilingUnit)) Implements IFilingUnitAdminService.GetFilingUnitByUser
        If String.IsNullOrEmpty(userCode) Then
            Throw New ArgumentNullException("userCode")
        End If
        Try
            Dim listFilingUnit As List(Of FilingUnit) = Me._filingUnitRepository.GetFilingUnitByUser(userCode.Trim())

            Return New ActionResult(Of List(Of FilingUnit)) With {.StateResult = True, .ObjectEmbbeded = listFilingUnit}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of FilingUnit)) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene las unidades de radicacion que tiene permiso un usuario
    ''' </summary>
    ''' <param name="userCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFilingUnitByUserPermission(userCode As String) As ActionResult(Of List(Of FilingUnitUser)) Implements IFilingUnitAdminService.GetFilingUnitByUserPermission
        If String.IsNullOrEmpty(userCode) Then
            Throw New ArgumentNullException("userCode")
        End If
        Try
            Dim listFilingUnit As List(Of FilingUnitUser) = Me._filingUnitRepository.GetFilingUnitByUserPermission(userCode.Trim())

            Return New ActionResult(Of List(Of FilingUnitUser)) With {.StateResult = True, .ObjectEmbbeded = listFilingUnit}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of FilingUnitUser)) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _filingUnitRepository = Nothing
            _secuenseDRepository = Nothing
            _userAdminService = Nothing
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
