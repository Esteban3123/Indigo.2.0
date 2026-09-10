Public Class CtrDigitalSignature

    Private _digitalSignature As Byte()

    Public Property DigitalSignature As Byte()
        Get
            Return _digitalSignature
        End Get
        Set(value As Byte())
            _digitalSignature = value
            INDpeDigitalSignature.EditValue = value
        End Set
    End Property

    Private Sub BtnChangeSignature_Click(sender As Object, e As EventArgs) Handles BtnChangeSignature.Click
        INDOpenFileDialog.Filter = "Imagenes |*.bmp;*.jpg;*.png"
        If INDOpenFileDialog.ShowDialog(Me) = DialogResult.OK Then
            Dim img As Image = Image.FromFile(INDOpenFileDialog.FileName)
            Dim data As Byte() = DevExpress.XtraEditors.Controls.ByteImageConverter.ToByteArray(img, img.RawFormat)
            DigitalSignature = data
        End If
    End Sub
End Class
