Imports DevExpress.Xpo

<Persistent("Billing.ViewElectronicsRIPSTraceability")>
Public Class ElectronicRIPSTraceability
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
    Dim fEntityId As Integer
    Public Property EntityId() As Integer
        Get
            Return fEntityId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("EntityId", fEntityId, value)
        End Set
    End Property

    Dim fEntityName As String

    Public Property EntityName() As String
        Get
            Return fEntityName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("EntityName", fEntityName, value)
        End Set
    End Property

    Dim fOperativeUnitId As Integer
    Public Property OperativeUnitId() As Integer
        Get
            Return fOperativeUnitId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("OperativeUnitId", fOperativeUnitId, value)
        End Set
    End Property

    Dim fDocumentTypeId As Integer
    Public Property DocumentTypeId() As Integer
        Get
            Return fDocumentTypeId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("DocumentTypeId", fDocumentTypeId, value)
        End Set
    End Property

    Dim fDocumentTypeName As String
    Public Property DocumentTypeName() As String
        Get
            Return fDocumentTypeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DocumentTypeName", fDocumentTypeName, value)
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

    Dim fDocumentDate As DateTime
    Public Property DocumentDate() As DateTime
        Get
            Return fDocumentDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("DocumentDate ", fDocumentDate, value)
        End Set
    End Property

    Dim fThirdPartyId As Integer
    Public Property ThirdPartyId() As Integer
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ThirdPartyId ", fThirdPartyId, value)
        End Set
    End Property

    Dim fThirdPartyNit As String
    Public Property ThirdPartyNit() As String
        Get
            Return fThirdPartyNit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ThirdPartyNit ", fThirdPartyNit, value)
        End Set
    End Property

    Dim fThirdPartyName As String
    Public Property ThirdPartyName() As String
        Get
            Return fThirdPartyName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ThirdPartyName ", fThirdPartyName, value)
        End Set
    End Property

    Dim fPatientCode As String
    Public Property PatientCode() As String
        Get
            Return fPatientCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientCode ", fPatientCode, value)
        End Set
    End Property

    Dim fPatientName As String
    Public Property PatientName() As String
        Get
            Return fPatientName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientName ", fPatientName, value)
        End Set
    End Property

    Dim fCUV As String
    Public Property CUV() As String
        Get
            Return fCUV
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CUV ", fCUV, value)
        End Set
    End Property

    Dim fStatusRIPS As Int16
    Public Property StatusRIPS() As Int16
        Get
            Return fStatusRIPS
        End Get
        Set(ByVal value As Int16)
            SetPropertyValue(Of Int16)("StatusRIPS ", fStatusRIPS, value)
        End Set
    End Property

    Dim fsendDate As DateTime
    Public Property sendDate() As DateTime
        Get
            Return fsendDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("sendDate ", fsendDate, value)
        End Set
    End Property

    Dim fCosmoDBId As String
    Public Property CosmoDBId() As String
        Get
            Return fCosmoDBId
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CosmoDBId ", fCosmoDBId, value)
        End Set
    End Property

    Dim fAdmissionNumber As String
    Public Property AdmissionNumber() As String
        Get
            Return fAdmissionNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AdmissionNumber ", fAdmissionNumber, value)
        End Set
    End Property

    Dim fCareGroupCode As String
    Public Property CareGroupCode() As String
        Get
            Return fCareGroupCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CareGroupCode ", fCareGroupCode, value)
        End Set
    End Property

    Dim fCareGroupName As String
    Public Property CareGroupName() As String
        Get
            Return fCareGroupName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CareGroupName ", fCareGroupName, value)
        End Set
    End Property

    Dim fRadicatedConsecutive As Integer?
    Public Property RadicatedConsecutive() As Integer?
        Get
            Return fRadicatedConsecutive
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("RadicatedConsecutive", fRadicatedConsecutive, value)
        End Set
    End Property

    <PersistentAlias("concat(ThirdPartyNit,' - ',ThirdPartyName)")>
    Public ReadOnly Property ThirdPartyNitName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("ThirdPartyNitName"))
        End Get
    End Property

    <PersistentAlias("concat(PatientCode,' - ',PatientName)")>
    Public ReadOnly Property PatientCodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("PatientCodeName"))
        End Get
    End Property

    <PersistentAlias("concat(CareGroupCode,' - ',CareGroupName)")>
    Public ReadOnly Property CareGroupCodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CareGroupCodeName"))
        End Get
    End Property

    <PersistentAlias("Iif(EntityName='Invoice','Factura', 'Factura Capita Final')")>
    Public ReadOnly Property EntityNameDescription() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("EntityNameDescription"))
        End Get
    End Property
#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

#End Region

End Class
