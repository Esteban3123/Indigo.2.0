'***********************************************************************
' Assembly         : Application.MixingStation
' Author           : Diego A. Roldan
' Created          : 2021-09-09
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface IRawMaterialDevolutionAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Consulta una devolucion de materia prima por código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Function GetRawMaterialDevolutionByCode(code As String, audit As AuditMessage) As ActionResult(Of RawMaterialDevolution)

    ''' <summary>
    ''' Guarda la devolucion de material prima
    ''' </summary>
    ''' <param name="rawMaterialDevolution"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function SaveRawMaterialDevolution(rawMaterialDevolution As RawMaterialDevolution, audit As AuditMessage, Optional idSecuence As Long = 0) As ActionResult(Of RawMaterialDevolution)

    ''' <summary>
    ''' Obtiene los detalles de devolucion de materia prima
    ''' </summary>
    ''' <param name="rawMatertialDevolutionId"></param>
    ''' <returns></returns>
    Function GetRawMaterialDevolutionDetailByRawMaterialDevolutionId(rawMatertialDevolutionId As Integer) As List(Of RawMaterialDevolutionDetail)

    ''' <summary>
    ''' Guarda y confirma una devolucion de materia prima
    ''' </summary>
    ''' <param name="rawMaterialDevolution"></param>
    ''' <param name="operatingUnitId"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSecuence"></param>
    ''' <returns></returns>
    Function SaveAndConfirmRawMaterialDevolution(rawMaterialDevolution As RawMaterialDevolution, operatingUnitId As Integer, audit As AuditMessage, Optional idSecuence As Long = 0) As ActionResult(Of RawMaterialDevolution)

    ''' <summary>
    ''' Confirma un documento
    ''' </summary>
    ''' <param name="rawMaterialDevolution"></param>
    ''' <param name="operatingUnitId"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function ConfirmRawMaterialDevolution(rawMaterialDevolution As RawMaterialDevolution, operatingUnitId As Integer, audit As AuditMessage) As ActionResult(Of RawMaterialDevolution)

    ''' <summary>
    ''' Anula un documento
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function AnnulateRawMaterialDevolution(code As String, audit As AuditMessage) As ActionResult(Of RawMaterialDevolution)
End Interface
