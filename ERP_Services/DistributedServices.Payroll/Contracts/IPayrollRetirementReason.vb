Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract()> _
Public Interface IPayrollRetirementReason

#Region "RetirementReason"

    ''' <summary>
    ''' Lista todos las Razones de Retiro
    ''' </summary>
    ''' <returns>Lista de Razones de Retiro</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function ListAllRetirementReason(session As SessionValues) As List(Of RetirementReason)

    ''' <summary>
    ''' Obtiene una Razón de Retiro especifica
    ''' </summary>
    ''' <param name="code">Codigo de la Razón de Retiro</param>
    ''' <returns>Razón de Retiro</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetRetirementReason(ByVal code As String, session As SessionValues) As RetirementReason

    ''' <summary>
    ''' Graba o Actualiza una Razón de Retiro
    ''' </summary>
    ''' <param name="RetirementReason">Razones de Retiro</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SaveRetirementReason(RetirementReason As Domain.Payroll.Entities.RetirementReason, session As SessionValues, idSequense As Int64, audit As AuditMessage) As ActionResult(Of Domain.Payroll.Entities.RetirementReason)

    ''' <summary>
    ''' Elimina una Razón de Retiro
    ''' </summary>
    ''' <param name="RetirementReason">Razón de Retiro</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function DeleteRetirementReason(RetirementReason As Domain.Payroll.Entities.RetirementReason, session As SessionValues, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Elimina un Nivel de Cargo
    ''' </summary>
    ''' <param name="code">Objeto de Nivel de Posición</param>
    ''' <param name="state">Objeto para la Auditoria</param>
    ''' <returns>Retorna un boleano: 1. Si es exitoso el borrado, 0. si no lo fue</returns>
    <OperationContract()>
    Function ChangeStateRetirementReason(ByVal code As String, ByVal state As Boolean, session As SessionValues, audit As AuditMessage) As ActionResult(Of Domain.Payroll.Entities.RetirementReason)
#End Region

End Interface
