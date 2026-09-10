'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Jose Luis Rojas
' Created          : 23-09-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base
Imports Domain.Payroll.Entities
Imports DevExpress.Xpo
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.Data.Async.Helpers

#End Region
''' <summary>
''' Clase que contiene complementos que para el funcionamiento correcto de los frontales que interfieran con el empleado
''' </summary>
Public Class EmployeeHelper

#Region "Fields N Properties"

    ''' <summary>
    ''' Propiedad que contiene lista de los tipos de identificacion en la forma de una tupla(enum, idBd, descripcion)
    ''' </summary>
    <Obsolete>
    Private Shared _identificationType As List(Of Tuple(Of Integer, Integer, String))
    Public Shared ReadOnly Property IdentificationType As List(Of Tuple(Of Integer, Integer, String))
        Get

            If _identificationType Is Nothing Then
                _identificationType = New List(Of Tuple(Of Integer, Integer, String))()
                _identificationType.Add(New Tuple(Of Integer, Integer, String)(Eresources.CedulaCiudadania, 0, obtenerRecurso(Eresources.CedulaCiudadania)))
                _identificationType.Add(New Tuple(Of Integer, Integer, String)(Eresources.CedulaExtranjeria, 1, obtenerRecurso(Eresources.CedulaExtranjeria)))
                _identificationType.Add(New Tuple(Of Integer, Integer, String)(Eresources.TarjetaIdentidad, 2, obtenerRecurso(Eresources.TarjetaIdentidad)))
                _identificationType.Add(New Tuple(Of Integer, Integer, String)(Eresources.Pasaporte, 4, obtenerRecurso(Eresources.Pasaporte)))
                _identificationType.Add(New Tuple(Of Integer, Integer, String)(Eresources.CarnetDiplomatico, 10, obtenerRecurso(Eresources.CarnetDiplomatico)))
                _identificationType.Add(New Tuple(Of Integer, Integer, String)(Eresources.Salvoconducto, 11, obtenerRecurso(Eresources.Salvoconducto)))
                _identificationType.Add(New Tuple(Of Integer, Integer, String)(Eresources.RegistroCivil, 3, obtenerRecurso(Eresources.RegistroCivil)))
                _identificationType.Add(New Tuple(Of Integer, Integer, String)(Eresources.PermisoEspecialPermanencia, 12, obtenerRecurso(Eresources.PermisoEspecialPermanencia)))
                _identificationType.Add(New Tuple(Of Integer, Integer, String)(Eresources.PermisoProteccionTemporal, 13, obtenerRecurso(Eresources.PermisoProteccionTemporal)))
            End If

            Return _identificationType
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que contiene lista de los tipos de cesantias en la forma de una tupla(enum, idBd, descripcion)
    ''' </summary>
    Private Shared _severanceType As List(Of Tuple(Of Integer, Integer, String))
    Public Shared ReadOnly Property SeveranceType As List(Of Tuple(Of Integer, Integer, String))
        Get

            If _severanceType Is Nothing Then
                _severanceType = New List(Of Tuple(Of Integer, Integer, String))()
                _severanceType.Add(New Tuple(Of Integer, Integer, String)(Eresources.NoAplica, 1, obtenerRecurso(Eresources.NoAplica, Empleado)))
                _severanceType.Add(New Tuple(Of Integer, Integer, String)(Eresources.Tradicional, 2, obtenerRecurso(Eresources.Tradicional, Empleado)))
                _severanceType.Add(New Tuple(Of Integer, Integer, String)(Eresources.TradicionalMensual, 3, obtenerRecurso(Eresources.TradicionalMensual, Empleado)))
                _severanceType.Add(New Tuple(Of Integer, Integer, String)(Eresources.Consignado, 4, obtenerRecurso(Eresources.Consignado, Empleado)))
                _severanceType.Add(New Tuple(Of Integer, Integer, String)(Eresources.ConsignadoMensual, 5, obtenerRecurso(Eresources.ConsignadoMensual, Empleado)))
                _severanceType.Add(New Tuple(Of Integer, Integer, String)(Eresources.Ley33, 6, obtenerRecurso(Eresources.Ley33, Empleado)))
            End If

            Return _severanceType
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que contiene lista de los tipos de sindicatos en la forma de una tupla(enum, idBd, descripcion)
    ''' </summary>
    Private Shared _tradeUnion As List(Of Tuple(Of Integer, Integer, String))
    Public Shared ReadOnly Property TradeUnion As List(Of Tuple(Of Integer, Integer, String))
        Get

            If _tradeUnion Is Nothing Then
                _tradeUnion = New List(Of Tuple(Of Integer, Integer, String))()
                _tradeUnion.Add(New Tuple(Of Integer, Integer, String)(Eresources.Ninguno, 1, obtenerRecurso(Eresources.Ninguno)))
                _tradeUnion.Add(New Tuple(Of Integer, Integer, String)(Eresources.Convencionado, 2, obtenerRecurso(Eresources.Convencionado, Empleado)))
                _tradeUnion.Add(New Tuple(Of Integer, Integer, String)(Eresources.Sindicalizado, 3, obtenerRecurso(Eresources.Sindicalizado, Empleado)))
                _tradeUnion.Add(New Tuple(Of Integer, Integer, String)(Eresources.PactoColectivo, 4, obtenerRecurso(Eresources.PactoColectivo, Empleado)))
            End If

            Return _tradeUnion

        End Get
    End Property

    ''' <summary>
    ''' Propiedad que contiene lista de los tipos de estado civil en la forma de una tupla(enum, idBd, descripcion)
    ''' </summary>
    Private Shared _maritalStatus As List(Of Tuple(Of Integer, Integer, String))
    Public Shared ReadOnly Property MaritalStatus As List(Of Tuple(Of Integer, Integer, String))
        Get

            If _maritalStatus Is Nothing Then
                _maritalStatus = New List(Of Tuple(Of Integer, Integer, String))()
                _maritalStatus.Add(New Tuple(Of Integer, Integer, String)(Eresources.Soltero, 0, obtenerRecurso(Eresources.Soltero)))
                _maritalStatus.Add(New Tuple(Of Integer, Integer, String)(Eresources.Casado, 1, obtenerRecurso(Eresources.Casado)))
                _maritalStatus.Add(New Tuple(Of Integer, Integer, String)(Eresources.Divorciado, 2, obtenerRecurso(Eresources.Divorciado)))
                _maritalStatus.Add(New Tuple(Of Integer, Integer, String)(Eresources.Viudo, 3, obtenerRecurso(Eresources.Viudo)))
                _maritalStatus.Add(New Tuple(Of Integer, Integer, String)(Eresources.UnionLibre, 4, obtenerRecurso(Eresources.UnionLibre)))
            End If

            Return _maritalStatus
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que contiene lista de los tipos de estado de estudio en la forma de una tupla(enum, idBd, descripcion)
    ''' </summary>
    Private Shared _studyStatus As List(Of Tuple(Of Integer, Integer, String))
    Public Shared ReadOnly Property StudyStatus As List(Of Tuple(Of Integer, Integer, String))
        Get

            If _studyStatus Is Nothing Then
                _studyStatus = New List(Of Tuple(Of Integer, Integer, String))()
                _studyStatus.Add(New Tuple(Of Integer, Integer, String)(Eresources.EstudiaActualmente, 1, obtenerRecurso(Eresources.EstudiaActualmente, Empleado)))
                _studyStatus.Add(New Tuple(Of Integer, Integer, String)(Eresources.EstudioInterrumpido, 2, obtenerRecurso(Eresources.EstudioInterrumpido, Empleado)))
                _studyStatus.Add(New Tuple(Of Integer, Integer, String)(Eresources.EstudioTerminado, 3, obtenerRecurso(Eresources.EstudioTerminado, Empleado)))
                _studyStatus.Add(New Tuple(Of Integer, Integer, String)(Eresources.Graduado, 4, obtenerRecurso(Eresources.Graduado, Empleado)))
            End If

            Return _studyStatus
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que contiene lista de los tipos de la jerarquia de estudios en la forma de una tupla(enum, idBd, descripcion)
    ''' </summary>
    Private Shared _studyHierarchy As List(Of Tuple(Of Integer, Integer, String))
    Public Shared ReadOnly Property StudyHierarchy As List(Of Tuple(Of Integer, Integer, String))
        Get

            If _studyHierarchy Is Nothing Then
                _studyHierarchy = New List(Of Tuple(Of Integer, Integer, String))()
                _studyHierarchy.Add(New Tuple(Of Integer, Integer, String)(Eresources.Primaria, 1, obtenerRecurso(Eresources.Primaria, TipoEstudio)))
                _studyHierarchy.Add(New Tuple(Of Integer, Integer, String)(Eresources.Secundaria, 2, obtenerRecurso(Eresources.Secundaria, TipoEstudio)))
                _studyHierarchy.Add(New Tuple(Of Integer, Integer, String)(Eresources.Tecnico, 3, obtenerRecurso(Eresources.Tecnico, TipoEstudio)))
                _studyHierarchy.Add(New Tuple(Of Integer, Integer, String)(Eresources.Universitario, 4, obtenerRecurso(Eresources.Universitario, TipoEstudio)))
                _studyHierarchy.Add(New Tuple(Of Integer, Integer, String)(Eresources.Postgrado, 5, obtenerRecurso(Eresources.Postgrado, TipoEstudio)))
            End If

            Return _studyHierarchy
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que contiene lista de los generos en la forma de una tupla(enum, idBd, descripcion)
    ''' </summary>
    Private Shared _gender As List(Of Tuple(Of Integer, Integer, String))
    Public Shared ReadOnly Property Gender As List(Of Tuple(Of Integer, Integer, String))
        Get

            If _gender Is Nothing Then
                _gender = New List(Of Tuple(Of Integer, Integer, String))()
                _gender.Add(New Tuple(Of Integer, Integer, String)(Eresources.Masculino, 1, obtenerRecurso(Eresources.Masculino)))
                _gender.Add(New Tuple(Of Integer, Integer, String)(Eresources.Femenino, 2, obtenerRecurso(Eresources.Femenino)))
                _gender.Add(New Tuple(Of Integer, Integer, String)(Eresources.Otro, 3, obtenerRecurso(Eresources.Otro)))
            End If

            Return _gender
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que contiene lista de los tipos de libreta militar en la forma de una tupla(enum, idBd, descripcion)
    ''' </summary>
    Private Shared _militaryCardType As List(Of Tuple(Of Integer, Integer, String))
    Public Shared ReadOnly Property MilitaryCardType As List(Of Tuple(Of Integer, Integer, String))
        Get

            If _militaryCardType Is Nothing Then
                _militaryCardType = New List(Of Tuple(Of Integer, Integer, String))()
                _militaryCardType.Add(New Tuple(Of Integer, Integer, String)(Eresources.NoTieneLibreta, 0, obtenerRecurso(Eresources.NoTieneLibreta, Empleado)))
                _militaryCardType.Add(New Tuple(Of Integer, Integer, String)(Eresources.LibretaPrimera, 1, obtenerRecurso(Eresources.LibretaPrimera, Empleado)))
                _militaryCardType.Add(New Tuple(Of Integer, Integer, String)(Eresources.LibretaSegunda, 2, obtenerRecurso(Eresources.LibretaSegunda, Empleado)))
            End If

            Return _militaryCardType
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que contiene lista de los grupos sanguineos en la forma de una tupla(enum, idBd, descripcion)
    ''' </summary>
    Private Shared _bloodGroup As List(Of Tuple(Of String, String))
    Public Shared ReadOnly Property BloodGroup As List(Of Tuple(Of String, String))
        Get

            If _bloodGroup Is Nothing Then
                _bloodGroup = New List(Of Tuple(Of String, String))()
                _bloodGroup.Add(New Tuple(Of String, String)("O", "O"))
                _bloodGroup.Add(New Tuple(Of String, String)("A", "A"))
                _bloodGroup.Add(New Tuple(Of String, String)("B", "B"))
                _bloodGroup.Add(New Tuple(Of String, String)("AB", "AB"))
            End If

            Return _bloodGroup
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que contiene lista de los rh en la forma de una tupla(enum, idBd, descripcion)
    ''' </summary>
    Private Shared _rh As List(Of Tuple(Of String, String))
    Public Shared ReadOnly Property RH As List(Of Tuple(Of String, String))
        Get

            If _rh Is Nothing Then
                _rh = New List(Of Tuple(Of String, String))()
                _rh.Add(New Tuple(Of String, String)("+", "+"))
                _rh.Add(New Tuple(Of String, String)("-", "-"))
            End If

            Return _rh
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que contiene Si o No en la forma de una tupla(enum, idBd, descripcion)
    ''' </summary>
    Private Shared _yesNoBoolean As List(Of Tuple(Of Integer, Boolean, String))
    Public Shared ReadOnly Property YesNoBoolean As List(Of Tuple(Of Integer, Boolean, String))
        Get

            If _yesNoBoolean Is Nothing Then
                _yesNoBoolean = New List(Of Tuple(Of Integer, Boolean, String))
                _yesNoBoolean.Add(New Tuple(Of Integer, Boolean, String)(Eresources.Si, True, obtenerRecurso(Eresources.Si)))
                _yesNoBoolean.Add(New Tuple(Of Integer, Boolean, String)(Eresources.No, False, obtenerRecurso(Eresources.No)))
            End If

            Return _yesNoBoolean
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que contiene Si o No en la forma de una tupla(enum, idBd, descripcion)
    ''' </summary>
    Private Shared _yesNoInteger As List(Of Tuple(Of Integer, Integer, String))
    Public Shared ReadOnly Property YesNoInteger As List(Of Tuple(Of Integer, Integer, String))
        Get

            If _yesNoInteger Is Nothing Then
                _yesNoInteger = New List(Of Tuple(Of Integer, Integer, String))
                _yesNoInteger.Add(New Tuple(Of Integer, Integer, String)(Eresources.Si, 1, obtenerRecurso(Eresources.Si)))
                _yesNoInteger.Add(New Tuple(Of Integer, Integer, String)(Eresources.No, 0, obtenerRecurso(Eresources.No)))
            End If

            Return _yesNoInteger
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que contiene estado activo y suspendido en la forma de una tupla(enum, idBd, descripcion)
    ''' </summary>
    Private Shared _activeSuspendedBoolean As List(Of Tuple(Of Integer, Boolean, String))
    Public Shared ReadOnly Property ActiveSuspendedBoolean As List(Of Tuple(Of Integer, Boolean, String))
        Get

            If _activeSuspendedBoolean Is Nothing Then
                _activeSuspendedBoolean = New List(Of Tuple(Of Integer, Boolean, String))
                _activeSuspendedBoolean.Add(New Tuple(Of Integer, Boolean, String)(Eresources.Activo, True, obtenerRecurso(Eresources.Activo)))
                _activeSuspendedBoolean.Add(New Tuple(Of Integer, Boolean, String)(Eresources.Suspendido, False, obtenerRecurso(Eresources.Suspendido)))
            End If

            Return _activeSuspendedBoolean
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que contiene lista de los tipos de retencion en la forma de una tupla(enum, idBd, descripcion)
    ''' </summary>
    Private Shared _retentionType As List(Of Tuple(Of Integer, Integer, String))
    Public Shared ReadOnly Property RetentionType As List(Of Tuple(Of Integer, Integer, String))
        Get

            If _retentionType Is Nothing Then
                '_retentionType = New List(Of Tuple(Of Integer, Integer, String))()
                '_retentionType.Add(New Tuple(Of Integer, Integer, String)(Eresources.Ninguno, 0, obtenerRecurso(Eresources.Ninguno)))
                '_retentionType.Add(New Tuple(Of Integer, Integer, String)(Eresources.ExentoRetencion, 1, obtenerRecurso(Eresources.ExentoRetencion, Empleado)))
                '_retentionType.Add(New Tuple(Of Integer, Integer, String)(Eresources.HaceRetencion, 2, obtenerRecurso(Eresources.HaceRetencion, Empleado)))
                '_retentionType.Add(New Tuple(Of Integer, Integer, String)(Eresources.Autoretenedor, 3, obtenerRecurso(Eresources.Autoretenedor, Empleado)))

                _retentionType = New List(Of Tuple(Of Integer, Integer, String))()
                _retentionType.Add(New Tuple(Of Integer, Integer, String)(1, 1, "Procedimiento 1"))
                _retentionType.Add(New Tuple(Of Integer, Integer, String)(2, 2, "Procedimiento 2"))
            End If

            Return _retentionType

        End Get
    End Property

    ''' <summary>
    ''' Propiedad que contiene lista de los tipos de educacion en la forma de una tupla(enum, idBd, descripcion)
    ''' </summary>
    Private Shared _isFormal As List(Of Tuple(Of Integer, Integer, String))
    Public Shared ReadOnly Property IsFormal As List(Of Tuple(Of Integer, Integer, String))
        Get

            If _isFormal Is Nothing Then
                _isFormal = New List(Of Tuple(Of Integer, Integer, String))()
                _isFormal.Add(New Tuple(Of Integer, Integer, String)(Eresources.Formal, 1, obtenerRecurso(Eresources.Formal, Empleado)))
                _isFormal.Add(New Tuple(Of Integer, Integer, String)(Eresources.NoFormal, 0, obtenerRecurso(Eresources.NoFormal, Empleado)))
            End If

            Return _isFormal

        End Get
    End Property

    ''' <summary>
    ''' Propiedad que contiene los tipos de salario en la forma de una tupla(enum, idBd, descripcion)
    ''' </summary>
    Private Shared _salaryType As List(Of Tuple(Of Integer, Integer, String))
    Public Shared ReadOnly Property SalaryType As List(Of Tuple(Of Integer, Integer, String))
        Get

            If _salaryType Is Nothing Then
                _salaryType = New List(Of Tuple(Of Integer, Integer, String))
                _salaryType.Add(New Tuple(Of Integer, Integer, String)(Eresources.Fijo, 1, obtenerRecurso(Eresources.Fijo, Contrato)))
                _salaryType.Add(New Tuple(Of Integer, Integer, String)(Eresources.Integral, 2, obtenerRecurso(Eresources.Integral, Contrato)))
                _salaryType.Add(New Tuple(Of Integer, Integer, String)(Eresources.Variable, 3, obtenerRecurso(Eresources.Variable, Contrato)))
            End If

            Return _salaryType
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que contiene los tipos de periodos de pago en la forma de una tupla(enum, idBd, descripcion)
    ''' </summary>
    Private Shared _paymentPeriod As List(Of Tuple(Of Integer, Integer, String))
    Public Shared ReadOnly Property PaymentPeriod As List(Of Tuple(Of Integer, Integer, String))
        Get

            If _paymentPeriod Is Nothing Then
                _paymentPeriod = New List(Of Tuple(Of Integer, Integer, String))
                _paymentPeriod.Add(New Tuple(Of Integer, Integer, String)(Eresources.Mensual, 1, obtenerRecurso(Eresources.Mensual, Contrato)))
                _paymentPeriod.Add(New Tuple(Of Integer, Integer, String)(Eresources.Quincenal, 2, obtenerRecurso(Eresources.Quincenal, Contrato)))
            End If

            Return _paymentPeriod
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que contiene los tipos de pago en la forma de una tupla(enum, idBd, descripcion)
    ''' </summary>
    Private Shared _paymentType As List(Of Tuple(Of Integer, Integer, String))
    Public Shared ReadOnly Property PaymentType As List(Of Tuple(Of Integer, Integer, String))
        Get

            If _paymentType Is Nothing Then
                _paymentType = New List(Of Tuple(Of Integer, Integer, String))
                _paymentType.Add(New Tuple(Of Integer, Integer, String)(Eresources.Cheque, 1, obtenerRecurso(Eresources.Cheque, Contrato)))
                _paymentType.Add(New Tuple(Of Integer, Integer, String)(Eresources.Efectivo, 2, obtenerRecurso(Eresources.Efectivo, Contrato)))
                _paymentType.Add(New Tuple(Of Integer, Integer, String)(Eresources.Consignacion, 3, obtenerRecurso(Eresources.Consignacion, Contrato)))
                _paymentType.Add(New Tuple(Of Integer, Integer, String)(Eresources.Desprendible, 4, obtenerRecurso(Eresources.Desprendible, Contrato)))
            End If

            Return _paymentType
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que contiene los tipos de cuentas bancarias en la forma de una tupla(enum, idBd, descripcion)
    ''' </summary>
    Private Shared _bankAccountType As List(Of Tuple(Of Integer, Integer, String))
    Public Shared ReadOnly Property BankAccountType As List(Of Tuple(Of Integer, Integer, String))
        Get

            If _bankAccountType Is Nothing Then
                _bankAccountType = New List(Of Tuple(Of Integer, Integer, String))
                _bankAccountType.Add(New Tuple(Of Integer, Integer, String)(Eresources.Ahorros, 1, obtenerRecurso(Eresources.Ahorros, Contrato)))
                _bankAccountType.Add(New Tuple(Of Integer, Integer, String)(Eresources.Corriente, 2, obtenerRecurso(Eresources.Corriente, Contrato)))
            End If

            Return _bankAccountType
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que contiene los tipos de empleado en la forma de una tupla(enum, idBd, descripcion)
    ''' </summary>
    Private Shared _employeeType As List(Of Tuple(Of Integer, Integer, String))
    Public Shared ReadOnly Property EmployeeType As List(Of Tuple(Of Integer, Integer, String))
        Get

            If _employeeType Is Nothing Then
                _employeeType = New List(Of Tuple(Of Integer, Integer, String))
                _employeeType.Add(New Tuple(Of Integer, Integer, String)(Eresources.Administrativo, 1, obtenerRecurso(Eresources.Administrativo, Contrato)))
                _employeeType.Add(New Tuple(Of Integer, Integer, String)(Eresources.Asistencial, 2, obtenerRecurso(Eresources.Asistencial, Contrato)))
            End If

            Return _employeeType
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que contiene las unidades de tiempo de duracion de un contrato en la forma de una tupla(enum, idBd, descripcion)
    ''' </summary>
    Private Shared _contractValidLengths As List(Of Tuple(Of Integer, Integer, String))
    Public Shared ReadOnly Property ContractValidLengths As List(Of Tuple(Of Integer, Integer, String))
        Get

            If _contractValidLengths Is Nothing Then
                _contractValidLengths = New List(Of Tuple(Of Integer, Integer, String))
                _contractValidLengths.Add(New Tuple(Of Integer, Integer, String)(Eresources.Ano, 3, obtenerRecurso(Eresources.Ano, Contrato)))
                _contractValidLengths.Add(New Tuple(Of Integer, Integer, String)(Eresources.Mes, 2, obtenerRecurso(Eresources.Mes, Contrato)))
                _contractValidLengths.Add(New Tuple(Of Integer, Integer, String)(Eresources.Dia, 1, obtenerRecurso(Eresources.Dia, Contrato)))
            End If

            Return _contractValidLengths
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que contiene las clases de contrato en la forma de una tupla(enum, idBd, descripcion)
    ''' </summary>
    Private Shared _contractClasses As List(Of Tuple(Of Integer, Byte, String))
    Public Shared ReadOnly Property ContractClasses As List(Of Tuple(Of Integer, Byte, String))
        Get

            If _contractClasses Is Nothing Then
                _contractClasses = New List(Of Tuple(Of Integer, Byte, String))
                _contractClasses.Add(New Tuple(Of Integer, Byte, String)(Eresources.ClaseContratoOtros, 1, obtenerRecurso(Eresources.ClaseContratoOtros, Contrato)))
                _contractClasses.Add(New Tuple(Of Integer, Byte, String)(Eresources.ClaseContratoAprendizaje, 2, obtenerRecurso(Eresources.ClaseContratoAprendizaje, Contrato)))
                _contractClasses.Add(New Tuple(Of Integer, Byte, String)(Eresources.ClaseContratoLaboralFijo, 3, obtenerRecurso(Eresources.ClaseContratoLaboralFijo, Contrato)))
                _contractClasses.Add(New Tuple(Of Integer, Byte, String)(Eresources.ClaseContratoLaboralIndefinido, 4, obtenerRecurso(Eresources.ClaseContratoLaboralIndefinido, Contrato)))
                _contractClasses.Add(New Tuple(Of Integer, Byte, String)(Eresources.ClaseContratoAprendizajePractica, 5, obtenerRecurso(Eresources.ClaseContratoAprendizajePractica, Contrato)))
                _contractClasses.Add(New Tuple(Of Integer, Byte, String)(Eresources.ClaseContratoPracticasoPasantiasUniversitarias, 6, obtenerRecurso(Eresources.ClaseContratoPracticasoPasantiasUniversitarias, Contrato)))
            End If

            Return _contractClasses
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que contiene los tipos de fondos en la forma de una tupla(enum, idBd, descripcion)
    ''' </summary>
    Private Shared _fundsTypes As List(Of Tuple(Of Integer, Byte, String))
    Public Shared ReadOnly Property FundsTypes As List(Of Tuple(Of Integer, Byte, String))
        Get

            If _fundsTypes Is Nothing Then
                _fundsTypes = New List(Of Tuple(Of Integer, Byte, String))
                _fundsTypes.Add(New Tuple(Of Integer, Byte, String)(Eresources.Salud, 1, obtenerRecurso(Eresources.Salud, Contrato)))
                _fundsTypes.Add(New Tuple(Of Integer, Byte, String)(Eresources.Pension, 2, obtenerRecurso(Eresources.Pension, Contrato)))
                _fundsTypes.Add(New Tuple(Of Integer, Byte, String)(Eresources.Cesantias, 3, obtenerRecurso(Eresources.Cesantias, Contrato)))
                _fundsTypes.Add(New Tuple(Of Integer, Byte, String)(Eresources.Riesgos, 4, obtenerRecurso(Eresources.Riesgos, Contrato)))
                _fundsTypes.Add(New Tuple(Of Integer, Byte, String)(Eresources.CajaCompensacion, 5, obtenerRecurso(Eresources.CajaCompensacion, Contrato)))
            End If

            Return _fundsTypes
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que contiene lista de los grupos sanguineos en la forma de una tupla(enum, idBd, descripcion)
    ''' </summary>
    Private Shared _DeclarantType As List(Of Tuple(Of Byte, String))
    Public Shared ReadOnly Property DeclarantType As List(Of Tuple(Of Byte, String))
        Get

            If _DeclarantType Is Nothing Then
                _DeclarantType = New List(Of Tuple(Of Byte, String))()
                _DeclarantType.Add(New Tuple(Of Byte, String)(1, "No Declarante"))
                _DeclarantType.Add(New Tuple(Of Byte, String)(2, "Declarante"))
            End If

            Return _DeclarantType
        End Get
    End Property


    ''' <summary>
    ''' Propiedad que contiene lista de los tipos de vivienda en la forma de una tupla(idBd, descripcion)
    ''' </summary>
    Private Shared _HousingType As List(Of Tuple(Of Byte, String))
    Public Shared ReadOnly Property HousingType As List(Of Tuple(Of Byte, String))
        Get

            If _HousingType Is Nothing Then
                _HousingType = New List(Of Tuple(Of Byte, String))()
                _HousingType.Add(New Tuple(Of Byte, String)(1, "Propia"))
                _HousingType.Add(New Tuple(Of Byte, String)(2, "Arriendo"))
                _HousingType.Add(New Tuple(Of Byte, String)(3, "Familiar"))
            End If

            Return _HousingType
        End Get
    End Property


    ''' <summary>
    ''' Propiedad que contiene lista de los posibles estratos socioeconomicos en la forma de una tupla(idBd, descripcion)
    ''' </summary>
    Private Shared _SocioEconomicStatus As List(Of Tuple(Of Byte, Integer, String))
    Public Shared ReadOnly Property SocioEconomicStatus As List(Of Tuple(Of Byte, Integer, String))
        Get

            If _SocioEconomicStatus Is Nothing Then
                _SocioEconomicStatus = New List(Of Tuple(Of Byte, Integer, String))()
                _SocioEconomicStatus.Add(New Tuple(Of Byte, Integer, String)(1, 1, "Bajo-bajo"))
                _SocioEconomicStatus.Add(New Tuple(Of Byte, Integer, String)(2, 2, "Bajo"))
                _SocioEconomicStatus.Add(New Tuple(Of Byte, Integer, String)(3, 3, "Medio-bajo"))
                _SocioEconomicStatus.Add(New Tuple(Of Byte, Integer, String)(4, 4, "Medio"))
                _SocioEconomicStatus.Add(New Tuple(Of Byte, Integer, String)(5, 5, "Medio-alto"))
                _SocioEconomicStatus.Add(New Tuple(Of Byte, Integer, String)(6, 6, "Alto"))
            End If

            Return _SocioEconomicStatus
        End Get
    End Property

#End Region

#Region "Methods"

    ''' <summary>
    ''' Calcula la fecha final del contrato basado en la duracion y unidad de tiempo seleccionada
    ''' </summary>
    ''' <param name="initialDate">Fecha Inicial del contrato</param>
    ''' <param name="length">Duracion del contrato numerica</param>
    ''' <param name="timeUnit">Unidad de tiempo</param>
    ''' <returns>Fecha final del contrato</returns>
    Public Shared Function CalculateContractEndingDate(initialDate As Date, length As Integer, timeUnit As Integer) As Date
        Dim finalDate As Date
        Dim timeUnitResources As Eresources = ContractValidLengths.Where(Function(i) i.Item2 = timeUnit).FirstOrDefault.Item1
        Select Case timeUnitResources
            'Años
            Case Eresources.Ano
                finalDate = initialDate.AddYears(length).AddDays(-1)
                'Meses
            Case Eresources.Mes
                finalDate = initialDate.AddMonths(length).AddDays(-1)
                'Dias
            Case Eresources.Dia
                finalDate = initialDate.AddDays(length).AddDays(-1)
            Case Else
                finalDate = Nothing
        End Select

        Return finalDate
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="initialDate"></param>
    ''' <param name="years"></param>
    ''' <param name="months"></param>
    ''' <param name="days"></param>
    ''' <returns></returns>
    Public Shared Function CalculateContractEndingDate(initialDate As Date, years As Integer, months As Integer, days As Integer) As Date
        Dim r As Date
        r = initialDate.AddYears(years).AddMonths(months).AddDays(days)
        Return r
    End Function

    ''' <summary>
    ''' Clona los valores basicos del contrato dado como parametro a un nuevo contrato
    ''' </summary>
    Public Shared Function CloneContract(c As Contract) As Contract
        Dim nc As New Contract

        With nc
            'Valores predefinidos
            .RowType = 2
            '------------------------------------------

            .BankId = c.BankId
            .BankAccountNumber = c.BankAccountNumber
            .BankAccountType = c.BankAccountType
            .BaseIncome = c.BaseIncome
            .BasicSalary = c.BasicSalary
            .ContractCreationDate = c.ContractCreationDate
            .ContractEndingDate = c.ContractEndingDate
            .ContractInitialDate = c.ContractInitialDate
            .ContractType = c.ContractType
            .ContractTypeId = c.ContractTypeId
            .FunctionalUnit = c.FunctionalUnit
            .FunctionalUnitId = c.FunctionalUnitId
            .Group = c.Group
            .GroupId = c.GroupId
            .HoursDaily = c.HoursDaily
            .IncomeDailyBase = c.IncomeDailyBase
            .JobBondingDate = c.JobBondingDate
            .LiquidationPayroll = c.LiquidationPayroll
            .PaymentPeriod = c.PaymentPeriod
            .PaymentType = c.PaymentType
            .Position = c.Position
            .PositionId = c.PositionId
            .RetirementDate = c.RetirementDate
            .RetirementReasonId = c.RetirementReasonId
            .Status = c.Status
            .TrialPeriod = c.TrialPeriod
            .TrialPeriodSalaryPercentage = c.TrialPeriodSalaryPercentage
            .TrialPeriodTime = c.TrialPeriodTime
            .TypeOfPensionContribution = c.TypeOfPensionContribution
            .Valid = c.Valid
            .Employee = c.Employee

        End With

        Return nc
    End Function

    ''' <summary>
    ''' Convierte la fila seleccionada de una rejilla a el tipo de entidad enviada a la funcion
    ''' </summary>
    ''' <typeparam name="T">Tipo de la entidad</typeparam>
    ''' <param name="gridview">Rejilla donde se encuentra el dato</param>
    ''' <returns>La entidad xpo convertida</returns>
    Public Shared Function ConvertFromSelectedRowInGridViewToXpoEntity(Of T As XPLiteObject)(ByVal gridview As GridView, Optional rowIndex As Integer = 0) As T
        Dim selectedRow = If(rowIndex = 0, CType(gridview, GridView).FocusedRowHandle(), rowIndex)
        Dim selectedObject = CType(gridview.GetRow(selectedRow), ReadonlyThreadSafeProxyForObjectFromAnotherThread)

        If selectedObject IsNot Nothing Then
            Dim entitySelected As T = CType(selectedObject.OriginalRow, T)
            Return entitySelected
        Else
            Return Nothing
        End If

    End Function

#End Region

End Class


Public Structure DateTimeSpan


    Private _years As Integer
    Public ReadOnly Property Years() As Integer
        Get
            Return _years
        End Get
    End Property

    Private _months As Integer
    Public ReadOnly Property Months() As Integer
        Get
            Return _months
        End Get
    End Property

    Private _days As Integer
    Public ReadOnly Property Days() As Integer
        Get
            Return _days
        End Get
    End Property

    Private _hours As Integer
    Public ReadOnly Property Hours() As Integer
        Get
            Return _hours
        End Get
    End Property

    Private _minutes As Integer
    Public ReadOnly Property Minutes() As Integer
        Get
            Return _minutes
        End Get
    End Property

    Private _seconds As Integer
    Public ReadOnly Property Seconds() As Integer
        Get
            Return _seconds
        End Get
    End Property

    Private _milliseconds As Integer
    Public ReadOnly Property Milliseconds() As Integer
        Get
            Return _milliseconds
        End Get
    End Property

    Public Sub New(years As Integer, months As Integer, days As Integer, hours As Integer, minutes As Integer, seconds As Integer, milliseconds As Integer)

        Me._years = years
        Me._months = months
        Me._days = days
        Me._hours = hours
        Me._minutes = minutes
        Me._seconds = seconds
        Me._milliseconds = milliseconds

    End Sub

    Enum Phase
        Years
        Months
        Days
        Done
    End Enum

    Public Shared Function CompareDates(date1 As DateTime, date2 As DateTime) As DateTimeSpan

        If (date2 < date1) Then
            Dim _sub = date1
            date1 = date2
            date2 = _sub
        End If

        Dim current As DateTime = date1
        Dim years As Integer = 0
        Dim months As Integer = 0
        Dim days As Integer = 0

        Dim Phase As Phase = Phase.Years
        Dim span As DateTimeSpan = New DateTimeSpan()

        If date2.Year = 9999 Then
            Dim TimeSpan As TimeSpan = date2 - current
            years = (date2.Year - current.Year)
            months = (date2.Month - current.Month)
            days = (date2.Day - current.Day)
            span = New DateTimeSpan(years, months, days, TimeSpan.Hours, TimeSpan.Minutes, TimeSpan.Seconds, TimeSpan.Milliseconds)
        Else
            While (Phase <> Phase.Done)

                Select Case Phase
                    Case Phase.Years
                        If (current.AddYears(years + 1) > date2) Then

                            Phase = Phase.Months
                            current = current.AddYears(years)

                        Else

                            years += 1
                        End If
                    'break()
                    Case Phase.Months
                        If (current.AddMonths(months + 1) > date2) Then

                            Phase = Phase.Days
                            current = current.AddMonths(months)

                        Else

                            months += 1
                        End If
                    'break()
                    Case Phase.Days
                        If (current.AddDays(days + 1) > date2) Then
                            current = current.AddDays(days)
                            Dim TimeSpan As TimeSpan = date2 - current
                            span = New DateTimeSpan(years, months, days, TimeSpan.Hours, TimeSpan.Minutes, TimeSpan.Seconds, TimeSpan.Milliseconds)
                            Phase = Phase.Done
                        Else
                            days += 1
                        End If
                        'break()
                End Select
            End While
        End If

        Return span

    End Function

End Structure