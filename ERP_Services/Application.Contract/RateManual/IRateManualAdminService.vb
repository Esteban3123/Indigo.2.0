'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 09/10/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IRateManualAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda o Actualiza un manual tarifario
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveRateManual(ByVal RateManual As RateManual, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of RateManual)

    ''' <summary>
    ''' Elimina un manual tarifario
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteRateManual(ByVal RateManual As RateManual, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene un manual tarifario por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetRateManual(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of RateManual)

    ''' <summary>
    ''' Obtiene un manual tarifario por id
    ''' </summary>
    ''' <returns></returns>
    Function GetRateManualById(ByVal id As Integer, ByVal audit As AuditMessage) As ActionResult(Of RateManual)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChangeStateRateManual(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of RateManual)

    ''' <summary>
    ''' metodo para pegar en la rejilla de manual de tarifas
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CopyAndPasteRateManual(data As List(Of List(Of String)), ServiceManual As Integer) As ActionResult(Of List(Of RateManualDetail), List(Of Tuple(Of String, Integer)))

    ''' <summary>
    ''' metodo para pegar en la rejilla de manual de tarifas
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CopyAndPasteRateManualSurgical(data As List(Of List(Of String)), ServiceManual As Integer) As ActionResult(Of List(Of RateManualDetailSurgical), List(Of Tuple(Of String, Integer)))

End Interface
