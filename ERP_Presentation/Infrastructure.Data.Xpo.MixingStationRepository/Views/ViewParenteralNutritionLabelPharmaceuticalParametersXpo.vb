'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Andrea Coqueco
' Created          : 2024-02-07
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports DevExpress.Xpo

<Persistent("MixingStation.ViewParenteralNutritionLabelPharmaceuticalParameters")>
Partial Public Class ViewParenteralNutritionLabelPharmaceuticalParametersXpo
    Inherits XPLiteObject

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

    Dim fId As Integer
    <Key(True)>
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property

    ''' <summary>
    ''' Id de la prescripción de nutrición parenteral (HCNUTPAREC.ID)
    ''' </summary>
    Dim fParenteralNutritionId As Integer
    Public Property ParenteralNutritionId() As Integer
        Get
            Return fParenteralNutritionId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ParenteralNutritionId", fParenteralNutritionId, value)
        End Set
    End Property

    Dim fParameterName As String
    Public Property ParameterName() As String
        Get
            Return fParameterName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ParameterName", fParameterName, value)
        End Set
    End Property

    Dim fResult As Decimal
    Public Property Result() As Decimal
        Get
            Return fResult
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Result", fResult, value)
        End Set
    End Property

    '#Region "Navigations Properties"

    '    <Association("VReportInvoicePartial_References_VReportInvoicePartialDetail", GetType(BillingVReportInvoicePartialDetail))>
    '    Public ReadOnly Property BillingVReportInvoicePartialDetail() As XPCollection(Of BillingVReportInvoicePartialDetail)
    '        Get
    '            Return GetCollection(Of BillingVReportInvoicePartialDetail)("BillingVReportInvoicePartialDetail")
    '        End Get
    '    End Property

    '#End Region

#Region "Builders"
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub
#End Region

End Class