'***********************************************************************
' Assembly         : DistributedServices.Common
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 09-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

<ServiceContract()> _
Public Interface ICommonERPSuppliersDistributionLines

    ''' <summary>
    ''' Guarda o Actualiza las lineas de distribucion del proveedor
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()> _
    Function SaveSuppliersDistributionLines(ByVal supplierDistributionLine As SuppliersDistributionLines, session As SessionValues) As ActionResult(Of SuppliersDistributionLines)

    ''' <summary>
    ''' Elimina linea de distribucion al proveedor
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()> _
    Function DeleteSuppliersDistributionLines(ByVal supplierDistributionLine As SuppliersDistributionLines, session As SessionValues) As ActionResult

    ''' <summary>
    ''' Obtiene las lineas de distribucion del proveedor
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetSuppliersDistributionLinesByIdSupplier(id As Integer, session As SessionValues) As List(Of SuppliersDistributionLines)

    ''' <summary>
    ''' Obtiene una linea de distribucion por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetSuppliersDistributionLinesById(id As Integer, session As SessionValues) As SuppliersDistributionLines

    ''' <summary>
    ''' Obtiene el porcentaje de ICA  de la linea de distribucion
    ''' </summary>
    ''' <param name="idSupplierDistributionLine"></param>
    ''' <param name="OperatingUnitId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetICARetentionConceptBySupplierDistributionLine(idSupplierDistributionLine As Integer, OperatingUnitId As Integer, session As SessionValues) As RetentionConcepts

    ''' <summary>
    ''' Obtiene de un proveedor, la lista de lineas de distribución por Concepto Acreencia
    ''' </summary>
    ''' <param name="idSupplier"></param>
    ''' <param name="accusationConcept"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetDistributionLinesByIdSupplierAndAccusationConcept(idSupplier As Integer, accusationConcept As Integer, session As SessionValues) As List(Of SuppliersDistributionLines)

End Interface
