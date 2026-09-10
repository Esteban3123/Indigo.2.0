#Region "Imports"

Imports System
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.InventoryRepository


#End Region

<Persistent("Maintenance.MaintenanceContract")>
Public Class MaintenanceContractXpo
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
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property


    Dim fOperatingUnitId As String
    Public Property OperatingUnitId() As String
        Get
            Return fOperatingUnitId
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("OperatingUnitId", fOperatingUnitId, value)
        End Set
    End Property

    Dim fContractTypeId As InventoryContractTypeXpo
    <Association("MaintenanceContractReferencesInventory_InventoryContractType")>
    Public Property ContractTypeId() As InventoryContractTypeXpo
        Get
            Return fContractTypeId
        End Get
        Set(ByVal value As InventoryContractTypeXpo)
            SetPropertyValue(Of InventoryContractTypeXpo)("ContractTypeId", fContractTypeId, value)
        End Set
    End Property

    Dim fDocumentDate As DateTime?
    Public Property DocumentDate() As DateTime?
        Get
            Return fDocumentDate
        End Get
        Set(ByVal value As DateTime?)
            SetPropertyValue(Of DateTime?)("DocumentDate", fDocumentDate, value)
        End Set
    End Property

    Dim fInitialDate As DateTime
    Public Property InitialDate() As DateTime
        Get
            Return fInitialDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("InitialDate", fInitialDate, value)
        End Set
    End Property

    Dim fEndDate As DateTime
    Public Property EndDate() As DateTime
        Get
            Return fEndDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("EndDate", fEndDate, value)
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

    Dim fContractNumber As String
    Public Property ContractNumber() As String
        Get
            Return fContractNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ContractNumber", fContractNumber, value)
        End Set
    End Property

    Dim fSupplierId As Maintenance_Supplier
    <Association("ReferencesCommon_Supplier")>
    Public Property SupplierId() As Maintenance_Supplier
        Get
            Return fSupplierId
        End Get
        Set(ByVal value As Maintenance_Supplier)
            SetPropertyValue(Of Maintenance_Supplier)("SupplierId", fSupplierId, value)
        End Set
    End Property

    Dim fSupplierDistributionLineId As Integer
    Public Property SupplierDistributionLineId() As Integer
        Get
            Return fSupplierDistributionLineId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("SupplierDistributionLineId", fSupplierDistributionLineId, value)
        End Set
    End Property

    Dim fExclusivity As Boolean
    Public Property Exclusivity() As Boolean
        Get
            Return fExclusivity
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Exclusivity", fExclusivity, value)
        End Set
    End Property

    Dim fSourceOrder As Byte
    Public Property SourceOrder() As Byte
        Get
            Return fSourceOrder
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("SourceOrder", fSourceOrder, value)
        End Set
    End Property


    Dim fOnlyGuarantee As Boolean
    Public Property OnlyGuarantee() As Boolean
        Get
            Return fOnlyGuarantee
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("OnlyGuarantee", fOnlyGuarantee, value)
        End Set
    End Property

    Dim fTechnicalSupervicion As String
    <Size(50)>
    Public Property TechnicalSupervicion() As String
        Get
            Return fTechnicalSupervicion
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("TechnicalSupervicion", fTechnicalSupervicion, value)
        End Set
    End Property

    Dim fSupervisionExecution As String
    <Size(50)>
    Public Property SupervisionExecution() As String
        Get
            Return fSupervisionExecution
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SupervisionExecution", fSupervisionExecution, value)
        End Set
    End Property

    Dim fClauses As String
    Public Property Clauses() As String
        Get
            Return fClauses
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Clauses", fClauses, value)
        End Set
    End Property

    Dim fAttachments As String
    Public Property Attachments() As String
        Get
            Return fAttachments
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Attachments", fAttachments, value)
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

    Dim fModificationDate As DateTime?
    Public Property ModificationDate() As DateTime?
        Get
            Return fModificationDate
        End Get
        Set(ByVal value As DateTime?)
            SetPropertyValue(Of DateTime?)("ModificationDate", fModificationDate, value)
        End Set
    End Property

    Dim fConfirmationUser As String
    Public Property ConfirmationUser() As String
        Get
            Return fConfirmationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ConfirmationUser", fConfirmationUser, value)
        End Set
    End Property

    Dim fConfirmationDate As DateTime?
    Public Property ConfirmationDate() As DateTime?
        Get
            Return fConfirmationDate
        End Get
        Set(ByVal value As DateTime?)
            SetPropertyValue(Of DateTime?)("ConfirmationDate", fConfirmationDate, value)
        End Set
    End Property

    Dim fAnnulmentUser As String
    Public Property AnnulmentUser() As String
        Get
            Return fAnnulmentUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AnnulmentUser", fAnnulmentUser, value)
        End Set
    End Property

    Dim fAnnulmentDate As DateTime?
    Public Property AnnulmentDate() As DateTime?
        Get
            Return fAnnulmentDate
        End Get
        Set(ByVal value As DateTime?)
            SetPropertyValue(Of DateTime?)("AnnulmentDate", fAnnulmentDate, value)
        End Set
    End Property
#End Region

#Region "CustomMembers"

    <PersistentAlias("Iif(Status = 1, 'Registrado', Status = 2, 'Confirmado', Status = 3, 'Anulado', Status = 4, 'Finalizado', '')")>
    Public ReadOnly Property StatusName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
        End Get
    End Property


    <PersistentAlias("Concat(Code, ' - ', SupplierId.Name)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property


    <Association("MaintenanceContractDetailReferences_MaintenanceContract", GetType(MaintenanceContractDetailXpo))>
    Public ReadOnly Property MaintenanceContractDetailXpo() As XPCollection(Of MaintenanceContractDetailXpo)
        Get
            Return GetCollection(Of MaintenanceContractDetailXpo)("MaintenanceContractDetailXpo")
        End Get
    End Property


#End Region

#Region "Builder"
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
#End Region

End Class
