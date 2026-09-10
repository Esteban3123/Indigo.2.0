'***********************************************************************
' Assembly         : Presentation.Billing.MVP
' Author           : Andres Alarcon
' Created          : 21-11-2025
'
' Last Modified By : 
' Last Modified On : 
' Description      : Modelo para Registro Soporte RIPS
'
'***********************************************************************

#Region "Imports"
Imports DevExpress.Xpo
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.ContractRepository
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Presentation.Billing.MVP
Imports Presentation.CloudAgent
#End Region

''' <summary>
''' Modelo de datos para el formulario de Registro Soporte RIPS.
''' Proporciona acceso a servicios de datos para unidades funcionales, tipos de estancia,
''' profesionales de salud, diagnósticos, CUPS y operaciones CRUD de registros RIPS.
''' </summary>
''' <remarks>
''' Este modelo sigue el patrón MVP y actúa como intermediario entre la vista y los servicios de datos.
''' Implementa IDisposable para liberar recursos de conexión.
''' </remarks>
Public Class MRIPSSupportRecord
    Implements IDisposable

#Region "Campos Privados"

    ''' <summary>Referencia a los valores de sesión del usuario.</summary>
    Private _indigoSessionValues As SessionValues

    ''' <summary>Identificador del formulario para auditoría.</summary>
    Private _tagForm As String

#End Region

#Region "Constructor"

    ''' <summary>
    ''' Inicializa una nueva instancia del modelo MRIPSSupportRecord.
    ''' </summary>
    ''' <param name="tag">Identificador del formulario para auditoría.</param>
    Public Sub New(tag As String)
        _indigoSessionValues = SessionValues.Instance
        _indigoSessionValues.AuditMessageWcf.Functional = tag
    End Sub

#End Region

#Region "Métodos de Unidades Funcionales"

    ''' <summary>
    ''' Obtiene las unidades funcionales hospitalarias autorizadas para el usuario.
    ''' </summary>
    ''' <returns>Fuente de datos con las unidades funcionales.</returns>
    Public Function GetHospitalFunctionalUnits() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).PayrollService.ListFunctionalUnitUserAuthorized(_indigoSessionValues.UserIndigo)
    End Function

    ''' <summary>
    ''' Obtiene una unidad funcional por su Id.
    ''' </summary>
    ''' <param name="functionalUnitId">Identificador de la unidad funcional.</param>
    ''' <returns>Entidad PayrollFunctionalUnit o Nothing si no existe.</returns>
    Public Function GetFunctionalUnitById(functionalUnitId As Integer) As PayrollFunctionalUnit
        Dim filter As String = $"Id = {functionalUnitId}"
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).PayrollService.GetXPOObject(Of PayrollFunctionalUnit)(filter)
    End Function

#End Region

#Region "Métodos de Tipos de Estancia"

    ''' <summary>
    ''' Obtiene los tipos de estancia activos filtrados por unidad funcional.
    ''' </summary>
    ''' <param name="FunctionalUnitCode">Código de la unidad funcional.</param>
    ''' <returns>Fuente de datos con los tipos de estancia.</returns>
    Public Function GetStayTypesByFunctionalUnit(FunctionalUnitCode As String) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).CrystalService.GetAllStayTypeActiveByFunctionalUnit(FunctionalUnitCode)
    End Function

    ''' <summary>
    ''' Obtiene el tipo de estancia por su código.
    ''' </summary>
    ''' <param name="stayTypeCode">Código del tipo de estancia (CODTIPEST).</param>
    ''' <returns>Entidad CHTIPESTA o Nothing si no existe.</returns>
    Public Function GetStayTypeByCode(stayTypeCode As String) As Object
        Dim filter As String = $"CODTIPEST = '{stayTypeCode}'"
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).CrystalService.GetXPOObject(Of CHTIPESTA)(filter)
    End Function

#End Region

