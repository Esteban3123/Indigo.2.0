Public Class CtrDilutionFactor

#Region "Delegate"

#End Region

#Region "Properties"
    ''' <summary>
    ''' Nombre de medicamento
    ''' </summary>
    Public WriteOnly Property MedicineName As String
        Set(value As String)
            INDlblMedicine.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Peso con unidad de medida
    ''' </summary>
    Public WriteOnly Property WeightStandart As String
        Set(value As String)
            INDlblWeightStandart.Text = value
        End Set
    End Property

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
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDliAdvanceTitle)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDliDilutionFactor)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlblMedicine)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlblWeightStandart)
    End Sub
End Class