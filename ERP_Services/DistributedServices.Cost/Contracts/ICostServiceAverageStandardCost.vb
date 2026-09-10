Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

<ServiceContract()>
Public Interface ICostServiceAverageStandardCost

    ''' <summary>
    ''' Guarda la entidad
    ''' </summary>
    ''' <param name="standarCost"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveAverageStandardCost(standarCost As StandarCost, audit As AuditMessage) As ActionResult(Of StandarCost)

    ''' <summary>
    ''' Obtiene al entidad por código
    ''' </summary>
    ''' <param name="standarCostCode"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetAverageStandardCostByCode(standarCostCode As String) As ActionResult(Of StandarCost)

    ''' <summary>
    ''' Obtiene al entidad por Id
    ''' </summary>
    ''' <param name="standarCostId"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetAverageStandardCostById(standarCostId As Integer) As ActionResult(Of StandarCost)

    ''' <summary>
    ''' Exportar detalles
    ''' </summary>
    ''' <param name="dataImportFile"></param>
    ''' <param name="dataCopyPaste"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function ImportOrCopyAndPasteDetails(dataImportFile As List(Of ImportFileRow), dataCopyPaste As List(Of List(Of String))) As ActionResult(Of List(Of StandarCostDetails))

End Interface
