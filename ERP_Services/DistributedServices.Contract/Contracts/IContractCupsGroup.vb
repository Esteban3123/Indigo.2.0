'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 02/04/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()> _
Public Interface IContractCupsGroup

    ''' <summary>
    ''' Guarda o Actualiza un grupo
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveCupsGroup(CupsGroup As Domain.Entities.CupsGroup, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CupsGroup)

    ''' <summary>
    ''' Elimina un grupo
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteCupsGroup(CupsGroup As Domain.Entities.CupsGroup, audit As AuditMessage) As Domain.Base.Entities.ActionResult

    ''' <summary>
    ''' Obtiene un determinado grupo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetCupsGroup(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CupsGroup)

    ''' <summary>
    ''' Obtiene un determinado grupo
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetCupsGroupById(id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CupsGroup)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ChangeStateCupsGroup(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CupsGroup)

End Interface
