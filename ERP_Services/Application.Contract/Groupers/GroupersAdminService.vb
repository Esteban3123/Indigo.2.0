'***********************************************************************
' Assembly         : Application.Contract
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 11/07/2017
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Data.Entity.Core
Imports Application.Base
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Crystal
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions

Public Class GroupersAdminService
    Implements IGroupersAdminService

#Region "Variables"

    Private _groupersRepository As IGroupersRepository
    Private _cupsEntityRepository As ICupsEntityRepository
    Private _activityRepository As IAGACTIMEDRepository
    Private _secuenseDRepository As ISequenseContractDRepository

#End Region

#Region "Builder"

    Public Sub New(ByVal groupersRepository As IGroupersRepository, cupsEntityRepository As ICupsEntityRepository, activityRepository As IAGACTIMEDRepository, ByVal secuenseDRepository As ISequenseContractDRepository)
        If groupersRepository Is Nothing Then
            Throw New ArgumentNullException("groupersRepository Vacio")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        _groupersRepository = groupersRepository
        _cupsEntityRepository = cupsEntityRepository
        _activityRepository = activityRepository
        _secuenseDRepository = secuenseDRepository
    End Sub

#End Region

#Region "Methods"

    Public Function GetGroupersById(id As Integer, audit As AuditMessage) As ActionResult(Of Groupers) Implements IGroupersAdminService.GetGroupersById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim groupers As Groupers = Me._groupersRepository.GetGroupersById(id)
            If groupers IsNot Nothing AndAlso groupers.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of Groupers)(groupers, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of Groupers) With {.StateResult = True, .ObjectEmbbeded = groupers}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Groupers) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Function GetGroupers(code As String, audit As AuditMessage) As ActionResult(Of Groupers) Implements IGroupersAdminService.GetGroupers
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim groupers As Groupers = Me._groupersRepository.GetGroupers(code.Trim())
            If groupers IsNot Nothing AndAlso groupers.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of Groupers)(groupers, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of Groupers) With {.StateResult = True, .ObjectEmbbeded = groupers}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Groupers) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Function CopyAndPasteGroupersCups(data As List(Of List(Of String))) As ActionResult(Of List(Of GroupersCups)) Implements IGroupersAdminService.CopyAndPasteGroupersCups
        If data Is Nothing OrElse data.Count = 0 Then
            Throw New ArgumentNullException("data")
        End If
        Try
            Dim errors As New System.Text.StringBuilder
            Dim ListGroupersCups As New List(Of GroupersCups)

            'Dictionaries
            Dim dictionaryCUPSEntity As New Dictionary(Of String, CUPSEntity)()

            'Items Individuals
            Dim cUPSEntity As CUPSEntity = Nothing
            Dim cUPSEntityContractDescriptions As CUPSEntityContractDescriptions = Nothing

            Dim position = 0
            For Each item In data
                position += 1

                If Not {1, 2}.Contains(item.Count) Then
                    errors.AppendLine(String.Format("El registro {0} no tiene la estructura requerida", position))
                    Continue For
                End If
                If String.IsNullOrWhiteSpace(item(0)) Then
                    errors.AppendLine(String.Format("El registro {0} no tiene un valor", position))
                    Continue For
                End If

                Dim cupsCode = item(0)
                Dim cupsDescription As String = Nothing
                If item.Count > 1 Then
                    cupsDescription = item(1)
                End If

                If Not dictionaryCUPSEntity.ContainsKey(cupsCode) Then
                    cUPSEntity = _cupsEntityRepository.GetCupsEntityWithContractDescriptions(cupsCode)
                    dictionaryCUPSEntity.Add(cupsCode, cUPSEntity)
                Else
                    cUPSEntity = dictionaryCUPSEntity(cupsCode)
                End If

                If cUPSEntity.Id = 0 Then
                    errors.AppendLine(String.Format("El codigo CUPS del registro {0} no existe", position))
                    Continue For
                End If

                cUPSEntityContractDescriptions = Nothing
                If cUPSEntity.CUPSEntityContractDescriptions.Any(Function(cecd) cecd.IsDelete = 0) Then
                    If String.IsNullOrWhiteSpace(cupsDescription) Then
                        errors.AppendLine(String.Format("El codigo CUPS del registro {0} maneja descripcion pero el campo esta vacio", position))
                        Continue For
                    End If

                    cUPSEntityContractDescriptions = cUPSEntity.CUPSEntityContractDescriptions.Where(Function(cecd) cecd.IsDelete = 0 And cecd.ContractDescriptions.Code = cupsDescription).FirstOrDefault()
                    If cUPSEntityContractDescriptions Is Nothing Then
                        errors.AppendLine(String.Format("El codigo de la descripcion del registro {0} no existe o no esta relacionado al cups", position))
                        Continue For
                    End If
                End If

                Dim grouperCups As New GroupersCups
                grouperCups.CUPSEntityId = cUPSEntity.Id
                grouperCups.DescriptionCups = String.Format("{0} - {1}", cUPSEntity.Code, cUPSEntity.Description)
                grouperCups.CupsSubGroupCodeName = String.Format("{0} - {1}", cUPSEntity.CupsSubgroup.Code, cUPSEntity.CupsSubgroup.Name)
                grouperCups.CupsGroupCodeName = String.Format("{0} - {1}", cUPSEntity.CupsSubgroup.CupsGroup.Code, cUPSEntity.CupsSubgroup.CupsGroup.Name)
                If cUPSEntityContractDescriptions IsNot Nothing Then
                    grouperCups.CUPSEntityContractDescriptionId = cUPSEntityContractDescriptions.Id
                    grouperCups.ContractDescriptionId = cUPSEntityContractDescriptions.ContractDescriptionId
                    grouperCups.ContractDescriptionCodeName = String.Format("{0} - {1}", cUPSEntityContractDescriptions.ContractDescriptions.Code, cUPSEntityContractDescriptions.ContractDescriptions.Name)
                End If

                If ListGroupersCups.Any(Function(x) x.CUPSEntityId = grouperCups.CUPSEntityId AndAlso If(x.ContractDescriptionId Is Nothing, 0, x.ContractDescriptionId) = If(grouperCups.ContractDescriptionId Is Nothing, 0, grouperCups.ContractDescriptionId)) Then
                    Continue For
                End If

                ListGroupersCups.Add(grouperCups)
            Next

            Return New ActionResult(Of List(Of GroupersCups)) With {.StateResultAux = True, .ObjectEmbbeded = ListGroupersCups, .Message = errors.ToString()}
        Catch ex As SqlClient.SqlException
            If ex.ErrorCode = -2146232060 Then
                Return New ActionResult(Of List(Of GroupersCups)) With {.StateResult = False, .Message = "Los valores contienen decimales con un formato no valido, por favor corrija para poder continuar"}
            Else
                Return New ActionResult(Of List(Of GroupersCups)) With {.StateResult = False, .Message = ex.ToString}
            End If
        Catch ex As Exception
            Return New ActionResult(Of List(Of GroupersCups)) With {.StateResult = False, .Message = ex.ToString}
        End Try
    End Function

    Public Function SaveGroupers(Groupers As Groupers, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of Groupers) Implements IGroupersAdminService.SaveGroupers
        If Groupers Is Nothing Then
            Throw New ArgumentNullException("Groupers")
        End If
        Dim unitOfWork As IUnitWork = Me._groupersRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            If Groupers.GroupersCups.Any() OrElse Groupers.GroupersActivities.Any() Then
                If _groupersRepository.ValidateIfGrouperIsParent(Groupers.Id) Then
                    Return New ActionResult(Of Groupers) With {.StateResult = False, .Message = "Un agrupador padre no puede tener cups o actividades"}
                End If
            End If
            If Groupers.Status = False Then
                If _groupersRepository.ValidateIfGrouperIsParent(Groupers.Id) Then

                    Return New ActionResult(Of Groupers) With {.StateResult = False, .Message = "No se puede inactivar un agrupador padre"}
                End If
            End If

            Dim seq As ContractSequenceDetail = Nothing
            If Groupers.Code Is Nothing OrElse Groupers.Code.Trim().Equals(String.Empty) Then
                seq = Me._secuenseDRepository.GetSequenseDById(idSequense)
                If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.ContractSequence.Sequential Then
                    Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                    If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                        Groupers.Code = res
                        seq.Next += 1
                        Me._secuenseDRepository.SaveEntity(seq)
                    Else
                        Return New ActionResult(Of Groupers) With {.StateResult = False, .Message = "_Seq02_"}
                    End If
                Else
                    Return New ActionResult(Of Groupers) With {.StateResult = False, .Message = "_Seq01_"}
                End If
            End If

            Dim auxGroupers As Groupers = Nothing
            Dim auditProcess As IndigoAuditSimpleEntity(Of Groupers)
            Dim status As Integer

            If Groupers.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                Groupers.CreationUser = audit.CodeUser
                Groupers.CreationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            Else
                auxGroupers = Groupers.OriginalValue
                Groupers.ModificationUser = audit.CodeUser
                Groupers.ModificationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Update
            End If

            Me._groupersRepository.SaveEntity(Groupers)
            unitOfWork.Commit()
            sequenseUnitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of Groupers)(Groupers, audit, status, auxGroupers)
            auditProcess.Execute()

            'Se marca la entidad como sin cambios
            Groupers.MarkAsUnchanged()

            Return New ActionResult(Of Groupers) With {.StateResult = True, .ObjectEmbbeded = Groupers}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of Groupers) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Groupers) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Function ChangeStateGroupers(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of Groupers) Implements IGroupersAdminService.ChangeStateGroupers
        Dim groupers As Groupers = _groupersRepository.GetGroupers(code)
        groupers.Status = state
        Return SaveGroupers(groupers, audit)
    End Function

    Public Function DeleteGroupers(Groupers As Groupers, audit As AuditMessage) As ActionResult Implements IGroupersAdminService.DeleteGroupers
        If Groupers Is Nothing Then
            Throw New ArgumentNullException("Groupers")
        End If
        Dim unitOfWork As IUnitWork = Me._groupersRepository.UnitWork
        Try
            If _groupersRepository.ValidateIfGrouperIsParent(Groupers.Id) Then
                Return New ActionResult With {.StateResult = False, .Message = "No se puede eliminar el agrupador porque es el padre de otros agrupadores"}
            End If

            Dim auditProcess As IndigoAuditSimpleEntity(Of Groupers)
            auditProcess = New IndigoAuditSimpleEntity(Of Groupers)(Groupers, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            Me._groupersRepository.DeleteEntity(Groupers)
            unitOfWork.Commit()
            auditProcess.Execute()
            Return New ActionResult With {.StateResult = True}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Function ImportGroupers(ByVal workSheetType As eGrouperWorkSheetType, data As List(Of ImportFileRow), ByVal audit As AuditMessage) As ActionResult(Of List(Of String)) Implements IGroupersAdminService.ImportGroupers
        If data Is Nothing OrElse data.Count = 0 Then
            Throw New ArgumentNullException("data")
        End If

        If workSheetType = eGrouperWorkSheetType.Grouper Then
            Return ImportGroupersData(data, audit)
        ElseIf workSheetType = eGrouperWorkSheetType.CUPS Then
            Return ImportGroupersCupsData(data, audit)
        ElseIf workSheetType = eGrouperWorkSheetType.Activities Then
            Return ImportGroupersActivitiesData(data, audit)
        End If

        Return New ActionResult(Of List(Of String)) With {.MessageResult = New List(Of String) From {"Proceso no válido"}}
    End Function

#End Region

#Region "Private Methods"

    Private Function ImportGroupersData(listDataRow As List(Of ImportFileRow), ByVal audit As AuditMessage) As ActionResult(Of List(Of String))
        Dim listErrorMessages As New List(Of String)
        Dim listSuccessMessages As New List(Of String)

        Try
            Dim listFilterErrors As New List(Of String)

            listFilterErrors = listDataRow.Where(Function(d) d.Row.Count < 14).Select(Function(d) String.Format("El agrupador del registro {0} no tiene una estructura válida", (d.IndexRow + 1).ToString())).ToList()
            If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                listDataRow.RemoveAll(Function(d) d.Row.Count < 12)
                listErrorMessages.AddRange(listFilterErrors)
            End If

            listFilterErrors = listDataRow.Where(Function(d) d.Row.Item(0).ToString().Trim().Length > 20).Select(Function(d) String.Format("El código del agrupador del registro {0} tiene una longitud mayor a 20", (d.IndexRow + 1).ToString())).ToList()
            If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                listDataRow.RemoveAll(Function(d) d.Row.Item(0).ToString().Trim().Length > 20)
                listErrorMessages.AddRange(listFilterErrors)
            End If

            listFilterErrors = listDataRow.Where(Function(d) String.IsNullOrEmpty(d.Row.Item(1))).Select(Function(d) String.Format("El nombre del agrupador del registro {0} esta vacío", (d.IndexRow + 1).ToString())).ToList()
            If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                listDataRow.RemoveAll(Function(d) String.IsNullOrEmpty(d.Row.Item(1)))
                listErrorMessages.AddRange(listFilterErrors)
            End If

            listFilterErrors = listDataRow.Where(Function(d) Not IsNumeric(d.Row.Item(3))).Select(Function(d) String.Format("El número usuarios del agrupador del registro {0} no es numerico", (d.IndexRow + 1).ToString())).ToList()
            If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                listDataRow.RemoveAll(Function(d) Not IsNumeric(d.Row.Item(3)))
                listErrorMessages.AddRange(listFilterErrors)
            End If

            listFilterErrors = listDataRow.Where(Function(d) Not IsNumeric(d.Row.Item(4))).Select(Function(d) String.Format("El mínimo del agrupador del registro {0} no es numerico", (d.IndexRow + 1).ToString())).ToList()
            If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                listDataRow.RemoveAll(Function(d) Not IsNumeric(d.Row.Item(4)))
                listErrorMessages.AddRange(listFilterErrors)
            End If

            listFilterErrors = listDataRow.Where(Function(d) Not IsNumeric(d.Row.Item(5))).Select(Function(d) String.Format("El máximo del agrupador del registro {0} no es numerico", (d.IndexRow + 1).ToString())).ToList()
            If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                listDataRow.RemoveAll(Function(d) Not IsNumeric(d.Row.Item(5)))
                listErrorMessages.AddRange(listFilterErrors)
            End If

            listFilterErrors = listDataRow.Where(Function(d) Not IsNumeric(d.Row.Item(6))).Select(Function(d) String.Format("El CME proyectado del agrupador del registro {0} no es numerico", (d.IndexRow + 1).ToString())).ToList()
            If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                listDataRow.RemoveAll(Function(d) Not IsNumeric(d.Row.Item(6)))
                listErrorMessages.AddRange(listFilterErrors)
            End If

            listFilterErrors = listDataRow.Where(Function(d) Not IsNumeric(d.Row.Item(7))).Select(Function(d) String.Format("La frecuencia del agrupador del registro {0} no es numerico", (d.IndexRow + 1).ToString())).ToList()
            If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                listDataRow.RemoveAll(Function(d) Not IsNumeric(d.Row.Item(7)))
                listErrorMessages.AddRange(listFilterErrors)
            End If

            listFilterErrors = listDataRow.Where(Function(d) Not IsNumeric(d.Row.Item(8))).Select(Function(d) String.Format("El total contratado del agrupador del registro {0} no es numerico", (d.IndexRow + 1).ToString())).ToList()
            If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                listDataRow.RemoveAll(Function(d) Not IsNumeric(d.Row.Item(8)))
                listErrorMessages.AddRange(listFilterErrors)
            End If

            listFilterErrors = listDataRow.Where(Function(d) Not IsNumeric(d.Row.Item(9))).Select(Function(d) String.Format("La unidad de medida del agrupador del registro {0} no es numerico", (d.IndexRow + 1).ToString())).ToList()
            If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                listDataRow.RemoveAll(Function(d) Not IsNumeric(d.Row.Item(9)))
                listErrorMessages.AddRange(listFilterErrors)
            End If

            listFilterErrors = listDataRow.Where(Function(d) Not {1, 2, 3, 4, 5, 6, 7}.Contains(d.Row.Item(9))).Select(Function(d) String.Format("La unidad de medida del agrupador del registro {0} no es un valor válido", (d.IndexRow + 1).ToString())).ToList()
            If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                listDataRow.RemoveAll(Function(d) Not {1, 2, 3, 4, 5, 6, 7}.Contains(d.Row.Item(9)))
                listErrorMessages.AddRange(listFilterErrors)
            End If

            listFilterErrors = listDataRow.Where(Function(d) Not IsNumeric(d.Row.Item(10))).Select(Function(d) String.Format("Advertir a partir del agrupador del registro {0} no es numerico", (d.IndexRow + 1).ToString())).ToList()
            If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                listDataRow.RemoveAll(Function(d) Not IsNumeric(d.Row.Item(10)))
                listErrorMessages.AddRange(listFilterErrors)
            End If

            listFilterErrors = listDataRow.Where(Function(d) String.IsNullOrEmpty(d.Row.Item(11))).Select(Function(d) String.Format("El mensaje a advertir a partir del agrupador del registro {0} esta vacío", (d.IndexRow + 1).ToString())).ToList()
            If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                listDataRow.RemoveAll(Function(d) String.IsNullOrEmpty(d.Row.Item(11)))
                listErrorMessages.AddRange(listFilterErrors)
            End If

            listFilterErrors = listDataRow.Where(Function(d) Not IsNumeric(d.Row.Item(12))).Select(Function(d) String.Format("Restringir en rango máximo del agrupador del registro {0} no es numerico", (d.IndexRow + 1).ToString())).ToList()
            If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                listDataRow.RemoveAll(Function(d) Not IsNumeric(d.Row.Item(12)))
                listErrorMessages.AddRange(listFilterErrors)
            End If

            listFilterErrors = listDataRow.Where(Function(d) Not {0, 1}.Contains(d.Row.Item(12))).Select(Function(d) String.Format("Restringir en rango máximo del agrupador del registro {0} no es un valor válido", (d.IndexRow + 1).ToString())).ToList()
            If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                listDataRow.RemoveAll(Function(d) Not {0, 1}.Contains(d.Row.Item(12)))
                listErrorMessages.AddRange(listFilterErrors)
            End If

            listFilterErrors = listDataRow.Where(Function(d) String.IsNullOrEmpty(d.Row.Item(13))).Select(Function(d) String.Format("El mensaje de la restriccion en rango máximo del agrupador del registro {0} esta vacío", (d.IndexRow + 1).ToString())).ToList()
            If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                listDataRow.RemoveAll(Function(d) String.IsNullOrEmpty(d.Row.Item(13)))
                listErrorMessages.AddRange(listFilterErrors)
            End If

            Dim listGrouperParentCode = listDataRow.Where(Function(d) Not String.IsNullOrEmpty(d.Row.Item(2))).Select(Function(d) d.Row.Item(2).ToString()).Distinct().ToList()
            Dim listGrouperParents = Me._groupersRepository.GetListGroupersPOCO(listGrouperParentCode)
            listFilterErrors = listDataRow.Where(Function(d) Not String.IsNullOrEmpty(d.Row.Item(2)) AndAlso Not listGrouperParents.Any(Function(c) c.Code = d.Row.Item(2))).Select(Function(d) String.Format("El codigo del agrupador padre del registro {0} no existe", (d.IndexRow + 1).ToString())).ToList()
            If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                listDataRow.RemoveAll(Function(d) Not String.IsNullOrEmpty(d.Row.Item(2)) AndAlso Not listGrouperParents.Any(Function(c) c.Code = d.Row.Item(2)))
                listErrorMessages.AddRange(listFilterErrors)
            End If

            If listDataRow.Any() Then
                For Each dataRow In listDataRow
                    Dim code = dataRow.Row.Item(0).ToString()
                    Dim parentId As Nullable(Of Integer) = Nothing
                    If Not String.IsNullOrEmpty(dataRow.Row.Item(2)) Then
                        parentId = listGrouperParents.Where(Function(d) d.Code = dataRow.Row.Item(2)).FirstOrDefault().Id
                    End If

                    Dim grouper = Me._groupersRepository.GetGroupersPOCO(code)
                    With grouper
                        .Code = code
                        .Description = dataRow.Row.Item(1).ToString()
                        .ParentId = parentId
                        .UserNumber = CInt(dataRow.Row.Item(3))
                        .UserMin = CInt(dataRow.Row.Item(4))
                        .UserMax = CInt(dataRow.Row.Item(5))
                        .ProjectCME = CDec(dataRow.Row.Item(6))
                        .Frequence = CDec(dataRow.Row.Item(7))
                        .TotalContract = CDec(dataRow.Row.Item(8))
                        .UserValue = If(.UserNumber = 0, 0, .TotalContract / .UserNumber)
                        .MeasurementUnit = CByte(dataRow.Row.Item(9))
                        .WarningFor = CInt(dataRow.Row.Item(10))
                        .WarningMessage = dataRow.Row.Item(11).ToString
                        .MaximunRangeRestrict = CBool(dataRow.Row.Item(12))
                        .RestrictMessage = dataRow.Row.Item(13)
                    End With

                    If grouper.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        grouper.CreationUser = audit.CodeUser
                        grouper.CreationDate = DateTime.Now
                    Else
                        grouper.ModificationUser = audit.CodeUser
                        grouper.ModificationDate = DateTime.Now
                    End If

                    Me._groupersRepository.SaveEntity(grouper)
                    Me._groupersRepository.UnitWork.Commit()

                    listSuccessMessages.Add(String.Format("El agrupador del registro {0} fue guardado correctamente", (dataRow.IndexRow + 1).ToString()))
                Next
            End If

            Return New ActionResult(Of List(Of String)) With {.ObjectEmbbeded = listSuccessMessages, .MessageResult = listErrorMessages}
        Catch ex As Exception
            listErrorMessages.Add(Utils.GetInnerExceptionMessageToString(ex))
            Return New ActionResult(Of List(Of String)) With {.ObjectEmbbeded = listSuccessMessages, .MessageResult = listErrorMessages}
        End Try
    End Function

    Private Function ImportGroupersCupsData(listDataRow As List(Of ImportFileRow), ByVal audit As AuditMessage) As ActionResult(Of List(Of String))
        Dim listErrorMessages As New List(Of String)
        Dim listSuccessMessages As New List(Of String)

        Try
            Dim listFilterErrors As New List(Of String)

            listFilterErrors = listDataRow.Where(Function(d) d.Row.Count < 3).Select(Function(d) String.Format("El registro de CUPS {0} no tiene una estructura válida", (d.IndexRow + 1).ToString())).ToList()
            If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                listDataRow.RemoveAll(Function(d) d.Row.Count < 3)
                listErrorMessages.AddRange(listFilterErrors)
            End If

            listFilterErrors = listDataRow.Where(Function(d) String.IsNullOrEmpty(d.Row.Item(0))).Select(Function(d) String.Format("El código del agrupador del registro de CUPS {0} esta vacío", (d.IndexRow + 1).ToString())).ToList()
            If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                listDataRow.RemoveAll(Function(d) String.IsNullOrEmpty(d.Row.Item(0)))
                listErrorMessages.AddRange(listFilterErrors)
            End If

            listFilterErrors = listDataRow.Where(Function(d) String.IsNullOrEmpty(d.Row.Item(1))).Select(Function(d) String.Format("El código CUPS del registro de CUPS {0} esta vacío", (d.IndexRow + 1).ToString())).ToList()
            If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                listDataRow.RemoveAll(Function(d) String.IsNullOrEmpty(d.Row.Item(1)))
                listErrorMessages.AddRange(listFilterErrors)
            End If

            Dim listGrouperCode = listDataRow.Select(Function(d) d.Row.Item(0).ToString()).Distinct().ToList()
            Dim listGroupers = Me._groupersRepository.GetListGroupersPOCO(listGrouperCode)
            listFilterErrors = listDataRow.Where(Function(d) Not listGroupers.Any(Function(c) c.Code = d.Row.Item(0))).Select(Function(d) String.Format("El codigo del agrupador del registro de CUPS {0} no existe", (d.IndexRow + 1).ToString())).ToList()
            If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                listDataRow.RemoveAll(Function(d) Not listGroupers.Any(Function(c) c.Code = d.Row.Item(0)))
                listErrorMessages.AddRange(listFilterErrors)
            End If

            Dim listCUPSCode = listDataRow.Select(Function(d) d.Row.Item(1).ToString()).Distinct().ToList()
            Dim listCUPS = Me._cupsEntityRepository.GetListCupsEntityWithContractDescriptions(listCUPSCode)
            listFilterErrors = listDataRow.Where(Function(d) Not listCUPS.Any(Function(c) c.Code = d.Row.Item(1))).Select(Function(d) String.Format("El codigo del CUPS del registro de CUPS {0} no existe", (d.IndexRow + 1).ToString())).ToList()
            If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                listDataRow.RemoveAll(Function(d) Not listCUPS.Any(Function(c) c.Code = d.Row.Item(1)))
                listErrorMessages.AddRange(listFilterErrors)
            End If

            listFilterErrors = listDataRow.Where(Function(d) String.IsNullOrEmpty(d.Row.Item(2)) AndAlso listCUPS.Any(Function(c) c.Code = d.Row.Item(1) AndAlso c.CUPSEntityContractDescriptions.Any(Function(cecd) cecd.IsDelete = 0))).
                Select(Function(d) String.Format("El CUPS del registro de CUPS {0} maneja descripción pero el campo esta vacío", (d.IndexRow + 1).ToString())).ToList()
            If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                listDataRow.RemoveAll(Function(d) String.IsNullOrEmpty(d.Row.Item(2)) AndAlso listCUPS.Any(Function(c) c.Code = d.Row.Item(1) AndAlso c.CUPSEntityContractDescriptions.Any(Function(cecd) cecd.IsDelete = 0)))
                listErrorMessages.AddRange(listFilterErrors)
            End If

            listFilterErrors = listDataRow.Where(Function(d) listCUPS.Any(Function(c) Not String.IsNullOrEmpty(d.Row.Item(2)) AndAlso c.Code = d.Row.Item(1) AndAlso Not c.CUPSEntityContractDescriptions.Any(Function(cecd) cecd.IsDelete = 0 AndAlso cecd.ContractDescriptions.Code = d.Row.Item(2)))).
                Select(Function(d) String.Format("El código de la descripción del registro de CUPS {0} no existe o no esta relacionada al cups", (d.IndexRow + 1).ToString())).ToList()
            If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                listDataRow.RemoveAll(Function(d) listCUPS.Any(Function(c) Not String.IsNullOrEmpty(d.Row.Item(2)) AndAlso c.Code = d.Row.Item(1) AndAlso Not c.CUPSEntityContractDescriptions.Any(Function(cecd) cecd.IsDelete = 0 AndAlso cecd.ContractDescriptions.Code = d.Row.Item(2))))
                listErrorMessages.AddRange(listFilterErrors)
            End If

            If listDataRow.Any() Then
                For Each dataRow In listDataRow
                    Dim grouper = listGroupers.Where(Function(d) d.Code = dataRow.Row.Item(0)).FirstOrDefault()
                    grouper = Me._groupersRepository.GetGroupersById(grouper.Id)
                    Dim cUPSEntity = listCUPS.Where(Function(d) d.Code = dataRow.Row.Item(1)).FirstOrDefault()
                    Dim cUPSEntityContractDescriptionId As Nullable(Of Integer)
                    Dim ContractDescriptionId As Nullable(Of Integer)
                    If cUPSEntity.CUPSEntityContractDescriptions.Any(Function(cecd) cecd.IsDelete = 0) Then
                        cUPSEntityContractDescriptionId = cUPSEntity.CUPSEntityContractDescriptions.Where(Function(cecd) cecd.IsDelete = 0 AndAlso cecd.ContractDescriptions.Code = dataRow.Row.Item(2)).FirstOrDefault().Id
                        ContractDescriptionId = cUPSEntity.CUPSEntityContractDescriptions.Where(Function(cecd) cecd.IsDelete = 0 AndAlso cecd.ContractDescriptions.Code = dataRow.Row.Item(2)).FirstOrDefault().ContractDescriptionId
                    End If

                    If grouper.GroupersCups.Any(Function(d) d.CUPSEntityId = cUPSEntity.Id AndAlso If(d.CUPSEntityContractDescriptionId Is Nothing, 0, d.CUPSEntityContractDescriptionId) = If(cUPSEntityContractDescriptionId Is Nothing, 0, cUPSEntityContractDescriptionId)) Then
                        listSuccessMessages.Add(String.Format("El CUPS del registro {0} ya fue agregado anteriormente", (dataRow.IndexRow + 1).ToString()))
                        Continue For
                    End If

                    grouper.ModificationUser = audit.CodeUser
                    grouper.ModificationDate = DateTime.Now

                    grouper.GroupersCups.Add(New GroupersCups With {
                        .CUPSEntityId = cUPSEntity.Id,
                        .CUPSEntityContractDescriptionId = cUPSEntityContractDescriptionId,
                        .ContractDescriptionId = ContractDescriptionId
                    })

                    If grouper.ChangeTracker.State <> Domain.Base.Entities.ObjectState.Added Then
                        grouper.ModificationUser = audit.CodeUser
                        grouper.ModificationDate = DateTime.Now
                    End If

                    Me._groupersRepository.SaveEntity(grouper)
                    Me._groupersRepository.UnitWork.Commit()

                    listSuccessMessages.Add(String.Format("El CUPS del registro {0} fue guardado correctamente", (dataRow.IndexRow + 1).ToString()))
                Next
            End If

            Return New ActionResult(Of List(Of String)) With {.ObjectEmbbeded = listSuccessMessages, .MessageResult = listErrorMessages}
        Catch ex As Exception
            listErrorMessages.Add(Utils.GetInnerExceptionMessageToString(ex))
            Return New ActionResult(Of List(Of String)) With {.ObjectEmbbeded = listSuccessMessages, .MessageResult = listErrorMessages}
        End Try
    End Function

    Private Function ImportGroupersActivitiesData(listDataRow As List(Of ImportFileRow), ByVal audit As AuditMessage) As ActionResult(Of List(Of String))
        Dim listErrorMessages As New List(Of String)
        Dim listSuccessMessages As New List(Of String)

        Try
            Dim listFilterErrors As New List(Of String)

            listFilterErrors = listDataRow.Where(Function(d) d.Row.Count < 2).Select(Function(d) String.Format("El registro de Actividad {0} no tiene una estructura válida", (d.IndexRow + 1).ToString())).ToList()
            If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                listDataRow.RemoveAll(Function(d) d.Row.Count < 2)
                listErrorMessages.AddRange(listFilterErrors)
            End If

            listFilterErrors = listDataRow.Where(Function(d) String.IsNullOrEmpty(d.Row.Item(0))).Select(Function(d) String.Format("El código del agrupador del registro de Actividad {0} esta vacío", (d.IndexRow + 1).ToString())).ToList()
            If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                listDataRow.RemoveAll(Function(d) String.IsNullOrEmpty(d.Row.Item(0)))
                listErrorMessages.AddRange(listFilterErrors)
            End If

            listFilterErrors = listDataRow.Where(Function(d) String.IsNullOrEmpty(d.Row.Item(1))).Select(Function(d) String.Format("El código Actividad del registro de Actividad {0} esta vacío", (d.IndexRow + 1).ToString())).ToList()
            If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                listDataRow.RemoveAll(Function(d) String.IsNullOrEmpty(d.Row.Item(1)))
                listErrorMessages.AddRange(listFilterErrors)
            End If

            Dim listGrouperCode = listDataRow.Select(Function(d) d.Row.Item(0).ToString()).Distinct().ToList()
            Dim listGroupers = Me._groupersRepository.GetListGroupersPOCO(listGrouperCode)
            listFilterErrors = listDataRow.Where(Function(d) Not listGroupers.Any(Function(c) c.Code = d.Row.Item(0))).Select(Function(d) String.Format("El codigo del agrupador del registro de Actividad {0} no existe", (d.IndexRow + 1).ToString())).ToList()
            If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                listDataRow.RemoveAll(Function(d) Not listGroupers.Any(Function(c) c.Code = d.Row.Item(0)))
                listErrorMessages.AddRange(listFilterErrors)
            End If

            Dim listActivityCode = listDataRow.Select(Function(d) d.Row.Item(1).ToString()).Distinct().ToList()
            Dim listActivity = Me._activityRepository.GetListAGACTIMEDPOCO(listActivityCode)
            listFilterErrors = listDataRow.Where(Function(d) Not listActivity.Any(Function(c) c.CODACTMED = d.Row.Item(1))).Select(Function(d) String.Format("El codigo de la actividad del registro de Actividad {0} no existe", (d.IndexRow + 1).ToString())).ToList()
            If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                listDataRow.RemoveAll(Function(d) Not listActivity.Any(Function(c) c.CODACTMED = d.Row.Item(1)))
                listErrorMessages.AddRange(listFilterErrors)
            End If

            If listDataRow.Any() Then
                For Each dataRow In listDataRow
                    Dim grouper = listGroupers.Where(Function(d) d.Code = dataRow.Row.Item(0)).FirstOrDefault()
                    grouper = Me._groupersRepository.GetGroupersById(grouper.Id)
                    Dim activity = listActivity.Where(Function(d) d.CODACTMED = dataRow.Row.Item(1)).FirstOrDefault()

                    If grouper.GroupersActivities.Any(Function(d) d.AGACTIMEDCode = activity.CODACTMED) Then
                        listSuccessMessages.Add(String.Format("La actividad del registro {0} ya fue agregado anteriormente", (dataRow.IndexRow + 1).ToString()))
                        Continue For
                    End If

                    grouper.ModificationUser = audit.CodeUser
                    grouper.ModificationDate = DateTime.Now

                    grouper.GroupersActivities.Add(New GroupersActivities With {
                        .AGACTIMEDCode = activity.CODACTMED,
                        .AGACTIMEDName = activity.DESACTMED
                    })

                    If grouper.ChangeTracker.State <> Domain.Base.Entities.ObjectState.Added Then
                        grouper.ModificationUser = audit.CodeUser
                        grouper.ModificationDate = DateTime.Now
                    End If

                    Me._groupersRepository.SaveEntity(grouper)
                    Me._groupersRepository.UnitWork.Commit()

                    listSuccessMessages.Add(String.Format("La actividad del registro {0} fue guardado correctamente", (dataRow.IndexRow + 1).ToString()))
                Next
            End If

            Return New ActionResult(Of List(Of String)) With {.ObjectEmbbeded = listSuccessMessages, .MessageResult = listErrorMessages}
        Catch ex As Exception
            listErrorMessages.Add(Utils.GetInnerExceptionMessageToString(ex))
            Return New ActionResult(Of List(Of String)) With {.ObjectEmbbeded = listSuccessMessages, .MessageResult = listErrorMessages}
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
            _groupersRepository = Nothing
            _cupsEntityRepository = Nothing
            _activityRepository = Nothing
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
