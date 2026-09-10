#Region "Imports"
Imports DevExpress.Xpo
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Presentation.CloudAgent

#End Region


Public Class MGlosaMedicalFees
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
    ''' Obtiene una cxp por Id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetAccountPayableById(ByVal Id As Integer) As AccountPayable
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayments.GetAccountPayableById(Id, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' lista los proveedores xpo 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListSupplierMaintenanceXPO(Optional SupplierId As Integer? = Nothing) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).MaintenanceService.GetSupplier(SupplierId)
    End Function


    Public Function ListAccountPayablebysupplier(SupplierId As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).PaymentsService.ListAccountPayablebysupplier(SupplierId)
    End Function

    Public Function ListGlosaMedicalFeesConcepts() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).MedicalFeesService.ListGlosaMedicalFeesConcepts()
    End Function

    ''' <summary>
    ''' Obtener un contrato por codigo
    ''' </summary>
    ''' <param name="code">El codigo de la marca</param>
    ''' <returns>objeto accesorio</returns>
    Public Async Function GetGlosaMedicalFeesByCodeAsync(ByVal Code As String) As Task(Of GlosaMedicalFees)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMedicalFees.GetGlosaMedicalFeesByCodeAsync(Code)
    End Function

    ''' <summary>
    ''' Graba la marca en modo asincrono
    ''' </summary>
    ''' <param name="GlosaMedicalFees">responsible</param>
    ''' <returns>Un valor que indica si se grabo la marca</returns>
    Public Async Function SaveGlosaMedicalFeesAsync(ByVal GlosaMedicalFees As GlosaMedicalFees, ByVal idSequense As Int64) As Task(Of ActionResult(Of GlosaMedicalFees))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMedicalFees.SaveGlosaMedicalFeesAsync(GlosaMedicalFees, idSequense, Me._indigoSessionValues.AuditMessageWcf)
    End Function


    ''' <summary>
    ''' Lista todos los accesorios
    ''' </summary>
    Public Async Function ListAllMedicalFeesAsync() As Task(Of List(Of GlosaMedicalFees))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMedicalFees.ListAllGlosaMedicalFeesAsync(Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Funcion para obtener placa por el Id
    ''' </summary>
    ''' <param name="Id">id de la unidad funcional</param>
    ''' <returns></returns>
    Public Async Function GetAccountPayableByIdAsync(ByVal Id As Integer) As Task(Of AccountPayable)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMedicalFees.GetAccountPayableByIdAsync(Id)
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
