'***********************************************************************
' Assembly         : Presentacion.Accounting.MVP
' Author           : Sergio Abraham Fernandez Cruz
' Created          : 10-07-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Presentation.CloudAgent
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Domain.Entities
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo
#End Region
Public Class MCompanySettings
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

#Region "Constructor"
    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New(ByVal tag As String)
        _tagForm = tag
        Me._indigoSessionValues = SessionValues.Instance
    End Sub
#End Region

#Region "Functions"
    ''' <summary>
    ''' Saves the close month.
    ''' </summary>
    Public Async Function SaveCompanySettings(ByVal companySettings As CompanySettings) As Task(Of ActionResult(Of CompanySettings))
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.InnerChannel)
            Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me._indigoSessionValues.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.SaveCompanySettingsAsync(companySettings)
        End Using
    End Function

    ''' <summary>
    ''' Gets the company settings.
    ''' </summary>
    ''' <returns></returns>
    Public Async Function GetCompanySettings() As Task(Of CompanySettings)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetCompanySettingsAsync()
    End Function

    ''' <summary>
    ''' consulta los registros del maestro de moneda
    ''' </summary>
    ''' <returns></returns>
    Public Function GetCurrencyDatasource() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me._indigoSessionValues.TransactionalContainer).CommonService.GetCurrency(True)
    End Function
    ''' <summary>
    ''' consulta los tipo contables
    ''' </summary>
    ''' <returns></returns>
    Public Function GetJournalVoucherTypes() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me._indigoSessionValues.TransactionalContainer).AccountingService.ListJournalVoucherByState(True)
    End Function
    ''' <summary>
    ''' consulta los tipo contables
    ''' </summary>
    ''' <returns></returns>
    Public Function GetMainAccountsDatasource() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me._indigoSessionValues.TransactionalContainer).AccountingService.ListAccountsByLevel(0, True, ClassType:=2)
    End Function

    ''' <summary>
    ''' consulta los centro de costo
    ''' </summary>
    ''' <returns></returns>
    Public Function GetCostCentersDatasource() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me._indigoSessionValues.TransactionalContainer).AccountingService.ListCostcenterReport()
    End Function

    ''' <summary>
    ''' Obtiene si la cuenta contable maneja centro de costo
    ''' </summary>
    ''' <param name="IdProfit"></param>
    ''' <param name="IdLost"></param>
    ''' <returns></returns>
    Public Async Function GetHadleCostCenterByMainAccountId(IdProfit As Integer?, IdLost As Integer?) As Task(Of ActionResult)
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.InnerChannel)
            Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me._indigoSessionValues.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetHadleCostCenterByMainAccountIdAsync(IdProfit, IdLost)
        End Using
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