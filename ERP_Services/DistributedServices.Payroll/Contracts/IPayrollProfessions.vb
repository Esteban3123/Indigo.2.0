Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract()> _
Public Interface IPayrollProfessions

#Region "Professions"

    ''' <summary>
    ''' Lista todos las profesiones 
    ''' </summary>
    ''' <returns>Lista de profesiones</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function ListAllProfessions(session As SessionValues) As List(Of Profession)

    ''' <summary>
    ''' Obtiene una profesion especifica
    ''' </summary>
    ''' <param name="code">Codigo de la profesion</param>
    ''' <returns>Profesion</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetProfessions(ByVal code As String, session As SessionValues) As Profession

    ''' <summary>
    ''' Graba o Actualiza una profesion
    ''' </summary>
    ''' <param name="profession">Profesion</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SaveProfessions(profession As Domain.Payroll.Entities.Profession, session As SessionValues, idSequense As Int64, audit As AuditMessage) As ActionResult(Of Domain.Payroll.Entities.Profession)

    ''' <summary>
    ''' Elimina una profesion
    ''' </summary>
    ''' <param name="profession">Profesion</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function DeleteProfession(profession As Domain.Payroll.Entities.Profession, session As SessionValues, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Elimina un Nivel de Cargo
    ''' </summary>
    ''' <param name="code">Objeto de Nivel de Posición</param>
    ''' <param name="state">Objeto para la Auditoria</param>
    ''' <returns>Retorna un boleano: 1. Si es exitoso el borrado, 0. si no lo fue</returns>
    <OperationContract()>
    Function ChangeStateProfession(ByVal code As String, ByVal state As Boolean, session As SessionValues, audit As AuditMessage) As ActionResult(Of Profession)

#End Region

End Interface
