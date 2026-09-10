
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
''' Encapsula los datos
''' </summary>
<Persistent("dbo.INSALARIM")> _
Public Class MinWageXpo
    Inherits XPLiteObject
    Dim fISALCODIG As String
    <Key()> _
    <Size(3)> _
    Public Property ISALCODIG() As String
        Get
            Return fISALCODIG
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ISALCODIG", fISALCODIG, value)
        End Set
    End Property
    Dim fISALNOMBR As String
    <Size(50)> _
    Public Property ISALNOMBR() As String
        Get
            Return fISALNOMBR
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ISALNOMBR", fISALNOMBR, value)
        End Set
    End Property
    Dim fISALVALOR As Decimal
    Public Property ISALVALOR() As Decimal
        Get
            Return fISALVALOR
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ISALVALOR", fISALVALOR, value)
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
End Class
