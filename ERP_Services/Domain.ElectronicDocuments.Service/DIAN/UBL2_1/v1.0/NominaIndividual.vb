Imports Domain.Base.Entities.Enums.ElectronicDocuments.v1_6
Imports Domain.ElectronicDocuments.Entities.UBL2_1.common
Imports Domain.ElectronicDocuments.Entities.UBL2_1.maindoc
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Root
Imports Domain.Base.Entities

Namespace DIAN.UBL2_1.v1_0

    Public Class NominaIndividual

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

        Public Function Populate() As NominaIndividualType
            Dim documentType As New NominaIndividualType
            Dim evaluationResult = Evaluador()

            If Not evaluationResult.StateResult Then
                Throw New Exception(evaluationResult?.Message)
            End If

            documentType.UBLExtensions = GenerateUBLExtensions()
            documentType.Periodo = GetPeriodoInformation()
            documentType.NumeroSecuenciaXML = GetNumeroSecuenciaXMLInformation()
            documentType.LugarGeneracionXML = GetLugarGeneracionXMLInformation()
            documentType.ProveedorXML = GetProveedorXMLInformation()
            documentType.CodigoQR = Me._electronicPayroll.GetQRCode()
            documentType.InformacionGeneral = GetInformacionGeneralInformation()
            documentType.Notas = New List(Of String) From {Me._electronicPayroll.ElectronicPayrollPaymentSupport.MoreInformation.MessageResult}
            documentType.Empleador = GetEmpleadorInformation()
            documentType.Trabajador = GetTrabajadorInformation()
            documentType.Pago = GetPagoInformation()
            documentType.FechasPagos = New List(Of String) From {Me._electronicPayroll.ElectronicPayrollPaymentSupport.MoreInformation.PaymentDate.Value.ToString("yyyy-MM-dd")}
            documentType.Devengados = GetDevengadosInformation()
            documentType.Deducciones = GetDeduccionesInformation()
            documentType.DevengadosTotal = Me._electronicPayroll.ElectronicPayrollPaymentSupport.getAcrualValue()
            documentType.DeduccionesTotal = Me._electronicPayroll.ElectronicPayrollPaymentSupport.getDeductionValue()
            documentType.ComprobanteTotal = Me._electronicPayroll.ElectronicPayrollPaymentSupport.getTotalValue()
            Return documentType
        End Function

#End Region

