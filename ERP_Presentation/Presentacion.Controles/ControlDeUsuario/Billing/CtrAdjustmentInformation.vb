Public Class CtrAdjustmentInformation

#Region "Delegates"

    ''' <summary>
    ''' Delegado de la funcion que establece la informacion (Neto, Descuento, Iva, Total Factura)
    ''' </summary>
    ''' <returns></returns>
    Public Delegate Function SetInfoDelegate() As Tuple(Of Decimal, Decimal)

    ''' <summary>
    ''' variable de tipo del delegado
    ''' </summary>
    Private _setInfoDelegate As SetInfoDelegate

#End Region

#Region "Properties"

    Public Property DecimalNumbers As Integer
    Public Property PositiveValue As String
    Public Property NeutralValue As String
    Public Property NegativeValue As String

    Public WriteOnly Property DebitValueText As String
        Set(value As String)
            INDpceDebitText.EditValue = value
        End Set
    End Property

    Public WriteOnly Property CreditValueText As String
        Set(value As String)
            INDpceCreditText.EditValue = value
        End Set
    End Property

#End Region

#Region "Variables"

    Private WriteOnly Property DebitValue As Decimal
        Set(value As Decimal)
            INDpceDebitValue.EditValue = Microsoft.VisualBasic.Strings.FormatCurrency(value, Me.DecimalNumbers)
            INDpceDebitValue.ToolTip = String.Format("{0}: {1}", INDpceDebitText.EditValue, INDpceDebitValue.EditValue)
        End Set
    End Property

    Private WriteOnly Property CreditValue As Decimal
        Set(value As Decimal)
            INDpceCreditValue.EditValue = Microsoft.VisualBasic.Strings.FormatCurrency(value, Me.DecimalNumbers)
            INDpceCreditValue.ToolTip = String.Format("{0}: {1}", INDpceCreditText.EditValue, INDpceCreditValue.EditValue)
        End Set
    End Property

    Private WriteOnly Property AdjustmentValue As Decimal
        Set(value As Decimal)
            If value > 0 Then
                INDpceAdjustmentText.Text = Me.PositiveValue
            ElseIf value = 0
                INDpceAdjustmentText.Text = Me.NeutralValue
            Else
                INDpceAdjustmentText.Text = Me.NegativeValue
            End If

            INDpceAdjustmentValue.EditValue = Microsoft.VisualBasic.Strings.FormatCurrency(Math.Abs(value), Me.DecimalNumbers)
            INDpceAdjustmentValue.ToolTip = String.Format("{0}: {1}", INDpceAdjustmentText.EditValue, INDpceAdjustmentValue.EditValue)
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
    ''' Imprime los valores en el control
    ''' </summary>
    Public Sub PrintInfo()
        If _setInfoDelegate IsNot Nothing Then
            Dim value As Tuple(Of Decimal, Decimal) = _setInfoDelegate()

            DebitValue = value.Item1
            CreditValue = value.Item2
            AdjustmentValue = value.Item1 - value.Item2
        End If
    End Sub

#End Region

#Region "Events"

    Private Sub Control_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.ApplyStyleSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName)
        AddHandler DevExpress.LookAndFeel.UserLookAndFeel.Default.StyleChanged, AddressOf Control_StyleChanged
    End Sub

    Private Sub Control_StyleChanged(sender As Object, e As EventArgs)
        Me.ApplyStyleSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName)
    End Sub

    Private Sub ApplyStyleSkin(ByVal skinName As String)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LayoutControl1)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDpceAdjustmentText)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDpceAdjustmentValue)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDpceDebitText)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDpceDebitValue)
        'Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(PopupContainerEdit4)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDpceCreditText)
        'Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(PopupContainerEdit6)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDpceCreditValue)
    End Sub

#End Region

End Class
