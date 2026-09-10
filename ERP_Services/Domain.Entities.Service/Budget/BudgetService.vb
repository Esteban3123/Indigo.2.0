'************************************************************
' Assembly         : Domain.Entities.Service
' Author           : Carlos Ernesto Cordoba
' Created          : 24-11-2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Infrastructure.CrossCutting
Imports Domain.Base
Imports Domain.Base.Entities
Imports System.Globalization
Imports Infrastructure.CrossCutting.Resources
Imports Domain.Entities
Imports Domain.Crystal.Entities
Imports System.Text
Imports Domain.Payroll
Imports Infrastructure.CrossCutting.Base
Imports System.Dynamic
Imports Domain.Crystal


#End Region

Public Class BudgetService
    Implements IBudgetService

    Private _validityRepository As IValidityRepository

    Private _categoryRepository As IBudgetItemRepository

    Private _revenueTypeRepository As IExpenseTypeRepository

    Private _availabilityRepository As IAvailabilityRepository

    Private _commitmentRepository As ICommitmentRepository

    Private _obligationRepository As IObligationRepository

    Private _budgetRepository As IBudgetRepository

    Private _suspensionDetail As ISuspensionDetailRepository

    Private _availabilityDetailRepository As IAvailabilityDetailRepository

    Public Sub New(validityRepository As IValidityRepository, categoryRepository As IBudgetItemRepository, revenueTypeRepository As IExpenseTypeRepository,
                   availabilityRepository As IAvailabilityRepository, commitmentRepository As ICommitmentRepository, obligationRepository As IObligationRepository,
                   budgetRepository As IBudgetRepository, suspensionDetail As ISuspensionDetailRepository, availabilityDetailRepository As IAvailabilityDetailRepository)
        _validityRepository = validityRepository
        _categoryRepository = categoryRepository
        _revenueTypeRepository = revenueTypeRepository
        _availabilityRepository = availabilityRepository
        _commitmentRepository = commitmentRepository
        _obligationRepository = obligationRepository
        _budgetRepository = budgetRepository
        _suspensionDetail = suspensionDetail
        _availabilityDetailRepository = availabilityDetailRepository
    End Sub

    ''' <summary>
    ''' metodo para validar el periodo de presupuesto
    ''' </summary>
    ''' <param name="validityId"></param>
    ''' <param name="documentDate"></param>
    ''' <param name="budgetType"></param>
    ''' <returns></returns>
    Public Function ValidateBudgetPeriod(validityId As Integer, documentDate As Date, budgetType As EBudgetType) As ActionResult Implements IBudgetService.ValidateBudgetPeriod
        Dim validity = _validityRepository.GetValidity(validityId, False)
        If budgetType = EBudgetType.Income Then
            If documentDate.Year <> validity.Year Or documentDate.Month <> validity.IncomeMonth Then
                Return New ActionResult With {.StateResult = False, .Message = "El mes " + DateAndTime.MonthName(documentDate.Month).ToUpper() + " y el año " + documentDate.Year.ToString() + " del documento no coinciden con los de la vigencia (" + DateAndTime.MonthName(validity.IncomeMonth).ToUpper() + " - " + validity.Year.ToString() + ")"}
            End If
            Return New ActionResult With {.StateResult = True}
        Else
            If documentDate.Year <> validity.Year Or documentDate.Month <> validity.ExpenseMonth Then
                Return New ActionResult With {.StateResult = False, .Message = "El mes " + DateAndTime.MonthName(documentDate.Month).ToUpper() + " y el año " + documentDate.Year.ToString() + " del documento no coinciden con los de la vigencia (" + DateAndTime.MonthName(validity.ExpenseMonth).ToUpper() + " - " + validity.Year.ToString() + ")"}
            End If
            Return New ActionResult With {.StateResult = True}
        End If
    End Function

    ''' <summary>
    ''' Metodo para validar los datos de traslado de presupuesto
    ''' </summary>
    ''' <param name="budgetTransfer"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateBudgetTransfer(budgetTransfer As BudgetTransfer) As String Implements IBudgetService.ValidateBudgetTransfer
        Dim listErrors As New StringBuilder
        'Se valida que hayan detalles de traslado en la rejilla
        If Not (budgetTransfer.BudgetTransferDetail IsNot Nothing AndAlso budgetTransfer.BudgetTransferDetail.Count > 0) Then
            listErrors.AppendLine("No hay detalles de traslado para poder guardar.")
        End If
        'Se valida que el debito sea igual que el credito
        If budgetTransfer.BudgetTransferDetail IsNot Nothing AndAlso budgetTransfer.BudgetTransferDetail.Count > 0 Then
            Dim sumCredit As Decimal = budgetTransfer.BudgetTransferDetail.Sum(Function(item) As Decimal
                                                                                   Return IIf(item.Nature = 2, item.Value, CDec(0.0))
                                                                               End Function)
            Dim sumDebit As Decimal = budgetTransfer.BudgetTransferDetail.Sum(Function(item) As Decimal
                                                                                  Return IIf(item.Nature = 1, item.Value, CDec(0.0))
                                                                              End Function)
            If sumCredit <> sumDebit Then
                listErrors.AppendLine("Los débitos y los créditos de los detalles del traslado no son iguales.")
            End If
            'Se valida que en los detalles no haya ningun item con naturaleza ninguna
            budgetTransfer.BudgetTransferDetail.ToList().ForEach(Sub(item)
                                                                     Dim category = _categoryRepository.GetCategoryById(item.CategoryId)
                                                                     Dim revenueType = _revenueTypeRepository.GetRevenueTypeById(item.RevenueTypeId)

                                                                     If item.Nature = 0 Then
                                                                         listErrors.AppendLine("La naturaleza del rubro " + category.Code + " - " + category.Name + " con el tipo de ingreso " + revenueType.Code + " - " + revenueType.Name + " no puede ser Ninguna.")
                                                                     End If
                                                                     If item.Nature = 1 And item.Value > item.Balance Then
                                                                         listErrors.AppendLine("El valor debito del rubro " + category.Code + " - " + category.Name + " con el tipo de ingreso " + revenueType.Code + " - " + revenueType.Name + " no puede ser mayor que el saldo.")
                                                                     End If
                                                                 End Sub)
        End If
        Return listErrors.ToString()
    End Function

    ''' <summary>
    ''' Metodo para validar los datos de la modificacion del pac
    ''' </summary>
    ''' <param name="annualizedCashFlowModification"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidatePACModification(annualizedCashFlowModification As AnnualizedCashFlowModification) As String Implements IBudgetService.ValidatePACModification
        Dim listErrors As New StringBuilder
        'Se valida que hayan detalles de traslado en la rejilla
        If Not (annualizedCashFlowModification.AnnualizedCashFlowModificationDetail IsNot Nothing AndAlso annualizedCashFlowModification.AnnualizedCashFlowModificationDetail.Count > 0) Then
            listErrors.AppendLine("No hay detalles de Modificación de PAC para poder guardar.")
        End If
        Return listErrors.ToString()
    End Function

    ''' <summary>
    ''' Metodo para validar los datos del traslado del pac
    ''' </summary>
    ''' <param name="annualizedCashFlowTrasnfer"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidatePACTransfer(annualizedCashFlowTrasnfer As AnnualizedCashFlowTransfer) As String Implements IBudgetService.ValidatePACTransfer
        Dim listErrors As New StringBuilder
        'Se valida que hayan detalles de traslado en la rejilla
        If Not (annualizedCashFlowTrasnfer.AnnualizedCashFlowTransferDetail IsNot Nothing AndAlso annualizedCashFlowTrasnfer.AnnualizedCashFlowTransferDetail.Count > 0) Then
            listErrors.AppendLine("No hay detalles de traslado del PAC para poder guardar.")
        End If
        'Se valida que el debito sea igual que el credito
        If annualizedCashFlowTrasnfer.AnnualizedCashFlowTransferDetail IsNot Nothing AndAlso annualizedCashFlowTrasnfer.AnnualizedCashFlowTransferDetail.Count > 0 Then

            Dim sumCredit As Decimal = annualizedCashFlowTrasnfer.AnnualizedCashFlowTransferDetail.Sum(Function(item) As Decimal
                                                                                                           Return IIf(item.Nature = 2, item.Value, CDec(0.0))
                                                                                                       End Function)
            Dim sumDebit As Decimal = annualizedCashFlowTrasnfer.AnnualizedCashFlowTransferDetail.Sum(Function(item) As Decimal
                                                                                                          Return IIf(item.Nature = 1, item.Value, CDec(0.0))
                                                                                                      End Function)
            If sumCredit <> sumDebit Then
                listErrors.AppendLine("Los débitos y los créditos de los detalles del traslado no son iguales.")
            End If
        End If
        Return listErrors.ToString()
    End Function

    ''' <summary>
    ''' Metodo para validar los datos del compromiso
    ''' </summary>
    ''' <param name="commitment"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateCommitment(commitment As Commitment) As String Implements IBudgetService.ValidateCommitment
        Dim listErrors As New StringBuilder
        'Se valida que hayan detalles de traslado en la rejilla
        If Not (commitment.CommitmentDetail IsNot Nothing AndAlso commitment.CommitmentDetail.Count > 0) Then
            listErrors.AppendLine("No hay detalles del compromiso para poder guardar.")
        End If
        'Se valida que el debito sea igual que el credito
        If commitment.CommitmentDetail IsNot Nothing AndAlso commitment.CommitmentDetail.Count > 0 Then
            'Se valida que en los detalles no haya ningun item con naturaleza ninguna
            For Each item In commitment.CommitmentDetail.ToList()
                Dim category = _categoryRepository.GetCategoryById(item.CategoryId)
                Dim revenueType = _revenueTypeRepository.GetRevenueTypeById(item.RevenueTypeId)

                If commitment.CommitmentType = 1 Then
                    Dim availabilityDetail = _availabilityDetailRepository.GetAvailabilityDetailById(item.AvailabilityDetailId, True)
                    Dim availability = _availabilityRepository.GetAvailabilityById(availabilityDetail.AvailabilityId, True)
                    If item.InitialValue > availabilityDetail.Balance Then
                        listErrors.AppendLine("el valor inicial del rubro " + category.Code + " - " + category.Name + " con el tipo de ingreso " + revenueType.Code + " - " + revenueType.Name + " no puede ser mayor al saldo de la disponibilidad.")
                    End If
                    If availability.DocumentDate > commitment.DocumentDate Then
                        listErrors.AppendLine("No se puede agregar los rubros de la disponibilidad " + availability.Code + " porque la fecha del compromiso es superior a la fecha de la disponibilidad.")
                    End If
                    If availability.ExpirationDate < commitment.DocumentDate Then
                        listErrors.AppendLine("No se puede agregar los rubros de la disponibilidad " + availability.Code + " porque la fecha de Vencimiento de la disponibilidad es menor a la fecha del compromiso.")
                    End If
                End If
                If item.InitialValue = 0 Then
                    listErrors.AppendLine("el valor inicial del rubro " + category.Code + " - " + category.Name + " con el tipo de ingreso " + revenueType.Code + " - " + revenueType.Name + " no puede ser 0.")
                End If
                If item.ExpiredDate < commitment.DocumentDate Then
                    listErrors.AppendLine("la fecha de vencimiento del rubro " + category.Code + " - " + category.Name + " con el tipo de ingreso " + revenueType.Code + " - " + revenueType.Name + " debe ser superior a la fecha del documento.")
                End If
            Next
        End If
        Return listErrors.ToString()
    End Function

    ''' <summary>
    ''' Metodo para validar los datos de una suspension presupuestal
    ''' </summary>
    ''' <param name="suspension"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateSuspension(suspension As Suspension) As String Implements IBudgetService.ValidateSuspension
        Dim listErrors As New StringBuilder

        'Se valida que hayan detalles de suspencion en la rejilla
        If Not (suspension.SuspensionDetail IsNot Nothing AndAlso suspension.SuspensionDetail.Count > 0) Then
            listErrors.AppendLine("No hay detalles de la suspencion de presupuesto para poder guardar.")
        End If
        'Se valida que el debito sea igual que el credito
        If suspension.SuspensionDetail IsNot Nothing AndAlso suspension.SuspensionDetail.Count > 0 Then
            'Se valida que en los detalles no haya ningun item con naturaleza ninguna
            For Each item In suspension.SuspensionDetail
                Dim budget = _budgetRepository.GetBudgetByIdAsNotTracking(item.BudgetId)
                Dim category = _categoryRepository.GetCategoryById(budget.CategoryId)
                Dim revenueType = _revenueTypeRepository.GetRevenueTypeById(budget.RevenueTypeId)

                If item.InitialValue = 0 Then
                    listErrors.AppendLine("el valor inicial del rubro " + category.Code + " - " + category.Name + " con el tipo de ingreso " + revenueType.Code + " - " + revenueType.Name + " no puede ser 0.")
                End If
                If item.InitialValue > budget.Balance Then
                    listErrors.AppendLine("el valor inicial del rubro " + category.Code + " - " + category.Name + " con el tipo de ingreso " + revenueType.Code + " - " + revenueType.Name + " no puede ser mayor al saldo.")
                End If
            Next
        End If
        Return listErrors.ToString()
    End Function

    ''' <summary>
    ''' Metodo para validar los datos de un levantamiento de suspencion presupuestal
    ''' </summary>
    ''' <param name="suspensionCancellation"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateSuspensionCancellation(suspensionCancellation As SuspensionCancellation) As String Implements IBudgetService.ValidateSuspensionCancellation
        Dim listErrors As New StringBuilder

        'Se valida que hayan detalles de levantamiento de suspencion en la rejilla
        If Not (suspensionCancellation.SuspensionCancellationDetail IsNot Nothing AndAlso suspensionCancellation.SuspensionCancellationDetail.Count > 0) Then
            listErrors.AppendLine("No hay detalles de el levantamiento suspención de presupuesto para poder guardar.")
        End If
        'Se valida que el debito sea igual que el credito
        If suspensionCancellation.SuspensionCancellationDetail IsNot Nothing AndAlso suspensionCancellation.SuspensionCancellationDetail.Count > 0 Then
            'Se valida que en los detalles no haya ningun item con naturaleza ninguna
            For Each item In suspensionCancellation.SuspensionCancellationDetail
                Dim suspensionDetail = _suspensionDetail.GetSuspensionDetailById(item.SuspensionDetailId, False)
                Dim budget = _budgetRepository.GetBudgetByIdAsNotTracking(suspensionDetail.BudgetId)
                Dim category = _categoryRepository.GetCategoryById(budget.CategoryId)
                Dim revenueType = _revenueTypeRepository.GetRevenueTypeById(budget.RevenueTypeId)

                If item.Value = 0 Then
                    listErrors.AppendLine("el valor inicial del rubro " + category.Code + " - " + category.Name + " con el tipo de ingreso " + revenueType.Code + " - " + revenueType.Name + " no puede ser 0.")
                End If
                If item.Value > suspensionDetail.Balance Then
                    listErrors.AppendLine("el valor inicial del rubro " + category.Code + " - " + category.Name + " con el tipo de ingreso " + revenueType.Code + " - " + revenueType.Name + " no puede ser mayor al saldo.")
                End If
            Next
        End If
        Return listErrors.ToString()
    End Function


#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _validityRepository = Nothing
            _categoryRepository = Nothing
            _revenueTypeRepository = Nothing
            _availabilityRepository = Nothing
            _commitmentRepository = Nothing
            _obligationRepository = Nothing
            _budgetRepository = Nothing
            _suspensionDetail = Nothing
            _availabilityDetailRepository = Nothing
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

Public Enum EBudgetType As Integer
    ''' <summary>
    ''' Ingreso
    ''' </summary>
    ''' <remarks></remarks>
    Income = 1
    ''' <summary>
    ''' Gasto
    ''' </summary>
    ''' <remarks></remarks>
    Expense = 2
End Enum