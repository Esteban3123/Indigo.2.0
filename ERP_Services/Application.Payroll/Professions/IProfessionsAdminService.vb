'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 16-04-2011
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IProfessionsAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lista todos las profesiones 
    ''' </summary>
    ''' <returns>Lista de profesiones</returns>
    ''' <remarks></remarks>
    Function ListAllProfessions() As List(Of Profession)

    ''' <summary>
    ''' Obtiene una profesion especifica
    ''' </summary>
    ''' <param name="code">Codigo de la profesion</param>
    ''' <returns>Profesion</returns>
    ''' <remarks></remarks>
    Function GetProfessions(ByVal code As String) As Profession

    ''' <summary>
    ''' Graba o Actualiza una profesion
    ''' </summary>
    ''' <param name="profession">Profesion</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function SaveProfessions(ByVal profession As Profession, ByVal audit As AuditMessage, Optional ByVal idSequence As Int64 = 0) As ActionResult(Of Profession)

    ''' <summary>
    ''' Elimina una profesion
    ''' </summary>
    ''' <param name="profession">Profesion</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function DeleteProfession(ByVal profession As Profession, ByVal audit As AuditMessage) As ActionResult


    Function ChangeStateProfession(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of Profession)

End Interface
