Imports System.Runtime.Serialization

Public Class ProductionCenter

#Region "Properties"

    <DataMember()>
    Property NullTextOrganizationalStructure As String

    <DataMember()>
    Property NumberNameMainAccountCancellationCost As String

    Public ReadOnly Property CodeName As String
        Get
            Return (Me.Code & " - " & Me.Name)
        End Get
    End Property

#End Region

End Class
