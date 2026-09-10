Imports System.Runtime.Serialization
Imports Domain.Base.Entities

Partial Public Class CommonSequence
    Inherits Entity(Of Domain.Entities.CommonSequence)

    <DataMember()>
    Public Property IsNotManual As Boolean
        Get
            Return Not Me.IsManual
        End Get
        Set(value As Boolean)
            Me.IsManual = Not value
        End Set
    End Property

    <DataMember()>
    Public Property IsNotSequential As Boolean
        Get
            Return Not Me.Sequential
        End Get
        Set(value As Boolean)
            Me.Sequential = Not value
        End Set
    End Property

End Class
