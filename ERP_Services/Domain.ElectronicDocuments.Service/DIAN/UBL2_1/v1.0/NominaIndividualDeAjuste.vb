Imports Domain.Base.Entities.Enums.ElectronicDocuments.v1_6
Imports Domain.ElectronicDocuments.Entities.UBL2_1.common
Imports Domain.ElectronicDocuments.Entities.UBL2_1.maindoc
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Root

Namespace DIAN.UBL2_1.v1_0

    Public Class NominaIndividualDeAjuste

#Region "Properties"

        ReadOnly _settingsAccount As GeneralLedgerSettings
        ReadOnly _billingAuthorization As BillingAuthorization
        ReadOnly _supplierThirdParty As ThirdParty
        ReadOnly _employeeThirdParty As ThirdParty
        ReadOnly _electronicPayroll As Domain.Payroll.Entities.ElectronicPayroll

#End Region

#Region "Buldier"

        Public Sub New(ByVal settingsAccount As GeneralLedgerSettings, ByVal supplierThirdParty As ThirdParty, ByVal employeeThirdParty As ThirdParty, ByVal electronicPayroll As Domain.Payroll.Entities.ElectronicPayroll)
            Me._settingsAccount = settingsAccount
            Me._supplierThirdParty = supplierThirdParty
            Me._employeeThirdParty = employeeThirdParty
            Me._electronicPayroll = electronicPayroll

            AmountType.TlsDefaultCurrencyID = CurrencyCode.COP
        End Sub

#End Region

#Region "Methods"

        Public Function Populate() As NominaIndividualDeAjusteType
            Dim documentType As New NominaIndividualDeAjusteType
            documentType.UBLExtensions = GenerateUBLExtensions()
            documentType.TipoNota = "1"
            documentType.Reemplazar = New NominaIndividualDeAjusteTypeReemplazar With
            {
                .ReemplazandoPredecesor = GetReemplazandoPredecesorInformation(),
                .Periodo = GetPeriodoInformation(),
                .NumeroSecuenciaXML = GetNumeroSecuenciaXMLInformation(),
                .LugarGeneracionXML = GetLugarGeneracionXMLInformation(),
                .ProveedorXML = GetProveedorXMLInformation(),
                .CodigoQR = Me._electronicPayroll.GetQRCode(),
                .InformacionGeneral = GetInformacionGeneralInformation(),
                .Notas = New List(Of String) From {Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.MoreInformation.MessageResult},
                .Empleador = GetEmpleadorInformation(),
                .Trabajador = GetTrabajadorInformation(),
                .Pago = GetPagoInformation(),
                .FechasPagos = New List(Of String) From {Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.MoreInformation.PaymentDate.Value.ToString("yyyy-MM-dd")},
                .Devengados = GetDevengadosInformation(),
                .Deducciones = GetDeduccionesInformation(),
                .DevengadosTotal = Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.getAcrualValue(),
                .DeduccionesTotal = Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.getDeductionValue(),
                .ComprobanteTotal = Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.getTotalValue()
            }
            Return documentType
        End Function

#End Region

