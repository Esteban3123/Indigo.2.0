'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Henry Alejandro Vargas Polania
' Created          : 14/01/2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Domain.Entities
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo
#End Region

Public Class MEntranceVoucher
    Implements IDisposable

#Region "Fields"

    '' <summary>
    ''' Variable que contiene la instancia de la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues
    ''' <summary>
    ''' Tago del formulario
    ''' </summary>
    Private _tagForm As String
#End Region

#Region "Builder"

    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="Tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(Tag As String)
        Me._tagForm = Tag
        Indigo = SessionValues.Instance
        Me.Indigo.AuditMessageWcf.Functional = Me._tagForm
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Funcion para obtener el tercero 
    ''' </summary>
    ''' <param name="Nit">Codigo del tercero</param>
    ''' <returns></returns>
    Public Async Function GetThirdPartyAsync(ByVal Nit As String) As Task(Of ThirdParty)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetThirdPartyByNitAsync(Nit, Indigo)
    End Function

    ''' <summary>
    ''' Obtiene un Entrance Voucher por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetEntranceVoucher(ByVal code As String) As Task(Of Domain.Base.Entities.ActionResult(Of EntranceVoucher))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetEntranceVoucherAsync(code, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene un Entrance Voucher por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetEntranceVoucherById(ByVal id As Integer) As Task(Of EntranceVoucher)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetEntranceVoucherByIdAsync(id)
    End Function

    ''' <summary>
    ''' Obtiene un Entrance Voucher por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetEntranceVoucherByIdSimple(ByVal id As Integer) As EntranceVoucher
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetEntranceVoucherById(id)
    End Function

    ''' <summary>
    ''' Obtiene el porcentaje de ICA que maneja la linea de distribucion
    ''' </summary>
    ''' <param name="idSupplierDistributionLine"></param>
    ''' <param name="OperatingUnitId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetICARetentionConceptBySupplierDistributionLine(ByVal idSupplierDistributionLine As Integer, ByVal OperatingUnitId As Integer) As Task(Of RetentionConcepts)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetICARetentionConceptBySupplierDistributionLineAsync(idSupplierDistributionLine, OperatingUnitId, Indigo)
    End Function

    ''' <summary>
    ''' Obtiene el porcentaje de retencion de iva que maneja el proveedor
    ''' </summary>
    ''' <param name="SupplierId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetIVARetentionPercentageBySupplierId(SupplierId As Integer) As Task(Of Decimal)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.GetIVARetentionPercentageBySupplierIdAsync(SupplierId)
    End Function

    ''' <summary>
    ''' Guarda o actualiza un Entrance Voucher
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveEntranceVoucher(ByVal record As EntranceVoucher, ByVal idSequense As Int64, ByVal sequenceC As Domain.Entities.InventorySequence) As Task(Of ActionResult(Of EntranceVoucher))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SaveEntranceVoucherAsync(record, idSequense, Me.Indigo.AuditMessageWcf, sequenceC)
    End Function

    ''' <summary>
    ''' guardar y confirmar un comprobante de entrada
    ''' </summary>
    ''' <param name="EntranceVoucher"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveAndConfirmEntranceVoucher(entranceVoucher As EntranceVoucher, idSequense As Integer, action As Integer, ByVal sequenceC As Domain.Entities.InventorySequence, Optional controlCost As Boolean = False) As Task(Of ActionResult(Of Domain.Entities.EntranceVoucher))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SaveAndConfirmbEntranceVoucherAsync(entranceVoucher, Indigo.HisContainer, Me.Indigo.AuditMessageWcf, idSequense, sequenceC, action, controlCost)
    End Function

    ''' <summary>
    ''' Elimina un ContractType
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteEntranceVoucher(ByVal record As EntranceVoucher) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.DeleteEntranceVoucherAsync(record, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ChangeState(ByVal code As String, ByVal state As Byte) As Task(Of ActionResult(Of EntranceVoucher))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.ChangeStateEntranceVoucherAsync(code, state, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' lista los proveedores xpo 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListSupplierMaintenanceXPO() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).MaintenanceService.GetSupplier()
    End Function

    ''' <summary>
    ''' Lista los almacenes xpo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListWarehouseByStatusAndUser(Status As Boolean, User As String) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListOwnAndConsignmentWarehouseByStatusAndUser(Status, User)
    End Function

    ''' <summary>
    ''' Consulta los parametros del modulo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetSettingInventory(OperatingUnitdId As Integer) As Task(Of SettingInventory)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetSettingInventoryAsync(OperatingUnitdId)
    End Function

    ''' <summary>
    ''' Consulta las retenciones y deduciones
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ListOtherRetentionAndDeduction() As Task(Of ActionResult(Of List(Of OtherWithholdingDeduction)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.ListOtherWithholdingDeductionAsync()
    End Function

    ''' <summary>
    ''' Consulta los productos del comprobante desde el batch
    ''' </summary>
    ''' <param name="EntranceVoucherId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDetailEntranceVoucherWithBatchSerialByIdEntranceVoucher(EntranceVoucherId As Integer) As List(Of EntranceVoucherDetailBatchSerial)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetDetailEntranceVoucherWithBatchSerialByIdEntranceVoucher(EntranceVoucherId)
    End Function

    Public Function ListPurchaseRemissionEntrance(SupplierId As Int32, SupplierDistributionLineId As Int32, warehouseId As Int32) As XPCollection
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListRemissionEntranceDetailBatchSerialBySupplierIdAndSupplierDistributionLineIdAndCodeUserAndWarehouseId(SupplierId, SupplierDistributionLineId, warehouseId)
    End Function

    Public Function ListPurchaseConsignmentInventoryRemission(SupplierId As Int32, SupplierDistributionLineId As Int32, warehouseId As Int32) As XPCollection
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListConsignmentInventoryRemissionDetailBatchSerialBySupplierIdAndSupplierDistributionLineIdAndCodeUserAndWarehouseId(SupplierId, SupplierDistributionLineId, warehouseId)
    End Function

    Public Function ListConsignmentInventoryRemissionWithoutLegalize(SupplierId As Int32, SupplierDistributionLineId As Int32, warehouseId As Int32) As XPCollection
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListConsignmentInventoryRemissionWithoutLegalize(SupplierId, SupplierDistributionLineId, warehouseId)
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
