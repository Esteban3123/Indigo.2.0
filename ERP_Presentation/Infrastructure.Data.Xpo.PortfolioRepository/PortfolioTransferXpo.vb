Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports Infrastructure.CrossCutting.Resources

<Persistent("Portfolio.PortfolioTransfer")> _
Public Class PortfolioTransferXpo
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
    <Size(20)>
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
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

    Dim fThirdPartyId As CommonThirdPartyXpo
    <Association("Portfolio_PortfolioTransferReferencesCommon_Thirdparty")> _
    Public Property ThirdPartyId() As CommonThirdPartyXpo
        Get
            Return fThirdPartyId
        End Get
        Set(value As CommonThirdPartyXpo)
            SetPropertyValue(Of CommonThirdPartyXpo)("ThirdPartyId", fThirdPartyId, value)
        End Set
    End Property

    Dim fCustomerId As CustomerXpo
    <Association("Portfolio_PortfolioTransferReferencesCommon_Customer")> _
    Public Property CustomerId() As CustomerXpo
        Get
            Return fCustomerId
        End Get
        Set(ByVal value As CustomerXpo)
            SetPropertyValue(Of CustomerXpo)("CustomerId", fCustomerId, value)
        End Set
    End Property

    Dim fPortfolioAdvanceId As PortfolioAdvanceXpo
    <Association("Portfolio_PortfolioTransferReferences_PortfolioAdvance")>
    Public Property PortfolioAdvanceId() As PortfolioAdvanceXpo
        Get
            Return fPortfolioAdvanceId
        End Get
        Set(ByVal value As PortfolioAdvanceXpo)
            SetPropertyValue(Of PortfolioAdvanceXpo)("PortfolioAdvanceId", fPortfolioAdvanceId, value)
        End Set
    End Property

    Dim fMainAccountId As Integer
    Public Property MainAccountId() As Integer
        Get
            Return fMainAccountId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("MainAccountId", fMainAccountId, value)
        End Set
    End Property

    Dim fCostCenterId As Integer
    Public Property CostCenterId() As Integer
        Get
            Return fCostCenterId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CostCenterId", fCostCenterId, value)
        End Set
    End Property

    Dim fObservations As String
    <Size(300)>
    Public Property Observations() As String
        Get
            Return fObservations
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Observations", fObservations, value)
        End Set
    End Property

    Dim fOperatingUnitId As Integer
    Public Property OperatingUnitId() As Integer
        Get
            Return fOperatingUnitId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("OperatingUnitId", fOperatingUnitId, value)
        End Set
    End Property

    Dim fStatus As Byte
    Public Property Status() As Byte
        Get
            Return fStatus
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Status", fStatus, value)
        End Set
    End Property

    Dim fCreationUser As String
    <Size(20)>
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
    <Size(20)>
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

    Dim fConfirmationUser As String
    <Size(20)>
    Public Property ConfirmationUser() As String
        Get
            Return fConfirmationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ConfirmationUser", fConfirmationUser, value)
        End Set
    End Property

    Dim fConfirmationDate As DateTime
    Public Property ConfirmationDate() As DateTime
        Get
            Return fConfirmationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ConfirmationDate", fConfirmationDate, value)
        End Set
    End Property

    Dim fAnnulmentUser As String
    <Size(20)>
    Public Property AnnulmentUser() As String
        Get
            Return fAnnulmentUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AnnulmentUser", fAnnulmentUser, value)
        End Set
    End Property

    Dim fAnnulmentDate As DateTime
    Public Property AnnulmentDate() As DateTime
        Get
            Return fAnnulmentDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("AnnulmentDate", fAnnulmentDate, value)
        End Set
    End Property

    

#End Region

#Region "Custom Members"

    <PersistentAlias("Iif(Status = 1, 'Registrado', Status = 2, 'Confirmado', Status = 3, 'Anulado', Status = 4, 'Reversado', '')")>
    Public ReadOnly Property StatusName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
        End Get
    End Property

    <PersistentAlias("Iif(CustomerId.Id > 0, CustomerId.NitName, ThirdPartyId.Id > 0, ThirdPartyId.NitName, '')")>
    Public ReadOnly Property NameCustomer() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("NameCustomer"))
        End Get
    End Property

    <PersistentAlias("concat(concat(Code, ' - '), NameCustomer)")>
    Public ReadOnly Property CodeNameCustomer() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeNameCustomer"))
        End Get
    End Property

#End Region

#Region "Navigations"

    <Association("PortfolioTransferDetailReferencesPortfolioTransfer", GetType(PortfolioTransferDetailXpo))> _
    Public ReadOnly Property PortfolioTransferDetailXpo() As XPCollection(Of PortfolioTransferDetailXpo)
        Get
            Return GetCollection(Of PortfolioTransferDetailXpo)("PortfolioTransferDetailXpo")
        End Get
    End Property

    <Association("Portfolio_PortfolioNoteReferences_PortfolioTransfer", GetType(PortfolioNoteXpo))>
    Public ReadOnly Property Portfolio_PortfolioNote() As XPCollection(Of PortfolioNoteXpo)
        Get
            Return GetCollection(Of PortfolioNoteXpo)("Portfolio_PortfolioNote")
        End Get
    End Property


    <Association("Portfolio_PortfolioNoteReportReferences_PortfolioTransfer", GetType(PortfolioNoteReportXpo))>
    Public ReadOnly Property Portfolio_PortfolioNoteReport() As XPCollection(Of PortfolioNoteReportXpo)
        Get
            Return GetCollection(Of PortfolioNoteReportXpo)("Portfolio_PortfolioNoteReport")
        End Get
    End Property

    <Association("PortfolioTransferOtherConceptXpoReferencesPortfolioTransferXpo", GetType(PortfolioTransferOtherConceptXpo))> _
    Public ReadOnly Property PortfolioTransferOtherConceptXpo() As XPCollection(Of PortfolioTransferOtherConceptXpo)
        Get
            Return GetCollection(Of PortfolioTransferOtherConceptXpo)("PortfolioTransferOtherConceptXpo")
        End Get
    End Property

    <PersistentAlias("PortfolioTransferDetailXpo.Sum(Value)")>
    Public ReadOnly Property Value() As Decimal
        Get
            Return Convert.ToDecimal(Me.EvaluateAlias("Value"))
        End Get
    End Property

    <PersistentAlias("PortfolioTransferOtherConceptXpo.Sum(Iif(Nature = 1,Value, Nature = 2, Value,0))")>
    Public ReadOnly Property ValueOtherConcept() As Decimal
        Get
                Return Convert.ToDecimal(Me.EvaluateAlias("ValueOtherConcept"))
        End Get
    End Property

    <PersistentAlias("Value+ValueOtherConcept")>
    Public ReadOnly Property ValueTotal() As Decimal
        Get
            Return Convert.ToDecimal(Me.EvaluateAlias("ValueTotal"))
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
