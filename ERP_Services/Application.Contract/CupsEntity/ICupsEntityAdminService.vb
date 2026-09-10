'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 25/09/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface ICupsEntityAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda o Actualiza una entidad cups
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveCupsEntity(ByVal CupsEntity As CUPSEntity, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of CUPSEntity)

    ''' <summary>
    ''' Elimina una entidad cups
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteCupsEntity(ByVal CupsEntity As CUPSEntity, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene una entidad cups por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetCupsEntity(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of CUPSEntity)

    ''' <summary>
    ''' Obtiene una entidad cups por id
    ''' </summary>
    ''' <returns></returns>
    Function GetCupsEntityById(ByVal id As Integer, ByVal audit As AuditMessage) As ActionResult(Of CUPSEntity)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChangeStateCupsEntity(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of CUPSEntity)

    ''' <summary>
    ''' Valilda la descripción antes de eliminarse
    ''' </summary>
    ''' <param name="CUPSEntityContractDescriptionId"></param>
    ''' <returns></returns>
    Function SP_ValidateDescriptionsInCrystal(CUPSEntityContractDescriptionId As Integer) As ActionResult(Of SP_ValidateDescriptionsInCrystal_Result)

    ''' <summary>
    ''' Valilda el cups cuando se agrega una descripción
    ''' </summary>
    ''' <param name="CUPSEntityCode"></param>
    ''' <returns></returns>
    Function SP_ValidateCUPSInCrystal(CUPSEntityCode As String) As ActionResult(Of SP_ValidateCUPSInCrystal_Result)

End Interface
