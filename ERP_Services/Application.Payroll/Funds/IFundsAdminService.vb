'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 16-04-2013
'
' Last Modified By : Cristhian Mauricio Salazar
' Last Modified On : 07-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IFundsAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lista Todos los Fondos
    ''' </summary>
    ''' <returns>Lista de Fondos</returns>
    Function ListAllFunds() As List(Of Fund)

    ''' <summary>
    ''' Obtiene un Fondo Específico
    ''' </summary>
    ''' <param name="code">Código del fondo</param>
    ''' <returns>Un fondo</returns>
    Function GetFunds(ByVal code As String) As Fund

    ''' <summary>
    ''' Guarda Un Fondo
    ''' </summary>
    ''' <param name="Funds">Fondo a Guardar</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>True o False</returns>
    Function SaveFunds(ByVal Funds As Fund, ByVal audit As AuditMessage, Optional ByVal idSequence As Int64 = 0) As ActionResult(Of Fund)

    ''' <summary>
    ''' Elimina un Fondo
    ''' </summary>
    ''' <param name="Funds">Fondo a Elimiar</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>True o False</returns>
    Function DeleteFunds(ByVal Funds As Fund, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene un tercero apartir del nit
    ''' </summary>
    ''' <param name="nit">Nit del tercero</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetThirdPartyByNit(ByVal nit As String) As ThirdParty

    Function ChangeStateFund(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of Fund)

End Interface
