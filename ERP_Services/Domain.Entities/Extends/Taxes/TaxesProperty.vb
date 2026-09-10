Imports System.Runtime.Serialization

Partial Public Class TaxesProperty

    Public ReadOnly Property ThirdPartyName As String
        Get
            If Me.ThirdParty IsNot Nothing Then
                Return Me.ThirdParty.Name
            End If
            Return String.Empty
        End Get
    End Property

End Class
