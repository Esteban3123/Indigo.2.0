'***********************************************************************
' Assembly         : Application.MixingStation
' Author           : Giovanny Plazas L
' Created          : 16-09-2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface IQuantityRemainingAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda sobrantes
    ''' </summary>
    ''' <param name="ListQuantityRemaining">The identifier.</param>
    ''' <param name="audit">The identifier.</param>
    Function SaveQuantityRemaining(ByVal ListQuantityRemaining As List(Of QuantityRemaining), ByVal audit As AuditMessage, Optional InvokeFromHernessed As Boolean = False) As ActionResult

    ''' <summary>
    ''' consulta todos los registros de la tabla
    ''' </summary>
    ''' <returns></returns>
    Function GetAllQuantityRemaining() As ActionResult(Of List(Of QuantityRemaining))

    ''' <summary>
    ''' funcion para consultar la tabla de remanentes por almacen de remanente el cual se parametriza por central de mezclas
    ''' </summary>
    ''' <param name="_cMConfigurationId"></param>
    ''' <returns></returns>
    Function GetQuantityRemainingByMixingStation(_cMConfigurationId As Integer) As ActionResult(Of List(Of QuantityRemaining))

End Interface