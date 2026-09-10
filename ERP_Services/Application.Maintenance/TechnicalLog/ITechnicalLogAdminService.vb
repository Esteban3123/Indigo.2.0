
#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region


''' <summary>
''' interface que especifica las metodos y funciones que manejara todas las acciones sobre registro tecnico
''' </summary>
''' <remarks></remarks>
Public Interface ITechnicalLogAdminService
    Inherits IDisposable
    ''' <summary>
    ''' funcion que sirve para listar todas los registros tecnicos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAllTechnicalLog() As List(Of TechnicalLog)

    ''' <summary>
    ''' funcion que sirve para eliminar un registro tecnico
    ''' </summary>
    ''' <param name="TechnicalLog"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DeleteTechnicalLog(ByVal TechnicalLog As TechnicalLog, ByVal audit As AuditMessage) As ActionResult


    ''' <summary>
    ''' funcion que sirve para guardar un registro tecnico
    ''' </summary>
    ''' <param name="TechnicalLog"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveTechnicalLog(ByVal TechnicalLog As TechnicalLog, ByVal audit As AuditMessage, Optional idSencuense As Int64 = 0) As ActionResult(Of TechnicalLog)

    ''' <summary>
    ''' funciona que sirve para listar un registro tecnico
    ''' </summary>
    ''' <param name="CodeTechnicalLog"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetTechnicalLog(ByVal CodeTechnicalLog As String, ByVal audit As AuditMessage) As TechnicalLog
    ''' <summary>
    ''' Metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    Function ChangeStateTechnicalLog(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of TechnicalLog)
End Interface
