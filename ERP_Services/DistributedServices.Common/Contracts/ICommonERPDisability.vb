Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base

<ServiceContract()> _
Public Interface ICommonERPDisability

    ''' <summary>
    ''' Lista todas las Discapacidades
    ''' </summary>
    ''' <returns>Lista de Discapacidades</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function ListAllDisability(session As SessionValues) As List(Of Disability)

    ''' <summary>
    ''' Elimina una discapacidad
    ''' </summary>
    ''' <returns>True o False</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function DeleteDisability(ByVal disability As Disability, session As SessionValues) As ActionMessageResult(Of Disability)

    ''' <summary>
    ''' Graba o Actualiza una Discapacidad
    ''' </summary>
    ''' <param name="disability">Discapacidad</param>
    ''' <param name="audit">Objeto de Auditoria</param>
    ''' <returns>True o False</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function SaveDisability(ByVal disability As Disability, session As SessionValues) As Boolean

    ''' <summary>
    ''' Obtiene una Discapacidad especifica
    ''' </summary>
    ''' <param name="code">Codigo Discapacidad</param>
    ''' <returns>Discapacidad</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetDisability(ByVal code As String, session As SessionValues) As Disability

End Interface