#Region "Private Methods"

        Private Function GenerateUBLExtensions() As List(Of UBLExtensionType)
            Dim listUBLExtensionType As New List(Of UBLExtensionType)
            listUBLExtensionType.Add(New UBLExtensionType With {.ExtensionContent = New ExtensionContentType})
            Return listUBLExtensionType
        End Function

        Private Function GetReemplazandoPredecesorInformation() As NominaIndividualDeAjusteTypeReemplazarReemplazandoPredecesor
            Return New NominaIndividualDeAjusteTypeReemplazarReemplazandoPredecesor With
            {
                .CUNEPred = Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.CUNE,
                .NumeroPred = Me._electronicPayroll.NoteAdjustmentElectronicPayroll.GetDocumentNumber(),
                .FechaGenPred = Me._electronicPayroll.NoteAdjustmentElectronicPayroll.CreationDate.ToString("yyyy-MM-dd")
            }
        End Function

        Private Function GetPeriodoInformation() As NominaIndividualDeAjusteTypeReemplazarPeriodo
            Return New NominaIndividualDeAjusteTypeReemplazarPeriodo With
            {
                .FechaIngreso = Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.MoreInformation.AdmissionDate.Value.ToString("yyyy-MM-dd"),
                .FechaRetiro = Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.MoreInformation.RetirementDate.Value.ToString("yyyy-MM-dd"),
                .FechaLiquidacionInicio = Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.MoreInformation.LiquidationDateStart.Value.ToString("yyyy-MM-dd"),
                .FechaLiquidacionFin = Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.MoreInformation.LiquidationDateEnd.Value.ToString("yyyy-MM-dd"),
                .TiempoLaborado = Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.MoreInformation.TimeWorked,
                .FechaGen = Me._electronicPayroll.CreationDate.ToString("yyyy-MM-dd")
            }
        End Function

        Private Function GetNumeroSecuenciaXMLInformation() As NominaIndividualDeAjusteTypeReemplazarNumeroSecuenciaXML
            Return New NominaIndividualDeAjusteTypeReemplazarNumeroSecuenciaXML With
            {
                .Prefijo = If(String.IsNullOrEmpty(Me._electronicPayroll.Prefix), Nothing, Me._electronicPayroll.Prefix),
                .Consecutivo = Me._electronicPayroll.DocumentNumber,
                .Numero = Me._electronicPayroll.GetDocumentNumber()
            }
        End Function

        Private Function GetLugarGeneracionXMLInformation() As NominaIndividualDeAjusteTypeReemplazarLugarGeneracionXML
            Dim address = Me._supplierThirdParty.Person.Address.First(Function(a) a.DepartmentId IsNot Nothing AndAlso a.CityId IsNot Nothing)
            Return New NominaIndividualDeAjusteTypeReemplazarLugarGeneracionXML With
            {
                .Pais = "CO",
                .DepartamentoEstado = address.DepartmentCode,
                .MunicipioCiudad = address.CityCode,
                .Idioma = "es"
            }
        End Function

        Private Function GetProveedorXMLInformation() As NominaIndividualDeAjusteTypeReemplazarProveedorXML
            Return New NominaIndividualDeAjusteTypeReemplazarProveedorXML With
            {
                .RazonSocial = Me._supplierThirdParty.Name,
                .NIT = Me._supplierThirdParty.Nit,
                .DV = Me._supplierThirdParty.DigitVerification,
                .SoftwareID = Me._settingsAccount.ElectronicPayrollIdentifier,
                .SoftwareSC = Utils.Sha384Encode(String.Concat(Me._settingsAccount.ElectronicPayrollIdentifier, Me._settingsAccount.ElectronicPayrollPin, Me._electronicPayroll.GetDocumentNumber()))
            }
        End Function

        Private Function GetInformacionGeneralInformation()
            Return New NominaIndividualDeAjusteTypeReemplazarInformacionGeneral With
            {
                .Version = "V1.0: Nota de Ajuste de Documento Soporte de Pago de Nómina Electrónica",
                .Ambiente = Utils.GetXmlEnumToString(Of Environment)(IIf(Me._settingsAccount.ElectronicPayrollEnvironment, Environment.Production, Environment.Tests)),
                .TipoXML = Me._electronicPayroll.TipoXML,
                .CUNE = Me._electronicPayroll.CUNE,
                .EncripCUNE = "CUNE-SHA384",
                .FechaGen = Me._electronicPayroll.CreationDate.ToString("yyyy-MM-dd"),
                .HoraGen = Me._electronicPayroll.CreationDate.ToString("HH:mm:ss-05:00"),
                .PeriodoNomina = Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.MoreInformation.PayrollPeriod,
                .TipoMoneda = Utils.GetXmlEnumToString(Of CurrencyCode)(AmountType.TlsDefaultCurrencyID)
            }
        End Function

        Private Function GetEmpleadorInformation() As NominaIndividualDeAjusteTypeReemplazarEmpleador
            Dim address = Me._supplierThirdParty.Person.Address.First(Function(a) a.DepartmentId IsNot Nothing AndAlso a.CityId IsNot Nothing)
            Return New NominaIndividualDeAjusteTypeReemplazarEmpleador With
            {
                .RazonSocial = Me._supplierThirdParty.Name,
                .NIT = Me._supplierThirdParty.Nit,
                .DV = Me._supplierThirdParty.DigitVerification,
                .Pais = "CO",
                .DepartamentoEstado = address.DepartmentCode,
                .MunicipioCiudad = address.CityCode,
                .Direccion = address.Addresss
            }
        End Function

        Private Function GetTrabajadorInformation() As NominaIndividualDeAjusteTypeReemplazarTrabajador
            Dim result As New NominaIndividualDeAjusteTypeReemplazarTrabajador
            result.TipoTrabajador = Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.MoreInformation.WorkerType
            result.SubTipoTrabajador = Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.MoreInformation.WorkerSubType
            result.AltoRiesgoPension = Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.MoreInformation.HighPensionRisk
            result.TipoDocumento = Me._employeeThirdParty.Person.getAcquirerType()
            result.NumeroDocumento = Me._employeeThirdParty.Nit
            result.PrimerApellido = Me._employeeThirdParty.Person.FirstLastName
            result.SegundoApellido = Me._employeeThirdParty.Person.SecondLastName
            result.PrimerNombre = Me._employeeThirdParty.Person.FirstName
            If Not String.IsNullOrEmpty(Me._employeeThirdParty.Person.SecondName) Then
                result.OtrosNombres = Me._employeeThirdParty.Person.SecondName
            End If
            result.LugarTrabajoPais = Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.MoreInformation.WorkplaceCountry
            result.LugarTrabajoDepartamentoEstado = Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.MoreInformation.WorkplaceDepartment
            result.LugarTrabajoMunicipioCiudad = Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.MoreInformation.WorkplaceCity
            result.LugarTrabajoDireccion = Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.MoreInformation.WorkplaceAddress
            result.SalarioIntegral = Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.MoreInformation.IntegralSalary
            result.TipoContrato = Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.MoreInformation.ContractType
            result.Sueldo = Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.MoreInformation.BasicSalary
            Return result
        End Function

        Private Function GetPagoInformation() As NominaIndividualDeAjusteTypeReemplazarPago
            Return New NominaIndividualDeAjusteTypeReemplazarPago With
            {
                .Forma = Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.MoreInformation.PaymentForm,
                .Metodo = Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.MoreInformation.PaymentMethod,
                .Banco = Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.MoreInformation.PaymentBank,
                .TipoCuenta = Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.MoreInformation.PaymentAccountType,
                .NumeroCuenta = Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.MoreInformation.PaymentAccountNumber
            }
        End Function

        Private Function GetDecimalInformationByTypes(types As Integer()) As NominaIndividualDeAjusteAmountType
            Dim result As NominaIndividualDeAjusteAmountType = Nothing
            If Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) types.Contains(d.Type)) Then
                result = Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) types.Contains(d.Type)).Sum(Function(d) d.Value)
            End If
            Return result
        End Function

        Private Function GetListDecimalInformationByTypes(types As Integer()) As List(Of NominaIndividualDeAjusteAmountType)
            Dim results As List(Of NominaIndividualDeAjusteAmountType) = Nothing
            If Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) types.Contains(d.Type)) Then
                results = New List(Of NominaIndividualDeAjusteAmountType)
                results.Add(Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) types.Contains(d.Type)).Sum(Function(d) d.Value))
            End If
            Return results
        End Function

