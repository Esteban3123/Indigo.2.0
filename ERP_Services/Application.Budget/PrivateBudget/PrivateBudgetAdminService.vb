'***********************************************************************
' Assembly         : Application.Budget
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 29/01/2018
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
Imports Application.Budget

Public Class PrivateBudgetAdminService
    Implements IPrivateBudgetAdminService

#Region "Variables"

    ''' <summary>
    ''' Variable tipo repositorio 
    ''' </summary>
    ''' <remarks></remarks>
    Private _privateBudgetRepository As IPrivateBudgetRepository


#End Region

#Region "Constructor"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal privateBudgetRepository As IPrivateBudgetRepository)
        If privateBudgetRepository Is Nothing Then
            Throw New ArgumentNullException("privateBudgetRepository Vacio")
        End If

        _privateBudgetRepository = privateBudgetRepository

    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' Elimina un registro
    ''' </summary>
    ''' <param name="PrivateBudget"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function DeletePrivateBudget(ByVal PrivateBudget As PrivateBudget, ByVal audit As AuditMessage) As ActionResult Implements IPrivateBudgetAdminService.DeletePrivateBudget
        If PrivateBudget Is Nothing Then
            Throw New ArgumentNullException("PrivateBudget")
        End If
        Dim unitOfWork As IUnitWork = Me._privateBudgetRepository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of PrivateBudget)
            auditProcess = New IndigoAuditSimpleEntity(Of PrivateBudget)(PrivateBudget, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            Me._privateBudgetRepository.DeleteEntity(PrivateBudget)
            unitOfWork.Commit()
            auditProcess.Execute()
            Return New ActionResult With {.StateResult = True}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"}), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    Public Function GetListPrivateBudget(audit As AuditMessage) As ActionResult(Of List(Of SP_ListPrivateBudget_Result)) Implements IPrivateBudgetAdminService.GetListPrivateBudget

        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim PrivateBudgetItemsStructure As List(Of SP_ListPrivateBudget_Result) = Me._privateBudgetRepository.GetListPrivateBudget()

            Return New ActionResult(Of List(Of SP_ListPrivateBudget_Result)) With {.StateResult = True, .ObjectEmbbeded = PrivateBudgetItemsStructure}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of SP_ListPrivateBudget_Result)) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function


    Public Function SavePrivateBudget(PrivateBudget As List(Of SP_ListPrivateBudget_Result), audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of List(Of SP_ListPrivateBudget_Result)) Implements IPrivateBudgetAdminService.SavePrivateBudget
        If PrivateBudget Is Nothing Then
            Throw New ArgumentNullException("PrivateBudgetItemsStructure")
        End If
        Dim unitOfWork As IUnitWork = Me._privateBudgetRepository.UnitWork

        Try
            Dim auxPrivateBudget As PrivateBudget = Nothing
            Dim auditProcess As IndigoAuditSimpleEntity(Of PrivateBudget)
            Dim status As Integer

            If PrivateBudget IsNot Nothing And PrivateBudget.Count > 0 Then
                For Each ObjPrivateBudget As SP_ListPrivateBudget_Result In PrivateBudget
                    Dim month As Integer = 0
                    Dim value As Double = 0
                    Dim isValueFill As Boolean = False
                    Dim RetentionTuple = MonthValue(ObjPrivateBudget)

                    For Each ObjTuple As Tuple(Of String, Double) In RetentionTuple
                        If ObjTuple.Item1 = "Mes" Then
                            month = ObjTuple.Item2
                        End If
                        If ObjTuple.Item1 = "Valor" Then
                            value = ObjTuple.Item2
                            isValueFill = True
                        End If

                        If isValueFill AndAlso (month > 0 AndAlso month < 13) Then
                            Dim tempBudget = _privateBudgetRepository.GetListPrivateBudgetByData(ObjPrivateBudget.IdRubro, ObjPrivateBudget.IdCuenta, ObjPrivateBudget.IdTercero, ObjPrivateBudget.IdCentroCosto, month)

                            If tempBudget.Id = 0 Then
                                tempBudget.PrivateBudgetItemsStructureId = ObjPrivateBudget.IdRubro
                                tempBudget.MainAccountId = ObjPrivateBudget.IdCuenta
                                tempBudget.ThirdPartyId = ObjPrivateBudget.IdTercero
                                tempBudget.CostCenterId = ObjPrivateBudget.IdCentroCosto
                                tempBudget.Month = month
                            End If

                            tempBudget.Value = value
                            Me._privateBudgetRepository.SaveEntity(tempBudget)

                            isValueFill = False
                            month = 0
                        End If
                    Next
                Next
            End If



            'If PrivateBudget.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
            '    PrivateBudgetItemsStructure.CreationUser = audit.CodeUser
            '    PrivateBudgetItemsStructure.CreationDate = DateTime.Now
            '    status = Infrastructure.CrossCutting.Audit.Actions.Insert
            'Else
            '    auxPrivateBudgetItemsStructure = PrivateBudgetItemsStructure.OriginalValue
            '    PrivateBudgetItemsStructure.ModificationUser = audit.CodeUser
            '    PrivateBudgetItemsStructure.ModificationDate = DateTime.Now
            '    status = Infrastructure.CrossCutting.Audit.Actions.Update
            'End If

            'For Each ObjPrivateBudget As PrivateBudget In PrivateBudget
            '    Me._privateBudgetRepository.SaveEntity(ObjPrivateBudget)
            'Next



            unitOfWork.Commit()

            ''auditProcess = New IndigoAuditSimpleEntity(Of List(Of PrivateBudget))(PrivateBudget, audit, status, auxPrivateBudget)
            ''auditProcess.Execute()

            'Se marca la entidad como sin cambios
            'PrivateBudget.MarkAsUnchanged()

            Return New ActionResult(Of List(Of SP_ListPrivateBudget_Result)) With {.StateResult = True, .ObjectEmbbeded = PrivateBudget}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of List(Of SP_ListPrivateBudget_Result)) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of SP_ListPrivateBudget_Result)) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    Public Function MonthValue(ObjSP_PrivateBudget As SP_ListPrivateBudget_Result) As List(Of Tuple(Of String, Double))

        Dim ListResult As New List(Of Tuple(Of String, Double))

        If ObjSP_PrivateBudget.Enero > 0 Then
            ListResult.Add(New Tuple(Of String, Double)("Mes", 1))
            ListResult.Add(New Tuple(Of String, Double)("Valor", ObjSP_PrivateBudget.Enero))

        End If

        If ObjSP_PrivateBudget.Febrero > 0 Then
            ListResult.Add(New Tuple(Of String, Double)("Mes", 2))
            ListResult.Add(New Tuple(Of String, Double)("Valor", ObjSP_PrivateBudget.Febrero))
        End If

        If ObjSP_PrivateBudget.Marzo > 0 Then
            ListResult.Add(New Tuple(Of String, Double)("Mes", 3))
            ListResult.Add(New Tuple(Of String, Double)("Valor", ObjSP_PrivateBudget.Marzo))
        End If

        If ObjSP_PrivateBudget.Abril > 0 Then
            ListResult.Add(New Tuple(Of String, Double)("Mes", 4))
            ListResult.Add(New Tuple(Of String, Double)("Valor", ObjSP_PrivateBudget.Abril))
        End If

        If ObjSP_PrivateBudget.Mayo > 0 Then
            ListResult.Add(New Tuple(Of String, Double)("Mes", 5))
            ListResult.Add(New Tuple(Of String, Double)("Valor", ObjSP_PrivateBudget.Mayo))
        End If

        If ObjSP_PrivateBudget.Junio > 0 Then
            ListResult.Add(New Tuple(Of String, Double)("Mes", 6))
            ListResult.Add(New Tuple(Of String, Double)("Valor", ObjSP_PrivateBudget.Junio))
        End If

        If ObjSP_PrivateBudget.Julio > 0 Then
            ListResult.Add(New Tuple(Of String, Double)("Mes", 7))
            ListResult.Add(New Tuple(Of String, Double)("Valor", ObjSP_PrivateBudget.Julio))
        End If

        If ObjSP_PrivateBudget.Agosto > 0 Then
            ListResult.Add(New Tuple(Of String, Double)("Mes", 8))
            ListResult.Add(New Tuple(Of String, Double)("Valor", ObjSP_PrivateBudget.Agosto))
        End If

        If ObjSP_PrivateBudget.Septiembre > 0 Then
            ListResult.Add(New Tuple(Of String, Double)("Mes", 9))
            ListResult.Add(New Tuple(Of String, Double)("Valor", ObjSP_PrivateBudget.Septiembre))
        End If

        If ObjSP_PrivateBudget.Octubre > 0 Then
            ListResult.Add(New Tuple(Of String, Double)("Mes", 10))
            ListResult.Add(New Tuple(Of String, Double)("Valor", ObjSP_PrivateBudget.Octubre))
        End If

        If ObjSP_PrivateBudget.Noviembre > 0 Then
            ListResult.Add(New Tuple(Of String, Double)("Mes", 11))
            ListResult.Add(New Tuple(Of String, Double)("Valor", ObjSP_PrivateBudget.Noviembre))
        End If

        If ObjSP_PrivateBudget.Diciembre > 0 Then
            ListResult.Add(New Tuple(Of String, Double)("Mes", 12))
            ListResult.Add(New Tuple(Of String, Double)("Valor", ObjSP_PrivateBudget.Diciembre))
        End If


        Return ListResult
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _privateBudgetRepository = Nothing
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
