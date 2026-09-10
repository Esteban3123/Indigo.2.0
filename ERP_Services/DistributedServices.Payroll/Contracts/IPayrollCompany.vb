'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Daniel Eduardo Arévalo
' Created          : 25-04-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract()> _
Public Interface IPayrollCompany

    ''' <summary>
    ''' Lista todas las Compañias
    ''' </summary>
    ''' <returns>Lista de Compañias</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function ListAllCompany(session As SessionValues) As List(Of Company)

    ''' <summary>
    ''' Obtiene una Compañia
    ''' </summary>
    ''' <param name="code">Código de Compañia</param>
    ''' <returns>Compañia</returns>
    <OperationContract()> _
    Function GetCompany(ByVal code As String, session As SessionValues) As Company

    ''' <summary>
    ''' Graba o Actualiza una Compañia
    ''' </summary>
    ''' <param name="company">Compañia</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function SaveCompany(ByVal company As Company, session As SessionValues) As Boolean

    ''' <summary>
    ''' Elimina una Compañía
    ''' </summary>
    ''' <param name="company">Compañia</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    <OperationContract()> _
    Function DeleteCompany(ByVal company As Company, session As SessionValues) As ActionMessageResult(Of Company)

End Interface
