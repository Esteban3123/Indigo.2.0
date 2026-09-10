'***********************************************************************
' Assembly         : Domain.Payroll.Entities
' Author           : Felix Camilo Salazar Roldan
' Created          : 25-11-2025
'
' Description      : Interfaz del servicio de dominio para validación de ajustes extemporáneos
'                    de Cargo y Salario Básico
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Public Interface IExtemporaneousAdjustmentDomain
    Inherits IDisposable

    ''' <summary>
    ''' Determina si una fecha es extemporánea (pertenece a un mes ya liquidado/cerrado)
    ''' </summary>
    ''' <param name="selectedDate">Fecha seleccionada para el cambio</param>
    ''' <returns>True si la fecha es extemporánea, False si es del mes actual o futuro</returns>
    ''' <remarks></remarks>
    Function IsExtemporaneousDate(selectedDate As Date) As Boolean

    ''' <summary>
    ''' Valida si una fecha extemporánea está dentro del rango permitido según parámetros
    ''' </summary>
    ''' <param name="selectedDate">Fecha seleccionada para el cambio</param>
    ''' <param name="allowedMonths">Cantidad de meses hacia atrás permitidos (0-6)</param>
    ''' <returns>True si la fecha está dentro del rango permitido, False si está fuera de rango</returns>
    ''' <remarks></remarks>
    Function IsWithinAllowedRange(selectedDate As Date, allowedMonths As Byte) As Boolean

    ''' <summary>
    ''' Valida completamente si una fecha puede usarse para un ajuste extemporáneo
    ''' </summary>
    ''' <param name="selectedDate">Fecha seleccionada para el cambio</param>
    ''' <param name="allowedMonths">Cantidad de meses hacia atrás permitidos (0-6)</param>
    ''' <param name="errorMessage">Mensaje de error si la validación falla</param>
    ''' <returns>True si la fecha es válida para ajuste extemporáneo, False en caso contrario</returns>
    ''' <remarks></remarks>
    Function ValidateExtemporaneousDate(selectedDate As Date,
                                        allowedMonths As Byte,
                                        ByRef errorMessage As String) As Boolean

    ''' <summary>
    ''' Determina si un cambio de contrato solo modifica Cargo y/o Salario Básico
    ''' </summary>
    ''' <param name="originalContract">Contrato original antes de los cambios</param>
    ''' <param name="modifiedContract">Contrato con los cambios propuestos</param>
    ''' <param name="invalidFields">Lista de campos modificados no permitidos</param>
    ''' <returns>True si solo se modificaron Cargo y/o Salario, False si se modificaron otros campos</returns>
    ''' <remarks></remarks>
    Function ValidateOnlyAllowedFieldsChanged(originalContract As Contract,
                                             modifiedContract As Contract,
                                             ByRef invalidFields As List(Of String)) As Boolean

    ''' <summary>
    ''' Determina si un cambio de contrato debe marcarse como ajuste extemporáneo
    ''' </summary>
    ''' <param name="contractInitialDate">Fecha de inicio del contrato/Otro Sí</param>
    ''' <param name="allowedMonths">Meses permitidos para ajustes extemporáneos</param>
    ''' <param name="originalContract">Contrato original (para validar campos)</param>
    ''' <param name="modifiedContract">Contrato modificado (para validar campos)</param>
    ''' <returns>True si debe marcarse como IsExtemporaneousChange = 1, False en caso contrario</returns>
    ''' <remarks></remarks>
    Function ShouldMarkAsExtemporaneous(contractInitialDate As Date,
                                       allowedMonths As Byte,
                                       originalContract As Contract,
                                       modifiedContract As Contract) As Boolean

    ''' <summary>
    ''' Calcula la fecha mínima permitida para ajustes extemporáneos
    ''' </summary>
    ''' <param name="allowedMonths">Meses permitidos hacia atrás</param>
    ''' <returns>Fecha mínima permitida</returns>
    ''' <remarks></remarks>
    Function GetMinimumAllowedDate(allowedMonths As Byte) As Date

    ''' <summary>
    ''' Calcula la fecha máxima permitida (siempre es la fecha actual o futura)
    ''' </summary>
    ''' <returns>Fecha máxima permitida</returns>
    ''' <remarks></remarks>
    Function GetMaximumAllowedDate() As Date

End Interface
