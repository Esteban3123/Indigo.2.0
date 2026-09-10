'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 27-04-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IKinshipAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lista todos los parentescos
    ''' </summary>
    ''' <returns>Parentesco</returns>
    ''' <remarks></remarks>
    Function ListAllKinship() As List(Of Kinship)

    ''' <summary>
    ''' Obtiene un parentesco en especifico
    ''' </summary>
    ''' <param name="code">Codigo del parentesco</param>
    ''' <returns>Parentesco</returns>
    ''' <remarks></remarks>
    Function GetKinship(ByVal code As String) As Kinship

    ''' <summary>
    ''' Graba o actualiza un parentesco
    ''' </summary>
    ''' <param name="kinship">Parentesco</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function SaveKinship(ByVal kinship As Kinship, ByVal audit As AuditMessage) As Boolean

    ''' <summary>
    ''' elimina un objeto parentesco
    ''' </summary>
    ''' <param name="kinship">Parentesco</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function DeleteKinship(ByVal kinship As Kinship, ByVal audit As AuditMessage) As ActionMessageResult(Of Kinship)

End Interface
