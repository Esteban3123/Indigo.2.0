'***********************************************************************
' Assembly         : Infrastructure.CrossCutting.AuditData
' Author           : WalterSierra
' Created          : 13-11-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.CrossCutting.Base
#End Region

''' <summary>
''' Interface con los metodos para saber si se realiza auditoria o no
''' basado en reglas por maquina, ususario, documento
''' </summary>
Public Interface IAuditRules

    ''' <summary>
    ''' Valida las reglas para saber si se aplica auditoria o no
    ''' </summary>
    ''' <returns></returns>
    Function ValidateAuditRules(ByVal auditValues As AuditMessage) As Boolean

    ''' <summary>
    ''' Valida las reglas por maquina
    ''' </summary>
    ''' <returns></returns>
    Function ValidateMachineRules(ByVal auditValues As AuditMessage) As Boolean

    ''' <summary>
    ''' Valida las reglas por usuario
    ''' </summary>
    ''' <returns></returns>
    Function ValidateUserRules(ByVal auditValues As AuditMessage) As Boolean

    ''' <summary>
    ''' Validas las reglas por documento
    ''' </summary>
    ''' <returns></returns>
    Function ValidateDocumentRules(ByVal auditValues As AuditMessage) As Boolean

End Interface
