'***********************************************************************
' Assembly         : Application.Budget
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/09/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IObligationModificationDetailAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene un detalle de una modificacion de obligacion por id
    ''' </summary>
    '''<param name="Id">Id del traslado</param>
    ''' <returns></returns>
    Function GetObligationModificationDetailById(Id As Integer, audit As AuditMessage) As ActionResult(Of ObligationModificationDetail)

    ''' <summary>
    ''' Guarda o Actualiza un detalle de una modificacion de obligacion
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveObligationModificationDetail(ByVal ObligationModificationDetail As ObligationModificationDetail, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of ObligationModificationDetail)

    ''' <summary>
    ''' Elimina un detalle de una modifciacion de obligacion
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteObligationModificationDetail(ByVal ObligationModificationDetail As ObligationModificationDetail, ByVal audit As AuditMessage) As ActionResult

End Interface
