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

Public Class MInventoryControl
    Implements IDisposable

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

#Region "Fields"
    '' <summary>
    ''' Variable que contiene la instancia de la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues

    ''' <summary>
    ''' Tago del formulario
    ''' </summary>
    Private _tagForm As String

    ''' <summary>
    ''' Referencia a los valores de session
    ''' </summary>
    Private _indigoSessionValues As SessionValues
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
        _indigoSessionValues = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"
    ''' <summary>
    ''' Lista los detalles del control de inventario
    ''' </summary>
    ''' <param name="inventoryControlId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListInventoryControlDetailByInventoryControlId(inventoryControlId As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).InventoryService.ListInventoryControlDetailByInventoryControlId(inventoryControlId)
    End Function
    ''' <summary>
    ''' Lista los detalles de los detalles del control de inventario
    ''' </summary>
    ''' <param name="inventoryControlDetailId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListInventoryControlDetailBatchSerialByInventoryControlDetailId(inventoryControlDetailId As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).InventoryService.ListInventoryControlDetailBatchSerialByInventoryControlDetailId(inventoryControlDetailId)
    End Function
    ''' <summary>
    ''' Lista los almacenes xpo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListWarehouseByStatusAndUser(Company As String, Status As Boolean, User As String) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).InventoryService.ListOwnWarehouseByStatusAndUser(Status, User)
    End Function

    ''' <summary>
    ''' Lista los almacenes de custodia xpo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListWarehouseByStatusUserCustodyStore(Company As String, Status As Boolean, User As String) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).InventoryService.ListCustodyWarehouseByStatusAndUser(Status, User)
    End Function

    ''' <summary>
    ''' Lista los almacenes de control xpo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListControlWarehouseByStatusAndUser(Company As String, Status As Boolean, User As String) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).InventoryService.ListControlWarehouseByStatusAndUser(Status, User)
    End Function

    ''' <summary>
    ''' Lista el inventario físico por almacenes xpo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListPhysicalInventoryByWarehouseId(WarehouseId As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).InventoryService.ListPhysicalInventoryByWarehouseId(WarehouseId)
    End Function

    ''' <summary>
    ''' Lista el inventario físico de custodia por control de ingreso y por almacenes xpo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListPhysicalInventoryCustodyByWarehouseId(AdmissionNumber As String, WarehouseId As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).InventoryService.ListPhysicalInventoryCustodyByWarehouseId(AdmissionNumber, WarehouseId)
    End Function

    ''' <summary>
    ''' Lista el inventario físico de custodia por control de ingreso y por almacenes xpo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListPhysicalInventoryCustodyByWarehouseIdPatientCodeAdmission(warehouseId As Integer, patientCode As String, admission As String) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).InventoryService.ListPhysicalInventoryCustodyByWarehouseIdPatientCodeAdmission(warehouseId, patientCode, admission)
    End Function

    ''' <summary>
    ''' Lista el inventorycontrol por tipo
    ''' </summary>
    ''' <param name="ControlType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListInventoryControlByTypeXpo(ControlType As Byte) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).InventoryService.ListInventoryControlByDocumentType(ControlType)
    End Function

    ''' <summary>
    ''' Obtiene el producto en el inventario físico
    ''' </summary>
    ''' <param name="ProductId"></param>
    ''' <param name="WarehouseId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPhysicalInventoryByProductAndWarehouse(ProductId As Integer, WarehouseId As Integer, Optional isInput As Boolean = False) As List(Of PhysicalInventory)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetListPhysicalInventory(ProductId, WarehouseId, isInput)
    End Function

    ''' <summary>
    ''' Consulta un inventorycontrol por codigo
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetInventoryControl(Code As String) As Task(Of ActionResult(Of InventoryControl))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetInventoryControlAsync(Code, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Consulta un inventorycontrol por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetInventoryControlById(ByVal id As Integer) As Task(Of InventoryControl)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetInventoryControlByIdAsync(id)
    End Function

    ''' <summary>
    ''' Consulta un inventorycontrol por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetInventoryControlByIdSimple(ByVal id As Integer) As InventoryControl
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetInventoryControlById(id)
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="status"></param>
    ''' <returns></returns>
    Public Async Function GetInventoryControlByIdAndStatusAsync(ByVal id As Integer, status As Byte) As Task(Of ActionResult(Of InventoryControl))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetInventoryControlByIdAndStatusAsync(id, status)
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Async Function GetInventoryAdjustmentControlByInventoryAdjustmentId(ByVal id As Integer) As Task(Of ActionResult(Of List(Of InventoryControlDetailBatchSerial)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetInventoryAdjustmentControlByInventoryAdjustmentIdAsync(id)
    End Function

    ''' <summary>
    ''' Guardar y actualzia un registro de inventorycontrol
    ''' </summary>
    ''' <param name="inventoryControl"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveInventoryControl(inventoryControl As InventoryControl, idSequense As Int64) As Task(Of ActionResult(Of InventoryControl))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SaveInventoryControlAsync(inventoryControl, idSequense, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guardar/Actualzia y Confirma un registro de inventory control
    ''' </summary>
    ''' <param name="InventoryControl"></param>
    ''' <param name="idSequense"></param>
    ''' <param name="action"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveAndConfirmInventoryControl(InventoryControl As InventoryControl, idSequense As Integer, action As Integer) As Task(Of ActionResult(Of Domain.Entities.InventoryControl))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SaveAndConfirmbInventoryControlAsync(InventoryControl, Me.Indigo.AuditMessageWcf, idSequense, action)
    End Function

    ''' <summary>
    ''' metodo para validar el archivo de excel
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetProductsInventoryControlImportFile(data As List(Of ImportFileRow), warehouseId As Integer, controlType As Integer, documentDate As Date, operatingUnitId As Integer) As ActionResult(Of List(Of InventoryControlDetail))
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SetProductsInventoryControlImportFile(data, warehouseId, controlType, documentDate, operatingUnitId, Me.Indigo.AuditMessageWcf)
    End Function
    ''' <summary>
    ''' metodo para validar los items pegados en la rejilla
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SetProductsInventoryControlCopyPaste(data As List(Of List(Of String)), warehouseId As Integer, controlType As Integer) As Task(Of ActionResult(Of List(Of InventoryControlDetail)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SetProductsInventoryControlCopyPasteAsync(data, warehouseId, controlType, Me.Indigo.AuditMessageWcf)
    End Function
    ''' <summary>
    ''' obtiene un control de inventario por id sin agregados, solo con el original value
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetInventoryControlByIdNoAdded(id As Integer) As InventoryControl
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetInventoryControlByIdNoAdded(id)
    End Function
    ''' <summary>
    ''' lista los detalles del control de inventario
    ''' </summary>
    ''' <param name="inventoryControlId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetInventoryControlDetailByInventoryControlId(inventoryControlId As Integer) As Task(Of List(Of InventoryControlDetail))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetInventoryControlDetailByInventoryControlIdAsync(inventoryControlId)
    End Function

    ''' <summary>
    ''' metodo para obtener el control de inventario sin agregados, solo con el original value
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetInventoryControlByCodeNoAdded(code As String) As Task(Of InventoryControl)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetInventoryControlByCodeNoAddedAsync(code, Me.Indigo.AuditMessageWcf)
    End Function
#End Region

End Class
