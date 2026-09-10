'***********************************************************************
' Assembly         : Presentacion.Accounting.MVP
' Author           : Sergio Abraham Fernandez
' Created          : 15-05-2014
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
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports System.Text

#End Region
Public Class MSettingsAccount
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
        _tagForm = tag
        Me._indigoSessionValues = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' funcion para obtener el parametro contable
    ''' </summary>
    ''' <returns></returns>
    Public Async Function GetSettingAccount(ByVal idOperatingUnit As Integer) As Task(Of GeneralLedgerSettings)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetSettingAccountAsync(idOperatingUnit)
    End Function

    ''' <summary>
    ''' Saves the setting account.
    ''' </summary>
    ''' <param name="settingAccount">The setting account.</param>
    ''' <returns></returns>
    Public Async Function SaveSettingAccount(ByVal settingAccount) As Task(Of ActionResult(Of GeneralLedgerSettings))
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.InnerChannel)
            Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me._indigoSessionValues.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.SaveSettingAccountAsync(settingAccount)
        End Using
    End Function

    ''' <summary>
    ''' Crea el bloqueo de un registro
    ''' </summary>
    ''' <param name="record">Registro a bloquear</param>
    ''' <returns>Resultados de la acción</returns>
    Public Async Function SaveBlockRecord(ByVal record As BlockRecordGeneralLedger) As Task(Of ActionResult(Of BlockRecordGeneralLedger))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.SaveBlockRecordAccountingAsync(record)
    End Function

    ''' <summary>
    ''' Elimina el bloqueo del registro
    ''' </summary>
    ''' <param name="record">Registro a desbloquear</param>
    ''' <returns>Resultado de la acción</returns>
    Public Async Function DeleteBlockRecord(ByVal record As BlockRecordGeneralLedger) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.DeleteBlockRecordAccountingAsync(record)
    End Function

    ''' <summary>
    ''' Consulta si el registro se encuentra bloqueado
    ''' </summary>
    ''' <param name="IdForm">Id del frontal</param>
    ''' <param name="IdRecord">Id del registro</param>
    ''' la información de bloqueo del registro</returns>
    Public Async Function GetBlockRecord(ByVal idForm As String, ByVal idRecord As String) As Task(Of BlockRecordGeneralLedger)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetBlockRecordAccountingByIdformAndIdRecordAsync(idForm, idRecord)
    End Function

    ''' <summary>
    ''' Genera el archivo plano de boletin deudores morosos
    ''' </summary>
    ''' <param name="DateCourt"></param>
    ''' <param name="ReportValue"></param>
    ''' <param name="value"></param>
    ''' <param name="PeriodType"></param>
    ''' <param name="PeriodValue"></param>
    ''' <param name="ThirdPartyStart"></param>
    ''' <param name="ThirdPartyEnd"></param>
    ''' <param name="EntityCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GenerateArchiveBulletinDefaultersState(TypeReport As Integer, DateCourt As String, ReportValue As Boolean, value As Decimal, PeriodType As Byte, PeriodValue As Integer, ThirdPartyStart As String, ThirdPartyEnd As String, EntityCode As String) As Task(Of StringBuilder)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GenerateArchiveBulletinDefaultersStateAsync(TypeReport, DateCourt, ReportValue, value, PeriodType, PeriodValue, ThirdPartyStart, ThirdPartyEnd, EntityCode, Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' Genera el archivo plano de CGN002
    ''' </summary>
    ''' <param name="DateStart"></param>
    ''' <param name="DateEnd"></param>
    ''' <param name="AccountStart"></param>
    ''' <param name="AccountEnd"></param>
    ''' <param name="LevelSubAccount"></param>
    ''' <param name="BookId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GenerateFileCGN002(DateStart As Date, DateEnd As Date, AccountStart As String, AccountEnd As String, LevelSubAccount As Boolean, BookId As Integer) As Task(Of StringBuilder)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GenerateFileCGN002Async(DateStart, DateEnd, AccountStart, AccountEnd, LevelSubAccount, BookId, Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' Genera el archivo plano de CGN001
    ''' </summary>
    ''' <param name="DateStart"></param>
    ''' <param name="DateEnd"></param>
    ''' <param name="AccountStart"></param>
    ''' <param name="AccountEnd"></param>
    ''' <param name="AccountingZero"></param>
    ''' <param name="BookId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GenerateFileCGN001(DateStart As Date, DateEnd As Date, AccountStart As String, AccountEnd As String, AccountingZero As Boolean, BookId As Integer, InThousands As Boolean) As Task(Of StringBuilder)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GenerateFileCGN001Async(DateStart, DateEnd, AccountStart, AccountEnd, AccountingZero, BookId, InThousands, Me._indigoSessionValues)
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
