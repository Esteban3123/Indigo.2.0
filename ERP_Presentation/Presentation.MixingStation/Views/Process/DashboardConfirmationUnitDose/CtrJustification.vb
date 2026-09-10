Imports System.ComponentModel

Public Class CtrJustification

#Region "Delegates"
    ''' <summary>
    ''' Delegado de la funcion que establece la Justificación de prescripción
    ''' </summary>
    ''' <returns></returns>
    Public Delegate Function SetInfoDelegate() As Tuple(Of String)

    ''' <summary>
    ''' variable de tipo del delegado
    ''' </summary>
    Private _setInfoDelegate As SetInfoDelegate
#End Region

#Region "Properties"

    WriteOnly Property Justification As String
        Set(value As String)
            PopupContainerEdit1.EditValue = value
            PopupContainerEdit1.ToolTip = "Justificación de Prescripción: " + PopupContainerEdit1.Text.Trim()
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el PopupContainerControl que será lanzado en el valor
    ''' </summary>
    ''' <value>PopupContainerControl que se lanzará</value>
    ''' <returns>El PopupContainerControl que se lanzará</returns>
    <BrowsableAttribute(False)>
    Public Property PopupContainerControlTotalValue As DevExpress.XtraEditors.PopupContainerControl
        Get
            Return Me.INDPceJustificationPrescription.Properties.PopupControl
        End Get
        Set(value As DevExpress.XtraEditors.PopupContainerControl)
            Me.INDPceJustificationPrescription.Properties.PopupControl = value
        End Set
    End Property

#End Region

#Region "Methods"
    ''' <summary>
    ''' Asigna el delegado que se da al ejecutar para obtener la justificacion de la prescripcion
    ''' </summary>
    Public Sub SetInfoFunction(setInfoDelegate As SetInfoDelegate)
        _setInfoDelegate = setInfoDelegate
    End Sub

    ''' <summary>
    ''' Muestra la justificacion de la prescripcion
    ''' </summary>
    Public Sub PrintInfo()
        If _setInfoDelegate IsNot Nothing Then
            Dim value As Tuple(Of String) = _setInfoDelegate()

            Justification = value.Item1
        End If
    End Sub
#End Region

#Region "Events"
    Private Sub INDPceJustificationPrescription_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDPceJustificationPrescription.QueryPopUp
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
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDPceJustification)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDPceJustificationPrescription)
    End Sub

End Class
