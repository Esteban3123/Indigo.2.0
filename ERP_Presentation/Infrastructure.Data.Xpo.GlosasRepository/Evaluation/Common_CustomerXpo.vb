Imports DevExpress.Xpo

<Persistent("Common.Customer")> _
Partial Public Class Common_CustomerXpo
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

    Dim fEPSCode As String
    Public Property EPSCode() As String
        Get
            Return fEPSCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("EPSCode", fEPSCode, value)
        End Set
    End Property

    Dim fThirdPartyId As CommonThirdPartyXpo
    <Association("Common_Customer_References_Common_ThirdParty")>
    Public Property ThirdPartyId() As CommonThirdPartyXpo
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As CommonThirdPartyXpo)
            SetPropertyValue(Of CommonThirdPartyXpo)("ThirdPartyId", fThirdPartyId, value)
        End Set
    End Property

    Dim fState As Boolean
    Public Property State() As Boolean
        Get
            Return fState
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("State", fState, value)
        End Set
    End Property

#End Region

#Region "Navigators"

    <Association("Glosas_GlosaObjectionsReceptionCReferencesCommon_Customer", GetType(Glosas_GlosaObjectionsReceptionC))>
    Public ReadOnly Property Glosas_GlosaObjectionsReceptionC() As XPCollection(Of Glosas_GlosaObjectionsReceptionC)
        Get
            Return GetCollection(Of Glosas_GlosaObjectionsReceptionC)("Glosas_GlosaObjectionsReceptionC")
        End Get
    End Property

    <Association("Glosas_RadicateInvoiceCReferencesCommon_Customer", GetType(Glosas_RadicateInvoiceC))>
    Public ReadOnly Property Glosas_RadicateInvoiceC() As XPCollection(Of Glosas_RadicateInvoiceC)
        Get
            Return GetCollection(Of Glosas_RadicateInvoiceC)("Glosas_RadicateInvoiceC")
        End Get
    End Property

    <Association("GlosasTransferJuridicalDebtCollectionCXpoReferencesCommon_CustomerXpo", GetType(GlosasTransferJuridicalDebtCollectionCXpo))>
    Public ReadOnly Property GlosasTransferJuridicalDebtCollectionCXpo() As XPCollection(Of GlosasTransferJuridicalDebtCollectionCXpo)
        Get
            Return GetCollection(Of GlosasTransferJuridicalDebtCollectionCXpo)("GlosasTransferJuridicalDebtCollectionCXpo")
        End Get
    End Property

#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

#End Region

End Class