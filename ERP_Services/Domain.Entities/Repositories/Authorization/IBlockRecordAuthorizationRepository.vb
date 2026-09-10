'************************************************************
' Assembly         : Domain.Billing
' Author           : Carlos Mario Arias Rubiano
' Created          : 28/02/2020
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Entities

#End Region

Public Interface IBlockRecordAuthorizationRepository
    Inherits IRepository(Of BlockRecordAuthorization)

    ''' <summary>
    ''' Lista Todos los registros bloqueados
    ''' </summary>
    ''' <returns>Registros bloqueados</returns>
    Function ListAllBlockRecord() As List(Of BlockRecordAuthorization)

    ''' <summary>
    ''' Obtiene una registro de bloqueo según parametros
    ''' </summary>
    ''' <param name="IdForm">Id del formulario</param>
    ''' <param name="IdRecord">Id del registro</param>
    ''' <returns>Registro bloqueado</returns>
    Function GetBlockRecordByIdformAndIdRecord(ByVal IdForm As String, ByVal IdRecord As String, Optional tracking As Boolean = True) As BlockRecordAuthorization

    ''' <summary>
    ''' Lista Todos los registros bloqueados por Formulario
    ''' </summary>
    ''' <param name="IdForm">Id del formulario</param>
    ''' <returns>Registros bloqueados</returns>
    Function ListBlockRecordByIdForm(IdForm As String) As List(Of BlockRecordAuthorization)
End Interface
