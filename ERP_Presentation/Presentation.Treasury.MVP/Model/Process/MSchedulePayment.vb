'***********************************************************************
' Assembly         : Presentacion.Treasury.MVP
' Author           : Diego Andrés Roldán Lozano
' Created          : 21-08-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Presentation.CloudAgent
Imports Domain.Base.Entities
Imports Presentation.Base
Imports System.ServiceModel
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.TreasuryRepository
#End Region

Public Class MSchedulePayment
    Implements IDisposable

#Region "Fields"

    ''' <summary>
    ''' Referencia a los valores de session
    ''' </summary>
    Private _indigoSessionValues As SessionValues

    ''' <summary>
    ''' Id del frontal
    ''' </summary>
    Private _tagForm As String

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New(ByVal tag As String)
        Me._tagForm = tag
        Me._indigoSessionValues = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Guarda una programacion de pagos
    ''' </summary>
    ''' <param name="schedulePayment">The schedule payment.</param>
    ''' <returns></returns>
    Public Async Function SaveSchedulePayment(ByVal schedulePayment As SchedulePayment, ByVal withConfirm As Boolean, ByVal idSequence As Int64) As Task(Of ActionResult(Of SchedulePayment))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.SaveSchedulePaymentAsync(schedulePayment, withConfirm, Me._indigoSessionValues.AuditMessageWcf, idSequence)
    End Function

    ''' <summary>
    ''' Confirms the schedule payment.
    ''' </summary>
    ''' <param name="schedulePaymentId">The schedule payment identifier.</param>
    ''' <returns></returns>
    Public Async Function ConfirmSchedulePayment(ByVal schedulePaymentId As Integer) As Task(Of ActionResult(Of String))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.ConfirmSchedulePaymentAsync(schedulePaymentId, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Elimina una programacion de pagos
    ''' </summary>
    ''' <param name="schedulePayment">The schedule payment.</param>
    ''' <returns></returns>
    Public Async Function DeleteSchedulePayment(ByVal schedulePayment As SchedulePayment) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.DeleteSchedulePaymentAsync(schedulePayment, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene una programacion de pagos por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Async Function GetSchedulePayment(ByVal code As String, tracking As Boolean) As Task(Of ActionResult(Of SchedulePayment))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetSchedulePaymentAsync(code, tracking, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene una dispersión por egreso
    ''' </summary>
    ''' <param name="VoucherTransaction"></param>
    ''' <returns></returns>
    Public Async Function GetSchedulePaymentByVoucherTransaction(VoucherTransaction As VoucherTransaction) As Task(Of ActionResult(Of SchedulePayment))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetSchedulePaymentByVoucherTransactionAsync(VoucherTransaction)
    End Function

    ''' <summary>
    ''' Gets the schedule payment datasource with payment.
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Async Function GetSchedulePaymentDatasourceWithPayment(ByVal code As String, Optional FlagDispersionFunds As Boolean = False) As Task(Of ActionResult(Of List(Of SP_SchedulePayment_Result)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetSchedulePaymentDatasourceWithPaymentAsync(code, Me._indigoSessionValues.AuditMessageWcf, FlagDispersionFunds)
    End Function

    ''' <summary>
    ''' Gets the schedule payment datasource by supplier type asynchronous.
    ''' </summary>
    ''' <param name="supplierTypeId">The supplier type identifier.</param>
    ''' <returns></returns>
    Public Async Function GetSchedulePaymentDatasourceBySupplierTypeAsync(supplierTypeId As Integer, code As String) As Task(Of ActionResult(Of List(Of SP_SchedulePayment_Result)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetSPSchedulePaymentBySupplierTypeIdAsync(supplierTypeId, code)
    End Function

    Public Async Function IsAccountPayableShareInSchedulePaymentDetailActive(AccountPayableShareId As Integer) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.IsAccountPayableShareInSchedulePaymentDetailActiveAsync(AccountPayableShareId)
    End Function

    ''' <summary>
    ''' Obtiene una programacion de pagos por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Public Async Function GetSchedulePaymentById(id As Integer) As Task(Of SchedulePayment)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetSchedulePaymentByIdAsync(id)
    End Function

    ''' <summary>
    ''' Obtiene los datos de la programacion de pagos
    ''' </summary>
    ''' <returns></returns>
    Public Async Function GetSPSchedulePayment(supplierTypeListId As String) As Task(Of ActionResult(Of List(Of SP_SchedulePayment_Result)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetSPSchedulePaymentAsync(supplierTypeListId)
    End Function

    ''' <summary>
    ''' Lista las detalles de la interfaz presupuestal de una programación de pagos
    ''' </summary>
    ''' <param name="SchedulePaymentDetailId"></param>
    ''' <returns></returns>
    Public Function ListSchedulePaymentDetailBudgetBySchedulePaymentDetailId(SchedulePaymentDetailId As Integer) As XPCollection
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).TreasuryService.ListSchedulePaymentDetailBudgetBySchedulePaymentDetailId(SchedulePaymentDetailId)
    End Function

    ''' <summary>
    ''' funcion para calcular el descuento de CxP
    ''' </summary>
    ''' <param name="ListSchedulePayment"></param>
    ''' <param name="PaymentDate"></param>
    ''' <returns></returns>
    Public Async Function CalculateDiscountByPaymentDate(ListSchedulePayment As List(Of SP_SchedulePayment_Result), PaymentDate As DateTime) As Task(Of ActionMessageResult(Of List(Of SP_SchedulePayment_Result)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.CalculateDiscountByPaymentDateAsync(ListSchedulePayment, PaymentDate)
    End Function

    ''' <summary>
    ''' funcion que lista el datasource de las cuentas bancarias por proveedor
    ''' </summary>
    ''' <param name="SupplierId"></param>
    ''' <returns></returns>
    Public Async Function SupplierBankAccountDataSource(SupplierId As Integer) As Task(Of List(Of SupplierBankAccountXpo))
        Return Await Task.Factory.StartNew(Function() As List(Of SupplierBankAccountXpo)
                                               Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).TreasuryService.GetCollection(Of SupplierBankAccountXpo)(Nothing, $"SupplierId = {SupplierId}").ToList()
                                           End Function)
    End Function
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: desechar estado administrado (objetos administrados).
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

    ' Visual Basic agregó este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en Dispose(disposing As Boolean).
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class