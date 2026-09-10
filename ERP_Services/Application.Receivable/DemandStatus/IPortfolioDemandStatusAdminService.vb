'***********************************************************************
' Assembly         : Application.Portfolio
' Author           : Hector Rodriguez Rubiano
' Created          : 09-08-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface IPortfolioDemandStatusAdminService
    Inherits IDisposable

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="_DemandStatus"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function DeleteDemandStatus(_DemandStatus As DemandStatus, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    Function GetDemandStatus(Code As String) As DemandStatus

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Function ListAllDemandStatus() As List(Of DemandStatus)

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="_DemandStatus"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    Function SaveDemandStatus(_DemandStatus As DemandStatus, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of DemandStatus)

    ''' <summary>
    ''' metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DemandStatusChangeState(code As String, state As Boolean, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.DemandStatus)

End Interface
