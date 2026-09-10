#Region "Imports"
Imports DevExpress.Xpo
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Presentation.CloudAgent

#End Region


Public Class MFixedAssetResponsible
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
    ''' lista los proveedores por con sus lineas de distribucion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListThirdPartyXpo() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).CommonService.GetThirdParty()
    End Function

    ''' <summary>
    ''' lista los Tipos de Equipo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListVinculationType() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).FixedAsset.ListFixedAssetVinculationType()
    End Function

    ''' <summary>
    ''' lista las Marcas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListResponsibleType() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).FixedAsset.ListFixedAssetResponsibleType()
    End Function

    ''' <summary>
    ''' lista las Polizas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListFunctionalUnit() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).PayrollService.GetFunctionalUnit()
    End Function

    ''' <summary>
    ''' Obtener una marca por su codigo, metodo asincrono
    ''' </summary>
    ''' <param name="code">El codigo de la marca</param>
    ''' <returns>objeto accesorio</returns>
    Public Async Function GetResponsibleAsync(ByVal code As String) As Task(Of FixedAssetResponsible)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.GetResponsibleByCodeAsync(code)
    End Function

    ''' <summary>
    ''' Graba la marca en modo asincrono
    ''' </summary>
    ''' <param name="responsible">responsible</param>
    ''' <returns>Un valor que indica si se grabo la marca</returns>
    Public Async Function SaveResponsibleAsync(ByVal responsible As FixedAssetResponsible, ByVal idSequense As Int64) As Task(Of ActionResult(Of FixedAssetResponsible))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.SaveResponsibleAsync(responsible, idSequense, Me._indigoSessionValues.AuditMessageWcf)
    End Function
    ''' <summary>
    ''' Elimina Marca modo asincrono
    ''' </summary>
    ''' <param name="Responsible">Responsible</param>
    ''' <returns>Un valor que indica si se elimino con exito la Marca</returns>
    Public Async Function DeleteResponsibleAsync(ByVal Responsible As FixedAssetResponsible) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.DeleteResponsibleAsync(Responsible, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Metodo para cambiar de estado la entidad
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <param name="State"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>


    Public Async Function ChangeState(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of FixedAssetResponsible))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.ChangeStateFixedAssetResponsibleAsync(code, state, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Lista todos los accesorios
    ''' </summary>
    Public Async Function ListAllResponsibleAsync() As Task(Of List(Of FixedAssetResponsible))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.ListAllResponsibleAsync(Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Funcion para obtener la unidad Funcional por el id
    ''' </summary>
    ''' <param name="id">id de la unidad funcional</param>
    ''' <returns></returns>
    Public Async Function GetFunctionalUnitByIdAsync(ByVal id As Integer) As Task(Of FunctionalUnit)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.GetFunctionalUnitByCodeAsync(id)
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
