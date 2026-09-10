Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("MedicalFees.MedicalFeesLiquidation")> _
Public Class MedicalFeesLiquidationReportXpo
    Inherits XPLiteObject
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
    <Size(20)> _
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
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
    Dim fMedicalFeesContractId As Integer
    Public Property MedicalFeesContractId() As Integer
        Get
            Return fMedicalFeesContractId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("MedicalFeesContractId", fMedicalFeesContractId, value)
        End Set
    End Property
    Dim fHealthProfessionalCode As String
    <Size(20)> _
    Public Property HealthProfessionalCode() As String
        Get
            Return fHealthProfessionalCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("HealthProfessionalCode", fHealthProfessionalCode, value)
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
    Dim fBillNumber As String
    <Size(20)> _
    Public Property BillNumber() As String
        Get
            Return fBillNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BillNumber", fBillNumber, value)
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
    Dim fFilingUnitId As Integer
    Public Property FilingUnitId() As Integer
        Get
            Return fFilingUnitId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("FilingUnitId", fFilingUnitId, value)
        End Set
    End Property
    Dim fSupplierId As CommonSupplierReportXpo
    Public Property SupplierId() As CommonSupplierReportXpo
        Get
            Return fSupplierId
        End Get
        Set(ByVal value As CommonSupplierReportXpo)
            SetPropertyValue(Of CommonSupplierReportXpo)("SupplierId", fSupplierId, value)
        End Set
    End Property
    Dim fSupplierTypeId As Integer
    Public Property SupplierTypeId() As Integer
        Get
            Return fSupplierTypeId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("SupplierTypeId", fSupplierTypeId, value)
        End Set
    End Property
    Dim fAccountPayableId As Integer
    Public Property AccountPayableId() As Integer
        Get
            Return fAccountPayableId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AccountPayableId", fAccountPayableId, value)
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
    <Size(20)> _
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
    <Size(20)> _
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
    <Size(20)> _
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
    <Size(20)> _
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
    <Association("MedicalFees_MedicalFeesLiquidationDetailReferencesMedicalFees_MedicalFeesLiquidation", GetType(MedicalFeesLiquidationDetailReportXpo))> _
    Public ReadOnly Property MedicalFees_MedicalFeesLiquidationDetails() As XPCollection(Of MedicalFeesLiquidationDetailReportXpo)
        Get
            Return GetCollection(Of MedicalFeesLiquidationDetailReportXpo)("MedicalFees_MedicalFeesLiquidationDetails")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
