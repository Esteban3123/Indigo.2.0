'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Henry Alejandro Vargas Polania
' Created          : 17/02/2015
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

Public Class MRefundPurchase
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

#Region "Methods"
    ''' <summary>
    ''' Guarda o actualiza un Entrance Voucher Devolution
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveEntranceVoucherDevolution(ByVal record As EntranceVoucherDevolution, ByVal idSequense As Int64, ByVal sequenceC As Domain.Entities.InventorySequence) As Task(Of ActionResult(Of EntranceVoucherDevolution))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SaveEntranceVoucherDevolutionAsync(record, idSequense, Me.Indigo.AuditMessageWcf, sequenceC)
    End Function

    ''' <summary>
    ''' Obtiene un Entrance Voucher por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetRefundPurchase(ByVal code As String) As Task(Of Domain.Base.Entities.ActionResult(Of EntranceVoucherDevolution))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetEntranceVoucherDevolutionAsync(code, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene un Entrance Voucher por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetRefundPurchaseById(ByVal id As Integer) As Task(Of EntranceVoucherDevolution)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetEntranceVoucherDevolutionByIdAsync(id)
    End Function

    ''' <summary>
    ''' Consulta los parametros del modulo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetSettingInventory(OperatingUnitId As Integer) As Task(Of SettingInventory)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetSettingInventoryAsync(OperatingUnitId)
    End Function

    ''' <summary>
    ''' Lista los almacenes xpo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListEntranceVoucherByStatus(Company As String, Status As Byte) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListEntranceVoucherByStatus(Status)
    End Function

    ''' <summary>
    ''' Lista los comprobantes de entrada xpo por estado y almacen
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListEntranceVoucherByStatusAndWarehouse(Company As String, Status As Byte, Optional warehouseId As Integer? = Nothing) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListEntranceVoucherByStatusAndWarehouse(Status, warehouseId)
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
    ''' guardar y confirmar una devolucion comprobante de entrada
    ''' </summary>
    ''' <param name="entranceDevolution"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveAndConfirmEntranceVoucherDevolution(entranceDevolution As EntranceVoucherDevolution, idSequense As Integer, action As Integer, ByVal sequenceC As Domain.Entities.InventorySequence) As Task(Of ActionResult(Of Domain.Entities.EntranceVoucherDevolution))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SaveAndConfirmbEntranceVoucherDevolutionAsync(entranceDevolution, Me.Indigo.AuditMessageWcf, idSequense, sequenceC, action)
    End Function
#End Region

End Class
