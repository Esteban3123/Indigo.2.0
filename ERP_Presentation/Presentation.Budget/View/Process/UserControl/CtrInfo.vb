Imports Presentation.Base.Extension
Public Class CtrInfo

#Region "Delegate"

    ''' <summary>
    ''' Delegado para especificar una funcion que me devuelva el valor de la cuenta por pagar y el valor de la factura
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Delegate Function InfoDelegate() As Tuple(Of String, String, String)

    ''' <summary>
    ''' Apuntador del delegado
    ''' </summary>
    ''' <remarks></remarks>
    Private _functionInfo As InfoDelegate

#End Region

#Region "Properties"

    ''' <summary>
    ''' Propiedad para establecer el texto del valor principal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property LabelValue1 As String
        Get
            Return INDlbItemBudgetaryEntity.Text
        End Get
        Set(value As String)
            INDlbItemBudgetaryEntity.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para establecer el texto del valor secundario
    ''' </summary>
    ''' <remarks></remarks>
    Public Property LabelValue2 As String
        Get
            Return INDlbItemValidity.Text
        End Get
        Set(value As String)
            INDlbItemValidity.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para establecer el texto del valor secundario
    ''' </summary>
    ''' <remarks></remarks>
    Public Property LabelValue3 As String
        Get
            Return INDlbItemStatus.Text
        End Get
        Set(value As String)
            INDlbItemStatus.Text = value
        End Set
    End Property

#End Region

#Region "Methods"

    ''' <summary>
    ''' Asigna el delegado que se va ejecutar para obtener el valor cxp y el valor factura
    ''' </summary>
    ''' <param name="functionInfo"></param>
    ''' <remarks></remarks>
    Public Sub SetTotalValues(functionInfo As InfoDelegate)
        _functionInfo = functionInfo
    End Sub

    ''' <summary>
    ''' Metodo para refrescar los valores
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub RefreshInfo()
        If _functionInfo IsNot Nothing Then
            Dim tuplaInfo = _functionInfo()
            INDlbBudgetaryEntity.Text = tuplaInfo.Item1
            INDlbValidity.Text = tuplaInfo.Item2
            INDlbStatus.Text = tuplaInfo.Item3
        End If
    End Sub

#End Region

    Private Sub Control_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.ApplyStyleSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName)
        AddHandler DevExpress.LookAndFeel.UserLookAndFeel.Default.StyleChanged, AddressOf Control_StyleChanged
    End Sub

    Private Sub Control_StyleChanged(sender As Object, e As EventArgs)
        Me.ApplyStyleSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName)
    End Sub

    Private Sub ApplyStyleSkin(ByVal skinName As String)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LayoutControl1)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LayoutControlItem1)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LayoutControlItem2)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LayoutControlItem3)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LayoutControlItem4)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LayoutControlItem5)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LayoutControlItem6)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlbItemBudgetaryEntity)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlbBudgetaryEntity)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlbItemValidity)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlbValidity)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlbItemStatus)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlbStatus)
    End Sub

End Class
