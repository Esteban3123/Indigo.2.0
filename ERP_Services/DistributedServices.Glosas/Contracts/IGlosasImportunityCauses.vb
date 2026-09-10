
#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
#End Region

<ServiceContract()>
Public Interface IGlosasImportunityCauses

#Region "Methods"

    ''' <summary>
    ''' Obtiene un concepto por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetImportunityCausesByCode(code As String, session As SessionValues) As ActionResult(Of ImportunityCauses)

    ''' <summary>
    ''' Obtiene un Concepto por ID
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetImportunityCausesById(ByVal Id As Integer, session As SessionValues) As ActionResult(Of ImportunityCauses)

    ''' <summary>
    ''' Guarda o Actualiza un registro
    ''' </summary>
    ''' <param name="ImportunityCauses">la entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveImportunityCauses(ImportunityCauses As ImportunityCauses, session As SessionValues, idSequense As Int64) As ActionResult(Of ImportunityCauses)

    ''' <summary>
    ''' Metodo para cambiar de estado la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function ChangeStateImportunityCauses(code As String, state As Boolean, session As SessionValues) As ActionResult(Of ImportunityCauses)

    ''' <summary>
    ''' Elimina  un registro
    ''' </summary>
    ''' <param name="ImportunityCauses">La entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteImportunityCauses(ImportunityCauses As ImportunityCauses, session As SessionValues) As ActionResult(Of ImportunityCauses)

#End Region

End Interface
