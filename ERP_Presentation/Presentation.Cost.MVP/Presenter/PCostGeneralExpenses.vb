'***********************************************************************
' Assembly         : Presentacion.InteropCost.MVP
' Author           : Diego Andrés Roldán
' Created          : 11-12-2014
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
Imports DevExpress.Xpo
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.CostRepository
Imports Infrastructure.Data.Xpo.AccountingRepository

#End Region

Public Class PCostGeneralExpenses

    ''' <summary>
    ''' variable para comunicar con la interfaz
    ''' </summary>
    Dim View As ICostGeneralExpenses

    ''' <summary>
    ''' variable que obtiene los valores de la sesion
    ''' </summary>
    Dim Indigo As SessionValues

    ''' <summary>
    ''' Constructor que comunica con la interfaz
    ''' </summary>
    Public Sub New(ByRef iView As ICostGeneralExpenses)
        If iView Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        View = iView
        Indigo = SessionValues.Instance
    End Sub

    Public Sub New()
        Indigo = SessionValues.Instance
    End Sub

    ''' <summary>
    ''' Loads the definition layout.
    ''' </summary>
    Public Async Sub LoadDefinitionLayout()
        Await Me.View.MyLayoutControl.LoadDefinitionAsync()
    End Sub

    ''' <summary>
    ''' Obtiene la secuencia
    ''' </summary>
    Public Async Sub GetSequence()
        Using Model As New MCommonCost(View.MyTag)
            Me.View.Sequence = Await Model.GetSequense()
        End Using
    End Sub

    ''' <summary>
    ''' Carga los parámetros de costos
    ''' </summary>
    Public Sub LoadSettingCost()
        Using Model As New MCostSetting(Me.View.MyTag)
            Me.View.SettingsCost = Model.GetCostSetting()
        End Using
    End Sub

    Public Sub InitializeCategory()
        View.CategoryXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).CostService.ListCostGeneralExpenseCategoryByStatusTreeList(True)
    End Sub

    Public Function ListAccounts() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).AccountingService.ListAccountsByLevel(5, True)
    End Function

    Public Function ListCostCenter() As XPInstantFeedbackSource

        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.GetCostCenterByState(True)
    End Function

    Public Function ListProductionCenterCostCenterByProductionCenterId(id As Integer) As XPCollection(Of CostProductionCenterCostCenterXpo)
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CostService.ListProductionCenterCostCenterByProductionCenterId(id)
    End Function

    Public Function ListCostCenterDinamicByListId(listId As List(Of Integer)) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.ListCostCenterDinamicByListId(listId)
    End Function

    Public Function ListMeasureUnitByType() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListMeasureUnitByType(4)
    End Function

    ''' <summary>
    ''' Inicializa los datasource de los search de tipos de comprobantes
    ''' </summary>
    ''' <returns></returns>
    Public Function InitializeVoucherType() As DevExpress.Xpo.XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).AccountingService.ListDocumentTypes(True)
    End Function


    ''' <summary>
    ''' Obtiene la configuracion de la moneda oficial
    ''' </summary>
    ''' <returns></returns>
    Public Function GetOfficialCurrencyFromCompanySettings() As GeneralLedgerCompanySettingsXpo
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).AccountingService.GetXPOObject(Of GeneralLedgerCompanySettingsXpo)(Nothing)
    End Function

End Class