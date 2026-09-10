Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("dbo.INENTIDAD")> _
Public Class EntityXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fCODENTIDA As String
    <Key()> _
    <Size(9)> _
    Public Property CODENTIDA() As String
        Get
            Return fCODENTIDA
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODENTIDA", fCODENTIDA, value)
        End Set
    End Property
    Dim fNOMENTIDA As String
    <Size(150)> _
    Public Property NOMENTIDA() As String
        Get
            Return fNOMENTIDA
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NOMENTIDA", fNOMENTIDA, value)
        End Set
    End Property
    Dim fCODIGONIT As String
    <Size(15)> _
    Public Property CODIGONIT() As String
        Get
            Return fCODIGONIT
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODIGONIT", fCODIGONIT, value)
        End Set
    End Property
    Dim fCODADMPAG As String
    <Size(9)> _
    Public Property CODADMPAG() As String
        Get
            Return fCODADMPAG
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODADMPAG", fCODADMPAG, value)
        End Set
    End Property
    Dim fDIGITOVER As Char
    Public Property DIGITOVER() As Char
        Get
            Return fDIGITOVER
        End Get
        Set(ByVal value As Char)
            SetPropertyValue(Of Char)("DIGITOVER", fDIGITOVER, value)
        End Set
    End Property
    Dim fENTDIRECC As String
    <Size(150)> _
    Public Property ENTDIRECC() As String
        Get
            Return fENTDIRECC
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ENTDIRECC", fENTDIRECC, value)
        End Set
    End Property
    Dim fENTITELEF As String
    <Size(15)> _
    Public Property ENTITELEF() As String
        Get
            Return fENTITELEF
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ENTITELEF", fENTITELEF, value)
        End Set
    End Property
    Dim fENTCONTAC As String
    <Size(80)> _
    Public Property ENTCONTAC() As String
        Get
            Return fENTCONTAC
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ENTCONTAC", fENTCONTAC, value)
        End Set
    End Property
    Dim fENTIEMAIL As String
    <Size(50)> _
    Public Property ENTIEMAIL() As String
        Get
            Return fENTIEMAIL
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ENTIEMAIL", fENTIEMAIL, value)
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
    <Association("ADINGRESOReferencesINENTIDAD", GetType(AdmissionXpo))> _
    Public ReadOnly Property ADINGRESOes() As XPCollection(Of AdmissionXpo)
        Get
            Return GetCollection(Of AdmissionXpo)("ADINGRESOes")
        End Get
    End Property
    <Association("INPACIENTReferencesINENTIDAD", GetType(PatientXpo))> _
    Public ReadOnly Property INPACIENTs() As XPCollection(Of PatientXpo)
        Get
            Return GetCollection(Of PatientXpo)("INPACIENTs")
        End Get
    End Property

#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

#End Region

End Class