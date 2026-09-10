'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.PaymentsRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 11/03/2015
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports System
Imports DevExpress.Xpo

#End Region


<Persistent("Payments.AccountPayableTransfer")> _
Public Class AccountPayableTransferXpo
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
    <Size(20)> _
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property
    Dim fTranferType As Byte
    Public Property TranferType() As Byte
        Get
            Return fTranferType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("TranferType", fTranferType, value)
        End Set
    End Property
    Dim fStatus As Integer
    <Persistent("Status")> _
    Public Property Status() As Integer
        Get
            Return fStatus
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Status", fStatus, value)
        End Set
    End Property

    <PersistentAlias("Iif(Status = 1, 'Sin Confirmar', Iif(Status = 2, 'Confirmado',Iif(Status = 3, 'Anulado',Iif(Status = 4, 'Evaluado', ''))))")>
    Public ReadOnly Property StatusName As String
        Get
            'Select Case fStatus
            '    Case 1
            '        Return "Sin Confirmar"
            '    Case 2
            '        Return "Confirmado"
            '    Case 3
            '        Return "Anulado"
            '    Case 4
            '        Return "Evaluado"
            '    Case Else
            '        Return String.Empty
            'End Select
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
        End Get
    End Property

    Dim fFilingUnitSourceId As FilingUnitXpo
    <Association("FilingUnitSourceReferencesFilingUnit")> _
    Public Property FilingUnitSourceId() As FilingUnitXpo
        Get
            Return fFilingUnitSourceId
        End Get
        Set(ByVal value As FilingUnitXpo)
            SetPropertyValue(Of FilingUnitXpo)("FilingUnitSourceId", fFilingUnitSourceId, value)
        End Set
    End Property

    Dim fFilingUnitTargetId As FilingUnitXpo
    <Association("FilingUnitTargetReferencesFilingUnit")> _
    Public Property FilingUnitTargetId() As FilingUnitXpo
        Get
            Return fFilingUnitTargetId
        End Get
        Set(ByVal value As FilingUnitXpo)
            SetPropertyValue(Of FilingUnitXpo)("FilingUnitTargetId", fFilingUnitTargetId, value)
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

    Dim fCreationUser As String
    Public Property CreationUser() As String
        Get
            Return fCreationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CreationUser", fCreationUser, value)
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
