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
''' Encapsula los datos de los idiomas
''' </summary>
<Persistent("dbo.ADIDIOMA")> _
Public Class LanguageXpo
    Inherits XPLiteObject

    Dim fIDICODIGO As String
    <Key()> _
    <Size(3)> _
    Public Property IDICODIGO() As String
        Get
            Return fIDICODIGO
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IDICODIGO", fIDICODIGO, value)
        End Set
    End Property
    Dim fIDIDESCRI As String
    Public Property IDIDESCRI() As String
        Get
            Return fIDIDESCRI
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IDIDESCRI", fIDIDESCRI, value)
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