Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("dbo.ADNIVELES")> _
Public Class LevelsXpo
    Inherits XPLiteObject
    Dim fNIVCODIGO As String
    <Key()> _
    <Size(2)> _
    Public Property NIVCODIGO() As String
        Get
            Return fNIVCODIGO
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NIVCODIGO", fNIVCODIGO, value)
        End Set
    End Property
    Dim fNIVDESCRI As String
    Public Property NIVDESCRI() As String
        Get
            Return fNIVDESCRI
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NIVDESCRI", fNIVDESCRI, value)
        End Set
    End Property
    Dim fNIVPORCMO As Decimal
    Public Property NIVPORCMO() As Decimal
        Get
            Return fNIVPORCMO
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("NIVPORCMO", fNIVPORCMO, value)
        End Set
    End Property
    Dim fNIVPORCOP As Decimal
    Public Property NIVPORCOP() As Decimal
        Get
            Return fNIVPORCOP
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("NIVPORCOP", fNIVPORCOP, value)
        End Set
    End Property
    Dim fNIVPORSUB As Decimal
    Public Property NIVPORSUB() As Decimal
        Get
            Return fNIVPORSUB
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("NIVPORSUB", fNIVPORSUB, value)
        End Set
    End Property
    Dim fNIVPORVIN As Decimal
    Public Property NIVPORVIN() As Decimal
        Get
            Return fNIVPORVIN
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("NIVPORVIN", fNIVPORVIN, value)
        End Set
    End Property
    Dim fNIVSISBEN As Integer
    Public Property NIVSISBEN() As Integer
        Get
            Return fNIVSISBEN
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("NIVSISBEN", fNIVSISBEN, value)
        End Set
    End Property
    Dim fTOPEVECMO As Decimal
    Public Property TOPEVECMO() As Decimal
        Get
            Return fTOPEVECMO
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TOPEVECMO", fTOPEVECMO, value)
        End Set
    End Property
    Dim fTOPEVECOP As Decimal
    Public Property TOPEVECOP() As Decimal
        Get
            Return fTOPEVECOP
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TOPEVECOP", fTOPEVECOP, value)
        End Set
    End Property
    Dim fTOPEVESUB As Decimal
    Public Property TOPEVESUB() As Decimal
        Get
            Return fTOPEVESUB
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TOPEVESUB", fTOPEVESUB, value)
        End Set
    End Property
    Dim fTOPEVEVIN As Decimal
    Public Property TOPEVEVIN() As Decimal
        Get
            Return fTOPEVEVIN
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TOPEVEVIN", fTOPEVEVIN, value)
        End Set
    End Property
    Dim fTOPANUCMO As Decimal
    Public Property TOPANUCMO() As Decimal
        Get
            Return fTOPANUCMO
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TOPANUCMO", fTOPANUCMO, value)
        End Set
    End Property
    Dim fTOPANUCOP As Decimal
    Public Property TOPANUCOP() As Decimal
        Get
            Return fTOPANUCOP
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TOPANUCOP", fTOPANUCOP, value)
        End Set
    End Property
    Dim fTOPANUSUB As Decimal
    Public Property TOPANUSUB() As Decimal
        Get
            Return fTOPANUSUB
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TOPANUSUB", fTOPANUSUB, value)
        End Set
    End Property
    Dim fTOPANUVIN As Decimal
    Public Property TOPANUVIN() As Decimal
        Get
            Return fTOPANUVIN
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TOPANUVIN", fTOPANUVIN, value)
        End Set
    End Property
    Dim fINDAUDFOR As Decimal
    Public Property INDAUDFOR() As Decimal
        Get
            Return fINDAUDFOR
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("INDAUDFOR", fINDAUDFOR, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
