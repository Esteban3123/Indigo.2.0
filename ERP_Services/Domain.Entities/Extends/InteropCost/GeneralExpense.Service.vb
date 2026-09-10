Imports System.Runtime.Serialization

Public Class GeneralExpense

#Region "Properties"
    <DataMember()>
    Property FullNameMainAccount As String

    Public ReadOnly Property CodeName As String
        Get
            Return (Me.Code & " - " & Me.Name)
        End Get
    End Property

    <DataMember()>
    Public Property CategoryCodeName As String

#End Region

End Class
