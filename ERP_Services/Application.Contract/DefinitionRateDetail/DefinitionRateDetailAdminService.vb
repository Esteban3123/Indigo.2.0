'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 26/08/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Collections.Concurrent
Imports System.Data.Entity.Core
Imports System.Text
Imports Application.Base
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Crystal
Imports Domain.Crystal.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Resources

Public Class DefinitionRateDetailAdminService
    Implements IDefinitionRateDetailAdminService

#Region "Variables"

    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _definitionRateDetailRepository As IDefinitionRateDetailRepository

    ''' <summary>
    ''' Repositorio de especialidad
    ''' </summary>
    ''' <remarks></remarks>
    Private _specialtyRepository As ISpecialityRepository

#End Region

#Region "Builder"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal definitionRateDetailRepository As IDefinitionRateDetailRepository, ByVal specialtyRepository As ISpecialityRepository)
        If definitionRateDetailRepository Is Nothing Then
            Throw New ArgumentNullException("definitionRateDetailRepository Vacio")
        End If
        _definitionRateDetailRepository = definitionRateDetailRepository
        _specialtyRepository = specialtyRepository
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Elimina la entidad
    ''' </summary>
    ''' <param name="SurgicalGroup"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteDefinitionRateDetail(DefinitionRateDetail As DefinitionRateDetail, audit As AuditMessage) As ActionResult Implements IDefinitionRateDetailAdminService.DeleteDefinitionRateDetail
        If DefinitionRateDetail Is Nothing Then
            Throw New ArgumentNullException("DefinitionRateDetail")
        End If
        Dim unitOfWork As IUnitWork = Me._definitionRateDetailRepository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of DefinitionRateDetail)
            auditProcess = New IndigoAuditSimpleEntity(Of DefinitionRateDetail)(DefinitionRateDetail, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            Me._definitionRateDetailRepository.DeleteEntity(DefinitionRateDetail)
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
    ''' Obtiene la entidad por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDefinitionRateDetailById(id As Integer, audit As AuditMessage) As ActionResult(Of DefinitionRateDetail) Implements IDefinitionRateDetailAdminService.GetDefinitionRateDetailById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim DefinitionRateDetail As DefinitionRateDetail = Me._definitionRateDetailRepository.GetDefinitionRateDetailById(id)
            If DefinitionRateDetail IsNot Nothing AndAlso DefinitionRateDetail.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of DefinitionRateDetail)(DefinitionRateDetail, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of DefinitionRateDetail) With {.StateResult = True, .ObjectEmbbeded = DefinitionRateDetail}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of DefinitionRateDetail) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Guarda o actualiza la entidad
    ''' </summary>
    ''' <param name="SurgicalGroup"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveDefinitionRateDetail(DefinitionRateDetail As DefinitionRateDetail, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of DefinitionRateDetail) Implements IDefinitionRateDetailAdminService.SaveDefinitionRateDetail
        If DefinitionRateDetail Is Nothing Then
            Throw New ArgumentNullException("DefinitionRateDetail")
        End If
        Dim unitOfWork As IUnitWork = Me._definitionRateDetailRepository.UnitWork
        Try
            Dim auxDefinitionRateDetail As DefinitionRateDetail = Nothing
            Dim auditProcess As IndigoAuditSimpleEntity(Of DefinitionRateDetail)
            Dim status As Integer

            Me._definitionRateDetailRepository.SaveEntity(DefinitionRateDetail)
            unitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of DefinitionRateDetail)(DefinitionRateDetail, audit, status, auxDefinitionRateDetail)
            auditProcess.Execute()

            'Se marca la entidad como sin cambios
            DefinitionRateDetail.MarkAsUnchanged()

            Return New ActionResult(Of DefinitionRateDetail) With {.StateResult = True, .ObjectEmbbeded = DefinitionRateDetail}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of DefinitionRateDetail) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of DefinitionRateDetail) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene el listado de condiciones del detalle de la definicion de tarifa
    ''' </summary>
    ''' <param name="definitionRateDetailId"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListDefinitionRateDetailConditionByDefinitionRateDetailId(definitionRateDetailId As Integer, audit As AuditMessage) As ActionResult(Of List(Of DefinitionRateDetailCondition)) Implements IDefinitionRateDetailAdminService.GetListDefinitionRateDetailConditionByDefinitionRateDetailId
        If definitionRateDetailId = 0 Then
            Throw New ArgumentNullException("definitionRateDetailId")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim ListDefinitionRateDetailCondition As List(Of DefinitionRateDetailCondition) = Me._definitionRateDetailRepository.GetListDefinitionRateDetailConditionByDefinitionRateDetailId(definitionRateDetailId)
            If ListDefinitionRateDetailCondition IsNot Nothing AndAlso ListDefinitionRateDetailCondition.Count > 0 Then
                For Each item As DefinitionRateDetailCondition In ListDefinitionRateDetailCondition
                    'Especialidad
                    If item.SpecialtyId IsNot Nothing Then
                        Dim specialty = _specialtyRepository.GetSpecialityByCode(item.SpecialtyId)
                        item.FirstCondition = item.FirstCondition + " (" + specialty.CODESPECI + " - " + specialty.DESESPECI.Trim + ")"
                        item.SpecialtyDescriptionFirst = specialty.CODESPECI + " - " + specialty.DESESPECI.Trim
                    End If
                    If item.SpecialtyId2 IsNot Nothing Then
                        Dim specialty = _specialtyRepository.GetSpecialityByCode(item.SpecialtyId2)
                        item.SecondCondition = item.SecondCondition + " (" + specialty.CODESPECI + " - " + specialty.DESESPECI.Trim + ")"
                        item.SpecialtyDescriptionSecond = specialty.CODESPECI + " - " + specialty.DESESPECI.Trim
                    End If

                    'RIAS
                    If item.RIASId IsNot Nothing Then
                        Dim rias = _specialtyRepository.GetRIASById(item.RIASId)
                        item.FirstCondition = item.FirstCondition + " (" + rias.CODPRO + " - " + rias.NOMBRE.Trim + ")"
                        item.RIASDescriptionFirst = rias.CODPRO + " - " + rias.NOMBRE.Trim
                    End If
                    If item.RIASId2 IsNot Nothing Then
                        Dim rias = _specialtyRepository.GetRIASById(item.RIASId2)
                        item.SecondCondition = item.SecondCondition + " (" + rias.CODPRO + " - " + rias.NOMBRE.Trim + ")"
                        item.RIASDescriptionSecond = rias.CODPRO + " - " + rias.NOMBRE.Trim
                    End If

                    If item.LogicOperator = 1 Then 'Ninguno
                        item.ConditionName = item.FirstCondition
                    Else 'Y u O
                        item.ConditionName = item.FirstCondition + " " + ResourceManager.GetString("LogicalOperator" & item.LogicOperator.ToString(), "Contract") + " " + item.SecondCondition
                    End If
                Next
            End If
            Return New ActionResult(Of List(Of DefinitionRateDetailCondition)) With {.StateResult = True, .ObjectEmbbeded = ListDefinitionRateDetailCondition}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of DefinitionRateDetailCondition)) With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' exporta la estructura detalle de la definicion de tarifa de forma Limpia (solo las columnas)
    ''' </summary>
    ''' <returns></returns>
    Public Function ExportCleanStructure() As ActionResult(Of Byte()) Implements IDefinitionRateDetailAdminService.ExportCleanStructure
        Try
            Dim listSheet = New List(Of ExcelSheetData)
            Dim excelSheet = New ExcelSheetData With {.Name = "Estructura Detalle", .Columns = New List(Of ExcelDataColumn) From {New ExcelDataColumn With {.Name = "Regla",
                                                                                                                                            .Comment = "Especifica el tipo de regla de la tarifa" & vbCrLf &
                                                                                                                                                            "1 - Servicio IPS" & vbCrLf &
                                                                                                                                                            "2 - CUPS" & vbCrLf &
                                                                                                                                                            "3 - SubGrupos CUPS" & vbCrLf &
                                                                                                                                                            "4 - Grupo CUPS" & vbCrLf &
                                                                                                                                                            "5 - General",
                                                                                                                                            .Type = ExcelColumnFormat.Number},
                                                                                                                        New ExcelDataColumn With {.Name = "Servicio IPS",
                                                                                                                                              .Comment = "Este campo se diligencia solo si la Regla es Servicio IPS, corresponde al Codigo Servicio IPS Padre del Manual Tarifario",
                                                                                                                                              .Type = ExcelColumnFormat.Text},
                                                                                                                        New ExcelDataColumn With {.Name = "Servicio Px Qx",
                                                                                                                                              .Comment = "Este campo solo se diligencia solo cuando se cumplen 2 escenarios:" & vbCrLf &
                                                                                                                                                            "a) Cuando el Tipo de Regla es 1 - Servicio IPS " & vbCrLf &
                                                                                                                                                            "b) Cuando el campo anterior Servicio Ips es Padre, es decir su Presentación es Quirurgico." & vbCrLf &
                                                                                                                                                            "Este código corresponde al Servicio IPS hijo (Honorario, anestesiólogo, ayudante, sala, materiales)",
                                                                                                                                              .Type = ExcelColumnFormat.Text},
                                                                                                                        New ExcelDataColumn With {.Name = "CUPS",
                                                                                                                                              .Comment = "Código CUPS, se diligencia si el tipo de Regla es 1 o 2",
                                                                                                                                              .Type = ExcelColumnFormat.Text},
                                                                                                                        New ExcelDataColumn With {.Name = "SubGrupo Cups",
                                                                                                                                              .Comment = "Código de Subgrupo Cups, se diligencia si el tipo de Regla es 3",
                                                                                                                                              .Type = ExcelColumnFormat.Text},
                                                                                                                        New ExcelDataColumn With {.Name = "Grupo Cups",
                                                                                                                                              .Comment = "Código de Grupo Cups, se diligencia si el tipo de Regla es 4",
                                                                                                                                              .Type = ExcelColumnFormat.Text},
                                                                                                                        New ExcelDataColumn With {.Name = "Condicion1",
                                                                                                                                              .Comment = "Especifica la primera condición de la liquidación" & vbCrLf &
                                                                                                                                                            "1 - Horario" & vbCrLf &
                                                                                                                                                            "2 - Especialidad" & vbCrLf &
                                                                                                                                                            "3 - Unidad Funcional" & vbCrLf &
                                                                                                                                                            "4 - Tipo de Unidad " & vbCrLf &
                                                                                                                                                            "5 - Ninguna " & vbCrLf &
                                                                                                                                                            "6 - RIAS" & vbCrLf &
                                                                                                                                                            "7 - Descripción",
                                                                                                                                              .Type = ExcelColumnFormat.Number},
                                                                                                                        New ExcelDataColumn With {.Name = "Operador",
                                                                                                                                              .Comment = "Especifica el Operador Lógico entre las Condiciones" & vbCrLf &
                                                                                                                                                            "1 - Ninguna" & vbCrLf &
                                                                                                                                                            "2 - 'Y'" & vbCrLf &
                                                                                                                                                            "3 - 'O'" & vbCrLf &
                                                                                                                                                            "Nota: Cuando el operador lógico es 1 quiere decir que solo maneja una condición de lo contrario son dos condiciones",
                                                                                                                                              .Type = ExcelColumnFormat.Number},
                                                                                                                        New ExcelDataColumn With {.Name = "Condicion2",
                                                                                                                                              .Comment = "Especifica la segunda condición de la liquidación" & vbCrLf &
                                                                                                                                                            "1 - Horario " & vbCrLf &
                                                                                                                                                            "2 - Especialidad" & vbCrLf &
                                                                                                                                                            "3 - Unidad Funcional " & vbCrLf &
                                                                                                                                                            "4 - Tipo de Unidad" & vbCrLf &
                                                                                                                                                            "5 - Ninguna " & vbCrLf &
                                                                                                                                                            "6 - RIAS" & vbCrLf &
                                                                                                                                                            "7 - Descripción" & vbCrLf &
                                                                                                                                                            "Nota: No puede ser igual a la Condición1",
                                                                                                                                              .Type = ExcelColumnFormat.Number},
                                                                                                                        New ExcelDataColumn With {.Name = "Peso",
                                                                                                                                              .Comment = "Especifica el Peso de la condición el cual debe ser de 1 a 10 siendo el numero 10 el más pesado es decir el primero que se va a evaluar 
                                                                                                                                                          Nota: Cuando la condición de liquidación es Ninguna pero va a ser siempre de 0",
                                                                                                                                              .Type = ExcelColumnFormat.Number},
                                                                                                                        New ExcelDataColumn With {.Name = "Permite Ajustar Valor",
                                                                                                                                              .Comment = "Valores 1 = Si, 0 = No, este campo especifica si permite cambiar los valores cuando se está facturando ",
                                                                                                                                              .Type = ExcelColumnFormat.Number},
                                                                                                                        New ExcelDataColumn With {.Name = "Tipo Liquidacion",
                                                                                                                                              .Comment = "Especifica el tipo de liquidación" & vbCrLf &
                                                                                                                                                            "1 - Fija" & vbCrLf &
                                                                                                                                                            "2 - Estándar" & vbCrLf &
                                                                                                                                                            "3 - Vigencia" & vbCrLf &
                                                                                                                                                            "Nota: Este campo solo se llena si la condicion es ninguna",
                                                                                                                                              .Type = ExcelColumnFormat.Number},
                                                                                                                        New ExcelDataColumn With {.Name = "Tipo de Manual",
                                                                                                                                              .Comment = "Tipo del manual tarifario" & vbCrLf &
                                                                                                                                                            "1 - ISS 2001" & vbCrLf &
                                                                                                                                                            "2 - ISS 2004 " & vbCrLf &
                                                                                                                                                            "3 - SOAT " & vbCrLf &
                                                                                                                                                            "4 - Institucional" & vbCrLf &
                                                                                                                                                            "Nota: solo se solicita si el tipo de liquidación es fija",
                                                                                                                                              .Type = ExcelColumnFormat.Number},
                                                                                                                        New ExcelDataColumn With {.Name = "Valor",
                                                                                                                                              .Comment = "Valor del servicio" & vbCrLf &
                                                                                                                                                          "Nota : este campo solo se llena si el tipo de liquidación es Fija",
                                                                                                                                              .Type = ExcelColumnFormat.Number},
                                                                                                                        New ExcelDataColumn With {.Name = "Vr con Recargo",
                                                                                                                                              .Comment = "Valor con recargo que se va cobrar " & vbCrLf &
                                                                                                                                                          "Nota : este campo solo se llena si el tipo de liquidación es Fija",
                                                                                                                                              .Type = ExcelColumnFormat.Number},
                                                                                                                        New ExcelDataColumn With {.Name = "Manual Tarifario",
                                                                                                                                              .Comment = "Código del manual tarifario " & vbCrLf &
                                                                                                                                                         "Nota: Este campo solo se llena para los tipos de liquidación 1-Fija y 2-Estandar, a partir de este campo se definen porcentajes a liquidar en eventos quirurgicos",
                                                                                                                                              .Type = ExcelColumnFormat.Text},
                                                                                                                        New ExcelDataColumn With {.Name = "Variacion",
                                                                                                                                              .Comment = "Porcentaje de la variación de la tarifa" & vbCrLf &
                                                                                                                                                          "Nota: Este campo solo se llena si el tipo de liquidación es 2-Estándar o 3-Vigencia",
                                                                                                                                              .Type = ExcelColumnFormat.Number},
                                                                                                                        New ExcelDataColumn With {.Name = "Vigencia",
                                                                                                                                              .Comment = "Código de la vigencia del manual tarifario" & vbCrLf &
                                                                                                                                                         "Nota: Este campo solo se llena si el tipo de liquidación es 3-Vigencia",
                                                                                                                                              .Type = ExcelColumnFormat.Text}}}
            listSheet.Add(excelSheet)
            Dim arrayExcelBytes = Utils.ExportDataToExcel(listSheet)

            If arrayExcelBytes Is Nothing Then
                Return New ActionResult(Of Byte()) With {.StateResult = False, .Message = "Error en la generación del documento"}
            End If

            Return New ActionResult(Of Byte()) With {.StateResult = True, .ObjectEmbbeded = arrayExcelBytes, .Message = "Generación exitosa"}
        Catch ex As Exception
            Return New ActionResult(Of Byte()) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' funcion que lee el archivo importado de excel de la estructura detalle para agregar a la rejilla
    ''' </summary>
    ''' <param name="dataImportFile"></param>
    ''' <returns></returns>
    Public Function ImportDataToAdd(dataImportFile As List(Of ImportFileRow), audit As AuditMessage) As ActionResult(Of List(Of DefinitionRateDetail)) Implements IDefinitionRateDetailAdminService.ImportDataToAdd
        Try
            If dataImportFile Is Nothing OrElse Not dataImportFile.Any() Then
                Throw New ArgumentNullException(NameOf(dataImportFile), "El parámetro no puede venir vacio")
            End If

            Dim listCodeIPSService As New HashSet(Of String)
            Dim listCodeServiceQx As New HashSet(Of String)
            Dim listCodeCUPS As New HashSet(Of String)
            Dim listCodeSubGroupCUPS As New HashSet(Of String)
            Dim listCodeGroupCUPS As New HashSet(Of String)
            Dim listCodeRateManual As New HashSet(Of String)
            Dim listCodeRateManualValidity As New HashSet(Of String)
            Dim indexRowToRemove As New HashSet(Of Integer)
            Dim listErrors As New List(Of String)

            For Each item In dataImportFile

                If item.Row.All(Function(y) String.IsNullOrEmpty(y)) Then
                    listErrors.Add($"Error en la fila {item.IndexRow}, esta vacía")
                    indexRowToRemove.Add(item.IndexRow)
                    Continue For
                End If

                Dim resultValidate = ValidateStructure(item)

                If resultValidate Is Nothing OrElse Not resultValidate.StateResult Then
                    listErrors.Add($"Error en la fila {item.IndexRow}: {resultValidate?.Message}")
                    indexRowToRemove.Add(item.IndexRow)
                    Continue For
                End If

                If dataImportFile.Exists(Function(x)
                                             Dim flag As Boolean = False

                                             'se comparan solo items que tengan la misma regla
                                             If CInt(item.Row.Item(0)) <> CInt(x.Row.Item(0)) Then
                                                 Return False
                                             End If

                                             Select Case CInt(item.Row.Item(0))
                                                 Case EDefinitionRateRuleType.IPSService
                                                     'se valida que el servicio IPS ya no este en otra linea, y si tiene qx valida contra registro que tenga qx o si es vacio valida contra registro que No tenga qx
                                                     flag = (x.Row.Item(1).ToString() = item.Row.Item(1).ToString()) _
                                                            AndAlso ((Not String.IsNullOrEmpty(item.Row.Item(2)) _
                                                                        AndAlso Not String.IsNullOrEmpty(x.Row.Item(2)) _
                                                                        AndAlso (x.Row.Item(2) = item.Row.Item(2))) _
                                                            OrElse (String.IsNullOrEmpty(x.Row.Item(2)) _
                                                                        AndAlso String.IsNullOrEmpty(item.Row.Item(2)) _
                                                                        AndAlso ((String.IsNullOrEmpty(item.Row.Item(3)) _
                                                                        AndAlso String.IsNullOrEmpty(x.Row.Item(3))) _
                                                            OrElse (item.Row.Item(3).ToString() = x.Row.Item(3).ToString()))))

                                                 Case EDefinitionRateRuleType.CUPS
                                                     flag = x.Row.Item(3).ToString() = item.Row.Item(3).ToString()
                                                 Case EDefinitionRateRuleType.SubGroupsCUPS
                                                     flag = x.Row.Item(4).ToString() = item.Row.Item(4).ToString()
                                                 Case EDefinitionRateRuleType.GroupCUPS
                                                     flag = x.Row.Item(5).ToString() = item.Row.Item(5).ToString()
                                             End Select

                                             'se determina si ya no fue procesado (para no validarlo nuevamente), se vaida que no tome el registro actual, ademas que las condiciones No se repitan
                                             Return Not indexRowToRemove.Contains(x.IndexRow) _
                                                    AndAlso x.IndexRow <> item.IndexRow _
                                                    AndAlso flag _
                                                    AndAlso ((Not String.IsNullOrEmpty(item.Row.Item(6)) AndAlso (CInt(x.Row.Item(6)) = CInt(item.Row.Item(6)))) _
                                                            OrElse (Not String.IsNullOrEmpty(item.Row.Item(8)) AndAlso (CInt(x.Row.Item(8)) = CInt(item.Row.Item(8)))))
                                         End Function) Then
                    listErrors.Add($"Error en la fila {item.IndexRow}: está duplicado en el listado")
                    indexRowToRemove.Add(item.IndexRow)
                    Continue For
                End If

                If Not String.IsNullOrEmpty(item.Row.Item(1)) Then
                    listCodeIPSService.Add(item.Row.Item(1).ToString())
                End If

                If Not String.IsNullOrEmpty(item.Row.Item(2)) Then
                    listCodeServiceQx.Add(item.Row.Item(2).ToString())
                End If

                If Not String.IsNullOrEmpty(item.Row.Item(3)) Then
                    listCodeCUPS.Add(item.Row.Item(3).ToString())
                End If

                If Not String.IsNullOrEmpty(item.Row.Item(4)) Then
                    listCodeSubGroupCUPS.Add(item.Row.Item(4).ToString())
                End If

                If Not String.IsNullOrEmpty(item.Row.Item(5)) Then
                    listCodeGroupCUPS.Add(item.Row.Item(5).ToString())
                End If

                If Not String.IsNullOrEmpty(item.Row.Item(15)) Then
                    listCodeRateManual.Add(item.Row.Item(15).ToString())
                End If

                If Not String.IsNullOrEmpty(item.Row.Item(17)) Then
                    listCodeRateManualValidity.Add(item.Row.Item(17).ToString())
                End If
            Next

            'se eliminan los registros que No pasaron validacion 
            dataImportFile.RemoveAll(Function(x) indexRowToRemove.Contains(x.IndexRow))

            'se crea el objeto para consultar los codigos necesarios de forma masiva y asincrona 
            Dim ObjectToQuery = New DefinitionDetailQuery With {.ListOfCodes = New Dictionary(Of String, List(Of String)) _
                                                                                From {{EObjectTypeDefinition.IPSService.ToString(), listCodeIPSService.ToList()},
                                                                                      {EObjectTypeDefinition.IPSServiceQx.ToString(), listCodeServiceQx.ToList()},
                                                                                      {EObjectTypeDefinition.CUPS.ToString(), listCodeCUPS.ToList()},
                                                                                      {EObjectTypeDefinition.SubGroupsCUPS.ToString(), listCodeSubGroupCUPS.ToList()},
                                                                                      {EObjectTypeDefinition.GroupCUPS.ToString(), listCodeGroupCUPS.ToList()},
                                                                                      {EObjectTypeDefinition.RateManual.ToString(), listCodeRateManual.ToList()},
                                                                                      {EObjectTypeDefinition.RateManualValidity.ToString(), listCodeRateManualValidity.ToList()}}}

            Dim definitionDetailQuery = _definitionRateDetailRepository.GetQueryToImportData(ObjectToQuery)

            Dim listConcurrentBag = New ConcurrentBag(Of DefinitionRateDetail)
            Dim listErrorsConcurentBag = New ConcurrentBag(Of String)
            Parallel.ForEach(dataImportFile, Sub(row)

                                                 Dim indexRow = row.IndexRow
                                                 Dim ruleType As Integer = CInt(row.Row.Item(0))
                                                 Dim condition1 As Integer? = CInt(row.Row.Item(6))
                                                 Dim fOperator As Integer? = If(condition1 <> 5, CInt(row.Row.Item(7)), 1)
                                                 Dim condition2 As Integer? = If(fOperator <> 1, CInt(row.Row.Item(8)), 5)
                                                 Dim weight As Integer = CInt(row.Row.Item(9))
                                                 Dim allowValueChange As Boolean = CBool(row.Row.Item(10))

                                                 Dim liquidationType As Integer? = Nothing
                                                 Dim manualType As Integer? = Nothing
                                                 Dim salesValue As Decimal? = Nothing
                                                 Dim salesValueWithSurcharge As Decimal? = Nothing
                                                 Dim rateVariation As Decimal? = Nothing

                                                 If condition1 = 5 Then
                                                     liquidationType = CInt(row.Row.Item(11))
                                                 End If

                                                 If condition1 = 5 AndAlso liquidationType = 1 Then
                                                     manualType = CInt(row.Row.Item(12))
                                                     salesValue = CDec(row.Row.Item(13))
                                                     salesValueWithSurcharge = CDec(row.Row.Item(14))
                                                 End If

                                                 If condition1 = 5 AndAlso liquidationType > 1 Then
                                                     rateVariation = CDec(row.Row.Item(16))
                                                 End If

                                                 Dim serviceIPS = TryCast(definitionDetailQuery.ListOfObjects(EObjectTypeDefinition.IPSService.ToString()), List(Of IPSService)) _
                                                                                                            .Find(Function(x) x.Code = row.Row.Item(1)?.ToString())

                                                 Dim serviceIPSQx = TryCast(definitionDetailQuery.ListOfObjects(EObjectTypeDefinition.IPSServiceQx.ToString()), List(Of IPSService)) _
                                                                                                            .Find(Function(x) x.Code = row.Row.Item(2)?.ToString())

                                                 Dim cups = TryCast(definitionDetailQuery.ListOfObjects(EObjectTypeDefinition.CUPS.ToString()), List(Of CUPSEntity)) _
                                                                                                            .Find(Function(x) x.Code = row.Row.Item(3)?.ToString())

                                                 Dim subGroupCUPS = TryCast(definitionDetailQuery.ListOfObjects(EObjectTypeDefinition.SubGroupsCUPS.ToString()), List(Of CupsSubgroup)) _
                                                                                                            .Find(Function(x) x.Code = row.Row.Item(4)?.ToString())

                                                 Dim groupCUPS = TryCast(definitionDetailQuery.ListOfObjects(EObjectTypeDefinition.GroupCUPS.ToString()), List(Of CupsGroup)) _
                                                                                                            .Find(Function(x) x.Code = row.Row.Item(5)?.ToString())

                                                 Dim rateManual = TryCast(definitionDetailQuery.ListOfObjects(EObjectTypeDefinition.RateManual.ToString()), List(Of RateManual)) _
                                                                                                            .Find(Function(x) x.Code = row.Row.Item(15)?.ToString())

                                                 Dim rateManualValidity = TryCast(definitionDetailQuery.ListOfObjects(EObjectTypeDefinition.RateManualValidity.ToString()), List(Of RateManualValidity)) _
                                                                                                            .Find(Function(x) x.Code = row.Row.Item(17)?.ToString())

                                                 Dim tupleRuleType = New Tuple(Of Integer, IPSService, IPSService, CUPSEntity, CupsSubgroup, CupsGroup) _
                                                                                                                            (ruleType, serviceIPS, serviceIPSQx, cups, subGroupCUPS, groupCUPS)

                                                 'se valida que el codigo asociado al tipo de regla exista o este activo 
                                                 Dim validate = ValidateRuleType(tupleRuleType)
                                                 If validate Is Nothing OrElse Not validate.StateResult Then
                                                     listErrorsConcurentBag.Add($"Error en la fila {row.IndexRow}: {validate?.Message}")
                                                     Exit Sub
                                                 End If

                                                 'se reasigna las entidades que correspondan 
                                                 serviceIPS = tupleRuleType.Item2
                                                 serviceIPSQx = tupleRuleType.Item3
                                                 cups = tupleRuleType.Item4
                                                 subGroupCUPS = tupleRuleType.Item5
                                                 groupCUPS = tupleRuleType.Item6

                                                 Dim definitionRateDetail = New DefinitionRateDetail
                                                 With definitionRateDetail
                                                     .RuleType = ruleType
                                                     .IPSServiceId = serviceIPS?.Id
                                                     .CUPSEntityId = cups?.Id
                                                     .CUPSSubgroupId = subGroupCUPS?.Id
                                                     .CUPSGroupId = groupCUPS?.Id
                                                     .ConditionType = condition1
                                                     .LogicalOperator = fOperator
                                                     .ConditionType2 = condition2
                                                     .Weight = weight
                                                     .AllowValueChange = allowValueChange
                                                     .LiquidationType = liquidationType
                                                     .ManualType = manualType
                                                     .SalesValue = salesValue
                                                     .SalesValueWithSurcharge = salesValueWithSurcharge
                                                     .RateManualId = rateManual?.Id
                                                     .RateManualDescription = $"{rateManual?.Code} - {rateManual?.Name}"
                                                     .RateVariation = rateVariation
                                                     .RateManualValidityId = rateManualValidity?.Id
                                                     .RateManualValidityDescription = $"{rateManualValidity?.Code} - {rateManualValidity?.Name}"
                                                     .ConditionName = .GetConditionTypeName()
                                                     .RuleTypeName = $"0{ruleType} - { .GetRuleTypeName}"
                                                     .IPSServiceCodeName = $"{serviceIPS?.Code} - {serviceIPS?.Name}"
                                                     .CUPSEntityCodeDescription = $"{cups?.Code} - {cups?.Description}"
                                                     .CUPSSubGroupCodeName = $"{subGroupCUPS?.Code} - {subGroupCUPS?.Name}"
                                                     .CUPSGroupCodeName = $"{groupCUPS?.Code} - {groupCUPS?.Name}"
                                                     Dim ruleDesc As String = .GetRuleDescription()
                                                     Dim idx As Integer = If(ruleDesc?.IndexOf("-"), -1)
                                                     .RuleDescription = If(ruleDesc?.Contains("-") AndAlso idx >= 0 _
                                                                        AndAlso Not String.IsNullOrEmpty(cups?.Code),
                                                                        ruleDesc.Substring(0, idx).Trim() & " - " & cups.Code & " - " &
                                                                        ruleDesc.Substring(idx + 1).Trim(), ruleDesc)
                                                 End With

                                                 If serviceIPSQx IsNot Nothing Then
                                                     Dim match = serviceIPS?.SurgicalProcedureService?.FirstOrDefault(Function(x) x.IPSServiceId = serviceIPSQx.Id)

                                                     If match IsNot Nothing Then
                                                         Dim surgicalProcedure = New DefinitionRateDetailSurgicalProcedures With {
                                                                .IPSServiceId = serviceIPSQx.Id,
                                                                .QxClassName = serviceIPSQx.ServiceClassName,
                                                                .QxCode = serviceIPSQx.Code,
                                                                .QxName = serviceIPSQx.Name,
                                                                .SurgicalProcedureServiceId = match.Id
                                                         }
                                                         definitionRateDetail.DefinitionRateDetailSurgicalProcedures.Add(surgicalProcedure)
                                                     End If
                                                 End If
                                                 listConcurrentBag.Add(definitionRateDetail)

                                             End Sub)

            listErrors.AddRange(listErrorsConcurentBag.ToList())
            Return New ActionResult(Of List(Of DefinitionRateDetail)) With {.StateResult = True, .ObjectEmbbeded = listConcurrentBag.ToList(), .MessageResult = listErrors}

        Catch ex As Exception
            Return New ActionResult(Of List(Of DefinitionRateDetail)) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' funcion que valida la estructura del archivo a importar
    ''' </summary>
    ''' <param name="dataImportFile"></param>
    ''' <returns></returns>
    Function ValidateStructure(dataImportFile As ImportFileRow) As ActionResult(Of String)
        Try

            Dim stringBuilder = New StringBuilder

            'codigos
            If dataImportFile.Row.GetRange(1, 5).All(Function(x) String.IsNullOrEmpty(x)) Then
                stringBuilder.AppendLine("No se ha diligenciado ningún campo de codigo")
                Return New ActionResult(Of String) With {.StateResult = False, .Message = stringBuilder.ToString()}
            End If

            'ruletype
            Dim flagRuleType As Boolean = String.IsNullOrEmpty(dataImportFile.Row.Item(0)) OrElse Not IsNumeric(dataImportFile.Row.Item(0)) _
                                            OrElse Not {1, 2, 3, 4, 5}.Contains(CInt(dataImportFile.Row.Item(0)))

            If flagRuleType Then
                stringBuilder.AppendLine("El campo regla no se ha diligenciado de forma correcta")
                Return New ActionResult(Of String) With {.StateResult = False, .Message = stringBuilder.ToString()}
            End If

            'servicio IPS
            If Not flagRuleType AndAlso CInt(dataImportFile.Row.Item(0)) = 1 AndAlso String.IsNullOrEmpty(dataImportFile.Row.Item(1)) Then
                stringBuilder.AppendLine("No se ha diligenciado el campo servicio IPS de forma correcta")
            End If

            'CUPS
            If Not flagRuleType AndAlso CInt(dataImportFile.Row.Item(0)) = 2 AndAlso String.IsNullOrEmpty(dataImportFile.Row.Item(3)) Then
                stringBuilder.AppendLine("No se ha diligenciado el campo CUPS de forma correcta")
            End If

            'subgroupCUPS
            If Not flagRuleType AndAlso CInt(dataImportFile.Row.Item(0)) = 3 AndAlso String.IsNullOrEmpty(dataImportFile.Row.Item(4)) Then
                stringBuilder.AppendLine("No se ha diligenciado el campo subgrupo CUPS de forma correcta")
            End If

            'groupCUPS
            If Not flagRuleType AndAlso CInt(dataImportFile.Row.Item(0)) = 4 AndAlso String.IsNullOrEmpty(dataImportFile.Row.Item(5)) Then
                stringBuilder.AppendLine("No se ha diligenciado el campo grupo CUPS de forma correcta")
            End If

            'Condition 1

            Dim flagCondition1 = String.IsNullOrEmpty(dataImportFile.Row.Item(6)) _
                                 OrElse Not IsNumeric(dataImportFile.Row.Item(6)) _
                                 OrElse Not {1, 2, 3, 4, 5, 6, 7}.Contains(CInt(dataImportFile.Row.Item(6)))

            If flagCondition1 Then
                stringBuilder.AppendLine("No se ha diligenciado el campo condición de forma correcta")
                Return New ActionResult(Of String) With {.StateResult = False, .Message = stringBuilder.ToString()}
            End If

            'Operator
            Dim flagOperator = String.IsNullOrEmpty(dataImportFile.Row.Item(7)) _
                                OrElse Not IsNumeric(dataImportFile.Row.Item(7)) _
                                OrElse Not {1, 2, 3}.Contains(dataImportFile.Row.Item(7)) _
                                OrElse (Not flagCondition1 _
                                        AndAlso CInt(dataImportFile.Row.Item(6)) = 5 _
                                        AndAlso CInt(dataImportFile.Row.Item(7)) <> 1)

            If flagOperator Then
                stringBuilder.AppendLine("No se ha diligenciado el campo operador de forma correcta")
            End If

            'logicCondition 2
            Dim flagCondition2 = (String.IsNullOrEmpty(dataImportFile.Row.Item(8)) _
                                    OrElse Not IsNumeric(dataImportFile.Row.Item(8)) _
                                    OrElse Not {1, 2, 3, 4, 5, 6, 7}.Contains(CInt(dataImportFile.Row.Item(8)))) _
                                    OrElse (Not flagCondition1 _
                                            AndAlso CInt(dataImportFile.Row.Item(6)) = 5 _
                                            AndAlso CInt(dataImportFile.Row.Item(8)) <> 5) _
                                    OrElse (Not flagOperator _
                                            AndAlso CInt(dataImportFile.Row.Item(7)) <> 1 _
                                            AndAlso CInt(dataImportFile.Row.Item(8)) = 5)
            If flagCondition2 Then
                stringBuilder.AppendLine("No se ha diligenciado el campo condición2 correctamente")
            End If

            'Logic Condition 1 equals Condition2
            If Not flagCondition1 AndAlso Not flagCondition2 AndAlso (CInt(dataImportFile.Row.Item(6)) <> EDefinitionConditionType.None) AndAlso (CInt(dataImportFile.Row.Item(6)) = CInt(dataImportFile.Row.Item(8))) Then
                stringBuilder.AppendLine("La regla no permite que la codición 2 sea igual a la condición 1 cuando esta utlima sea diferente de ninguna")
            End If

            'weight
            If String.IsNullOrEmpty(dataImportFile.Row.Item(9)) OrElse Not IsNumeric(dataImportFile.Row.Item(9)) OrElse CInt(dataImportFile.Row.Item(9) > 10) Then
                stringBuilder.AppendLine("No se ha diligenciado el campo peso de forma correcta")
            End If

            'allow adjustment Value
            If String.IsNullOrEmpty(dataImportFile.Row.Item(10)) OrElse Not IsNumeric(dataImportFile.Row.Item(10)) OrElse Not {0, 1}.Contains(CInt(dataImportFile.Row.Item(10))) Then
                stringBuilder.AppendLine("No se ha diligenciado el campo 'Permite Ajustar Valor' de forma correcta")
            End If

            'TypeLiquidation
            Dim flagTypeLiquidation = (Not flagCondition1 _
                                        AndAlso (CInt(dataImportFile.Row.Item(6)) = 5) _
                                        AndAlso (String.IsNullOrEmpty(dataImportFile.Row.Item(11)) _
                                                    OrElse Not IsNumeric(dataImportFile.Row.Item(11)) _
                                                    OrElse Not {1, 2, 3}.Contains(dataImportFile.Row.Item(11))))
            If flagTypeLiquidation Then
                stringBuilder.AppendLine("No se ha diligenciado el campo tipo de liquidación")
            End If

            'manualType 
            If ((Not flagTypeLiquidation _
                AndAlso Not String.IsNullOrEmpty(dataImportFile.Row.Item(11)) _
                AndAlso CInt(dataImportFile.Row.Item(11)) = 1) _
                AndAlso (String.IsNullOrEmpty(dataImportFile.Row.Item(12)) _
                            OrElse Not IsNumeric(dataImportFile.Row.Item(12)) _
                            OrElse Not {1, 2, 3, 4}.Contains(CInt(dataImportFile.Row.Item(12))))) _
                OrElse (Not flagCondition1 _
                        AndAlso CInt(dataImportFile.Row.Item(6)) <> 5 _
                        AndAlso Not String.IsNullOrEmpty(dataImportFile.Row.Item(12))) Then
                stringBuilder.AppendLine("No se ha diligenciado el campo tipo de manual forma correcta")
            End If

            'Logic value
            If ((Not flagCondition1 _
                AndAlso CInt(dataImportFile.Row.Item(6)) = 5) _
                AndAlso (Not flagTypeLiquidation _
                            AndAlso Not String.IsNullOrEmpty(dataImportFile.Row.Item(11)) _
                            AndAlso CInt(dataImportFile.Row.Item(11)) = 1) _
                AndAlso (String.IsNullOrEmpty(dataImportFile.Row.Item(13)) _
                            OrElse Not IsNumeric(dataImportFile.Row.Item(13)) _
                            OrElse String.IsNullOrEmpty(dataImportFile.Row.Item(14)) _
                            OrElse Not IsNumeric(dataImportFile.Row.Item(14)))) _
                OrElse (Not flagCondition1 _
                            AndAlso (CInt(dataImportFile.Row.Item(6)) <> 5) _
                            AndAlso (Not String.IsNullOrEmpty(dataImportFile.Row.Item(13)) _
                                      OrElse Not String.IsNullOrEmpty(dataImportFile.Row.Item(14)))) Then
                stringBuilder.AppendLine("No se ha diligenciado el campo valor/valor recargo de forma correcta")
            End If

            'Logic rate manual
            If ((Not flagTypeLiquidation _
                    AndAlso Not String.IsNullOrEmpty(dataImportFile.Row.Item(11)) _
                    AndAlso CInt(dataImportFile.Row.Item(11)) <> 3) _
                    AndAlso (String.IsNullOrEmpty(dataImportFile.Row.Item(15)))) _
                    OrElse ((Not flagCondition1 _
                                AndAlso CInt(dataImportFile.Row.Item(6)) <> 5) _
                                AndAlso (Not String.IsNullOrEmpty(dataImportFile.Row.Item(15)))) Then
                stringBuilder.AppendLine("No se ha diligenciado el campo manual de tarifas de forma correcta")
            End If

            'logic rate
            If ((Not flagTypeLiquidation _
                AndAlso Not String.IsNullOrEmpty(dataImportFile.Row.Item(11)) _
                AndAlso CInt(dataImportFile.Row.Item(11)) > 1) _
                AndAlso (String.IsNullOrEmpty(dataImportFile.Row.Item(16)) _
                            OrElse Not IsNumeric(dataImportFile.Row.Item(16)))) _
                OrElse ((Not flagCondition1 _
                            AndAlso CInt(dataImportFile.Row.Item(6)) <> 5) _
                            AndAlso (Not String.IsNullOrEmpty(dataImportFile.Row.Item(16)))) Then

                stringBuilder.AppendLine("No se ha diligenciado el campo variación de forma correcta")
            End If

            'Logic RateManualValidity
            If ((Not flagTypeLiquidation _
                AndAlso Not String.IsNullOrEmpty(dataImportFile.Row.Item(11)) _
                AndAlso CInt(dataImportFile.Row.Item(11)) = 3) _
                AndAlso String.IsNullOrEmpty(dataImportFile.Row.Item(17))) _
                OrElse (Not flagCondition1 _
                            AndAlso CInt(dataImportFile.Row.Item(6)) <> 5 _
                            AndAlso (Not String.IsNullOrEmpty(dataImportFile.Row.Item(17)))) Then
                stringBuilder.AppendLine("No se ha diligenciado el campo vigencia de forma correcta")
            End If
            'revisado hasta aqui

            If stringBuilder.Length > 0 Then
                Return New ActionResult(Of String) With {.StateResult = False, .Message = stringBuilder.ToString()}
            End If

            Return New ActionResult(Of String) With {.StateResult = True}

        Catch ex As Exception
            Return New ActionResult(Of String) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Valida la regla a nivel de entidad (la entidad corresponda al tipo de regla)
    ''' </summary>
    ''' <param name="tupleRule"></param>
    ''' <returns></returns>
    Private Function ValidateRuleType(tupleRule As Tuple(Of Integer, IPSService, IPSService, CUPSEntity, CupsSubgroup, CupsGroup)) As ActionResult(Of Tuple(Of Integer, IPSService, IPSService, CUPSEntity, CupsSubgroup, CupsGroup))
        Dim ruleType = tupleRule.Item1
        Dim ipsService = tupleRule.Item2
        Dim ipserviceQx = tupleRule.Item3
        Dim cups = tupleRule.Item4
        Dim subGroupCups = tupleRule.Item5
        Dim GroupCups = tupleRule.Item6
        Dim message As String = String.Empty

        Select Case ruleType
            Case EDefinitionRateRuleType.IPSService
                If ipsService Is Nothing Then
                    message = "No se encontró el servicio IPS y el tipo de regla es por Servicio IPS"
                    Exit Select
                End If

                Dim flagQx As Boolean = ipsService IsNot Nothing AndAlso ipsService.Presentation = 2

                If flagQx AndAlso (ipserviceQx Is Nothing OrElse ipserviceQx.ServiceClass = EClassService.None) Then
                    message = "No se encontró el servicio IPS QX hijo, cuando el servicio IPS padre es quirurgico"
                    Exit Select
                End If

                If flagQx AndAlso (ipsService.SurgicalProcedureService Is Nothing OrElse Not ipsService.SurgicalProcedureService.ToList().Exists(Function(x) x.IPSServiceId = ipserviceQx.Id)) Then
                    message = "No se encontró el servicio Qx hijo asociado a la lista de procedimientos Qx del padre"
                    Exit Select
                End If

                GroupCups = Nothing
                subGroupCups = Nothing
            Case EDefinitionRateRuleType.CUPS
                If cups Is Nothing Then
                    message = "No se encontró el campo CUPS y el tipo de regla es por Servicio CUPS"
                    Exit Select
                End If
                ipsService = Nothing
                subGroupCups = Nothing
                GroupCups = Nothing
            Case EDefinitionRateRuleType.GroupCUPS
                If GroupCups Is Nothing Then
                    message = "No se encontró el campo grupo CUPS y el tipo de regla es por Servicio CUPS"
                    Exit Select
                End If
                ipsService = Nothing
                subGroupCups = Nothing
                cups = Nothing
            Case EDefinitionRateRuleType.SubGroupsCUPS
                If subGroupCups Is Nothing Then
                    message = "No se encontró el campo grupo CUPS y el tipo de regla es por Servicio CUPS"
                    Exit Select
                End If
                ipsService = Nothing
                GroupCups = Nothing
                cups = Nothing
            Case Else
                ipsService = Nothing
                GroupCups = Nothing
                subGroupCups = Nothing
                cups = Nothing
        End Select

        If Not String.IsNullOrEmpty(message) Then
            Return New ActionResult(Of Tuple(Of Integer, IPSService, IPSService, CUPSEntity, CupsSubgroup, CupsGroup)) _
                With {.StateResult = False, .Message = message}
        End If

        Dim returnTuple = New Tuple(Of Integer, IPSService, IPSService, CUPSEntity, CupsSubgroup, CupsGroup) _
                            (ruleType, ipsService, ipserviceQx, cups, subGroupCups, GroupCups)

        Return New ActionResult(Of Tuple(Of Integer, IPSService, IPSService, CUPSEntity, CupsSubgroup, CupsGroup)) _
                With {.StateResult = True, .ObjectEmbbeded = returnTuple}
    End Function

    ''' <summary>
    ''' funcion para exportar la estructura de DefinitionRateDetailCondition por codigo de la definicion de tarifas
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function ExportConditionStructureByCode(code As String) As ActionResult(Of Byte()) Implements IDefinitionRateDetailAdminService.ExportConditionStructureByCode
        Try
            If String.IsNullOrEmpty(code) Then
                Throw New ArgumentNullException("El parámetro es obligatorio", code)
            End If

            Dim listDefinitionRateDetail = _definitionRateDetailRepository.GetByFilter(Function(x) x.DefinitionRate.Code = code AndAlso x.ConditionType <> 5, False, {"IPSService", "CUPSEntity", "CupsSubgroup", "CupsGroup"}).ToList()

            Return ExportConditionStructure(listDefinitionRateDetail)

        Catch ex As Exception
            Return New ActionResult(Of Byte()) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' funcion para exportar la estructura de DefinitionRateDetailCondition por Id de la definicion de tarifas
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function ExportConditionStructureById(id As Integer) As ActionResult(Of Byte()) Implements IDefinitionRateDetailAdminService.ExportConditionStructureById
        Try
            If id = 0 Then
                Throw New ArgumentNullException("El parámetro es obligatorio", id)
            End If

            Dim listDefinitionRateDetail = _definitionRateDetailRepository.GetByFilter(Function(x) x.DefinitionRateId = id AndAlso x.ConditionType <> 5, False, {"IPSService", "CUPSEntity", "CupsSubgroup", "CupsGroup"}).ToList()

            Return ExportConditionStructure(listDefinitionRateDetail)

        Catch ex As Exception
            Return New ActionResult(Of Byte()) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' exporta la estructura detalle de la definicion de tarifa de forma Limpia (solo las columnas)
    ''' </summary>
    ''' <returns></returns>
    Public Function ExportConditionStructure(listDefinitionRateDetail As List(Of DefinitionRateDetail)) As ActionResult(Of Byte())
        Try

            If listDefinitionRateDetail Is Nothing OrElse Not listDefinitionRateDetail.Any() Then
                Return New ActionResult(Of Byte()) With {.StateResult = False, .Message = "No se encontraron datos para generar la estructura"}
            End If

            Dim listSheet = New List(Of ExcelSheetData)
            Dim excelSheet = New ExcelSheetData With {.Name = "Estructura Detalle Condición", .Columns = New List(Of ExcelDataColumn) From {New ExcelDataColumn With {.Name = "DefinitionRateDetailId",
                                                                                                                                            .Comment = "Id asociado al registro de la tabla de cabecera.
                                                                                                                                                        Este campo no se debe modificar, es el Id que relaciona los datos de condición con los detalles agregados",
                                                                                                                                            .Type = ExcelColumnFormat.Number},
                                                                                                                                            New ExcelDataColumn With {.Name = "Regla",
                                                                                                                                                                    .Comment = "Exportar el Dato de la Regla del Detalle.
                                                                                                                                                                                Este Campo es Informativo, no se valida a la hora de Importar o Pegar la estructura",
                                                                                                                                                                    .Type = ExcelColumnFormat.Text},
                                                                                                                                            New ExcelDataColumn With {.Name = "Servicio IPS",
                                                                                                                                                                    .Comment = "Exportar el Código del Servicio IPS del Detalle.
                                                                                                                                                                                Este Campo es Informativo, no se valida a la hora de Importar o Pegar la estructura",
                                                                                                                                                                    .Type = ExcelColumnFormat.Text},
                                                                                                                                            New ExcelDataColumn With {.Name = "CUPS",
                                                                                                                                                                    .Comment = "Exportar el Código del CUPS del Detalle.
                                                                                                                                                                                Este Campo es Informativo, no se valida a la hora de Importar o Pegar la estructura",
                                                                                                                                                                    .Type = ExcelColumnFormat.Text},
                                                                                                                                            New ExcelDataColumn With {.Name = "SubGrupo Cups",
                                                                                                                                                                    .Comment = "Exportar el Código del Subgrupo Cups del Detalle.
                                                                                                                                                                                Este Campo es Informativo, no se valida a la hora de Importar o Pegar la estructura",
                                                                                                                                                                    .Type = ExcelColumnFormat.Text},
                                                                                                                                            New ExcelDataColumn With {.Name = "Grupo Cups",
                                                                                                                                                                    .Comment = "Exportar el Código del Grupo Cups del Detalle.
                                                                                                                                                                                Este Campo es Informativo, no se valida a la hora de Importar o Pegar la estructura",
                                                                                                                                                                    .Type = ExcelColumnFormat.Text},
                                                                                                                                            New ExcelDataColumn With {.Name = "Tipo de condición",
                                                                                                                                                                    .Comment = "Exportar el Tipo de condición del Detalle.
                                                                                                                                                                                Este Campo es Informativo, no se valida a la hora de Importar o Pegar la estructura",
                                                                                                                                                                    .Type = ExcelColumnFormat.Text},
                                                                                                                                            New ExcelDataColumn With {.Name = "Operador",
                                                                                                                                                                    .Comment = "Especifica el Operador de la Condición1," & vbCrLf &
                                                                                                                                                                                "donde 1 : '=' y 2: '<>'",
                                                                                                                                                                    .Type = ExcelColumnFormat.Number},
                                                                                                                                            New ExcelDataColumn With {.Name = "Hora Inicial",
                                                                                                                                                                    .Comment = "Hora de inicio, solo se llena si la Condición1 = '1' - Horario",
                                                                                                                                                                    .Type = ExcelColumnFormat.Text},
                                                                                                                                            New ExcelDataColumn With {.Name = "Hora Final",
                                                                                                                                                                    .Comment = "Hora de finalización, solo se llena si la Condición1 = '1' - Horario",
                                                                                                                                                                    .Type = ExcelColumnFormat.Text},
                                                                                                                                            New ExcelDataColumn With {.Name = "Especialidad",
                                                                                                                                                                    .Comment = "Código de la especialidad, solo se llena sila condición1 = '2' - Especialidad",
                                                                                                                                                                    .Type = ExcelColumnFormat.Text},
                                                                                                                                            New ExcelDataColumn With {.Name = "Unidad Funcional",
                                                                                                                                                                    .Comment = "Código de la Unidad Funcional, solo se llena si la condición1 = '3' - Unidad Funcional",
                                                                                                                                                                    .Type = ExcelColumnFormat.Text},
                                                                                                                                            New ExcelDataColumn With {.Name = "Tipo de Unidad Funcional",
                                                                                                                                                                    .Comment = "Tipo de Unidad Funcional:" & vbCrLf &
                                                                                                                                                                                "1: Urgencias " & vbCrLf &
                                                                                                                                                                                "2: Hospitalización" & vbCrLf &
                                                                                                                                                                                "3: Apoyo Dx" & vbCrLf &
                                                                                                                                                                                "4: Apoyo Terapéutico" & vbCrLf &
                                                                                                                                                                                "5: Unidades de Cuidado Intensivo Adulto" & vbCrLf &
                                                                                                                                                                                "6: Unidades de Cuidado Intermedio Adulto" & vbCrLf &
                                                                                                                                                                                "7: Unidades de Cuidado Intensivo Pediátrica" & vbCrLf &
                                                                                                                                                                                "8: Unidades de Cuidado Intermedio Pediátrica" & vbCrLf &
                                                                                                                                                                                "9: Unidades de Cuidado Intensivo Neonatal" & vbCrLf &
                                                                                                                                                                                "10: Unidades de Cuidado Intermedio Neonatal" & vbCrLf &
                                                                                                                                                                                "11: Unidades de Cuidado Básico Neonatal" & vbCrLf &
                                                                                                                                                                                "12: Unidad Renal" & vbCrLf &
                                                                                                                                                                                "13 Unidad Oncológica" & vbCrLf &
                                                                                                                                                                                "14: Unidad Medicina Nuclear" & vbCrLf &
                                                                                                                                                                                "15: Consulta Externa" & vbCrLf &
                                                                                                                                                                                "16: Unidad Mental" & vbCrLf &
                                                                                                                                                                                "17: Unidad de Quemados" & vbCrLf &
                                                                                                                                                                                "18: Unidad de Cuidado Paliativo" & vbCrLf &
                                                                                                                                                                                "19: Cirugía" & vbCrLf &
                                                                                                                                                                                "20: Laboratorio" & vbCrLf &
                                                                                                                                                                                "21: Cardiología No Invasiva" & vbCrLf &
                                                                                                                                                                                "22: Cardiología Invasiva" & vbCrLf &
                                                                                                                                                                                "23: Gineco-Obstetricia" & vbCrLf &
                                                                                                                                                                                "24: Consulta Externa - Gineco-Obstetricia" & vbCrLf &
                                                                                                                                                                                "25: Otras" & vbCrLf &
                                                                                                                                                                                "solo se llena si la Condición= '4' - Tipo de Unidad",
                                                                                                                                                                    .Type = ExcelColumnFormat.Number},
                                                                                                                                            New ExcelDataColumn With {.Name = "RIAS",
                                                                                                                                                                    .Comment = "Código RIAS Solo se llena si la Condición1= '6' - RIAS",
                                                                                                                                                                    .Type = ExcelColumnFormat.Text},
                                                                                                                                            New ExcelDataColumn With {.Name = "Descripción Relacionada",
                                                                                                                                                                    .Comment = "Código de la Descripción Relacionada del Cups, solo se llena si la Condición1 = '7' - Descripción",
                                                                                                                                                                    .Type = ExcelColumnFormat.Text},
                                                                                                                                            New ExcelDataColumn With {.Name = "Operator2",
                                                                                                                                                                    .Comment = "Especifica el Operador de la Confición2, donde" & vbCrLf &
                                                                                                                                                                                "1 - '='" & vbCrLf &
                                                                                                                                                                                "2 - '<>'" & vbCrLf &
                                                                                                                                                                                "Este campo solo se llena si hay dos condiciones",
                                                                                                                                                                    .Type = ExcelColumnFormat.Number},
                                                                                                                                            New ExcelDataColumn With {.Name = "Hora Inicial2",
                                                                                                                                                                    .Comment = "Especifica la Hora inicial, este campo solo se llena si la Condicion2 es = '1' - Horario",
                                                                                                                                                                    .Type = ExcelColumnFormat.Text},
                                                                                                                                            New ExcelDataColumn With {.Name = "Hora Final2",
                                                                                                                                                                    .Comment = "Especifica la Hora Final. este campo solo se llena si la Condicion2 es = '1' - Horario",
                                                                                                                                                                    .Type = ExcelColumnFormat.Text},
                                                                                                                                            New ExcelDataColumn With {.Name = "Especialidad2",
                                                                                                                                                                    .Comment = "Código de la especialidad, este campo solo se llena si la Condicion2 es = '2' - Especialidad",
                                                                                                                                                                    .Type = ExcelColumnFormat.Text},
                                                                                                                                            New ExcelDataColumn With {.Name = "Unidad Funcional2",
                                                                                                                                                                    .Comment = "Código de la unidad funcional, este campo solo se llena si la Condicion2 es = '3' -Unidad Funcional",
                                                                                                                                                                    .Type = ExcelColumnFormat.Text},
                                                                                                                                            New ExcelDataColumn With {.Name = "Tipo de Unidad Funcional2",
                                                                                                                                                                    .Comment = "Tipo de Unidad Funcional:" & vbCrLf &
                                                                                                                                                                                "1: Urgencias " & vbCrLf &
                                                                                                                                                                                "2: Hospitalización" & vbCrLf &
                                                                                                                                                                                "3: Apoyo Dx" & vbCrLf &
                                                                                                                                                                                "4: Apoyo Terapéutico" & vbCrLf &
                                                                                                                                                                                "5: Unidades de Cuidado Intensivo Adulto" & vbCrLf &
                                                                                                                                                                                "6: Unidades de Cuidado Intermedio Adulto" & vbCrLf &
                                                                                                                                                                                "7: Unidades de Cuidado Intensivo Pediátrica" & vbCrLf &
                                                                                                                                                                                "8: Unidades de Cuidado Intermedio Pediátrica" & vbCrLf &
                                                                                                                                                                                "9: Unidades de Cuidado Intensivo Neonatal" & vbCrLf &
                                                                                                                                                                                "10: Unidades de Cuidado Intermedio Neonatal" & vbCrLf &
                                                                                                                                                                                "11: Unidades de Cuidado Básico Neonatal" & vbCrLf &
                                                                                                                                                                                "12: Unidad Renal" & vbCrLf &
                                                                                                                                                                                "13 Unidad Oncológica" & vbCrLf &
                                                                                                                                                                                "14: Unidad Medicina Nuclear" & vbCrLf &
                                                                                                                                                                                "15: Consulta Externa" & vbCrLf &
                                                                                                                                                                                "16: Unidad Mental" & vbCrLf &
                                                                                                                                                                                "17: Unidad de Quemados" & vbCrLf &
                                                                                                                                                                                "18: Unidad de Cuidado Paliativo" & vbCrLf &
                                                                                                                                                                                "19: Cirugía" & vbCrLf &
                                                                                                                                                                                "20: Laboratorio" & vbCrLf &
                                                                                                                                                                                "21: Cardiología No Invasiva" & vbCrLf &
                                                                                                                                                                                "22: Cardiología Invasiva" & vbCrLf &
                                                                                                                                                                                "23: Gineco-Obstetricia" & vbCrLf &
                                                                                                                                                                                "24: Consulta Externa - Gineco-Obstetricia" & vbCrLf &
                                                                                                                                                                                "25: Otras" & vbCrLf &
                                                                                                                                                                                "solo se llena si la Condición= '4' - Tipo de Unidad",
                                                                                                                                                                    .Type = ExcelColumnFormat.Number},
                                                                                                                                            New ExcelDataColumn With {.Name = "RIAS2",
                                                                                                                                                                    .Comment = "Código RIAS Solo se llena si la Condición2= '6' - RIAS",
                                                                                                                                                                    .Type = ExcelColumnFormat.Text},
                                                                                                                                            New ExcelDataColumn With {.Name = "Descripción2",
                                                                                                                                                                    .Comment = "Código de la Descripción2, este campo solo se llena si la Condicion2 es = '7' - Descripción",
                                                                                                                                                                    .Type = ExcelColumnFormat.Text},
                                                                                                                                            New ExcelDataColumn With {.Name = "Tipo Liquidación",
                                                                                                                                                                    .Comment = "Especifica el tipo de liquidación" & vbCrLf &
                                                                                                                                                                                "1 - Fija" & vbCrLf &
                                                                                                                                                                                "2 - Estándar" & vbCrLf &
                                                                                                                                                                                "3 - Vigencia",
                                                                                                                                                                    .Type = ExcelColumnFormat.Number},
                                                                                                                                            New ExcelDataColumn With {.Name = "Tipo de Manual",
                                                                                                                                                                    .Comment = "Tipo del manual tarifario" & vbCrLf &
                                                                                                                                                                                " 1 - ISS 2001" & vbCrLf &
                                                                                                                                                                                " 2 - ISS 2004" & vbCrLf &
                                                                                                                                                                                " 3 - SOAT" & vbCrLf &
                                                                                                                                                                                " 4 - Institucional" & vbCrLf &
                                                                                                                                                                                "solo se solicita si el tipo de liquidación es fija",
                                                                                                                                                                    .Type = ExcelColumnFormat.Number},
                                                                                                                                            New ExcelDataColumn With {.Name = "Valor",
                                                                                                                                                                    .Comment = "Valor del servicio" & vbCrLf &
                                                                                                                                                                                "Nota : este campo solo se llena si el tipo de liquidación es Fija",
                                                                                                                                                                    .Type = ExcelColumnFormat.Number},
                                                                                                                                            New ExcelDataColumn With {.Name = "Valor con Recargo",
                                                                                                                                                                    .Comment = "Valor del servicio" & vbCrLf &
                                                                                                                                                                                "Nota : este campo solo se llena si el tipo de liquidación es Fija",
                                                                                                                                                                    .Type = ExcelColumnFormat.Number},
                                                                                                                                            New ExcelDataColumn With {.Name = "Manual Tarifario",
                                                                                                                                                                    .Comment = "Código del manual tarifario" & vbCrLf &
                                                                                                                                                                                "Nota: Este campo solo se llena para los tipos de liquidación 1-Fija y 2-Estandar," & vbCrLf &
                                                                                                                                                                                "a partir de este campo se definen porcentajes a liquidar en eventos quirúrgicos",
                                                                                                                                                                    .Type = ExcelColumnFormat.Text},
                                                                                                                                            New ExcelDataColumn With {.Name = "Variación",
                                                                                                                                                                    .Comment = "Porcentaje de la variación de la tarifa" & vbCrLf &
                                                                                                                                                                                "Nota: Este campo solo se llena si el tipo de liquidación es 2-Estándar o 3-Vigencia",
                                                                                                                                                                    .Type = ExcelColumnFormat.Number},
                                                                                                                                            New ExcelDataColumn With {.Name = "Vigencia",
                                                                                                                                                                    .Comment = "Código de la vigencia del manual tarifario." & vbCrLf &
                                                                                                                                                                                "Nota: Este campo solo se llena si el tipo de liquidación es 3-Vigencia",
                                                                                                                                                                    .Type = ExcelColumnFormat.Text}}}


            Dim concurrentBagExcelRow = New ConcurrentBag(Of ExcelRow)

            'obtengo los codigos S.IPS,CUPS,SubGrupo CUPS, Grupo CUPS
            Parallel.ForEach(listDefinitionRateDetail, Sub(item)
                                                           Dim excelRow = New ExcelRow
                                                           With excelRow
                                                               .Cells = New List(Of Object)
                                                               .Cells.Add(item.Id)
                                                               .Cells.Add(item.GetRuleTypeName())
                                                               .Cells.Add(item.IPSService?.Code)
                                                               .Cells.Add(item.CUPSEntity?.Code)
                                                               .Cells.Add(item.CupsSubgroup?.Code)
                                                               .Cells.Add(item.CupsGroup?.Code)
                                                               .Cells.Add(item.GetConditionTypeName())
                                                           End With
                                                           concurrentBagExcelRow.Add(excelRow)
                                                       End Sub)


            excelSheet.Rows = New List(Of ExcelRow)
            excelSheet.Rows.AddRange(concurrentBagExcelRow.ToList())

            listSheet.Add(excelSheet)
            Dim arrayExcelBytes = Utils.ExportDataToExcel(listSheet)

            If arrayExcelBytes Is Nothing Then
                Return New ActionResult(Of Byte()) With {.StateResult = False, .Message = "Error en la generación del documento"}
            End If

            Return New ActionResult(Of Byte()) With {.StateResult = True, .ObjectEmbbeded = arrayExcelBytes, .Message = "Generación exitosa"}
        Catch ex As Exception
            Return New ActionResult(Of Byte()) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' funcion que importa las condiciones de las reglas de la definicion de tarifa
    ''' </summary>
    ''' <param name="dataImportFile"></param>
    ''' <returns></returns>
    Public Function ImportConditionDataToAdd(dataImportFile As List(Of ImportFileRow), audit As AuditMessage) As ActionResult(Of List(Of DefinitionRateDetailCondition)) Implements IDefinitionRateDetailAdminService.ImportConditionDataToAdd
        Try
            If dataImportFile Is Nothing OrElse Not dataImportFile.Any() Then
                Throw New ArgumentNullException(NameOf(dataImportFile), "El parámetro no puede venir vacio")
            End If

            Dim listSpecialityCodes As New HashSet(Of String)
            Dim listFunctionalUnitCodes As New HashSet(Of String)
            Dim listRIASCodes As New HashSet(Of String)
            Dim listContractDescriptionsCodes As New HashSet(Of String)
            Dim listRateManualCodes As New HashSet(Of String)
            Dim listRateManualValidityCodes As New HashSet(Of String)
            Dim listDefinitionRateDetailIds As New HashSet(Of Integer)

            Dim indexRowToRemove As New HashSet(Of Integer)
            Dim listErrors As New List(Of String)

            For Each item In dataImportFile

                Dim resultValidate = Me.ValidateRowPlaneConditionStructure(item)
                If resultValidate Is Nothing OrElse Not resultValidate.StateResult Then
                    listErrors.Add($"Error en la fila {item.IndexRow}: {resultValidate?.Message}")
                    indexRowToRemove.Add(item.IndexRow)
                    Continue For
                End If

                'DefinitionRateDetail
                listDefinitionRateDetailIds.Add(CInt(item.Row.Item(0)))

                'Espacialidad 1 y 2
                If Not String.IsNullOrEmpty(item.Row.Item(10)) Then
                    listSpecialityCodes.Add(item.Row.Item(10).ToString())
                End If

                If Not String.IsNullOrEmpty(item.Row.Item(18)) Then
                    listSpecialityCodes.Add(item.Row.Item(18).ToString())
                End If

                'Unidad funcional 1 y 2
                If Not String.IsNullOrEmpty(item.Row.Item(11)) Then
                    listFunctionalUnitCodes.Add(item.Row.Item(11).ToString())
                End If

                If Not String.IsNullOrEmpty(item.Row.Item(19)) Then
                    listFunctionalUnitCodes.Add(item.Row.Item(19).ToString())
                End If

                'RIAS 1 y 2
                If Not String.IsNullOrEmpty(item.Row.Item(13)) Then
                    listRIASCodes.Add(item.Row.Item(13).ToString())
                End If

                If Not String.IsNullOrEmpty(item.Row.Item(21)) Then
                    listRIASCodes.Add(item.Row.Item(21).ToString())
                End If

                'Descripcion relacionada 1 y 2
                If Not String.IsNullOrEmpty(item.Row.Item(14)) Then
                    listContractDescriptionsCodes.Add(item.Row.Item(14).ToString())
                End If

                If Not String.IsNullOrEmpty(item.Row.Item(22)) Then
                    listContractDescriptionsCodes.Add(item.Row.Item(22).ToString())
                End If

                'Manual de tarifa
                If Not String.IsNullOrEmpty(item.Row.Item(27)) Then
                    listRateManualCodes.Add(item.Row.Item(27).ToString())
                End If

                'Vigencia
                If Not String.IsNullOrEmpty(item.Row.Item(29)) Then
                    listRateManualValidityCodes.Add(item.Row.Item(29).ToString())
                End If
            Next

            'se eliminan los registros que No pasaron validacion 
            dataImportFile.RemoveAll(Function(x) indexRowToRemove.Contains(x.IndexRow))

            'se verifica si quedan datos disponibles por procesar sino se retorna la respuesta con la validacion
            If Not dataImportFile.Any() Then
                Return New ActionResult(Of List(Of DefinitionRateDetailCondition)) With {.StateResult = True, .MessageResult = listErrors}
            End If

            'se crea el objeto para consultar los codigos necesarios de forma masiva y asincrona 
            Dim ObjectToQuery = New DefinitionDetailQuery With {.ListOfCodes = New Dictionary(Of String, List(Of String)) _
                                                                                From {{EObjectTypeDefinitionCondition.Specialty.ToString(), listSpecialityCodes.ToList()},
                                                                                      {EObjectTypeDefinitionCondition.FunctionalUnit.ToString(), listFunctionalUnitCodes.ToList()},
                                                                                      {EObjectTypeDefinitionCondition.RIAS.ToString(), listRIASCodes.ToList()},
                                                                                      {EObjectTypeDefinitionCondition.Description.ToString(), listContractDescriptionsCodes.ToList()},
                                                                                      {EObjectTypeDefinitionCondition.RateManual.ToString(), listRateManualCodes.ToList()},
                                                                                      {EObjectTypeDefinitionCondition.RateManualValidity.ToString(), listRateManualValidityCodes.ToList()}}}

            Dim definitionDetailQuery = _definitionRateDetailRepository.GetQueryToImportCoditionData(ObjectToQuery)

            Dim listDefinitionRateDetail = _definitionRateDetailRepository.GetByFilter(Function(x) listDefinitionRateDetailIds.Contains(x.Id), False)?.ToList()

            If listDefinitionRateDetail Is Nothing OrElse Not listDefinitionRateDetail.Any() Then
                Throw New ArgumentNullException(NameOf(listDefinitionRateDetail), "No se encontró Ids relacionados a una definición de tarifas")
            End If

            Dim listConcurrentBag = New ConcurrentBag(Of DefinitionRateDetailCondition)
            Dim listErrorsConcurentBag = New ConcurrentBag(Of String)
            Parallel.ForEach(dataImportFile, Sub(row)

                                                 Dim indexRow = row.IndexRow
                                                 Dim definitionRateDetailId As Integer = CInt(row.Row.Item(0))
                                                 Dim definitionRateDetail = listDefinitionRateDetail.Find(Function(item) item.Id = definitionRateDetailId)

                                                 If definitionRateDetail Is Nothing Then
                                                     listErrorsConcurentBag.Add($"Error en la fila {row.IndexRow}: no existe el Id del detalle {definitionRateDetailId}")
                                                     Exit Sub
                                                 End If

                                                 If definitionRateDetail.ConditionType = definitionRateDetail.ConditionType2 Then
                                                     listErrorsConcurentBag.Add($"Error en la fila {row.IndexRow}: la regla tiene las mismas condiciones 1 y 2")
                                                     Exit Sub
                                                 End If

                                                 Dim iNESPECIA As INESPECIA = Nothing
                                                 Dim functionalUnit As FunctionalUnit = Nothing
                                                 Dim rIAS As RIAS = Nothing
                                                 Dim description As ContractDescriptions = Nothing
                                                 Dim iNESPECIA2 As INESPECIA = Nothing
                                                 Dim functionalUnit2 As FunctionalUnit = Nothing
                                                 Dim rIAS2 As RIAS = Nothing
                                                 Dim description2 As ContractDescriptions = Nothing
                                                 Dim rateManual As RateManual = Nothing
                                                 Dim rateManualValidity As RateManualValidity = Nothing
                                                 Dim fOperator = CByte(row.Row.Item(7))
                                                 Dim fOperator2 As Byte? = Nothing

                                                 Dim dictionaryCondition As New Dictionary(Of EDefinitionConditionType, Object)
                                                 'Condition 1

                                                 iNESPECIA = TryCast(definitionDetailQuery.ListOfObjects(EObjectTypeDefinitionCondition.Specialty.ToString()), List(Of INESPECIA)) _
                                                                                                            .Find(Function(x) x.CODESPECI = row.Row.Item(10)?.ToString())

                                                 functionalUnit = TryCast(definitionDetailQuery.ListOfObjects(EObjectTypeDefinitionCondition.FunctionalUnit.ToString()), List(Of FunctionalUnit)) _
                                                                                                            .Find(Function(x) x.Code = row.Row.Item(11)?.ToString())

                                                 rIAS = TryCast(definitionDetailQuery.ListOfObjects(EObjectTypeDefinitionCondition.RIAS.ToString()), List(Of RIAS)) _
                                                                                                            .Find(Function(x) x.CODPRO = row.Row.Item(13)?.ToString())

                                                 description = TryCast(definitionDetailQuery.ListOfObjects(EObjectTypeDefinitionCondition.Description.ToString()), List(Of ContractDescriptions)) _
                                                                                                            .Find(Function(x) x.Code = row.Row.Item(14)?.ToString())

                                                 dictionaryCondition.Add(EDefinitionConditionType.Schedule, New Tuple(Of String, String)(row.Row.Item(8)?.ToString(), row.Row.Item(9)?.ToString()))
                                                 dictionaryCondition.Add(EDefinitionConditionType.Speciality, iNESPECIA)
                                                 dictionaryCondition.Add(EDefinitionConditionType.FunctionalUnit, functionalUnit)
                                                 dictionaryCondition.Add(EDefinitionConditionType.FunctionalUnitType, row.Row.Item(12)?.ToString())
                                                 dictionaryCondition.Add(EDefinitionConditionType.RIAS, rIAS)
                                                 dictionaryCondition.Add(EDefinitionConditionType.ContractDescription, description)

                                                 Dim resultCondition1 = Me.ValidateCondition(definitionRateDetail.ConditionType, dictionaryCondition)

                                                 If resultCondition1 Is Nothing OrElse Not resultCondition1.StateResult Then
                                                     listErrorsConcurentBag.Add($"Error en la fila {row.IndexRow}: la condición {definitionRateDetail.GetConditionTypeName} no se cumple satisfactoriamente")
                                                     Exit Sub
                                                 End If

                                                 If definitionRateDetail.ConditionType2 <> EDefinitionConditionType.None Then

                                                     If String.IsNullOrEmpty(row.Row.Item(15)) Then
                                                         listErrorsConcurentBag.Add($"Error en la fila {row.IndexRow}: el operador 2 está vacio")
                                                         Exit Sub
                                                     End If

                                                     fOperator2 = CByte(row.Row.Item(15))
                                                     iNESPECIA2 = TryCast(definitionDetailQuery.ListOfObjects(EObjectTypeDefinitionCondition.Specialty.ToString()), List(Of INESPECIA)) _
                                                                                                            .Find(Function(x) x.CODESPECI = row.Row.Item(18)?.ToString())

                                                     functionalUnit2 = TryCast(definitionDetailQuery.ListOfObjects(EObjectTypeDefinitionCondition.FunctionalUnit.ToString()), List(Of FunctionalUnit)) _
                                                                                                                .Find(Function(x) x.Code = row.Row.Item(19)?.ToString())

                                                     rIAS2 = TryCast(definitionDetailQuery.ListOfObjects(EObjectTypeDefinitionCondition.RIAS.ToString()), List(Of RIAS)) _
                                                                                                                .Find(Function(x) x.CODPRO = row.Row.Item(21)?.ToString())

                                                     description2 = TryCast(definitionDetailQuery.ListOfObjects(EObjectTypeDefinitionCondition.Description.ToString()), List(Of ContractDescriptions)) _
                                                                                                                .Find(Function(x) x.Code = row.Row.Item(22)?.ToString())

                                                     dictionaryCondition = New Dictionary(Of EDefinitionConditionType, Object)
                                                     dictionaryCondition.Add(EDefinitionConditionType.Schedule, New Tuple(Of String, String)(row.Row.Item(16)?.ToString(), row.Row.Item(17)?.ToString()))
                                                     dictionaryCondition.Add(EDefinitionConditionType.Speciality, iNESPECIA2)
                                                     dictionaryCondition.Add(EDefinitionConditionType.FunctionalUnit, functionalUnit2)
                                                     dictionaryCondition.Add(EDefinitionConditionType.FunctionalUnitType, row.Row.Item(20)?.ToString())
                                                     dictionaryCondition.Add(EDefinitionConditionType.RIAS, rIAS2)
                                                     dictionaryCondition.Add(EDefinitionConditionType.ContractDescription, description2)

                                                     Dim resultCondition2 = Me.ValidateCondition(definitionRateDetail.ConditionType2, dictionaryCondition)

                                                     If resultCondition2 Is Nothing OrElse Not resultCondition2.StateResult Then
                                                         listErrorsConcurentBag.Add($"Error en la fila {row.IndexRow}: la condición {definitionRateDetail.GetConditionTypeName} no se cumple satisfactoriamente")
                                                         Exit Sub
                                                     End If
                                                 End If

                                                 Dim liquidationType = CInt(row.Row.Item(23))

                                                 If liquidationType = EDefinitionLiquidationType.Validity Then
                                                     rateManualValidity = TryCast(definitionDetailQuery.ListOfObjects(EObjectTypeDefinitionCondition.RateManualValidity.ToString()), List(Of RateManualValidity)) _
                                                                                                               .Find(Function(x) x.Code = row.Row.Item(29)?.ToString())
                                                     If rateManualValidity Is Nothing OrElse rateManualValidity.Id = 0 Then
                                                         listErrorsConcurentBag.Add($"Error en la fila {row.IndexRow}: no se encontró la vigencia - {row.Row.Item(29)?.ToString()}")
                                                         Exit Sub
                                                     End If
                                                 Else
                                                     rateManual = TryCast(definitionDetailQuery.ListOfObjects(EObjectTypeDefinitionCondition.RateManual.ToString()), List(Of RateManual)) _
                                                                                                              .Find(Function(x) x.Code = row.Row.Item(27)?.ToString())
                                                     If rateManual Is Nothing OrElse rateManual.Id = 0 Then
                                                         listErrorsConcurentBag.Add($"Error en la fila {row.IndexRow}: no se encontró el manual - {row.Row.Item(27)?.ToString()}")
                                                         Exit Sub
                                                     End If
                                                 End If

                                                 Dim definitionRateDetailCondition As New DefinitionRateDetailCondition
                                                 With definitionRateDetailCondition
                                                     .DefinitionRateDetailId = definitionRateDetailId
                                                     .Operator = fOperator
                                                     Dim firstConditionName As String = .FirstOperatorName

                                                     Select Case definitionRateDetail.ConditionType
                                                         Case EDefinitionConditionType.Schedule
                                                             .StartTime = TimeSpan.Parse(DateTime.Parse(row.Row.Item(8)?.ToString()).ToString("HH:mm"))
                                                             .EndTime = TimeSpan.Parse(DateTime.Parse(row.Row.Item(9)?.ToString()).ToString("HH:mm"))
                                                             firstConditionName &= $" ({ .StartTime.ToString()} - { .EndTime.ToString()})"
                                                         Case EDefinitionConditionType.Speciality
                                                             .SpecialtyId = iNESPECIA.CODESPECI
                                                             .SpecialtyDescriptionFirst = $"{iNESPECIA.CODESPECI} - {iNESPECIA.DESESPECI}"
                                                             firstConditionName &= $" ({ .SpecialtyDescriptionFirst})"
                                                         Case EDefinitionConditionType.FunctionalUnit
                                                             .FunctionalUnitId = functionalUnit.Id
                                                             .FunctionalUnitDescriptionFirst = $"{functionalUnit.Code} - {functionalUnit.Name}"
                                                             firstConditionName &= $" ({ .FunctionalUnitDescriptionFirst})"
                                                         Case EDefinitionConditionType.FunctionalUnitType
                                                             .UnitTypeId = CByte(row.Row.Item(12))
                                                             firstConditionName &= $" ({ Utils.UnitTypes.Find(Function(i) i.Item1 = .UnitTypeId).Item2})"
                                                         Case EDefinitionConditionType.RIAS
                                                             .RIASId = rIAS.CODPRO
                                                             .RIASDescriptionFirst = $"{rIAS.CODPRO} - {rIAS.NOMBRE}"
                                                             firstConditionName &= $" ({ .RIASDescriptionFirst})"
                                                         Case EDefinitionConditionType.ContractDescription
                                                             .ContractDescriptionId = description.Id
                                                             .DescriptionCodeNameFirst = $"{description.Code} - {description.Name}"
                                                             firstConditionName &= $" ({ .DescriptionCodeNameFirst})"
                                                         Case Else
                                                             listErrorsConcurentBag.Add($"Error en la fila {row.IndexRow}: condición invalida")
                                                             Exit Sub
                                                     End Select

                                                     .Operator2 = fOperator2
                                                     Dim secondConditionName As String = .SecondOperatorName
                                                     Select Case definitionRateDetail.ConditionType2
                                                         Case EDefinitionConditionType.Schedule
                                                             .StartTime2 = TimeSpan.Parse(DateTime.Parse(row.Row.Item(16)?.ToString()).ToString("HH:mm"))
                                                             .EndTime2 = TimeSpan.Parse(DateTime.Parse(row.Row.Item(17)?.ToString()).ToString("HH:mm"))
                                                             secondConditionName &= $" ({ .StartTime2.ToString()} - { .EndTime2.ToString()})"
                                                         Case EDefinitionConditionType.Speciality
                                                             .SpecialtyId2 = iNESPECIA2.CODESPECI
                                                             .SpecialtyDescriptionSecond = $"{iNESPECIA2.CODESPECI} - {iNESPECIA2.DESESPECI}"
                                                             secondConditionName &= $" ({ .SpecialtyDescriptionSecond})"
                                                         Case EDefinitionConditionType.FunctionalUnit
                                                             .FunctionalUnitId2 = functionalUnit.Id
                                                             .FunctionalUnitDescriptionSecond = $"{functionalUnit2.Code} - {functionalUnit2.Name}"
                                                             secondConditionName &= $" ({ .FunctionalUnitDescriptionSecond})"
                                                         Case EDefinitionConditionType.FunctionalUnitType
                                                             .UnitTypeId2 = CByte(row.Row.Item(19))
                                                             secondConditionName &= $" ({ Utils.UnitTypes.Find(Function(i) i.Item1 = .UnitTypeId2).Item2})"
                                                         Case EDefinitionConditionType.RIAS
                                                             .RIASId2 = rIAS2.CODPRO
                                                             .RIASDescriptionSecond = $"{rIAS2.CODPRO} - {rIAS2.NOMBRE}"
                                                             secondConditionName &= $" ({ .RIASDescriptionSecond})"
                                                         Case EDefinitionConditionType.ContractDescription
                                                             .ContractDescriptionId2 = description2.Id
                                                             .DescriptionCodeNameSecond = $"{description2.Code} - {description2.Name}"
                                                             secondConditionName &= $" ({ .DescriptionCodeNameSecond})"
                                                     End Select
                                                     .ConditionName = $"{firstConditionName} {definitionRateDetail.LogicOperatorName} {secondConditionName} "

                                                     .LiquidationType = CByte(liquidationType)
                                                     .RateManualValidityId = rateManualValidity?.Id
                                                     .RateManualValidityDescription = $"{rateManualValidity?.Code} - {rateManualValidity?.Name}"
                                                     .RateManualId = rateManual?.Id
                                                     .RateManualDescription = $"{rateManual?.Code} - {rateManual?.Name}"

                                                     .RateName = .LiquidationTypeName

                                                     If .LiquidationType = EDefinitionLiquidationType.Fixed Then
                                                         .ManualType = CByte(row.Row.Item(24))
                                                         .SalesValue = CDec(row.Row.Item(25))
                                                         .SalesValueWithSurcharge = CDec(row.Row.Item(26))
                                                         .RateName &= $" - { .ManualTypeName} -  {String.Format("{0:n2}", .SalesValue)}"
                                                     End If

                                                     If {EDefinitionLiquidationType.Validity, EDefinitionLiquidationType.Standard}.Contains(.LiquidationType) Then
                                                         .RateVariation = CDec(row.Row.Item(28))
                                                         .RateName &= $" - {If(.RateManualId IsNot Nothing, .RateManualDescription, .RateManualValidityDescription) } - { .RateVariation}% "
                                                     End If

                                                 End With

                                                 listConcurrentBag.Add(definitionRateDetailCondition)
                                             End Sub)

            listErrors.AddRange(listErrorsConcurentBag.ToList())
            Return New ActionResult(Of List(Of DefinitionRateDetailCondition)) With {.StateResult = True, .ObjectEmbbeded = listConcurrentBag.ToList(), .MessageResult = listErrors}

        Catch ex As Exception
            Return New ActionResult(Of List(Of DefinitionRateDetailCondition)) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' valida la estructura plana del excel a importar
    ''' </summary>
    ''' <param name="dataImportFile"></param>
    ''' <returns></returns>
    Function ValidateRowPlaneConditionStructure(dataImportFile As ImportFileRow) As ActionResult(Of String)
        Try
            Dim stringBuilder = New StringBuilder
            Dim outTime As DateTime

            If dataImportFile.Row.All(Function(y) String.IsNullOrEmpty(y)) Then
                Return New ActionResult(Of String) With {.StateResult = False, .Message = $"No se ecuentra diligenciado correctamente"}
            End If

            'DefinitionRateDetailId
            If String.IsNullOrEmpty(dataImportFile.Row.Item(0)) Then
                Return New ActionResult(Of String) With {.StateResult = False, .Message = "No se ha detectado Id del detalle de la definición de tarifa"}
            End If

            'data in columns contion 1
            If dataImportFile.Row.GetRange(7, 14).All(Function(x) String.IsNullOrEmpty(x)) Then
                Return New ActionResult(Of String) With {.StateResult = False, .Message = "No se ha diligenciado ningún campo de la condición 1"}
            End If

            'operador
            If String.IsNullOrEmpty(dataImportFile.Row.Item(7)) OrElse Not IsNumeric(dataImportFile.Row.Item(7)) _
                                            OrElse Not {1, 2}.Contains(CInt(dataImportFile.Row.Item(7))) Then
                Return New ActionResult(Of String) With {.StateResult = False, .Message = "No se ha diligenciado el campo operador 1 de forma correcta"}
            End If

            'tipo de liquidacion
            If String.IsNullOrEmpty(dataImportFile.Row.Item(23)) _
                OrElse Not IsNumeric(dataImportFile.Row.Item(23)) _
                 OrElse Not {1, 2, 3}.Contains(CInt(dataImportFile.Row.Item(23))) Then
                Return New ActionResult(Of String) With {.StateResult = False, .Message = "No se ha diligenciado el campo tipo de liquidación de forma correcta"}
            End If

            'Tipo de Manual
            If CInt(dataImportFile.Row.Item(23)) = 1 _
                AndAlso (String.IsNullOrEmpty(dataImportFile.Row.Item(24)) _
                            OrElse Not IsNumeric(dataImportFile.Row.Item(24)) _
                            OrElse Not {1, 2, 3, 4}.Contains(dataImportFile.Row.Item(24))) Then
                stringBuilder.AppendLine("El campo tipo de manual no tiene el formato correcto")
            End If

            'valor 
            If CInt(dataImportFile.Row.Item(23)) = 1 _
                AndAlso (String.IsNullOrEmpty(dataImportFile.Row.Item(25)) _
                            OrElse Not IsNumeric(dataImportFile.Row.Item(25))) Then
                stringBuilder.AppendLine("El campo valor no tiene el formato correcto")
            End If

            'valor con recargo
            If CInt(dataImportFile.Row.Item(23)) = 1 _
                AndAlso (String.IsNullOrEmpty(dataImportFile.Row.Item(26)) _
                            OrElse Not IsNumeric(dataImportFile.Row.Item(26))) Then
                stringBuilder.AppendLine("El campo valor con recargo no tiene el formato correcto")
            End If

            'variacion
            If {2, 3}.Contains(CInt(dataImportFile.Row.Item(23))) _
                AndAlso (String.IsNullOrEmpty(dataImportFile.Row.Item(28)) OrElse Not IsNumeric(dataImportFile.Row.Item(28))) Then
                stringBuilder.AppendLine("El campo variación no tiene el formato correcto")
            End If

            'manual tarifario
            If {1, 2}.Contains(CInt(dataImportFile.Row.Item(23))) AndAlso String.IsNullOrEmpty(dataImportFile.Row.Item(27)) Then
                stringBuilder.AppendLine("El campo manual tarifario no tiene el formato correcto")
            End If

            'vigencia
            If CInt(dataImportFile.Row.Item(23)) = 3 AndAlso String.IsNullOrEmpty(dataImportFile.Row.Item(29)) Then
                stringBuilder.AppendLine("El campo vigencia no tiene el formato correcto")
            End If

            Dim listTypeFunctionalUnitRange As List(Of Integer) = Enumerable.Range(1, 25).ToList()

            'HoraInicial
            If Not String.IsNullOrEmpty(dataImportFile.Row.Item(8)) AndAlso Not DateTime.TryParse(dataImportFile.Row.Item(8), outTime) Then
                stringBuilder.AppendLine("El campo hora inicial no tiene el formato correcto ")
            End If

            'HoraFinal
            If Not String.IsNullOrEmpty(dataImportFile.Row.Item(9)) AndAlso Not DateTime.TryParse(dataImportFile.Row.Item(9), outTime) Then
                stringBuilder.AppendLine("El campo hora final no tiene el formato correcto ")
            End If

            'Tipo unidad funcional
            If Not String.IsNullOrEmpty(dataImportFile.Row.Item(12)) AndAlso (Not IsNumeric(dataImportFile.Row.Item(12)) OrElse Not listTypeFunctionalUnitRange.Contains(dataImportFile.Row.Item(12))) Then
                stringBuilder.AppendLine("El campo tipo de unidad funcional no tiene el formato correcto")
            End If

            'HoraInicial 2
            If Not String.IsNullOrEmpty(dataImportFile.Row.Item(16)) AndAlso Not DateTime.TryParse(dataImportFile.Row.Item(16), outTime) Then
                stringBuilder.AppendLine("El campo hora inicial 2 no tiene el formato correcto ")
            End If

            'HoraFinal 2
            If Not String.IsNullOrEmpty(dataImportFile.Row.Item(17)) AndAlso Not DateTime.TryParse(dataImportFile.Row.Item(17), outTime) Then
                stringBuilder.AppendLine("El campo hora final 2 no tiene el formato correcto ")
            End If

            'Tipo unidad funcional 2
            If Not String.IsNullOrEmpty(dataImportFile.Row.Item(20)) AndAlso (Not IsNumeric(dataImportFile.Row.Item(20)) OrElse Not listTypeFunctionalUnitRange.Contains(dataImportFile.Row.Item(20))) Then
                stringBuilder.AppendLine("El campo tipo de unidad funcional 2 no tiene el formato correcto")
            End If

            If stringBuilder.Length > 1 Then
                Return New ActionResult(Of String) With {.StateResult = False, .Message = stringBuilder.ToString()}
            End If

            Return New ActionResult(Of String) With {.StateResult = True}

        Catch ex As Exception
            Return New ActionResult(Of String) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Funcion para validar la condicion vs los datos
    ''' </summary>
    ''' <param name="conditionType"></param>
    ''' <param name="dictionaryOfObjects"></param>
    ''' <returns></returns>
    Function ValidateCondition(conditionType As Byte,
                                dictionaryOfObjects As Dictionary(Of EDefinitionConditionType, Object)) As ActionResult(Of String)
        Try

            If conditionType = CByte(EDefinitionConditionType.None) Then
                Return New ActionResult(Of String) With {.StateResult = False, .Message = "La regla no maneja condición"}
            End If

            Dim stringBuilder = New StringBuilder
            Dim tupleSchedule = TryCast(dictionaryOfObjects(conditionType), Tuple(Of String, String))
            Dim scheduleInit As String = tupleSchedule?.Item1
            Dim scheduleEnd As String = tupleSchedule?.Item2
            Dim speciality As INESPECIA = TryCast(dictionaryOfObjects(conditionType), INESPECIA)
            Dim functionalUnit As FunctionalUnit = TryCast(dictionaryOfObjects(conditionType), FunctionalUnit)
            Dim functionalUnitType As String = TryCast(dictionaryOfObjects(conditionType), String)
            Dim rIAS As RIAS = TryCast(dictionaryOfObjects(conditionType), RIAS)
            Dim contractDescription As ContractDescriptions = TryCast(dictionaryOfObjects(conditionType), ContractDescriptions)

            Dim starTime As DateTime
            Dim endTime As DateTime

            ' Crear un diccionario con las validaciones
            Dim validations As New Dictionary(Of Integer, Func(Of Boolean)) From {
                                                                                    {EDefinitionConditionType.Schedule, Function() Not DateTime.TryParse(scheduleInit, starTime) OrElse Not DateTime.TryParse(scheduleEnd, endTime)},
                                                                                    {EDefinitionConditionType.Speciality, Function() speciality Is Nothing OrElse String.IsNullOrEmpty(speciality.CODESPECI)},
                                                                                    {EDefinitionConditionType.FunctionalUnit, Function() functionalUnit Is Nothing OrElse functionalUnit.Id = 0},
                                                                                    {EDefinitionConditionType.FunctionalUnitType, Function() String.IsNullOrEmpty(functionalUnitType)},
                                                                                    {EDefinitionConditionType.RIAS, Function() rIAS Is Nothing OrElse rIAS.ID = 0},
                                                                                    {EDefinitionConditionType.ContractDescription, Function() contractDescription Is Nothing OrElse contractDescription.Id = 0}
                                                                                }


            Dim flagValidation As Boolean = True

            If validations.ContainsKey(CInt(conditionType)) Then
                flagValidation = Not validations(CInt(conditionType))()
            Else
                Return New ActionResult(Of String) With {.StateResult = False, .Message = "La regla no maneja condición"}
            End If

            Return New ActionResult(Of String) With {.StateResult = flagValidation}

        Catch ex As Exception
            Return New ActionResult(Of String) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
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
            _definitionRateDetailRepository = Nothing
            _specialtyRepository = Nothing
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
