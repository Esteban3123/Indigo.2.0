Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Domain.Payroll.Entities

Public Class PReligiousBeliefs

    Dim View As IReligiousBeliefs

    Dim Indigo As SessionValues

    Public Sub New(ByRef iview As IReligiousBeliefs)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
        Indigo = SessionValues.Instance
    End Sub

    Public Async Sub GetSequense()
        Using model As New MBlockRecordAndSequensePayroll(Me.View.MyTag)
            Me.View.Sequense = Await model.GetSequense()
        End Using
    End Sub

End Class
