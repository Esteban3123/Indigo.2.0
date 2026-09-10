'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 06-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Public Interface IPositionAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lista todos los cargos
    ''' </summary>
    ''' <returns>Lista de cargos</returns>
    Function ListAllPosition() As List(Of Position)

    ''' <summary>
    ''' Elimina un cargo
    ''' </summary>
    ''' <param name="position">Cargo</param>
    ''' <returns></returns>
    Function DeletePosition(ByVal position As Position, ByVal audit As AuditMessage) As ActionMessageResult(Of Position)

    ''' <summary>
    ''' Guarda o edita un cargo
    ''' </summary>
    ''' <param name="position">Cargo</param>
    ''' <returns></returns>
    Function SavePosition(ByVal position As Position, ByVal audit As AuditMessage) As Boolean

    ''' <summary>
    ''' Obtiene un cargo
    ''' </summary>
    ''' <param name="code">Código del cargo</param>
    ''' <returns>Cargo</returns>
    Function GetPosition(ByVal code As String) As Position

    ''' <summary>
    ''' Cambiar el Estado del Cargo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChangeState(code As String, state As Boolean, audit As AuditMessage) As Boolean

End Interface
