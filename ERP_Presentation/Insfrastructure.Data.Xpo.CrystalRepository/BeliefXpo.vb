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
<Persistent("dbo.ADCREDO")> _
Public Class BeliefXpo
    Inherits XPLiteObject

    Dim fCREDCODIGO As String
    <Key()> _
    <Size(3)> _
    Public Property CREDCODIGO() As String
        Get
            Return fCREDCODIGO
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CREDCODIGO", fCREDCODIGO, value)
        End Set
    End Property
    Dim fCREDDESCRI As String
    Public Property CREDDESCRI() As String
        Get
            Return fCREDDESCRI
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CREDDESCRI", fCREDDESCRI, value)
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