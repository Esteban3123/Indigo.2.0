'***********************************************************************
' Assembly         : Application.MixingStation
' Author           : Ruben Dario Castañeda Giraldo
' Created          : 06-05-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface ICMCenterAttention
    Inherits IDisposable

    ''' <summary>
    ''' Lista todos los parámetros de configuración de central de mezclas
    ''' </summary>
    ''' <returns>Lista de turnos</returns>
    Function ListAllCMCenterAttention(audit As AuditMessage) As List(Of CMCenterAttention)
    ''' <summary>
    ''' Guarda la asociacion entre el centro de atencion y la central de mezclas
    ''' </summary>
    Function SaveCMCenterAttention(ByVal cmCenterAttention As CMCenterAttention) As ActionResult(Of CMCenterAttention)

    ''' <summary>
    ''' Obtiene los parámetros de central de mezclas por Id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    Function GetCMCenterAttention(ByVal id As String) As ActionResult(Of CMCenterAttention)

    ''' <summary>
    ''' Actualiza los parámetros de configuración de central de mezclas
    ''' </summary>
    ''' <param name="state">The identifier.</param>
    ''' <param name="audit">The identifier.</param>
    Function UpdateStateCMCenterAttention(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of CMCenterAttention)
    ''' <summary>
    ''' Obtiene los tipos de dosis unitarias
    ''' </summary>
    ''' <param name="Id_MIxingStation">The identifier.</param>
    ''' <param name="audit">The identifier.</param>
    Function ListAllCMMixingProducitonLine(ByVal Id_MIxingStation As Integer, ByVal audit As AuditMessage) As List(Of Tuple(Of Integer, String))
End Interface
