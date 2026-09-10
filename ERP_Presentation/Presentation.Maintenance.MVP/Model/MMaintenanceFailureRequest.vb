'***********************************************************************
' Assembly         : Presentation.Maintenance
' Author           : Johan Sebastian Cuellar Esquivel
' Created          : 04-02-2021
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
Imports Infrastructure.Data.Xpo.FixedAssetRepository
#End Region

''' <summary>
''' Modelo que se comunica con los servicios corresporndientes al funcional
''' </summary>
Public Class MMaintenanceFailureRequest
    Implements IDisposable

    ''' <summary>
    ''' Variable que contiene la instancia de la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues
    ''' <summary>
    ''' Tago del formulario
    ''' </summary>
    Private _tagForm As String

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

#Region "Methods"

    ''' <summary>
    ''' Obtener una marca por su codigo, metodo asincrono
    ''' </summary>
    ''' <param name="code">El codigo de la marca</param>
    ''' <returns>objeto accesorio</returns>
    Public Async Function GetMaintenanceFailureRequestByCodeAsync(ByVal code As String) As Task(Of MaintenanceFailureRequest)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.GetMaintenanceFailureRequestByCodeAsync(code)
    End Function

    ''' <summary>
    ''' Graba la marca en modo asincrono
    ''' </summary>
    ''' <param name="MaintenanceFailureRequest">MaintenanceFailureRequest</param>
    ''' <returns>Un valor que indica si se grabo la marca</returns>
    Public Async Function SaveMaintenanceFailureRequestAsync(ByVal MaintenanceFailureRequest As MaintenanceFailureRequest, ByVal idSequense As Int64) As Task(Of ActionResult(Of MaintenanceFailureRequest))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.SaveMaintenanceFailureRequestAsync(MaintenanceFailureRequest, idSequense, Me.Indigo.AuditMessageWcf)
    End Function


    ''' <summary>
    ''' Confirma
    ''' </summary>
    ''' <param name="MaintenanceFailureRequest"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ConfirmMaintenanceFailureRequestAsync(ByVal MaintenanceFailureRequest As MaintenanceFailureRequest, ByVal idSequense As Int64) As Task(Of ActionResult(Of MaintenanceFailureRequest))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.ConfirmMaintenanceFailureRequestAsync(MaintenanceFailureRequest, idSequense, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Metodo para cambiar de estado la entidad
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <param name="State"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ChangeState(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of MaintenanceResponsible))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.ChangeStateMaintenanceResponsibleAsync(code, state, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Inicializa el datasource 
    ''' </summary>
    ''' <remarks></remarks>
    Public Function InitializeItem() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.ListFixedAssetPhysicalAsset()
    End Function

    Public Function InitializeParts() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.ListFixedAssetPhysicalAssetParts()
    End Function

    ''' <summary>
    ''' Obtiene el physical
    ''' </summary>
    ''' <returns></returns>
    Public Function GetPhysicalById(Id As Integer) As FixedAssetFixedAssetPhysicalAssetXpo
        Dim filtroConsulta As String = "Id = " & Id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.GetCollection(Of FixedAssetFixedAssetPhysicalAssetXpo)(Nothing, filtroConsulta).FirstOrDefault()
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


