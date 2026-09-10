'***********************************************************************
' Assembly         : Infrastructure.CrossCutting.AuditData
' Author           : Juan Diego Diaz M.
' Created          : 13-11-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.Reflection
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
#End Region

''' <summary>
''' clase con los metodos necesarios para la auditoria del sistema
''' </summary>
Public NotInheritable Class IndigoAudit

#Region "Log to Database"

    ''' <summary>
    ''' Graba los registros auditoria provenientes de la cola de MSQM.
    ''' </summary>
    ''' <param name="auditObject">el dataset leido desde la cola de MSQM.</param>
    Public Shared Sub SaveAuditLog(ByVal auditObject As DataSet)

        Using context As New GENESISSECURITYAuditEntities()
            context.CreateAuditData(auditObject.GetXml)
        End Using
    End Sub

#End Region

#Region "Create Objects"

    ''' <summary>
    ''' Variable para evitar que se creen ciclos infinitos
    ''' el primer parametro es la combinacion del nombre de la clase y el id
    ''' el segundo la definicion
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared dictionaryAudit As Dictionary(Of String, Tuple(Of DataRow, List(Of PropertyInfo)))
    ''' <summary>
    ''' Crea el objeto auditoria que se envia hacia la cola.
    ''' </summary>
    ''' <typeparam name="TEntity">el Tipo {T} de la entidad a crear.</typeparam>
    ''' <param name="currentEntity">la entidad con los valores actuales.</param>
    ''' <param name="auditValues">los valores de auditoria, como usuario, frontal, pc.</param>
    ''' <param name="action">la accion a realizar (enumeracion).</param>
    ''' <param name="sourceEntity">la entidad con los valores originales (antes de ser modificados).</param>
    ''' <returns></returns>
    Public Shared Function CreateAuditObject(Of TEntity)(ByVal currentEntity As TEntity, ByVal auditValues As AuditMessage, ByVal action As Actions, Optional ByVal sourceEntity As TEntity = Nothing) As DataSet
        If currentEntity Is Nothing Then
            Throw New ArgumentNullException("currentEntity vacio")
        End If
        If auditValues Is Nothing Then
            Throw New ArgumentNullException("auditValues vacio")
        End If
        If action = Actions.Update Then
            If sourceEntity Is Nothing Then
                Throw New ArgumentNullException("sourceEntity vacio")
            End If
        End If
        dictionaryAudit = New Dictionary(Of String, Tuple(Of DataRow, List(Of PropertyInfo)))()
        'creo el XML
        Dim dtHeader As New DataTable("Cabecera")
        dtHeader.Columns.Add("AUTO", GetType(Integer)).AutoIncrement = True
        dtHeader.Columns.Add("ACCION", GetType(Integer))
        dtHeader.Columns.Add("TABLAENTIDAD", GetType(String))
        dtHeader.Columns.Add("KEYTABLAENTIDAD", GetType(Integer))
        dtHeader.Columns.Add("IDAUDIT", GetType(Integer)).AllowDBNull = True
        dtHeader.Columns.Add("FECHA", GetType(DateTime))
        dtHeader.Columns.Add("USUARIO", GetType(String))
        dtHeader.Columns.Add("FRONTAL", GetType(String))
        dtHeader.Columns.Add("USUARIOWIN", GetType(String))
        dtHeader.Columns.Add("MAQUINA", GetType(String))
        dtHeader.Columns.Add("APLICACION", GetType(String)).AllowDBNull = True
        dtHeader.Columns.Add("COMPANY", GetType(String))
        dtHeader.Columns.Add("ISPARENT", GetType(Boolean))

        Dim dtDetail As New DataTable("Detalle")
        dtDetail.Columns.Add("AUTO", GetType(Integer)).AutoIncrement = True
        dtDetail.Columns.Add("AUTOAUDITORIA", GetType(String))
        dtDetail.Columns.Add("TABLAASOCIADA", GetType(String)).AllowDBNull = True
        dtDetail.Columns.Add("IDAUDITASOCIADA", GetType(Integer)).AllowDBNull = True
        dtDetail.Columns.Add("PROPIEDAD", GetType(String))
        dtDetail.Columns.Add("VALOR_ANTERIOR", GetType(String)).AllowDBNull = True
        dtDetail.Columns.Add("NUEVO_VALOR", GetType(String)).AllowDBNull = True
        InsertDataSet(dtHeader, dtDetail, currentEntity, Nothing, action, auditValues, sourceEntity, Nothing, True)
        Dim dsData As New DataSet("Auditoria")
        dsData.Tables.Add(dtHeader)
        dsData.Tables.Add(dtDetail)
        dictionaryAudit = New Dictionary(Of String, Tuple(Of DataRow, List(Of PropertyInfo)))()
        Return dsData

    End Function

    ''' <summary>
    ''' Funcion utilizada cuando en la entidad principal hay una relacion de uno a muchos
    ''' </summary>
    ''' <param name="dtHeader">Cabecera</param>
    ''' <param name="dtDetail">Detalle</param>
    ''' <param name="currentEntity">Entidad con datos modificados por el usuario</param>
    ''' <param name="idAuditParent">Id del registro padre</param>
    ''' <param name="action">Accion que se esta realizando</param>
    ''' <param name="auditValues">Valores de auditoria</param>
    ''' <param name="sourceEntity">Entidad sin modificar</param>
    ''' <param name="propertyExclude">Propiedad que se quiere excluir</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function InsertDataSet(ByRef dtHeader As DataTable, ByRef dtDetail As DataTable, currentEntity As Object, idAuditParent As Nullable(Of Integer), action As Actions, auditValues As AuditMessage, sourceEntity As Object, propertyExclude As String, isParent As Boolean) As Integer
        Dim keyAuditDictionary As String = currentEntity.GetType.Name & "-" & currentEntity.GetType.GetProperty("Id").GetValue(currentEntity, Nothing)
        If Not dictionaryAudit.ContainsKey(keyAuditDictionary) Then
            Dim drRowHeader As DataRow
            drRowHeader = dtHeader.NewRow
            With drRowHeader
                .Item("ACCION") = action
                .Item("TABLAENTIDAD") = currentEntity.GetType.Name
                .Item("KEYTABLAENTIDAD") = currentEntity.GetType.GetProperty("Id").GetValue(currentEntity, Nothing)
                If idAuditParent IsNot Nothing Then
                    .Item("IDAUDIT") = idAuditParent.Value
                End If
                '*****COMPANY******
                .Item("COMPANY") = auditValues.Company
                '******************
                .Item("FECHA") = DateTime.Now
                .Item("USUARIO") = auditValues.CodeUser
                .Item("FRONTAL") = auditValues.Functional
                .Item("USUARIOWIN") = auditValues.WindowsUser
                .Item("MAQUINA") = auditValues.ComputerName
                .Item("APLICACION") = DBNull.Value 'TODO: establecer valor
                .Item("ISPARENT") = isParent
            End With
            dtHeader.Rows.Add(drRowHeader)
            dictionaryAudit.Add(keyAuditDictionary, New Tuple(Of DataRow, List(Of PropertyInfo))(drRowHeader, currentEntity.GetType.GetProperties.ToList))
            Dim listRowDetail As List(Of DataRow) = New List(Of DataRow)()
            For Each prop As PropertyInfo In currentEntity.GetType.GetProperties
                'Varifico solo ingresen propiedades que me interesen
                If prop.Name.Contains("ObjectChangeTracker") = False And prop.Name.Contains("ControlConcurrencia") = False And prop.Name.Contains("EstadoEliminado") = False And prop.Name.Contains("EstadoSincronizacion") = False And prop.Name.Contains("OriginalValue") = False And prop.Name.Contains("ChangeTracker") = False Then
                    Dim drRowDetail As DataRow
                    'Verifico que la propiedad a excluir no sea nula y que si es la propiedad continue con el otro item
                    If propertyExclude IsNot Nothing AndAlso prop.Name.Contains(propertyExclude) Then
                        Continue For
                    End If
                    If prop.PropertyType.FullName.Contains("TrackableCollection") = True Then
                        Dim type = currentEntity.GetType.GetProperty(prop.Name).DeclaringType
                        Dim trackeable As IList = currentEntity.GetType.GetProperty(prop.Name).GetValue(currentEntity, Nothing)
                        Dim trackeableSource As IList
                        If sourceEntity IsNot Nothing Then
                            trackeableSource = sourceEntity.GetType.GetProperty(prop.Name).GetValue(sourceEntity, Nothing)
                        End If
                        For Each track In trackeable
                            Dim accionTmp As Actions = action
                            Dim trackSource As Object
                            If trackeableSource Is Nothing Then
                                trackSource = Nothing
                            Else
                                Dim queryTrack = From e In trackeableSource
                                            Where e.GetType.GetProperty("Id").GetValue(e) = track.GetType.GetProperty("Id").GetValue(track)
                                            Select e
                                If queryTrack.Count > 0 Then
                                    trackSource = queryTrack.SingleOrDefault()
                                Else
                                    trackSource = Nothing
                                    accionTmp = Actions.Insert
                                End If
                            End If
                            InsertDataSet(dtHeader, dtDetail, track, drRowHeader.Item("AUTO"), accionTmp, auditValues, trackSource, currentEntity.GetType.UnderlyingSystemType.UnderlyingSystemType.Name, False)
                        Next
                        ' Verifico los objetos eliminados los cuales se encuentran en el objeto Source
                        If trackeableSource IsNot Nothing Then
                            For Each trackDelete In trackeableSource
                                Dim queryDelete = From e In trackeable
                                            Where e.GetType.GetProperty("Id").GetValue(e) = trackDelete.GetType.GetProperty("Id").GetValue(trackDelete)
                                            Select e
                                If queryDelete.Count = 0 Then
                                    InsertDataSet(dtHeader, dtDetail, trackDelete, drRowHeader.Item("AUTO"), Actions.Delete, auditValues, Nothing, currentEntity.GetType.UnderlyingSystemType.UnderlyingSystemType.Name, False)
                                End If
                            Next
                        End If
                    ElseIf prop.PropertyType.FullName.Contains("Domain.") = True Then
                        Dim objProperty = currentEntity.GetType.GetProperty(prop.Name).GetValue(currentEntity, Nothing)
                        Dim objPropertySource As Object
                        If sourceEntity IsNot Nothing Then
                            objPropertySource = sourceEntity.GetType.GetProperty(prop.Name).GetValue(sourceEntity, Nothing)
                        End If
                        Dim idRow As Nullable(Of Integer) = Nothing
                        If objProperty IsNot Nothing Then
                            idRow = InsertDataSet(dtHeader, dtDetail, objProperty, Nothing, action, auditValues, objPropertySource, currentEntity.GetType.UnderlyingSystemType.UnderlyingSystemType.Name, False)
                        End If
                        'guardo los valores
                        drRowDetail = dtDetail.NewRow
                        With drRowDetail
                            .Item("AUTOAUDITORIA") = drRowHeader.Item("AUTO")
                            .Item("PROPIEDAD") = prop.Name
                            .Item("TABLAASOCIADA") = currentEntity.GetType.GetProperty(prop.Name).GetValue(currentEntity, Nothing)
                            If idRow IsNot Nothing Then
                                .Item("IDAUDITASOCIADA") = idRow.Value
                            End If
                        End With
                        listRowDetail.Add(drRowDetail)
                        dtDetail.Rows.Add(drRowDetail)
                    Else
                        'guardo los valores
                        drRowDetail = dtDetail.NewRow
                        With drRowDetail
                            .Item("AUTOAUDITORIA") = drRowHeader.Item("AUTO")
                            .Item("PROPIEDAD") = prop.Name
                            Select Case action
                                Case Actions.Insert
                                    .Item("NUEVO_VALOR") = currentEntity.GetType.GetProperty(prop.Name).GetValue(currentEntity, Nothing)
                                Case Actions.Update, Actions.Confirm, Actions.Annular, Actions.Disconfirm
                                    If sourceEntity IsNot Nothing Then
                                        .Item("VALOR_ANTERIOR") = sourceEntity.GetType.GetProperty(prop.Name).GetValue(sourceEntity, Nothing)
                                    Else
                                        .Item("VALOR_ANTERIOR") = Nothing
                                    End If
                                    .Item("NUEVO_VALOR") = currentEntity.GetType.GetProperty(prop.Name).GetValue(currentEntity, Nothing)
                                Case Actions.Delete, Actions.Print
                                    .Item("VALOR_ANTERIOR") = currentEntity.GetType.GetProperty(prop.Name).GetValue(currentEntity, Nothing)
                            End Select
                        End With
                        listRowDetail.Add(drRowDetail)
                        dtDetail.Rows.Add(drRowDetail)
                    End If
                End If
            Next
            Return drRowHeader.Item("AUTO")
        Else 'Cuando esta en el diccionarrio de auditoria

        End If
    End Function

    ''' <summary>
    ''' Crea el objeto auditoria que se envia hacia la cola.
    ''' </summary>
    ''' <typeparam name="TEntity">el Tipo {T} de la entidad a crear.</typeparam>
    ''' <param name="currentEntity">la entidad con los valores actuales.</param>
    ''' <param name="auditValues">los valores de auditoria, como usuario, frontal, pc.</param>
    ''' <param name="action">la accion a realizar (enumeracion).</param>
    ''' <param name="sourceEntity">la entidad con los valores originales (antes de ser modificados).</param>
    ''' <returns></returns>
    Public Shared Function CreateRelatedTableAuditObject(Of TEntity, TEntityAssociated)(ByVal relationshipName As String, ByVal currentEntity As TEntity, ByVal currentEntityAssociated As TEntityAssociated, ByVal auditValues As AuditMessage, ByVal action As Actions, Optional ByVal sourceEntity As TEntity = Nothing, Optional ByVal sourceEntityAssociated As TEntityAssociated = Nothing) As DataSet
        'valido parametros
        If String.IsNullOrEmpty(relationshipName) = True Then
            Throw New ArgumentNullException("relationshipName vacio")
        End If
        If currentEntity Is Nothing Then
            Throw New ArgumentNullException("currentEntity vacio")
        End If
        If currentEntityAssociated Is Nothing Then
            Throw New ArgumentNullException("currentEntityAssociated vacio")
        End If
        If auditValues Is Nothing Then
            Throw New ArgumentNullException("auditValues vacio")
        End If
        If action = Actions.Update Then
            If sourceEntity Is Nothing Then
                Throw New ArgumentNullException("sourceEntity vacio")
            End If
            If sourceEntityAssociated Is Nothing Then
                Throw New ArgumentNullException("sourceEntityAssociated vacio")
            End If
        End If
        'creo el XML
        Dim dtHeader As New DataTable("Cabecera")
        dtHeader.Columns.Add("AUTO", GetType(Integer)).AutoIncrement = True
        dtHeader.Columns.Add("ACCION", GetType(Integer))
        dtHeader.Columns.Add("TABLAENTIDAD", GetType(String))
        dtHeader.Columns.Add("KEYTABLAENTIDAD", GetType(Integer))
        dtHeader.Columns.Add("ASOCIACION", GetType(String)).AllowDBNull = True
        dtHeader.Columns.Add("FECHA", GetType(DateTime))
        dtHeader.Columns.Add("USUARIO", GetType(String))
        dtHeader.Columns.Add("FRONTAL", GetType(String))
        dtHeader.Columns.Add("USUARIOWIN", GetType(String))
        dtHeader.Columns.Add("MAQUINA", GetType(String))
        dtHeader.Columns.Add("APLICACION", GetType(String)).AllowDBNull = True
        dtHeader.Columns.Add("COMPANY", GetType(String)).AllowDBNull = True

        Dim dtDetail As New DataTable("Detalle")
        dtDetail.Columns.Add("AUTO", GetType(Integer)).AutoIncrement = True
        dtDetail.Columns.Add("AUTOAUDITORIA", GetType(String))
        dtDetail.Columns.Add("TABLAASOCIADA", GetType(String)).AllowDBNull = True
        dtDetail.Columns.Add("KEYTABLAASOCIADA", GetType(Integer)).AllowDBNull = True
        dtDetail.Columns.Add("PROPIEDAD", GetType(String))
        dtDetail.Columns.Add("VALOR_ANTERIOR", GetType(String)).AllowDBNull = True
        dtDetail.Columns.Add("NUEVO_VALOR", GetType(String)).AllowDBNull = True

        Dim drRowHeader As DataRow
        drRowHeader = dtHeader.NewRow
        With drRowHeader
            .Item("ACCION") = action
            .Item("TABLAENTIDAD") = currentEntity.GetType.Name
            .Item("KEYTABLAENTIDAD") = currentEntity.GetType.GetProperty("Autonumerico").GetValue(currentEntity, Nothing)
            .Item("ASOCIACION") = relationshipName
            .Item("FECHA") = DateTime.Now
            .Item("USUARIO") = auditValues.CodeUser
            .Item("FRONTAL") = auditValues.Functional
            .Item("USUARIOWIN") = auditValues.WindowsUser
            .Item("MAQUINA") = auditValues.ComputerName
            .Item("APLICACION") = DBNull.Value 'TODO: establecer valor
            .Item("COMPANY") = auditValues.Company
        End With
        dtHeader.Rows.Add(drRowHeader)

        Dim drRowDetail As DataRow
        'recorro la tabla principal
        For Each prop As PropertyInfo In currentEntity.GetType.GetProperties
            'excluyo los tipo sin interes
            If prop.PropertyType.Name.Contains("TrackableCollection") = False And prop.PropertyType.Name.Contains("ObjectChangeTracker") = False Then
                'excluyo los propiedades sin interes
                If prop.Name.Contains("ControlConcurrencia") = False And prop.Name.Contains("EstadoEliminado") = False And prop.Name.Contains("EstadoSincronizacion") = False Then
                    'guardo los valores
                    drRowDetail = dtDetail.NewRow
                    With drRowDetail
                        .Item("PROPIEDAD") = prop.Name
                        Select Case action
                            Case Actions.Insert
                                .Item("NUEVO_VALOR") = currentEntity.GetType.GetProperty(prop.Name).GetValue(currentEntity, Nothing)
                            Case Actions.Update
                                .Item("VALOR_ANTERIOR") = sourceEntity.GetType.GetProperty(prop.Name).GetValue(sourceEntity, Nothing)
                                .Item("NUEVO_VALOR") = currentEntity.GetType.GetProperty(prop.Name).GetValue(currentEntity, Nothing)
                            Case Actions.Delete, Actions.Print
                                .Item("VALOR_ANTERIOR") = currentEntity.GetType.GetProperty(prop.Name).GetValue(currentEntity, Nothing)
                        End Select
                    End With
                    dtDetail.Rows.Add(drRowDetail)
                End If
            End If
        Next

        'recorro la tabla asociada
        For i As Integer = 0 To CInt(currentEntityAssociated.GetType.GetProperty("Count").GetValue(currentEntityAssociated, Nothing)) - 1
            'por cada item
            For Each prop As PropertyInfo In currentEntityAssociated.GetType.GetProperty("Item").GetValue(currentEntityAssociated, {i}).GetType.GetProperties
                'excluyo los tipo sin interes
                If prop.PropertyType.Name.Contains("TrackableCollection") = False And prop.PropertyType.Name.Contains("ObjectChangeTracker") = False Then
                    'excluyo los propiedades sin interes
                    If prop.Name.Contains("ControlConcurrencia") = False And prop.Name.Contains("EstadoEliminado") = False And prop.Name.Contains("EstadoSincronizacion") = False Then
                        'guardo los valores
                        drRowDetail = dtDetail.NewRow
                        With drRowDetail
                            .Item("TABLAASOCIADA") = relationshipName
                            .Item("KEYTABLAASOCIADA") = currentEntityAssociated.GetType.GetProperty("Item").GetValue(currentEntityAssociated, {i}).GetType.GetProperty("Autonumerico").GetValue(currentEntityAssociated.GetType.GetProperty("Item").GetValue(currentEntityAssociated, {i}), Nothing)
                            .Item("PROPIEDAD") = prop.Name
                            Select Case action
                                Case Actions.Insert
                                    .Item("NUEVO_VALOR") = currentEntityAssociated.GetType.GetProperty("Item").GetValue(currentEntityAssociated, {i}).GetType.GetProperty(prop.Name).GetValue(currentEntityAssociated.GetType.GetProperty("Item").GetValue(currentEntityAssociated, {i}), Nothing)
                                Case Actions.Update
                                    If CInt(currentEntityAssociated.GetType.GetProperty("Item").GetValue(currentEntityAssociated, {i}).GetType.GetProperty("Autonumerico").GetValue(currentEntityAssociated.GetType.GetProperty("Item").GetValue(currentEntityAssociated, {i}), Nothing)) > 0 Then
                                        .Item("VALOR_ANTERIOR") = sourceEntityAssociated.GetType.GetProperty("Item").GetValue(sourceEntityAssociated, {i}).GetType.GetProperty(prop.Name).GetValue(sourceEntityAssociated.GetType.GetProperty("Item").GetValue(sourceEntityAssociated, {i}), Nothing)
                                    End If
                                    .Item("NUEVO_VALOR") = currentEntityAssociated.GetType.GetProperty("Item").GetValue(currentEntityAssociated, {i}).GetType.GetProperty(prop.Name).GetValue(currentEntityAssociated.GetType.GetProperty("Item").GetValue(currentEntityAssociated, {i}), Nothing)
                                Case Actions.Delete, Actions.Print
                                    .Item("VALOR_ANTERIOR") = currentEntityAssociated.GetType.GetProperty("Item").GetValue(currentEntityAssociated, {i}).GetType.GetProperty(prop.Name).GetValue(currentEntityAssociated.GetType.GetProperty("Item").GetValue(currentEntityAssociated, {i}), Nothing)
                            End Select
                        End With
                        dtDetail.Rows.Add(drRowDetail)
                    End If
                End If
            Next
        Next

        Dim dsData As New DataSet("Auditoria")
        dsData.Tables.Add(dtHeader)
        dsData.Tables.Add(dtDetail)

        Return dsData

    End Function

#End Region

End Class
