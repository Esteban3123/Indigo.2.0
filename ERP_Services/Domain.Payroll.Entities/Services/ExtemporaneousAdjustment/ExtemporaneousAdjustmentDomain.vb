'***********************************************************************
' Assembly         : Domain.Payroll.Entities
' Author           : Felix Camilo Salazar Roldan
' Created          : 25-11-2025
'
' Description      : Servicio de dominio para validación de ajustes extemporáneos
'                    de Cargo y Salario Básico
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base

Public Class ExtemporaneousAdjustmentDomain
    Implements IExtemporaneousAdjustmentDomain

#Region "Constantes"
    ''' <summary>
    ''' Valor mínimo permitido para meses de ajustes extemporáneos
    ''' </summary>
    Public Const MIN_ALLOWED_MONTHS As Byte = 0

    ''' <summary>
    ''' Valor máximo permitido para meses de ajustes extemporáneos
    ''' </summary>
    Public Const MAX_ALLOWED_MONTHS As Byte = 6
#End Region

#Region "Functions"

    ''' <summary>
    ''' Determina si una fecha es extemporánea (pertenece a un mes ya liquidado/cerrado)
    ''' </summary>
    ''' <param name="selectedDate">Fecha seleccionada para el cambio</param>
    ''' <returns>True si la fecha es extemporánea, False si es del mes actual o futuro</returns>
    ''' <remarks></remarks>
    Public Function IsExtemporaneousDate(selectedDate As Date) As Boolean Implements IExtemporaneousAdjustmentDomain.IsExtemporaneousDate
        Dim firstDayOfCurrentMonth As Date = New Date(Date.Today.Year, Date.Today.Month, 1)
        Return selectedDate < firstDayOfCurrentMonth
    End Function

    ''' <summary>
    ''' Valida si una fecha extemporánea está dentro del rango permitido según parámetros
    ''' </summary>
    ''' <param name="selectedDate">Fecha seleccionada para el cambio</param>
    ''' <param name="allowedMonths">Cantidad de meses hacia atrás permitidos (0-6)</param>
    ''' <returns>True si la fecha está dentro del rango permitido, False si está fuera de rango</returns>
    ''' <remarks></remarks>
    Public Function IsWithinAllowedRange(selectedDate As Date, allowedMonths As Byte) As Boolean Implements IExtemporaneousAdjustmentDomain.IsWithinAllowedRange
        ' Si allowedMonths es 0, no se permiten ajustes extemporáneos
        If allowedMonths = 0 Then
            Return False
        End If

        ' Validar que allowedMonths esté en el rango válido
        If allowedMonths < MIN_ALLOWED_MONTHS OrElse allowedMonths > MAX_ALLOWED_MONTHS Then
            Throw New ArgumentOutOfRangeException("allowedMonths", 
                String.Format("El valor de meses permitidos debe estar entre {0} y {1}", 
                MIN_ALLOWED_MONTHS, MAX_ALLOWED_MONTHS))
        End If

        Dim firstDayOfCurrentMonth As Date = New Date(Date.Today.Year, Date.Today.Month, 1)
        Dim lowerBoundDate As Date = firstDayOfCurrentMonth.AddMonths(-allowedMonths)

        Return selectedDate >= lowerBoundDate
    End Function

    ''' <summary>
    ''' Valida completamente si una fecha puede usarse para un ajuste extemporáneo
    ''' </summary>
    ''' <param name="selectedDate">Fecha seleccionada para el cambio</param>
    ''' <param name="allowedMonths">Cantidad de meses hacia atrás permitidos (0-6)</param>
    ''' <param name="errorMessage">Mensaje de error si la validación falla</param>
    ''' <returns>True si la fecha es válida para ajuste extemporáneo, False en caso contrario</returns>
    ''' <remarks></remarks>
    Public Function ValidateExtemporaneousDate(selectedDate As Date, 
                                              allowedMonths As Byte, 
                                              ByRef errorMessage As String) As Boolean Implements IExtemporaneousAdjustmentDomain.ValidateExtemporaneousDate
        errorMessage = String.Empty

        ' Si no es extemporánea, es válida (flujo normal)
        If Not IsExtemporaneousDate(selectedDate) Then
            Return True
        End If

        ' Si es extemporánea pero allowedMonths es 0, no está permitido
        If allowedMonths = 0 Then
            errorMessage = "Los ajustes extemporáneos están deshabilitados. " & _
                          "La fecha de inicio debe ser del mes actual o posterior."
            Return False
        End If

        ' Verificar si está dentro del rango permitido
        If Not IsWithinAllowedRange(selectedDate, allowedMonths) Then
            Dim firstDayOfCurrentMonth As Date = New Date(Date.Today.Year, Date.Today.Month, 1)
            Dim lowerBoundDate As Date = firstDayOfCurrentMonth.AddMonths(-allowedMonths)
            
            errorMessage = String.Format(
                "La fecha seleccionada está fuera del rango permitido. " & _
                "Solo se permiten fechas desde {0:dd/MM/yyyy} hasta {1:dd/MM/yyyy}.", 
                lowerBoundDate, Date.Today)
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' Determina si un cambio de contrato solo modifica Cargo y/o Salario Básico
    ''' </summary>
    ''' <param name="originalContract">Contrato original antes de los cambios</param>
    ''' <param name="modifiedContract">Contrato con los cambios propuestos</param>
    ''' <param name="invalidFields">Lista de campos modificados no permitidos</param>
    ''' <returns>True si solo se modificaron Cargo y/o Salario, False si se modificaron otros campos</returns>
    ''' <remarks></remarks>
    Public Function ValidateOnlyAllowedFieldsChanged(originalContract As Contract, 
                                                    modifiedContract As Contract, 
                                                    ByRef invalidFields As List(Of String)) As Boolean Implements IExtemporaneousAdjustmentDomain.ValidateOnlyAllowedFieldsChanged
        invalidFields = New List(Of String)()

        ' Verificar que originalContract no sea Nothing
        If originalContract Is Nothing Then
            Throw New ArgumentNullException("originalContract", "El contrato original no puede ser nulo")
        End If

        If modifiedContract Is Nothing Then
            Throw New ArgumentNullException("modifiedContract", "El contrato modificado no puede ser nulo")
        End If

        ' Verificar campos que NO deben cambiar en ajustes extemporáneos
        ' (excepto PositionId y BasicSalary que SÍ pueden cambiar)

        If originalContract.ContractTypeId <> modifiedContract.ContractTypeId Then
            invalidFields.Add("Tipo de Contrato")
        End If

        If originalContract.FunctionalUnitId <> modifiedContract.FunctionalUnitId Then
            invalidFields.Add("Unidad Funcional")
        End If

        If originalContract.GroupId <> modifiedContract.GroupId Then
            invalidFields.Add("Grupo")
        End If

        If originalContract.BankId <> modifiedContract.BankId Then
            invalidFields.Add("Banco")
        End If

        If originalContract.BankAccountNumber <> modifiedContract.BankAccountNumber Then
            invalidFields.Add("Número de Cuenta")
        End If

        If originalContract.BankAccountType <> modifiedContract.BankAccountType Then
            invalidFields.Add("Tipo de Cuenta")
        End If

        If originalContract.PaymentPeriod <> modifiedContract.PaymentPeriod Then
            invalidFields.Add("Periodo de Pago")
        End If

        If originalContract.PaymentType <> modifiedContract.PaymentType Then
            invalidFields.Add("Tipo de Pago")
        End If

        If originalContract.HoursDaily <> modifiedContract.HoursDaily Then
            invalidFields.Add("Horas Diarias")
        End If

        If originalContract.SalaryType <> modifiedContract.SalaryType Then
            invalidFields.Add("Tipo de Salario")
        End If

        ' Verificar que al menos uno de los campos permitidos haya cambiado
        Dim positionChanged As Boolean = originalContract.PositionId <> modifiedContract.PositionId
        Dim salaryChanged As Boolean = originalContract.BasicSalary <> modifiedContract.BasicSalary

        If Not positionChanged AndAlso Not salaryChanged Then
            ' No hay cambios en los campos permitidos
            Return False
        End If

        ' Retornar True solo si no hay campos inválidos modificados
        Return invalidFields.Count = 0
    End Function

    ''' <summary>
    ''' Determina si un cambio de contrato debe marcarse como ajuste extemporáneo
    ''' </summary>
    ''' <param name="contractInitialDate">Fecha de inicio del contrato/Otro Sí</param>
    ''' <param name="allowedMonths">Meses permitidos para ajustes extemporáneos</param>
    ''' <param name="originalContract">Contrato original (para validar campos)</param>
    ''' <param name="modifiedContract">Contrato modificado (para validar campos)</param>
    ''' <returns>True si debe marcarse como IsExtemporaneousChange = 1, False en caso contrario</returns>
    ''' <remarks></remarks>
    Public Function ShouldMarkAsExtemporaneous(contractInitialDate As Date, 
                                              allowedMonths As Byte,
                                              originalContract As Contract,
                                              modifiedContract As Contract) As Boolean Implements IExtemporaneousAdjustmentDomain.ShouldMarkAsExtemporaneous
        ' Si no es extemporánea, es cambio normal
        If Not IsExtemporaneousDate(contractInitialDate) Then
            Return False
        End If

        ' Si es extemporánea pero no está en rango permitido, no se debe permitir
        ' (esto debería haberse validado antes, pero por seguridad lo verificamos)
        If Not IsWithinAllowedRange(contractInitialDate, allowedMonths) Then
            Return False
        End If

        ' Verificar que solo se hayan modificado campos permitidos
        Dim invalidFields As New List(Of String)()
        If Not ValidateOnlyAllowedFieldsChanged(originalContract, modifiedContract, invalidFields) Then
            Return False
        End If

        ' Todas las condiciones se cumplen: es un ajuste extemporáneo válido
        Return True
    End Function

    ''' <summary>
    ''' Calcula la fecha mínima permitida para ajustes extemporáneos
    ''' </summary>
    ''' <param name="allowedMonths">Meses permitidos hacia atrás</param>
    ''' <returns>Fecha mínima permitida</returns>
    ''' <remarks></remarks>
    Public Function GetMinimumAllowedDate(allowedMonths As Byte) As Date Implements IExtemporaneousAdjustmentDomain.GetMinimumAllowedDate
        If allowedMonths = 0 Then
            ' Si no se permiten ajustes extemporáneos, la fecha mínima es el primer día del mes actual
            Return New Date(Date.Today.Year, Date.Today.Month, 1)
        End If

        Dim firstDayOfCurrentMonth As Date = New Date(Date.Today.Year, Date.Today.Month, 1)
        Return firstDayOfCurrentMonth.AddMonths(-allowedMonths)
    End Function

    ''' <summary>
    ''' Calcula la fecha máxima permitida (siempre es la fecha actual o futura)
    ''' </summary>
    ''' <returns>Fecha máxima permitida</returns>
    ''' <remarks></remarks>
    Public Function GetMaximumAllowedDate() As Date Implements IExtemporaneousAdjustmentDomain.GetMaximumAllowedDate
        ' No hay límite superior, pero por convención usamos un año adelante
        Return Date.Today.AddYears(10)
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
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

