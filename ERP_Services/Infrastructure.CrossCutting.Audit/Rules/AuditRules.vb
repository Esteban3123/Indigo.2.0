'***********************************************************************
' Assembly         : Infrastructure.CrossCutting.AuditData
' Author           : Juan Diego Diaz
' Created          : 13-11-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.CrossCutting.Base
#End Region

''' <summary>
''' Clase que implementa las reglas de auditoria para las entidades 
''' </summary>
Public NotInheritable Class AuditRules

#Region "Members"

    ''' <summary>
    ''' Valiable para manejar el repositorio de Auditoria
    ''' </summary>
    Public Shared AuditRepository As AuditRepository


#End Region

#Region "IAuditRules Methods"

    ''' <summary>
    ''' Valida las reglas para saber si se aplica auditoria o no
    ''' </summary>
    ''' <param name="auditValues">Mensaje de Auditoria enviado a los servicios WCF</param>
    ''' <returns></returns>
    Public Shared Function ValidateAuditRules(ByVal auditValues As AuditMessage, ByVal company As String) As Boolean 'Implements IReglasAuditoria.ValidaReglasAuditoria
        If auditValues Is Nothing Then
            Throw New ArgumentNullException("ValoresAuditoria vacio")
        End If
        'primero verifico las reglas por maquina
        If ValidateMachineRules(auditValues, company) = True Then
            'realizo auditoria
            Return True
        End If
        'verifico las reglas del usuario
        If ValidateUserRules(auditValues, company) = True Then
            'realizo auditoria
            Return True
        End If
        'por ultimo valido las reglas por funcional o documento
        If ValidateDocumentRules(auditValues, company) = True Then
            'realizo auditoria
            Return True
        End If
        'no realizo auditoria
        Return False
    End Function

    ''' <summary>
    ''' Validas las reglas por documento
    ''' </summary>
    ''' <param name="auditValues">Mensaje de Auditoria enviado a los servicios WCF</param>
    ''' <returns></returns>
    Private Shared Function ValidateDocumentRules(ByVal auditValues As AuditMessage, ByVal company As String) As Boolean 'Implements IReglasAuditoria.ValidaReglasDocumento
        If auditValues Is Nothing Then
            Throw New ArgumentNullException("ValoresAuditoria vacio")
        End If
        AuditRepository = New AuditRepository(auditValues)
        Return AuditRepository.GetDocumentRules()
    End Function

    ''' <summary>
    ''' Valida las reglas por maquina
    ''' </summary>
    ''' <param name="auditValues">Mensaje de Auditoria enviado a los servicios WCF</param>
    ''' <returns></returns>
    Private Shared Function ValidateMachineRules(ByVal auditValues As AuditMessage, ByVal company As String) As Boolean 'Implements IReglasAuditoria.ValidaReglasMaquina
        If auditValues Is Nothing Then
            Throw New ArgumentNullException("ValoresAuditoria vacio")
        End If
        AuditRepository = New AuditRepository(auditValues)
        Return AuditRepository.GetMachineRules()
    End Function

    ''' <summary>
    ''' Valida las reglas por usuario
    ''' </summary>
    ''' <param name="auditValues">Mensaje de Auditoria enviado a los servicios WCF</param>
    ''' <returns></returns>
    Private Shared Function ValidateUserRules(ByVal auditValues As AuditMessage, ByVal company As String) As Boolean 'Implements IReglasAuditoria.ValidaReglasUsuario
        If auditValues Is Nothing Then
            Throw New ArgumentNullException("ValoresAuditoria vacio")
        End If
        AuditRepository = New AuditRepository(auditValues)
        Return AuditRepository.GetUserRules()
    End Function

#End Region

End Class
