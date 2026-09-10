'************************************************************
' Assembly         : Domain.Billing
' Author           : Carlos Ernesto Cordoba
' Created          : 13-11-2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Entities

#End Region


Public Interface IBillingConceptRepository
    Inherits IRepository(Of BillingConcept)

    ''' <summary>
    ''' obtiene un concepto de facturacion por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBillingConceptById(Id As Integer, Optional tracking As Boolean = True) As BillingConcept

End Interface
