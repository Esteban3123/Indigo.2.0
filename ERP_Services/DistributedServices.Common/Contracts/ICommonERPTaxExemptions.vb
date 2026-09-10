'***********************************************************************
' Assembly         : DistributedServices.Common
' Author           : Andres Alarcon
' Created          : 26/08/2025
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base

<ServiceContract()>
Public Interface ICommonERPTaxExemptions

    ''' <summary>
    ''' Guarda o Actualiza una exoneracion tributaria
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveTaxExemptions(ByVal TaxExemptions As Domain.Entities.TaxExemptions, session As SessionValues) As ActionResult(Of Domain.Entities.TaxExemptions)

    ''' <summary>
    ''' Obtiene una exoneracion tributaria por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetTaxExemptionsById(id As Integer, session As SessionValues) As ActionResult(Of Domain.Entities.TaxExemptions)

    ''' <summary>
    ''' Obtiene una exoneracion tributaria por codigo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetTaxExemptions(code As String, session As SessionValues) As ActionResult(Of Domain.Entities.TaxExemptions)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ChangeStateTaxExemptions(ByVal code As String, ByVal state As Boolean, session As SessionValues) As ActionResult(Of Domain.Entities.TaxExemptions)

End Interface