#Region "Private Methods"

        Private Function GenerateUBLExtensions() As List(Of UBLExtensionType)
            Dim listUBLExtensionType As New List(Of UBLExtensionType)
            listUBLExtensionType.Add(New UBLExtensionType With {.ExtensionContent = New ExtensionContentType})
            Return listUBLExtensionType
        End Function

        Private Function GetPeriodoInformation() As NominaIndividualTypePeriodo
            Return New NominaIndividualTypePeriodo With
            {
                .FechaIngreso = Me._electronicPayroll.ElectronicPayrollPaymentSupport.MoreInformation.AdmissionDate.Value.ToString("yyyy-MM-dd"),
                .FechaRetiro = Me._electronicPayroll.ElectronicPayrollPaymentSupport.MoreInformation.RetirementDate.Value.ToString("yyyy-MM-dd"),
                .FechaLiquidacionInicio = Me._electronicPayroll.ElectronicPayrollPaymentSupport.MoreInformation.LiquidationDateStart.Value.ToString("yyyy-MM-dd"),
                .FechaLiquidacionFin = Me._electronicPayroll.ElectronicPayrollPaymentSupport.MoreInformation.LiquidationDateEnd.Value.ToString("yyyy-MM-dd"),
                .TiempoLaborado = Me._electronicPayroll.ElectronicPayrollPaymentSupport.MoreInformation.TimeWorked,
                .FechaGen = Me._electronicPayroll.CreationDate.ToString("yyyy-MM-dd")
            }
        End Function

        Private Function GetNumeroSecuenciaXMLInformation() As NominaIndividualTypeNumeroSecuenciaXML
            Return New NominaIndividualTypeNumeroSecuenciaXML With
            {
                .Prefijo = If(String.IsNullOrEmpty(Me._electronicPayroll.Prefix), Nothing, Me._electronicPayroll.Prefix),
                .Consecutivo = Me._electronicPayroll.DocumentNumber,
                .Numero = Me._electronicPayroll.GetDocumentNumber()
            }
        End Function

        Private Function GetLugarGeneracionXMLInformation() As NominaIndividualTypeLugarGeneracionXML
            Dim address = Me._supplierThirdParty.Person.Address.First(Function(a) a.DepartmentId IsNot Nothing AndAlso a.CityId IsNot Nothing)
            Return New NominaIndividualTypeLugarGeneracionXML With
            {
                .Pais = "CO",
                .DepartamentoEstado = address.DepartmentCode,
                .MunicipioCiudad = address.CityCode,
                .Idioma = "es"
            }
        End Function

        Private Function GetProveedorXMLInformation() As NominaIndividualTypeProveedorXML
            Return New NominaIndividualTypeProveedorXML With
            {
                .RazonSocial = Me._supplierThirdParty.Name,
                .NIT = Me._supplierThirdParty.Nit,
                .DV = Me._supplierThirdParty.DigitVerification,
                .SoftwareID = Me._settingsAccount.ElectronicPayrollIdentifier,
                .SoftwareSC = Utils.Sha384Encode(String.Concat(Me._settingsAccount.ElectronicPayrollIdentifier, Me._settingsAccount.ElectronicPayrollPin, Me._electronicPayroll.GetDocumentNumber()))
            }
        End Function

        Private Function GetInformacionGeneralInformation()
            Return New NominaIndividualTypeInformacionGeneral With
            {
                .Version = "V1.0: Documento Soporte de Pago de Nómina Electrónica",
                .Ambiente = Utils.GetXmlEnumToString(Of Environment)(IIf(Me._settingsAccount.ElectronicPayrollEnvironment, Environment.Production, Environment.Tests)),
                .TipoXML = Me._electronicPayroll.TipoXML,
                .CUNE = Me._electronicPayroll.CUNE,
                .EncripCUNE = "CUNE-SHA384",
                .FechaGen = Me._electronicPayroll.CreationDate.ToString("yyyy-MM-dd"),
                .HoraGen = Me._electronicPayroll.CreationDate.ToString("HH:mm:ss-05:00"),
                .PeriodoNomina = Me._electronicPayroll.ElectronicPayrollPaymentSupport.MoreInformation.PayrollPeriod,
                .TipoMoneda = Utils.GetXmlEnumToString(Of CurrencyCode)(AmountType.TlsDefaultCurrencyID)
            }
        End Function

        Private Function GetEmpleadorInformation() As NominaIndividualTypeEmpleador
            Dim address = Me._supplierThirdParty.Person.Address.First(Function(a) a.DepartmentId IsNot Nothing AndAlso a.CityId IsNot Nothing)
            Return New NominaIndividualTypeEmpleador With
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

        Private Function GetTrabajadorInformation() As NominaIndividualTypeTrabajador
            Dim result As New NominaIndividualTypeTrabajador
            result.TipoTrabajador = Me._electronicPayroll.ElectronicPayrollPaymentSupport.MoreInformation.WorkerType
            result.SubTipoTrabajador = Me._electronicPayroll.ElectronicPayrollPaymentSupport.MoreInformation.WorkerSubType
            result.AltoRiesgoPension = Me._electronicPayroll.ElectronicPayrollPaymentSupport.MoreInformation.HighPensionRisk
            result.TipoDocumento = Me._employeeThirdParty.Person.getAcquirerType()
            result.NumeroDocumento = Me._employeeThirdParty.Nit
            result.PrimerApellido = Me._employeeThirdParty.Person.FirstLastName
            result.SegundoApellido = Me._employeeThirdParty.Person.SecondLastName
            result.PrimerNombre = Me._employeeThirdParty.Person.FirstName
            If Not String.IsNullOrEmpty(Me._employeeThirdParty.Person.SecondName) Then
                result.OtrosNombres = Me._employeeThirdParty.Person.SecondName
            End If
            result.LugarTrabajoPais = Me._electronicPayroll.ElectronicPayrollPaymentSupport.MoreInformation.WorkplaceCountry
            result.LugarTrabajoDepartamentoEstado = Me._electronicPayroll.ElectronicPayrollPaymentSupport.MoreInformation.WorkplaceDepartment
            result.LugarTrabajoMunicipioCiudad = Me._electronicPayroll.ElectronicPayrollPaymentSupport.MoreInformation.WorkplaceCity
            result.LugarTrabajoDireccion = Me._electronicPayroll.ElectronicPayrollPaymentSupport.MoreInformation.WorkplaceAddress
            result.SalarioIntegral = Me._electronicPayroll.ElectronicPayrollPaymentSupport.MoreInformation.IntegralSalary
            result.TipoContrato = Me._electronicPayroll.ElectronicPayrollPaymentSupport.MoreInformation.ContractType
            result.Sueldo = Me._electronicPayroll.ElectronicPayrollPaymentSupport.MoreInformation.BasicSalary
            Return result
        End Function

        Private Function GetPagoInformation() As NominaIndividualTypePago
            Return New NominaIndividualTypePago With
            {
                .Forma = Me._electronicPayroll.ElectronicPayrollPaymentSupport.MoreInformation.PaymentForm,
                .Metodo = Me._electronicPayroll.ElectronicPayrollPaymentSupport.MoreInformation.PaymentMethod,
                .Banco = Me._electronicPayroll.ElectronicPayrollPaymentSupport.MoreInformation.PaymentBank,
                .TipoCuenta = Me._electronicPayroll.ElectronicPayrollPaymentSupport.MoreInformation.PaymentAccountType,
                .NumeroCuenta = Me._electronicPayroll.ElectronicPayrollPaymentSupport.MoreInformation.PaymentAccountNumber
            }
        End Function

        Private Function GetDecimalInformationByTypes(types As Integer()) As NominaIndividualAmountType
            Dim result As NominaIndividualAmountType = Nothing
            If Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) types.Contains(d.Type)) Then
                result = Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) types.Contains(d.Type)).Sum(Function(d) d.Value)
            End If
            Return result
        End Function

        Private Function GetListDecimalInformationByTypes(types As Integer()) As List(Of NominaIndividualAmountType)
            Dim results As List(Of NominaIndividualAmountType) = Nothing
            If Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) types.Contains(d.Type)) Then
                results = New List(Of NominaIndividualAmountType)
                results.Add(Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) types.Contains(d.Type)).Sum(Function(d) d.Value))
            End If
            Return results
        End Function

