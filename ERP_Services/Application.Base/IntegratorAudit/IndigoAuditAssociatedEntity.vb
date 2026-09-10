'***********************************************************************
' Assembly         : Application.Base
' Author           : WalterSierra
' Created          : 20-04-2011
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-03-01
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
''' para entidades con el patron de agregado
''' </summary>
Public NotInheritable Class IndigoAuditAssociatedEntity(Of TSimpleEntity, TAssociatedEntity)

#Region "Fields"

    ''' <summary>
    ''' la variable para manejar la entidad principal
    ''' </summary>
    Private Shared _entity As TSimpleEntity
    ''' <summary>
    ''' la variable para manejar la entidad asociada
    ''' </summary>
    Private Shared _associatedEntity As TAssociatedEntity
    ''' <summary>
    ''' la variable con el nombre de la relacion
    ''' </summary>
    Private Shared _nameRelation As String
    ''' <summary>
    ''' la variable para manejar la entidad con los valores originales (usada cuando la accion es modificacion)
    ''' </summary>
    Private Shared _sourceEntity As TSimpleEntity
    ''' <summary>
    ''' la variable para manejar la entidad asociada con los valores originales (usada cuando la accion es modificacion)
    ''' </summary>
    Private Shared _associatedSourceEntity As TAssociatedEntity
    ''' <summary>
    ''' el mensaje de Auditoria
    ''' </summary>
    Private Shared _audit As Infrastructure.CrossCutting.Base.AuditMessage
    ''' <summary>
    ''' la accion puede ser insertar, modificar, eliminar e imprimir
    ''' </summary>
    Private Shared _action As Infrastructure.CrossCutting.Audit.Actions
    ''' <summary>
    ''' Código compañia
    ''' </summary>
    Private Shared _company As String

#End Region

#Region "Execution"

    ''' <summary>
    ''' ejecuta el proceso de auditoria en un segundo hilo con el patron BackgroundWorker
    ''' </summary>
    Public Shared Sub Execute(ByVal nameRelation As String, ByVal entity As TSimpleEntity, ByVal associatedEntity As TAssociatedEntity, ByVal audit As Infrastructure.CrossCutting.Base.AuditMessage, ByVal action As Infrastructure.CrossCutting.Audit.Actions, ByVal company As String, Optional ByVal sourceEntity As TSimpleEntity = Nothing, Optional ByVal associatedSourceEntity As TAssociatedEntity = Nothing)
        If String.IsNullOrEmpty(nameRelation) = True Then
            Throw New ArgumentNullException("nameRelation vacio")
        End If
        If entity Is Nothing Then
            Throw New ArgumentNullException("entity vacio")
        End If
        If associatedEntity Is Nothing Then
            Throw New ArgumentNullException("associatedEntity vacio")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit vacio")
        End If
        If action = Infrastructure.CrossCutting.Audit.Actions.Update Then
            If sourceEntity Is Nothing Then
                Throw New ArgumentNullException("sourceEntity vacio")
            End If
            If associatedSourceEntity Is Nothing Then
                Throw New ArgumentNullException("associatedSourceEntity vacio")
            End If
        End If
        'igualo valores
        _nameRelation = nameRelation
        _entity = entity
        _associatedEntity = associatedEntity
        _sourceEntity = sourceEntity
        _associatedSourceEntity = associatedSourceEntity
        _audit = audit
        _action = action
        _company = company
        Dim auditThread As New BackgroundWorker
        AddHandler auditThread.DoWork, AddressOf Run
        AddHandler auditThread.RunWorkerCompleted, AddressOf Completed
        auditThread.RunWorkerAsync()
    End Sub

#End Region

#Region "Threads"

    ''' <summary>
    ''' lanza el proceso de validacion de auditoria
    ''' </summary>
    ''' <param name="sender">el sender.</param>
    ''' <param name="e">variable que contiene datos del evento <see cref="System.ComponentModel.DoWorkEventArgs" />.</param>
    Private Shared Sub Run(ByVal sender As Object, ByVal e As DoWorkEventArgs)
        'ejecuto la validacion del sistema de auditoria
        If _action = Infrastructure.CrossCutting.Audit.Actions.Update Then
            'si es modificacion debo enviar la entidad con los valores originales
            IntegratorAudit.AuditAggregateEntity(Of TSimpleEntity, TAssociatedEntity)(_nameRelation, _entity, _associatedEntity, _audit, _action, _company, _sourceEntity, _associatedSourceEntity)
        Else
            IntegratorAudit.AuditAggregateEntity(Of TSimpleEntity, TAssociatedEntity)(_nameRelation, _entity, _associatedEntity, _audit, _action, _company)
        End If
    End Sub

    ''' <summary>
    ''' metodo para el manejo de las acciones luego de temrinado el proceso del segundo hilo
    ''' </summary>
    ''' <param name="sender">el sender.</param>
    ''' <param name="e">variable que contiene datos del evento <see cref="System.ComponentModel.RunWorkerCompletedEventArgs" />.</param>
    Private Shared Sub Completed(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)
        'aviso al sistema que termino el proceso
        If (e.Error IsNot Nothing) Then
            'informo del error en el repositorio de logging
            IndigoLogging.LogCriticalMessage(System.String.Format("Error al Enviar el Mensaje de Auditoria de la Entidad: {0}-{1} a la cola de MSMQ: {2}", _entity.ToString, _associatedEntity.ToString, e.Error.Message), Priority.VeryHigh)
        ElseIf e.Cancelled Then
            'informo en el repositorio de logging
            IndigoLogging.LogWarningMessage(System.String.Format("La Operacion de Envio de Mensaje de Auditoria de la Entidad: {0}-{1} a la Cola fue Cancelado", _entity.ToString, _associatedEntity.ToString), Priority.Normal)
        Else
            'informo en el repositorio de logging
            IndigoLogging.LogInformationMessage(System.String.Format("El Mensaje de Auditoria a sido enviado Correctamente a la cola de MSMQ {0}: {1}-{2}", [Enum].GetName(GetType(Infrastructure.CrossCutting.Audit.Actions), _action), _entity.ToString, _associatedEntity.ToString), Priority.Low)
        End If
    End Sub

#End Region

End Class
