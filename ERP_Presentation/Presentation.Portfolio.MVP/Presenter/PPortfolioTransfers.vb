'***********************************************************************
' Assembly         : Presentacion.Portfolio.MVP
' Author           : Carlos Ernesto Cordoba
' Created          : 02-04-2014
'
' Last Modified By :
' Last Modified On :
' Description      :
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Presentation.Base
Imports Presentation.Controls.MVP

#End Region

Public Class PPortfolioTransfers

#Region "Fields"

    ''' <summary>
    ''' Instancia de la interface
    ''' </summary>
    Dim View As IPortfolioTransfers

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
    Public Sub New(ByRef view As IPortfolioTransfers)
        If view Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.Indigo = SessionValues.Instance
            Me.View = view
        End If
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Loads the definition layout.
    ''' </summary>
    Public Async Sub LoadDefinitionLayout()
        Await Me.View.MyLayoutControl.LoadDefinitionAsync()
    End Sub

    ''' <summary>
    ''' inicializa los centros de costo
    ''' </summary>
    Public Sub InitializeCostCenterXPO()
        Me.View.CostCenterXPO = XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.GetCostCenterByState(True)
    End Sub

    ''' <summary>
    ''' inicializa los centros de costo
    ''' </summary>
    Public Sub InitializeCostCenterConceptXPO()
        Me.View.CostCenterConceptXPO = XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.GetCostCenterByState(True)
    End Sub

    Public Sub InitializeCustomerXPO()
        Using model As New MBusqueda
            Dim filter() As Object = {True}
            Me.View.CustomerXPO = model.ConsultarEntidades(eDataSource.ListCustomerByStatus, filter)
        End Using
    End Sub

    ''' <summary>
    ''' obtiene la secuencia numerica
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub GetSequense()
        Using model As New MBlockRecordAndSequense(CStr(Me.View.MyTag))
            Me.View.Sequense = Await model.GetSequense()
        End Using
    End Sub

    'Public Sub InitializeBillsXPO(ThirdPartyId As Integer)
    '    Using model As New MBusqueda
    '        View.BillsXPO = model.ConsultarEntidades(eDataSource.ListBillsTransfers, ThirdPartyId.ToString())
    '    End Using
    'End Sub

    Public Sub InitializeAdvanceXPO(ThirdPartyId As Integer)
        Using model As New MBusqueda
            View.AdvanceXPO = model.ConsultarEntidades(eDataSource.ListAdvanceTransfers, ThirdPartyId.ToString())
        End Using
    End Sub

    ''' <summary>
    ''' inicializa los conceptos de notas
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeNoteConceptXPO()
        Using model As New MBusqueda
            Dim filter() As Object = {True}
            Me.View.NoteConceptXpo = model.ConsultarEntidades(eDataSource.GetAllPortfolioNoteConceptByStatus, filter)
        End Using
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de las ciudades
    ''' </summary>
    Public Sub InitializeAccountXPO()
        Using modelAccountsXPO As New MBusqueda
            Dim filter() As Object = {5, True}
            Me.View.AccountsXPO = modelAccountsXPO.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    ''' <summary>
    ''' Lista los detalles de la definición de tarifa
    ''' </summary>
    ''' <param name="PortfolioTransferId">Id de la cabacera del traslado</param>
    ''' <remarks></remarks>
    Public Function ListPortfolioTransferDetailByPortfolioTransferId(PortfolioTransferId As Integer) As DevExpress.Xpo.XPCollection
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PortfolioService.ListPortfolioTransferDetailByPortfolioTransferId(PortfolioTransferId)
    End Function

    ''' <summary>
    ''' Inicializa los terceros
    ''' </summary>
    Public Sub InitializeThirdPartyXpo()
        Using msearch As New MBusqueda
            Me.View.ThirdPartyXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.GetThirdParty()
        End Using
    End Sub

    ''' <summary>
    ''' lista los facturas viewxpo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ViewInitializeBillsXPO(ThirdPartyId As Integer, StatusSettingTranfers As Boolean) As XPInstantFeedbackSource
        View.BillsXPO = XpoServiceEx.Instance(Indigo.TransactionalContainer).PortfolioService.ViewListBillsTransfers(ThirdPartyId, StatusSettingTranfers)
    End Function
#End Region

End Class