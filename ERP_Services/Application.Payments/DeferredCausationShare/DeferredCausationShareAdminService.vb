'***********************************************************************
' Assembly         : Application.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 01/04/2014
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
Imports System.Transactions

Public Class DeferredCausationShareAdminService
    Implements IDeferredCausationShareAdminService

    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _deferredCausationShareRepository As IDeferredCausationShareRepository

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal deferredCausationShareRepository As IDeferredCausationShareRepository)
        If deferredCausationShareRepository Is Nothing Then
            Throw New ArgumentNullException("deferredCausationShareRepository Vacio")
        End If
        _deferredCausationShareRepository = deferredCausationShareRepository
    End Sub

    ''' <summary>
    ''' Elimina una cuota de causacion diferida
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteDeferredCausationShare(deferredCausationShare As DeferredCausationShare) As ActionResult Implements IDeferredCausationShareAdminService.DeleteDeferredCausationShare
        If deferredCausationShare Is Nothing Then
            Throw New ArgumentNullException("deferredCausationShare")
        End If
        Dim unitOfWork As IUnitWork = Me._deferredCausationShareRepository.UnitWork
        Try
            If deferredCausationShare.ChangeTracker.State = ObjectState.Deleted Then
                Me._deferredCausationShareRepository.DeleteEntity(deferredCausationShare)
                unitOfWork.Commit()
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
            Return New ActionResult With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una cuota de la causacion diferida
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDeferredCausationShareById(id As Integer) As ActionResult(Of DeferredCausationShare) Implements IDeferredCausationShareAdminService.GetDeferredCausationShareById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Try
            Dim deferredCausationShare As DeferredCausationShare = _deferredCausationShareRepository.GetDeferredCausationShareById(id)
            Return New ActionResult(Of DeferredCausationShare) With {.StateResult = True, .ObjectEmbbeded = deferredCausationShare}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of DeferredCausationShare) With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Guarda o actualiza una cuota de  causacion diferida
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveDeferredCausationShare(deferredCausationShare As DeferredCausationShare) As ActionResult(Of DeferredCausationShare) Implements IDeferredCausationShareAdminService.SaveDeferredCausationShare
        If deferredCausationShare Is Nothing Then
            Throw New ArgumentNullException("deferredCausationShare")
        End If
        Dim unitOfWork As IUnitWork = Me._deferredCausationShareRepository.UnitWork
        Try

            Me._deferredCausationShareRepository.SaveEntity(deferredCausationShare)
            unitOfWork.Commit()


            'Se marca la entidad como sin cambios
            deferredCausationShare.MarkAsUnchanged()

            Return New ActionResult(Of DeferredCausationShare) With {.StateResult = True, .ObjectEmbbeded = deferredCausationShare}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of DeferredCausationShare) With {.StateResult = False, .Message = "-999"}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of DeferredCausationShare) With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene el listado de cuotas de causacion que tiene asociado la cxp
    ''' </summary>
    ''' <param name="accountPayableId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDeferredCausationShareByAccountPayableId(accountPayableId As Integer) As ActionResult(Of List(Of DeferredCausationShare)) Implements IDeferredCausationShareAdminService.GetDeferredCausationShareByAccountPayableId
        If accountPayableId = 0 Then
            Throw New ArgumentNullException("accountPayableId")
        End If
        Try
            Dim ListDeferredCausationShare As List(Of DeferredCausationShare) = _deferredCausationShareRepository.GetDeferredCausationShareByAccountPayableId(accountPayableId)
            If ListDeferredCausationShare IsNot Nothing Then
                Return New ActionResult(Of List(Of DeferredCausationShare)) With {.StateResult = True, .ObjectEmbbeded = ListDeferredCausationShare}
            Else
                Return New ActionResult(Of List(Of DeferredCausationShare)) With {.StateResult = False}
            End If
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of DeferredCausationShare)) With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Actualiza el valor de las cuotas de causacion diferida
    ''' </summary>
    ''' <param name="ListDeferredCausationShare"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveListDeferredCausationShare(ListDeferredCausationShare As List(Of DeferredCausationShare), ByVal audit As AuditMessage) As ActionResult(Of List(Of DeferredCausationShare)) Implements IDeferredCausationShareAdminService.SaveListDeferredCausationShare
        If ListDeferredCausationShare Is Nothing Then
            Throw New ArgumentNullException("ListDeferredCausationShare")
        End If
        Using Transaction As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
            Try
                For Each itemDeferredCausationShare As DeferredCausationShare In ListDeferredCausationShare
                    itemDeferredCausationShare.ModificationUser = audit.CodeUser
                    itemDeferredCausationShare.ModificationDate = DateTime.Now
                    Dim resultSaveDeferredCausationShare As ActionResult(Of DeferredCausationShare) = SaveDeferredCausationShare(itemDeferredCausationShare)
                    If resultSaveDeferredCausationShare.StateResult = False Then
                        Transaction.Dispose()
                        Return New ActionResult(Of List(Of DeferredCausationShare)) With {.StateResult = False, .Message = "Error al actualizar las cuotas de la causación diferida."}
                    End If
                Next

                Transaction.Complete()
                Return New ActionResult(Of List(Of DeferredCausationShare)) With {.StateResult = True, .ObjectEmbbeded = ListDeferredCausationShare}
            Catch ex As Exception
                Transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of List(Of DeferredCausationShare)) With {.StateResult = False, .Message = ex.Message}
            End Try
        End Using
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _deferredCausationShareRepository = Nothing
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
