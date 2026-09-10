'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Juan Diego Díaz
' Created          : 31-08-2018
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base.Entities
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface ISportPracticeAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Elimina una Practica Deportiva
    ''' </summary>
    ''' <param name="sportPractice">Practica Deportiva</param>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function DeleteSportPractice(ByVal sportPractice As SportPractice, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Almacena o Actualiza una Practica Deportiva
    ''' </summary>
    ''' <param name="sportPractice">Practica Deportiva</param>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function SaveSportPractice(ByVal sportPractice As SportPractice, ByVal audit As AuditMessage, ByVal Optional idSequense As Long = 0) As ActionResult(Of SportPractice)

    ''' <summary>
    ''' Obtiene una Practica Deportiva
    ''' </summary>
    ''' <param name="code">Código de la Practica Deportiva</param>
    ''' <returns>Practica Deportiva</returns>
    ''' <remarks></remarks>
    Function GetSportPractice(ByVal code As String, ByVal tracking As Boolean, ByVal audit As AuditMessage) As ActionResult(Of SportPractice)

    ''' <summary>
    ''' Obtiene una Practica Deportiva por ID
    ''' </summary>
    ''' <param name="ID">Id de la Practica Deportiva</param>
    ''' <returns>Practica Deportiva</returns>
    ''' <remarks></remarks>
    Function GetSportPracticeById(ByVal id As Integer, ByVal tracking As Boolean, ByVal audit As AuditMessage) As ActionResult(Of SportPractice)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChangeState(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of SportPractice)

End Interface
