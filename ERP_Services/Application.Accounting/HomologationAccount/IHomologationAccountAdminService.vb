'***********************************************************************
' Assembly         : Application.Accounting
' Author           : Carlos Mario Arias Rubiano
' Created          : 01/12/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region

Public Interface IHomologationAccountAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda o actualiza una homologación de cuentas
    ''' </summary>
    ''' <param name="audit">Mensaje de auditoria</param>
    ''' <returns>Resultado de la acción</returns>
    Function SaveHomologationAccount(ByVal ListHomologationAccount As List(Of HomologationAccount), ByVal audit As AuditMessage) As ActionResult

End Interface
