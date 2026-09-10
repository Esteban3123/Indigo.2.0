Imports DevExpress.Xpo

<Persistent("Portfolio.DemandStatus")> _
Public Class PortfolioDemandStatusXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fId As Integer
    <Key(True)> _
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property

    Dim fCode As String
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property

    Dim fDescription As String
    Public Property Description() As String
        Get
            Return fDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Description", fDescription, value)
        End Set
    End Property

#End Region

#Region "Navigators"

    <Association("Glosas_TransferJuridicalDebtCollectionC_References_Portfolio_DemandStatus", GetType(GlosasTransferJuridicalDebtCollectionCXpo))>
    Public ReadOnly Property TransferJuridicalDebtCollection() As XPCollection(Of GlosasTransferJuridicalDebtCollectionCXpo)
        Get
            Return GetCollection(Of GlosasTransferJuridicalDebtCollectionCXpo)("TransferJuridicalDebtCollection")
        End Get
    End Property

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
