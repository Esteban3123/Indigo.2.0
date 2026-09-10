Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Budget.ViewReportListRadicate")> _
Public Class BudgetViewReportListRadicateXpo
    Inherits XPLiteObject
    Dim fconsecutivo As Integer
    <Key(True)> _
    Public Property consecutivo() As Integer
        Get
            Return fconsecutivo
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("consecutivo", fconsecutivo, value)
        End Set
    End Property
    Dim fRegimen As String
    <Size(30)> _
    Public Property Regimen() As String
        Get
            Return fRegimen
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Regimen", fRegimen, value)
        End Set
    End Property
    Dim fTipo As String
    <Size(30)> _
    Public Property Tipo() As String
        Get
            Return fTipo
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Tipo", fTipo, value)
        End Set
    End Property
    Dim fValorRC As Decimal
    Public Property ValorRC() As Decimal
        Get
            Return fValorRC
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValorRC", fValorRC, value)
        End Set
    End Property
    Dim fValorLiquidacion As Decimal
    Public Property ValorLiquidacion() As Decimal
        Get
            Return fValorLiquidacion
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValorLiquidacion", fValorLiquidacion, value)
        End Set
    End Property
    Dim fDiferencia As Decimal
    Public Property Diferencia() As Decimal
        Get
            Return fDiferencia
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Diferencia", fDiferencia, value)
        End Set
    End Property
    Dim fDocumentDate As DateTime
    Public Property DocumentDate() As DateTime
        Get
            Return fDocumentDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("DocumentDate", fDocumentDate, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
