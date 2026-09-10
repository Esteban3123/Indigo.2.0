'***********************************************************************
' Assembly         : Application.MixingStation
' Author           : Andres Alarcon
' Created          : 21-02-2025
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

#End Region

Public Interface ILineClearanceCriteriaService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda una criterio de despeje de linea
    ''' </summary>
    ''' <param name="_lineClearanceCriteria">The identifier.</param>
    ''' <param name="audit">The identifier.</param>
    Function SaveLineClearanceCriteria(ByVal _lineClearanceCriteria As LineClearanceCriteria, ByVal audit As AuditMessage, Optional ByVal idSequence As Int64 = 0) As ActionResult(Of LineClearanceCriteria)

    ''' <summary>
    ''' Elimina un criterio de despeje de linea
    ''' </summary>
    ''' <param name="_lineClearanceCriteria">The identifier.</param>
    ''' <param name="audit">The identifier.</param>
    Function DeleteLineClearanceCriteria(ByVal _lineClearanceCriteria As LineClearanceCriteria, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Cambia el estado de un criterio de despeje de linea
    ''' </summary>
    ''' <param name="code">The identifier.</param>
    ''' <param name="state">The identifier.</param>
    ''' <param name="audit">The identifier.</param>
    Function UpdateStateLineClearanceCriteria(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of LineClearanceCriteria)

    ''' <summary>
    ''' Obtiene un criterio de despeje de linea mediante el codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="audit">The identifier.</param>
    ''' <returns></returns>
    Function GetLineClearanceCriteriaByCode(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of LineClearanceCriteria)

End Interface