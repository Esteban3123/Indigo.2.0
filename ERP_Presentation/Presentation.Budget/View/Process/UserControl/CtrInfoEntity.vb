Imports Presentation.Base.Extension
Imports System.ComponentModel

Public Class CtrInfoEntity

#Region "Delegate"

    ''' <summary>
    ''' Delegado para especificar una funcion que me devuelva el nombre de la entidad y la vigencia
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Delegate Function InfoDelegate() As Tuple(Of Integer, String, Integer, String, String)

    ''' <summary>
    ''' Apuntador del delegado
    ''' </summary>
    ''' <remarks></remarks>
    Private _functionInfo As InfoDelegate

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene el id de la entidad presupuestal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property EntityId As Integer

    ''' <summary>
    ''' Obtiene el id de la vigencia
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ValidityId As Integer

    ''' <summary>
    ''' Obtiene o asigna el PopupContainerControl que será lanzado en el valor
    ''' </summary>
    ''' <value>PopupContainerControl que se lanzará</value>
    ''' <returns>El PopupContainerControl que se lanzará</returns>
    Public Property PopupContainerControlEntity As DevExpress.XtraEditors.PopupContainerControl
        Get
            Return Me.INDpceChangeData.Properties.PopupControl
        End Get
        Set(value As DevExpress.XtraEditors.PopupContainerControl)
            Me.INDpceChangeData.Properties.PopupControl = value
        End Set
    End Property

#End Region

#Region "Methods"

    ''' <summary>
    ''' Asigna el delegado que se va ejecutar para obtener el nombre de la entidad y la vigencia
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
            EntityId = tuplaInfo.Item1
            INDlblEntityData.Text = tuplaInfo.Item2
            ValidityId = tuplaInfo.Item3
            INDlblValidateData.Text = tuplaInfo.Item4
            INDlbStatus.Text = tuplaInfo.Item5
        End If
    End Sub

#End Region

#Region "Events"
    Private Sub INDpceChangeData_QueryPopUp(sender As Object, e As CancelEventArgs)
        RaiseEvent OpenPopupValue(sender, e)
    End Sub

    Event OpenPopupValue(sender As Object, e As CancelEventArgs)
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
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlblEntityTitle)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlblEntityData)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlblValidateTitle)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlblValidateData)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlbItemStatus)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlbStatus)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDpceChangeData)
        'Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LayoutControlItem7)
    End Sub

End Class
