Public Class CtrProgress
    ''' <summary>
    ''' Delegado de la funcion que establece la informacion (Neto, Descuento, Iva, Total Factura)
    ''' </summary>
    ''' <returns></returns>
    Public Delegate Function SetInfoDelegate() As Tuple(Of String, String)

    ''' <summary>
    ''' variable de tipo del delegado
    ''' </summary>
    Private _setInfoDelegate As SetInfoDelegate

    Public WriteOnly Property SetTitle As String
        Set(value As String)
            INDLblTitle.Text = value
        End Set
    End Property

#Region "Methods"
    ''' <summary>
    ''' Asigna el delegado que se da al ejecutar para obtener el valor del comprobante
    ''' </summary>
    Public Sub SetInfoFunction(setInfoDelegate As SetInfoDelegate)
        _setInfoDelegate = setInfoDelegate
    End Sub

    ''' <summary>
    ''' Muestra el valor del comprobante de egreso
    ''' </summary>
    Public Sub PrintInfo()

        If _setInfoDelegate IsNot Nothing Then
                Dim value As Tuple(Of String, String) = _setInfoDelegate()
                LabelControl1.Text = value.Item1 + " de " + value.Item2
            End If

    End Sub

    ''' <summary>
    ''' muestra el valor de los items porcesados se llama sin funcion delegada
    ''' </summary>
    ''' <param name="value1"></param>
    ''' <param name="value2"></param>
    Public Sub PrintInfo(value1 As String, value2 As String)
        LabelControl1.Text = value1 + " de " + value2
    End Sub
#End Region
End Class
