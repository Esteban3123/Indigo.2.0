'***********************************************************************
' Assembly         : Presentacion.Portfolio.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 13/10/2016
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Controls.MVP
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.PortfolioRepository
Imports DevExpress.Xpo
Imports Domain.Base.Entities

#End Region

Public Class PPortfolioProvision

#Region "Fields"

    ''' <summary>
    ''' Instancia de la interface
    ''' </summary>
    Dim View As IProvisionAndDeterioration

    ''' <summary>
    ''' Referencia a los valores de sesión
    ''' </summary>
    Dim Indigo As SessionValues

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="view">Referencia a la vista del frontal</param>
    Public Sub New(ByRef view As IProvisionAndDeterioration)
        If view Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.Indigo = SessionValues.Instance
            Me.View = view
        End If
    End Sub


#End Region

#Region "Methods"

    Public Async Sub LoadDefinitionLayout()
        Await Me.View.MyLayoutControl.LoadDefinitionAsync()
    End Sub

    Public Async Sub GetSequense()
        Using model As New MBlockRecordAndSequense(Me.View.MyTag)
            Me.View.Sequense = Await model.GetSequense()
        End Using
    End Sub

#Region "Ages"

    Public Sub InititalizeAges(OperatingUnitId As Integer)
        If View.ListAgesPorfolio Is Nothing Then
            Dim filtro As String = "OperatingUnitId = " & OperatingUnitId
            Dim settingPortfolioXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).PaymentsService.GetCollection(Of PortfolioSettingPortfolioXpo)(Nothing, filtro).FirstOrDefault()
            If settingPortfolioXpo IsNot Nothing Then
                Dim ListAgesPorfolio = New List(Of Domain.Entities.AgesPortfolio)
                Dim agesXpo = settingPortfolioXpo.PortfolioAgesPortfolioXpo.ToList()
                For Each age In agesXpo
                    ListAgesPorfolio.Add(New Domain.Entities.AgesPortfolio With
                    {
                        .Id = age.Id,
                        .Name = age.Name,
                        .InitialRange = age.InitialRange,
                        .EndRange = age.EndRange
                    })
                Next

                ListAgesPorfolio.Add(New Domain.Entities.AgesPortfolio With
                {
                    .Name = settingPortfolioXpo.NameMaximumAgeRange,
                    .InitialRange = settingPortfolioXpo.MaximunAgeRange + 1,
                    .EndRange = Integer.MaxValue
                })

                View.ListAgesPorfolio = ListAgesPorfolio
            End If
        End If
    End Sub

#End Region

#Region "Get Details"

    Public Function ListPortfolioProvisionDetailByProvisionId(PortfolioProvisionId As Integer) As List(Of PortfolioProvisionDetailXpo)
        Dim filtroConsulta As String = "PortfolioProvisionId = " & PortfolioProvisionId
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PortfolioService.GetCollection(Of PortfolioProvisionDetailXpo)(Nothing, filtroConsulta)
    End Function

    Public Function ListVPortfolioProvisionDetailByProvisionId(PortfolioProvisionId As Integer) As List(Of VPortfolioProvisionDetailXpo)
        Dim filtroConsulta As String = "PortfolioProvisionId = " & PortfolioProvisionId
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PortfolioService.GetCollection(Of VPortfolioProvisionDetailXpo)(Nothing, filtroConsulta)
    End Function

#End Region

#Region "Get Invoices"

    Public Sub ListInvoiceProvisionAndDeteriorationGlosa(courtDate As Date)
        View.InvoiceNumberXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).PortfolioService.ListInvoiceCxCProvisionAndDeteriorationGlosa(courtDate)
    End Sub

    Public Sub ListInvoiceProvisionAndDeteriorationNotGlosa(courtDate As Date)
        View.InvoiceNumberXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).PortfolioService.ListInvoiceCxCProvisionAndDeteriorationNotGlosa(courtDate)
    End Sub

    Public Sub ListInvoiceProvisionAndDeterioration()
        View.InvoiceNumberXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).PortfolioService.ListInvoiceCxCProvisionAndDeterioration()
    End Sub

    Public Function GeViewAccountReceivableByPortfolioProvisionById(Id As Integer) As ViewAccountReceivableByPortfolioProvisionXpo
        Dim filter As String = "Id = " & Id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PortfolioService.GetCollection(Of ViewAccountReceivableByPortfolioProvisionXpo)(Nothing, filter).FirstOrDefault()
    End Function

    Public Function GetInvoiceByInitialRangeAndEndRangeGlosa(CourtDate As Date, InitialRange As Integer, EndRange As Integer) As XPCollection(Of ViewAccountReceivableByPortfolioProvisionXpo)
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PortfolioService.ListInvoiceCxCProvisionAndDeteriorationByInitialRangeAndEndRangeGlosa(CourtDate.ToString("yyyy/MM/dd"), InitialRange, EndRange)
    End Function

    Public Function GetInvoiceByInitialRangeAndEndRangeNotGlosa(CourtDate As Date, InitialRange As Integer, EndRange As Integer) As XPCollection(Of ViewAccountReceivableByPortfolioProvisionXpo)
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PortfolioService.ListInvoiceCxCProvisionAndDeteriorationByInitialRangeAndEndRangeNotGlosa(CourtDate.ToString("yyyy/MM/dd"), InitialRange, EndRange)
    End Function

    Public Function GetInvoiceByInitialRangeAndEndRange(CourtDate As Date, InitialRange As Integer, EndRange As Integer) As XPCollection(Of ViewAccountReceivableByPortfolioProvisionXpo)
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PortfolioService.ListInvoiceCxCProvisionAndDeteriorationByInitialRangeAndEndRange(CourtDate.ToString("yyyy/MM/dd"), InitialRange, EndRange)
    End Function

#End Region

#End Region

End Class