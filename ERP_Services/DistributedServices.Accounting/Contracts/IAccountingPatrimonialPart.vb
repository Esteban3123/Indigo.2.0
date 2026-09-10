'***********************************************************************
' Assembly         : DistributedServices.Accounting
' Author           : Diego Andrés Roldán Lozano
' Created          : 10-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()>
Public Interface IAccountingPatrimonialPart

    ''' <summary>
    ''' Saves the patrimonial part.
    ''' </summary>
    ''' <param name="patrimonialPart">The patrimonial part.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SavePatrimonialPart(ByVal patrimonialPart As Shareholding) As ActionResult(Of Shareholding)

    ''' <summary>
    ''' Updates the state.
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    <OperationContract()>
    Function UpdateStatePatrimonialPart(ByVal code As String, ByVal state As Boolean) As ActionResult(Of Shareholding)

    ''' <summary>
    ''' Deletes the card.
    ''' </summary>
    ''' <param name="patrimonialPart">The patrimonial part.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function DeletePatrimonialPart(ByVal patrimonialPart As Shareholding) As ActionResult

    ''' <summary>
    ''' Gets the card.
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetPatrimonialPart(ByVal code As String) As Shareholding

End Interface