#Region "Métodos de CUPS"

    ''' <summary>
    ''' Obtiene una entidad CUPS por su Id.
    ''' </summary>
    ''' <param name="CupsEntityId">Identificador del CUPS.</param>
    ''' <returns>Entidad CupsEntityXpo o Nothing si no existe.</returns>
    Public Function GetCupsEntityById(CupsEntityId As Integer) As CupsEntityXpo
        Dim filter As String = $"Id = {CupsEntityId}"
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).ContractService.GetXPOObject(Of CupsEntityXpo)(filter)
    End Function

    ''' <summary>
    ''' Obtiene la descripción de contrato de CUPS por su Id.
    ''' </summary>
    ''' <param name="cupsEntityContractDescriptionId">Identificador de la descripción.</param>
    ''' <returns>Entidad CUPSEntityContractDescriptionsXpo o Nothing si no existe.</returns>
    Public Function GetCUPSEntityContractDescriptionById(cupsEntityContractDescriptionId As Integer) As CUPSEntityContractDescriptionsXpo
        Dim filter As String = $"Id = {cupsEntityContractDescriptionId}"
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).ContractService.GetXPOObject(Of CUPSEntityContractDescriptionsXpo)(filter)
    End Function

    ''' <summary>
    ''' Obtiene la información de CUPS a partir de un tipo de estancia.
    ''' Navega: StayType → Tarifa (CHGENTARI) → CupsId/DescriptionId → CupsEntity
    ''' </summary>
    ''' <param name="stayTypeCode">Código del tipo de estancia.</param>
    ''' <returns>Tupla con CupsEntity y DescriptionId de la tarifa asociada.</returns>
    ''' <remarks>
    ''' El DescriptionId se obtiene según el tipo de liquidación (TIPLIQEST):
    ''' - Caso 1: usa GENCUPS y IDDESCRIPCIONRELACIONADA_CUPS
    ''' - Caso 2,3: usa GENCUPS2 y IDDESCRIPCIONRELACIONADA_CUPS2
    ''' </remarks>
    Public Function GetCupsInfoFromStayType(stayTypeCode As String) As (CupsEntity As CupsEntityXpo, DescriptionId As Integer?)
        Dim stayType = GetStayTypeByCode(stayTypeCode)
        If stayType Is Nothing Then Return (Nothing, Nothing)

        Dim tariff = stayType.CHGENTARIs(0)
        If tariff Is Nothing Then Return (Nothing, Nothing)

        Dim cupsId As Integer? = Nothing
        Dim descriptionId As Integer? = Nothing

        Select Case tariff.TIPLIQEST
            Case 1
                cupsId = tariff.GENCUPS
                descriptionId = tariff.IDDESCRIPCIONRELACIONADA_CUPS
            Case 2, 3
                cupsId = tariff.GENCUPS2
                descriptionId = tariff.IDDESCRIPCIONRELACIONADA_CUPS2
        End Select

        If Not cupsId.HasValue OrElse cupsId.Value = 0 Then Return (Nothing, Nothing)

        Dim cupsEntity = GetCupsEntityById(cupsId.Value)
        Return (cupsEntity, descriptionId)
    End Function

#End Region

#Region "Métodos de Profesionales de Salud"

    ''' <summary>
    ''' Obtiene un profesional de salud a partir del Id del tercero.
    ''' </summary>
    ''' <param name="thirdPartyId">Identificador del tercero (ThirdParty).</param>
    ''' <returns>Entidad HealthCareProfessionalXpo o Nothing si no existe.</returns>
    ''' <remarks>
    ''' Primero obtiene el ThirdParty para conseguir el NIT,
    ''' luego busca el profesional por CODIGONIT.
    ''' </remarks>
    Public Function GetHealthProfessionalByThirdPartyId(thirdPartyId As Integer) As HealthCareProfessionalXpo
        Dim thirdPartyFilter As String = $"Id = {thirdPartyId}"
        Dim thirdParty = XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).BillingService.GetXPOObject(Of BillingRepository.ThirdPartyXpo)(thirdPartyFilter)

        If thirdParty Is Nothing OrElse String.IsNullOrEmpty(thirdParty.Nit) Then
            Return Nothing
        End If

        Dim professionalFilter As String = $"CODIGONIT = '{thirdParty.Nit}'"
        Return XpoServiceEx.Instance(_indigoSessionValues.HisContainer).CrystalService.GetXPOObject(Of HealthCareProfessionalXpo)(professionalFilter)
    End Function

    ''' <summary>
    ''' Obtiene la lista de profesionales de salud activos.
    ''' </summary>
    ''' <returns>Fuente de datos con los profesionales.</returns>
    Public Function GetHealthProfessionals() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.HisContainer).CrystalService.ListHealthCareProfessional()
    End Function

    ''' <summary>
    ''' Obtiene la lista de especialidades médicas activas.
    ''' </summary>
    ''' <returns>Fuente de datos con las especialidades.</returns>
    Public Function GetSpecialties() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me._indigoSessionValues.HisContainer).CrystalService.ListSpecialties(True)
    End Function

#End Region

