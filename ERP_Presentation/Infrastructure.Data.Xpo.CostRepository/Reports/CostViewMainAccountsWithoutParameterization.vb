Imports DevExpress.Xpo

<Persistent("Cost.ViewMainAccountsWithoutParameterization")>
Public Class CostViewMainAccountsWithoutParameterization
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

    Dim fYear As Integer
    Public Property Year() As Integer
        Get
            Return fYear
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Year", fYear, value)
        End Set
    End Property

    Dim fMonth As Integer

    Public Property Month() As Integer
        Get
            Return fMonth
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Month", fMonth, value)
        End Set
    End Property

    Dim fParentAccountNumber As String

    Public Property ParentAccountNumber() As String
        Get
            Return fParentAccountNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ParentAccountNumber", fParentAccountNumber, value)
        End Set
    End Property

    Dim fParentAccountName As String

    Public Property ParentAccountName() As String
        Get
            Return fParentAccountName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ParentAccountName", fParentAccountName, value)
        End Set
    End Property

    Dim fMainAccountNumber As String

    Public Property MainAccountNumber() As String
        Get
            Return fMainAccountNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("MainAccountNumber", fMainAccountNumber, value)
        End Set
    End Property

    Dim fMainAccountName As String

    Public Property MainAccountName() As String
        Get
            Return fMainAccountName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("MainAccountName", fMainAccountName, value)
        End Set
    End Property

    Dim fCostCenterCode As String

    Public Property CostCenterCode() As String
        Get
            Return fCostCenterCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CostCenterCode", fCostCenterCode, value)
        End Set
    End Property

    Dim fCostCenterName As String
    Public Property CostCenterName() As String
        Get
            Return fCostCenterName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CostCenterName", fCostCenterName, value)
        End Set
    End Property

    Dim fThirdPartyNit As String

    Public Property ThirdPartyNit() As String
        Get
            Return fThirdPartyNit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ThirdPartyNit", fThirdPartyNit, value)
        End Set
    End Property

    Dim fThirdPartyName As String
    Public Property ThirdPartyName() As String
        Get
            Return fThirdPartyName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ThirdPartyName", fThirdPartyName, value)
        End Set
    End Property

    Dim fBalance As Decimal
    Public Property Balance() As Decimal
        Get
            Return fBalance
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Balance", fBalance, value)
        End Set
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
