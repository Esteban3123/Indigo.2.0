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
Imports System.Configuration
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Msmq
Imports Infrastructure.CrossCutting.Audit
Imports Infrastructure.CrossCutting.MessageStore

#End Region

''' <summary>
''' clase para la integracion de la capa d eaplicacion con la capa transaversal de auditoria
''' </summary>
Public NotInheritable Class IntegratorAudit

    ''' <summary>
    ''' Disparador del Proceso de Auditoria para entidades sin el patron agregado
    ''' </summary>
    ''' <typeparam name="TEntity">El Tipo de la entidad.</typeparam>
    ''' <param name="entity">el objeto de la entidad.</param>
    ''' <param name="audit">Mensaje de valores de auditoria.</param>
    ''' <param name="action">La Accion Realizada, Insertar,modificar,eliminar e imprimir</param>
    Public Shared Async Sub AuditSimpleEntity(Of TEntity)(ByVal entity As TEntity, ByVal dsData As DataSet, ByVal audit As Infrastructure.CrossCutting.Base.AuditMessage, ByVal action As Infrastructure.CrossCutting.Audit.Actions)
        'pregunto si realiza auditoria
        If CBool(ConfigurationManager.AppSettings("Auditoria")) = True Then
            Using messStore As New MessageStore(New AuditingConfigStore())
                Dim mess As New Infrastructure.CrossCutting.MessageStore.Message(String.Format("Audit {0}: {1}", [Enum].GetName(GetType(Actions), action), entity.GetType().Name), dsData)
                Await messStore.AddMessageAsync(mess)
            End Using
            'End If
        End If
    End Sub

    ''' <summary>
    ''' Obtiene el numero total de impresiones y exportación de una entidad
    ''' </summary>
    ''' <param name="entityName">Nombre de la entidad</param>
    ''' <param name="entityKey">Id de la entidad</param>
    ''' <returns>Número total de impresiones</returns>
    Public Function GetTotalPrint(ByVal entityName As String, ByVal entityKey As Integer, ByVal company As String) As Integer
        Try
            Dim rep As New Infrastructure.Data.SecurityRepository.BasicAuditRepository(New Infrastructure.Data.SecurityRepository.GenesisEntities())
            Dim res = rep.GetTotalPrint(entityName, entityKey, company)
            Return res
        Catch ex As Exception
            Infrastructure.CrossCutting.Exceptions.IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return 0
        End Try
    End Function

    ''' <summary>
    ''' Disparador del Proceso de Auditoria para entidades sin el patron agregado
    ''' </summary>
    ''' <typeparam name="TEntity">El Tipo de la entidad.</typeparam>
    ''' <param name="entity">el objeto de la entidad.</param>
    ''' <param name="audit">Mensaje de valores de auditoria.</param>
    ''' <param name="action">La Accion Realizada, Insertar,modificar,eliminar e imprimir</param>
    ''' <param name="sourceEntity">Si la Accion es Modificar, se debe envia la Entidad con los Valores Originales</param>
    Public Shared Sub AuditAggregateEntity(Of TEntity, TAssociatedEntity)(ByVal nameRelation As String, ByVal entity As TEntity, ByVal associatedEntity As TAssociatedEntity, ByVal audit As Infrastructure.CrossCutting.Base.AuditMessage, ByVal action As Infrastructure.CrossCutting.Audit.Actions, ByVal company As String, Optional ByVal sourceEntity As TEntity = Nothing, Optional ByVal associatedSourceEntity As TAssociatedEntity = Nothing)
        'valido parametros
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
        'pregunto si realiza auditoria
        If CBool(ConfigurationManager.AppSettings("Auditoria")) = True Then
            'verifico si de acuerdo a las reglas debo auditar el proceso
            If AuditRules.ValidateAuditRules(audit, company) = True Then
                'existen reglas debo auditar el proceso
                Dim dsData As New DataSet
                If action = Actions.Update Then
                    'se necesita la entidad Original
                    dsData = IndigoAudit.CreateRelatedTableAuditObject(Of TEntity, TAssociatedEntity)(nameRelation, entity, associatedEntity, audit, action, sourceEntity, associatedSourceEntity)
                Else
                    dsData = IndigoAudit.CreateRelatedTableAuditObject(Of TEntity, TAssociatedEntity)(nameRelation, entity, associatedEntity, audit, action)
                End If

                If ConfigurationManager.AppSettings("Container") IsNot Nothing Then
                    IndigoMsmq.PrivateContainerPath = ConfigurationManager.AppSettings("Container").ToString().Trim()
                End If
                'enviar el mensaje a la cola de MSMQ
                IndigoMsmq.SendPrivateMessage(System.String.Format("Auditoria {0}: {1}-{2}", [Enum].GetName(GetType(Actions), action), entity.ToString, associatedEntity.ToString), dsData)
            End If
        End If
    End Sub

End Class
