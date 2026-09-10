'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.CrystalRepository
' Author           : Jhossept K. Garay
' Created          : 24-01-2015
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel
Imports Infrastructure.CrossCutting.Resources

#End Region

''' <summary>
''' Encapsula los datos de las ubicaciónes
''' </summary>
<Persistent("dbo.INUbicaci")> _
Public Class LocationXpo
    Inherits XPLiteObject
    Dim fAUUBICACI As String
    <Key()> _
    <Size(10)> _
    Public Property AUUBICACI() As String
        Get
            Return fAUUBICACI
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AUUBICACI", fAUUBICACI, value)
        End Set
    End Property
    Dim fDEPMUNCOD As TownXpo
    <Size(5)> _
    <Association("INUBICACIReferencesINMUNICIP")> _
    Public Property DEPMUNCOD() As TownXpo
        Get
            Return fDEPMUNCOD
        End Get
        Set(ByVal value As TownXpo)
            SetPropertyValue(Of TownXpo)("DEPMUNCOD", fDEPMUNCOD, value)
        End Set
    End Property
    Dim fUBICODIGO As String
    <Size(5)> _
    Public Property UBICODIGO() As String
        Get
            Return fUBICODIGO
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UBICODIGO", fUBICODIGO, value)
        End Set
    End Property
    Dim fUBINOMBRE As String
    <Size(40)> _
    Public Property UBINOMBRE() As String
        Get
            Return fUBINOMBRE
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UBINOMBRE", fUBINOMBRE, value)
        End Set
    End Property
    Dim fTIPOUBICA As Integer
    Public Property TIPOUBICA() As Integer
        Get
            Return fTIPOUBICA
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("TIPOUBICA", fTIPOUBICA, value)
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

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

#End Region

End Class