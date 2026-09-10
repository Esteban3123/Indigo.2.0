'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           :
' Created          : 05-06-2026
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base

<ServiceContract()>
Public Interface IPayrollContributorTypeSubtype

    ''' <summary>
    ''' Lista todas las combinaciones Tipo+Subtipo de cotizante con nombres desnormalizados
    ''' </summary>
    <OperationContract()>
    Function ListAllContributorTypeSubtype(ByVal session As SessionValues) As List(Of ContributorTypeSubtype)

    ''' <summary>
    ''' Lista los subtipos válidos para un Tipo de cotizante
    ''' </summary>
    <OperationContract()>
    Function ListByContributorTypeId(ByVal contributorTypeId As Integer, ByVal session As SessionValues) As List(Of ContributorTypeSubtype)

    ''' <summary>
    ''' Guarda una nueva combinacion Tipo+Subtipo de cotizante
    ''' </summary>
    <OperationContract()>
    Function SaveContributorTypeSubtype(ByVal contributorTypeSubtype As ContributorTypeSubtype, ByVal session As SessionValues) As Boolean

    ''' <summary>
    ''' Elimina una combinacion Tipo+Subtipo de cotizante por Id
    ''' </summary>
    <OperationContract()>
    Function DeleteContributorTypeSubtype(ByVal id As Integer, ByVal session As SessionValues) As Boolean

End Interface
