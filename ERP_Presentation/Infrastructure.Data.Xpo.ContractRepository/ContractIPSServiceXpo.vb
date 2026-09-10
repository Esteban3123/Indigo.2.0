Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
<Persistent("Contract.IPSService")>
Public Class ContractIPSServiceXPO
    Inherits XPLiteObject
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
    Dim fServiceManual As Byte
    Public Property ServiceManual() As Byte
        Get
            Return fServiceManual
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("ServiceManual", fServiceManual, value)
        End Set
    End Property

    <PersistentAlias("Iif(ServiceManual = 1, 'ISS 2001', ServiceManual = 2, 'ISS 2004', ServiceManual = 3, 'SOAT', ServiceManual = 4, 'Institucional', '')")>
    Public ReadOnly Property ServiceManualName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("ServiceManualName"))
        End Get
    End Property

    Dim fServiceClass As Byte
    Public Property ServiceClass() As Byte
        Get
            Return fServiceClass
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("ServiceClass", fServiceClass, value)
        End Set
    End Property

    <PersistentAlias("Iif(ServiceClass = 1, 'Ninguno', ServiceClass = 2, 'Cirujano', ServiceClass = 3, 'Anestesiólogo', ServiceClass = 4, 'Ayudante', ServiceClass = 5, 'Derecho Sala', ServiceClass = 6, 'Materiales Sutura', ServiceClass = 7, 'Instrumentación Quirúrgica', '')")>
    Public ReadOnly Property ServiceClassName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("ServiceClassName"))
        End Get
    End Property

    Dim fAssociatedMaterialIPSServiceId As ContractIPSServiceXPO
    <Association("AssociatedMaterialIPSServiceReferencesIPSService")>
    Public Property AssociatedMaterialIPSServiceId() As ContractIPSServiceXPO
        Get
            Return fAssociatedMaterialIPSServiceId
        End Get
        Set(ByVal value As ContractIPSServiceXPO)
            SetPropertyValue(Of ContractIPSServiceXPO)("AssociatedMaterialIPSServiceId", fAssociatedMaterialIPSServiceId, value)
        End Set
    End Property

    <PersistentAlias("Iif(Presentation = 1, 'No Quirurgico', Iif(Presentation = 2, 'Quirurgico',Iif(Presentation = 3, 'Paquete', '')))")>
    Public ReadOnly Property PresentationName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("PresentationName"))
        End Get
    End Property

    Dim fSelectOption As Boolean
    <NonPersistent()>
    Public Property SelectOption As Boolean
        Get
            Return fSelectOption
        End Get
        Set(value As Boolean)
            fSelectOption = value
        End Set
    End Property

    Dim fServiceType As Byte
    Public Property ServiceType() As Byte
        Get
            Return fServiceType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("ServiceType", fServiceType, value)
        End Set
    End Property
    Dim fPresentation As Byte
    Public Property Presentation() As Byte
        Get
            Return fPresentation
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Presentation", fPresentation, value)
        End Set
    End Property
    Dim fAuthorizationLevel As Byte
    Public Property AuthorizationLevel() As Byte
        Get
            Return fAuthorizationLevel
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("AuthorizationLevel", fAuthorizationLevel, value)
        End Set
    End Property
    Dim fContributionsWeeks As Integer
    Public Property ContributionsWeeks() As Integer
        Get
            Return fContributionsWeeks
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ContributionsWeeks", fContributionsWeeks, value)
        End Set
    End Property
    Dim fProcedure As Byte
    Public Property Procedure() As Byte
        Get
            Return fProcedure
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Procedure", fProcedure, value)
        End Set
    End Property
    Dim fSubattentionCode As Byte
    Public Property SubattentionCode() As Byte
        Get
            Return fSubattentionCode
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("SubattentionCode", fSubattentionCode, value)
        End Set
    End Property
    Dim fMinimunAgeUnit As Byte
    Public Property MinimunAgeUnit() As Byte
        Get
            Return fMinimunAgeUnit
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("MinimunAgeUnit", fMinimunAgeUnit, value)
        End Set
    End Property
    Dim fMinimunAge As Integer
    Public Property MinimunAge() As Integer
        Get
            Return fMinimunAge
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("MinimunAge", fMinimunAge, value)
        End Set
    End Property
    Dim fMaximumAgeUnit As Byte
    Public Property MaximumAgeUnit() As Byte
        Get
            Return fMaximumAgeUnit
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("MaximumAgeUnit", fMaximumAgeUnit, value)
        End Set
    End Property
    Dim fMaximumAge As Integer
    Public Property MaximumAge() As Integer
        Get
            Return fMaximumAge
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("MaximumAge", fMaximumAge, value)
        End Set
    End Property
    Dim fInMale As Boolean
    Public Property InMale() As Boolean
        Get
            Return fInMale
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("InMale", fInMale, value)
        End Set
    End Property
    Dim fInFemale As Boolean
    Public Property InFemale() As Boolean
        Get
            Return fInFemale
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("InFemale", fInFemale, value)
        End Set
    End Property
    Dim fChildbirthAbortion As Boolean
    Public Property ChildbirthAbortion() As Boolean
        Get
            Return fChildbirthAbortion
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("ChildbirthAbortion", fChildbirthAbortion, value)
        End Set
    End Property
    Dim fPOS As Boolean
    Public Property POS() As Boolean
        Get
            Return fPOS
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("POS", fPOS, value)
        End Set
    End Property
    Dim fComplexityLevel As Byte
    Public Property ComplexityLevel() As Byte
        Get
            Return fComplexityLevel
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("ComplexityLevel", fComplexityLevel, value)
        End Set
    End Property
    Dim fPromotionAndPrevention As Boolean
    Public Property PromotionAndPrevention() As Boolean
        Get
            Return fPromotionAndPrevention
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("PromotionAndPrevention", fPromotionAndPrevention, value)
        End Set
    End Property
    Dim fPromotionAndPreventionActivities As String
    <Size(SizeAttribute.Unlimited)>
    Public Property PromotionAndPreventionActivities() As String
        Get
            Return fPromotionAndPreventionActivities
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PromotionAndPreventionActivities", fPromotionAndPreventionActivities, value)
        End Set
    End Property
    Dim fSurgeryArtroscopica As Boolean
    Public Property SurgeryArtroscopica() As Boolean
        Get
            Return fSurgeryArtroscopica
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("SurgeryArtroscopica", fSurgeryArtroscopica, value)
        End Set
    End Property
    Dim fPathologyService As Boolean
    Public Property PathologyService() As Boolean
        Get
            Return fPathologyService
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("PathologyService", fPathologyService, value)
        End Set
    End Property

    <PersistentAlias("IVA.Id")>
    Public ReadOnly Property IVAId() As Integer
        Get
            Return Convert.ToInt32(EvaluateAlias("IVAId"))
        End Get
    End Property

    Dim fIVA As GeneralLedgerIVAXpo
    <Persistent("IVAId")>
    <Association("AssociatedIPSServiceReferencesIVA")>
    Public Property IVA() As GeneralLedgerIVAXpo
        Get
            Return fIVA
        End Get
        Set(ByVal value As GeneralLedgerIVAXpo)
            SetPropertyValue(Of GeneralLedgerIVAXpo)("IVA", fIVA, value)
        End Set
    End Property

    Dim fTaxedProduct As Boolean
    Public Property TaxedProduct() As Boolean
        Get
            Return fTaxedProduct
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("TaxedProduct", fTaxedProduct, value)
        End Set
    End Property

    Dim fStatus As Boolean
    <Persistent("Status")>
    Public Property Status() As Boolean
        Get
            Return fStatus
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Status", fStatus, value)
        End Set
    End Property

    <PersistentAlias("Iif(Status = 1, 'Activo', 'Inactivo')")>
    Public ReadOnly Property StatusName As String
        Get
            'If fStatus Then
            '    Return ResourceManager.GetString("StateActive")
            'Else
            '    Return ResourceManager.GetString("StateInactive")
            'End If
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
        End Get
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
    'columna que devuelve el codigo y el nombre concatenado
    <Size(50)>
    <PersistentAlias("concat(concat(Code,' - '),Name)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property



    <Association("AssociatedMaterialIPSServiceReferencesIPSService", GetType(ContractIPSServiceXPO))>
    Public ReadOnly Property ContractIPSServiceXPO() As XPCollection(Of ContractIPSServiceXPO)
        Get
            Return GetCollection(Of ContractIPSServiceXPO)("ContractIPSServiceXPO")
        End Get
    End Property



    <Association("RateManualDetailReferencesIPSService", GetType(RateManualDetailXpo))>
    Public ReadOnly Property RateManualDetailXpo() As XPCollection(Of RateManualDetailXpo)
        Get
            Return GetCollection(Of RateManualDetailXpo)("RateManualDetailXpo")
        End Get
    End Property

    <Association("Contract_CupsHomologationReferencesContract_IPSService", GetType(ContractCupsHomologationXpo))>
    Public ReadOnly Property Contract_CupsHomologations() As XPCollection(Of ContractCupsHomologationXpo)
        Get
            Return GetCollection(Of ContractCupsHomologationXpo)("Contract_CupsHomologations")
        End Get
    End Property

    <Association("DefinitionRateDetailReferencesIPSService", GetType(DefinitionRateDetailXpo))>
    Public ReadOnly Property DefinitionRateDetailXpo() As XPCollection(Of DefinitionRateDetailXpo)
        Get
            Return GetCollection(Of DefinitionRateDetailXpo)("DefinitionRateDetailXpo")
        End Get
    End Property

    <Association("SurgicalProceduresServiceReferencesIPSService", GetType(SurgicalProcedureServiceXpo))>
    Public ReadOnly Property SurgicalProcedureServiceXpo() As XPCollection(Of SurgicalProcedureServiceXpo)
        Get
            Return GetCollection(Of SurgicalProcedureServiceXpo)("SurgicalProcedureServiceXpo")
        End Get
    End Property

    <Association("DefinitionRateDetailSurgicalReferencesIPSService", GetType(DefinitionRateDetailSurgicalProceduresXpo))>
    Public ReadOnly Property DefinitionRateDetailSurgicalProceduresXpo() As XPCollection(Of DefinitionRateDetailSurgicalProceduresXpo)
        Get
            Return GetCollection(Of DefinitionRateDetailSurgicalProceduresXpo)("DefinitionRateDetailSurgicalProceduresXpo")
        End Get
    End Property

    <Association("ContractPackagesReferencesIPSService", GetType(ContractPackageXpo))>
    Public ReadOnly Property ContractPackages() As XPCollection(Of ContractPackageXpo)
        Get
            Return GetCollection(Of ContractPackageXpo)("ContractPackages")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
