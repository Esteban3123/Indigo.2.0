Imports DevExpress.Xpo

<Persistent("Payroll.ViewElectronicPayrollInformation")>
Public Class ElectronicPayrollInformation
    Inherits XPLiteObject

#Region "Members"

    Dim fId As String
    <Key(True)>
    Public Property Id() As String
        Get
            Return fId
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Id", fId, value)
        End Set
    End Property

    Dim fElectronicPayrollId As Integer
    Public Property ElectronicPayrollId() As Integer
        Get
            Return fElectronicPayrollId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ElectronicPayrollId", fElectronicPayrollId, value)
        End Set
    End Property

    Dim fYear As Integer
    Public Property Year() As Integer
        Get
            Return fYear
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Year", fYear, value)
        End Set
    End Property

    Dim fMonth As Byte
    Public Property Month() As Byte
        Get
            Return fMonth
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Month", fMonth, value)
        End Set
    End Property

    Dim fEmployeePartyNit As String
    Public Property EmployeePartyNit() As String
        Get
            Return fEmployeePartyNit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("EmployeePartyNit", fEmployeePartyNit, value)
        End Set
    End Property

    Dim fEmployeePartyName As String
    Public Property EmployeePartyName() As String
        Get
            Return fEmployeePartyName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("EmployeePartyName", fEmployeePartyName, value)
        End Set
    End Property

    Dim fNature As Byte
    Public Property Nature() As Byte
        Get
            Return fNature
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Nature", fNature, value)
        End Set
    End Property

    Dim fNatureDescription As String
    Public Property NatureDescription() As String
        Get
            Return fNatureDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NatureDescription", fNatureDescription, value)
        End Set
    End Property

    Dim fDetail As String
    Public Property Detail() As String
        Get
            Return fDetail
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Detail", fDetail, value)
        End Set
    End Property

    Dim fDateStart As Nullable(Of DateTime)
    Public Property DateStart() As Nullable(Of DateTime)
        Get
            Return fDateStart
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            SetPropertyValue(Of Nullable(Of DateTime))("DateStart", fDateStart, value)
        End Set
    End Property

    Dim fDateEnd As Nullable(Of DateTime)
    Public Property DateEnd() As Nullable(Of DateTime)
        Get
            Return fDateEnd
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            SetPropertyValue(Of Nullable(Of DateTime))("DateEnd", fDateEnd, value)
        End Set
    End Property

    Dim fQuantity As Nullable(Of Integer)
    Public Property Quantity() As Nullable(Of Integer)
        Get
            Return fQuantity
        End Get
        Set(ByVal value As Nullable(Of Integer))
            SetPropertyValue(Of Nullable(Of Integer))("Quantity", fQuantity, value)
        End Set
    End Property

    Dim fPercentage As Decimal
    Public Property Percentage() As Decimal
        Get
            Return fPercentage
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Percentage", fPercentage, value)
        End Set
    End Property

    Dim fValue As Decimal
    Public Property Value() As Decimal
        Get
            Return fValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Value", fValue, value)
        End Set
    End Property

    Dim fCUNE As String
    Public Property CUNE() As String
        Get
            Return fCUNE
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CUNE", fCUNE, value)
        End Set
    End Property

#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

#End Region

End Class