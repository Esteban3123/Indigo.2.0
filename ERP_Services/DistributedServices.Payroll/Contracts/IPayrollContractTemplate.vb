'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 06-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract()> _
Public Interface IPayrollContractTemplate

    ''' <summary>
    ''' Lista Todos las Plantillas de Contrato
    ''' </summary>
    ''' <returns>Plantillas de Contrato</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function ListAllContractTemplate(session As SessionValues) As List(Of ContractTemplate)

    ''' <summary>
    ''' Elimina una Plantilla de Contrato
    ''' </summary>
    ''' <param name="contractTemplate">Plantilla de Contrato</param>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function DeleteContractTemplate(ByVal contractTemplate As ContractTemplate, session As SessionValues) As ActionMessageResult(Of ContractTemplate)

    ''' <summary>
    ''' Almacena o Actualiza una Plantilla de Contrato
    ''' </summary>
    ''' <param name="contractTemplate">Plantilla de Contrato</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function SaveContractTemplate(ByVal contractTemplate As ContractTemplate, session As SessionValues) As Boolean

    ''' <summary>
    ''' Obtiene una Plantilla de Contrato
    ''' </summary>
    ''' <param name="code">Código de la Plantilla de Contrato</param>
    ''' <returns>Plantilla de Contrato</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetContractTemplate(ByVal code As String, session As SessionValues) As ContractTemplate

End Interface
