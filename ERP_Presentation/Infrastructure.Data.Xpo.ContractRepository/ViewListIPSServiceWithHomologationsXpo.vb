'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.ContractRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 30/03/2020
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports System
Imports DevExpress.Xpo

#End Region

''' <summary>
''' conceptos de nota usado en los servicios Xpo
''' </summary>
<Persistent("Contract.ViewListIPSServiceWithHomologations")>
Public Class ViewListIPSServiceWithHomologationsXpo
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

    Dim fId As Integer
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

    Dim fName As String
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property

    Dim fServiceManual As Integer
    Public Property ServiceManual() As Integer
        Get
            Return fServiceManual
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ServiceManual", fServiceManual, value)
        End Set
    End Property

    Dim fServiceManualName As String
    Public Property ServiceManualName() As String
        Get
            Return fServiceManualName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ServiceManualName", fServiceManualName, value)
        End Set
    End Property

    Dim fServiceClass As Integer
    Public Property ServiceClass() As Integer
        Get
            Return fServiceClass
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ServiceClass", fServiceClass, value)
        End Set
    End Property

    Dim fServiceClassName As String
    Public Property ServiceClassName() As String
        Get
            Return fServiceClassName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ServiceClassName", fServiceClassName, value)
        End Set
    End Property

    Dim fPresentation As Integer
    Public Property Presentation() As Integer
        Get
            Return fPresentation
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Presentation", fPresentation, value)
        End Set
    End Property

    Dim fPresentationName As String
    Public Property PresentationName() As String
        Get
            Return fPresentationName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PresentationName", fPresentationName, value)
        End Set
    End Property

    Dim fSelectOption As Boolean
    Public Property SelectOption() As Boolean
        Get
            Return fSelectOption
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("SelectOption", fSelectOption, value)
        End Set
    End Property

    Dim fServiceType As Integer
    Public Property ServiceType() As Integer
        Get
            Return fServiceType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ServiceType", fServiceType, value)
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

    Dim fCodeName As String
    Public Property CodeName() As String
        Get
            Return fCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeName", fCodeName, value)
        End Set
    End Property

    Dim fCupsEntityId As Integer?
    Public Property CupsEntityId() As Integer?
        Get
            Return fCupsEntityId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("CupsEntityId", fCupsEntityId, value)
        End Set
    End Property

    Dim fCupsEntityCode As String
    Public Property CupsEntityCode() As String
        Get
            Return fCupsEntityCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CupsEntityCode", fCupsEntityCode, value)
        End Set
    End Property

    Dim fCupsEntityName As String
    Public Property CupsEntityName() As String
        Get
            Return fCupsEntityName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CupsEntityName", fCupsEntityName, value)
        End Set
    End Property

    Dim fCupsEntityCodeName As String
    Public Property CupsEntityCodeName() As String
        Get
            Return fCupsEntityCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CupsEntityCodeName", fCupsEntityCodeName, value)
        End Set
    End Property

    Dim fIVAValue As Decimal
    Public Property IVAValue() As Decimal
        Get
            Return fIVAValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue("IVAValue", fIVAValue, value)
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
