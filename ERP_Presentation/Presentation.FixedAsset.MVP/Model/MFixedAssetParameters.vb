'***********************************************************************
' Assembly         : Presentacion.FixedAsset.MVP
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 27-01-2016
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Import"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Presentation.Base
Imports Infrastructure.Data.Xpo
Imports DevExpress.Xpo

#End Region

Public Class MSettingFixedAsset
    Implements IDisposable


    ''' <summary>
    ''' Referencia a los valores de session
    ''' </summary>
    Private _indigoSessionValues As SessionValues

    ''' <summary>
    ''' Id del frontal
    ''' </summary>
    Private _tagForm As String

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New(ByVal tag As String)
        Me._tagForm = tag
        Me._indigoSessionValues = SessionValues.Instance
    End Sub

    Public Sub New()
        Me._indigoSessionValues = SessionValues.Instance
    End Sub

#End Region


    ''' <summary>
    ''' Funcion para obtener la aseguradora
    ''' </summary>
    ''' <returns></returns>
    Public Async Function GetSettingFixedAssetByOperatingUnitId(OperatingUnitId As Integer) As Task(Of SettingFixedAsset)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.GetSettingFixedAssetByOperatingUnitIdAsync(OperatingUnitId, _indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Funcion para guardar la aseguradora
    ''' </summary>
    ''' <param name="Record">The registro.</param>
    ''' <returns></returns>
    Public Async Function SaveParameters(ByVal Record As SettingFixedAsset) As Task(Of ActionResult(Of SettingFixedAsset))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.SaveSettingFixedAssetAsync(Record, _indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Funcion para eliminar la aseguradora
    ''' </summary>
    ''' <param name="Record">The registro.</param>
    ''' <returns></returns>
    Public Async Function DeleteParameters(ByVal Record As SettingFixedAsset) As Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.DeleteSettingFixedAssetAsync(Record, _indigoSessionValues.AuditMessageWcf)
    End Function

    Public Function ListJournalVoucher() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).AccountingService.ListJournalVoucherByState(True)
    End Function

    Public Function ListAccounts() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).AccountingService.ListAccountsByLevel(5, True, 0)
    End Function

    Public Function ListThirdParty() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).CommonService.GetThirdParty()
    End Function

    ''' <summary>
    ''' Lista todas las monedas del formulario Currency
    ''' </summary>
    ''' <returns></returns>
    Public Function GetCurrency() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).CommonService.GetCurrency()
    End Function
    ''' <summary>
    ''' Obtener registro bloqueado
    ''' </summary>
    ''' <param name="IdForm"></param>
    ''' <param name="IdRecord"></param>
    ''' <returns></returns>
    Public Async Function GetBlockRecord(ByVal IdForm As String, ByVal IdRecord As String) As Task(Of BlockRecordFixedAsset)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.GetBlockRecordFixedAssetByIdformAndIdRecordAsync(IdForm, IdRecord)
    End Function

    ''' <summary>
    ''' Funcion para guardar el registro bloqueado
    ''' </summary>
    ''' <param name="Record">The registro.</param>
    ''' <returns></returns>
    Public Async Function SaveBlockRecord(ByVal Record As BlockRecordFixedAsset) As Task(Of ActionResult(Of BlockRecordFixedAsset))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.SaveBlockRecordFixedAssetAsync(Record, _indigoSessionValues)
    End Function

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