#Region "Métodos de Diagnósticos"

    ''' <summary>
    ''' Obtiene la lista de diagnósticos principales activos.
    ''' </summary>
    ''' <returns>Fuente de datos con los diagnósticos.</returns>
    Public Function GetPrincipalDiagnoses() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me._indigoSessionValues.HisContainer).CrystalService.ListAllINDIAGNOS()
    End Function

    ''' <summary>
    ''' Obtiene la lista de diagnósticos relacionados activos.
    ''' </summary>
    ''' <returns>Fuente de datos con los diagnósticos.</returns>
    ''' <remarks>Utiliza la misma fuente que diagnósticos principales.</remarks>
    Public Function GetRelatedDiagnoses() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me._indigoSessionValues.HisContainer).CrystalService.ListAllINDIAGNOS()
    End Function

    ''' <summary>
    ''' Obtiene la lista de diagnósticos de causa de muerte activos.
    ''' </summary>
    ''' <returns>Fuente de datos con los diagnósticos.</returns>
    ''' <remarks>Utiliza la misma fuente que diagnósticos principales.</remarks>
    Public Function GetDeathCauseDiagnoses() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me._indigoSessionValues.HisContainer).CrystalService.ListAllINDIAGNOS()
    End Function

#End Region

#Region "Métodos de Cálculo"

    ''' <summary>
    ''' Calcula la estancia unitaria para un rango de fechas.
    ''' </summary>
    ''' <param name="admissionCode">Código del ingreso.</param>
    ''' <param name="startDate">Fecha inicial del rango.</param>
    ''' <param name="endDate">Fecha final del rango.</param>
    ''' <returns>Objeto UnitStay con los días calculados.</returns>
    Public Function CalculateUnitStayForRange(admissionCode As String, startDate As Date, endDate As Date) As UnitStay
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCrystal.CalculateUnitStayForRange(admissionCode, startDate, endDate)
    End Function

#End Region

#Region "Métodos CRUD de Registro RIPS"

    ''' <summary>
    ''' Obtiene un registro de soporte RIPS por su código.
    ''' </summary>
    ''' <param name="code">Código del registro RIPS.</param>
    ''' <returns>Entidad RIPSSupportRecord o Nothing si no existe.</returns>
    Public Async Function GetRIPSSupportRecordByCode(code As String) As Task(Of RIPSSupportRecord)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetRIPSSupportRecordByCodeAsync(code)
    End Function

    ''' <summary>
    ''' Guarda o actualiza un registro de soporte RIPS.
    ''' </summary>
    ''' <param name="supportRecord">Entidad RIPSSupportRecord a guardar.</param>
    ''' <param name="operativeUnitId">Id de la unidad operativa.</param>
    ''' <param name="audit">Mensaje de auditoría.</param>
    ''' <returns>ActionResult con el registro guardado o mensaje de error.</returns>
    ''' <remarks>
    ''' El servicio determina si es INSERT o UPDATE basándose en el ChangeTracker.State:
    ''' - ObjectState.Added: INSERT
    ''' - ObjectState.Modified: UPDATE
    ''' </remarks>
    Public Async Function SaveRIPSSupportRecord(supportRecord As RIPSSupportRecord, operativeUnitId As Integer, audit As AuditMessage, Optional idCurrentSequense As Long = 0) As Task(Of ActionResult(Of RIPSSupportRecord))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.NewRIPSSupportRecordAsync(supportRecord, operativeUnitId, audit, idCurrentSequense)
    End Function

    ''' <summary>
    ''' Confirma un registro de soporte RIPS.
    ''' </summary>
    ''' <param name="supportRecordId">Identificador del registro RIPS.</param>
    ''' <param name="audit">Mensaje de auditoría.</param>
    ''' <returns>ActionResult con el registro confirmado o mensaje de error.</returns>
    Public Async Function ConfirmRIPSSupportRecord(supportRecordId As Integer, audit As AuditMessage) As Task(Of ActionResult(Of RIPSSupportRecord))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.ConfirmRIPSSupportRecordAsync(supportRecordId, audit)
    End Function


#End Region

#Region "IDisposable Support"

    Private disposedValue As Boolean

    ''' <summary>
    ''' Libera los recursos utilizados por el modelo.
    ''' </summary>
    ''' <param name="disposing">True si se llama desde Dispose(); False si se llama desde el finalizador.</param>
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' Liberar recursos administrados si es necesario
            End If
        End If
        Me.disposedValue = True
    End Sub

    ''' <summary>
    ''' Libera los recursos utilizados por el modelo.
    ''' </summary>
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

#End Region

End Class
