'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.PaymentsRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 09/01/2015
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports System
Imports DevExpress.Xpo

#End Region


<Persistent("Payments.FilingUnit")> _
Public Class FilingUnitXpo
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

    Dim fName As String
    <Size(60)> _
    <Persistent("Name")> _
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
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

    Dim fStatus As Boolean
    <Persistent("Status")> _
    Public Property Status() As Boolean
        Get
            Return fStatus
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Status", fStatus, value)
        End Set
    End Property

    'Dim fParentId As Integer
    '<Persistent("ParentId")> _
    'Public Property ParentId() As Integer
    '    Get
    '        Return fParentId
    '    End Get
    '    Set(ByVal value As Integer)
    '        SetPropertyValue(Of Integer)("ParentId", fParentId, value)
    '    End Set
    'End Property

    Dim fParentId As FilingUnitXpo
    <Association("FilingUnitReferencesFilingUnit")> _
    Public Property ParentId() As FilingUnitXpo
        Get
            Return fParentId
        End Get
        Set(ByVal value As FilingUnitXpo)
            SetPropertyValue(Of FilingUnitXpo)("ParentId", fParentId, value)
        End Set
    End Property

    <PersistentAlias("ParentId")> _
    Public ReadOnly Property PadreId As Integer
        Get
            If ParentId Is Nothing Then
                Return 0
            Else
                Return ParentId.Id
            End If
        End Get
    End Property

    <Association("FilingUnitReferencesFilingUnit", GetType(FilingUnitXpo))> _
    Public ReadOnly Property FilingUnitXpo() As XPCollection(Of FilingUnitXpo)
        Get
            Return GetCollection(Of FilingUnitXpo)("FilingUnitXpo")
        End Get
    End Property

    'columna que devuelve el codigo y el nombre del tipo de proveedor
    <Size(50)> _
    <PersistentAlias("concat(concat(Code,' - '),Name)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property


    <Association("FilingUnitUserReferencesFilingUnit", GetType(FilingUnitUserXpo))> _
    Public ReadOnly Property FilingUnitUserXpo() As XPCollection(Of FilingUnitUserXpo)
        Get
            Return GetCollection(Of FilingUnitUserXpo)("FilingUnitUserXpo")
        End Get
    End Property

    <Association("FilingUnitSourceReferencesFilingUnit", GetType(AccountPayableTransferXpo))> _
    Public ReadOnly Property AccountPayableTransferSourceXpo() As XPCollection(Of AccountPayableTransferXpo)
        Get
            Return GetCollection(Of AccountPayableTransferXpo)("AccountPayableTransferSourceXpo")
        End Get
    End Property

    <Association("FilingUnitTargetReferencesFilingUnit", GetType(AccountPayableTransferXpo))> _
    Public ReadOnly Property AccountPayableTransferTargetXpo() As XPCollection(Of AccountPayableTransferXpo)
        Get
            Return GetCollection(Of AccountPayableTransferXpo)("AccountPayableTransferTargetXpo")
        End Get
    End Property

    <Association("FilingUnitId_Accountpayable", GetType(AccountPayableXpo))> _
    Public ReadOnly Property AccountPayableXpo() As XPCollection(Of AccountPayableXpo)
        Get
            Return GetCollection(Of AccountPayableXpo)("AccountPayableXpo")
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
