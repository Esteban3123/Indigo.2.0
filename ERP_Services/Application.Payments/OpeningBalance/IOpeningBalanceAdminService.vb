'***********************************************************************
' Assembly         : Application.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 31-03-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IOpeningBalanceAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda o Actualiza una dependencia
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveOpeningBalance(ByVal openingBalance As InitialBalance, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of InitialBalance)

    ''' <summary>
    ''' Guarda o Actualiza una dependencia
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function ConfirmOpeningBalance(ByVal openingBalance As InitialBalance, ByVal modeSaveAndConfirm As Boolean, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0, Optional ByVal _idOperativeUnit As Int32 = 0) As ActionResult(Of InitialBalance)

    ''' <summary>
    ''' Elimina una dependencia
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteOpeningBalance(ByVal openingBalance As InitialBalance, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene una determinada dependencia
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetOpeningBalance(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of InitialBalance)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChangeState(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of InitialBalance)

    ''' <summary>
    ''' metodo para validar la factura del copyAndPaste
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SetBillsInitialBalance(data As List(Of List(Of String))) As ActionResult(Of List(Of InitialBalanceAccountPayable))

    ''' <summary>
    ''' metodo para validar el anticipo del copyAndPaste
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SetAdvanceInitialBalance(data As List(Of List(Of String))) As ActionResult(Of List(Of InitialBalanceAdvance))

End Interface
