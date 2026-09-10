'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Carlos Mario Arias Rubiano
' Created          : 17/01/2017
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IConstitutionCashSmallerAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda un registro
    ''' </summary>
    Function SaveConstitutionCashSmaller(ByVal ConstitutionCashSmaller As ConstitutionCashSmaller, ByVal audit As AuditMessage, Optional ByVal idSequence As Int64 = 0) As ActionResult(Of ConstitutionCashSmaller)

    ''' <summary>
    ''' Confirma un registro
    ''' </summary>
    Function ConfirmConstitutionCashSmaller(ByVal ConstitutionCashSmaller As ConstitutionCashSmaller, ByVal audit As AuditMessage, Optional ByVal idSequence As Int64 = 0) As ActionResult(Of ConstitutionCashSmaller)

    ''' <summary>
    ''' Obtiene un registro por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetConstitutionCashSmaller(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of ConstitutionCashSmaller)

    ''' <summary>
    ''' Obtiene un registro por id
    ''' </summary>
    ''' <returns></returns>
    Function GetConstitutionCashSmallerById(id As Integer, tracking As Boolean, audit As AuditMessage) As ActionResult(Of ConstitutionCashSmaller)

End Interface
