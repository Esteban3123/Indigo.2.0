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
''' Encapsula los datos de los niveles de educación
''' </summary>
<Persistent("dbo.ADNIVELED")> _
Public Class EducationLevelsXpo
    Inherits XPLiteObject

    Dim fNIVECODIGO As String
    <Key()> _
    <Size(3)> _
    Public Property NIVECODIGO() As String
        Get
            Return fNIVECODIGO
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NIVECODIGO", fNIVECODIGO, value)
        End Set
    End Property
    Dim fNIVEDESCRI As String
    Public Property NIVEDESCRI() As String
        Get
            Return fNIVEDESCRI
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NIVEDESCRI", fNIVEDESCRI, value)
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