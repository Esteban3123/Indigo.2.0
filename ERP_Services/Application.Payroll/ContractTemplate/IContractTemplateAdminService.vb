'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Daniel Eduardo Arévalo
' Created          : 06-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IContractTemplateAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lista Todas las Plantillas de Contrato
    ''' </summary>
    ''' <returns>Plantillas de Contrato</returns>
    ''' <remarks></remarks>
    Function ListAllContractTemplate() As List(Of ContractTemplate)

    ''' <summary>
    ''' Elimina una Plantilla de Contrato
    ''' </summary>
    ''' <param name="contractTemplate">Plantilla de Contrato</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function DeleteContractTemplate(ByVal contractTemplate As ContractTemplate, ByVal audit As AuditMessage) As ActionMessageResult(Of ContractTemplate)

    ''' <summary>
    ''' Almacena o Actualiza una Plantilla de Contrato
    ''' </summary>
    ''' <param name="contractTemplate">Plantilla de Contrato</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function SaveContractTemplate(ByVal contractTemplate As ContractTemplate, ByVal audit As AuditMessage) As Boolean

    ''' <summary>
    ''' Obtiene una Plantilla de Contrato
    ''' </summary>
    ''' <param name="code">Código de la Plantilla</param>
    ''' <returns>Plantilla de Contrato</returns>
    ''' <remarks></remarks>
    Function GetContractTemplate(ByVal code As String) As ContractTemplate



End Interface
