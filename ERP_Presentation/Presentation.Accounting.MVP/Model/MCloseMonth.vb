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
Public Class MCloseMonth
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

#Region "funtions"
    ''' <summary>
    ''' Saves the close month.
    ''' </summary>
    ''' <param name="closeMonth">The close month.</param>
    ''' <returns></returns>
    Public Async Function SaveCloseMonth(ByVal closeMonth As ClosedMonth, ByVal Status As Nullable(Of Integer), ListIdJournalVouchers As String, ByVal idMonth As Nullable(Of Integer)) As Task(Of ActionResult(Of ClosedMonth))
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.InnerChannel)
            Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me._indigoSessionValues.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.SaveCloseMonthAsync(closeMonth, Status, ListIdJournalVouchers, idMonth)
        End Using
    End Function


    ''' <summary>
    ''' Gets the month.
    ''' </summary>
    ''' <param name="mont">The mont.</param>
    ''' <param name="year">The year.</param>
    ''' <returns></returns>
    Public Async Function GetMonth(ByVal mont As Integer, ByVal year As Integer) As Task(Of ClosedMonth)
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.InnerChannel)
            Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me._indigoSessionValues.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetMonthCloseAsync(mont, year)
        End Using
    End Function

    ''' <summary>
    ''' Funcion para validar si el periodo se encuentra abierto
    ''' </summary>
    ''' <param name="month">true si esta abierto.</param>
    ''' <param name="year">false si esta cerrado.</param>
    ''' <param name="status"></param>
    ''' <returns></returns>
    Public Async Function ValidateOpenMonth(month As Integer, year As Integer, status As Boolean) As Task(Of Boolean)
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.InnerChannel)
            Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me._indigoSessionValues.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.ValidateOpenMonthAsync(month, year, status)
        End Using
    End Function

    ''' <summary>
    ''' Metodo para validar si el periodo esta abierto
    ''' </summary>
    ''' <param name="month">el mes.</param>
    ''' <param name="year">el año.</param>
    ''' <returns>
    ''' true = si esta abierto . false = si esta cerrado
    ''' </returns>
    Public Async Function ValidatePeriodOpen(month As Integer, year As Integer) As Task(Of Boolean)
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.InnerChannel)
            Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me._indigoSessionValues.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.ValidatePeriodOpenAsync(month, year)
        End Using
    End Function

    ''' <summary>
    ''' funcion para obtener todos los meses
    ''' </summary>
    ''' <param name="year"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetAllMonth(ByVal year As Integer, ByVal Status As Boolean) As Task(Of List(Of CloseMonthComplex))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetAllMontbyYearAsync(year, Status)
    End Function

    ''' <summary>
    ''' Gets the last month open.
    ''' </summary>
    ''' <param name="status">if set to <c>true</c> [status].</param>
    ''' <returns></returns>
    Public Async Function GetLastMonthOpen(status As Boolean) As Task(Of Integer)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetLastMonthOpenAsync(status)
    End Function

    ''' <summary>
    ''' Validates the open period.
    ''' </summary>
    ''' <param name="month">The month.</param>
    ''' <param name="year">The year.</param>
    ''' <returns></returns>
    Public Async Function ValidateOpenPeriod(month As Integer, year As Integer) As Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.ValidateOpenPeriodAsync(month, year)
    End Function

    Public Function ValidateBalanceCloseMonth(period As String, year As Integer) As ActionResult(Of List(Of Tuple(Of String, Integer)))
        Return IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.ValidateBalanceCloseMonth(period, year)
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
