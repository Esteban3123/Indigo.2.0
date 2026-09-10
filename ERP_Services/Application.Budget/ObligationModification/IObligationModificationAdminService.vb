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

Public Interface IObligationModificationAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene una modificacion de obligacion por codigo
    ''' </summary>
    '''<param name="Code">Código</param>
    ''' <returns></returns>
    Function GetObligationModification(Code As String, validityId As Integer, audit As AuditMessage) As ActionResult(Of ObligationModification)

    ''' <summary>
    ''' Obtiene una modificacion de obligacion por id
    ''' </summary>
    '''<param name="Id">Id del traslado</param>
    ''' <returns></returns>
    Function GetObligationModificationById(Id As Integer, audit As AuditMessage) As ActionResult(Of ObligationModification)

    ''' <summary>
    ''' Guarda o Actualiza una modificacion de obligacion
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveObligationModification(ObligationModification As ObligationModification, listObligationModificationDetailDelete As List(Of Integer), audit As AuditMessage) As ActionResult(Of ObligationModification)

    ''' <summary>
    ''' Elimina una modifciacion de obligacion
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteObligationModification(ByVal ObligationModification As ObligationModification, ByVal audit As AuditMessage) As ActionResult

End Interface
