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
Public Interface IContractCupsSubGroup

    ''' <summary>
    ''' Guarda o Actualiza un subgrupo
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveCupsSubgroup(CupsSubgroup As Domain.Entities.CupsSubgroup, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CupsSubgroup)

    ''' <summary>
    ''' Elimina un subgrupo
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteCupsSubgroup(CupsSubgroup As Domain.Entities.CupsSubgroup, audit As AuditMessage) As Domain.Base.Entities.ActionResult

    ''' <summary>
    ''' Obtiene un determinado subgrupo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetCupsSubgroup(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CupsSubgroup)

    ''' <summary>
    ''' Obtiene un determinado subgrupo
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetCupsSubgroupById(id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CupsSubgroup)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ChangeStateCupsSubgroup(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CupsSubgroup)

End Interface
