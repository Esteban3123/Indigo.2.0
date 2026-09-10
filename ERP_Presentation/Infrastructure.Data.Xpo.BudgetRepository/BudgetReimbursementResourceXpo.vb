Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Budget.ReimbursementResource")> _
Public Class BudgetReimbursementResourceXpo
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
    Dim fBudgetaryValidityId As BudgetValidityXpo
    <Association("BudgetReimbursementResourceXpoReferencesBudgetValidityXpo")> _
    Public Property BudgetaryValidityId() As BudgetValidityXpo
        Get
            Return fBudgetaryValidityId
        End Get
        Set(ByVal value As BudgetValidityXpo)
            SetPropertyValue(Of BudgetValidityXpo)("BudgetaryValidityId", fBudgetaryValidityId, value)
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
    Dim fPaymentOrderId As BudgetPaymentOrderXpo
    <Association("BudgetReimbursementResourceXpoReferencesBudgetPaymentOrderXpo")> _
    Public Property PaymentOrderId() As BudgetPaymentOrderXpo
        Get
            Return fPaymentOrderId
        End Get
        Set(ByVal value As BudgetPaymentOrderXpo)
            SetPropertyValue(Of BudgetPaymentOrderXpo)("PaymentOrderId", fPaymentOrderId, value)
        End Set
    End Property
    Dim fUpTo As Byte
    Public Property UpTo() As Byte
        Get
            Return fUpTo
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("UpTo", fUpTo, value)
        End Set
    End Property
    <PersistentAlias("Iif(UpTo = 1, 'Obligación', Iif(UpTo = 2, 'Compromiso/Reserva',Iif(UpTo = 3, 'Disponibilidad',Iif(UpTo = 4, 'Presupuesto', ''))))")>
    Public ReadOnly Property UpToName As String
        Get
            'Select Case fUpTo
            '    Case 1
            '        Return "Obligación"
            '    Case 2
            '        Return "Compromiso/Reserva"
            '    Case 3
            '        Return "Disponibilidad"
            '    Case 4
            '        Return "Presupuesto"
            '    Case Else
            '        Return String.Empty
            'End Select
            Return Convert.ToString(Me.EvaluateAlias("UpToName"))
        End Get
    End Property
    Dim fDocument As String
    Public Property Document() As String
        Get
            Return fDocument
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Document", fDocument, value)
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
    Dim fCreationUser As String
    <Size(20)> _
    Public Property CreationUser() As String
        Get
            Return fCreationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CreationUser", fCreationUser, value)
        End Set
    End Property
    Dim fCreationDate As DateTime
    Public Property CreationDate() As DateTime
        Get
            Return fCreationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("CreationDate", fCreationDate, value)
        End Set
    End Property
    Dim fModificationUser As String
    <Size(20)> _
    Public Property ModificationUser() As String
        Get
            Return fModificationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ModificationUser", fModificationUser, value)
        End Set
    End Property
    Dim fModificationDate As DateTime
    Public Property ModificationDate() As DateTime
        Get
            Return fModificationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ModificationDate", fModificationDate, value)
        End Set
    End Property
    Dim fConfirmationUser As String
    <Size(20)> _
    Public Property ConfirmationUser() As String
        Get
            Return fConfirmationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ConfirmationUser", fConfirmationUser, value)
        End Set
    End Property
    Dim fConfirmationDate As DateTime
    Public Property ConfirmationDate() As DateTime
        Get
            Return fConfirmationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ConfirmationDate", fConfirmationDate, value)
        End Set
    End Property
    Dim fAnnulmentUser As String
    <Size(20)> _
    Public Property AnnulmentUser() As String
        Get
            Return fAnnulmentUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AnnulmentUser", fAnnulmentUser, value)
        End Set
    End Property
    Dim fAnnulmentDate As DateTime
    Public Property AnnulmentDate() As DateTime
        Get
            Return fAnnulmentDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("AnnulmentDate", fAnnulmentDate, value)
        End Set
    End Property
    <Association("BudgetReimbursementResourceDetailXpoReferencesBudgetReimbursementResourceXpo", GetType(BudgetReimbursementResourceDetailXpo))> _
    Public ReadOnly Property BudgetReimbursementResourceDetailXpo() As XPCollection(Of BudgetReimbursementResourceDetailXpo)
        Get
            Return GetCollection(Of BudgetReimbursementResourceDetailXpo)("BudgetReimbursementResourceDetailXpo")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
