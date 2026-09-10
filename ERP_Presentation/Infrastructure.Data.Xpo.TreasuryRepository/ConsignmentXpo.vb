'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.TreasuryRepository
' Author           : Diego Andrés Roldán Lozano
' Created          : 09-10-2014
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports System
Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Resources

#End Region

<Persistent("Treasury.Consignment")> _
Public Class ConsignmentXpo
    Inherits XPLiteObject

#Region "Members"

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
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
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

    Dim fEntityBankAccountId As EntityBankAccountXpo
    <Association("Treasury_ConsignmentReferencesTreasury_EntityBankAccount")>
    Public Property EntityBankAccountId() As EntityBankAccountXpo
        Get
            Return fEntityBankAccountId
        End Get
        Set(value As EntityBankAccountXpo)
            SetPropertyValue(Of EntityBankAccountXpo)("EntityBankAccountId", fEntityBankAccountId, value)
        End Set
    End Property

    Dim fMainAccountId As Integer
    Public Property MainAccountId() As Integer
        Get
            Return fMainAccountId
        End Get
        Set(value As Integer)
            SetPropertyValue(Of Integer)("MainAccountId", fMainAccountId, value)
        End Set
    End Property

    Dim fDescription As String
    Public Property Description() As String
        Get
            Return fDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Description", fDescription, value)
        End Set
    End Property

    Dim fValue As Decimal
    Public Property Value() As Decimal
        Get
            Return fValue
        End Get
        Set(value As Decimal)
            SetPropertyValue(Of Decimal)("Value", fValue, value)
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

    <PersistentAlias("Iif(Status = 1, 'Sin Confirmar', Status = 2, 'Confirmado', Status = 3, 'Anulado', Status = 4, 'Reversado', '')")>
    Public ReadOnly Property StatusName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
        End Get
    End Property

    <PersistentAlias("concat(Code,' - ',Description)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property

#End Region

#Region "Navigations"

    <Association("Treasury_TreasuryNoteReferencesTreasury_Consignment", GetType(TreasuryNoteXpo))>
    Public ReadOnly Property Treasury_TreasuryNotes() As XPCollection(Of TreasuryNoteXpo)
        Get
            Return GetCollection(Of TreasuryNoteXpo)("Treasury_TreasuryNotes")
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
