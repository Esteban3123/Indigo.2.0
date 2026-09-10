'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.ContractRepository
' Author           : Miguel Angel Fonseca
' Created          : 2018-09-11
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports System
Imports DevExpress.Xpo

#End Region

''' <summary>
''' asociacion el grupo de atención y los agrupadores usado en los servicios Xpo
''' </summary>
<Persistent("Contract.ViewCareGroupsWithGroupers")>
Public Class ViewCareGroupsWithGroupersXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fRow As String
    <Key(True)>
    Public Property Row() As String
        Get
            Return fRow
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Row", fRow, value)
        End Set
    End Property

    Dim fInvoiceEntityCapitatedId As Integer?
    Public Property InvoiceEntityCapitatedId() As Integer?
        Get
            Return fInvoiceEntityCapitatedId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("InvoiceEntityCapitatedId", fInvoiceEntityCapitatedId, value)
        End Set
    End Property

    Dim fCareGroupId As Integer
    Public Property CareGroupId() As Integer
        Get
            Return fCareGroupId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CareGroupId", fCareGroupId, value)
        End Set
    End Property

    Dim fGroupersId As Integer
    Public Property GroupersId() As Integer
        Get
            Return fGroupersId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("GroupersId", fGroupersId, value)
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

    Dim fDescription As String
    Public Property Description() As String
        Get
            Return fDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Description", fDescription, value)
        End Set
    End Property

    Dim fUserNumber As Integer
    Public Property UserNumber() As Integer
        Get
            Return fUserNumber
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("UserNumber", fUserNumber, value)
        End Set
    End Property

    Dim fUserMin As Integer
    Public Property UserMin() As Integer
        Get
            Return fUserMin
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("UserMin", fUserMin, value)
        End Set
    End Property

    Dim fUserMax As Integer
    Public Property UserMax() As Integer
        Get
            Return fUserMax
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("UserMax", fUserMax, value)
        End Set
    End Property

    Dim fRealCME As Decimal
    Public Property RealCME() As Decimal
        Get
            Return fRealCME
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("RealCME", fRealCME, value)
        End Set
    End Property

    Dim fProjectCME As Decimal
    Public Property ProjectCME() As Decimal
        Get
            Return fProjectCME
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ProjectCME", fProjectCME, value)
        End Set
    End Property

    Dim fFrequence As Decimal
    Public Property Frequence() As Decimal
        Get
            Return fFrequence
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Frequence", fFrequence, value)
        End Set
    End Property

    Dim fEjectEvent As Integer
    Public Property EjectEvent() As Integer
        Get
            Return fEjectEvent
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("EjectEvent", fEjectEvent, value)
        End Set
    End Property

    Dim fTotalEject As Decimal
    Public Property TotalEject() As Decimal
        Get
            Return fTotalEject
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalEject", fTotalEject, value)
        End Set
    End Property

    Dim fTotalContract As Decimal
    Public Property TotalContract() As Decimal
        Get
            Return fTotalContract
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalContract", fTotalContract, value)
        End Set
    End Property

    Dim fUserValue As Decimal
    Public Property UserValue() As Decimal
        Get
            Return fUserValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("UserValue", fUserValue, value)
        End Set
    End Property

    Dim fDocumentDate As Date
    Public Property DocumentDate() As Date
        Get
            Return fDocumentDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("DocumentDate", fDocumentDate, value)
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
