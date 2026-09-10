Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract()> _
Public Interface ICommonERPCountry

#Region "Country"

    ''' <summary>
    ''' Obtiene todos los Paises
    ''' </summary>
    ''' <returns>Lista de Paises</returns>
    <OperationContract()>
    Function ListAllCountry(session As SessionValues) As List(Of Country)

    ''' <summary>
    ''' Obtiene un Pais especifico
    ''' </summary>
    ''' <param name="code">Codigo del Pais</param>
    ''' <returns>Pais</returns>
    <OperationContract()>
    Function GetCountry(ByVal code As String, session As SessionValues) As Country

    ''' <summary>
    ''' Devuelve un país por ID
    ''' </summary>
    ''' <param name="idCountry">Id del pais</param>
    ''' <returns>El pais</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetCountryById(ByVal idCountry As Integer, session As SessionValues) As Country

    ''' <summary>
    ''' Graba un Pais
    ''' </summary>
    ''' <param name="country">pais a grabar</param>
    ''' <param name="audit">Objeto para la Auditoria</param>
    ''' <returns>Retorna un boleano: 1. Si es exitoso, 0. si no lo fue</returns>
    <OperationContract()>
    Function SaveCountry(ByVal country As Country, session As SessionValues, idSequence As Long) As ActionResult(Of Country)

    ''' <summary>
    ''' Elimina un Pais
    ''' </summary>
    ''' <param name="country">Pais que se desa eliminar</param>
    ''' <param name="audit">Objeto para la Auditoria</param>
    ''' <returns>Retorna un boleano: 1. Si es exitoso el borrado, 0. si no lo fue</returns>
    <OperationContract()>
    Function DeleteCountry(ByVal country As Country, session As SessionValues) As ActionMessageResult(Of Country)

#End Region

End Interface
