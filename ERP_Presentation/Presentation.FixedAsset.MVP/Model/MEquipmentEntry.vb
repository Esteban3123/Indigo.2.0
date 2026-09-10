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

Public Class MEquipmentEntry
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
    Public Function ListEquipmentType() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).FixedAsset.ListFixedAssetEquipmentType()
    End Function

    ''' <summary>
    ''' Obtiene el physical
    ''' </summary>
    ''' <returns></returns>
    Public Function GetEquipmentById(Id As Integer) As FixedAssetEquipmentXpo
        Dim filtroConsulta As String = "Id = " & Id
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).FixedAsset.GetCollection(Of FixedAssetEquipmentXpo)(Nothing, filtroConsulta).FirstOrDefault()
    End Function

    ''' <summary>
    ''' lista los Equipos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListFixedAssetItem(Optional AdquisitionType As Integer = 0) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).FixedAsset.ListFixedAssetEquipmentByStatus(True, AdquisitionType)
    End Function


    ''' <summary>
    ''' lista las Sucursales
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
    ''' lista las sucursales
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function listBranchOffice() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).PayrollService.GetBranchOffice()
    End Function

    ''' <summary>
    ''' lista las unidades funcionales por sucursal
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListFunctionalUnitByBranchOffice(ByVal branchOffice As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).PayrollService.GetFunctionalUnitByBranchOffice(branchOffice)
    End Function


    ''' <summary>
    ''' guardar una remision
    ''' </summary>
    ''' <param name="EquipmentCatalog"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveInputRemission(InputRemission As FixedAssetRemissionEntrance, idSequense As Integer, ByVal sequenceC As Domain.Entities.FixedAssetSequence) As Task(Of ActionResult(Of FixedAssetRemissionEntrance))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.SaveFixedAssetRemissionEntranceAsync(InputRemission, idSequense, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Funcion para eliminar la aseguradora
    ''' </summary>
    ''' <param name="Record">The registro.</param>
    ''' <returns></returns>
    Public Async Function DeleteInputRemission(ByVal Record As FixedAssetRemissionEntrance, idSequense As Integer, ByVal sequenceC As Domain.Entities.FixedAssetSequence) As Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.DeleteFixedAssetRemissionEntranceAsync(Record, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtener una marca por su codigo, metodo asincrono
    ''' </summary>
    ''' <param name="code">El codigo de la marca</param>
    ''' <returns>objeto accesorio</returns>
    Public Async Function GetInputRemissionAsync(ByVal code As String) As Task(Of Object)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.GetFixedAssetRemissionEntranceAsync(code)
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
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).MaintenanceService.GetLocation()
    End Function


    ''' <summary>
    ''' lista los Tipos de Inventario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListLocationXpo() As XPCollection
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).FixedAsset.ListFixedAssetLocation()
    End Function

    ''' <summary>
    ''' lista los Tipos de Estado de los Activos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListStatusAsset() As Object
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).FixedAsset.ListFixedAssetStatusAssetByStatus(True)
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

    Public Function ListPartsAccesoriesConsumibles() As Object
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).FixedAsset.ListFixedAssetPartsAccesoriesConsumibles()
    End Function

    Public Async Function GetPartsAccesoriesConsumablesByCodeAsync(ByVal Code As String) As Task(Of FixedAssetPartsAccesoriesConsumables)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.GetPartsAccesoriesConsumablesByCodeAsync(Code, _indigoSessionValues)
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
