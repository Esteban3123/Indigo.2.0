'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Jose Luis Rojas
' Created          : 09-05-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Domain.Payroll.Entities
Imports Presentation.CloudAgent
#End Region
''' <summary>
''' Realiza la conexion con los servicios de contratos
''' </summary>
Public Class MContract
    Implements IDisposable

#Region "Properties"
    Dim Indigo As SessionValues = SessionValues.Instance
#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene el talento humano de acuerdo al codigo 
    ''' </summary>
    ''' <param name="code">Codigo</param>
    ''' <returns>Talento humano</returns>
    Public Async Function GetEmployeeContractAsync(ByVal code As String) As Task(Of Employee)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetEmployeeBasicContractAsync(code, Indigo)
    End Function


    ''' <summary>
    ''' Busca si el contrato tiene alguna nomina confirmada y devuelve falso o verdadero
    ''' </summary>
    ''' <param name="contractId">Id del contrato a buscar</param>
    ''' <returns>Verdadero si tiene nominas confirmadas, falso en caso contrario</returns>
    Public Async Function LiquidationConfirmatedByContractAsync(ByVal contractId As Integer) As Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.LiquidationConfirmatedByContractAsync(contractId, Indigo)
    End Function


    Public Async Function DeleteNotConfirmedLiquidationByContractAsync(ByVal contractId As Integer) As Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.DeleteLiquidationByContractIdAsync(contractId, Indigo)
    End Function

    ''' <summary>
    ''' Lista los campos nulos de la base de datos y permitir su customizacion
    ''' </summary>
    ''' <returns>Un conjuto de datos con los campos marcados como nulos en la base de datos</returns>
    Public Function GetFieldsNULL() As DataSet
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetFieldsNULL("Contract", Indigo)
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
