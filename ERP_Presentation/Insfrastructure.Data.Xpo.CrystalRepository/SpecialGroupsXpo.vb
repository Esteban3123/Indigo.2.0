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
''' Encapsula los datos de los grupos especiales
''' </summary>
<Persistent("dbo.ADGRUPESP")> _
Public Class SpecialGroupsXpo
    Inherits XPLiteObject

    Dim fGRUPCODIGO As String
    <Key()> _
    <Size(3)> _
    Public Property GRUPCODIGO() As String
        Get
            Return fGRUPCODIGO
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("GRUPCODIGO", fGRUPCODIGO, value)
        End Set
    End Property
    Dim fGRUPDESCRI As String
    Public Property GRUPDESCRI() As String
        Get
            Return fGRUPDESCRI
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("GRUPDESCRI", fGRUPDESCRI, value)
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