
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Common.Customer")> _
Public Class CustomerXpo
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

    Dim fNit As String
    Public Property Nit() As String
        Get
            Return fNit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Nit", fNit, value)
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

    <PersistentAlias("concat(concat(Nit,' - '),Name)")>
    Public ReadOnly Property NitName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("NitName"))
        End Get
    End Property

    Dim fThirdPartyId As ThirdsPartyXpo
    <Association("Customer_References_ThirdParty")>
    Public Property ThirdPartyId() As ThirdsPartyXpo
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As ThirdsPartyXpo)
            SetPropertyValue(Of ThirdsPartyXpo)("ThirdPartyId", fThirdPartyId, value)
        End Set
    End Property

    Dim fTerm As Integer
    Public Property Term() As Integer
        Get
            Return fTerm
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Term", fTerm, value)
        End Set
    End Property

#End Region

#Region "Navigation"

    <Association("BasicBilling_References_Customer", GetType(BasicBillingXpo))>
    Public ReadOnly Property BasicBillings() As XPCollection(Of BasicBillingXpo)
        Get
            Return GetCollection(Of BasicBillingXpo)("BasicBillings")
        End Get
    End Property

    <Association("BasicBillingReport_References_Customer", GetType(BasicBillingReportXpo))>
    Public ReadOnly Property BasicBillingsReport() As XPCollection(Of BasicBillingReportXpo)
        Get
            Return GetCollection(Of BasicBillingReportXpo)("BasicBillingsReport")
        End Get
    End Property

#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

#End Region

End Class