Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

'<Indices("Nit;State")> _
<Persistent("Common.Customer")> _
Public Class GlosasCustomerXpo
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
    Dim fNit As String
    '<Indexed(Name:="IX_Customer_Nit", Unique:=True)> _
    <Size(15)> _
    Public Property Nit() As String
        Get
            Return fNit.Trim
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
    Dim fThirdPartyId As Integer
    Public Property ThirdPartyId() As Integer
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ThirdPartyId", fThirdPartyId, value)
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
    <Association("CustomerObjectionC", GetType(GlosasObjectionCXpo))> _
    Public ReadOnly Property Glosas_GlosaObjectionsReceptionCs() As XPCollection(Of GlosasObjectionCXpo)
        Get
            Return GetCollection(Of GlosasObjectionCXpo)("Glosas_GlosaObjectionsReceptionCs")
        End Get
    End Property

    <Association("CustomerGlosas_RadicateInvoiceCXpo", GetType(Glosas_RadicateInvoiceCXpo))> _
    Public ReadOnly Property CustomerGlosas_RadicateInvoiceCXpos() As XPCollection(Of Glosas_RadicateInvoiceCXpo)
        Get
            Return GetCollection(Of Glosas_RadicateInvoiceCXpo)("CustomerGlosas_RadicateInvoiceCXpos")
        End Get
    End Property

    <Association("Glosas_GlosaDevolutionsReceptionCReferencesCommon_Customer", GetType(GlosaDevolutionsReceptionCXpo))> _
    Public ReadOnly Property Glosas_GlosaDevolutionsReceptionCs() As XPCollection(Of GlosaDevolutionsReceptionCXpo)
        Get
            Return GetCollection(Of GlosaDevolutionsReceptionCXpo)("Glosas_GlosaDevolutionsReceptionCs")
        End Get
    End Property

    <Association("Glosas_TransferJuridical_Customer", GetType(Glosas_TransferJuridicalDebtCollectionC))> _
    Public ReadOnly Property Glosas_GlosaTransferJuridicalCs() As XPCollection(Of Glosas_TransferJuridicalDebtCollectionC)
        Get
            Return GetCollection(Of Glosas_TransferJuridicalDebtCollectionC)("Glosas_GlosaTransferJuridicalCs")
        End Get
    End Property

    <Association("Glosas_PartialPaymentsCXpoReferencesCommon_Customer", GetType(Glosas_PartialPaymentsCXpo))> _
    Public ReadOnly Property Glosas_PartialPaymentsCXpo() As XPCollection(Of Glosas_PartialPaymentsCXpo)
        Get
            Return GetCollection(Of Glosas_PartialPaymentsCXpo)("Glosas_PartialPaymentsCXpo")
        End Get
    End Property

    <Size(100)> _
    <PersistentAlias("concat(concat(Nit,' - '),Name)")>
    Public ReadOnly Property NitName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("NitName"))
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class