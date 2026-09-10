#Region "Imports"
Imports DevExpress.Xpo
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Presentation.CloudAgent

#End Region


Public Class MMaintenanceTools
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
    ''' Obtener una tools por su codigo, metodo asincrono
    ''' </summary>
    ''' <param name="code">El codigo de la marca</param>
    ''' <returns>objeto accesorio</returns>
    Public Async Function GetToolsAsync(ByVal code As String) As Task(Of MaintenanceTools)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.GetToolsByCodeAsync(code)
    End Function

    ''' <summary>
    ''' Graba la tools en modo asincrono
    ''' </summary>
    ''' <param name="Tools">Tools</param>
    ''' <returns>Un valor que indica si se grabo la marca</returns>
    Public Async Function SaveToolsAsync(ByVal Tools As MaintenanceTools, ByVal idSequense As Int64) As Task(Of ActionResult(Of MaintenanceTools))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.SaveToolsAsync(Tools, idSequense, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Elimina tools modo asincrono
    ''' </summary>
    ''' <param name="Tools">Tools</param>
    ''' <returns>Un valor que indica si se elimino con exito la Marca</returns>
    Public Async Function DeleteToolsAsync(ByVal Tools As MaintenanceTools) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.DeleteToolsAsync(Tools, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Lista todos los accesorios
    ''' </summary>
    Public Async Function ListAllToolsAsync() As Task(Of List(Of MaintenanceTools))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.ListAllToolsAsync(Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Funcion para obtener el item
    ''' </summary>
    ''' <param name="Code">id de la unidad funcional</param>
    ''' <returns></returns>
    Public Async Function GetItemByCodeAsync(ByVal Code As String) As Task(Of FixedAssetItem)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.GetItemByCodeAsync(Code)
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
