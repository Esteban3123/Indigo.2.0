#Region "Imports"
Imports Domain.Entities
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region

Public Interface IOperatingUnitAdminService
    Inherits IDisposable

    ''' <summary>
    ''' funcion que obtiene una unidad operativa por codigo
    ''' </summary>
    ''' <param name="code">codigo de la unidad operativa</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetOperatingUnitByCode(code As String, audit As AuditMessage) As ActionResult(Of OperatingUnit)

    ''' <summary>
    ''' funcion que obtiene una unidad operativa por id
    ''' </summary>
    ''' <param name="id">id de la unidad operativa</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetOperatingUnitById(id As Integer, asudit As AuditMessage) As ActionResult(Of OperatingUnit)

    ''' <summary>
    ''' Funcion que lista todas las unidades operativas
    ''' </summary>
    ''' <returns>Lista de unidades operativos</returns>
    ''' <remarks></remarks>
    Function ListAllOperatingUnit() As List(Of OperatingUnit)

    ''' <summary>
    ''' Guarda o actualiza una unidad operativa
    ''' </summary>
    ''' <param name="operatingUnit"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveOperatingUnit(operatingUnit As OperatingUnit, audit As AuditMessage) As ActionResult(Of OperatingUnit)

    ''' <summary>
    ''' Elimina una unidad operativa
    ''' </summary>
    ''' <param name="operatingUnit"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DeleteOperatingUnit(operatingUnit As OperatingUnit, audit As AuditMessage) As ActionResult

End Interface
