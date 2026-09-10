'***********************************************************************
' Assembly         : Application.Payments
' Author           : Diego Andres Roldan Lozano
' Created          : 31-03-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Core
Imports Infrastructure.CrossCutting.Resources

Public Class AgesPaymentAdminService
    Implements IAgesPaymentAdminService

    ''' <summary>
    ''' Variable tipo repositorio para edades de pagos
    ''' </summary>
    ''' <remarks></remarks>
    Private _agesPaymentsRepository As IAgesPaymentsRepository

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal agesPaymentsRepository As IAgesPaymentsRepository)
        If agesPaymentsRepository Is Nothing Then
            Throw New ArgumentNullException("agesPaymentsRepository")
        End If
        _agesPaymentsRepository = agesPaymentsRepository
    End Sub

    ''' <summary>
    ''' elimina una edad de pagos
    ''' </summary>
    ''' <param name="agesPayment"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">agesPayment</exception>
    Public Function DeleteAgesPayment(agesPayment As AgesPayments, audit As AuditMessage) As ActionResult Implements IAgesPaymentAdminService.DeleteAgesPayment
        If agesPayment Is Nothing Then
            Throw New ArgumentNullException("agesPayment")
        End If
        Dim unitOfWork As IUnitWork = Me._agesPaymentsRepository.UnitWork
        Try
            If agesPayment.ChangeTracker.State = ObjectState.Deleted Then
                Me._agesPaymentsRepository.DeleteEntity(agesPayment)
                unitOfWork.Commit()
                'Auditoria básica
                IndigoAuditBasic.Execute(GetType(AgesPayments).Name, audit.Functional, agesPayment.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, Infrastructure.CrossCutting.Base.ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
                'Ausitoria avanzada
                Dim auditObject As New IndigoAuditSimpleEntity(Of AgesPayments)(agesPayment, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
                auditObject.Execute()
                Return New ActionResult With {.StateResult = True}
            Else
                Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
            End If
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una edad de pagos
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">id</exception>
    Public Function GetAgesPaymentsById(Id As Integer) As AgesPayments Implements IAgesPaymentAdminService.GetAgesPaymentsById
        If Id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Try
            Return Me._agesPaymentsRepository.GetAgesPaymentsById(Id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' guarda una edad de pagos
    ''' </summary>
    ''' <param name="agesPayment"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">agesPayment</exception>
    Public Function SaveAgesPayment(agesPayment As AgesPayments, audit As AuditMessage) As ActionResult(Of AgesPayments) Implements IAgesPaymentAdminService.SaveAgesPayment
        If agesPayment Is Nothing Then
            Throw New ArgumentNullException("agesPayment")
        End If
        Dim unitOfWork As IUnitWork = Me._agesPaymentsRepository.UnitWork
        Try
            Dim auxAgesPayments = agesPayment.OriginalValue
            If agesPayment.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added OrElse agesPayment.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                Me._agesPaymentsRepository.SaveEntity(agesPayment)
            End If
            unitOfWork.Commit()

            If agesPayment.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                '/***** Auditoria Basica ********/
                IndigoAuditBasic.Execute(GetType(AgesPayments).Name, audit.Functional, agesPayment.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Crear, audit.Company, audit.ContainerSecurity)
                '/***** Auditoria Avanzada ******/
                Dim auditObject As New IndigoAuditSimpleEntity(Of AgesPayments)(agesPayment, audit, Infrastructure.CrossCutting.Audit.Actions.Insert)
                auditObject.Execute()
            ElseIf agesPayment.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                '/***** Auditoria Basica ********/
                IndigoAuditBasic.Execute(GetType(AgesPayments).Name, audit.Functional, agesPayment.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Modificar, audit.Company, audit.ContainerSecurity)
                '/***** Auditoria Avanzada ******/
                Dim auditObject As New IndigoAuditSimpleEntity(Of AgesPayments)(agesPayment, audit, Infrastructure.CrossCutting.Audit.Actions.Update, auxAgesPayments)
                auditObject.Execute()
            End If

            'Se marca la entidad como sin cambios
            agesPayment.MarkAsUnchanged()

            Return New ActionResult(Of AgesPayments) With {.StateResult = True, .ObjectEmbbeded = agesPayment}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of AgesPayments) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of AgesPayments) With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' lista las edades de pagos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAgesPayments() As ActionResult(Of List(Of AgesPayments)) Implements IAgesPaymentAdminService.ListAgesPayments
        Try
            Dim _list As List(Of AgesPayments) = _agesPaymentsRepository.ListAgesPayments()
            Return New ActionResult(Of List(Of AgesPayments)) With {.StateResult = True, .ObjectEmbbeded = _list}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of AgesPayments)) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    ''' <summary>
    ''' lista las edades de pagos por unidad operativa
    ''' </summary>
    ''' <param name="UnitOperativeId">The unit operative identifier.</param>
    ''' <returns></returns>
    Public Function ListAgesPaymentsByUnitOperativeId(UnitOperativeId As Integer) As ActionResult(Of List(Of AgesPayments)) Implements IAgesPaymentAdminService.ListAgesPaymentsByUnitOperativeId
        Try
            Dim _list As List(Of AgesPayments) = _agesPaymentsRepository.ListAgesPaymentsByUnitOperativeId(UnitOperativeId)
            Return New ActionResult(Of List(Of AgesPayments)) With {.StateResult = True, .ObjectEmbbeded = _list}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of AgesPayments)) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _agesPaymentsRepository = Nothing
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