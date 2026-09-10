'***********************************************************************
' Assembly         : Application.Glosas
' Author           : RafaelPatiño
' Created          : 09-04-2013
'
' Last Modified By : RafaelPatiño
' Last Modified On : 2013-09-22
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports System.Transactions

Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Common.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports System.Data.Entity.Core
Imports Infrastructure.CrossCutting.Base
Imports Domain.InterfaceERPGlosa

Public Class PartialPaymentsDAdminService
    Implements IPartialPaymentsDAdminService

    Private _PartialPaymentsDRepository As IPartialPaymentsDRepository
    Private _PartialPaymentsMovementRepository As IPartialPaymentsMovementRepository
    Private _PortfolioGlosadaRepository As IPortfolioGlosadaRepository


    Public Sub New(ByVal PartialPaymentsDRepository As IPartialPaymentsDRepository, ByVal PartialPaymentsMovementRepository As IPartialPaymentsMovementRepository, PortfolioGlosadaRepository As IPortfolioGlosadaRepository)
        If PartialPaymentsDRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio de detalle pagos parciales Vacio")
        End If
        If PartialPaymentsMovementRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio de movimientos pagos parciales Vacio")
        End If
        If PortfolioGlosadaRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio de cartera de glosa Vacio")
        End If
        _PartialPaymentsDRepository = PartialPaymentsDRepository
        _PortfolioGlosadaRepository = PortfolioGlosadaRepository
        _PartialPaymentsMovementRepository = PartialPaymentsMovementRepository
    End Sub

    ''' <summary>
    ''' Eliminacion masiva de facturas en oficio de pago parcial
    ''' </summary>
    ''' <param name="tmpList"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeletePartialPaymentsD(tmpList As List(Of PartialPaymentsD), audit As AuditMessage) As ActionResult Implements IPartialPaymentsDAdminService.DeletePartialPaymentsD
        If tmpList.Count = 0 Then
            Throw New ArgumentNullException("lista de factura de pago parcial Vacio")
        End If
        Dim unitOfWork As IUnitWork = _PartialPaymentsDRepository.UnitWork
        Dim unitofWorkPortfolio As IUnitWork = _PortfolioGlosadaRepository.UnitWork
        Try
            'configuro la transaccion
            Dim txSettings As New TransactionOptions()
            txSettings.Timeout = TransactionManager.DefaultTimeout
            txSettings.IsolationLevel = IsolationLevel.ReadCommitted

            'inicio la transaccion
            Using scope As New TransactionScope(TransactionScopeOption.Required, txSettings)
                For Each item As PartialPaymentsD In tmpList
                    item.State = 4 'anulo factura 
                    'actualizamos estado en cartera, marcamos esa factura en estado '13 pendiente confirmar pago parcial
                    Dim InvoicePortfolio As GlosaPortfolioGlosada = _PortfolioGlosadaRepository.GetPortfolioGlosada(item.InvoiceNumber)
                    Dim State = InvoicePortfolio.State
                    InvoicePortfolio.State = InvoicePortfolio.TempState 'como anulamos, procedemos a devolver estado en el que se encontraba
                    InvoicePortfolio.TempState = State
                    _PortfolioGlosadaRepository.SaveEntity(InvoicePortfolio)
                    'Elimino la factura
                    item.MarkAsDeleted()
                    _PartialPaymentsDRepository.DeleteEntity(item)
                    '/*****Auditoria Avanzada ******/
                    Dim auditObject As New IndigoAuditSimpleEntity(Of PartialPaymentsD)(item, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
                    auditObject.Execute()
                Next
                unitofWorkPortfolio.Commit()
                unitOfWork.Commit()
                'confirmo la transaccion
                scope.Complete()
            End Using
            Return New ActionResult With {.StateResult = True}
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

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _PartialPaymentsDRepository = Nothing
            _PortfolioGlosadaRepository = Nothing
            _PartialPaymentsMovementRepository = Nothing
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
