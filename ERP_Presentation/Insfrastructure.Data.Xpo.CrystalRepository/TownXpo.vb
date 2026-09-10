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
''' Encapsula los datos de los municipios
''' </summary>
<Persistent("dbo.INMUNICIP")> _
Public Class TownXpo
    Inherits XPLiteObject

    Dim fDEPMUNCOD As String
    <Key()> _
    <Size(5)> _
    Public Property DEPMUNCOD() As String
        Get
            Return fDEPMUNCOD
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DEPMUNCOD", fDEPMUNCOD, value)
        End Set
    End Property
    Dim fDEPCODIGO As DepartmentXpo
    <Size(2)> _
    <Association("INMUNICIPReferencesINDEPARTA")> _
    Public Property DEPCODIGO() As DepartmentXpo
        Get
            Return fDEPCODIGO
        End Get
        Set(ByVal value As DepartmentXpo)
            SetPropertyValue(Of DepartmentXpo)("DEPCODIGO", fDEPCODIGO, value)
        End Set
    End Property
    Dim fMUNCODIGO As String
    <Size(3)> _
    Public Property MUNCODIGO() As String
        Get
            Return fMUNCODIGO
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("MUNCODIGO", fMUNCODIGO, value)
        End Set
    End Property
    Dim fMUNNOMBRE As String
    <Size(40)> _
    Public Property MUNNOMBRE() As String
        Get
            Return fMUNNOMBRE
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("MUNNOMBRE", fMUNNOMBRE, value)
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

    <Size(150)> _
    <PersistentAlias("concat(trim(DEPMUNCOD),' - ',trim(MUNNOMBRE))")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property

    <Association("INUBICACIReferencesINMUNICIP", GetType(LocationXpo))> _
    Public ReadOnly Property INUBICACIs() As XPCollection(Of LocationXpo)
        Get
            Return GetCollection(Of LocationXpo)("INUBICACIs")
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