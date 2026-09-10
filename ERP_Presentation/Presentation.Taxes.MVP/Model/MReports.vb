#Region "Imports"

Imports System.Data
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent

#End Region

Public Class MReports
    Implements IDisposable

#Region "Fields"

    ''' <summary>
    ''' Id del frontal
    ''' </summary>
    Private _tagForm As String

    ''' <summary>
    ''' Referencia a los valores de session
    ''' </summary>
    Private _indigo As SessionValues

#End Region

#Region "Builder"

    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="Tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal tag As String)
        _tagForm = tag
        _indigo = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    Public Async Function GetListReportCertificateRetSource(ByVal LegalBookId As Integer, ByVal Year As Integer, ByVal AccountStart As String, ByVal AccountEnd As String, ByVal NitStart As String, ByVal NitEnd As String, InitialDate As String, FinalDate As String) As Task(Of DataSet)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetListReportCertificateRetSourceAsync(LegalBookId, Year, AccountStart, AccountEnd, NitStart, NitEnd, InitialDate, FinalDate, Me._indigo)
    End Function

    Public Async Function GetListReportCertificateRetICA(ByVal DateStart As Date, ByVal DateEnd As Date, ByVal AccountStart As String, ByVal AccountEnd As String, ByVal NitStart As String, ByVal NitEnd As String, ByVal RetencionType As Integer, ByVal LegalBookId As Integer) As Task(Of DataSet)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetListReportCertificateRetICAAsync(DateStart, DateEnd, AccountStart, AccountEnd, NitStart, NitEnd, RetencionType, LegalBookId, Me._indigo)
    End Function

    Public Async Function GetListReportCertificateRetIVA(ByVal DateInitial As Date, ByVal DateEnd As Date, ByVal AccountStart As String, ByVal AccountEnd As String, ByVal NitStart As String, ByVal NitEnd As String, ByVal RetencionType As Integer, ByVal LegalBookId As Integer) As Task(Of DataSet)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetListReportCertificateRetIVAAsync(DateInitial, DateEnd, AccountStart, AccountEnd, NitStart, NitEnd, RetencionType, LegalBookId, Me._indigo)
    End Function

#End Region

#Region "IDisposable Support"

    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: desechar estado administrado (objetos administrados).
            End If

            ' TODO: liberar recursos no administrados (objetos no administrados) e invalidar Finalize() below.
            ' TODO: Establecer campos grandes como Null.
        End If
        Me.disposedValue = True
    End Sub

    ' Visual Basic agregó este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en Dispose(disposing As Boolean).
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

#End Region

End Class
