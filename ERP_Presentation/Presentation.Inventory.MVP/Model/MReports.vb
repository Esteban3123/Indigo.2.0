#Region "Imports"

Imports System.Data
Imports Domain.Entities
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

    Public Async Function GetReportControlMedications(criterias As Dictionary(Of String, String)) As Task(Of DataSet)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetReportControlMedicationsAsync(criterias, Me._indigo)
    End Function

    Public Async Function GetReportFiscalAccount(criterias As Dictionary(Of String, String)) As Task(Of DataSet)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetReportFiscalAccountAsync(criterias, Me._indigo)
    End Function

    Public Async Function GetReportValuedInventory(filters As Dictionary(Of String, String), range As Dictionary(Of String, String)) As Task(Of List(Of SP_ReportValuedInventory_Result))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetReportValuedInventoryAsync(filters, range, Me._indigo)
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        ' TODO: uncomment the following line if Finalize() is overridden above.
        ' GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class