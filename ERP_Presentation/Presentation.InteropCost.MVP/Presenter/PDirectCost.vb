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
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.InteropCostRepository

#End Region

Public Class PDirectCost

    ''' <summary>
    ''' variable para comunicar con la interfaz
    ''' </summary>
    Dim View As IDistributionDirectCost

    ''' <summary>
    ''' variable que obtiene los valores de la sesion
    ''' </summary>
    Dim Indigo As SessionValues

    ''' <summary>
    ''' Constructor que comunica con la interfaz
    ''' </summary>
    Public Sub New(ByRef iView As IDistributionDirectCost)
        If iView Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        View = iView
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
        Using Model As New MCommonInteropCost(View.MyTag)
            Me.View.Sequence = Await Model.GetSequense()
        End Using
    End Sub

    ''' <summary>
    ''' Initializes the general expenses.
    ''' </summary>
    Public Async Function InitializeGeneralExpenses() As Task
        Using model As New MGeneralExpenses(View.MyTag)
            'TODO: Cambiar esta consulta, porque ya no se trae toda las entidades sino que se realiza paso a paso con xpo Att Carlos Mario
            'If Me.View.GeneralExpenseDatasource Is Nothing Then
            '    Dim listAllGeneralExpense As List(Of GeneralExpense) = Await model.ListGeneralExpenseByStatus(True)
            '    Me.View.GeneralExpenseDatasource = listAllGeneralExpense
            'End If
            'Dim listPeriodGeneralExpense As List(Of GeneralExpense) = Await model.ListGeneralExpenseByPeriod(View.Year, View.Month)
            'Me.View.GeneralExpenseDatasource = listAllGeneralExpense.Where(Function(o) Not listPeriodGeneralExpense.Any(Function(y) y.Id = o.Id)).ToList()
        End Using
    End Function

    ''' <summary>
    ''' Initializes the production center.
    ''' </summary>
    Public Sub InitializeProductionCenter(GeneralExpenseId As Integer)
        'Obtengo el elemento del costo por id para sacar los ids de los centros de producción
        Dim generalExpenseXpo = GetGeneralExpenseById(GeneralExpenseId)

        'Obtengo los ids de los centro de producción que están asociados al elemento del costo
        Dim ListProductionCenterIds = (From x In generalExpenseXpo.DistributionBaseXpo(0).DistributionBaseDetailXpo Select x.ProductionCenterId.Id).ToList
        View.ProductionCenterXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).InteropCostService.ListProductionCenterByIds(ListProductionCenterIds)
    End Sub

    ''' <summary>
    ''' Datasource unidades de medida
    ''' </summary>
    ''' <param name="GeneralExpenseId"></param>
    ''' <remarks></remarks>
    Public Sub InitializeMeasurimentUnit(GeneralExpenseId As Integer)
        'Obtengo el elemento del costo por id para sacar los ids de los centros de producción
        Dim generalExpenseXpo = GetGeneralExpenseById(GeneralExpenseId)

        'Obtengo los ids de los centro de producción que están asociados al elemento del costo
        Dim ListMeasurementUnitIds = (From x In generalExpenseXpo.DistributionBaseXpo(0).DistributionBaseMeasurementUnitXpo Select x.MeasurementUnitId.Id).ToList
        View.MeasurementUnitXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListMeasurementUnitByIds(ListMeasurementUnitIds)
    End Sub

    ''' <summary>
    ''' Carga los parámetros de costos
    ''' </summary>
    Public Sub LoadSettingCost()
        Using Model As New MInteropCostSetting(Me.View.MyTag)
            Dim _settingsCost = Model.GetInteropCostSetting()
            If _settingsCost Is Nothing OrElse _settingsCost.Id = 0 Then
                Me.View.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SettingCostNotFound", "InteropCost")
                Exit Sub
            End If
            Me.View.SettingsCost = _settingsCost
        End Using
    End Sub

    ''' <summary>
    ''' Datasource de elementos del costo
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeXpoGeneralExpense()
        Me.View.GeneralExpenseXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).InteropCostService.ListGeneralExpensesByStatus(True)
    End Sub

    ''' <summary>
    ''' Obtiene el elemento del costo por id
    ''' </summary>
    ''' <param name="GeneralExpenseId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetGeneralExpenseById(GeneralExpenseId As Integer) As GeneralExpenseXpo
        Dim filtroConsulta As String = "Id = " & GeneralExpenseId
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InteropCostService.GetCollection(Of GeneralExpenseXpo)(Nothing, filtroConsulta).FirstOrDefault()
    End Function

End Class