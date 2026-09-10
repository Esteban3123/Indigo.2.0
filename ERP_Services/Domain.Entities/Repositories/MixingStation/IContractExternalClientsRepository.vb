'***********************************************************************
' Assembly         : Domain.MixingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 17/12/2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base

Public Interface IContractExternalClientsRepository
    Inherits IRepository(Of ContractExternalClients)

    ''' <summary>
    ''' Obtiene un paquete por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetContractExternalClients(code As String, Optional tracking As Boolean = True) As ContractExternalClients

    ''' <summary>
    ''' Obtiene un paquete por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Function GetContractExternalClientsById(id As String, Optional tracking As Boolean = True) As ContractExternalClients

    ''' <summary>
    ''' Valida el CopyPaste de Excepciones Materia Prima
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_ImportExceptionsRawMaterial(XmlObject As String) As List(Of SP_ImportExceptionsRawMaterial_Result)

    ''' <summary>
    ''' consulta la definicion de tarifa que esta asociada al contrato de centro de atencion externo
    ''' </summary>
    ''' <param name="ContractExternalClientsId"></param>
    ''' <param name="serviceDate"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    Function GetContractExternalClientsDefinitionRateByContractEIdServiceDate(ContractExternalClientsId As Integer, serviceDate As Date, Optional tracking As Boolean = True) As ContractExternalClientsDefinitionRate
End Interface


