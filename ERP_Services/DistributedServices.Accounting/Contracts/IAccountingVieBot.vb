#Region "Imports"

Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities

#End Region

<ServiceContract()>
Public Interface IAccountingVieBot

#Region "Methods"

    ''' <summary>
    ''' Lista la configuración de VieBot por formulario
    ''' </summary>
    ''' <param name="Form">Form</param>
    ''' <returns></returns>
    <OperationContract()> _
    Function GetVieBotByForm(ByVal Form As String) As ActionResult(Of List(Of VieBot))

    ''' <summary>
    ''' Guarda o Actualiza un las configuraciones de VieBot
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()> _
    Function SaveVieBot(ByVal ListVieBot As List(Of VieBot)) As ActionResult(Of List(Of VieBot))

#End Region

End Interface
