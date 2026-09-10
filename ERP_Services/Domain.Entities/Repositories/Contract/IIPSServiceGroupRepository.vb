'************************************************************
' Assembly         : Domain.Contract
' Author           : Carlos Ernesto Cordoba
' Created          : 22/10/2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region

Public Interface IIPSServiceGroupRepository
    Inherits IRepository(Of BillingConcept)

    ''' <summary>
    ''' Obtiene un grupo de servicios Ips por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetIPSServiceGroup(code As String) As BillingConcept

    ''' <summary>
    ''' Obtiene un grupo de servicios Ips por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetIPSServiceGroupById(id As Integer) As BillingConcept

    ''' <summary>
    ''' Gets the billing concept by identifier includes.
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Function GetBillingConceptByIdIncludes(id As Integer, includes() As String) As BillingConcept

    ''' <summary>
    ''' obtiene un concepto de facturacion por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBillingConceptById(Id As Integer, Optional tracking As Boolean = True) As BillingConcept

    ''' <summary>
    ''' Copia y pega los centros de costo por sucursal y unidad funcional
    ''' </summary>
    ''' <param name="XmlObject"></param>
    ''' <returns></returns>
    Function SP_CopyAndPasteBillingConceptCostCenter(XmlObject As String) As List(Of SP_CopyAndPasteBillingConceptCostCenter_Result)

End Interface
