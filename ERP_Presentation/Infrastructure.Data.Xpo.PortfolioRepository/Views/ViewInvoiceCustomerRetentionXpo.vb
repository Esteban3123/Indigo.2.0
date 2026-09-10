Imports DevExpress.Xpo

<Persistent("Portfolio.ViewInvoiceCustomerRetention")>
Public Class ViewInvoiceCustomerRetentionXpo
    Inherits XPLiteObject

#Region "Properties"

    Dim fId As String
    <Key>
    Public Property Id() As String
        Get
            Return fId
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Id", fId, value)
        End Set
    End Property

    Dim fInvoiceNumber As String
    Public Property InvoiceNumber() As String
        Get
            Return fInvoiceNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("InvoiceNumber", fInvoiceNumber, value)
        End Set
    End Property

    Dim fRetentionName As String
    Public Property RetentionName() As String
        Get
            Return fRetentionName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RetentionName", fRetentionName, value)
        End Set
    End Property

    Dim fRetentionRate As Decimal
    Public Property RetentionRate() As Decimal
        Get
            Return fRetentionRate
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("RetentionRate", fRetentionRate, value)
        End Set
    End Property

    Dim fBaseValue As Decimal
    Public Property BaseValue() As Decimal
        Get
            Return fBaseValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("BaseValue", fBaseValue, value)
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
