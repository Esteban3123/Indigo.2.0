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
''' Encapsula los datos de los grupos étnicos
''' </summary>
<Persistent("dbo.ADGRUETNI")> _
Public Class EthnicGroupXpo
    Inherits XPLiteObject

    Dim fCODGRUPOE As String
    <Key()> _
    <Size(3)> _
    Public Property CODGRUPOE() As String
        Get
            Return fCODGRUPOE
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODGRUPOE", fCODGRUPOE, value)
        End Set
    End Property
    Dim fDESGRUPET As String
    <Size(70)> _
    Public Property DESGRUPET() As String
        Get
            Return fDESGRUPET
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DESGRUPET", fDESGRUPET, value)
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