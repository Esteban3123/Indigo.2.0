#Region "Imports"

Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel

#End Region

<ServiceContract()>
Public Interface IAccountingHealthSuperParameters

    ''' <summary>
    ''' Obtiene un formato  por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetHealthSuperParametersById(id As Integer, ByVal audit As AuditMessage) As ActionResult(Of HealthSuperParameters)

    ''' <summary>
    ''' Obtiene un formato  por codigo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetHealthSuperParametersByCode(code As String, ByVal audit As AuditMessage) As ActionResult(Of HealthSuperParameters)

    ''' <summary>
    ''' Guarda o Actualiza un formato 
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveHealthSuperParameters(ByVal HealthSuperParameters As HealthSuperParameters, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of HealthSuperParameters)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ChangeStateHealthSuperParameters(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of HealthSuperParameters)

    ''' <summary>
    '''Elimina la entidad
    ''' </summary>
    ''' <param name="HealthSuperParameters"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function DeleteHealthSuperParameters(ByVal HealthSuperParameters As HealthSuperParameters, ByVal audit As AuditMessage) As ActionResult(Of HealthSuperParameters)

End Interface
