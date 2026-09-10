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
Imports Infrastructure.Data.Xpo.AccountingRepository
Imports Infrastructure.Data.Xpo.CostRepository

#End Region

Public Class MCostProductionCenter
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

    Public Async Function GetProductionCenterByCostCenterId(costCenterId As Integer) As Task(Of CostProductionCenter)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.GetProductionCenterByCostCenterIdAsync(costCenterId)
    End Function

    ''' <summary>
    ''' Elimina un centro de produccion
    ''' </summary>
    ''' <param name="productionCenter"></param>
    ''' <returns></returns>
    Public Async Function DeleteProductionCenter(productionCenter As CostProductionCenter) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.DeleteCostProductionCenterAsync(productionCenter, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene un centro de produccion por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Async Function GetProductionCenter(code As String) As Task(Of ActionResult(Of CostProductionCenter))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.GetCostProductionCenterAsync(code, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda un centro de produccion
    ''' </summary>
    ''' <param name="productionCenter"></param>
    ''' <returns></returns>
    Public Async Function SaveProductionCenter(productionCenter As CostProductionCenter, ByVal idSequence As Int64) As Task(Of ActionResult(Of CostProductionCenter))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.SaveCostProductionCenterAsync(productionCenter, Me._indigoSessionValues.AuditMessageWcf, idSequence)
    End Function

    ''' <summary>
    ''' Updates the state1.
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    Public Async Function UpdateStateProductionCenter(code As String, state As Boolean) As Task(Of ActionResult(Of CostProductionCenter))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.UpdateStateCostProductionCenterAsync(code, state, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Lista todos los centros de producción
    ''' </summary>
    ''' <returns></returns>
    Public Async Function ListProductionCenter() As Task(Of List(Of CostProductionCenter))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.ListCostProductionCenterAsync()
    End Function

    ''' <summary>
    ''' Obtiene centro de produccion por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Async Function GetProductionCenterById(id As Integer) As Task(Of CostProductionCenter)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.GetCostProductionCenterByIdAsync(id)
    End Function

    Public Function ListFunctionalUnitByCostCenter(idCostCenter As List(Of Integer)) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).PayrollService.GetFunctionalUnitByCostCenterId(idCostCenter)
    End Function

    Function ListOrganizationalStructure() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).CostService.ListCostOrganizationalStructure()
    End Function

    Function ListCategories() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).CostService.ListCostProductionCenterCategoryByStatus(True)
    End Function

    ''' <summary>
    ''' Obtiene el listado de los centros de costo
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCostCenter() As XPInstantFeedbackSource
        Dim ListCostProductionCenterCostCenter = XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).PayrollService.GetCollection(Of CostProductionCenterCostCenterXpo).ToList()
        Dim ListIds As New List(Of Integer)
        If ListCostProductionCenterCostCenter IsNot Nothing AndAlso ListCostProductionCenterCostCenter.Count > 0 Then
            ListCostProductionCenterCostCenter.ForEach(Sub(x) ListIds.Add(x.CostCenterId.Id))
        End If
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).PayrollService.ListCostCenterByIds(ListIds)
    End Function

    Function ListMainAccountlabor(costCenterIdList As List(Of Integer)) As XPInstantFeedbackSource
        Dim service = XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).PayrollService
        Dim _functionalUnitByCostCenter As XPCollection(Of Infrastructure.Data.Xpo.PayrollRepository.PayrollFunctionalUnit) = service.ListFunctionalUnitByCostCenterId(costCenterIdList)
        Dim structuresId As List(Of Integer) = (From f In _functionalUnitByCostCenter Select f.AccountingStructureId).ToList()
        If structuresId IsNot Nothing AndAlso structuresId.Any() Then
            Dim _listConceptAccountingStructure As XPCollection(Of Infrastructure.Data.Xpo.PayrollRepository.ConceptAccountingStructureXpo) = service.ListConceptAccountingStructureByStructureIds(structuresId)
            Dim mainAccountAccrued As List(Of String) = (From o In _listConceptAccountingStructure Where o.AccruedAccount IsNot Nothing Select o.AccruedAccount).Distinct().ToList()
            Dim mainAccountDeducted As List(Of String) = (From o In _listConceptAccountingStructure Where o.DeductedAccount IsNot Nothing Select o.DeductedAccount).Distinct().ToList()
            Dim legalBookOfficial As BookXpo = XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).AccountingService.GetLegalBook().Object(0)
            Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).CostService.ListMainAccountByNumberAccountList(mainAccountAccrued.Union(mainAccountDeducted).ToList().Distinct().ToList(), legalBookOfficial.Id)
        Else
            Return Nothing
        End If
    End Function
    Public Function ListMainAccountlabor() As XPInstantFeedbackSource
        Dim service = XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).PayrollService
        Dim _listConceptAccountingStructure As XPCollection(Of Infrastructure.Data.Xpo.PayrollRepository.ConceptAccountingStructureXpo) = service.ListConceptAccountingStructure()
        Dim mainAccountAccrued As List(Of String) = (From o In _listConceptAccountingStructure Where o.AccruedAccount IsNot Nothing Select o.AccruedAccount).Distinct().ToList()
        Dim mainAccountDeducted As List(Of String) = (From o In _listConceptAccountingStructure Where o.DeductedAccount IsNot Nothing Select o.DeductedAccount).Distinct().ToList()
        Dim legalBookOfficial As BookXpo = XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).AccountingService.GetLegalBook().Object(0)
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).CostService.ListMainAccountByNumberAccountList(mainAccountAccrued.Union(mainAccountDeducted).ToList().Distinct().ToList(), legalBookOfficial.Id)
    End Function
    Public Function ListMainAccountDeprecation(listFunctionalUnit As List(Of Integer), LegalBookId As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).CostService.ListMainAccountDeprecation(listFunctionalUnit, LegalBookId)
    End Function
    Function ListMainAccounts(LegalBookId As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).CostService.ListMainAccountsByStatusAndBookId(True, LegalBookId, True)
    End Function

    Public Function ListMainAccountsByStatusAndBookIdAndClass(status As Boolean, legalBookId As Integer, classes As List(Of Integer), AllowMovement As Boolean) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).CostService.ListMainAccountsByStatusAndBookIdAndClass(status, legalBookId, AllowMovement, classes)
    End Function

    Function ListMainAccountConsumpsion(LegalBookId As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).CostService.ListMainAccountConsumpsion(LegalBookId)
    End Function

    Function GetLegalBook() As BookXpo
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).AccountingService.GetXPOObject(Of BookXpo)($"Status={True} And OfficialBook={True} AND TypeBook IN (1, 2)")
    End Function

    Function ListCostProductionCenterByStatus(status As Boolean) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).CostService.ListCostProductionCenterByStatus(status)
    End Function

    Function ListCostProductionCenterByStatusAndCenterType(status As Boolean, CenterType As Byte) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).CostService.ListCostProductionCenterByStatusAndCenterType(status, CenterType)
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