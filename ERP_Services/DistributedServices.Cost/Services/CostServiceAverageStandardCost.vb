Imports System.ServiceModel
Imports Application.Cost
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

Partial Public Class CostService
    Implements ICostServiceAverageStandardCost

    ''' <summary>
    ''' Guarda la entidad
    ''' </summary>
    ''' <param name="standarCost"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function SaveAverageStandardCost(standarCost As StandarCost, audit As AuditMessage) As ActionResult(Of StandarCost) Implements ICostServiceAverageStandardCost.SaveAverageStandardCost
        Using Service As IAverageStandardCostAdminService = Container.Current.Resolve(Of IAverageStandardCostAdminService)
            Return Service.SaveAverageStandardCost(standarCost, audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene al entidad por código
    ''' </summary>
    ''' <param name="standarCostCode"></param>
    ''' <returns></returns>
    Public Function GetAverageStandardCostByCode(standarCostCode As String) As ActionResult(Of StandarCost) Implements ICostServiceAverageStandardCost.GetAverageStandardCostByCode
        Using Service As IAverageStandardCostAdminService = Container.Current.Resolve(Of IAverageStandardCostAdminService)
            Return Service.GetAverageStandardCostByCode(standarCostCode)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene al entidad por Id
    ''' </summary>
    ''' <param name="standarCostId"></param>
    ''' <returns></returns>
    Public Function GetAverageStandardCostById(standarCostId As Integer) As ActionResult(Of StandarCost) Implements ICostServiceAverageStandardCost.GetAverageStandardCostById
        Using Service As IAverageStandardCostAdminService = Container.Current.Resolve(Of IAverageStandardCostAdminService)
            Return Service.GetAverageStandardCostById(standarCostId)
        End Using
    End Function

    ''' <summary>
    ''' Exportar detalles
    ''' </summary>
    ''' <param name="dataImportFile"></param>
    ''' <param name="dataCopyPaste"></param>
    ''' <returns></returns>
    Public Function ImportOrCopyAndPasteDetails(dataImportFile As List(Of ImportFileRow), dataCopyPaste As List(Of List(Of String))) As ActionResult(Of List(Of StandarCostDetails)) Implements ICostServiceAverageStandardCost.ImportOrCopyAndPasteDetails
        Using Service As IAverageStandardCostAdminService = Container.Current.Resolve(Of IAverageStandardCostAdminService)
            Return Service.ImportOrCopyAndPasteDetails(dataImportFile, dataCopyPaste)
        End Using
    End Function
End Class
