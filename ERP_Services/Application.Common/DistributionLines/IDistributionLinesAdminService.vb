'***********************************************************************
' Assembly         : Application.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 02/04/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IDistributionLinesAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda o Actualiza linea de distirbucion
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveDistributionLines(ByVal distributionLines As DistributionLines, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of DistributionLines)

    ''' <summary>
    ''' Elimina linea de distribucion al proveedor
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteDistributionLines(ByVal distributionLines As DistributionLines, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene una linea de distribucion por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDistributionLinesById(id As Integer, ByVal audit As AuditMessage) As ActionResult(Of DistributionLines)

    ''' <summary>
    ''' Obtiene una linea de distribucion por codigo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDistributionLines(code As String, ByVal audit As AuditMessage) As ActionResult(Of DistributionLines)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChangeStateDistributionLines(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of DistributionLines)

End Interface
