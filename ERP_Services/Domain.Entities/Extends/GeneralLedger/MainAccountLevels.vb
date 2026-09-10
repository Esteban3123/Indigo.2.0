Imports System.Runtime.Serialization

Partial Public Class MainAccountLevels
    <DataMember>
    Public Property CodeName As String
        Get
            If Not String.IsNullOrEmpty(Code) Then
                Return $"{Code} - {Name}"
            End If

            Return ""
        End Get
        Set(value As String)

        End Set
    End Property
End Class