#Region "Devengados"

        Private Function GetDevengadosInformation() As NominaIndividualTypeDevengados
            Return New NominaIndividualTypeDevengados With
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

        Private Function GetBasicoInformation() As NominaIndividualTypeDevengadosBasico
            Dim result As NominaIndividualTypeDevengadosBasico = Nothing
            If Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {1}.Contains(d.Type)) Then
                result = New NominaIndividualTypeDevengadosBasico With
                {
                    .DiasTrabajados = Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {1}.Contains(d.Type)).Sum(Function(d) d.Quantity),
                    .SueldoTrabajado = Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {1}.Contains(d.Type)).Sum(Function(d) d.Value)
                }
            Else
                result = New NominaIndividualTypeDevengadosBasico With
                {
                    .DiasTrabajados = 0,
                    .SueldoTrabajado = 0
                }
            End If
            Return result
        End Function

        Private Function GetTransporteInformation() As List(Of NominaIndividualTypeDevengadosTransporte)
            Dim results As List(Of NominaIndividualTypeDevengadosTransporte) = Nothing
            If Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {2, 3, 4}.Contains(d.Type)) Then
                results = New List(Of NominaIndividualTypeDevengadosTransporte)
                Dim result = New NominaIndividualTypeDevengadosTransporte
                If Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {2}.Contains(d.Type)) Then
                    result.AuxilioTransporte = Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {2}.Contains(d.Type)).Sum(Function(d) d.Value)
                End If
                If Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {3}.Contains(d.Type)) Then
                    result.ViaticoManuAlojS = Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {3}.Contains(d.Type)).Sum(Function(d) d.Value)
                End If
                If Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {4}.Contains(d.Type)) Then
                    result.ViaticoManuAlojNS = Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {4}.Contains(d.Type)).Sum(Function(d) d.Value)
                End If
                results.Add(result)
            End If
            Return results
        End Function

        Private Function GetHEDsInformation() As List(Of NominaIndividualTypeDevengadosHED)
            Dim results As List(Of NominaIndividualTypeDevengadosHED) = Nothing
            If Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {5}.Contains(d.Type)) Then
                results = New List(Of NominaIndividualTypeDevengadosHED)
                For Each detail In Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {5}.Contains(d.Type))
                    Dim result = New NominaIndividualTypeDevengadosHED
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

        Private Function GetHENsInformation() As List(Of NominaIndividualTypeDevengadosHEN)
            Dim results As List(Of NominaIndividualTypeDevengadosHEN) = Nothing
            If Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {6}.Contains(d.Type)) Then
                results = New List(Of NominaIndividualTypeDevengadosHEN)
                For Each detail In Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {6}.Contains(d.Type))
                    Dim result = New NominaIndividualTypeDevengadosHEN
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

        Private Function GetHRNsInformation() As List(Of NominaIndividualTypeDevengadosHRN)
            Dim results As List(Of NominaIndividualTypeDevengadosHRN) = Nothing
            If Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {7}.Contains(d.Type)) Then
                results = New List(Of NominaIndividualTypeDevengadosHRN)
                For Each detail In Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {7}.Contains(d.Type))
                    Dim result = New NominaIndividualTypeDevengadosHRN
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

        Private Function GetHEDDsFInformation() As List(Of NominaIndividualTypeDevengadosHEDDF)
            Dim results As List(Of NominaIndividualTypeDevengadosHEDDF) = Nothing
            If Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {8}.Contains(d.Type)) Then
                results = New List(Of NominaIndividualTypeDevengadosHEDDF)
                For Each detail In Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {8}.Contains(d.Type))
                    Dim result = New NominaIndividualTypeDevengadosHEDDF
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

        Private Function GetHRDDFsInformation() As List(Of NominaIndividualTypeDevengadosHRDDF)
            Dim results As List(Of NominaIndividualTypeDevengadosHRDDF) = Nothing
            If Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {9}.Contains(d.Type)) Then
                results = New List(Of NominaIndividualTypeDevengadosHRDDF)
                For Each detail In Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {9}.Contains(d.Type))
                    Dim result = New NominaIndividualTypeDevengadosHRDDF
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

        Private Function GetHENDFsInformation() As List(Of NominaIndividualTypeDevengadosHENDF)
            Dim results As List(Of NominaIndividualTypeDevengadosHENDF) = Nothing
            If Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {10}.Contains(d.Type)) Then
                results = New List(Of NominaIndividualTypeDevengadosHENDF)
                For Each detail In Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {10}.Contains(d.Type))
                    Dim result = New NominaIndividualTypeDevengadosHENDF
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

        Private Function GetHRNDFsInformation() As List(Of NominaIndividualTypeDevengadosHRNDF)
            Dim results As List(Of NominaIndividualTypeDevengadosHRNDF) = Nothing
            If Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {11}.Contains(d.Type)) Then
                results = New List(Of NominaIndividualTypeDevengadosHRNDF)
                For Each detail In Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {11}.Contains(d.Type))
                    Dim result = New NominaIndividualTypeDevengadosHRNDF
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

        Private Function GetVacacionesInformation() As NominaIndividualTypeDevengadosVacaciones
            Dim result As NominaIndividualTypeDevengadosVacaciones = Nothing
            If Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {12, 13}.Contains(d.Type)) Then
                result = New NominaIndividualTypeDevengadosVacaciones
                If Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {12}.Contains(d.Type)) Then
                    result.VacacionesComunes = New List(Of NominaIndividualTypeDevengadosVacacionesVacacionesComunes)
                    For Each detail In Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {12}.Contains(d.Type))
                        Dim resultDetail = New NominaIndividualTypeDevengadosVacacionesVacacionesComunes
                        If detail.DateStart IsNot Nothing AndAlso detail.DateEnd IsNot Nothing Then
                            resultDetail.FechaInicio = detail.DateStart.Value.ToString("yyyy-MM-dd")
                            resultDetail.FechaFin = detail.DateEnd.Value.ToString("yyyy-MM-dd")
                        End If
                        resultDetail.Cantidad = detail.Quantity
                        resultDetail.Pago = detail.Value
                        result.VacacionesComunes.Add(resultDetail)
                    Next
                End If
                If Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {13}.Contains(d.Type)) Then
                    result.VacacionesCompensadas = New List(Of NominaIndividualTypeDevengadosVacacionesVacacionesCompensadas)
                    For Each detail In Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {13}.Contains(d.Type))
                        Dim resultDetail = New NominaIndividualTypeDevengadosVacacionesVacacionesCompensadas
                        resultDetail.Cantidad = detail.Quantity
                        resultDetail.Pago = detail.Value
                        result.VacacionesCompensadas.Add(resultDetail)
                    Next
                End If
            End If
            Return result
        End Function

        Private Function GetPrimasInformation() As NominaIndividualTypeDevengadosPrimas
            Dim result As NominaIndividualTypeDevengadosPrimas = Nothing
            If Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {14}.Contains(d.Type)) Then
                result = New NominaIndividualTypeDevengadosPrimas
                result.Cantidad = Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {14}.Contains(d.Type)).Sum(Function(d) d.Quantity)
                If Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {14}.Contains(d.Type) AndAlso d.Subtype = 0) Then
                    result.Pago = Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {14}.Contains(d.Type) AndAlso d.Subtype = 0).Sum(Function(d) d.Value)
                End If
                If Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {14}.Contains(d.Type) AndAlso d.Subtype = 1) Then
                    result.PagoNS = Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {14}.Contains(d.Type) AndAlso d.Subtype = 1).Sum(Function(d) d.Value)
                End If
            End If
            Return result
        End Function

        Private Function GetCesantiasInformation() As NominaIndividualTypeDevengadosCesantias
            Dim result As NominaIndividualTypeDevengadosCesantias = Nothing
            If Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {15, 16}.Contains(d.Type)) Then
                result = New NominaIndividualTypeDevengadosCesantias
                result.Pago = Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {15}.Contains(d.Type)).Sum(Function(d) d.Value)
                result.Porcentaje = Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {16}.Contains(d.Type)).Sum(Function(d) d.Percentage)
                result.PagoIntereses = Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {16}.Contains(d.Type)).Sum(Function(d) d.Value)
            End If
            Return result
        End Function

        Private Function GetIncapacidadesInformation() As List(Of NominaIndividualTypeDevengadosIncapacidad)
            Dim results As List(Of NominaIndividualTypeDevengadosIncapacidad) = Nothing
            If Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {17}.Contains(d.Type)) Then
                results = New List(Of NominaIndividualTypeDevengadosIncapacidad)
                For Each detail In Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {17}.Contains(d.Type))
                    Dim result = New NominaIndividualTypeDevengadosIncapacidad
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

        Private Function GetLicenciasInformation() As NominaIndividualTypeDevengadosLicencias
            Dim result As NominaIndividualTypeDevengadosLicencias = Nothing
            If Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {18, 19, 20}.Contains(d.Type)) Then
                result = New NominaIndividualTypeDevengadosLicencias
                If Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {18}.Contains(d.Type)) Then
                    result.LicenciaMP = New List(Of NominaIndividualTypeDevengadosLicenciasLicenciaMP)
                    For Each detail In Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {18}.Contains(d.Type))
                        Dim resultDetail = New NominaIndividualTypeDevengadosLicenciasLicenciaMP
                        If detail.DateStart IsNot Nothing AndAlso detail.DateEnd IsNot Nothing Then
                            resultDetail.FechaInicio = detail.DateStart.Value.ToString("yyyy-MM-dd")
                            resultDetail.FechaFin = detail.DateEnd.Value.ToString("yyyy-MM-dd")
                        End If
                        resultDetail.Cantidad = detail.Quantity
                        resultDetail.Pago = detail.Value
                        result.LicenciaMP.Add(resultDetail)
                    Next
                End If
                If Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {19}.Contains(d.Type)) Then
                    result.LicenciaR = New List(Of NominaIndividualTypeDevengadosLicenciasLicenciaR)
                    For Each detail In Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {19}.Contains(d.Type))
                        Dim resultDetail = New NominaIndividualTypeDevengadosLicenciasLicenciaR
                        If detail.DateStart IsNot Nothing AndAlso detail.DateEnd IsNot Nothing Then
                            resultDetail.FechaInicio = detail.DateStart.Value.ToString("yyyy-MM-dd")
                            resultDetail.FechaFin = detail.DateEnd.Value.ToString("yyyy-MM-dd")
                        End If
                        resultDetail.Cantidad = detail.Quantity
                        resultDetail.Pago = detail.Value
                        result.LicenciaR.Add(resultDetail)
                    Next
                End If
                If Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {20}.Contains(d.Type)) Then
                    result.LicenciaNR = New List(Of NominaIndividualTypeDevengadosLicenciasLicenciaNR)
                    For Each detail In Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {20}.Contains(d.Type))
                        Dim resultDetail = New NominaIndividualTypeDevengadosLicenciasLicenciaNR
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

        Private Function GetBonificacionesInformation() As List(Of NominaIndividualTypeDevengadosBonificacion)
            Dim results As List(Of NominaIndividualTypeDevengadosBonificacion) = Nothing
            If Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {21, 22}.Contains(d.Type)) Then
                results = New List(Of NominaIndividualTypeDevengadosBonificacion)
                For Each detail In Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {21}.Contains(d.Type))
                    Dim result = New NominaIndividualTypeDevengadosBonificacion
                    result.BonificacionS = detail.Value
                    results.Add(result)
                Next
                For Each detail In Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {22}.Contains(d.Type))
                    Dim result = New NominaIndividualTypeDevengadosBonificacion
                    result.BonificacionNS = detail.Value
                    results.Add(result)
                Next
            End If
            Return results
        End Function

        Private Function GetAuxilosInformation() As List(Of NominaIndividualTypeDevengadosAuxilio)
            Dim results As List(Of NominaIndividualTypeDevengadosAuxilio) = Nothing
            If Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {23, 24}.Contains(d.Type)) Then
                results = New List(Of NominaIndividualTypeDevengadosAuxilio)
                For Each detail In Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {23}.Contains(d.Type))
                    Dim result = New NominaIndividualTypeDevengadosAuxilio
                    result.AuxilioS = detail.Value
                    results.Add(result)
                Next
                For Each detail In Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {24}.Contains(d.Type))
                    Dim result = New NominaIndividualTypeDevengadosAuxilio
                    result.AuxilioNS = detail.Value
                    results.Add(result)
                Next
            End If
            Return results
        End Function

        Private Function GetHuelgasInformation() As List(Of NominaIndividualTypeDevengadosHuelgaLegal)
            Dim results As List(Of NominaIndividualTypeDevengadosHuelgaLegal) = Nothing
            If Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {25}.Contains(d.Type)) Then
                results = New List(Of NominaIndividualTypeDevengadosHuelgaLegal)
                For Each detail In Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {25}.Contains(d.Type))
                    Dim result = New NominaIndividualTypeDevengadosHuelgaLegal

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

        Private Function GetOtrosConceptosDevengadosInformation() As List(Of NominaIndividualTypeDevengadosOtroConcepto)
            Dim results As List(Of NominaIndividualTypeDevengadosOtroConcepto) = Nothing
            If Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {26}.Contains(d.Type)) Then
                results = New List(Of NominaIndividualTypeDevengadosOtroConcepto)
                For Each detail In Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {26}.Contains(d.Type))
                    Dim result = New NominaIndividualTypeDevengadosOtroConcepto
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

        Private Function GetCompensacionesInformation() As List(Of NominaIndividualTypeDevengadosCompensacion)
            Dim results As List(Of NominaIndividualTypeDevengadosCompensacion) = Nothing
            If Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {27, 28}.Contains(d.Type)) Then
                results = New List(Of NominaIndividualTypeDevengadosCompensacion)
                For Each detail In Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {27}.Contains(d.Type))
                    Dim result = New NominaIndividualTypeDevengadosCompensacion
                    result.CompensacionO = detail.Value
                    results.Add(result)
                Next
                For Each detail In Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {28}.Contains(d.Type))
                    Dim result = New NominaIndividualTypeDevengadosCompensacion
                    result.CompensacionE = detail.Value
                    results.Add(result)
                Next
            End If
            Return results
        End Function

        Private Function GetBonosInformation() As List(Of NominaIndividualTypeDevengadosBonoEPCTV)
            Dim results As List(Of NominaIndividualTypeDevengadosBonoEPCTV) = Nothing
            If Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {29}.Contains(d.Type)) Then
                results = New List(Of NominaIndividualTypeDevengadosBonoEPCTV)
                For Each detail In Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {29}.Contains(d.Type) AndAlso d.Subtype = 0)
                    Dim result = New NominaIndividualTypeDevengadosBonoEPCTV
                    result.PagoS = detail.Value
                    results.Add(result)
                Next
                For Each detail In Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {29}.Contains(d.Type) AndAlso d.Subtype = 1)
                    Dim result = New NominaIndividualTypeDevengadosBonoEPCTV
                    result.PagoNS = detail.Value
                    results.Add(result)
                Next
                For Each detail In Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {29}.Contains(d.Type) AndAlso d.Subtype = 2)
                    Dim result = New NominaIndividualTypeDevengadosBonoEPCTV
                    result.PagoAlimentacionS = detail.Value
                    results.Add(result)
                Next
                For Each detail In Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {29}.Contains(d.Type) AndAlso d.Subtype = 3)
                    Dim result = New NominaIndividualTypeDevengadosBonoEPCTV
                    result.PagoAlimentacionNS = detail.Value
                    results.Add(result)
                Next
            End If
            Return results
        End Function

