'***********************************************************************
' Assembly         : Presentacion.FixedAsset.MVP
' Author           : Sergio Abraham Fernandez Cruz
' Created          : 02-05-2014
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

#End Region
Public Class MPUC
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

#Region "Funcionts"

    ''' <summary>
    ''' Gets all accoutn level.
    ''' </summary>
    ''' <returns></returns>
    Public Async Function GetAllAccoutnLevel() As Task(Of List(Of MainAccountLevels))
        Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetAllAccountLevelAsync()
    End Function

    ''' <summary>
    ''' Crea el bloqueo de un registro
    ''' </summary>
    ''' <param name="record">Registro a bloquear</param>
    ''' <returns>Resultados de la acción</returns>
    Public Async Function SaveBlockRecord(ByVal record As Domain.Entities.BlockRecordGeneralLedger) As Task(Of ActionResult(Of Domain.Entities.BlockRecordGeneralLedger))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.SaveBlockRecordAccountingAsync(record)
    End Function

    ''' <summary>
    ''' Elimina el bloqueo del registro
    ''' </summary>
    ''' <param name="record">Registro a desbloquear</param>
    ''' <returns>Resultado de la acción</returns>
    Public Async Function DeleteBlockRecord(ByVal record As Domain.Entities.BlockRecordGeneralLedger) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.DeleteBlockRecordAccountingAsync(record)
    End Function

    ''' <summary>
    ''' Consulta si el registro se encuentra bloqueado
    ''' </summary>
    ''' <param name="IdForm">Id del frontal</param>
    ''' <param name="IdRecord">Id del registro</param>
    ''' la información de bloqueo del registro</returns>
    Public Async Function GetBlockRecord(ByVal idForm As String, ByVal idRecord As String) As Task(Of Domain.Entities.BlockRecordGeneralLedger)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetBlockRecordAccountingByIdformAndIdRecordAsync(idForm, idRecord)
    End Function

    ''' <summary>
    ''' Funcion para obtener  la cuenta por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Public Async Function GetAccountByCode(code As String, Optional tracking As Boolean = True) As Task(Of Domain.Entities.MainAccounts)
        Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetAccountByCodeAsync(code, tracking, _indigoSessionValues)
    End Function

    ''' <summary>
    ''' Funcion para obtener  la cuenta por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Async Function GetAccountByCodeAndLegalBookId(ByVal code As String, LegalBookId As Integer) As Task(Of Domain.Entities.MainAccounts)
        Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetAccountByCodeAndLegalBookIdAsync(code, LegalBookId)
    End Function

    ''' <summary>
    ''' Funcion para obtener la cuenta por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Public Async Function GetAccountById(id As Integer, Optional tracking As Boolean = True) As Task(Of Domain.Entities.MainAccounts)
        Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetAccountByIdAsync(id, tracking, _indigoSessionValues)
    End Function

    Public Function GetAccountByIdSimple(id As Integer, Optional tracking As Boolean = True) As Domain.Entities.MainAccounts
        Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
        Return IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetAccountById(id, tracking, _indigoSessionValues)
    End Function

    ''' <summary>
    ''' Funcion para obtener la cuenta por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Public Function GetAccountId(id As Integer, Optional tracking As Boolean = True) As Domain.Entities.MainAccounts
        Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
        Return IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetAccountById(id, tracking, _indigoSessionValues)
    End Function

    ''' <summary>
    ''' Funcion para guardar la cuenta contable
    ''' </summary>
    ''' <returns></returns>
    Public Async Function Sp_InsertPUC(mainAccount As MainAccounts) As Task(Of ActionResult(Of Domain.Entities.MainAccounts))
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.InnerChannel)
            Dim mess As New MessageHeader(Of AuditMessage)(Me._indigoSessionValues.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.Sp_InsertPUCAsync(mainAccount)
        End Using
    End Function

    ''' <summary>
    ''' Gets all account.
    ''' </summary>
    ''' <returns></returns>
    Public Async Function GetAllAccounts() As Task(Of List(Of Domain.Entities.MainAccounts))
        Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetAllAcountsAsync()
    End Function

    ''' <summary>
    ''' Gets the account parent by code.
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Async Function GetAccountParentByCode(ByVal code As String, legalBookId As Integer) As Task(Of Domain.Entities.MainAccounts)
        Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetAccountParentbyCodeAsync(code, legalBookId)
    End Function

    ''' <summary>
    ''' Updates the puc.
    ''' </summary>
    ''' <param name="puc">The puc.</param>
    ''' <returns></returns>
    Public Async Function UpdatePuc(ByVal puc As Domain.Entities.MainAccounts) As Task(Of ActionResult(Of Domain.Entities.MainAccounts))
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.InnerChannel)
            Dim mess As New MessageHeader(Of AuditMessage)(Me._indigoSessionValues.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.UpdatePucAsync(puc)
        End Using
    End Function

    ''' <summary>
    ''' Actualiza el estado del registro
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    Public Async Function UpdateStatePUC(ByVal code As String, LegalBookId As Integer, ByVal state As Boolean) As Task(Of ActionResult(Of Domain.Entities.MainAccounts))
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.InnerChannel)
            Dim mess As New MessageHeader(Of AuditMessage)(Me._indigoSessionValues.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.UpdateStatePUCAsync(code, LegalBookId, state)
        End Using
    End Function

    ''' <summary>
    ''' Elimina una cuenta
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteAccounting(ByVal record As MainAccounts) As Task(Of ActionResult)
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.InnerChannel)
            Dim mess As New MessageHeader(Of AuditMessage)(Me._indigoSessionValues.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.DeleteAccountingAsync(record)
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
