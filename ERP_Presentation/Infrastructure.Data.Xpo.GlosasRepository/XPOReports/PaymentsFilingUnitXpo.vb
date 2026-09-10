Imports DevExpress.Xpo

<Persistent("Payments.FilingUnit")>
Public Class PaymentsFilingUnitXpo
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

    Dim fCode As String
    <Persistent("Code")>
    Public Property Codigo() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property

    Dim fName As String
    <Persistent("Name")>
    Public Property Descripcion() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property

#End Region

#Region "Navigators"

    <Association("Glosas_TransferJuridicalDebtCollectionC1_References_Payments_FilingUnit", GetType(GlosasTransferJuridicalDebtCollectionCXpo))>
    Public ReadOnly Property TransferJuridicalFilingUnitSource() As XPCollection(Of GlosasTransferJuridicalDebtCollectionCXpo)
        Get
            Return GetCollection(Of GlosasTransferJuridicalDebtCollectionCXpo)("TransferJuridicalFilingUnitSource")
        End Get
    End Property

    <Association("Glosas_TransferJuridicalDebtCollectionC2_References_Payments_FilingUnit", GetType(GlosasTransferJuridicalDebtCollectionCXpo))>
    Public ReadOnly Property TransferJuridicalFilingUnitTarget() As XPCollection(Of GlosasTransferJuridicalDebtCollectionCXpo)
        Get
            Return GetCollection(Of GlosasTransferJuridicalDebtCollectionCXpo)("TransferJuridicalFilingUnitTarget")
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
