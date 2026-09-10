#Region "Imports"

Imports DevExpress.Xpo

#End Region

<Persistent("Contract.DefinitionRateDetail")>
Public Class DefinitionRateDetailXpo
    Inherits XPLiteObject

#Region "Properties"

    Dim fId As Integer
    <Key(True)>
    <Persistent("Id")>
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property

    Dim fAllowValueChange As Boolean
    <Persistent("AllowValueChange")>
    Public Property AllowValueChange() As Boolean
        Get
            Return fAllowValueChange
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("AllowValueChange", fAllowValueChange, value)
        End Set
    End Property

#End Region

#Region "Navigation Properties"

    <Association("Billing_ServiceOrderDetailReferencesContract_DefinitionRateDetail", GetType(ServiceOrderDetailXpo))>
    Public ReadOnly Property Billing_ServiceOrderDetails() As XPCollection(Of ServiceOrderDetailXpo)
        Get
            Return GetCollection(Of ServiceOrderDetailXpo)("Billing_ServiceOrderDetails")
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
