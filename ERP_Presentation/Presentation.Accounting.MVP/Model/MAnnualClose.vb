'***********************************************************************
' Assembly         : Presentacion.Accounting.MVP
' Author           : Miguel Angel Fonseca Castro
' Created          : 2018-08-01
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

Public Class MAnnualClose
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
    ''' Saves the annual closure.
    ''' </summary>
    ''' <param name="LegalBookId"></param>
    ''' <param name="Year"></param>
    ''' <returns></returns>
    Public Async Function FiscalYearClose(ByVal LegalBookId As Integer, ByVal Year As Integer, ByVal OperativeUnitId As Integer) As Task(Of ActionResult(Of List(Of Tuple(Of String, Integer))))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.FiscalYearCloseAsync(LegalBookId, Year, OperativeUnitId, _indigoSessionValues.IndigoCompanyNit, _indigoSessionValues.UserIndigo)
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
