'***********************************************************************
' Assembly         : Application.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 02/04/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface ISuppliersDistributionLinesAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda o Actualiza las lineas de distribucion del proveedor
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveSuppliersDistributionLines(ByVal supplierDistributionLine As SuppliersDistributionLines, ByVal audit As AuditMessage) As ActionResult(Of SuppliersDistributionLines)

    ''' <summary>
    ''' Elimina linea de distribucion al proveedor
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteSuppliersDistributionLines(ByVal supplierDistributionLine As SuppliersDistributionLines, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene las lineas de distribucion del proveedor
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetSuppliersDistributionLinesByIdSupplier(id As Integer, ByVal audit As AuditMessage) As List(Of SuppliersDistributionLines)

    ''' <summary>
    ''' Obtiene una linea de distribucion por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetSuppliersDistributionLinesById(id As Integer, ByVal audit As AuditMessage) As SuppliersDistributionLines

    ''' <summary>
    ''' Obtiene el ICA porcentaje que maneja la linea de distribucion
    ''' </summary>
    ''' <param name="idSupplierDistributionLine"></param>
    ''' <param name="OperatingUnitId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetICARetentionConceptBySupplierDistributionLine(idSupplierDistributionLine As Integer, OperatingUnitId As Integer) As RetentionConcepts

    Function GetAccountPayableICARetentionByDistributionLineIdAndOperatingUnitId(idSupplierDistributionLine As Integer, OperatingUnitId As Integer) As AccountPayableConcepts

    ''' <summary>
    ''' Obtiene de un proveedor, la lista de lineas de distribución por Concepto Acreencia
    ''' </summary>
    ''' <param name="idSupplier"></param>
    ''' <param name="accusationConcept"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function GetDistributionLinesByIdSupplierAndAccusationConcept(idSupplier As Integer, accusationConcept As Integer, ByVal audit As AuditMessage) As List(Of SuppliersDistributionLines)
End Interface
