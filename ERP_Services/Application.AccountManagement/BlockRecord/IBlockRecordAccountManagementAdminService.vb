'***********************************************************************
' Assembly         : Application.AccountManagement
' Author           : Felix Camilo Salazar Roldan
' Created          : 13-12-2024
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "imports"
Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
#End Region

Public Interface IBlockRecordAccountManagementAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lista Todos los registros bloqueados
    ''' </summary>
    ''' <returns>Registros bloqueados</returns>
    Function ListAllBlockRecord() As List(Of BlockRecordAccountManagement)

    ''' <summary>
    ''' Obtiene una registro de bloqueo según parametros
    ''' </summary>
    ''' <param name="IdForm">Id del formulario</param>
    ''' <param name="IdRecord">Id del registro</param>
    ''' <returns>Registro bloqueado</returns>
    Function GetBlockRecordByIdformAndIdRecord(ByVal IdForm As String, ByVal IdRecord As String) As BlockRecordAccountManagement

    ''' <summary>
    ''' Almacena o Actualiza registro bloqueado
    ''' </summary>
    ''' <param name="blockRecord">Registro bloqueado</param>
    ''' <returns>ActionResult</returns>
    Function SaveBlockRecord(ByVal blockRecord As BlockRecordAccountManagement) As ActionResult(Of BlockRecordAccountManagement)

    ''' <summary>
    ''' Elimina una registro bloqueado
    ''' </summary>
    ''' <param name="blockRecord">Registro bloqueado</param>
    ''' <returns>ActionResult</returns>
    Function DeleteBlockRecord(ByVal blockRecord As BlockRecordAccountManagement) As ActionResult
End Interface

