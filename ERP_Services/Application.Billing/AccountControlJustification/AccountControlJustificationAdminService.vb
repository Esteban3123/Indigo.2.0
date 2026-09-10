#Region "Imports"

Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.Data.Entity.Core
Imports Application.Security
Imports System.Transactions
Imports System.Data.Entity.Infrastructure
Imports System.Text
Imports Application.Treasury
Imports System.Threading.Tasks

#End Region

Public Class AccountControlJustificationAdminService
    Implements IAccountControlJustificationAdminService

#Region "Properties"

    'Repositorios
    Private _accountControlJustification As IAccountControlJustificationRepository

#End Region

#Region "Builder"

    Public Sub New(AccountControlJustification As IAccountControlJustificationRepository)
        Me._accountControlJustification = AccountControlJustification
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' funcion para guardar el la tabla las justificaciones por item de control de cuentas Hospitalario
    ''' </summary>
    ''' <param name="_listAccountControlJustification"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function SaveAccountControlJustification(_listAccountControlJustification As List(Of AccountControlJustification), audit As AuditMessage) As ActionResult Implements IAccountControlJustificationAdminService.SaveAccountControlJustification
        If Not _listAccountControlJustification.Any Then
            Throw New Exception("No existen justificaciones para guardar")
        End If
        Dim unitOfWork As IUnitWork = Me._accountControlJustification.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})

                _listAccountControlJustification.ForEach(Sub(x)
                                                             If x.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                                                                 x.CreationUser = audit.CodeUser
                                                                 x.CreationDate = DateTime.Now
                                                             Else

                                                                 x.ModificationUser = audit.CodeUser
                                                                 x.ModificationDate = DateTime.Now
                                                             End If
                                                             Me._accountControlJustification.SaveEntity(x)
                                                         End Sub)
                unitOfWork.Commit()
                scope.Complete()
                Return New ActionResult With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .Message = "Los Items Fueron Justificados Exitosamente"}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList,
                .Message = IIf(String.IsNullOrEmpty(ex?.InnerException?.InnerException?.Message),
                               IndigoManagementExceptions.GetExceptionDetails(ex),
                               IIf(ex?.InnerException?.InnerException?.Message.Contains("UNIQUE"), "Algunos items ya cuentan con Justificación, por favor refrescar el formulario", ex?.InnerException?.InnerException?.Message))}
        End Try
    End Function



#End Region


#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
            End If

            Me._accountControlJustification = Nothing
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
