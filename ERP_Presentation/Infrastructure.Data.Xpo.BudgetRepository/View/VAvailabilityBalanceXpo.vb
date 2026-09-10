Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Budget.ViewAvailabilityBalance")> _
Public Class VAvailabilityBalanceXpo
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
    Dim fCode As String
    <Size(20)> _
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
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
    Dim fDependencyId As Integer
    Public Property DependencyId() As Integer
        Get
            Return fDependencyId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("DependencyId", fDependencyId, value)
        End Set
    End Property
    Dim fExpirationDate As DateTime
    Public Property ExpirationDate() As DateTime
        Get
            Return fExpirationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ExpirationDate", fExpirationDate, value)
        End Set
    End Property
    Dim fExpirationDays As Integer
    Public Property ExpirationDays() As Integer
        Get
            Return fExpirationDays
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ExpirationDays", fExpirationDays, value)
        End Set
    End Property
    Dim fDocumentDate As DateTime
    Public Property DocumentDate() As DateTime
        Get
            Return fDocumentDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("DocumentDate", fDocumentDate, value)
        End Set
    End Property
    Dim fAvailabilityType As Byte
    Public Property AvailabilityType() As Byte
        Get
            Return fAvailabilityType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("AvailabilityType", fAvailabilityType, value)
        End Set
    End Property
    Dim fObservations As String
    <Size(300)> _
    Public Property Observations() As String
        Get
            Return fObservations
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Observations", fObservations, value)
        End Set
    End Property
    Dim fStatus As Byte
    Public Property Status() As Byte
        Get
            Return fStatus
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Status", fStatus, value)
        End Set
    End Property
    Dim fBalanceAvaliability As Decimal
    Public Property BalanceAvaliability() As Decimal
        Get
            Return fBalanceAvaliability
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("BalanceAvaliability", fBalanceAvaliability, value)
        End Set
    End Property
    <PersistentAlias("Iif(Status = 1, 'Registrado', Iif(Status = 2, 'Confirmado',Iif(Status = 3, 'Anulado', '')))")>
    Public ReadOnly Property StatusName As String
        Get
            'Select Case fStatus
            '    Case 1
            '        Return "Registrado"
            '    Case 2
            '        Return "Confirmado"
            '    Case 3
            '        Return "Anulado"
            '    Case Else
            '        Return String.Empty
            'End Select
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
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
