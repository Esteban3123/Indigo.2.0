Imports DevExpress.Xpo

<Persistent("Billing.Quotation")>
Public Class QuotationXpo
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

    Dim fDocumentDate As DateTime
    Public Property DocumentDate() As DateTime
        Get
            Return fDocumentDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("DocumentDate", fDocumentDate, value)
        End Set
    End Property

    Dim fQuotationType As Integer
    Public Property QuotationType() As Integer
        Get
            Return fQuotationType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("QuotationType", fQuotationType, value)
        End Set
    End Property

    'Dim fAdmissionNumber As ViewQuotationAdmissionInformationXpo
    '<Association("BillingViewAdmissionInformation_References_BillingQuotation")>
    'Public Property AdmissionNumber() As ViewQuotationAdmissionInformationXpo
    '    Get
    '        Return fAdmissionNumber
    '    End Get
    '    Set(ByVal value As ViewQuotationAdmissionInformationXpo)
    '        SetPropertyValue(Of ViewQuotationAdmissionInformationXpo)("AdmissionNumber", fAdmissionNumber, value)
    '    End Set
    'End Property

    Dim fThirdPartyId As ThirdPartyXpo
    <Association("QuotationReferencesCommon_ThirdParty")>
    Public Property ThirdPartyId() As ThirdPartyXpo
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As ThirdPartyXpo)
            SetPropertyValue(Of ThirdPartyXpo)("ThirdPartyId", fThirdPartyId, value)
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

#End Region

#Region "Custom Members"

    <PersistentAlias("Iif(Status = 1, 'Registrado', Iif(Status = 2, 'Confirmado',Iif(Status = 3, 'Anulado', '')))")>
    Public ReadOnly Property StatusName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
        End Get
    End Property

    <PersistentAlias("Iif(QuotationType = 1, 'Intrahospitalario', 'Ambulatoria')")>
    Public ReadOnly Property QuotationTypeName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("QuotationTypeName"))
        End Get
    End Property

    <PersistentAlias("AdmissionInformations[].Single()")>
    Public ReadOnly Property AdmissionInformation() As ViewQuotationAdmissionInformationXpo
        Get
            Return CType(Me.EvaluateAlias("AdmissionInformation"), ViewQuotationAdmissionInformationXpo)
        End Get
    End Property

#End Region

#Region "Navigation"

    <Aggregated, Association("BillingViewAdmissionInformation_References_BillingQuotation")>
    Public ReadOnly Property AdmissionInformations() As XPCollection(Of ViewQuotationAdmissionInformationXpo)
        Get
            Return GetCollection(Of ViewQuotationAdmissionInformationXpo)("AdmissionInformations")
        End Get
    End Property

    <Association("QuotationServiceOrderDetailReferencesQuotation", GetType(QuotationServiceOrderDetailXpo))>
    Public ReadOnly Property QuotationServiceOrderDetailXpo() As XPCollection(Of QuotationServiceOrderDetailXpo)
        Get
            Return GetCollection(Of QuotationServiceOrderDetailXpo)("QuotationServiceOrderDetailXpo")
        End Get
    End Property

    <Association("QuotationPharmaceuticalDispensingDetailReferencesQuotation", GetType(QuotationPharmaceuticalDispensingDetailXpo))>
    Public ReadOnly Property QuotationPharmaceuticalDispensingDetailXpo() As XPCollection(Of QuotationPharmaceuticalDispensingDetailXpo)
        Get
            Return GetCollection(Of QuotationPharmaceuticalDispensingDetailXpo)("QuotationPharmaceuticalDispensingDetailXpo")
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