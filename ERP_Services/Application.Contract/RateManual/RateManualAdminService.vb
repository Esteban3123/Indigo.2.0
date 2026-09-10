'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 09/10/2014
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
Imports System.Data.SqlClient
Imports System.Text
Imports System.Globalization

Public Class RateManualAdminService
    Implements IRateManualAdminService

#Region "Variables"

    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _rateManualRepository As IRateManualRepository
    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDRepository As ISequenseContractDRepository

#End Region

#Region "Builder"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal rateManualRepository As IRateManualRepository, ByVal secuenseDRepository As ISequenseContractDRepository)
        If rateManualRepository Is Nothing Then
            Throw New ArgumentNullException("rateManualRepository Vacio")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        _rateManualRepository = rateManualRepository
        _secuenseDRepository = secuenseDRepository
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChangeStateRateManual(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of RateManual) Implements IRateManualAdminService.ChangeStateRateManual
        Dim RateManual As RateManual = _rateManualRepository.GetRateManual(code)
        RateManual.Status = state
        Return SaveRateManual(RateManual, audit)
    End Function

    ''' <summary>
    ''' Elimina un manual tarifario
    ''' </summary>
    ''' <param name="RateManual"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteRateManual(RateManual As RateManual, audit As AuditMessage) As ActionResult Implements IRateManualAdminService.DeleteRateManual
        If RateManual Is Nothing Then
            Throw New ArgumentNullException("RateManual")
        End If
        Dim unitOfWork As IUnitWork = Me._rateManualRepository.UnitWork
        Try
            RateManual.StartTracking()
            While RateManual.SurgeriesPercentageManual.Count > 0
                RateManual.SurgeriesPercentageManual.Item(0).MarkAsDeleted()
            End While
            While RateManual.RateManualDetail.Count > 0
                RateManual.RateManualDetail.Item(0).MarkAsDeleted()
            End While
            While RateManual.RateManualDetailSurgical.Count > 0
                RateManual.RateManualDetailSurgical.Item(0).MarkAsDeleted()
            End While
            RateManual.MarkAsDeleted()
            Dim auditProcess As IndigoAuditSimpleEntity(Of RateManual)
            auditProcess = New IndigoAuditSimpleEntity(Of RateManual)(RateManual, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            Me._rateManualRepository.SaveEntity(RateManual)
            unitOfWork.Commit()
            auditProcess.Execute()
            Return New ActionResult With {.StateResult = True}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un manual tarifario por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetRateManual(code As String, audit As AuditMessage) As ActionResult(Of RateManual) Implements IRateManualAdminService.GetRateManual
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim RateManual As RateManual = Me._rateManualRepository.GetRateManual(code.Trim())
            If RateManual IsNot Nothing AndAlso RateManual.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of RateManual)(RateManual, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of RateManual) With {.StateResult = True, .ObjectEmbbeded = RateManual}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of RateManual) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un manual tarifario por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetRateManualById(id As Integer, audit As AuditMessage) As ActionResult(Of RateManual) Implements IRateManualAdminService.GetRateManualById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim RateManual As RateManual = Me._rateManualRepository.GetRateManualById(id)
            If RateManual IsNot Nothing AndAlso RateManual.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of RateManual)(RateManual, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of RateManual) With {.StateResult = True, .ObjectEmbbeded = RateManual}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of RateManual) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Guarda o actualiza un manual tarifario
    ''' </summary>
    ''' <param name="RateManual"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveRateManual(RateManual As RateManual, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of RateManual) Implements IRateManualAdminService.SaveRateManual
        If RateManual Is Nothing Then
            Throw New ArgumentNullException("RateManual")
        End If
        Dim unitOfWork As IUnitWork = Me._rateManualRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Dim seq As ContractSequenceDetail = Nothing
            If RateManual.Code Is Nothing OrElse RateManual.Code.Trim().Equals(String.Empty) Then
                seq = Me._secuenseDRepository.GetSequenseDById(idSequense)
                If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.ContractSequence.Sequential Then
                    Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                    If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                        RateManual.Code = res
                        seq.Next += 1
                        Me._secuenseDRepository.SaveEntity(seq)
                    Else
                        Return New ActionResult(Of RateManual) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
                    End If
                Else
                    Return New ActionResult(Of RateManual) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
                End If
            End If

            Dim auxRateManual As RateManual = Nothing
            Dim auditProcess As IndigoAuditSimpleEntity(Of RateManual)
            Dim status As Integer

            If RateManual.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                RateManual.CreationUser = audit.CodeUser
                RateManual.CreationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            Else
                auxRateManual = RateManual.OriginalValue
                RateManual.ModificationUser = audit.CodeUser
                RateManual.ModificationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Update
            End If

            Me._rateManualRepository.SaveEntity(RateManual)
            unitOfWork.Commit()
            sequenseUnitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of RateManual)(RateManual, audit, status, auxRateManual)
            auditProcess.Execute()

            'Se marca la entidad como sin cambios
            RateManual.MarkAsUnchanged()

            Return New ActionResult(Of RateManual) With {.StateResult = True, .ObjectEmbbeded = RateManual}
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of RateManual) With {.StateResult = False, .MessageResult = {"-111"}.ToList(), .Message = "No se puede insertar porque existe código duplicado: " + RateManual.Code}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of RateManual) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of RateManual) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Copiar y pegar del form de manual de tarifas
    ''' </summary>
    ''' <param name="data"></param>
    ''' <param name="ServiceManual"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CopyAndPasteRateManual(data As List(Of List(Of String)), ServiceManual As Integer) As ActionResult(Of List(Of RateManualDetail), List(Of Tuple(Of String, Integer))) Implements IRateManualAdminService.CopyAndPasteRateManual
        If data Is Nothing OrElse data.Count = 0 Then
            Throw New ArgumentNullException("data")
        End If
        'Listado que se devuelve para pegar a la rejilla del form
        Dim ListRateManualDetail As New List(Of RateManualDetail)
        'Listado de errores
        Dim listErrors As New List(Of Tuple(Of String, Integer))
        Try
            'Objeto xml
            Dim xmlObject = ConvertToXml(data, 0, ServiceManual)
            'Se consume el procedimiento almacenado
            Dim resultStore = _rateManualRepository.SP_CopyAndPasteRateManual(xmlObject, ServiceManual, 0)

            'Se crean los objetos para devolver y pegar en la rejilla
            If resultStore IsNot Nothing AndAlso resultStore.Count > 0 Then
                For Each itemXml In resultStore
                    If itemXml.StatusField = 1 Then 'Si el estado del item es True y pasó todas las validaciones
                        If ListRateManualDetail IsNot Nothing AndAlso ListRateManualDetail.Count > 0 Then
                            Dim itemAddeed = ListRateManualDetail.Find(Function(x) x.IPSServiceId = itemXml.IPSServiceId)
                            If itemAddeed IsNot Nothing Then
                                Continue For
                            End If
                        End If
                        Dim RateManualDetail As New RateManualDetail
                        With RateManualDetail
                            .IPSServiceId = itemXml.IPSServiceId
                            .IPSServiceDescription = itemXml.IPSServiceDescription
                            Dim SalesValue = Utils.correctDecimalFormat(itemXml.SalesValue.ToString())
                            Dim SalesValueWithSurcharge = Utils.correctDecimalFormat(itemXml.SalesValueWithSurcharge.ToString())
                            .SalesValue = SalesValue
                            .SalesValueWithSurcharge = SalesValueWithSurcharge
                        End With
                        ListRateManualDetail.Add(RateManualDetail)
                    Else 'Si el estado del item es False y no pasó alguna validación
                        listErrors.Add(New Tuple(Of String, Integer)(itemXml.MessageField, 2))
                    End If
                Next
            End If


            'Se devuelve el mensaje
            Return New ActionResult(Of List(Of RateManualDetail), List(Of Tuple(Of String, Integer))) With {.ObjectEmbbeded = ListRateManualDetail, .ObjectEmbbededAux = listErrors, .StateResult = True}
        Catch ex As SqlException
            If ex.ErrorCode = -2146232060 Then
                Return New ActionResult(Of List(Of RateManualDetail), List(Of Tuple(Of String, Integer))) With {.StateResult = False, .Message = "Los valores contienen decimales con un formato no valido, por favor corrija para poder continuar"}
            Else
                Return New ActionResult(Of List(Of RateManualDetail), List(Of Tuple(Of String, Integer))) With {.StateResult = False, .Message = ex.ToString}
            End If
        Catch ex As Exception
            Return New ActionResult(Of List(Of RateManualDetail), List(Of Tuple(Of String, Integer))) With {.StateResult = False, .Message = ex.ToString}
        End Try
    End Function

    ''' <summary>
    ''' Convierte el listado de datos a xml
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConvertToXml(data As List(Of List(Of String)), GridOption As Integer, ServiceManual As Integer)
        Dim builder As StringBuilder = New StringBuilder()
        builder.Append("<Data>")

        For Each item In data
            builder.Append("<Row>")

            builder.Append("<CountFields>" & item.Count & "</CountFields>")
            builder.Append("<StatusField>" & 0 & "</StatusField>")
            builder.Append("<MessageField>" & "Ok" & "</MessageField>")
            If item(0) IsNot Nothing Then
                builder.Append("<IPSServiceCode>" & item(0) & "</IPSServiceCode>")
            Else
                builder.Append("<IPSServiceCode>" & 0 & "</IPSServiceCode>")
            End If
            builder.Append("<IPSServiceDescription>" & "---" & "</IPSServiceDescription>")
            builder.Append("<IPSServiceId>" & 0 & "</IPSServiceId>")
            If item.Count > 1 Then
                builder.Append("<SalesValue>" & item(1) & "</SalesValue>")
            Else
                builder.Append("<SalesValue>" & 0 & "</SalesValue>")
            End If
            If item.Count > 2 Then
                builder.Append("<SalesValueWithSurcharge>" & item(2) & "</SalesValueWithSurcharge>")
            Else
                builder.Append("<SalesValueWithSurcharge>" & 0 & "</SalesValueWithSurcharge>")
            End If
            builder.Append("<SurgicalGroupDescription>" & "---" & "</SurgicalGroupDescription>")
            builder.Append("<SurgicalGroupId>" & 0 & "</SurgicalGroupId>")
            builder.Append("<UVRRangeDescription>" & "---" & "</UVRRangeDescription>")
            builder.Append("<UVRRangeId>" & 0 & "</UVRRangeId>")
            If GridOption = 1 Then 'Si es la rejilla que tiene grupo
                If ServiceManual = 1 OrElse ServiceManual = 2 Then 'Si es ISS2001 o ISS2004
                    builder.Append("<SurgicalGroupCode>" & "---" & "</SurgicalGroupCode>")
                    If item.Count > 3 Then
                        builder.Append("<UVRRangeCode>" & item(3) & "</UVRRangeCode>")
                    Else
                        builder.Append("<UVRRangeCode>" & "---" & "</UVRRangeCode>")
                    End If
                Else 'Si es SOAT
                    builder.Append("<UVRRangeCode>" & "---" & "</UVRRangeCode>")
                    If item.Count > 3 Then
                        builder.Append("<SurgicalGroupCode>" & item(3) & "</SurgicalGroupCode>")
                    Else
                        builder.Append("<SurgicalGroupCode>" & "---" & "</SurgicalGroupCode>")
                    End If
                End If
            Else 'Si es la rejilla que no tiene grupo
                builder.Append("<SurgicalGroupCode>" & "---" & "</SurgicalGroupCode>")
                builder.Append("<UVRRangeCode>" & "---" & "</UVRRangeCode>")
            End If
            builder.Append("</Row>")
        Next

        builder.Append("</Data>")
        Return builder.ToString
    End Function

    ''' <summary>
    ''' Copiar y pegar del form de manual de tarifas
    ''' </summary>
    ''' <param name="data"></param>
    ''' <param name="ServiceManual"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CopyAndPasteRateManualSurgical(data As List(Of List(Of String)), ServiceManual As Integer) As ActionResult(Of List(Of RateManualDetailSurgical), List(Of Tuple(Of String, Integer))) Implements IRateManualAdminService.CopyAndPasteRateManualSurgical
        If data Is Nothing OrElse data.Count = 0 Then
            Throw New ArgumentNullException("data")
        End If
        'Listado que se devuelve para pegar a la rejilla del form
        Dim ListRateManualDetailSurgical As New List(Of RateManualDetailSurgical)
        'Listado de errores
        Dim listErrors As New List(Of Tuple(Of String, Integer))
        Try
            'Objeto xml
            Dim xmlObject = ConvertToXml(data, 1, ServiceManual)
            'Se consume el procedimiento almacenado
            Dim resultStore = _rateManualRepository.SP_CopyAndPasteRateManual(xmlObject, ServiceManual, 1)

            'Se crean los objetos para devolver y pegar en la rejilla
            If resultStore IsNot Nothing AndAlso resultStore.Count > 0 Then
                For Each itemXml In resultStore
                    If itemXml.StatusField = 1 Then 'Si el estado del item es True y pasó todas las validaciones
                        If ListRateManualDetailSurgical IsNot Nothing AndAlso ListRateManualDetailSurgical.Count > 0 Then
                            Dim itemAddeed = ListRateManualDetailSurgical.Find(Function(x) x.IPSServiceId = itemXml.IPSServiceId)
                            If itemAddeed IsNot Nothing Then
                                Continue For
                            End If
                        End If
                        Dim RateManualDetailSurgical As New RateManualDetailSurgical
                        With RateManualDetailSurgical
                            .IPSServiceId = itemXml.IPSServiceId
                            .IPSServiceDescription = itemXml.IPSServiceDescription
                            .SalesValue = itemXml.SalesValue
                            .SalesValueWithSurcharge = itemXml.SalesValueWithSurcharge
                            If ServiceManual = 1 OrElse ServiceManual = 2 Then 'Si es ISS2001 o ISS2004
                                .UVRRangeId = itemXml.UVRRangeId
                                .UVRRangeDescription = itemXml.UVRRangeDescription
                            ElseIf ServiceManual <> eRateManuelType.Institutional Then 'Si es SOAT
                                .SurgicalGroupId = itemXml.SurgicalGroupId
                                .SurgicalGroupDescription = itemXml.SurgicalGroupDescription
                            End If
                        End With
                        ListRateManualDetailSurgical.Add(RateManualDetailSurgical)
                    Else 'Si el estado del item es False y no pasó alguna validación
                        listErrors.Add(New Tuple(Of String, Integer)(itemXml.MessageField, 2))
                    End If
                Next
            End If


            'Se devuelve el mensaje
            Return New ActionResult(Of List(Of RateManualDetailSurgical), List(Of Tuple(Of String, Integer))) With {.ObjectEmbbeded = ListRateManualDetailSurgical, .ObjectEmbbededAux = listErrors, .StateResult = True}
        Catch ex As SqlException
            If ex.ErrorCode = -2146232060 Then
                Return New ActionResult(Of List(Of RateManualDetailSurgical), List(Of Tuple(Of String, Integer))) With {.StateResult = False, .Message = "Los valores contienen decimales con un formato no valido, por favor corrija para poder continuar"}
            Else
                Return New ActionResult(Of List(Of RateManualDetailSurgical), List(Of Tuple(Of String, Integer))) With {.StateResult = False, .Message = ex.ToString}
            End If
        Catch ex As Exception
            Return New ActionResult(Of List(Of RateManualDetailSurgical), List(Of Tuple(Of String, Integer))) With {.StateResult = False, .Message = ex.ToString}
        End Try
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _rateManualRepository = Nothing
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
