'***********************************************************************
' Assembly         : Application.InteropCost
' Author           : Diego Andrés Roldán Lozano
' Created          : 26-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface ICostDistributionDirectCostAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda un gasto directo
    ''' </summary>
    Function SaveDistributionDirectCost(ByVal distributionDirectCost As CostDistributionDirectCost, ByVal audit As AuditMessage, Optional ByVal idSequence As Int64 = 0) As ActionResult(Of CostDistributionDirectCost)

    ''' <summary>
    ''' Elimina un gasto directo
    ''' </summary>
    Function DeleteDistributionDirectCost(ByVal distributionDirectCost As CostDistributionDirectCost, ByVal audit As AuditMessage) As ActionResult

    ' ''' <summary>
    ' ''' Updates the state.
    ' ''' </summary>
    Function UpdateStateDistributionDirectCost(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of CostDistributionDirectCost)

    ''' <summary>
    ''' Obtiene un gasto directo por codigo
    ''' </summary>
    Function GetDistributionDirectCost(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of CostDistributionDirectCost)

    ''' <summary>
    ''' Obtiene un gasto directo por id
    ''' </summary>
    Function GetDistributionDirectCostById(id As Integer) As CostDistributionDirectCost

    ''' <summary>
    ''' Lista los gastos directos por año y mes
    ''' </summary>
    Function ListDistributionDirectCostByYearMonth(ByVal year As Integer, ByVal month As Integer) As List(Of CostDistributionDirectCost)

    ''' <summary>
    ''' Obtiene el valor contable por contenedor, número de cuenta contable, año y mes
    ''' </summary>
    Function GetMainAccountValueByNumberAccountYearAndMotn(ByVal mainAccountId As Integer, ByVal year As String, ByVal month As Integer) As Decimal

    ''' <summary>
    ''' Calcula la distribución del valor entre los distintos centros de producción
    ''' </summary>
    ''' <param name="CostGeneralExpenseId">Elemento del gasto</param>
    ''' <param name="value">Valor a distribuir</param>
    ''' <returns>Lista de detalles de distribución de costos</returns>
    Function CalculateCostDistribution(ByVal CostGeneralExpenseId As Integer, ByVal value As Decimal, ByVal year As Int32, ByVal month As Int32, ByVal containerName As String) As ActionResult(Of List(Of CostDistributionDirectCostDetail))

    ''' <summary>
    ''' Desconfirma la distribución del elemento del costo
    ''' </summary>
    Function DisconfirmDistributionDirectCost(ByVal distributionDirectCost As CostDistributionDirectCost, ByVal audit As AuditMessage) As ActionResult(Of CostDistributionDirectCost)

    ''' <summary>
    ''' Confirma las distribuciones de elementos del costo cuando son documentos tipo provisión
    ''' </summary>
    ''' <remarks></remarks>
    Function SaveDistributionDirectCostProvisionDocument(ByVal IdProvisionDocument As Integer, ByVal audit As AuditMessage) As ActionResult(Of CostDistributionDirectCost)

    ''' <summary>
    ''' Reversa los documentos de provisión
    ''' </summary>
    ''' <remarks></remarks>
    Function ReverseProvisionDocument(ByVal IdProvisionDocument As Integer, ByVal audit As AuditMessage) As ActionResult(Of CostDistributionDirectCost)

    ''' <summary>
    ''' Copy And Paste Detalles de la distribucion de elementos del costo
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    Function CopyAndPasteCostDistributionDirectCostDetail(GeneralExpenseId As Integer, data As List(Of List(Of String))) As ActionResult(Of List(Of CostDistributionDirectCostDetail))

End Interface