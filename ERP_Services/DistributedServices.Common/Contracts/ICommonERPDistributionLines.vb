'***********************************************************************
' Assembly         : DistributedServices.Common
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 09-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

<ServiceContract()> _
Public Interface ICommonERPDistributionLines

    ''' <summary>
    ''' Guarda o Actualiza linea de distirbucion
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()> _
    Function SaveDistributionLines(ByVal distributionLines As DistributionLines, session As SessionValues, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of DistributionLines)

    ''' <summary>
    ''' Elimina linea de distribucion al proveedor
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()> _
    Function DeleteDistributionLines(ByVal distributionLines As DistributionLines, session As SessionValues) As ActionResult

    ''' <summary>
    ''' Obtiene una linea de distribucion por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetDistributionLinesById(id As Integer, session As SessionValues) As ActionResult(Of DistributionLines)

    ''' <summary>
    ''' Obtiene una linea de distribucion por codigo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetDistributionLines(code As String, session As SessionValues) As ActionResult(Of DistributionLines)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function ChangeStateDistributionLines(ByVal code As String, ByVal state As Boolean, session As SessionValues) As ActionResult(Of DistributionLines)

End Interface
