'***********************************************************************
' Assembly         : Application.MixingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 09/02/2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities


Public Interface IRequestUnitDoseExternalCareCenterAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda o Actualiza un grupo
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveRequestUnitDoseExternalCareCenter(ByVal RequestUnitDoseExternalCareCenter As RequestUnitDoseExternalCareCenter, ByVal audit As AuditMessage, operatingUnitId As Integer, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of RequestUnitDoseExternalCareCenter)

    ''' <summary>
    ''' Obtiene un grupo por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetRequestUnitDoseExternalCareCenter(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of RequestUnitDoseExternalCareCenter)

    ''' <summary>
    ''' Obtiene un grupo uvr por id
    ''' </summary>
    ''' <returns></returns>
    Function GetRequestUnitDoseExternalCareCenterById(ByVal id As Integer) As ActionResult(Of RequestUnitDoseExternalCareCenter)

End Interface
