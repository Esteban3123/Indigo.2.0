#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Domain.Entities
Imports Domain.Base.Entities

#End Region

Public Class MFixedAssetCatalogOfPropertyandServices
    Implements IDisposable

    ''' <summary>
    ''' Variable que contiene la instancia de la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues

    ''' <summary>
    ''' Tago del formulario
    ''' </summary>
    Private _tagForm As String


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
    ''' Obtener una catalogo de bienes y servicios por su codigo, metodo asincrono
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns>objeto accesorio</returns>
    Public Async Function GetFixedAssetCatalogOfPropertyandServicesByCode(ByVal Code As String) As Task(Of ActionResult(Of FixedAssetCatalogOfPropertyandServices))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.GetFixedAssetCatalogOfPropertyandServicesByCodeAsync(Code, Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' guarda un catalgo de bienes y servicios
    ''' </summary>
    ''' <param name="FixedAssetCatalogOfPropertyandServices">Trademark</param>
    ''' <returns></returns>
    Public Async Function SaveItem(ByVal FixedAssetCatalogOfPropertyandServices As FixedAssetCatalogOfPropertyandServices, ByVal idSequense As Int64) As Task(Of ActionResult(Of FixedAssetCatalogOfPropertyandServices))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.SaveFixedAssetCatalogOfPropertyandServicesAsync(FixedAssetCatalogOfPropertyandServices, idSequense, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Elimina item modo asincrono
    ''' </summary>
    ''' <param name="FixedAssetCatalogOfPropertyandServices">Trademark</param>
    ''' <returns></returns>
    Public Async Function DeleteItem(ByVal FixedAssetCatalogOfPropertyandServices As FixedAssetCatalogOfPropertyandServices) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.DeleteFixedAssetCatalogOfPropertyandServicesAsync(FixedAssetCatalogOfPropertyandServices, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <param name="State"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ChangeStateItem(ByVal Code As String, ByVal State As Boolean) As Task(Of ActionResult(Of FixedAssetCatalogOfPropertyandServices))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.ChangeFixedAssetCatalogOfPropertyandServicesStatusAsync(Code, State, Me.Indigo.AuditMessageWcf)
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
