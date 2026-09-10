'***********************************************************************
' Assembly         : Domain.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities

Public Interface IPaymentsConceptRepository
    Inherits IRepository(Of AccountPayableConcepts)

    ''' <summary>
    ''' Lista todos los conceptos de pagos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAllPaymentsConcepts() As List(Of AccountPayableConcepts)

    ''' <summary>
    ''' Obtiene un concepto de pago
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPaymentConcept(code As String, Optional tracking As Boolean = True) As AccountPayableConcepts

    ''' <summary>
    ''' Obtiene un concepto de pago
    ''' </summary>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPaymentConceptById(id As String, Optional tracking As Boolean = True) As AccountPayableConcepts

    ''' <summary>
    ''' Obtiene un concepto de pago
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPaymentConceptByIdSimple(id As Integer) As AccountPayableConcepts

    Function GetAccountPayableICARetentionByDistributionLineIdAndOperatingUnitIdPay(idSupplierDistributionLine As Integer, OperatingUnitId As Integer) As AccountPayableConcepts

End Interface
