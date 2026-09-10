'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 30/09/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Core
Imports Infrastructure.CrossCutting.Resources
Imports System.Transactions
Imports System.Data.SqlClient
Imports System.Text

Public Class ProcedureTemplateAdminService
    Implements IProcedureTemplateAdminService

    Private _procedureTemplateRepository As IProcedureTemplateRepository
    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDRepository As ISequenseContractDRepository
    ''' <summary>
    ''' Repositorio de detalles de cubrimientos
    ''' </summary>
    ''' <remarks></remarks>
    Private _procedureCupsRepository As IProcedureCupsRepository

    Public Sub New(procedureTemplateRepository As IProcedureTemplateRepository, secuenseDRepository As ISequenseContractDRepository,
                   procedureCupsRepository As IProcedureCupsRepository)
        If procedureTemplateRepository Is Nothing Then
            Throw New ArgumentNullException("procedureTemplateRepository")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        If procedureCupsRepository Is Nothing Then
            Throw New ArgumentNullException("procedureCupsRepository")
        End If
        _procedureTemplateRepository = procedureTemplateRepository
        _secuenseDRepository = secuenseDRepository
        _procedureCupsRepository = procedureCupsRepository
    End Sub


    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function ChangeStateProcedureTemplate(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of ProcedureTemplate) Implements IProcedureTemplateAdminService.ChangeStateProcedureTemplate
        Dim ProcedureTemplate As ProcedureTemplate = _procedureTemplateRepository.GetProcedureTemplate(code)
        ProcedureTemplate.Status = state
        Return SaveProcedureTemplate(ProcedureTemplate, Nothing, Nothing, audit)
    End Function

    ''' <summary>
    ''' Elimina una ProcedureTemplate
    ''' </summary>
    ''' <param name="ProcedureTemplate"></param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">ProcedureTemplate</exception>
    Public Function DeleteProcedureTemplate(ProcedureTemplate As ProcedureTemplate, ListProcedureCups As List(Of ProcedureCups), ListDeleteProcedureCups As List(Of ProcedureCups), company As String, audit As AuditMessage) As ActionResult Implements IProcedureTemplateAdminService.DeleteProcedureTemplate
        If ProcedureTemplate Is Nothing Then
            Throw New ArgumentNullException("ProcedureTemplate")
        End If

        Using cnx As New System.Data.SqlClient.SqlConnection(Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS, String.Empty, company, False))
            cnx.Open()
            Dim tx As System.Data.SqlClient.SqlTransaction = cnx.BeginTransaction()
            Dim command As New System.Data.SqlClient.SqlCommand("", cnx, tx)
            command.CommandTimeout = 30000
            command.CommandType = CommandType.Text

            Try
                'Se eliminan todos los detalles
                command.CommandText = "DELETE  FROM [Contract].[ProcedureCups] WHERE [ProceduresTemplateId] = " & ProcedureTemplate.Id
                command.ExecuteNonQuery()

                'Se elimina la cabecera
                command.CommandText = "DELETE  FROM [Contract].[ProcedureTemplate] WHERE [Id] = " & ProcedureTemplate.Id
                command.ExecuteNonQuery()

                tx.Commit()
                Return New ActionResult With {.StateResult = True}
            Catch ex As OptimisticConcurrencyException
                tx.Rollback()
                Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
            Catch ex As UpdateException
                tx.Rollback()
                Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
            Catch ex As Exception
                tx.Rollback()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
            Finally
                cnx.Close()
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Obtiene una ProcedureTemplate por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">
    ''' code
    ''' or
    ''' audit
    ''' </exception>
    Public Function GetProcedureTemplate(code As String, audit As AuditMessage) As ProcedureTemplate Implements IProcedureTemplateAdminService.GetProcedureTemplate
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim ProcedureTemplate As ProcedureTemplate = Me._procedureTemplateRepository.GetProcedureTemplate(code.Trim())
            If ProcedureTemplate IsNot Nothing AndAlso ProcedureTemplate.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of ProcedureTemplate)(ProcedureTemplate, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return ProcedureTemplate
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ProcedureTemplate
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una ProcedureTemplate por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">id</exception>
    Public Function GetProcedureTemplateById(id As Integer) As ProcedureTemplate Implements IProcedureTemplateAdminService.GetProcedureTemplateById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Try
            Return Me._procedureTemplateRepository.GetProcedureTemplateById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ProcedureTemplate
        End Try
    End Function

    ''' <summary>
    ''' Guarda o Actualiza una ProcedureTemplate
    ''' </summary>
    ''' <param name="ProcedureTemplate"></param>
    ''' <param name="audit">The audit.</param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">ProcedureTemplate</exception>
    Public Function SaveProcedureTemplate(ProcedureTemplate As ProcedureTemplate, ListProcedureCups As List(Of ProcedureCups), ListDeleteProcedureCups As List(Of ProcedureCups), audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of ProcedureTemplate) Implements IProcedureTemplateAdminService.SaveProcedureTemplate
        If ProcedureTemplate Is Nothing Then
            Throw New ArgumentNullException("ProcedureTemplate")
        End If
        Dim unitOfWork As IUnitWork = Me._procedureTemplateRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Dim unitOfWorkProcedureCups As IUnitWork = Me._procedureCupsRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = IsolationLevel.ReadCommitted
        'inicio la transaccion
        Using Transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim seq As ContractSequenceDetail = Nothing
                If ProcedureTemplate.Code Is Nothing OrElse ProcedureTemplate.Code.Trim().Equals(String.Empty) Then
                    seq = Me._secuenseDRepository.GetSequenseDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.ContractSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            ProcedureTemplate.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            Return New ActionResult(Of ProcedureTemplate) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
                        End If
                    Else
                        Return New ActionResult(Of ProcedureTemplate) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
                    End If
                End If

                Dim auxProcedureTemplate As ProcedureTemplate = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of ProcedureTemplate)
                Dim status As Integer

                If ProcedureTemplate.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    ProcedureTemplate.CreationUser = audit.CodeUser
                    ProcedureTemplate.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    auxProcedureTemplate = ProcedureTemplate.OriginalValue
                    ProcedureTemplate.ModificationUser = audit.CodeUser
                    ProcedureTemplate.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._procedureTemplateRepository.SaveEntity(ProcedureTemplate)
                unitOfWork.Commit()

                'Recorro el detalle de eliminados para ejecutar la accion al detalle por separado de la cabecera
                If ListDeleteProcedureCups IsNot Nothing AndAlso ListDeleteProcedureCups.Count > 0 Then
                    For Each itemDelete In ListDeleteProcedureCups
                        Dim procedureCups = _procedureCupsRepository.GetProcedureCupsById(itemDelete.Id)
                        'procedureCups.MarkAsDeleted()
                        _procedureCupsRepository.DeleteEntity(procedureCups)
                        unitOfWorkProcedureCups.Commit()
                    Next
                End If

                'Recorro el listado de detalles para poder guardar por separado de la cabecera
                If ListProcedureCups IsNot Nothing AndAlso ListProcedureCups.Count > 0 Then
                    'Se obtienen los registros agregados
                    Dim listAdd = ListProcedureCups.Where(Function(x) x.ChangeTracker.State = ObjectState.Added).ToList()
                    If listAdd IsNot Nothing AndAlso listAdd.Count > 0 Then
                        ListProcedureCups.ForEach(Sub(item) item.ProceduresTemplateId = ProcedureTemplate.Id)
                        _procedureTemplateRepository.SaveList(listAdd)
                    End If

                    'Se obtienen los registros modificados
                    Dim listModified = ListProcedureCups.Where(Function(x) x.ChangeTracker.State = ObjectState.Modified).ToList()
                    If listModified IsNot Nothing AndAlso listModified.Count > 0 Then
                        For Each item In listModified
                            _procedureCupsRepository.SaveEntity(item)
                        Next
                    End If
                End If

                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of ProcedureTemplate)(ProcedureTemplate, audit, status, auxProcedureTemplate)
                auditProcess.Execute()

                Transaction.Complete()
                Return New ActionResult(Of ProcedureTemplate) With {.StateResult = True, .ObjectEmbbeded = ProcedureTemplate}
            Catch ex As OptimisticConcurrencyException
                unitOfWork.RollbackChanges()
                unitOfWorkProcedureCups.RollbackChanges()
                Transaction.Dispose()
                Return New ActionResult(Of ProcedureTemplate) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                unitOfWorkProcedureCups.RollbackChanges()
                Transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of ProcedureTemplate) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Copiar y pegar del formulario de plantilla de procedimientos
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CopyAndPasteProcedureTemplate(data As List(Of List(Of String))) As ActionResult(Of List(Of ProcedureCups), List(Of Tuple(Of String, Integer))) Implements IProcedureTemplateAdminService.CopyAndPasteProcedureTemplate
        If data Is Nothing OrElse data.Count = 0 Then
            Throw New ArgumentNullException("data")
        End If
        'Listado que se devuelve para pegar a la rejilla del form
        Dim ListProcedureCups As New List(Of ProcedureCups)
        'Listado de errores
        Dim listErrors As New List(Of Tuple(Of String, Integer))
        Try
            'Objeto xml
            Dim xmlObject = ConvertToXml(data)
            'Se consume el procedimiento almacenado
            Dim resultStore = _procedureTemplateRepository.SP_CopyAndPasteProcedureTemplate(xmlObject)

            'Se crean los objetos para devolver y pegar en la rejilla
            If resultStore IsNot Nothing AndAlso resultStore.Count > 0 Then
                For Each itemXml In resultStore
                    If itemXml.StatusField = 1 Then 'Si el estado del item es True y pasó todas las validaciones
                        If ListProcedureCups IsNot Nothing AndAlso ListProcedureCups.Count > 0 Then
                            Dim itemAddeed = ListProcedureCups.Find(Function(x) x.CupsId = itemXml.CUPSId AndAlso If(x.ContractDescriptionId Is Nothing, 0, x.ContractDescriptionId) = If(itemXml.ContractDescriptionId Is Nothing, 0, itemXml.ContractDescriptionId))
                            If itemAddeed IsNot Nothing Then
                                Continue For
                            End If
                        End If
                        Dim ProcedureCups As New ProcedureCups
                        With ProcedureCups
                            .CupsId = itemXml.CUPSId
                            .CodeNameCUPS = itemXml.CUPSDescription
                            .CUPSCode = itemXml.CUPSDescription.Split("-")(0).ToString()
                            .CUPSName = itemXml.CUPSDescription.Split("-")(1).ToString()
                            .Contracted = True
                            .Quoted = False
                            If itemXml.ContractDescriptionId IsNot Nothing AndAlso itemXml.ContractDescriptionId > 0 Then
                                .ContractDescriptionId = itemXml.ContractDescriptionId
                                .CUPSEntityContractDescriptionId = itemXml.CUPSEntityContractDescriptionId
                                .ContractDescriptionCodeName = itemXml.ContractDescriptionCodeName
                            End If
                        End With
                        ListProcedureCups.Add(ProcedureCups)
                    Else 'Si el estado del item es False y no pasó alguna validación
                        listErrors.Add(New Tuple(Of String, Integer)(itemXml.MessageField, 2))
                    End If
                Next
            End If


            'Se devuelve el mensaje
            Return New ActionResult(Of List(Of ProcedureCups), List(Of Tuple(Of String, Integer))) With {.ObjectEmbbeded = ListProcedureCups, .ObjectEmbbededAux = listErrors, .StateResult = True}
        Catch ex As SqlException
            If ex.ErrorCode = -2146232060 Then
                Return New ActionResult(Of List(Of ProcedureCups), List(Of Tuple(Of String, Integer))) With {.StateResult = False, .Message = "Los valores contienen decimales con un formato no valido, por favor corrija para poder continuar"}
            Else
                Return New ActionResult(Of List(Of ProcedureCups), List(Of Tuple(Of String, Integer))) With {.StateResult = False, .Message = ex.ToString}
            End If
        Catch ex As Exception
            Return New ActionResult(Of List(Of ProcedureCups), List(Of Tuple(Of String, Integer))) With {.StateResult = False, .Message = ex.ToString}
        End Try
    End Function

    ''' <summary>
    ''' Convierte el listado de datos a xml
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConvertToXml(data As List(Of List(Of String)))
        Dim builder As StringBuilder = New StringBuilder()
        builder.Append("<Data>")

        For Each item In data
            builder.Append("<Row>")

            builder.Append("<CountFields>" & item.Count & "</CountFields>")
            builder.Append("<StatusField>" & 0 & "</StatusField>")
            builder.Append("<MessageField>" & "Ok" & "</MessageField>")
            builder.Append("<CUPSCode>" & item(0) & "</CUPSCode>")
            builder.Append("<CUPSDescription>" & "---" & "</CUPSDescription>")
            builder.Append("<CUPSId>" & 0 & "</CUPSId>")
            If item.Count > 1 Then
                builder.Append("<ContractDescriptionCode>" & item(1) & "</ContractDescriptionCode>")
            Else
                builder.Append("<ContractDescriptionCode>" & "" & "</ContractDescriptionCode>")
            End If
            builder.Append("<ContractDescriptionId>" & 0 & "</ContractDescriptionId>")
            builder.Append("<CUPSEntityContractDescriptionId>" & 0 & "</CUPSEntityContractDescriptionId>")
            builder.Append("<ContractDescriptionCodeName>" & "---" & "</ContractDescriptionCodeName>")

            builder.Append("</Row>")
        Next

        builder.Append("</Data>")
        Return builder.ToString
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _procedureTemplateRepository = Nothing
            _secuenseDRepository = Nothing
            _procedureCupsRepository = Nothing
            IndigoGC.Execute()
        End If
        disposedValue = True
    End Sub

    ' Visual Basic agrega este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
