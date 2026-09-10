Imports System.ComponentModel

Public Class CtrEntranceVoucherSource
    ''' <summary>
    ''' Delegado de la funcion que establece la informacion
    ''' </summary>
    ''' <returns></returns>
    Public Delegate Function SetInfoDelegate() As Tuple(Of String, String)
    ''' <summary>
    ''' variable de tipo del delegado
    ''' </summary>
    Private _setInfoDelegate As SetInfoDelegate

#Region "Properties"
    Private WriteOnly Property Source As String
        Set(value As String)
            Select Case value
                Case 1
                    INDLblSource.Text = ""
                Case 2
                    INDLblSource.Text = "Orden de Compra"
                Case 3
                    INDLblSource.Text = "Contrato"
                Case 4
                    INDLblSource.Text = "Remisión"
            End Select
        End Set
    End Property

    Private WriteOnly Property Code As String
        Set(value As String)
            If value <> String.Empty Then
                INDLblCode.Text = "Nº - " + value
            Else
                INDLblCode.Text = "Ningún Origen"
            End If

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
            Dim value As Tuple(Of String, String) = _setInfoDelegate()
            Source = value.Item1
            Code = value.Item2
        End If
    End Sub
#End Region

    Private Sub CtrDebitCredit_Load(sender As Object, e As EventArgs) Handles Me.Load
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
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDLblSource)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDLblCode)

    End Sub

End Class
