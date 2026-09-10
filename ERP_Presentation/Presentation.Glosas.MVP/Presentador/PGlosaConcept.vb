Public Class PGlosaConcept

    Private _iview As IGlosaConcept

    Public Sub New(iview As IGlosaConcept)
        _iview = iview
    End Sub

    Public Async Sub GetSequense()
        '_iview.AsyncLoader(True)
        Using model As New MBlockRecordAndSequense(_iview.MyTag)
            _iview.Sequence = Await model.GetSequense()
        End Using
        '_iview.AsyncLoader(False)
    End Sub
End Class
