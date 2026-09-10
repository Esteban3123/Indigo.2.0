'***********************************************************************
' Assembly         : Application.Budget
' Author           : Oscar Ivan Sierra  Jaramillo
' Created          : 21-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "imports"
Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Transactions
Imports System.Data.Entity.Core
Imports System.Text

#End Region

Public Class LowTaxLiquidationAdminService
    Implements ILowTaxLiquidationAdminService

#Region "Fields"

    'Repositorio para la liquidación de impuestos
    Private _lowtaxesLiquidationRepository As ILowTaxesLiquidationRepository
    'repositori de secuencia de commons
    Private _ConsecutiveRepository As IConsecutiveRepository

#End Region

#Region "Constructor"
    Public Sub New(lowtaxesLiquidationRepository As ILowTaxesLiquidationRepository, ByVal ConsecutiveRepository As IConsecutiveRepository)
        _lowtaxesLiquidationRepository = lowtaxesLiquidationRepository
        _ConsecutiveRepository = ConsecutiveRepository
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    '''
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetLowTaxLiquidation(consecutive As String, audit As AuditMessage) As LowTaxLiquidation Implements ILowTaxLiquidationAdminService.GetLowTaxLiquidation
        If String.IsNullOrEmpty(consecutive) Then
            Throw New ArgumentNullException("consecutive")
        End If
        'If audit Is Nothing Then
        '    Throw New ArgumentNullException("audit")
        'End If
        Try
            Dim Entity As LowTaxLiquidation = Me._lowtaxesLiquidationRepository.GetLowTaxLiquidationByConsecutive(consecutive)

            Return Entity
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function SaveLowTaxLiquidation(Entity As LowTaxLiquidation, state As Integer, audit As AuditMessage) As ActionResult(Of LowTaxLiquidation) Implements ILowTaxLiquidationAdminService.SaveLowTaxLiquidation
        If Entity Is Nothing Then
            Throw New ArgumentNullException("Entity")
        End If
        Dim unitOfWork As IUnitWork = Me._lowtaxesLiquidationRepository.UnitWork
        Dim unitOfWorkConsecutive As IUnitWork = TryCast(_ConsecutiveRepository.UnitWork, IUnitWork)

        'Se arma el mensaje que se va a devover
        Dim messages As New StringBuilder
        Try
            'configuro la transaccion
            Dim txSettings As New TransactionOptions()
            txSettings.Timeout = TransactionManager.DefaultTimeout
            txSettings.IsolationLevel = IsolationLevel.ReadCommitted
            'inicio la transaccion
            Using scope As New TransactionScope(TransactionScopeOption.Required, txSettings)
                Dim status As Integer
                Dim auxLowLiquidation As LowTaxLiquidation = Nothing
                If Entity.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then

                    Dim consecutive As Domain.Entities.Consecutive = Nothing
                    consecutive = _ConsecutiveRepository.GetConsecutiveByCode("11") 'consecutivo de liquidacion de impuesto menores
                    'Actualizamos el numero de consecutivo
                    consecutive.NumberConsecutive += 1
                    _ConsecutiveRepository.SaveEntity(consecutive)
                    unitOfWorkConsecutive.Commit()

                    Entity.Consecutive = consecutive.NumberConsecutive
                    Entity.CreationUser = audit.CodeUser
                    Entity.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    Entity.ModificationUser = audit.CodeUser
                    Entity.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                    auxLowLiquidation = Entity.OriginalValue
                End If


                If state = 2 Then
                    'confirmamos
                    Entity.Status = 2
                    Entity.ConfirmationUser = audit.CodeUser
                    Entity.ConfirmationDate = DateTime.Now
                    Me._lowtaxesLiquidationRepository.SaveEntity(Entity)
                    unitOfWork.Commit()
                    'ejecutamo sp para crear cuenta por cobrar, comprobante contable
                    Dim result As SP_ConfirmLowTaxesLiquidation_Result = _lowtaxesLiquidationRepository.SP_ConfirmLowTaxesLiquidation(Entity.Id, audit.CodeUser)
                    If result.State = 0 Then
                        scope.Dispose()
                        Return New ActionResult(Of LowTaxLiquidation) With {.Message = result.Message, .StatusCode = eStatusResult.WARNING, .StateResult = False}
                    End If

                    messages.AppendLine("Se confirmó correctamente")
                    '   messages.AppendLine("Se generaron facturas con números: " + result.MessagesInvoiceNumber)
                    messages.AppendLine("Se genero cuenta por cobrar con código: " + result.MessagesAccountReceivableCode)
                    messages.AppendLine("Se genero comprobantes contables: " + result.MessagesJournalVoucher)
                Else
                    Me._lowtaxesLiquidationRepository.SaveEntity(Entity)
                    unitOfWork.Commit()
                End If

                Dim auditProcess As New IndigoAuditSimpleEntity(Of LowTaxLiquidation)(Entity, audit, status, auxLowLiquidation)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                Entity.MarkAsUnchanged()
                scope.Complete()
            End Using

            Return New ActionResult(Of LowTaxLiquidation) With {.StateResult = True, .Message = messages.ToString(), .ObjectEmbbeded = Entity, .StateResultAux = True}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of LowTaxLiquidation) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of LowTaxLiquidation) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
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
            _lowtaxesLiquidationRepository = Nothing
            _ConsecutiveRepository = Nothing
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
