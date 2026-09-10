'***********************************************************************
' Assembly         : Application.Base
' Author           : WalterSierra
' Created          : 20-04-2011
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-03-04
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.ComponentModel
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Logging
#End Region

''' <summary>
''' clase con los metodos compartidos para el llamado del proceso de auditoria en segundo hilo, 
''' para entidades sin el patron de agregado
''' </summary>
Public NotInheritable Class IndigoAuditSimpleEntity(Of TSimpleEntity)

    Private indigoEntity As TSimpleEntity
    Private indigoAudit As Infrastructure.CrossCutting.Base.AuditMessage
    Private indigoAction As Infrastructure.CrossCutting.Audit.Actions
    Private indigoSourceEntity As TSimpleEntity = Nothing
    Private indigoData As DataSet

    Public Sub New(ByVal entity As TSimpleEntity, ByVal audit As Infrastructure.CrossCutting.Base.AuditMessage, ByVal action As Infrastructure.CrossCutting.Audit.Actions, Optional ByVal sourceEntity As TSimpleEntity = Nothing)
        Return
        indigoEntity = entity
        indigoAudit = audit
        indigoAction = action
        indigoSourceEntity = sourceEntity
        'valido parametros
        If entity Is Nothing Then
            Throw New ArgumentNullException("entity", "Error en IndigoAuditSimpleEntity el parametro entity es nothing")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit", "Error en IndigoAuditSimpleEntity el parametro audit es nothing")
        End If
        If action = Infrastructure.CrossCutting.Audit.Actions.Update Then
            If sourceEntity Is Nothing Then
                Throw New ArgumentNullException("sourceEntity", "Error en IndigoAuditSimpleEntity el parametro sourceEntity es nothing")
            End If
        End If
        If action = Infrastructure.CrossCutting.Audit.Actions.Update Or action = Infrastructure.CrossCutting.Audit.Actions.Confirm _
                 Or action = Infrastructure.CrossCutting.Audit.Actions.Annular Or action = Infrastructure.CrossCutting.Audit.Actions.Disconfirm Then
            'se necesita la entidad Original
            indigoData = Infrastructure.CrossCutting.Audit.IndigoAudit.CreateAuditObject(Of TSimpleEntity)(entity, audit, action, sourceEntity)
        Else
            indigoData = Infrastructure.CrossCutting.Audit.IndigoAudit.CreateAuditObject(Of TSimpleEntity)(entity, audit, action)
        End If
    End Sub

    Public Async Sub Execute()
        Try
            Return
            Await Task.Factory.StartNew(Sub()
                                            IntegratorAudit.AuditSimpleEntity(Of TSimpleEntity)(indigoEntity, indigoData, indigoAudit, indigoAction)
                                        End Sub)
        Catch ex As Exception 'Si ocurre una excepcion es por que el mensaje es muy grande en este caso no se hace nada

        End Try
    End Sub

#Region "Execution"

    ' ''' <summary>
    ' ''' ejecuta el proceso de auditoria en un segundo hilo con el patron BackgroundWorker
    ' ''' </summary>
    'Public Shared Sub Execute(ByVal entity As TSimpleEntity, ByVal audit As Infrastructure.CrossCutting.Base.AuditMessage, ByVal action As Infrastructure.CrossCutting.Audit.Actions, Optional ByVal company As String = "01", Optional ByVal sourceEntity As TSimpleEntity = Nothing)
    '    'If entity Is Nothing Then
    '    '    Throw New ArgumentNullException("entity vacio")
    '    'End If
    '    'If audit Is Nothing Then
    '    '    Throw New ArgumentNullException("audit vacio")
    '    'End If
    '    'If action = Infrastructure.CrossCutting.Audit.Actions.Update Then
    '    '    If sourceEntity Is Nothing Then
    '    '        Throw New ArgumentNullException("sourceEntity vacio")
    '    '    End If
    '    'End If
    '    ''igualo valores
    '    '_entity = entity
    '    '_sourceEntity = sourceEntity
    '    '_audit = audit
    '    '_action = action
    '    '_company = company
    '    ''Dim auditThread As New BackgroundWorker
    '    ''AddHandler auditThread.DoWork, AddressOf Run
    '    ''AddHandler auditThread.RunWorkerCompleted, AddressOf Completed
    '    ''auditThread.RunWorkerAsync()
    '    'IndigoAuditSimpleEntity(Of TSimpleEntity).Run()
    'End Sub

#End Region

End Class
