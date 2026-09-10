Imports DevExpress.Xpo

<Persistent("Cost.ViewListProvisionDocument")>
Public Class ViewListProvisionDocumentXpo
    Inherits XPLiteObject

#Region "Properties"

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

    Dim fCode As String
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property

    Dim fGeneralExpenseId As Integer
    Public Property GeneralExpenseId() As Integer
        Get
            Return fGeneralExpenseId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("GeneralExpenseId", fGeneralExpenseId, value)
        End Set
    End Property

    Dim fSupplierId As Integer
    Public Property SupplierId() As Integer
        Get
            Return fSupplierId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("SupplierId", fSupplierId, value)
        End Set
    End Property

    Dim fSupplierCodeName As String
    Public Property SupplierCodeName() As String
        Get
            Return fSupplierCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SupplierCodeName", fSupplierCodeName, value)
        End Set
    End Property

    Dim fSuppliersDistributionLinesId As Integer
    Public Property SuppliersDistributionLinesId() As Integer
        Get
            Return fSuppliersDistributionLinesId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("SuppliersDistributionLinesId", fSuppliersDistributionLinesId, value)
        End Set
    End Property

    Dim fDistributionLineCodeName As String
    Public Property DistributionLineCodeName() As String
        Get
            Return fDistributionLineCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DistributionLineCodeName", fDistributionLineCodeName, value)
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

    Dim fStatus As Integer
    Public Property Status() As Integer
        Get
            Return fStatus
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Status", fStatus, value)
        End Set
    End Property

    Dim fStatusName As String
    Public Property StatusName() As String
        Get
            Return fStatusName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("StatusName", fStatusName, value)
        End Set
    End Property

    Dim fConfirmUser As String
    Public Property ConfirmUser() As String
        Get
            Return fConfirmUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ConfirmUser", fConfirmUser, value)
        End Set
    End Property

    Dim fConfirmDate As DateTime
    Public Property ConfirmDate() As DateTime
        Get
            Return fConfirmDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ConfirmDate", fConfirmDate, value)
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
