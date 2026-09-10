'***********************************************************************
' Assembly         : Presentacion.FixedAsset.MVP
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 20-01-2016
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
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.FixedAssetRepository
Imports DevExpress.Xpo
Imports System.Dynamic

#End Region

Public Class MFixedAssetPurchaseOrder
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
    ''' lista los proveedores por con sus lineas de distribucion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListSuppliersDistributionLines() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).CommonService.ListSuppliersDistributionLines()
    End Function

    ''' <summary>
    ''' lista los Tipos de Equipo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListEquipmentType()
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).FixedAsset.ListAllEquipmentType()
    End Function

    ''' <summary>
    ''' lista los Equipos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListEquipment() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).FixedAsset.ListFixedAssetEquipment()
    End Function

    ''' <summary>
    ''' Listado de los equipos por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListFixedAssetItem() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).FixedAsset.ListFixedAssetEquipmentByStatus(True)
    End Function

    ''' <summary>
    ''' lista las Marcas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListTrademark() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).FixedAsset.ListTrademark()
    End Function

    ''' <summary>
    ''' lista las Polizas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPoliza() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).FixedAsset.ListFixedAssetPoliza()
    End Function

    ''' <summary>
    ''' lista los Tipos de Inventario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListInventoryType() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).FixedAsset.ListFixedAssetInventoryType()
    End Function

    ''' <summary>
    ''' lista los IVA
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListIVA() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).InventoryService.ListGeneralLedgerIva()
    End Function

    
    ''' <summary>
    ''' guardar una remision
    ''' </summary>
    ''' <param name="EquipmentCatalog"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SavePurchaseOrder(PurchaseOrder As FixedAssetPurchaseOrder, idSequense As Integer, ByVal sequenceC As Domain.Entities.FixedAssetSequence) As Task(Of ActionResult(Of FixedAssetPurchaseOrder))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.SaveFixedAssetPurchaseOrderAsync(PurchaseOrder, idSequense, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Funcion para eliminar la aseguradora
    ''' </summary>
    ''' <param name="Record">The registro.</param>
    ''' <returns></returns>
    Public Async Function DeletePurchaseOrder(ByVal Record As FixedAssetPurchaseOrder, idSequense As Integer, ByVal sequenceC As Domain.Entities.FixedAssetSequence) As Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.DeleteFixedAssetPurchaseOrderAsync(Record, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtener una marca por su codigo, metodo asincrono
    ''' </summary>
    ''' <param name="code">El codigo de la marca</param>
    ''' <returns>objeto accesorio</returns>
    Public Async Function GetFixedAssetPurchaseOrderByCode(ByVal code As String) As Task(Of ActionResult(Of FixedAssetPurchaseOrder))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.GetFixedAssetPurchaseOrderAsync(code)
    End Function

    ''' <summary>
    ''' lista los Tipos de Inventario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListResponsible() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).FixedAsset.ListFixedAssetResponsible()
    End Function

    ''' <summary>
    ''' lista los Tipos de Inventario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListLocation() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).MaintenanceService.getlocation()
    End Function

    ''' <summary>
    ''' Obtener una marca por su codigo, metodo asincrono
    ''' </summary>
    ''' <param name="code">El codigo de la marca</param>
    ''' <returns>objeto accesorio</returns>
    Public Async Function GetResponsibleFunctionalUnitAsync(ByVal IdUser As Integer) As Task(Of Object)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.GetFuncionalUnitByIdUserAsync(IdUser)
    End Function

    ''' <summary>
    ''' Obtener una marca por su codigo, metodo asincrono
    ''' </summary>
    ''' <param name="code">El codigo de la marca</param>
    ''' <returns>objeto accesorio</returns>
    Public Async Function GetPartsAccesoriesConsumablesAsync(ByVal IdEquipmentType As Integer) As Task(Of Object)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.GetPartsAccesoriesConsumablesByEquipmentTypeAsync(IdEquipmentType)
    End Function

    ''' <summary>
    ''' Desconfirma el documento
    ''' </summary>
    ''' <param name="fixedAssetPurchaseOrder"></param>
    ''' <returns></returns>
    Public Async Function UnconfirmPurchaseOrder(fixedAssetPurchaseOrder As FixedAssetPurchaseOrder) As Task(Of ActionResult(Of Domain.Entities.FixedAssetPurchaseOrder))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.UnconfirmPurchaseOrderAsync(fixedAssetPurchaseOrder, Me._indigoSessionValues.AuditMessageWcf)
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
