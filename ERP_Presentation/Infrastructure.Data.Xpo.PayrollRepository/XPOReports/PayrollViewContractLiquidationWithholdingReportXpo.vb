Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Payroll.ViewReportContractLiquidationWithholding")> _
Public Class PayrollViewContractLiquidationWithholdingReportXpo
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

    Dim fContractLiquidationId As Integer
    Public Property ContractLiquidationId() As Integer
        Get
            Return fContractLiquidationId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ContractLiquidationId", fContractLiquidationId, value)
        End Set
    End Property

    Dim fEmployeeId As Integer
    Public Property EmployeeId() As Integer
        Get
            Return fEmployeeId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("EmployeeId", fEmployeeId, value)
        End Set
    End Property

    Dim fIdentification As String
    <Size(15)> _
    Public Property Identification() As String
        Get
            Return fIdentification
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Identification", fIdentification, value)
        End Set
    End Property

    Dim fName As String
    <Size(300)> _
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
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

    Dim fLiquidationDate As DateTime
    Public Property LiquidationDate() As DateTime
        Get
            Return fLiquidationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("LiquidationDate", fLiquidationDate, value)
        End Set
    End Property

    Dim fGroupId As Integer
    Public Property GroupId() As Integer
        Get
            Return fGroupId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("GroupId", fGroupId, value)
        End Set
    End Property

    Dim fGroupCode As String
    <Size(20)> _
    Public Property GroupCode() As String
        Get
            Return fGroupCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("GroupCode", fGroupCode, value)
        End Set
    End Property

    Dim fGroupName As String
    <Size(150)> _
    Public Property GroupName() As String
        Get
            Return fGroupName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("GroupName", fGroupName, value)
        End Set
    End Property

    Dim fTotalAccrued As Decimal
    Public Property TotalAccrued() As Decimal
        Get
            Return fTotalAccrued
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalAccrued", fTotalAccrued, value)
        End Set
    End Property

    Dim fTotalAccruedRTF As Decimal
    Public Property TotalAccruedRTF() As Decimal
        Get
            Return fTotalAccruedRTF
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalAccruedRTF", fTotalAccruedRTF, value)
        End Set
    End Property

    Dim fPensionContribution As Decimal
    Public Property PensionContribution() As Decimal
        Get
            Return fPensionContribution
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PensionContribution", fPensionContribution, value)
        End Set
    End Property

    Dim fVoluntaryPensionContribution As Decimal
    Public Property VoluntaryPensionContribution() As Decimal
        Get
            Return fVoluntaryPensionContribution
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("VoluntaryPensionContribution", fVoluntaryPensionContribution, value)
        End Set
    End Property

    Dim fSolidarityFund As Decimal
    Public Property SolidarityFund() As Decimal
        Get
            Return fSolidarityFund
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("SolidarityFund", fSolidarityFund, value)
        End Set
    End Property

    Dim fAFC As Decimal
    Public Property AFC() As Decimal
        Get
            Return fAFC
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("AFC", fAFC, value)
        End Set
    End Property

    Dim fHealthContribution As Decimal
    Public Property HealthContribution() As Decimal
        Get
            Return fHealthContribution
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("HealthContribution", fHealthContribution, value)
        End Set
    End Property

    Dim fPrepaidMedicine As Decimal
    Public Property PrepaidMedicine() As Decimal
        Get
            Return fPrepaidMedicine
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PrepaidMedicine", fPrepaidMedicine, value)
        End Set
    End Property

    Dim fDependentDeduction As Decimal
    Public Property DependentDeduction() As Decimal
        Get
            Return fDependentDeduction
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("DependentDeduction", fDependentDeduction, value)
        End Set
    End Property

    Dim fHousingDeduction As Decimal
    Public Property HousingDeduction() As Decimal
        Get
            Return fHousingDeduction
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("HousingDeduction", fHousingDeduction, value)
        End Set
    End Property

    Dim fTaxableBase As Integer
    Public Property TaxableBase() As Integer
        Get
            Return fTaxableBase
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("TaxableBase", fTaxableBase, value)
        End Set
    End Property

    Dim fTotalWithholding As Decimal
    Public Property TotalWithholding() As Decimal
        Get
            Return fTotalWithholding
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalWithholding", fTotalWithholding, value)
        End Set
    End Property

    Dim fArticle As Byte
    Public Property Article() As Byte
        Get
            Return fArticle
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Article", fArticle, value)
        End Set
    End Property

    Dim fDeclarantType As Byte
    Public Property DeclarantType() As Byte
        Get
            Return fDeclarantType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("DeclarantType", fDeclarantType, value)
        End Set
    End Property

    Dim fBranchOfficeId As Integer
    Public Property BranchOfficeId() As Long
        Get
            Return fBranchOfficeId
        End Get
        Set(ByVal value As Long)
            SetPropertyValue(Of Long)("BranchOfficeId", fBranchOfficeId, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
