'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Daniel Eduardo Arévalo
' Created          : 04-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface IContractTypeAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lista Todos los Tipos de Contrato
    ''' </summary>
    ''' <returns>Tipos de Contratos</returns>
    ''' <remarks></remarks>
    Function ListAllContractType() As List(Of ContractType)

    ''' <summary>
    ''' Elimina un Tipo de Contrato
    ''' </summary>
    ''' <param name="contractType">Tipo de Contrato</param>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function DeleteContractType(ByVal contractType As ContractType, ByVal audit As AuditMessage) As Boolean

    ''' <summary>
    ''' Almacena o Actualiza un Tipo de Contrato
    ''' </summary>
    ''' <param name="contractType">Tipos de Contratos</param>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function SaveContractType(ByVal contractType As ContractType, ByVal audit As AuditMessage) As Boolean

    ''' <summary>
    ''' Obtiene un Tipo de Contrato
    ''' </summary>
    ''' <param name="code">Código del Tipo de Contrato</param>
    ''' <returns>Tipo de Contrato</returns>
    ''' <remarks></remarks>
    Function GetContractType(ByVal code As String) As ContractType

    ''' <summary>
    ''' Obtiene un Tipo de Contrato por ID
    ''' </summary>
    ''' <param name="code">Id del Tipo de Contrato</param>
    ''' <returns>Tipo de Contrato</returns>
    ''' <remarks></remarks>
    Function GetContractTypeById(ByVal ID As String) As ContractType

End Interface
