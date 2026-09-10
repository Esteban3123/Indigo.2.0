Imports DevExpress.Xpo

<Persistent("Glosas.TransferJuridicalDebtCollectionC")> _
Public Class GlosasTransferJuridicalDebtCollectionCXpo
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

    Dim fJuridicalTransferConsecutive As Decimal
    Public Property JuridicalTransferConsecutive() As Decimal
        Get
            Return fJuridicalTransferConsecutive
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("JuridicalTransferConsecutive", fJuridicalTransferConsecutive, value)
        End Set
    End Property

    Dim fDocumentNumber As String
    Public Property DocumentNumber() As String
        Get
            Return fDocumentNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DocumentNumber", fDocumentNumber, value)
        End Set
    End Property

    Dim fCustomerId As Common_CustomerXpo
    <Association("GlosasTransferJuridicalDebtCollectionCXpoReferencesCommon_CustomerXpo")>
    Public Property CustomerId() As Common_CustomerXpo
        Get
            Return fCustomerId
        End Get
        Set(ByVal value As Common_CustomerXpo)
            SetPropertyValue(Of Common_CustomerXpo)("CustomerId", fCustomerId, value)
        End Set
    End Property

    Dim fJuridicalTransferDate As DateTime
    Public Property JuridicalTransferDate() As DateTime
        Get
            Return fJuridicalTransferDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("JuridicalTransferDate", fJuridicalTransferDate, value)
        End Set
    End Property

    Dim fDocumentDate As DateTime
    Public Property DocumentDate() As DateTime
        Get
            Return fDocumentDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("DocumentDate", fDocumentDate, value)
        End Set
    End Property

    Dim fComment As String
    Public Property Comment() As String
        Get
            Return fComment
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Comment", fComment, value)
        End Set
    End Property

    Dim fState As Char
    Public Property State() As Char
        Get
            Return fState
        End Get
        Set(ByVal value As Char)
            SetPropertyValue(Of Char)("State", fState, value)
        End Set
    End Property

    Dim fCreationUser As String
    Public Property CreationUser() As String
        Get
            Return fCreationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CreationUser", fCreationUser, value)
        End Set
    End Property

    Dim fCreationDate As DateTime
    Public Property CreationDate() As DateTime
        Get
            Return fCreationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("CreationDate", fCreationDate, value)
        End Set
    End Property

    Dim fModificationUser As String
    Public Property ModificationUser() As String
        Get
            Return fModificationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ModificationUser", fModificationUser, value)
        End Set
    End Property

    Dim fModificationDate As DateTime
    Public Property ModificationDate() As DateTime
        Get
            Return fModificationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ModificationDate", fModificationDate, value)
        End Set
    End Property

    Dim fFilingUnitSourceId As PaymentsFilingUnitXpo
    <Association("Glosas_TransferJuridicalDebtCollectionC1_References_Payments_FilingUnit")>
    Public Property FilingUnitSourceId() As PaymentsFilingUnitXpo
        Get
            Return fFilingUnitSourceId
        End Get
        Set(ByVal value As PaymentsFilingUnitXpo)
            SetPropertyValue(Of PaymentsFilingUnitXpo)("FilingUnitSourceId", fFilingUnitSourceId, value)
        End Set
    End Property

    Dim fFilingUnitTargetId As PaymentsFilingUnitXpo
    <Association("Glosas_TransferJuridicalDebtCollectionC2_References_Payments_FilingUnit")>
    Public Property FilingUnitTargetId() As PaymentsFilingUnitXpo
        Get
            Return fFilingUnitTargetId
        End Get
        Set(ByVal value As PaymentsFilingUnitXpo)
            SetPropertyValue(Of PaymentsFilingUnitXpo)("FilingUnitTargetId", fFilingUnitTargetId, value)
        End Set
    End Property

    Dim fLawyerId As PortfolioLawyerXpo
    <Association("Glosas_TransferJuridicalDebtCollectionC_References_Portfolio_Lawyer")>
    Public Property LawyerId() As PortfolioLawyerXpo
        Get
            Return fLawyerId
        End Get
        Set(ByVal value As PortfolioLawyerXpo)
            SetPropertyValue(Of PortfolioLawyerXpo)("LawyerId", fLawyerId, value)
        End Set
    End Property

    Dim fDemandStatusId As PortfolioDemandStatusXpo
    <Association("Glosas_TransferJuridicalDebtCollectionC_References_Portfolio_DemandStatus")>
    Public Property DemandStatusId() As PortfolioDemandStatusXpo
        Get
            Return fDemandStatusId
        End Get
        Set(ByVal value As PortfolioDemandStatusXpo)
            SetPropertyValue(Of PortfolioDemandStatusXpo)("DemandStatusId", fDemandStatusId, value)
        End Set
    End Property

    Dim fReclassified As Boolean
    Public Property Reclassified() As Boolean
        Get
            Return fReclassified
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Reclassified", fReclassified, value)
        End Set
    End Property

#End Region

#Region "Navigation Properties"

    <Association("GlosasTransferJuridicalDebtCollectionDXpoReferencesGlosasTransferJuridicalDebtCollectionCXpo", GetType(GlosasTransferJuridicalDebtCollectionDXpo))>
    Public ReadOnly Property GlosasTransferJuridicalDebtCollectionDXpo() As XPCollection(Of GlosasTransferJuridicalDebtCollectionDXpo)
        Get
            Return GetCollection(Of GlosasTransferJuridicalDebtCollectionDXpo)("GlosasTransferJuridicalDebtCollectionDXpo")
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
