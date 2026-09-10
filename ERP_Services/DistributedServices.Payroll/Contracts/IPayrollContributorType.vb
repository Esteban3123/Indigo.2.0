'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 04-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract()> _
Public Interface IPayrollContributorType

    ''' <summary>
    ''' Lista todos los tipos de contribuyentes
    ''' </summary>
    ''' <returns>Lista de tipos de contribuyentes</returns>
    <OperationContract()> _
    Function ListAllContributorType(session As SessionValues) As List(Of ContributorType)

    ''' <summary>
    ''' Elimina un tipo de contribuyente
    ''' </summary>
    ''' <param name="contributorType">Tipo contribuyente</param>
    ''' <returns></returns>
    <OperationContract()> _
    Function DeleteContributorType(ByVal contributorType As ContributorType, session As SessionValues) As ActionMessageResult(Of ContributorType)

    ''' <summary>
    ''' Guarda o edita un tipo contribuyente
    ''' </summary>
    ''' <param name="contributorType">Tipo Contribuyente</param>
    ''' <returns></returns>
    <OperationContract()> _
    Function SaveContributorType(ByVal contributorType As ContributorType, session As SessionValues) As Boolean

    ''' <summary>
    ''' Obtiene un tipo contribuyente
    ''' </summary>
    ''' <param name="code">Código del tipo contribuyente</param>
    ''' <returns>Tipo Contribuyente</returns>
    <OperationContract()> _
    Function GetContributorType(ByVal code As String, session As SessionValues) As ContributorType

End Interface
