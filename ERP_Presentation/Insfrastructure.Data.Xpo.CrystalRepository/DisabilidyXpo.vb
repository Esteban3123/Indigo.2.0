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
''' Encapsula los datos de las Discapacidades
''' </summary>
<Persistent("dbo.ADDISCAPACI")> _
Public Class DisabilidyXpo
    Inherits XPLiteObject

    Dim fDISCCODIGO As String
    <Key()> _
    <Size(3)> _
    Public Property DISCCODIGO() As String
        Get
            Return fDISCCODIGO
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DISCCODIGO", fDISCCODIGO, value)
        End Set
    End Property
    Dim fDISCDESCRI As String
    Public Property DISCDESCRI() As String
        Get
            Return fDISCDESCRI
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DISCDESCRI", fDISCDESCRI, value)
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