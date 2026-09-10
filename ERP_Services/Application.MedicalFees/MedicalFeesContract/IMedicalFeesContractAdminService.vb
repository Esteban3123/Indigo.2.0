'***********************************************************************
' Assembly         : Application.MedicalFees
' Author           : Carlos Mario Arias Rubiano
' Created          : 11/12/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IMedicalFeesContractAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda o Actualiza la entidad
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveMedicalFeesContractAsync(ByVal MedicalFeesContract As MedicalFeesContract, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As Task(Of ActionResult(Of MedicalFeesContract))

    ''' <summary>
    ''' Elimina la entidad
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteMedicalFeesContract(ByVal MedicalFeesContract As MedicalFeesContract, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene la entidad por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetMedicalFeesContract(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of MedicalFeesContract)

    ''' <summary>
    ''' Obtiene la entidad por id
    ''' </summary>
    ''' <returns></returns>
    Function GetMedicalFeesContractById(ByVal id As Integer, ByVal audit As AuditMessage) As ActionResult(Of MedicalFeesContract)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChangeStateMedicalFeesContractAsync(ByVal code As String, ByVal state As Integer, ByVal audit As AuditMessage) As Task(Of ActionResult(Of MedicalFeesContract))

End Interface
