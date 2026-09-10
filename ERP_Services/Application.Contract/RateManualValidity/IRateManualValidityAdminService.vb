#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region

Public Interface IRateManualValidityAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene una vigencia de manual tarifario por id
    ''' </summary>
    ''' <returns></returns>
    Function GetRateManualValidityById(ByVal id As Integer, ByVal audit As AuditMessage) As ActionResult(Of RateManualValidity)

    ''' <summary>
    ''' Obtiene una vigencia de manual tarifario por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetRateManualValidity(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of RateManualValidity)

    ''' <summary>
    ''' Guarda o Actualiza una vigencia de manual tarifario
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveRateManualValidity(ByVal RateManualValidity As RateManualValidity, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of RateManualValidity)

    ''' <summary>
    ''' Cambia el estado de la vigencia de manual tarifario
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChangeStateRateManualValidity(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of RateManualValidity)

End Interface
