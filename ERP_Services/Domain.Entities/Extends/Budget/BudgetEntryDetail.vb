Imports System.Runtime.CompilerServices

''' <summary>
''' clase para mapear la consulta del detalle del presupuesto por rubro
''' </summary>
''' <remarks></remarks>
Public Class BudgetEntryDetail
    Implements IDisposable


    Property Code As String
    Property Name As String
    Property Value As Decimal
    Property RevenueTypeId As Integer
    Property CategoryId As Integer
    Property Budget As Budget


#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: eliminar estado administrado (objetos administrados).
            End If

            ' TODO: liberar recursos no administrados (objetos no administrados) e invalidar Finalize() below.
            ' TODO: Establecer campos grandes como Null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: invalidar Finalize() sólo si la instrucción Dispose(ByVal disposing As Boolean) anterior tiene código para liberar recursos no administrados.
    'Protected Overrides Sub Finalize()
    '    ' No cambie este código. Ponga el código de limpieza en la instrucción Dispose(ByVal disposing As Boolean) anterior.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' Visual Basic agregó este código para implementar correctamente el modelo descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en Dispose(disposing As Boolean).
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
Public Module ExtendsBudget


    <Extension()> _
    Public Function IsConfirmated(ListBudget As List(Of Budget), TotalResolutionValue As Double, SumTotal As Double) As Boolean
        If Double.Equals(TotalResolutionValue, SumTotal) = True Then
            Return True
        Else
            Return False
        End If
    End Function
End Module