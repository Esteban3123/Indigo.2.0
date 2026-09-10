NotInheritable Class RTFCalculator
    Private Shared _Editor As DevExpress.XtraEditors.Repository.RepositoryItemRichTextEdit
    Private Shared _Viewer As DevExpress.XtraEditors.Controls.Rtf.RtfViewer

    Public Shared Function CalculateTextHeight(control As DevExpress.XtraRichEdit.RichEditControl) As Integer
        _Editor = New DevExpress.XtraEditors.Repository.RepositoryItemRichTextEdit()
        _Viewer = New DevExpress.XtraEditors.Controls.Rtf.RtfViewer(_Editor)
        Dim graphics As Graphics = control.CreateGraphics()
        Try
            _Viewer.EditValue = control.RtfText
            Return _Viewer.GetEditorHeight(graphics, control.Width, Integer.MaxValue, control.ActiveView.ZoomFactor)
        Finally
            graphics.Dispose()
        End Try
    End Function
End Class
