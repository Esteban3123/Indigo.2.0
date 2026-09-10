'***********************************************************************
' Assembly         : DistributedServices.Common
' Author           : Carlos Mario Arias Rubiano
' Created          : 03/12/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base

<ServiceContract()> _
Public Interface ICommonERPEconomicActivity

    ''' <summary>
    ''' Guarda o Actualiza una actividad economica
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()> _
    Function SaveEconomicActivity(ByVal EconomicActivity As Domain.Entities.EconomicActivity, session As SessionValues) As ActionResult(Of Domain.Entities.EconomicActivity)

    ''' <summary>
    ''' Elimina una actividad economica
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()> _
    Function DeleteEconomicActivity(ByVal EconomicActivity As Domain.Entities.EconomicActivity, session As SessionValues) As ActionResult

    ''' <summary>
    ''' Obtiene una actividad economica por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetEconomicActivityById(id As Integer, session As SessionValues) As ActionResult(Of Domain.Entities.EconomicActivity)

    ''' <summary>
    ''' Obtiene una actividad economica por codigo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetEconomicActivity(code As String, session As SessionValues) As ActionResult(Of Domain.Entities.EconomicActivity)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function ChangeStateEconomicActivity(ByVal code As String, ByVal state As Boolean, session As SessionValues) As ActionResult(Of Domain.Entities.EconomicActivity)

End Interface
