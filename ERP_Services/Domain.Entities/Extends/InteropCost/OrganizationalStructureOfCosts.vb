Imports System.Runtime.Serialization

Partial Public Class OrganizationalStructureOfCosts

    Public ReadOnly Property CodeName As String
        Get
            Return (Me.Code & " - " & Me.Name)
        End Get
    End Property

End Class
