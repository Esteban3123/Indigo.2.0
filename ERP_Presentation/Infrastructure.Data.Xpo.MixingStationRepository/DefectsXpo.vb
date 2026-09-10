'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Cristian Camilo Bahamon Castaño
' Created          : 01-09-2022
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports DevExpress.Xpo

<Persistent("MixingStation.DefectClassificationItem")>
Partial Public Class DefectsXpo

    Inherits XPLiteObject

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

    Dim _Id As Integer
    <Key(True)>
    Public Property Id() As Integer
        Get
            Return _Id
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", _Id, value)
        End Set
    End Property

    Dim _Description As String
    <Persistent("Description")>
    Public Property Description() As String
        Get
            Return _Description
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Description", _Description, value)
        End Set
    End Property

    Dim _Code As String
    <Persistent("Code")>
    Public Property Code() As String
        Get
            Return _Code
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", _Code, value)
        End Set
    End Property


    <PersistentAlias("concat(concat(Code,' - '),Description)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property


    <Association("DefectsUnitDoseTypeReferencesDefects", GetType(DefectsUnitDoseTypeXpo))>
    Public ReadOnly Property ProductionLineUnitDoseTypeXpo() As XPCollection(Of DefectsUnitDoseTypeXpo)
        Get
            Return GetCollection(Of DefectsUnitDoseTypeXpo)("DefectsUnitDoseTypeXpo")
        End Get
    End Property
End Class
