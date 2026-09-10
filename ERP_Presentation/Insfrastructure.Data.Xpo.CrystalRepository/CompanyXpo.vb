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
''' Encapsula los datos de las empresas
''' </summary>
<Persistent("dbo.ADEMPRESA")> _
Public Class CompanyXpo
    Inherits XPLiteObject

    Dim fCODEMPRES As String
    <Key()> _
    <Size(5)> _
    Public Property CODEMPRES() As String
        Get
            Return fCODEMPRES
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODEMPRES", fCODEMPRES, value)
        End Set
    End Property
    Dim fDESEMPRES As String
    <Size(80)> _
    Public Property DESEMPRES() As String
        Get
            Return fDESEMPRES
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DESEMPRES", fDESEMPRES, value)
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