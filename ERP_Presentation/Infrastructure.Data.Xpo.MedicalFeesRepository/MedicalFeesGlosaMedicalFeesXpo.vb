#Region "Imports"

Imports DevExpress.Xpo

#End Region

<Persistent("MedicalFees.GlosaMedicalFees")>
Public Class GlosaMedicalFeesXpo
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

    Dim fDocumentDate As DateTime?

    Public Property DocumentDate() As DateTime?
        Get
            Return fDocumentDate
        End Get
        Set(ByVal value As DateTime?)
            SetPropertyValue(Of DateTime?)("DocumentDate", fDocumentDate, value)
        End Set
    End Property

    Dim fObservation As String

    Public Property Observation() As String
        Get
            Return fObservation
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Observation", fObservation, value)
        End Set
    End Property

    Dim fPendingValue As Decimal

    Public Property PendingValue() As Decimal
        Get
            Return fPendingValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PendingValue", fPendingValue, value)
        End Set
    End Property

    Dim fGlossedValue As Decimal

    Public Property GlossedValue() As Decimal
        Get
            Return fGlossedValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("GlossedValue", fGlossedValue, value)
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
#End Region
#Region "Associations"
    <Association("GlosaMedicalFeesDetailReferences_GlosaMedicalFees", GetType(GlosaMedicalFeesDetailXpo))>
    Public ReadOnly Property GlosaMedicalFeesDetailXpo() As XPCollection(Of GlosaMedicalFeesDetailXpo)
        Get
            Return GetCollection(Of GlosaMedicalFeesDetailXpo)("GlosaMedicalFeesDetailXpo")
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