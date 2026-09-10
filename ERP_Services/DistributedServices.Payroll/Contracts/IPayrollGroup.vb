Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
<ServiceContract()> _
Public Interface IPayrollGroup

#Region "Group"

    ''' <summary>
    ''' Lista de Grupos
    ''' </summary>
    ''' <returns>Lista de Grupos</returns>
    <OperationContract()>
    Function ListAllGroups(session As SessionValues) As List(Of Group)

    ''' <summary>
    ''' Elimina un Grupo
    ''' </summary>
    ''' <param name="Group">Grupo</param>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteGroup(ByVal Group As Group, session As SessionValues) As ActionMessageResult(Of Group)

    ''' <summary>
    ''' Guarda un Grupo
    ''' </summary>
    ''' <param name="Group">Grupo</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveGroup(ByVal Group As Group, session As SessionValues) As Boolean

    ''' <summary>
    ''' Obtiene un Grupo
    ''' </summary>
    ''' <param name="code">Código de Grupo</param>
    ''' <returns> Grupo</returns>
    <OperationContract()>
    Function GetGroup(ByVal code As String, session As SessionValues) As Group

    ''' <summary>
    ''' Obtiene un Grupo por id
    ''' </summary>
    ''' <param name="id">Código de Grupo</param>
    ''' <returns> Grupo</returns>
    <OperationContract()>
    Function GetGroupById(ByVal id As String, session As SessionValues) As Group

    ''' <summary>
    ''' Obtiene unos grupos filtrado por empresa
    ''' </summary>
    ''' <param name="companyId">Id de la empresa</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetGroupsByCompanyId(ByVal companyId As String, session As SessionValues) As List(Of Group)

    ''' <summary>
    ''' Grupo y sus correspondientes liquidaciones
    ''' </summary>
    ''' <param name="GroupId">Id del Grupo</param>
    ''' <returns>Grupo</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetGroupLiquidationById(ByVal GroupId As String, session As SessionValues) As List(Of Group)

    ''' <summary>
    ''' Cambiar el Estado del Grupo de Nómina
    ''' </summary>
    ''' <param name="code">Código</param>
    ''' <param name="state">Estado</param>
    ''' <param name="audit">audit</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GroupChangeState(code As String, state As Boolean, session As SessionValues) As Boolean

    ''' <summary>
    ''' Lista de Grupos
    ''' </summary>
    ''' <returns>Lista de Grupos</returns>
    <OperationContract()>
    Function ListGroupsByStatus(Status As Boolean, session As SessionValues) As List(Of Group)

#End Region

End Interface
