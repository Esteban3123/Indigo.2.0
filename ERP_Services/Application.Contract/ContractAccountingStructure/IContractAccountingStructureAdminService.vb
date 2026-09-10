'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/10/2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IContractAccountingStructureAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene un grupo de atencion por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetContractAccountingStructure(code As String, audit As AuditMessage) As ActionResult(Of ContractAccountingStructure)

    ''' <summary>
    ''' Obtiene un grupo de atencion por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetContractAccountingStructureById(id As Integer, audit As AuditMessage) As ActionResult(Of ContractAccountingStructure)

    ''' <summary>
    ''' Guarda o Actualiza un grupo de atencion
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveContractAccountingStructure(ByVal ContractAccountingStructure As ContractAccountingStructure, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of ContractAccountingStructure)

    ''' <summary>
    ''' Elimina una entidad cups
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteContractAccountingStructure(ByVal ContractAccountingStructure As ContractAccountingStructure, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChangeStateContractAccountingStructure(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of ContractAccountingStructure)

    ''' <summary>
    ''' Devuelve la lista de estructuras contables activas (Status=1) para llenar selectores en formularios
    ''' (ej: dropdown del Excel template de saldos iniciales).
    ''' </summary>
    Function GetActiveList(audit As AuditMessage) As ActionResult(Of List(Of ContractAccountingStructure))

    ''' <summary>
    ''' Resuelve un batch de Codes contra ContractAccountingStructure activas.
    ''' Usado para validar Excel de saldos iniciales y poblar las cuentas del AccountReceivable.
    ''' </summary>
    Function GetListByCodes(codes As List(Of String), audit As AuditMessage) As ActionResult(Of List(Of ContractAccountingStructure))

End Interface
