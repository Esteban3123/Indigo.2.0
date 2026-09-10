'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 06/05/2017
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
Imports Domain.Payroll.Entities
Imports Presentation.Base
Imports Domain.Base.Entities
Imports Domain.Entities

#End Region

Public Class MInitialBalancePayroll
    Inherits ModelBase
    Implements IDisposable

#Region "Builder"

    Sub New(tag As String)
        MyBase.New(tag)
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Método para consumir el sp de saldo inicial de nómina
    ''' </summary>
    ''' <returns></returns>
    Public Function SP_ImportFileInitialBalancePayroll(data As List(Of ImportFileRow)) As ActionResult(Of List(Of SP_ImportFileInitialBalancePayroll_Result))
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SP_ImportFileInitialBalancePayroll(data, Indigo)
    End Function

    ''' <summary>
    ''' Guarda los saldos iniciales
    ''' </summary>
    ''' <returns></returns>
    Public Async Function SP_SaveInitialBalancePayroll(ListInfo As List(Of SP_ImportFileInitialBalancePayroll_Result)) As Task(Of ActionResult(Of List(Of Tuple(Of String, Integer))))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SP_SaveInitialBalancePayrollAsync(ListInfo, Indigo)
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
