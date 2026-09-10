Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract()> _
Public Interface IPayrollEducationLevels

#Region "EducationLevels"

    ''' <summary>
    ''' Lista todos los niveles de educacion
    ''' </summary>
    ''' <returns>Lista de niveles de educacion</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ListAllEducationLevels(session As SessionValues) As List(Of EducationLevel)

    ''' <summary>
    ''' Obtiene un nivel de educacion especifico
    ''' </summary>
    ''' <returns>Nivel de educacion</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetEducationLevels(ByVal code As String, session As SessionValues) As EducationLevel

    ''' <summary>
    ''' Graba o actualiza el nivel de educacion
    ''' </summary>
    ''' <returns>True o False</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SaveEducationLevels(EducationLevel As Domain.Payroll.Entities.EducationLevel, session As SessionValues, idSequense As Int64, audit As AuditMessage) As ActionResult(Of Domain.Payroll.Entities.EducationLevel)

    ''' <summary>
    ''' Elimina un nivel de educacion
    ''' </summary>
    ''' <returns>True o False</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function DeleteEducationLevels(EducationLevel As Domain.Payroll.Entities.EducationLevel, session As SessionValues, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Elimina un Nivel de Cargo
    ''' </summary>
    ''' <param name="code">Objeto de Nivel de Posición</param>
    ''' <param name="state">Objeto para la Auditoria</param>
    ''' <returns>Retorna un boleano: 1. Si es exitoso el borrado, 0. si no lo fue</returns>
    <OperationContract()>
    Function ChangeStateEducationLevel(ByVal code As String, ByVal state As Boolean, session As SessionValues, audit As AuditMessage) As ActionResult(Of Domain.Payroll.Entities.EducationLevel)


#End Region

End Interface