#Region "Devengados"

        Private Function GetDevengadosInformation() As NominaIndividualDeAjusteTypeReemplazarDevengados
            Return New NominaIndividualDeAjusteTypeReemplazarDevengados With
            {
                .Basico = GetBasicoInformation(),
                .Transporte = GetTransporteInformation(),
                .HEDs = GetHEDsInformation(),
                .HENs = GetHENsInformation(),
                .HRNs = GetHRNsInformation(),
                .HEDDFs = GetHEDDsFInformation(),
                .HRDDFs = GetHRDDFsInformation(),
                .HENDFs = GetHENDFsInformation(),
                .HRNDFs = GetHRNDFsInformation(),
                .Vacaciones = GetVacacionesInformation(),
                .Primas = GetPrimasInformation(),
                .Cesantias = GetCesantiasInformation(),
                .Incapacidades = GetIncapacidadesInformation(),
                .Licencias = GetLicenciasInformation(),
                .Bonificaciones = GetBonificacionesInformation(),
                .Auxilios = GetAuxilosInformation(),
                .HuelgasLegales = GetHuelgasInformation(),
                .OtrosConceptos = GetOtrosConceptosDevengadosInformation(),
                .Compensaciones = GetCompensacionesInformation(),
                .BonoEPCTVs = GetBonosInformation(),
                .Comisiones = GetListDecimalInformationByTypes({30}),
                .PagosTerceros = GetListDecimalInformationByTypes({31}),
                .Anticipos = GetListDecimalInformationByTypes({32}),
                .Dotacion = GetDecimalInformationByTypes({33}),
                .ApoyoSost = GetDecimalInformationByTypes({34}),
                .Teletrabajo = GetDecimalInformationByTypes({35}),
                .BonifRetiro = GetDecimalInformationByTypes({36}),
                .Indemnizacion = GetDecimalInformationByTypes({37}),
                .Reintegro = GetDecimalInformationByTypes({38})
            }
        End Function

        Private Function GetBasicoInformation() As NominaIndividualDeAjusteTypeReemplazarDevengadosBasico
            Dim result As NominaIndividualDeAjusteTypeReemplazarDevengadosBasico = Nothing
            If Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {1}.Contains(d.Type)) Then
                result = New NominaIndividualDeAjusteTypeReemplazarDevengadosBasico With
                {
                    .DiasTrabajados = Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {1}.Contains(d.Type)).Sum(Function(d) d.Quantity),
                    .SueldoTrabajado = Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {1}.Contains(d.Type)).Sum(Function(d) d.Value)
                }
            Else
                result = New NominaIndividualDeAjusteTypeReemplazarDevengadosBasico With
                {
                    .DiasTrabajados = 0,
                    .SueldoTrabajado = 0
                }
            End If
            Return result
        End Function

        Private Function GetTransporteInformation() As List(Of NominaIndividualDeAjusteTypeReemplazarDevengadosTransporte)
            Dim results As List(Of NominaIndividualDeAjusteTypeReemplazarDevengadosTransporte) = Nothing
            If Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {2, 3, 4}.Contains(d.Type)) Then
                results = New List(Of NominaIndividualDeAjusteTypeReemplazarDevengadosTransporte)
                Dim result = New NominaIndividualDeAjusteTypeReemplazarDevengadosTransporte
                If Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {2}.Contains(d.Type)) Then
                    result.AuxilioTransporte = Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {2}.Contains(d.Type)).Sum(Function(d) d.Value)
                End If
                If Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {3}.Contains(d.Type)) Then
                    result.ViaticoManuAlojS = Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {3}.Contains(d.Type)).Sum(Function(d) d.Value)
                End If
                If Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {4}.Contains(d.Type)) Then
                    result.ViaticoManuAlojNS = Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {4}.Contains(d.Type)).Sum(Function(d) d.Value)
                End If
                results.Add(result)
            End If
            Return results
        End Function

        Private Function GetHEDsInformation() As List(Of NominaIndividualDeAjusteTypeReemplazarDevengadosHED)
            Dim results As List(Of NominaIndividualDeAjusteTypeReemplazarDevengadosHED) = Nothing
            If Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {5}.Contains(d.Type)) Then
                results = New List(Of NominaIndividualDeAjusteTypeReemplazarDevengadosHED)
                For Each detail In Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {5}.Contains(d.Type))
                    Dim result = New NominaIndividualDeAjusteTypeReemplazarDevengadosHED
                    If detail.DateStart IsNot Nothing AndAlso detail.DateEnd IsNot Nothing Then
                        result.HoraInicio = detail.DateStart.Value.ToString("yyyy-MM-ddTHH:mm:ss")
                        result.HoraFin = detail.DateEnd.Value.ToString("yyyy-MM-ddTHH:mm:ss")
                    End If
                    result.Cantidad = detail.Quantity
                    result.Porcentaje = detail.Percentage
                    result.Pago = detail.Value
                    results.Add(result)
                Next
            End If
            Return results
        End Function

        Private Function GetHENsInformation() As List(Of NominaIndividualDeAjusteTypeReemplazarDevengadosHEN)
            Dim results As List(Of NominaIndividualDeAjusteTypeReemplazarDevengadosHEN) = Nothing
            If Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {6}.Contains(d.Type)) Then
                results = New List(Of NominaIndividualDeAjusteTypeReemplazarDevengadosHEN)
                For Each detail In Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {6}.Contains(d.Type))
                    Dim result = New NominaIndividualDeAjusteTypeReemplazarDevengadosHEN
                    If detail.DateStart IsNot Nothing AndAlso detail.DateEnd IsNot Nothing Then
                        result.HoraInicio = detail.DateStart.Value.ToString("yyyy-MM-ddTHH:mm:ss")
                        result.HoraFin = detail.DateEnd.Value.ToString("yyyy-MM-ddTHH:mm:ss")
                    End If
                    result.Cantidad = detail.Quantity
                    result.Porcentaje = detail.Percentage
                    result.Pago = detail.Value
                    results.Add(result)
                Next
            End If
            Return results
        End Function

        Private Function GetHRNsInformation() As List(Of NominaIndividualDeAjusteTypeReemplazarDevengadosHRN)
            Dim results As List(Of NominaIndividualDeAjusteTypeReemplazarDevengadosHRN) = Nothing
            If Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {7}.Contains(d.Type)) Then
                results = New List(Of NominaIndividualDeAjusteTypeReemplazarDevengadosHRN)
                For Each detail In Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {7}.Contains(d.Type))
                    Dim result = New NominaIndividualDeAjusteTypeReemplazarDevengadosHRN
                    If detail.DateStart IsNot Nothing AndAlso detail.DateEnd IsNot Nothing Then
                        result.HoraInicio = detail.DateStart.Value.ToString("yyyy-MM-ddTHH:mm:ss")
                        result.HoraFin = detail.DateEnd.Value.ToString("yyyy-MM-ddTHH:mm:ss")
                    End If
                    result.Cantidad = detail.Quantity
                    result.Porcentaje = detail.Percentage
                    result.Pago = detail.Value
                    results.Add(result)
                Next
            End If
            Return results
        End Function

        Private Function GetHEDDsFInformation() As List(Of NominaIndividualDeAjusteTypeReemplazarDevengadosHEDDF)
            Dim results As List(Of NominaIndividualDeAjusteTypeReemplazarDevengadosHEDDF) = Nothing
            If Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {8}.Contains(d.Type)) Then
                results = New List(Of NominaIndividualDeAjusteTypeReemplazarDevengadosHEDDF)
                For Each detail In Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {8}.Contains(d.Type))
                    Dim result = New NominaIndividualDeAjusteTypeReemplazarDevengadosHEDDF
                    If detail.DateStart IsNot Nothing AndAlso detail.DateEnd IsNot Nothing Then
                        result.HoraInicio = detail.DateStart.Value.ToString("yyyy-MM-ddTHH:mm:ss")
                        result.HoraFin = detail.DateEnd.Value.ToString("yyyy-MM-ddTHH:mm:ss")
                    End If
                    result.Cantidad = detail.Quantity
                    result.Porcentaje = detail.Percentage
                    result.Pago = detail.Value
                    results.Add(result)
                Next
            End If
            Return results
        End Function

        Private Function GetHRDDFsInformation() As List(Of NominaIndividualDeAjusteTypeReemplazarDevengadosHRDDF)
            Dim results As List(Of NominaIndividualDeAjusteTypeReemplazarDevengadosHRDDF) = Nothing
            If Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {9}.Contains(d.Type)) Then
                results = New List(Of NominaIndividualDeAjusteTypeReemplazarDevengadosHRDDF)
                For Each detail In Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {9}.Contains(d.Type))
                    Dim result = New NominaIndividualDeAjusteTypeReemplazarDevengadosHRDDF
                    If detail.DateStart IsNot Nothing AndAlso detail.DateEnd IsNot Nothing Then
                        result.HoraInicio = detail.DateStart.Value.ToString("yyyy-MM-ddTHH:mm:ss")
                        result.HoraFin = detail.DateEnd.Value.ToString("yyyy-MM-ddTHH:mm:ss")
                    End If
                    result.Cantidad = detail.Quantity
                    result.Porcentaje = detail.Percentage
                    result.Pago = detail.Value
                    results.Add(result)
                Next
            End If
            Return results
        End Function

        Private Function GetHENDFsInformation() As List(Of NominaIndividualDeAjusteTypeReemplazarDevengadosHENDF)
            Dim results As List(Of NominaIndividualDeAjusteTypeReemplazarDevengadosHENDF) = Nothing
            If Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {10}.Contains(d.Type)) Then
                results = New List(Of NominaIndividualDeAjusteTypeReemplazarDevengadosHENDF)
                For Each detail In Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {10}.Contains(d.Type))
                    Dim result = New NominaIndividualDeAjusteTypeReemplazarDevengadosHENDF
                    If detail.DateStart IsNot Nothing AndAlso detail.DateEnd IsNot Nothing Then
                        result.HoraInicio = detail.DateStart.Value.ToString("yyyy-MM-ddTHH:mm:ss")
                        result.HoraFin = detail.DateEnd.Value.ToString("yyyy-MM-ddTHH:mm:ss")
                    End If
                    result.Cantidad = detail.Quantity
                    result.Porcentaje = detail.Percentage
                    result.Pago = detail.Value
                    results.Add(result)
                Next
            End If
            Return results
        End Function

        Private Function GetHRNDFsInformation() As List(Of NominaIndividualDeAjusteTypeReemplazarDevengadosHRNDF)
            Dim results As List(Of NominaIndividualDeAjusteTypeReemplazarDevengadosHRNDF) = Nothing
            If Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {11}.Contains(d.Type)) Then
                results = New List(Of NominaIndividualDeAjusteTypeReemplazarDevengadosHRNDF)
                For Each detail In Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {11}.Contains(d.Type))
                    Dim result = New NominaIndividualDeAjusteTypeReemplazarDevengadosHRNDF
                    If detail.DateStart IsNot Nothing AndAlso detail.DateEnd IsNot Nothing Then
                        result.HoraInicio = detail.DateStart.Value.ToString("yyyy-MM-ddTHH:mm:ss")
                        result.HoraFin = detail.DateEnd.Value.ToString("yyyy-MM-ddTHH:mm:ss")
                    End If
                    result.Cantidad = detail.Quantity
                    result.Porcentaje = detail.Percentage
                    result.Pago = detail.Value
                    results.Add(result)
                Next
            End If
            Return results
        End Function

        Private Function GetVacacionesInformation() As NominaIndividualDeAjusteTypeReemplazarDevengadosVacaciones
            Dim result As NominaIndividualDeAjusteTypeReemplazarDevengadosVacaciones = Nothing
            If Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {12, 13}.Contains(d.Type)) Then
                result = New NominaIndividualDeAjusteTypeReemplazarDevengadosVacaciones
                If Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {12}.Contains(d.Type)) Then
                    result.VacacionesComunes = New List(Of NominaIndividualDeAjusteTypeReemplazarDevengadosVacacionesVacacionesComunes)
                    For Each detail In Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {12}.Contains(d.Type))
                        Dim resultDetail = New NominaIndividualDeAjusteTypeReemplazarDevengadosVacacionesVacacionesComunes
                        If detail.DateStart IsNot Nothing AndAlso detail.DateEnd IsNot Nothing Then
                            resultDetail.FechaInicio = detail.DateStart.Value.ToString("yyyy-MM-dd")
                            resultDetail.FechaFin = detail.DateEnd.Value.ToString("yyyy-MM-dd")
                        End If
                        resultDetail.Cantidad = detail.Quantity
                        resultDetail.Pago = detail.Value
                        result.VacacionesComunes.Add(resultDetail)
                    Next
                End If
                If Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {13}.Contains(d.Type)) Then
                    result.VacacionesCompensadas = New List(Of NominaIndividualDeAjusteTypeReemplazarDevengadosVacacionesVacacionesCompensadas)
                    For Each detail In Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {13}.Contains(d.Type))
                        Dim resultDetail = New NominaIndividualDeAjusteTypeReemplazarDevengadosVacacionesVacacionesCompensadas
                        resultDetail.Cantidad = detail.Quantity
                        resultDetail.Pago = detail.Value
                        result.VacacionesCompensadas.Add(resultDetail)
                    Next
                End If
            End If
            Return result
        End Function

        Private Function GetPrimasInformation() As NominaIndividualDeAjusteTypeReemplazarDevengadosPrimas
            Dim result As NominaIndividualDeAjusteTypeReemplazarDevengadosPrimas = Nothing
            If Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {14}.Contains(d.Type)) Then
                result = New NominaIndividualDeAjusteTypeReemplazarDevengadosPrimas
                result.Cantidad = Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {14}.Contains(d.Type)).Sum(Function(d) d.Quantity)
                If Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {14}.Contains(d.Type) AndAlso d.Subtype = 0) Then
                    result.Pago = Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {14}.Contains(d.Type) AndAlso d.Subtype = 0).Sum(Function(d) d.Value)
                End If
                If Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {14}.Contains(d.Type) AndAlso d.Subtype = 1) Then
                    result.PagoNS = Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {14}.Contains(d.Type) AndAlso d.Subtype = 1).Sum(Function(d) d.Value)
                End If
            End If
            Return result
        End Function

        Private Function GetCesantiasInformation() As NominaIndividualDeAjusteTypeReemplazarDevengadosCesantias
            Dim result As NominaIndividualDeAjusteTypeReemplazarDevengadosCesantias = Nothing
            If Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {15, 16}.Contains(d.Type)) Then
                result = New NominaIndividualDeAjusteTypeReemplazarDevengadosCesantias
                result.Pago = Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {15}.Contains(d.Type)).Sum(Function(d) d.Quantity)
                result.Porcentaje = Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {16}.Contains(d.Type)).Sum(Function(d) d.Percentage)
                result.PagoIntereses = Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {16}.Contains(d.Type)).Sum(Function(d) d.Value)
            End If
            Return result
        End Function

        Private Function GetIncapacidadesInformation() As List(Of NominaIndividualDeAjusteTypeReemplazarDevengadosIncapacidad)
            Dim results As List(Of NominaIndividualDeAjusteTypeReemplazarDevengadosIncapacidad) = Nothing
            If Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {17}.Contains(d.Type)) Then
                results = New List(Of NominaIndividualDeAjusteTypeReemplazarDevengadosIncapacidad)
                For Each detail In Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {17}.Contains(d.Type))
                    Dim result = New NominaIndividualDeAjusteTypeReemplazarDevengadosIncapacidad
                    If detail.DateStart IsNot Nothing AndAlso detail.DateEnd IsNot Nothing Then
                        result.FechaInicio = detail.DateStart.Value.ToString("yyyy-MM-dd")
                        result.FechaFin = detail.DateEnd.Value.ToString("yyyy-MM-dd")
                    End If
                    result.Cantidad = detail.Quantity
                    result.Tipo = detail.Subtype
                    result.Pago = detail.Value
                    results.Add(result)
                Next
            End If
            Return results
        End Function

        Private Function GetLicenciasInformation() As NominaIndividualDeAjusteTypeReemplazarDevengadosLicencias
            Dim result As NominaIndividualDeAjusteTypeReemplazarDevengadosLicencias = Nothing
            If Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {18, 19, 20}.Contains(d.Type)) Then
                result = New NominaIndividualDeAjusteTypeReemplazarDevengadosLicencias
                If Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {18}.Contains(d.Type)) Then
                    result.LicenciaMP = New List(Of NominaIndividualDeAjusteTypeReemplazarDevengadosLicenciasLicenciaMP)
                    For Each detail In Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {18}.Contains(d.Type))
                        Dim resultDetail = New NominaIndividualDeAjusteTypeReemplazarDevengadosLicenciasLicenciaMP
                        If detail.DateStart IsNot Nothing AndAlso detail.DateEnd IsNot Nothing Then
                            resultDetail.FechaInicio = detail.DateStart.Value.ToString("yyyy-MM-dd")
                            resultDetail.FechaFin = detail.DateEnd.Value.ToString("yyyy-MM-dd")
                        End If
                        resultDetail.Cantidad = detail.Quantity
                        resultDetail.Pago = detail.Value
                        result.LicenciaMP.Add(resultDetail)
                    Next
                End If
                If Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {19}.Contains(d.Type)) Then
                    result.LicenciaR = New List(Of NominaIndividualDeAjusteTypeReemplazarDevengadosLicenciasLicenciaR)
                    For Each detail In Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {19}.Contains(d.Type))
                        Dim resultDetail = New NominaIndividualDeAjusteTypeReemplazarDevengadosLicenciasLicenciaR
                        If detail.DateStart IsNot Nothing AndAlso detail.DateEnd IsNot Nothing Then
                            resultDetail.FechaInicio = detail.DateStart.Value.ToString("yyyy-MM-dd")
                            resultDetail.FechaFin = detail.DateEnd.Value.ToString("yyyy-MM-dd")
                        End If
                        resultDetail.Cantidad = detail.Quantity
                        resultDetail.Pago = detail.Value
                        result.LicenciaR.Add(resultDetail)
                    Next
                End If
                If Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {20}.Contains(d.Type)) Then
                    result.LicenciaNR = New List(Of NominaIndividualDeAjusteTypeReemplazarDevengadosLicenciasLicenciaNR)
                    For Each detail In Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {20}.Contains(d.Type))
                        Dim resultDetail = New NominaIndividualDeAjusteTypeReemplazarDevengadosLicenciasLicenciaNR
                        If detail.DateStart IsNot Nothing AndAlso detail.DateEnd IsNot Nothing Then
                            resultDetail.FechaInicio = detail.DateStart.Value.ToString("yyyy-MM-dd")
                            resultDetail.FechaFin = detail.DateEnd.Value.ToString("yyyy-MM-dd")
                        End If
                        resultDetail.Cantidad = detail.Quantity
                        result.LicenciaNR.Add(resultDetail)
                    Next
                End If
            End If
            Return result
        End Function

        Private Function GetBonificacionesInformation() As List(Of NominaIndividualDeAjusteTypeReemplazarDevengadosBonificacion)
            Dim results As List(Of NominaIndividualDeAjusteTypeReemplazarDevengadosBonificacion) = Nothing
            If Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {21, 22}.Contains(d.Type)) Then
                results = New List(Of NominaIndividualDeAjusteTypeReemplazarDevengadosBonificacion)
                For Each detail In Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {21}.Contains(d.Type))
                    Dim result = New NominaIndividualDeAjusteTypeReemplazarDevengadosBonificacion
                    result.BonificacionS = detail.Value
                    results.Add(result)
                Next
                For Each detail In Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {22}.Contains(d.Type))
                    Dim result = New NominaIndividualDeAjusteTypeReemplazarDevengadosBonificacion
                    result.BonificacionNS = detail.Value
                    results.Add(result)
                Next
            End If
            Return results
        End Function

        Private Function GetAuxilosInformation() As List(Of NominaIndividualDeAjusteTypeReemplazarDevengadosAuxilio)
            Dim results As List(Of NominaIndividualDeAjusteTypeReemplazarDevengadosAuxilio) = Nothing
            If Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {23, 24}.Contains(d.Type)) Then
                results = New List(Of NominaIndividualDeAjusteTypeReemplazarDevengadosAuxilio)
                For Each detail In Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {23}.Contains(d.Type))
                    Dim result = New NominaIndividualDeAjusteTypeReemplazarDevengadosAuxilio
                    result.AuxilioS = detail.Value
                    results.Add(result)
                Next
                For Each detail In Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {24}.Contains(d.Type))
                    Dim result = New NominaIndividualDeAjusteTypeReemplazarDevengadosAuxilio
                    result.AuxilioNS = detail.Value
                    results.Add(result)
                Next
            End If
            Return results
        End Function

        Private Function GetHuelgasInformation() As List(Of NominaIndividualDeAjusteTypeReemplazarDevengadosHuelgaLegal)
            Dim results As List(Of NominaIndividualDeAjusteTypeReemplazarDevengadosHuelgaLegal) = Nothing
            If Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {25}.Contains(d.Type)) Then
                results = New List(Of NominaIndividualDeAjusteTypeReemplazarDevengadosHuelgaLegal)
                For Each detail In Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {25}.Contains(d.Type))
                    Dim result = New NominaIndividualDeAjusteTypeReemplazarDevengadosHuelgaLegal

                    If detail.DateStart IsNot Nothing AndAlso detail.DateEnd IsNot Nothing Then
                        result.FechaInicio = detail.DateStart.Value.ToString("yyyy-MM-dd")
                        result.FechaFin = detail.DateEnd.Value.ToString("yyyy-MM-dd")
                    End If
                    result.Cantidad = detail.Quantity

                    results.Add(result)
                Next
            End If
            Return results
        End Function

        Private Function GetOtrosConceptosDevengadosInformation() As List(Of NominaIndividualDeAjusteTypeReemplazarDevengadosOtroConcepto)
            Dim results As List(Of NominaIndividualDeAjusteTypeReemplazarDevengadosOtroConcepto) = Nothing
            If Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {26}.Contains(d.Type)) Then
                results = New List(Of NominaIndividualDeAjusteTypeReemplazarDevengadosOtroConcepto)
                For Each detail In Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {26}.Contains(d.Type))
                    Dim result = New NominaIndividualDeAjusteTypeReemplazarDevengadosOtroConcepto
                    result.DescripcionConcepto = detail.Detail
                    If detail.Subtype = 0 Then
                        result.ConceptoS = detail.Value
                    ElseIf detail.Subtype = 1 Then
                        result.ConceptoNS = detail.Value
                    End If
                    results.Add(result)
                Next
            End If
            Return results
        End Function

        Private Function GetCompensacionesInformation() As List(Of NominaIndividualDeAjusteTypeReemplazarDevengadosCompensacion)
            Dim results As List(Of NominaIndividualDeAjusteTypeReemplazarDevengadosCompensacion) = Nothing
            If Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {27, 28}.Contains(d.Type)) Then
                results = New List(Of NominaIndividualDeAjusteTypeReemplazarDevengadosCompensacion)
                For Each detail In Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {27}.Contains(d.Type))
                    Dim result = New NominaIndividualDeAjusteTypeReemplazarDevengadosCompensacion
                    result.CompensacionO = detail.Value
                    results.Add(result)
                Next
                For Each detail In Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {28}.Contains(d.Type))
                    Dim result = New NominaIndividualDeAjusteTypeReemplazarDevengadosCompensacion
                    result.CompensacionE = detail.Value
                    results.Add(result)
                Next
            End If
            Return results
        End Function

        Private Function GetBonosInformation() As List(Of NominaIndividualDeAjusteTypeReemplazarDevengadosBonoEPCTV)
            Dim results As List(Of NominaIndividualDeAjusteTypeReemplazarDevengadosBonoEPCTV) = Nothing
            If Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {29}.Contains(d.Type)) Then
                results = New List(Of NominaIndividualDeAjusteTypeReemplazarDevengadosBonoEPCTV)
                For Each detail In Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {29}.Contains(d.Type) AndAlso d.Subtype = 0)
                    Dim result = New NominaIndividualDeAjusteTypeReemplazarDevengadosBonoEPCTV
                    result.PagoS = detail.Value
                    results.Add(result)
                Next
                For Each detail In Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {29}.Contains(d.Type) AndAlso d.Subtype = 1)
                    Dim result = New NominaIndividualDeAjusteTypeReemplazarDevengadosBonoEPCTV
                    result.PagoNS = detail.Value
                    results.Add(result)
                Next
                For Each detail In Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {29}.Contains(d.Type) AndAlso d.Subtype = 2)
                    Dim result = New NominaIndividualDeAjusteTypeReemplazarDevengadosBonoEPCTV
                    result.PagoAlimentacionS = detail.Value
                    results.Add(result)
                Next
                For Each detail In Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {29}.Contains(d.Type) AndAlso d.Subtype = 3)
                    Dim result = New NominaIndividualDeAjusteTypeReemplazarDevengadosBonoEPCTV
                    result.PagoAlimentacionNS = detail.Value
                    results.Add(result)
                Next
            End If
            Return results
        End Function

