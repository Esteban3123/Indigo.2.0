Imports System.Runtime.Serialization

Partial Public Class Group

    Private _apply As Boolean = False

    <DataMember()> _
    Public Property Apply() As Boolean
        Get
            Return _apply
        End Get
        Set(ByVal value As Boolean)
            If Not Equals(_apply, value) Then
                _apply = value
                OnPropertyChanged("Apply")
            End If
        End Set
    End Property

End Class
