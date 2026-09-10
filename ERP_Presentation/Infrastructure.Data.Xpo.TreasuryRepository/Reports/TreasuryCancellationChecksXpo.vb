Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Treasury.CancellationChecks")> _
    Public Class TreasuryCancellationChecksXpo
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
    Dim fIdEntityAccount As TreasuryEntityBankAccountsXpo
    <Association("TreasuryCancellationChecksXpoReferencesTreasuryEntityBankAccountsXpo")> _
    Public Property IdEntityAccount() As TreasuryEntityBankAccountsXpo
        Get
            Return fIdEntityAccount
        End Get
        Set(ByVal value As TreasuryEntityBankAccountsXpo)
            SetPropertyValue(Of TreasuryEntityBankAccountsXpo)("IdEntityAccount", fIdEntityAccount, value)
        End Set
    End Property
    Dim fIdCheckBook As Integer
    Public Property IdCheckBook() As Integer
        Get
            Return fIdCheckBook
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdCheckBook", fIdCheckBook, value)
        End Set
    End Property
    Dim fCancellationDate As DateTime
    Public Property CancellationDate() As DateTime
        Get
            Return fCancellationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("CancellationDate", fCancellationDate, value)
        End Set
    End Property
    Dim fCheckNumber As String
    Public Property CheckNumber() As String
        Get
            Return fCheckNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CheckNumber", fCheckNumber, value)
        End Set
    End Property
    Dim fDescription As String
    <Size(SizeAttribute.Unlimited)> _
    Public Property Description() As String
        Get
            Return fDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Description", fDescription, value)
        End Set
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
    'Propiedad Añadida
    Dim fSeleccionado As Boolean = False
    <NonPersistent()> _
    Public Property Seleccionado() As Boolean
        Get
            Return fSeleccionado
        End Get
        Set(ByVal value As Boolean)
            Me.fSeleccionado = value
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