#End Region

#Region "Deducciones"

        Private Function GetDeduccionesInformation() As NominaIndividualDeAjusteTypeReemplazarDeducciones
            Return New NominaIndividualDeAjusteTypeReemplazarDeducciones With
            {
                .Salud = GetSaludInformation(),
                .FondoPension = GetFondoPensionInformation(),
                .FondoSP = GetFondoSPInformation(),
                .Sindicatos = GetSindicatosInformation(),
                .Sanciones = GetSancionesInformation(),
                .Libranzas = GetLibranzasInformation(),
                .PagosTerceros = GetListDecimalInformationByTypes({46}),
                .Anticipos = GetListDecimalInformationByTypes({47}),
                .OtrasDeducciones = GetListDecimalInformationByTypes({48}),
                .PensionVoluntaria = GetDecimalInformationByTypes({49}),
                .RetencionFuente = GetDecimalInformationByTypes({50}),
                .AFC = GetDecimalInformationByTypes({51}),
                .Cooperativa = GetDecimalInformationByTypes({52}),
                .EmbargoFiscal = GetDecimalInformationByTypes({53}),
                .PlanComplementarios = GetDecimalInformationByTypes({54}),
                .Educacion = GetDecimalInformationByTypes({55}),
                .Reintegro = GetDecimalInformationByTypes({56}),
                .Deuda = GetDecimalInformationByTypes({57})
            }
        End Function

        Private Function GetSaludInformation() As NominaIndividualDeAjusteTypeReemplazarDeduccionesSalud
            Dim result As NominaIndividualDeAjusteTypeReemplazarDeduccionesSalud = New NominaIndividualDeAjusteTypeReemplazarDeduccionesSalud
            If Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {39}.Contains(d.Type)) Then
                result.Porcentaje = Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {39}.Contains(d.Type)).Sum(Function(d) d.Percentage)
                result.Deduccion = Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {39}.Contains(d.Type)).Sum(Function(d) d.Value)
            Else
                result.Porcentaje = 0
                result.Deduccion = 0
            End If
            Return result
        End Function

        Private Function GetFondoPensionInformation() As NominaIndividualDeAjusteTypeReemplazarDeduccionesFondoPension
            Dim result As NominaIndividualDeAjusteTypeReemplazarDeduccionesFondoPension = New NominaIndividualDeAjusteTypeReemplazarDeduccionesFondoPension
            If Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {40}.Contains(d.Type)) Then
                result.Porcentaje = Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {40}.Contains(d.Type)).Sum(Function(d) d.Percentage)
                result.Deduccion = Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {40}.Contains(d.Type)).Sum(Function(d) d.Value)
            Else
                result.Porcentaje = 0
                result.Deduccion = 0
            End If
            Return result
        End Function

        Private Function GetFondoSPInformation() As NominaIndividualDeAjusteTypeReemplazarDeduccionesFondoSP
            Dim result As NominaIndividualDeAjusteTypeReemplazarDeduccionesFondoSP = Nothing
            If Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {41, 42}.Contains(d.Type)) Then
                result = New NominaIndividualDeAjusteTypeReemplazarDeduccionesFondoSP
                If Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {41}.Contains(d.Type)) Then
                    result.Porcentaje = Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {41}.Contains(d.Type)).Sum(Function(d) d.Percentage)
                    result.DeduccionSP = Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {41}.Contains(d.Type)).Sum(Function(d) d.Value)
                End If
                If Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {42}.Contains(d.Type)) Then
                    result.PorcentajeSub = Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {42}.Contains(d.Type)).Sum(Function(d) d.Percentage)
                    result.DeduccionSub = Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {42}.Contains(d.Type)).Sum(Function(d) d.Value)
                End If
            End If
            Return result
        End Function

        Private Function GetSindicatosInformation() As List(Of NominaIndividualDeAjusteTypeReemplazarDeduccionesSindicato)
            Dim results As List(Of NominaIndividualDeAjusteTypeReemplazarDeduccionesSindicato) = Nothing
            If Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {43}.Contains(d.Type)) Then
                results = New List(Of NominaIndividualDeAjusteTypeReemplazarDeduccionesSindicato)
                For Each detail In Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {43}.Contains(d.Type))
                    Dim result = New NominaIndividualDeAjusteTypeReemplazarDeduccionesSindicato
                    result.Porcentaje = detail.Percentage
                    result.Deduccion = detail.Value
                    results.Add(result)
                Next
            End If
            Return results
        End Function

        Private Function GetSancionesInformation() As List(Of NominaIndividualDeAjusteTypeReemplazarDeduccionesSancion)
            Dim results As List(Of NominaIndividualDeAjusteTypeReemplazarDeduccionesSancion) = Nothing
            If Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {44}.Contains(d.Type)) Then
                results = New List(Of NominaIndividualDeAjusteTypeReemplazarDeduccionesSancion)
                For Each detail In Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {44}.Contains(d.Type))
                    Dim result = New NominaIndividualDeAjusteTypeReemplazarDeduccionesSancion
                    result.SancionPublic = If(detail.Subtype = 0, detail.Value, 0)
                    result.SancionPriv = If(detail.Subtype = 1, detail.Value, 0)
                    results.Add(result)
                Next
            End If
            Return results
        End Function

        Private Function GetLibranzasInformation() As List(Of NominaIndividualDeAjusteTypeReemplazarDeduccionesLibranza)
            Dim results As List(Of NominaIndividualDeAjusteTypeReemplazarDeduccionesLibranza) = Nothing
            If Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {45}.Contains(d.Type)) Then
                results = New List(Of NominaIndividualDeAjusteTypeReemplazarDeduccionesLibranza)
                For Each detail In Me._electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {45}.Contains(d.Type))
                    Dim result = New NominaIndividualDeAjusteTypeReemplazarDeduccionesLibranza
                    result.Descripcion = detail.Detail
                    result.Deduccion = detail.Value
                    results.Add(result)
                Next
            End If
            Return results
        End Function

#End Region

#End Region

    End Class

End Namespace



