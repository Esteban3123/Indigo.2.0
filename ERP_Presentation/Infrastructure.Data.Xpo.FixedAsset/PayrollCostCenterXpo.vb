Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Payroll.CostCenter")> _
Public Class PayrollCostCenterXpo
    Inherits XPLiteObject
    Dim fId As Integer
    <Key(True)> _
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property
    Dim fCode As String
    <Size(20)> _
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property
    Dim fName As String
    <Size(200)> _
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property

    'columna que devuelve el codigo y el nombre de un centro de costo
    <Size(320)> _
    <PersistentAlias("concat(concat(Code,' - '),Name)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property

    Dim fState As Boolean
    Public Property State() As Boolean
        Get
            Return fState
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("State", fState, value)
        End Set
    End Property

    <PersistentAlias("Iif(State = 0, 'Inactivo', Iif(State = 1, 'Activo', ''))")>
    Public ReadOnly Property StateName As String
        Get
            'Select Case fState
            '    Case 0
            '        Return "Inactivo"
            '    Case 1
            '        Return "Activo"
            '    Case Else
            '        Return String.Empty
            'End Select
            Return Convert.ToString(Me.EvaluateAlias("StateName"))
        End Get
    End Property

    Dim fCreationUser As String
    <Size(20)> _
    Public Property CreationUser() As String
        Get
            Return fCreationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CreationUser", fCreationUser, value)
        End Set
    End Property
    Dim fCreationDate As DateTime
    Public Property CreationDate() As DateTime
        Get
            Return fCreationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("CreationDate", fCreationDate, value)
        End Set
    End Property
    Dim fModificationUser As String
    <Size(20)> _
    Public Property ModificationUser() As String
        Get
            Return fModificationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ModificationUser", fModificationUser, value)
        End Set
    End Property
    Dim fModificationDate As DateTime
    Public Property ModificationDate() As DateTime
        Get
            Return fModificationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ModificationDate", fModificationDate, value)
        End Set
    End Property

    <Association("DepreciationDetailCostReferenceCostCenter", GetType(FixedAssetDepreciationDetailCostXpo))>
    Public ReadOnly Property FixedAssetDepreciationDetailCostXpo() As XPCollection(Of FixedAssetDepreciationDetailCostXpo)
        Get
            Return GetCollection(Of FixedAssetDepreciationDetailCostXpo)("FixedAssetDepreciationDetailCostXpo")
        End Get
    End Property

    <Association("FixedAssetAmortizationDetailCostReferenceCostCenter", GetType(FixedAssetAmortizationDetailCostXpo))>
    Public ReadOnly Property FixedAssetAmortizationDetailCostXpo() As XPCollection(Of FixedAssetAmortizationDetailCostXpo)
        Get
            Return GetCollection(Of FixedAssetAmortizationDetailCostXpo)("FixedAssetAmortizationDetailCostXpo")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
