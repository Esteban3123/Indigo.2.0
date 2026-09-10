Imports System.Runtime.Serialization

Partial Public Class Company

    Private _CodeNameConcatenated As String

    <DataMember()>
    Public Property CodeNameConcatenated As String
        Get
            Return Me._CodeNameConcatenated
        End Get
        Set(value As String)
            Me._CodeNameConcatenated = value
        End Set
    End Property

End Class
