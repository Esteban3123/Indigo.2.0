'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 17/10/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IRateManualDetailAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda o Actualiza un manual de servicios
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveRateManualDetail(ByVal RateManualDetail As RateManualDetail, ByVal audit As AuditMessage) As ActionResult(Of RateManualDetail)

    ''' <summary>
    ''' Guarda o actualiza el listado de manual de servicios
    ''' </summary>
    ''' <param name="ListRateManualDetail"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveListRateManualDetail(ByVal ListRateManualDetail As List(Of RateManualDetail), ByVal ListDeleteRateManualDetail As List(Of RateManualDetail), ByVal audit As AuditMessage) As ActionResult(Of List(Of RateManualDetail))

    ''' <summary>
    ''' Elimina un detalle de manual tarifario
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteRateManualDetail(ByVal RateManualDetail As RateManualDetail, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene un detalle de manual tarifario por id
    ''' </summary>
    ''' <returns></returns>
    Function GetRateManualDetailById(ByVal id As Integer, ByVal audit As AuditMessage) As ActionResult(Of RateManualDetail)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChangeStateRateManualDetail(ByVal id As Integer, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of RateManualDetail)

End Interface
