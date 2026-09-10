Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Taxes.TaxesLiquidation")> _
Public Class TaxesLiquidationXpo
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

    Dim fYearLiquidation As Integer
    Public Property YearLiquidation() As Integer
        Get
            Return fYearLiquidation
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("YearLiquidation", fYearLiquidation, value)
        End Set
    End Property

    Dim fStatus As Byte
    Public Property Status() As Byte
        Get
            Return fStatus
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Status", fStatus, value)
        End Set
    End Property

    <Association("TaxesLiquidationDetailItemReferenceTaxesLiquidation", GetType(TaxesLiquidationDetailXpo))> _
    Public ReadOnly Property TaxesLiquidationDetailXpo() As XPCollection(Of TaxesLiquidationDetailXpo)
        Get
            Return GetCollection(Of TaxesLiquidationDetailXpo)("TaxesLiquidationDetailXpo")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