#End Region

#Region "Deducciones"

        Private Function GetDeduccionesInformation() As NominaIndividualTypeDeducciones
            Return New NominaIndividualTypeDeducciones With
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

        Private Function GetSaludInformation() As NominaIndividualTypeDeduccionesSalud
            Dim result As NominaIndividualTypeDeduccionesSalud = New NominaIndividualTypeDeduccionesSalud
            If Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {39}.Contains(d.Type)) Then
                result.Porcentaje = Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {39}.Contains(d.Type)).Sum(Function(d) d.Percentage)
                result.Deduccion = Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {39}.Contains(d.Type)).Sum(Function(d) d.Value)
            Else
                'Si no encontramos salud enviamos datos en 0
                result.Porcentaje = 0
                result.Deduccion = 0
            End If
            Return result
        End Function

        Private Function GetFondoPensionInformation() As NominaIndividualTypeDeduccionesFondoPension
            Dim result As NominaIndividualTypeDeduccionesFondoPension = New NominaIndividualTypeDeduccionesFondoPension
            If Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {40}.Contains(d.Type)) Then
                result.Porcentaje = Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {40}.Contains(d.Type)).Sum(Function(d) d.Percentage)
                result.Deduccion = Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {40}.Contains(d.Type)).Sum(Function(d) d.Value)
            Else
                result.Porcentaje = 0
                result.Deduccion = 0
            End If
            Return result
        End Function

        Private Function GetFondoSPInformation() As NominaIndividualTypeDeduccionesFondoSP
            Dim result As NominaIndividualTypeDeduccionesFondoSP = Nothing
            If Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {41, 42}.Contains(d.Type)) Then
                result = New NominaIndividualTypeDeduccionesFondoSP
                If Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {41}.Contains(d.Type)) Then
                    result.Porcentaje = Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {41}.Contains(d.Type)).Sum(Function(d) d.Percentage)
                    result.DeduccionSP = Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {41}.Contains(d.Type)).Sum(Function(d) d.Value)
                End If
                If Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {42}.Contains(d.Type)) Then
                    result.PorcentajeSub = Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {42}.Contains(d.Type)).Sum(Function(d) d.Percentage)
                    result.DeduccionSub = Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {42}.Contains(d.Type)).Sum(Function(d) d.Value)
                End If
            End If
            Return result
        End Function

        Private Function GetSindicatosInformation() As List(Of NominaIndividualTypeDeduccionesSindicato)
            Dim results As List(Of NominaIndividualTypeDeduccionesSindicato) = Nothing
            If Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {43}.Contains(d.Type)) Then
                results = New List(Of NominaIndividualTypeDeduccionesSindicato)
                For Each detail In Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {43}.Contains(d.Type))
                    Dim result = New NominaIndividualTypeDeduccionesSindicato
                    result.Porcentaje = detail.Percentage
                    result.Deduccion = detail.Value
                    results.Add(result)
                Next
            End If
            Return results
        End Function

        Private Function GetSancionesInformation() As List(Of NominaIndividualTypeDeduccionesSancion)
            Dim results As List(Of NominaIndividualTypeDeduccionesSancion) = Nothing
            If Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {44}.Contains(d.Type)) Then
                results = New List(Of NominaIndividualTypeDeduccionesSancion)
                For Each detail In Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {44}.Contains(d.Type))
                    Dim result = New NominaIndividualTypeDeduccionesSancion
                    result.SancionPublic = If(detail.Subtype = 0, detail.Value, 0)
                    result.SancionPriv = If(detail.Subtype = 1, detail.Value, 0)
                    results.Add(result)
                Next
            End If
            Return results
        End Function

        Private Function GetLibranzasInformation() As List(Of NominaIndividualTypeDeduccionesLibranza)
            Dim results As List(Of NominaIndividualTypeDeduccionesLibranza) = Nothing
            If Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) {45}.Contains(d.Type)) Then
                results = New List(Of NominaIndividualTypeDeduccionesLibranza)
                For Each detail In Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) {45}.Contains(d.Type))
                    Dim result = New NominaIndividualTypeDeduccionesLibranza
                    result.Descripcion = detail.Detail
                    result.Deduccion = detail.Value
                    results.Add(result)
                Next
            End If
            Return results
        End Function


        ''' <summary>
        ''' Funcion que recorre los items de la tabla de detalle traidos del sp y comprueba si hay alguno nulo, si lo hay bota el mensaje con los detalles nulos
        ''' </summary>
        ''' <returns></returns>
        Private Function Evaluador() As ActionResult

            If Me._electronicPayroll.ElectronicPayrollPaymentSupport.Details.Exists(Function(d) d.Type Is Nothing) Then


                Dim listDetail = Me._electronicPayroll _
                                    .ElectronicPayrollPaymentSupport _
                                    .Details.FindAll(Function(d) d.Type Is Nothing) _
                                    .Select(Function(item) item.Detail).ToList()


                Dim resultMessage As String = String.Join(", ", listDetail)

                Return New ActionResult With {
                    .StateResult = False,
                    .Message = "Algunos detalles tienen el tipo de concepto nulo: " & resultMessage
                }

            End If

            Return New ActionResult With {
                  .StateResult = True,
                  .Message = "Todos los detalles son válidos."
              }
        End Function

#End Region

#End Region

    End Class

End Namespace



