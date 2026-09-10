'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 28-07-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.CloudAgent

#End Region
Public Class MCostDistribution
    Inherits ModelBase
    Implements IDisposable

    Dim Indigo As SessionValues = SessionValues.Instance

#Region "Construct"

    Shared TAG As String = ""

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Sub New()
        MyBase.New(TAG)
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' Devuelve armado el archivo de banco, por codigo de archivo de banco, y todas las liquidaciones
    ''' </summary>
    ''' <param name="GroupId">Código de Grupo</param>
    ''' <param name="PayrollDate">Fecha Liquidación de Nómina</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GenerateCostDistributionAsync(GroupId As Integer, PayrollDate As Date) As Task(Of ActionResult(Of List(Of String)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GenerateCostDistributionAsync(GroupId, PayrollDate, Indigo)
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
