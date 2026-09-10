Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IBankAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lista todos los bancos
    ''' </summary>
    ''' <returns>Lista de bancos</returns>
    Function ListAllBank() As List(Of Bank)

    ''' <summary>
    ''' Elimina un banco
    ''' </summary>
    ''' <param name="bank">Banco</param>
    ''' <returns></returns>
    Function DeleteBank(ByVal bank As Bank, ByVal audit As AuditMessage) As ActionMessageResult(Of Bank)

    ''' <summary>
    ''' Guarda o edita un banco
    ''' </summary>
    ''' <param name="bank">Banco</param>
    ''' <returns></returns>
    Function SaveBank(ByVal bank As Bank, ByVal audit As AuditMessage, Optional idSequence As Long = 0) As ActionResult(Of Bank)

    ''' <summary>
    ''' Obtiene un Banco especifico
    ''' </summary>
    ''' <param name="code">Código de el banco</param>
    ''' <returns> Banco</returns>
    Function GetBank(ByVal code As String) As Bank

    ''' <summary>
    ''' Obtiene un banco por Id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Function GetBankById(ByVal Id As Integer) As Bank

    Function UpdateStateBank(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of Bank)
End Interface
