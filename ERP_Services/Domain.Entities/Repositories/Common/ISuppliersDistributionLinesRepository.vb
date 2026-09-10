'***********************************************************************
' Assembly         : Domain.Common
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 30-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities
Public Interface ISuppliersDistributionLinesRepository
    Inherits IRepository(Of SuppliersDistributionLines)

    Function GetAccountPayableICARetentionByDistributionLineIdAndOperatingUnitId(idSupplierDistributionLine As Integer, OperatingUnitId As Integer) As AccountPayableConcepts

    ''' <summary>
    ''' Obtiene las lineas de distribucion que tiene el proveedor
    ''' </summary>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetSuppliersDistributionLinesByIdSupplier(id As Integer, Optional tracking As Boolean = True) As List(Of SuppliersDistributionLines)

    ''' <summary>
    ''' Obtiene las lineas de distribucion que tiene el proveedor
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetSuppliersDistributionLinesByIdSupplierSimple(id As Integer) As List(Of SuppliersDistributionLines)

    ''' <summary>
    ''' Obtiene una linea de distribucion por id
    ''' </summary>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetSuppliersDistributionLinesById(id As Integer, Optional tracking As Boolean = True) As SuppliersDistributionLines

    ''' <summary>
    ''' Obtiene el porcentaje de ICA que maneja la linea de distribucion
    ''' </summary>
    ''' <param name="idSupplierDistributionLine"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetICARetentionConceptBySupplierDistributionLine(idSupplierDistributionLine As Integer, OperatingUnitId As Integer) As RetentionConcepts

    ''' <summary>
    ''' Obtiene de un proveedor, la lista de lineas de distribución por Concepto Acreencia
    ''' </summary>
    ''' <param name="idSupplier"></param>
    ''' <param name="accusationConcept"></param>
    ''' <returns></returns>
    Function GetDistributionLinesByIdSupplierAndAccusationConcept(idSupplier As Integer, accusationConcept As Integer) As List(Of SuppliersDistributionLines)

End Interface
