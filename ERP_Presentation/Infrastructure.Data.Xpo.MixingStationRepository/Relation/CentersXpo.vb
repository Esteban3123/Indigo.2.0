Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("dbo.ADCENATEN")> _
Public Class CentersXpo
    Inherits XPLiteObject
    Dim fCODCENATE As String
    <Key()> _
    <Size(10)> _
    Public Property CODCENATE() As String
        Get
            Return fCODCENATE
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODCENATE", fCODCENATE, value)
        End Set
    End Property
    Dim fNOMCENATE As String
    Public Property NOMCENATE() As String
        Get
            Return fNOMCENATE
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NOMCENATE", fNOMCENATE, value)
        End Set
    End Property
    Dim fCODIPSSEC As String
    <Size(12)> _
    Public Property CODIPSSEC() As String
        Get
            Return fCODIPSSEC
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODIPSSEC", fCODIPSSEC, value)
        End Set
    End Property
    Dim fNIVATENCI As Integer
    Public Property NIVATENCI() As Integer
        Get
            Return fNIVATENCI
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("NIVATENCI", fNIVATENCI, value)
        End Set
    End Property
    Dim fDIRCENATE As String
    <Size(80)> _
    Public Property DIRCENATE() As String
        Get
            Return fDIRCENATE
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DIRCENATE", fDIRCENATE, value)
        End Set
    End Property
    Dim fINDNUMTEL As String
    <Size(5)> _
    Public Property INDNUMTEL() As String
        Get
            Return fINDNUMTEL
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("INDNUMTEL", fINDNUMTEL, value)
        End Set
    End Property
    Dim fNUMTELCEN As String
    <Size(7)> _
    Public Property NUMTELCEN() As String
        Get
            Return fNUMTELCEN
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NUMTELCEN", fNUMTELCEN, value)
        End Set
    End Property
    Dim fNUMEXTTEL As String
    <Size(6)> _
    Public Property NUMEXTTEL() As String
        Get
            Return fNUMEXTTEL
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NUMEXTTEL", fNUMEXTTEL, value)
        End Set
    End Property
    Dim fNUMCELCEN As String
    <Size(10)> _
    Public Property NUMCELCEN() As String
        Get
            Return fNUMCELCEN
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NUMCELCEN", fNUMCELCEN, value)
        End Set
    End Property
    Dim fDEPMUNCOD As String
    <Size(5)> _
    Public Property DEPMUNCOD() As String
        Get
            Return fDEPMUNCOD
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DEPMUNCOD", fDEPMUNCOD, value)
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
    <Size(150)>
    <PersistentAlias("concat(trim(CODCENATE),' - ',trim(NOMCENATE))")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property

    <Association("CMCenterAttentionReferencesCareCenter", GetType(CMCenterAttentionXpo))>
    Public ReadOnly Property CMCenterAttentionXpo() As XPCollection(Of CMCenterAttentionXpo)
        Get
            Return GetCollection(Of CMCenterAttentionXpo)("CMCenterAttentionXpo")
        End Get
    End Property

    <Association("MedicinesProductionReferencesCareCenter", GetType(MedicinesProductionXpo))>
    Public ReadOnly Property MedicinesProductionXpo() As XPCollection(Of MedicinesProductionXpo)
        Get
            Return GetCollection(Of MedicinesProductionXpo)("MedicinesProductionXpo")
        End Get
    End Property



    <Association("INCENUNFU_References_ADCENATEN", GetType(INCENUNFUXpo))>
    Public ReadOnly Property INCENUNFUs() As XPCollection(Of INCENUNFUXpo)
        Get
            Return GetCollection(Of INCENUNFUXpo)("INCENUNFUs")
        End Get
    End Property


    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
