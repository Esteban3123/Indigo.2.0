Imports System.ComponentModel
Imports DevExpress.XtraEditors.Mask

Public Class CtrTotalInfo

#Region "Delegates"
    ''' <summary>
    ''' Delegado de la funcion que establece la informacion
    ''' </summary>
    ''' <returns></returns>
    Public Delegate Function SetInfoDelegate() As Tuple(Of String)
    ''' <summary>
    ''' variable de tipo del delegado
    ''' </summary>
    Private _setInfoDelegate As SetInfoDelegate
#End Region

#Region "Properties"

    WriteOnly Property TotalValue As String
        Set(value As String)
            INDPceTotalValue.Text = value.Trim()
            INDPceTotalValue.EditValue = value.Trim()
            INDPceTotalValue.ToolTip = "Total: " + value.Trim()
        End Set
    End Property

    ''' <summary>
    ''' Texto del control de iva value
    ''' </summary>
    ''' <remarks></remarks>
    Private _textIvaValue As String
    Property TextIvaValue As String
        Get
            Return _textIvaValue
        End Get
        Set(value As String)
            _textIvaValue = value
        End Set
    End Property

    ''' <summary>
    ''' Texto del control de iva value
    ''' </summary>
    ''' <remarks></remarks>
    Private _textDiscountValue As String
    Property TextDiscountValue As String
        Get
            Return _textDiscountValue
        End Get
        Set(value As String)
            _textDiscountValue = value
        End Set
    End Property

    ''' <summary>
    ''' recibe un listado de tipo string que define el EditMask y de tipo MaskType que define el tipo de mascara
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    WriteOnly Property MaskTotalValue As String
        Set(value As String)
            INDPceTotalValue.Properties.Mask.EditMask = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el PopupContainerControl que será lanzado en el valor
    ''' </summary>
    ''' <value>PopupContainerControl que se lanzará</value>
    ''' <returns>El PopupContainerControl que se lanzará</returns>
    <BrowsableAttribute(False)> _
    Public Property PopupContainerControlTotalValue As DevExpress.XtraEditors.PopupContainerControl
        Get
            Return Me.INDPceTotalValue.Properties.PopupControl
        End Get
        Set(value As DevExpress.XtraEditors.PopupContainerControl)
            Me.INDPceTotalValue.Properties.PopupControl = value
        End Set
    End Property
#End Region

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
            Dim value As Tuple(Of String) = _setInfoDelegate()
            TotalValue = value.Item1
        End If
    End Sub
#End Region

    Private Sub Control_Load(sender As Object, e As EventArgs) Handles Me.Load
        Presentation.Resources.ThemeResourceManager.SetStyleThemeOnControl(Me)
        AddHandler DevExpress.LookAndFeel.UserLookAndFeel.Default.StyleChanged, AddressOf Control_StyleChanged
    End Sub

    Private Sub Control_StyleChanged(sender As Object, e As EventArgs)
        Presentation.Resources.ThemeResourceManager.SetStyleThemeOnControl(Me)
    End Sub

    Private Sub INDPceTotalValue_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDPceTotalValue.QueryPopUp
        RaiseEvent OpenPopupValue(sender, e)
    End Sub

    Event OpenPopupValue(sender As Object, e As CancelEventArgs)
End Class
