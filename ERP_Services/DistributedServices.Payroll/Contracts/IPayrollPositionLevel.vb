Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract()> _
Public Interface IPayrollPositionLevel

#Region "Position Level"

    ''' <summary>
    ''' Obtiene todos los niveles de cargos
    ''' </summary>
    ''' <returns>Lista de niveles de cargos</returns>
    <OperationContract()>
    Function ListAllPositionLevel(session As SessionValues) As List(Of PositionLevel)

    ''' <summary>
    ''' Obtiene un nivel de cargo especifico
    ''' </summary>
    ''' <param name="code">Codigo del nivel del cargo</param>
    ''' <returns>El nivel de cargo del codigo que envien</returns>
    <OperationContract()>
    Function GetPositionLevel(ByVal code As String, session As SessionValues) As PositionLevel

    ''' <summary>
    ''' Graba un Nivel de Cargo
    ''' </summary>
    ''' <param name="PositionLevel">Objeto de Nivel de Posición</param>
    ''' <param name="audit">Objeto para la Auditoria</param>
    ''' <returns>Retorna un boleano: 1. Si es exitoso, 0. si no lo fue</returns>
    <OperationContract()>
    Function SavePositionLevel(ByVal PositionLevel As PositionLevel, session As SessionValues, idSequense As Int64, audit As AuditMessage) As ActionResult(Of PositionLevel)

    ''' <summary>
    ''' Elimina un Nivel de Cargo
    ''' </summary>
    ''' <param name="PositionLevel">Objeto de Nivel de Posición</param>
    ''' <param name="audit">Objeto para la Auditoria</param>
    ''' <returns>Retorna un boleano: 1. Si es exitoso el borrado, 0. si no lo fue</returns>
    <OperationContract()>
    Function DeletePositionLevel(ByVal PositionLevel As PositionLevel, session As SessionValues, audit As AuditMessage) As ActionResult


    ''' <summary>
    ''' Elimina un Nivel de Cargo
    ''' </summary>
    ''' <param name="code">Objeto de Nivel de Posición</param>
    ''' <param name="state">Objeto para la Auditoria</param>
    ''' <returns>Retorna un boleano: 1. Si es exitoso el borrado, 0. si no lo fue</returns>
    <OperationContract()>
    Function ChangeStatePositionLevel(ByVal code As String, ByVal state As Boolean, session As SessionValues, audit As AuditMessage) As ActionResult(Of PositionLevel)


#End Region

End Interface
