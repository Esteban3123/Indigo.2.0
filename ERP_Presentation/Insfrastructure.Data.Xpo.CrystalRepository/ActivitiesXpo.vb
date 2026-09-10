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
''' Encapsula los datos las actividades o cargos
''' </summary>
<Persistent("dbo.ADACTIVID")> _
Public Class ActivitiesXpo
    Inherits XPLiteObject

    Dim fcodactivi As String
    <Key()> _
    <Size(4)> _
    Public Property codactivi() As String
        Get
            Return fcodactivi
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("codactivi", fcodactivi, value)
        End Set
    End Property
    Dim fdesactivi As String
    <Size(80)> _
    Public Property desactivi() As String
        Get
            Return fdesactivi
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("desactivi", fdesactivi, value)
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