'***********************************************************************
' Assembly         : Presentacion.InteropCost.MVP
' Author           : Diego Andrés Roldán Lozano
' Created          : 09-12-2014
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
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.InteropCostRepository

#End Region

Public Class MProductionCenter
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
    ''' Elimina un centro de produccion
    ''' </summary>
    ''' <param name="productionCenter"></param>
    ''' <returns></returns>
    Public Async Function DeleteProductionCenter(productionCenter As ProductionCenter) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.DeleteProductionCenterAsync(productionCenter, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene un centro de produccion por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Async Function GetProductionCenter(code As String) As Task(Of ActionResult(Of ProductionCenter))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.GetProductionCenterAsync(code, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda un centro de produccion
    ''' </summary>
    ''' <param name="productionCenter"></param>
    ''' <returns></returns>
    Public Async Function SaveProductionCenter(productionCenter As ProductionCenter, ByVal idSequence As Int64) As Task(Of ActionResult(Of ProductionCenter))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.SaveProductionCenterAsync(productionCenter, idSequence, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Updates the state1.
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    Public Async Function UpdateStateProductionCenter(code As String, state As Boolean) As Task(Of ActionResult(Of ProductionCenter))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.UpdateStateProductionCenterAsync(code, state, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    Public Async Function GetProductionCenterByCostCenterOid(costCenterId As Integer) As Task(Of ProductionCenter)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.GetProductionCenterByCostCenterOidAsync(costCenterId)
    End Function

    ''' <summary>
    ''' Lista todos los centros de producción
    ''' </summary>
    ''' <returns></returns>
    Public Async Function ListProductionCenter() As Task(Of List(Of ProductionCenter))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.ListProductionCenterAsync()
    End Function

    ''' <summary>
    ''' Obtiene centro de produccion por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Async Function GetProductionCenterById(id As Integer) As Task(Of ProductionCenter)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.GetProductionCenterByIdAsync(id)
    End Function

    Public Function ListServiceAreaByCostCenter(oidCostCenter As List(Of Integer)) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.InteropCostContainer).InteropCostService.ListServiceAreaByCostCenter(oidCostCenter)
    End Function
    'Function ListOrganizationalStructure() As XPInstantFeedbackSource
    '    Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).InteropCostService.ListOrganizationalStructure()
    'End Function

    Public Function ListCostCenterDinamic() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.InteropCostContainer).InteropCostService.ListCostCenterDinamic()
    End Function

    Public Function ListMainAccountSupplyByServiceAreaList(listCuentaServiceArea As List(Of Integer)) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.InteropCostContainer).InteropCostService.ListMainAccountByOIDAccountList(listCuentaServiceArea)
    End Function
    Public Function ListMainAccountErpByClass(classAccount As List(Of Integer)) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.InteropCostContainer).InteropCostService.ListMainAccountErpByClasses(classAccount)
    End Function
    Function ListMainAccountErpByClassAndNivel(clases As List(Of Integer), niveles As List(Of Integer)) As Object
        Return XpoServiceEx.Instance(_indigoSessionValues.InteropCostContainer).InteropCostService.ListMainAccountErpByClassAndNivel(clases, niveles)
    End Function
    Public Function ListMainAccountlabor(costCenterCodeList As List(Of String)) As XPInstantFeedbackSource

        'Dim service = XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).PayrollService
        'Dim _listConceptAccountingStructure As XPCollection(Of Infrastructure.Data.Xpo.PayrollRepository.ConceptAccountingStructureXpo) = service.ListConceptAccountingStructure()
        'Dim mainAccountAccrued As List(Of String) = (From o In _listConceptAccountingStructure Where o.AccruedAccount IsNot Nothing Select o.AccruedAccount).Distinct().ToList()
        'Dim mainAccountDeducted As List(Of String) = (From o In _listConceptAccountingStructure Where o.DeductedAccount IsNot Nothing Select o.DeductedAccount).Distinct().ToList()
        'Return XpoServiceEx.Instance(_indigoSessionValues.InteropCostContainer).InteropCostService.ListMainAccountByNumberAccountList(mainAccountAccrued.Union(mainAccountDeducted).ToList().Distinct().ToList())

        Dim service = XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).PayrollService
        Dim _functionalUnitByCostCenter As XPCollection(Of Infrastructure.Data.Xpo.PayrollRepository.PayrollFunctionalUnit) = service.ListFunctionalUnitByCostCenter(costCenterCodeList)
        Dim structuresId As List(Of Integer) = (From f In _functionalUnitByCostCenter Select f.AccountingStructureId).ToList()
        If structuresId IsNot Nothing AndAlso structuresId.Any() Then
            Dim _listConceptAccountingStructure As XPCollection(Of Infrastructure.Data.Xpo.PayrollRepository.ConceptAccountingStructureXpo) = service.ListConceptAccountingStructureByStructureIds(structuresId)
            Dim mainAccountAccrued As List(Of String) = (From o In _listConceptAccountingStructure Where o.AccruedAccount IsNot Nothing Select o.AccruedAccount).Distinct().ToList()
            Dim mainAccountDeducted As List(Of String) = (From o In _listConceptAccountingStructure Where o.DeductedAccount IsNot Nothing Select o.DeductedAccount).Distinct().ToList()
            Return XpoServiceEx.Instance(_indigoSessionValues.InteropCostContainer).InteropCostService.ListMainAccountByNumberAccountList(mainAccountAccrued.Union(mainAccountDeducted).ToList().Distinct().ToList())
        Else
            Return Nothing
        End If
    End Function
    Public Function ListMainAccountDeprecation(costCenterOid As List(Of Integer)) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.InteropCostContainer).InteropCostService.ListMainAccountDeprecation(costCenterOid)
    End Function
    Function ListMainAccountErpByNivel(nivel As List(Of Integer)) As Object
        Return XpoServiceEx.Instance(_indigoSessionValues.InteropCostContainer).InteropCostService.ListMainAccountErpByNivel(nivel)
    End Function
    Function ListMainAccountComsumo() As Object
        Return XpoServiceEx.Instance(_indigoSessionValues.InteropCostContainer).InteropCostService.ListMainAccountConsumo()
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