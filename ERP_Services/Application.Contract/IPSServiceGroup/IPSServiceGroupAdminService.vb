'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Ernesto Cordoba
' Created          : 22/10/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Core
Imports Infrastructure.CrossCutting.Resources
Imports System.Transactions
Imports System.Data.Entity.Infrastructure
Imports System.Text
#End Region

Public Class IPSServiceGroupAdminService
    Implements IIPSServiceGroupAdminService
    Private Const FORM_NAME As String = "FrmBillingConcept"

    Private _iPSServiceGroupRepository As IIPSServiceGroupRepository
    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDRepository As IBillingSequenceDetailRepository

    Public Sub New(iPSServiceGroupRepository As IIPSServiceGroupRepository, secuenseDRepository As IBillingSequenceDetailRepository)
        If iPSServiceGroupRepository Is Nothing Then
            Throw New ArgumentNullException("iPSServiceGroupRepository")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        _iPSServiceGroupRepository = iPSServiceGroupRepository
        _secuenseDRepository = secuenseDRepository
    End Sub

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function ChangeStateIPSServiceGroup(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of BillingConcept) Implements IIPSServiceGroupAdminService.ChangeStateIPSServiceGroup
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If String.IsNullOrEmpty(state) Then
            Throw New ArgumentNullException("state")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim IPSServiceGroup As BillingConcept = Me._iPSServiceGroupRepository.GetIPSServiceGroup(code.Trim())
            If IPSServiceGroup IsNot Nothing AndAlso IPSServiceGroup.Id > 0 Then
                IPSServiceGroup.Status = state
            End If
            Dim result = Me.SaveIPSServiceGroup(IPSServiceGroup, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of BillingConcept) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Elimina una IPSServiceGroup
    ''' </summary>
    ''' <param name="IPSServiceGroup"></param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">IPSServiceGroup</exception>
    Public Function DeleteIPSServiceGroup(IPSServiceGroup As BillingConcept, audit As AuditMessage) As ActionResult Implements IIPSServiceGroupAdminService.DeleteIPSServiceGroup
        If IPSServiceGroup Is Nothing Then
            Throw New ArgumentNullException("IPSServiceGroup")
        End If
        Dim unitOfWork As IUnitWork = Me._iPSServiceGroupRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                IPSServiceGroup.ModificationUser = audit.CodeUser
                IPSServiceGroup.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of BillingConcept)(IPSServiceGroup, audit, status)

                While IPSServiceGroup.BillingConceptAccount.Count > 0
                    IPSServiceGroup.BillingConceptAccount(IPSServiceGroup.BillingConceptAccount.Count - 1).MarkAsDeleted()
                End While
                IPSServiceGroup.MarkAsDeleted()
                Me._iPSServiceGroupRepository.SaveEntity(IPSServiceGroup)
                unitOfWork.Commit()
                auditProcess.Execute()
                scope.Complete()
                Return New ActionResult With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .Message = ResourceManager.GetString("RecordDeleted")}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-999"}), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una IPSServiceGroup por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">
    ''' code
    ''' or
    ''' audit
    ''' </exception>
    Public Function GetIPSServiceGroup(code As String, audit As AuditMessage) As BillingConcept Implements IIPSServiceGroupAdminService.GetIPSServiceGroup
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim IPSServiceGroup As BillingConcept = Me._iPSServiceGroupRepository.GetIPSServiceGroup(code.Trim())
            If IPSServiceGroup IsNot Nothing AndAlso IPSServiceGroup.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of BillingConcept)(IPSServiceGroup, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return IPSServiceGroup
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New BillingConcept
        End Try
    End Function

    Public Function GetIPSServiceGroupById(id As Integer) As BillingConcept Implements IIPSServiceGroupAdminService.GetIPSServiceGroupById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Try
            Return Me._iPSServiceGroupRepository.GetIPSServiceGroupById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New BillingConcept
        End Try
    End Function

    ''' <summary>
    ''' Guarda o Actualiza una IPSServiceGroup
    ''' </summary>
    ''' <param name="IPSServiceGroup"></param>
    ''' <param name="audit">The audit.</param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">IPSServiceGroup</exception>
    Public Function SaveIPSServiceGroup(IPSServiceGroup As BillingConcept, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of BillingConcept) Implements IIPSServiceGroupAdminService.SaveIPSServiceGroup
        If IPSServiceGroup Is Nothing Then
            Throw New ArgumentNullException("ObjEntity")
        End If
        Dim unitOfWork As IUnitWork = Me._iPSServiceGroupRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(IPSServiceGroup.Code) Then
                    Dim seq As BillingSequenceDetail = Me._secuenseDRepository.GetSequenseDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.BillingSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            IPSServiceGroup.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of BillingConcept) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.BillingSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), IPSServiceGroup.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of BillingConcept) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As BillingConcept = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of BillingConcept)
                Dim status As Integer

                If IPSServiceGroup.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    IPSServiceGroup.CreationUser = audit.CodeUser
                    IPSServiceGroup.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = IPSServiceGroup.OriginalValue
                    IPSServiceGroup.ModificationUser = audit.CodeUser
                    IPSServiceGroup.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._iPSServiceGroupRepository.SaveEntity(IPSServiceGroup)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of BillingConcept)(IPSServiceGroup, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                IPSServiceGroup.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of BillingConcept) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = IPSServiceGroup, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of BillingConcept) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of BillingConcept) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Function CopyAndPasteBillingConceptCostCenter(data As List(Of List(Of String))) As ActionResult(Of List(Of BillingConceptCostCenter)) Implements IIPSServiceGroupAdminService.CopyAndPasteBillingConceptCostCenter
        If data Is Nothing OrElse data.Count = 0 Then
            Throw New ArgumentNullException("data")
        End If
        'Listado que se devuelve para pegar a la rejilla del form
        Dim ListBillingConceptCostCenter As New List(Of BillingConceptCostCenter)
        'Listado de errores
        Dim listErrors As New List(Of String)
        Try
            'Objeto xml
            Dim xmlObject = ConvertToXmlCopyAndPasteBillingConceptCostCenter(data)
            'Se consume el procedimiento almacenado
            Dim resultStore = Me._iPSServiceGroupRepository.SP_CopyAndPasteBillingConceptCostCenter(xmlObject)

            'Se crean los objetos para devolver y pegar en la rejilla
            If resultStore IsNot Nothing AndAlso resultStore.Count > 0 Then
                For Each itemXml In resultStore
                    If itemXml.StatusField = 0 Then
                        If ListBillingConceptCostCenter.Any(Function(d) d.BranchOfficeId = itemXml.BranchOfficeId AndAlso d.FunctionalUnitId = itemXml.FunctionalUnitId) Then
                            listErrors.Add(String.Format("La sucursal {0} con la unidad funcional {1} ya existe en la lista.", String.Format("{0} - {1}", itemXml.BranchOfficeCode, itemXml.BranchOfficeName), String.Format("{0} - {1}", itemXml.FunctionalUnitCode, itemXml.FunctionalUnitName)))
                            Continue For
                        End If

                        'Se crea el nuevo objeto para agregarlo al listado
                        ListBillingConceptCostCenter.Add(New BillingConceptCostCenter With
                        {
                            .BranchOfficeId = itemXml.BranchOfficeId,
                            .BranchOfficeCodeName = String.Format("{0} - {1}", itemXml.BranchOfficeCode, itemXml.BranchOfficeName),
                            .FunctionalUnitId = itemXml.FunctionalUnitId,
                            .FunctionalUnitCodeName = String.Format("{0} - {1}", itemXml.FunctionalUnitCode, itemXml.FunctionalUnitName),
                            .CostCenterId = itemXml.CostCenterId,
                            .CostCenterCodeName = String.Format("{0} - {1}", itemXml.CostCenterCode, itemXml.CostCenterName)
                        })
                    Else 'Si el estado del item es False y no pasó alguna validación
                        listErrors.Add(itemXml.MessageField)
                    End If
                Next
            End If

            'Se devuelve el mensaje
            Return New ActionResult(Of List(Of BillingConceptCostCenter)) With {.StateResult = True, .ObjectEmbbeded = ListBillingConceptCostCenter, .MessageResult = listErrors}
        Catch ex As Exception
            Return New ActionResult(Of List(Of BillingConceptCostCenter)) With {.StateResult = False, .Message = ex.ToString}
        End Try
    End Function

#Region "Copy & Paste"

    Private Function ConvertToXmlCopyAndPasteBillingConceptCostCenter(data As List(Of List(Of String)))
        Dim builder As StringBuilder = New StringBuilder()

        For Each item In data
            builder.Append("<Data>")

            builder.Append("<BranchOfficeCode>" & If(item.Count > 0, item(0), String.Empty) & "</BranchOfficeCode>")
            builder.Append("<FunctionalUnitCode>" & If(item.Count > 1, item(1), String.Empty) & "</FunctionalUnitCode>")
            builder.Append("<CostCenterCode>" & If(item.Count > 2, item(2), String.Empty) & "</CostCenterCode>")

            builder.Append("</Data>")
        Next

        Return builder.ToString
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _iPSServiceGroupRepository = Nothing
            _secuenseDRepository = Nothing
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
