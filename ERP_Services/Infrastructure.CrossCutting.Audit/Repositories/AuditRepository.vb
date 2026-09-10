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
''' Metodos de consulta para confrontar reglas de auditoria
''' </summary>
Public Class AuditRepository

#Region "Members"

    ''' <summary>
    ''' Variable para el manejo del contexto
    ''' </summary>
    Dim _contex As GENESISSECURITYAuditEntities

    ''' <summary>
    ''' Variable para el manejo de los valores del mensaje de auditoria enviado a los servicios WCF
    ''' </summary>
    Dim _auditValues As AuditMessage

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase <see cref="AuditRepository" />.
    ''' </summary>
    Public Sub New(ByVal auditValues As AuditMessage)
        _contex = New GENESISSECURITYAuditEntities()
        _auditValues = auditValues
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Consultar las reglas de auditoria por nombre de pc
    ''' </summary>
    ''' <returns></returns>
    Function GetMachineRules() As Boolean
        Dim busqueda = From e In _contex.AuditRulesPC.Include("AuditRulesPCDocument").Include("AuditRulesPCUser")
                       Where e.PcName.Trim = _auditValues.ComputerName
                       Select e
        If busqueda.Count > 0 Then
            Dim entity As AuditRulesPC = busqueda.Single
            ' no filtra por ningun campo
            If entity.UserFiler = False And entity.DocumentFilter = False Then
                'aplico la regla
                Return True
            End If
            'filtra por los dos campos
            If entity.UserFiler = True And entity.DocumentFilter = True Then
                If entity.AuditRulesPCUser.Where(Function(e) e.Users.Trim = _auditValues.CodeUser).Count > 0 And entity.AuditRulesPCDocument.Where(Function(e) e.Documents.Trim = _auditValues.Functional).Count > 0 Then
                    'aplico la regla
                    Return True
                Else
                    ' no se cumplen ambas reglas
                    Return False
                End If
            End If
            'solo filtra por usuario
            If entity.UserFiler = True Then
                If entity.AuditRulesPCUser.Where(Function(e) e.Users.Trim = _auditValues.CodeUser).Count > 0 Then
                    'aplico la regla
                    Return True
                End If
            End If
            'solo filtra por documento
            If entity.DocumentFilter = True Then
                If entity.AuditRulesPCDocument.Where(Function(e) e.Documents.Trim = _auditValues.Functional).Count > 0 Then
                    'aplico la regla
                    Return True
                End If
            End If
            'no se aplico ninguna regla
            Return False
        Else
            'no existen reglas
            Return False
        End If
    End Function

    ''' <summary>
    ''' Consultar las reglas de auditoria por usuario
    ''' </summary>
    ''' <returns></returns>
    Function GetUserRules() As Boolean
        Dim busqueda = From e In _contex.AuditRulesUser.Include("AuditRulesUserPC").Include("AuditRulesUserDocument")
                      Where e.UserCode.Trim = _auditValues.CodeUser
                      Select e
        If busqueda.Count > 0 Then
            Dim entity As AuditRulesUser = busqueda.Single
            ' no filtra por ningun campo
            If entity.PcFilter = False And entity.DocumentFilter = False Then
                'aplico la regla
                Return True
            End If
            'filtra por los dos campos
            If entity.PcFilter = True And entity.DocumentFilter = True Then
                If entity.AuditRulesUserPC.Where(Function(e) e.PcName.Trim = _auditValues.ComputerName).Count > 0 And entity.AuditRulesUserDocument.Where(Function(e) e.Documents.Trim = _auditValues.Functional).Count > 0 Then
                    'aplico la regla
                    Return True
                Else
                    ' no se cumplen ambas reglas
                    Return False
                End If
            End If
            'solo filtra por pc
            If entity.PcFilter = True Then
                If entity.AuditRulesUserPC.Where(Function(e) e.PcName.Trim = _auditValues.ComputerName).Count > 0 Then
                    'aplico la regla
                    Return True
                End If
            End If
            'solo filtra por documento
            If entity.DocumentFilter = True Then
                If entity.AuditRulesUserDocument.Where(Function(e) e.Documents.Trim = _auditValues.Functional).Count > 0 Then
                    'aplico la regla
                    Return True
                End If
            End If
            'no se aplico ninguna regla
            Return False
        Else
            'no existen reglas
            Return False
        End If
    End Function

    ''' <summary>
    ''' Consultar las reglas de auditoria por codigo del documento
    ''' </summary>
    ''' <returns></returns>
    Function GetDocumentRules() As Boolean
        Dim busqueda = From e In _contex.AuditRulesDocument.Include("AuditRulesDocumentPC").Include("AuditRuledDocumentUser")
                     Where e.DocumentCode = _auditValues.Functional
                     Select e
        If busqueda.Count > 0 Then
            Dim entity As AuditRulesDocument = busqueda.Single
            ' no filtra por ningun campo
            If entity.PcFilter = False And entity.UserFilter = False Then
                'aplico la regla
                Return True
            End If
            'filtra por los dos campos
            If entity.PcFilter = True And entity.UserFilter = True Then
                If entity.AuditRulesDocumentPC.Where(Function(e) e.Pc.Trim = _auditValues.ComputerName).Count > 0 And entity.AuditRuledDocumentUser.Where(Function(e) e.Users.Trim = _auditValues.CodeUser).Count > 0 Then
                    'aplico la regla
                    Return True
                Else
                    ' no se cumplen ambas reglas
                    Return False
                End If
            End If
            'solo filtra por pc
            If entity.PcFilter = True Then
                If entity.AuditRulesDocumentPC.Where(Function(e) e.Pc = _auditValues.ComputerName).Count > 0 Then
                    'aplico la regla
                    Return True
                End If
            End If
            'solo filtra por usuario
            If entity.UserFilter = True Then
                If entity.AuditRuledDocumentUser.Where(Function(e) e.Users.Trim = _auditValues.CodeUser).Count > 0 Then
                    'aplico la regla
                    Return True
                End If
            End If
            'no se aplico ninguna regla
            Return False
        Else
            'no existen reglas
            Return False
        End If
    End Function

#End Region

End Class
