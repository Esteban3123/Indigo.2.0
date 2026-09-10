Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Taxes.TaxesPropertyAppraisal")> _
Public Class TaxesTaxesPropertyAppraisalReportXpo
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
    Dim fTaxesPropertyId As TaxesTaxesPropertyReportXpo
    <Association("Taxes_TaxesPropertyAppraisalReferencesTaxes_TaxesProperty")> _
    Public Property TaxesPropertyId() As TaxesTaxesPropertyReportXpo
        Get
            Return fTaxesPropertyId
        End Get
        Set(ByVal value As TaxesTaxesPropertyReportXpo)
            SetPropertyValue(Of TaxesTaxesPropertyReportXpo)("TaxesPropertyId", fTaxesPropertyId, value)
        End Set
    End Property
    Dim fValidity As Integer
    Public Property Validity() As Integer
        Get
            Return fValidity
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Validity", fValidity, value)
        End Set
    End Property
    Dim fAppraisal As Decimal
    Public Property Appraisal() As Decimal
        Get
            Return fAppraisal
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Appraisal", fAppraisal, value)
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
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
