'***********************************************************************
' Assembly         : Application.MedicalFees
' Author           : Carlos Mario Arias Rubiano
' Created          : 26/05/2015
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
Imports System.Transactions
Imports Domain.Entities.Service
Imports Application.Payments
Imports Domain.Crystal
Imports Domain.Crystal.Entities

Public Class HealthProfessionalContractAdminService
    Implements IHealthProfessionalContractAdminService

#Region "Variables"

    ''' <summary>
    ''' Repositorio para detalles de contratos que tiene asociado el medico
    ''' </summary>
    ''' <remarks></remarks>
    Private _healthProfessionalContractRepository As IHealthProfessionalContractRepository

#End Region

#Region "Builder"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(healthProfessionalContractRepository As IHealthProfessionalContractRepository)
        If healthProfessionalContractRepository Is Nothing Then
            Throw New ArgumentNullException("healthProfessionalContractRepository Vacio")
        End If
        _healthProfessionalContractRepository = healthProfessionalContractRepository
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene un detalle de contrato que tiene asociado el medico por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetHealthProfessionalContractById(id As Integer) As ActionResult(Of HealthProfessionalContract) Implements IHealthProfessionalContractAdminService.GetHealthProfessionalContractById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Try
            Dim healthProfessionalContract As HealthProfessionalContract = Me._healthProfessionalContractRepository.GetHealthProfessionalContractById(id)
            Return New ActionResult(Of HealthProfessionalContract) With {.StateResult = True, .ObjectEmbbeded = healthProfessionalContract}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of HealthProfessionalContract) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un listado de detalles de contratos que tiene asociado un medico
    ''' </summary>
    ''' <param name="healthProfessionalCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListHealthProfessionalContractByHealthProfessionalCode(healthProfessionalCode As String) As ActionResult(Of List(Of HealthProfessionalContract)) Implements IHealthProfessionalContractAdminService.GetListHealthProfessionalContractByHealthProfessionalCode
        If healthProfessionalCode Is String.Empty Then
            Throw New ArgumentNullException("healthProfessionalCode")
        End If
        Try
            Dim ListHealthProfessionalContract As List(Of HealthProfessionalContract) = Me._healthProfessionalContractRepository.GetListHealthProfessionalContractByHealthProfessionalCode(healthProfessionalCode)
            Return New ActionResult(Of List(Of HealthProfessionalContract)) With {.StateResult = True, .ObjectEmbbeded = ListHealthProfessionalContract}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of HealthProfessionalContract)) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene el listado de contratos que tiene asociado el médico y son de tipo estandar
    ''' </summary>
    ''' <param name="healthProfessionalCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListHealthProfessionalContractWithTypeStandard(healthProfessionalCode As String) As ActionResult(Of List(Of HealthProfessionalContract)) Implements IHealthProfessionalContractAdminService.ListHealthProfessionalContractWithTypeStandard
        If healthProfessionalCode Is String.Empty Then
            Throw New ArgumentNullException("healthProfessionalCode")
        End If
        Try
            Dim ListHealthProfessionalContract As List(Of HealthProfessionalContract) = Me._healthProfessionalContractRepository.ListHealthProfessionalContractWithTypeStandard(healthProfessionalCode)
            Return New ActionResult(Of List(Of HealthProfessionalContract)) With {.StateResult = True, .ObjectEmbbeded = ListHealthProfessionalContract}
        Catch ex As ArgumentNullException
            Return New ActionResult(Of List(Of HealthProfessionalContract)) With {.StateResult = False, .Message = ex.ParamName}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of HealthProfessionalContract)) With {.StateResult = False, .Message = ex.Message}
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
            _healthProfessionalContractRepository = Nothing
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
