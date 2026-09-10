'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 02/03/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()> _
Public Interface IPaymentsFilingUnit

    ''' <summary>
    ''' Guarda o Actualiza una unidad de radicacion
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveFilingUnit(FilingUnit As Domain.Entities.FilingUnit, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FilingUnit)

    ''' <summary>
    ''' Elimina una unidad de radicacion
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteFilingUnit(FilingUnit As Domain.Entities.FilingUnit, audit As AuditMessage) As Domain.Base.Entities.ActionResult

    ''' <summary>
    ''' Obtiene una unidad de radicacion
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetFilingUnit(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FilingUnit)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ChangeStateFilingUnit(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FilingUnit)

    ''' <summary>
    ''' Obtiene una unidad de radicacion
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetFilingUnitById(id As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FilingUnit)

    ''' <summary>
    ''' Obtiene las unidades operativas a las cuales tiene permiso un usuario, tambien carga las unidades funcionales padres
    ''' </summary>
    ''' <param name="userCode">Codigo del usuario</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetFilingUnitByUser(userCode As String) As ActionResult(Of List(Of FilingUnit))

    ''' <summary>
    ''' Obtiene las unidades de radicacion que tiene permiso un usuario
    ''' </summary>
    ''' <param name="userCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetFilingUnitByUserPermission(userCode As String) As ActionResult(Of List(Of FilingUnitUser))

End Interface
