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
''' Encapsula los datos de las creencias
''' </summary>
<Persistent("dbo.INDEPARTA")> _
Public Class DepartmentXpo
    Inherits XPLiteObject

    Dim fdepcodigo As String
    <Key()> _
    <Size(2)> _
    Public Property depcodigo() As String
        Get
            Return fdepcodigo
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("depcodigo", fdepcodigo, value)
        End Set
    End Property
    Dim fnomdepart As String
    <Size(40)> _
    Public Property nomdepart() As String
        Get
            Return fnomdepart
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("nomdepart", fnomdepart, value)
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
    <Association("INMUNICIPReferencesINDEPARTA", GetType(TownXpo))> _
    Public ReadOnly Property INMUNICIPs() As XPCollection(Of TownXpo)
        Get
            Return GetCollection(Of TownXpo)("INMUNICIPs")
        End Get
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