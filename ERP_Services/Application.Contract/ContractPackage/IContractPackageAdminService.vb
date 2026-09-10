'***********************************************************************
' Assembly         : Application.Contract
' Author           : Giovanny Plazas Lozano
' Created          : 24/08/2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IContractPackageAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda o Actualiza una entidad cups
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveContractPackage(ByVal ContractPackage As ContractPackage, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of ContractPackage)

    ''' <summary>
    ''' Elimina una entidad cups
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteContractPackage(ByVal ContractPackage As ContractPackage, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene una entidad cups por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetContractPackage(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of ContractPackage)

    ''' <summary>
    ''' Obtiene una entidad cups por id
    ''' </summary>
    ''' <returns></returns>
    Function GetContractPackageById(ByVal id As Integer, ByVal audit As AuditMessage) As ActionResult(Of ContractPackage)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChangeStateContractPackage(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of ContractPackage)

    ''' <summary>
    '''  Copia y pega los elementos de excel de la rejilla
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SetCopyPasteOrImportFile(ByVal dataCopyPaste As List(Of List(Of String)), ByVal Name As String, ByVal audit As AuditMessage) As ActionResult(Of ContractPackage)

End Interface
