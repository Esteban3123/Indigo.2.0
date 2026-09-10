Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface IAverageStandardCostAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda la entidad
    ''' </summary>
    ''' <param name="standarCost"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function SaveAverageStandardCost(standarCost As StandarCost, audit As AuditMessage) As ActionResult(Of StandarCost)

    ''' <summary>
    ''' Obtiene al entidad por código
    ''' </summary>
    ''' <param name="standarCostCode"></param>
    ''' <returns></returns>
    Function GetAverageStandardCostByCode(standarCostCode As String) As ActionResult(Of StandarCost)

    ''' <summary>
    ''' Obtiene al entidad por Id
    ''' </summary>
    ''' <param name="standarCostId"></param>
    ''' <returns></returns>
    Function GetAverageStandardCostById(standarCostId As Integer) As ActionResult(Of StandarCost)

    ''' <summary>
    ''' carga datos desde un excel
    ''' </summary>
    ''' <param name="dataImportFile"></param>
    ''' <param name="dataCopyPaste"></param>
    ''' <returns></returns>
    Function ImportOrCopyAndPasteDetails(dataImportFile As List(Of ImportFileRow), dataCopyPaste As List(Of List(Of String))) As ActionResult(Of List(Of StandarCostDetails))


End Interface
