Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Budget.ViewListAnnualizedCashFlow")> _
Public Class VListAnnualizedCashFlowXpo
    Inherits XPLiteObject
    
    Dim fCode As String
    <Key(True)> _
    <Size(20)> _
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
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
    Dim fFinancialSource As String
    <Size(125)> _
    Public Property FinancialSource() As String
        Get
            Return fFinancialSource
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FinancialSource", fFinancialSource, value)
        End Set
    End Property
    Dim fItemType As Integer
    Public Property ItemType() As Integer
        Get
            Return fItemType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ItemType", fItemType, value)
        End Set
    End Property
    Dim fBudgetaryValidityId As Integer
    Public Property BudgetaryValidityId() As Integer
        Get
            Return fBudgetaryValidityId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("BudgetaryValidityId", fBudgetaryValidityId, value)
        End Set
    End Property
    Dim fBudgetValue As Decimal
    Public Property BudgetValue() As Decimal
        Get
            Return fBudgetValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("BudgetValue", fBudgetValue, value)
        End Set
    End Property
    Dim fPACValue As String
    Public Property PACValue() As String
        Get
            Return fPACValue
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PACValue", fPACValue, value)
        End Set
    End Property
    Dim fStatus As String
    Public Property Status() As String
        Get
            Return fStatus
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Status", fStatus, value)
        End Set
    End Property

    <PersistentAlias("Iif(Status = 0, 'Sin Registrar', Iif(Status = 1, 'Registrado',Iif(Status = 2, 'Confirmado',Iif(Status = 3, 'Anulado', ''))))")>
    Public ReadOnly Property StatusText As String
        Get
            'Select Case fStatus
            '    Case 0
            '        Return "Sin Registrar"
            '    Case 1
            '        Return "Registrado"
            '    Case 2
            '        Return "Confirmado"
            '    Case 3
            '        Return "Anulado"
            '    Case Else
            '        Return String.Empty
            'End Select
            Return Convert.ToString(Me.EvaluateAlias("StatusText"))
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
