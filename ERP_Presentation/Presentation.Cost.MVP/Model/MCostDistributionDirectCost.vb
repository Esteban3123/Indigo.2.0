'***********************************************************************
' Assembly         : Presentacion.InteropCost.MVP
' Author           : Diego Andrés Roldán Lozano
' Created          : 26-02-2016
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Presentation.CloudAgent
Imports Domain.Base.Entities
Imports Presentation.Base
Imports System.ServiceModel
Imports Infrastructure.Data.Xpo
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.CostRepository
#End Region

Public Class MCostDistributionDirectCost
    Implements IDisposable

#Region "Fields"

    ''' <summary>
    ''' Referencia a los valores de session
    ''' </summary>
    Private _indigoSessionValues As SessionValues

    ''' <summary>
    ''' Id del frontal
    ''' </summary>
    Private _tagForm As String

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New(ByVal tag As String)
        Me._tagForm = tag
        Me._indigoSessionValues = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Calcula la distribución para cada centro de producción
    ''' </summary>
    ''' <param name="GeneralExpenseId">Elemento del costo</param>
    ''' <param name="value">Valor a distribuir</param>
    ''' <returns>Lista de costos</returns>
    Public Async Function CalcDistribution(GeneralExpenseId As Integer, ByVal value As Decimal, ByVal year As Integer, ByVal month As Integer, ByVal containerName As String) As Task(Of ActionResult(Of List(Of CostDistributionDirectCostDetail)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.CalculateCostDistributionAsync(GeneralExpenseId, value, year, month, containerName)
    End Function

    ''' <summary>
    ''' Elimina un gasto directo
    ''' </summary>
    Public Async Function DeleteDistributionDirectCost(distributionDirectCost As CostDistributionDirectCost) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.DeleteDistributionDirectCostAsync(distributionDirectCost, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene un gasto directo por codigo
    ''' </summary>
    Public Async Function GetDistributionDirectCost(code As String) As Task(Of ActionResult(Of CostDistributionDirectCost))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.GetDistributionDirectCostAsync(code, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene un gasto directo por id
    ''' </summary>
    Public Async Function GetDistributionDirectCostById(id As Integer) As Task(Of CostDistributionDirectCost)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.GetDistributionDirectCostByIdAsync(id)
    End Function

    ''' <summary>
    ''' Obtiene una el tipo de proveedor
    ''' </summary>
    Public Async Function GetSupplierTypeBySupplierId(supplierId As Integer) As Task(Of ActionResult(Of List(Of SupplierType)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.GetSupplierTypeBySupplierIdAsync(supplierId)
    End Function

    ''' <summary>
    ''' Lists the distribution direct cost by year month.
    ''' </summary>
    Public Async Function ListDistributionDirectCostByYearMonth(ByVal year As String, ByVal month As Integer) As Task(Of List(Of CostDistributionDirectCost))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.ListDistributionDirectCostByYearMonthAsync(year, month)
    End Function

    ''' <summary>
    ''' Gets the main account value by container number account year and motn.
    ''' </summary>
    Public Async Function GetMainAccountValueByNumberAccountYearAndMotn(ByVal accountId As Integer, ByVal year As String, ByVal month As Integer) As Task(Of Decimal)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.GetMainAccountValueByNumberAccountYearAndMotnAsync(accountId, year, month)
    End Function

    ''' <summary>
    ''' Guarda un gasto directo
    ''' </summary>
    Public Async Function SaveDistributionDirectCost(distributionDirectCost As CostDistributionDirectCost, ByVal idSequence As Long) As Task(Of ActionResult(Of CostDistributionDirectCost))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.SaveDistributionDirectCostAsync(distributionDirectCost, Me._indigoSessionValues.AuditMessageWcf, idSequence)
    End Function

    ''' <summary>
    ''' Reversa un documento de provisión
    ''' </summary>
    Public Async Function ReverseProvisionDocument(distributionDirectCostId As Integer) As Task(Of ActionResult(Of CostDistributionDirectCost))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.ReverseProvisionDocumentAsync(distributionDirectCostId, Me._indigoSessionValues.AuditMessageWcf)
    End Function


    ''' <summary>
    ''' Updates the state distribution direct cost.
    ''' </summary>
    Public Async Function UpdateStateDistributionDirectCost(code As String, state As Boolean) As Task(Of ActionResult(Of CostDistributionDirectCost))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.UpdateStateDistributionDirectCostAsync(code, state, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Desconfirma la distribución del costo
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DisconfirmDistributionDirectCost(distributionDirectCost As CostDistributionDirectCost) As Task(Of ActionResult(Of Domain.Entities.CostDistributionDirectCost))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.DisconfirmDistributionDirectCostAsync(distributionDirectCost, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Funcion para obtener el fabricante 
    ''' </summary>
    ''' <param name="id">El id del fabricante</param>
    ''' <returns></returns>
    Public Function GetThirdPartyById(ByVal id As Integer) As Domain.Entities.ThirdParty
        Return IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.GetThirdPartyById(id, _indigoSessionValues)
    End Function

    Public Function GetThirdPartyByIdSupplier(ByVal id As Integer) As Domain.Entities.ThirdParty
        Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
        If id > 0 Then
            Return IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.GetThirdPartyByIdSupplier(id, _indigoSessionValues)
        End If
    End Function

    ''' <summary>
    ''' Lista los documentos de provision
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetListViewProvisionDocument(SupplierId As Integer, GeneralExpenseId As Integer) As XPCollection
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).CostService.GetListViewProvisionDocument(SupplierId, GeneralExpenseId)
    End Function

    ''' <summary>
    ''' Función asíncrona para el copiado y pegado de datos en rejilla
    ''' </summary>
    ''' <param name="GeneralExpenseId"></param>
    ''' <param name="data"></param>
    ''' <returns></returns>
    Public Async Function CopyAndPasteCostDistributionDirectCostDetailAsync(ByVal GeneralExpenseId As Integer, data As List(Of List(Of String))) As Task(Of ActionResult(Of List(Of CostDistributionDirectCostDetail)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.CopyAndPasteCostDistributionDirectCostDetailAsync(GeneralExpenseId, data)
    End Function

    ''' <summary>
    ''' Función síncrona para el copiado y pegado de datos en rejilla
    ''' </summary>
    ''' <param name="GeneralExpenseId"></param>
    ''' <param name="data"></param>
    ''' <returns></returns>
    Public Function CopyAndPasteCostDistributionDirectCostDetail(ByVal GeneralExpenseId As Integer, data As List(Of List(Of String))) As ActionResult(Of List(Of CostDistributionDirectCostDetail))
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCost.CopyAndPasteCostDistributionDirectCostDetail(GeneralExpenseId, data)
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: desechar estado administrado (objetos administrados).
            End If

            ' TODO: liberar recursos no administrados (objetos no administrados) e invalidar Finalize() below.
            ' TODO: Establecer campos grandes como Null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: invalidar Finalize() sólo si la instrucción Dispose(ByVal disposing As Boolean) anterior tiene código para liberar recursos no administrados.
    'Protected Overrides Sub Finalize()
    '    ' No cambie este código. Ponga el código de limpieza en la instrucción Dispose(ByVal disposing As Boolean) anterior.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' Visual Basic agregó este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en Dispose(disposing As Boolean).
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
