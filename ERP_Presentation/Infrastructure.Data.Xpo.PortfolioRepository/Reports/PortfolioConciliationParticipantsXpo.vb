#Region "Imports"

Imports DevExpress.Xpo

#End Region

<Persistent("Portfolio.PortfolioConciliationParticipants")>
Public Class PortfolioConciliationParticipantsXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fId As Integer

    <Key(True)>
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property

    Dim fPortfolioConciliationId As PortfolioConciliationXpo
    <Association("Portfolio_PortfolioConciliationXpo_PortfolioConciliationId")>
    Public Property PortfolioConciliationId() As PortfolioConciliationXpo
        Get
            Return fPortfolioConciliationId
        End Get
        Set(ByVal value As PortfolioConciliationXpo)
            SetPropertyValue(Of PortfolioConciliationXpo)("PortfolioConciliationId", fPortfolioConciliationId, value)
        End Set
    End Property

    Dim fFullName As String
    Public Property FullName() As String
        Get
            Return fFullName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FullName", fFullName, value)
        End Set
    End Property

    Dim fPosition As String
    Public Property Position() As String
        Get
            Return fPosition
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Position", fPosition, value)
        End Set
    End Property

    Dim fType As Byte
    Public Property Type() As Byte
        Get
            Return fType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Type", fType, value)
        End Set
    End Property

#End Region

#Region "Custom Members"

    <PersistentAlias("Iif(Type = 1, 'IPS',Type = 2, 'EAPB','N/A')")>
    Public ReadOnly Property TypeName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("TypeName"))
        End Get
    End Property

#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

#End Region

End Class