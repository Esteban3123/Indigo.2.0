#Region "Imports"

Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities

#End Region

<ServiceContract()>
Public Interface IAccountingAccountClass

#Region "Methods"

    ''' <summary>
    ''' Obtiene un tipo de documento por su código
    ''' </summary>
    ''' <param name="code">Código del documento a consultar</param>
    ''' <returns>Tipo de documento consultado</returns>
    <OperationContract()>
    Function GetAccountClassByCode(ByVal code As String, Optional tracking As Boolean = True) As MainAccountClasses

    ''' <summary>
    ''' Obtiene un tipo de documento por su código
    ''' </summary>
    ''' <returns>Tipo de documento consultado</returns>
    <OperationContract()>
    Function GetAccountClassById(ByVal id As Integer, Optional tracking As Boolean = True) As MainAccountClasses

    ''' <summary>
    ''' Graba un tipo de documento
    ''' </summary>
    ''' <param name="doc">Tipo de documento a grabar</param>
    ''' <returns>Resultado de la acción</returns>
    <OperationContract()>
    Function SaveAccountClass(ByVal doc As MainAccountClasses) As ActionResult(Of MainAccountClasses)

    ''' <summary>
    ''' Updates the state card.
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    <OperationContract()>
    Function UpdateStateAccountClass(ByVal code As String, ByVal state As Boolean) As ActionResult(Of MainAccountClasses)

    ''' <summary>
    ''' Elimina un tipo de documento
    ''' </summary>
    ''' <param name="doc">Tipo de documento a eliminar</param>
    ''' <returns>Resultado de la acción</returns>
    <OperationContract()>
    Function DeleteAccountClass(ByVal doc As MainAccountClasses) As ActionResult

    ''' <summary>
    ''' Gets all acount class.
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetAllAcountClass() As List(Of MainAccountClasses)

#End Region

End Interface
