Public Class FrmAdecuationLabelDialog
    Public Property LabelTypes As List(Of Byte)

    Public Event ClickJeringa()

    Public Event ClickTablet()

    Public Event ClickNptLabel()

    Public Event ClickBolsa()

    Public Event ClickMagistral()

    Private Sub FrmAdecuationLabelDialog_KeyDown(sender As Object, e As Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Close()
        End If
    End Sub

    Private Sub FrmAdecuationLabelDialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        showLabelTypes()
    End Sub

    Private Sub showLabelTypes()
        Dim count As Integer = 0

        If LabelTypes.Contains(1) Then
            INDLciBolsa.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            count += 1
        End If

        If LabelTypes.Contains(2) Then
            INDLciNutricionParenteral.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            count += 1
        End If

        If LabelTypes.Contains(3) Then
            INDLciJeringa.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            count += 1
        End If

        If LabelTypes.Contains(4) Then
            INDLciTableteria4x4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            count += 1
        End If

        If LabelTypes.Contains(5) Then
            INDLciMagistral.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            count += 1
        End If

        Me.Size = New Drawing.Size(Me.Size.Width, 70 + 50 * count)
    End Sub

    Private Sub INDSbJeringa_Click(sender As Object, e As EventArgs) Handles INDSbJeringa.Click
        RaiseEvent ClickJeringa()
    End Sub

    Private Sub INDSbTableteria4x4_Click(sender As Object, e As EventArgs) Handles INDSbTableteria4x4.Click
        RaiseEvent ClickTablet()
    End Sub

    Private Sub INDSbNutricionParenteral_Click(sender As Object, e As EventArgs) Handles INDSbNutricionParenteral.Click
        RaiseEvent ClickNptLabel()
    End Sub

    Private Sub INDSbBolsa_Click(sender As Object, e As EventArgs) Handles INDSbBolsa.Click
        RaiseEvent ClickBolsa()
    End Sub

    Private Sub INDSbMagistralLabel_Click(sender As Object, e As EventArgs) Handles INDSbMagistral.Click
        RaiseEvent ClickMagistral()
    End Sub
End Class