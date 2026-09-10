#Region "Imports"
Imports DevExpress.Xpo
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Presentation.CloudAgent

#End Region


Public Class MMaintenanceContract
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

#Region "Builder"

    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="Tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(Tag As String)
        Me._tagForm = Tag
        _indigoSessionValues = SessionValues.Instance
        Me._indigoSessionValues.AuditMessageWcf.Functional = Me._tagForm
    End Sub

#End Region


#Region "Methods"
    ''' <summary>
    ''' lista los tipos de contrato XPO
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListContracTypeXPO(ByVal Status As Boolean) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).InventoryService.ListInventoryContractTypeByStatus(Status)
    End Function

    ''' <summary>
    ''' lista los proveedores xpo 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListSupplierMaintenanceXPO(Optional SupplierId As Integer? = Nothing) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).MaintenanceService.GetSupplier(SupplierId)
    End Function

    ''' <summary>
    ''' lista los lineas de distribución xpo 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListSuppliersDistributionLines(Optional SupplierId As Integer? = Nothing) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).CommonService.ListSuppliersDistributionLines(SupplierId)
    End Function

    ''' <summary>
    ''' lista las Placas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPhysicalAsset() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).FixedAsset.ListAllFixedAssetPhysicalAsset()
    End Function

    ''' <summary>
    ''' Obtener un contrato por codigo
    ''' </summary>
    ''' <param name="code">El codigo de la marca</param>
    ''' <returns>objeto accesorio</returns>
    Public Async Function GetMaintenanceContractByCodeAsync(ByVal code As String) As Task(Of MaintenanceContract)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.GetMaintenanceContractByCodeAsync(code)
    End Function

    ''' <summary>
    ''' Graba la marca en modo asincrono
    ''' </summary>
    ''' <param name="MaintenanceContract">responsible</param>
    ''' <returns>Un valor que indica si se grabo la marca</returns>
    Public Async Function SaveMaintenanceContractAsync(ByVal MaintenanceContract As MaintenanceContract, ByVal idSequense As Int64) As Task(Of ActionResult(Of MaintenanceContract))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.SaveMaintenanceContractAsync(MaintenanceContract, idSequense, Me._indigoSessionValues.AuditMessageWcf)
    End Function


    ''' <summary>
    ''' Lista todos los accesorios
    ''' </summary>
    Public Async Function ListAllMaintenanceContractAsync() As Task(Of List(Of MaintenanceContract))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.ListAllMaintenanceContractAsync(Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Funcion para obtener placa por el Id
    ''' </summary>
    ''' <param name="Id">id de la unidad funcional</param>
    ''' <returns></returns>
    Public Async Function GetPhysicalAssetByIdAsyn(ByVal Id As Integer) As Task(Of FixedAssetPhysicalAsset)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.GetPhysicalAssetByIdAsync(Id)
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
