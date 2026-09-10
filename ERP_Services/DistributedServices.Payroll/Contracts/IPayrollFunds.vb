Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract()> _
Public Interface IPayrollFunds

#Region "Funds"

    ''' <summary>
    ''' Lista todos los Fondos 
    ''' </summary>
    ''' <returns>Lista de Fondos</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function ListAllFunds(session As SessionValues) As List(Of Fund)

    ''' <summary>
    ''' Obtiene un Fondo especifica
    ''' </summary>
    ''' <param name="code">Codigo de la profesion</param>
    ''' <returns>Fondos</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetFunds(ByVal code As String, session As SessionValues) As Fund

    ''' <summary>
    ''' Graba o Actualiza un Fondo
    ''' </summary>
    ''' <param name="Fund">Fondo</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SaveFunds(Funds As Domain.Payroll.Entities.Fund, session As SessionValues, idSequense As Int64, audit As AuditMessage) As ActionResult(Of Domain.Payroll.Entities.Fund)

    ''' <summary>
    ''' Elimina un Fondo
    ''' </summary>
    ''' <param name="Fund">Fondo</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function DeleteFunds(Funds As Domain.Payroll.Entities.Fund, session As SessionValues, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene un tercero atraves del nit
    ''' </summary>
    ''' <param name="nit">nit del tercero</param>
    ''' <returns>Tercero</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetThirdPartyByNit(ByVal nit As String, session As SessionValues) As ThirdParty


    ''' <summary>
    ''' Elimina un Nivel de Cargo
    ''' </summary>
    ''' <param name="code">Objeto de Nivel de Posición</param>
    ''' <param name="state">Objeto para la Auditoria</param>
    ''' <returns>Retorna un boleano: 1. Si es exitoso el borrado, 0. si no lo fue</returns>
    <OperationContract()>
    Function ChangeStateFund(ByVal code As String, ByVal state As Boolean, session As SessionValues, audit As AuditMessage) As ActionResult(Of Domain.Payroll.Entities.Fund)

#End Region

End Interface
