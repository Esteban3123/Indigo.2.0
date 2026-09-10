Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

<ServiceContract()>
Public Interface ICommonERPThirdParty

    ''' <summary>
    ''' Busca un tercero atraves de su nit
    ''' </summary>
    ''' <param name="nit">Nit del tercero</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetThirdPartyByNit(nit As String, session As SessionValues) As ThirdParty

    ''' <summary>
    ''' Busca una persona por el numero de identificacion
    ''' </summary>
    ''' <param name="identificationNumber">Numero de identificacion de la persona</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetPersonByIdentification(identificationNumber As String, session As SessionValues) As Person

    ''' <summary>
    ''' Lista todos los terceros
    ''' </summary>
    ''' <returns>Lista de terceros</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ListAllThirdParty(session As SessionValues) As List(Of ThirdParty)

    ''' <summary>
    ''' Funcion que nos retorna si la longitud del Nit es correcta
    ''' </summary>
    ''' <returns>Lista de terceros</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ValidateLenghtNit(ThirdPartyNit As String, IdentificationAcronyms As String, session As SessionValues) As ActionResult(Of Boolean)

    ''' <summary>
    ''' Guarda o edita el tercero
    ''' </summary>
    ''' <param name="thirdParty">Tercero</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function SaveThirdParty(thirdParty As ThirdParty, session As SessionValues) As Boolean

    ''' <summary>
    ''' Elimina un tercero
    ''' </summary>
    ''' <param name="thirdParty">Tercero</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function DeleteThirdParty(thirdParty As ThirdParty, session As SessionValues) As Boolean

    ''' <summary>
    ''' Obtiene una determinada dependencia
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()> _
    Function GetThirdPartyById(ByVal id As Integer, ByVal session As SessionValues) As ThirdParty


    <OperationContract()> _
    Function UpdateStateThirdParty(id As Integer, state As Boolean, session As SessionValues) As Boolean
End Interface
