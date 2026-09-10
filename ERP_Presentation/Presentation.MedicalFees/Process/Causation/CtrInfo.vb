Imports Presentation.Base.Extension
Imports System.ComponentModel

Public Class CtrInfo

#Region "Delegate"

    ''' <summary>
    ''' Delegado para especificar una funcion que me devuelva el numero del ingreso y el nombre del paciente
    ''' </summary>
    ''' <returns>Nombre del paciente</returns>
    ''' <remarks>Numero de ingreso</remarks>
    Public Delegate Function InfoDelegate() As Tuple(Of String, String, String)

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
    <BrowsableAttribute(False)> _
    Public Property PopupContainerControlTotalValue As DevExpress.XtraEditors.PopupContainerControl
        Get
            Return Me.INDpcePacient.Properties.PopupControl
        End Get
        Set(value As DevExpress.XtraEditors.PopupContainerControl)
            Me.INDpcePacient.Properties.PopupControl = value
        End Set
    End Property

#End Region

    Private Sub Control_Load(sender As Object, e As EventArgs) Handles Me.Load
        Presentation.Resources.ThemeResourceManager.SetStyleThemeOnControl(Me)
        AddHandler DevExpress.LookAndFeel.UserLookAndFeel.Default.StyleChanged, AddressOf Control_StyleChanged
    End Sub

    Private Sub Control_StyleChanged(sender As Object, e As EventArgs)
        Presentation.Resources.ThemeResourceManager.SetStyleThemeOnControl(Me)
    End Sub

#Region "Methods"

    ''' <summary>
    ''' Asigna el delegado que se va ejecutar para obtener el
    ''' numero de ingreso y el nombre del paciente
    ''' </summary>
    ''' <param name="functionInfo"></param>
    ''' <remarks></remarks>
    Public Sub SetInfo(functionInfo As InfoDelegate)
        _functionInfo = functionInfo
    End Sub

    ''' <summary>
    ''' Metodo para refrescar los valores
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub RefreshInfo()
        If _functionInfo IsNot Nothing Then
            Dim tuplaInfo = _functionInfo()
            INDpcePacient.Text = tuplaInfo.Item1
            INDpcePacient.ToolTip = INDpcePacient.Text
            INDlbAdmissionNumber.Text = tuplaInfo.Item2
            INDlbEntity.Text = tuplaInfo.Item3
        End If
    End Sub

#End Region

End Class
