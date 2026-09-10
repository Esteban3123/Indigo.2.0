Imports Presentation.Base.Extension
Imports System.ComponentModel

Public Class CtrInfoRequirements

#Region "Delegate"

    ''' <summary>
    ''' Delegado para especificar una funcion que me devuelva el nombre de la plantilla de requerimiento
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Delegate Function InfoDelegate() As Tuple(Of String, Integer)

    ''' <summary>
    ''' Apuntador del delegado
    ''' </summary>
    ''' <remarks></remarks>
    Private _functionInfo As InfoDelegate

#End Region

#Region "Properties"

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
            INDpceChangeData.Text = tuplaInfo.Item1
            INDpceChangeData.ToolTip = tuplaInfo.Item1
            INDlblCount.Text = tuplaInfo.Item2.ToString
        End If
    End Sub

#End Region

#Region "Events"

    Private Sub INDpceChangeData_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDpceChangeData.QueryPopUp
        RaiseEvent OpenPopupValue(sender, e)
    End Sub

    Event OpenPopupValue(sender As Object, e As CancelEventArgs)

#End Region

    Private Sub Control_Load(sender As Object, e As EventArgs) Handles Me.Load
        Presentation.Resources.ThemeResourceManager.SetStyleThemeOnControl(Me)
        AddHandler DevExpress.LookAndFeel.UserLookAndFeel.Default.StyleChanged, AddressOf Control_StyleChanged
    End Sub

    Private Sub Control_StyleChanged(sender As Object, e As EventArgs)
        Presentation.Resources.ThemeResourceManager.SetStyleThemeOnControl(Me)
    End Sub

End Class
