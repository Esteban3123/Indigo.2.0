Imports DevExpress.Xpo

<Persistent("Contract.ViewContractCareGroup")>
Public Class ViewContractCareGroupXpo
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
    Dim fName As String
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property
    Dim fDefaultManual As Byte
    Public Property DefaultManual() As Byte
        Get
            Return fDefaultManual
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("DefaultManual", fDefaultManual, value)
        End Set
    End Property
    Dim fCareGroupType As Byte
    Public Property CareGroupType() As Byte
        Get
            Return fCareGroupType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("CareGroupType", fCareGroupType, value)
        End Set
    End Property
    Dim fContractId As Integer
    Public Property ContractId() As Integer
        Get
            Return fContractId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ContractId", fContractId, value)
        End Set
    End Property
    Dim fLiquidationType As Byte
    Public Property LiquidationType() As Byte
        Get
            Return fLiquidationType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("LiquidationType", fLiquidationType, value)
        End Set
    End Property
    Dim fBillingPeriod As Byte
    Public Property BillingPeriod() As Byte
        Get
            Return fBillingPeriod
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("BillingPeriod", fBillingPeriod, value)
        End Set
    End Property
    Dim fMaximumIndividualBilling As Decimal
    Public Property MaximumIndividualBilling() As Decimal
        Get
            Return fMaximumIndividualBilling
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("MaximumIndividualBilling", fMaximumIndividualBilling, value)
        End Set
    End Property
    Dim fPeriodMaximumBilling As Decimal
    Public Property PeriodMaximumBilling() As Decimal
        Get
            Return fPeriodMaximumBilling
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PeriodMaximumBilling", fPeriodMaximumBilling, value)
        End Set
    End Property

    Dim fRequirementsTemplateId As Integer
    Public Property RequirementsTemplateId() As Integer
        Get
            Return fRequirementsTemplateId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RequirementsTemplateId", fRequirementsTemplateId, value)
        End Set
    End Property

    Dim fInvoiceDeadlines As Integer
    Public Property InvoiceDeadlines() As Integer
        Get
            Return fInvoiceDeadlines
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("InvoiceDeadlines", fInvoiceDeadlines, value)
        End Set
    End Property

    Dim fProcedureTemplateId As Integer
    Public Property ProcedureTemplateId() As Integer
        Get
            Return fProcedureTemplateId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ProcedureTemplateId", fProcedureTemplateId, value)
        End Set
    End Property

    Dim fProductRateId As Integer
    Public Property ProductRateId() As Integer
        Get
            Return fProductRateId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ProductRateId", fProductRateId, value)
        End Set
    End Property
    Dim fConceptToBill As Byte
    Public Property ConceptToBill() As Byte
        Get
            Return fConceptToBill
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("ConceptToBill", fConceptToBill, value)
        End Set
    End Property
    Dim fEntityType As Byte
    Public Property EntityType() As Byte
        Get
            Return fEntityType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("EntityType", fEntityType, value)
        End Set
    End Property

    Dim fStatus As Boolean
    Public Property Status() As Boolean
        Get
            Return fStatus
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Status", fStatus, value)
        End Set
    End Property

    Dim fApplyRIAS As Boolean
    Public Property ApplyRIAS() As Boolean
        Get
            Return fApplyRIAS
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("ApplyRIAS", fApplyRIAS, value)
        End Set
    End Property

    Dim fAuthorizationRequired As Boolean
    Public Property AuthorizationRequired() As Boolean
        Get
            Return fAuthorizationRequired
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("AuthorizationRequired", fAuthorizationRequired, value)
        End Set
    End Property

    Dim fThirdPartyId As Integer
    Public Property ThirdPartyId() As Integer
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ThirdPartyId", fThirdPartyId, value)
        End Set
    End Property

#End Region
#Region "CustomMembers"

    <PersistentAlias("Iif(EntityType = 1, 'EPS',Iif(EntityType = 2, 'EPS-S',Iif(EntityType = 3, 'Vinculados',Iif(EntityType = 4, 'ARL',Iif(EntityType = 5, 'Plan Complementario EMP',Iif(EntityType = 6, 'IPS Privada',Iif(EntityType = 7, 'IPS Publica',Iif(EntityType = 8, 'Fosyga',Iif(EntityType = 9, 'Particulares',Iif(EntityType = 10, 'Regimen Especial',Iif(EntityType = 11, 'Otros','')))))))))))")>
    Public ReadOnly Property EntityTypeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("EntityTypeName"))
        End Get
    End Property

    <PersistentAlias("Iif(CareGroupType = 1, 'EAPB Con contrato',Iif(CareGroupType = 2, 'EAPB Sin Contrato',Iif(CareGroupType = 3, 'Particulares',Iif(CareGroupType = 4, 'Aseguradoras',''))))")>
    Public ReadOnly Property CareGroupTypeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CareGroupTypeName"))
        End Get
    End Property

    <PersistentAlias("Iif(Status, 'Activo', 'Inactivo')")>
    Public ReadOnly Property StatusName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
        End Get
    End Property

    'columna que devuelve el codigo y el nombre concatenado
    <Size(50)>
    <PersistentAlias("concat(concat(Code,' - '),Name)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property
#End Region

#Region "Builder"

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