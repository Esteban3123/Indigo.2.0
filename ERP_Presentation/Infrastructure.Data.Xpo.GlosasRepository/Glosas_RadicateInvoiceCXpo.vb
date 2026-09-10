Imports DevExpress.Xpo

Partial Public Class Glosas_RadicateInvoiceCXpo

#Region "Custom Members"

    <NonPersistent>
    Public Property StateOperation As Byte

#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

#End Region

End Class