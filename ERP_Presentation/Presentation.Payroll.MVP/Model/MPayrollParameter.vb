'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Cristhian Mauricio Salazar
' Created          : 05-09-2013
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Domain.Payroll.Entities
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo
''' <summary>
''' Modelo de parametros de nomina el cual se comunica con los sevicios distribuidos
''' </summary>
''' <remarks></remarks>
Public Class MPayrollParameter
    Implements IDisposable

    ''' <summary>
    ''' Variable que contiene la instancia de la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Obtiene un parametro atraves de un grupo
    ''' </summary>
    ''' <param name="groupId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetPayrollParameterAsync(groupId As Integer) As Task(Of PayrollParameter)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetPayrollParameterAsync(groupId, Indigo)
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
