#Region "Imports"

Imports System.Globalization
Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Xml.Serialization
Imports DistributedServices.DIAN.WcfDianCustomerServices
Imports Domain.Base.Entities
Imports Domain.ElectronicDocuments.Service
Imports Domain.Entities
Imports Domain.Payroll
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.AzureBlobStorage
Imports Infrastructure.CrossCutting.AzureBlobStorage.Factory
Imports Infrastructure.CrossCutting.Base

#End Region

Public Class ElectronicPayrollAdminService
    Implements IElectronicPayrollAdminService

#Region "Fields"

    Private _electronicPayrollRepository As IElectronicPayrollRepository
    Private _electronicPayrollDetailRepository As IElectronicPayrollDetailRepository
    Private _electronicPayrollNotificationRepository As IElectronicPayrollNotificationRepository
    Private _thirdPartyRepository As IThirdPartyRepository
    Private _settingsAccountRepository As ISettingsAccountRepository
    Private _electronicPayrollPaymentSupportRepository As IElectronicPayrollPaymentSupportRepository
    Private ReadOnly _factoryStorage As IFactoryStorage
    Private ReadOnly _storage As IStorage

#End Region

#Region "Properties"

    Private ReadOnly statusCodeErrors As String() =
    {
        "500",
        "503"
    }

#End Region

#Region "Builders"

    Public Sub New(electronicPayrollRepository As IElectronicPayrollRepository,
                   electronicPayrollDetailRepository As IElectronicPayrollDetailRepository,
                   electronicPayrollNotificationRepository As IElectronicPayrollNotificationRepository,
                   thirdPartyRepository As IThirdPartyRepository,
                   settingsAccountRepository As ISettingsAccountRepository,
                   electronicPayrollPaymentSupportRepository As IElectronicPayrollPaymentSupportRepository,
                   factoryStorage As IFactoryStorage)

        Me._electronicPayrollRepository = electronicPayrollRepository
        Me._electronicPayrollDetailRepository = electronicPayrollDetailRepository
        Me._electronicPayrollNotificationRepository = electronicPayrollNotificationRepository
        Me._thirdPartyRepository = thirdPartyRepository
        Me._settingsAccountRepository = settingsAccountRepository
        Me._electronicPayrollPaymentSupportRepository = electronicPayrollPaymentSupportRepository
        Me._factoryStorage = factoryStorage
        Me._storage = Me._factoryStorage.CreateStorageControl()

        ' Setting TLS 1.2 protocol '
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
        ServicePointManager.ServerCertificateValidationCallback = Function(sender1, certificate, chain, sslPolicyErrors)
                                                                      Return True
                                                                  End Function
    End Sub

#End Region

#Region "Methods"

    Public Async Function ProcessElectronicPayroll(operatingUnitId As Integer, electronicPayrollIds As List(Of Integer)) As Task(Of ActionResult(Of String)) Implements IElectronicPayrollAdminService.ProcessElectronicPayroll
        Try
            Return Await ExecuteProcessAsync(operatingUnitId, electronicPayrollIds)
        Catch ex As Exception
            Return New ActionResult(Of String) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

#Region "ElectronicPaymentSupportReport"

    ''' <summary>
    ''' Método para obtener información para el reporte de soporte de nómina electrónica
    ''' </summary>
    ''' <param name="consecutive"></param>
    ''' <returns></returns>
    Public Async Function GetElectronicPaymentSupportXML(consecutive As String) As Task(Of ActionResult(Of NominaIndividual)) Implements IElectronicPayrollAdminService.GetElectronicPaymentSupportXML
        Try
            Dim electronicPayroll As ElectronicPayroll = _electronicPayrollRepository.GetElectronicPayrollByDocumentNumber(consecutive)
            If electronicPayroll Is Nothing Then
                Return New ActionResult(Of NominaIndividual) With {.StateResult = False, .Message = $"No se encontró nómina electrónica para el consecutivo '{consecutive}'"}
            End If

            Dim fileName = GetFileName(electronicPayroll.Consecutive, electronicPayroll.Year)
            Dim filePath = electronicPayroll.FilePath

            Dim factory As New FactoryStorage(ServerSessionValues.Current.BlobContainerName)
            Dim storage As IStorage = factory.CreateStorageControl()

            If Not storage.ValidateFileExists(filePath, fileName) Then
                Return New ActionResult(Of NominaIndividual) With {.StateResult = False, .Message = $"El archivo XML para el número de documento '{consecutive}' no fue encontrado"}
            End If

            Dim fileBytes As Byte() = storage.ReadFile(filePath, fileName)
            Dim xmlContent As String = Encoding.UTF8.GetString(fileBytes)
            Dim cleanedXml As String = RegularExpressions.Regex.Replace(xmlContent, "<ext:UBLExtensions[\s\S]*?</ext:UBLExtensions>", "", RegularExpressions.RegexOptions.IgnoreCase)

            Dim serializer As New XmlSerializer(GetType(NominaIndividual))
            Using stringReader As New StringReader(cleanedXml)
                Dim payrollDocument As NominaIndividual = CType(serializer.Deserialize(stringReader), NominaIndividual)

                payrollDocument.ReportConcept = FlattenerConcepts(payrollDocument)
                Return New ActionResult(Of NominaIndividual) With {.StateResult = True, .ObjectEmbbeded = payrollDocument}
            End Using

        Catch ex As Exception

            Return New ActionResult(Of NominaIndividual) With {.StateResult = False, .Message = $"Error obteniendo el documento soporte XML: {ex.Message}"}
        End Try
    End Function

    ''' <summary>
    ''' Propiedad de tipo HashSet para conceptos de nómina anidados
    ''' </summary>
    Private Shared ReadOnly ContainerConcept As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase) From {
    "Vacaciones",
    "HEDs",
    "HENs",
    "HRNs",
    "HEDDFs",
    "HRDDFs",
    "HENDFs",
    "HRNDFs",
    "Incapacidades",
    "Licencias",
    "Bonificaciones",
    "Auxilios",
    "HuelgasLegales",
    "OtrosConceptos",
    "Compensaciones",
    "BonoEPCTVs",
    "Comisiones",
    "PagosTerceros",
    "Anticipos",
    "Sindicatos",
    "Sanciones",
    "Libranzas",
    "PagosTerceros",
    "Anticipos",
    "OtrasDeducciones"
    }

    ''' <summary>
    ''' Diccionario de conceptos (Devengados/Deducciones) y sus descripciones
    ''' </summary>
    Private Shared ReadOnly conceptDescription As New Dictionary(Of String, Dictionary(Of String, String)) From {
        {"Devengado", New Dictionary(Of String, String) From {
        {"Basico", "Salario"},
        {"Transporte", "Auxilio de Transporte / ViaticoS / ViaticosNS"},
        {"HED", "Hora Extra Diurna"},
        {"HEN", "Hora Extra Nocturna"},
        {"HRN", "Hora Recargo Nocturno"},
        {"HEDDF", "Horas Extras Dominicales y Festivas"},
        {"HRDDF", "Recargo Dominicales y Festivas"},
        {"HENDF", "Horas Extras Nocturnas Dominicales y Festivas"},
        {"HRNDF", "Recargo Nocturno Dominicales y Festivas"},
        {"VacacionesComunes", "Vacaciones"},
        {"VacacionesCompensadas", "Vacaciones Compensadas"},
        {"Primas", "Prima / PrimaNS"},
        {"Cesantias", "Cesantías"},
        {"InteresesCesantias", "Intereses Cesantías"},
        {"Incapacidad", "Incapacidades"},
        {"LicenciaMP", "Licencia de Materinidad o Paternidad"},
        {"LicenciaR", "Licencia Remunerada"},
        {"LicenciaNR", "Licencia No Remunerada"},
        {"Bonificacion", "Bonificaciones"},
        {"Auxilio", "AuxilioS / AuxilioNS"},
        {"Huelga Legal", "Huelgas Legales"},
        {"OtroConcepto", ""},
        {"Compensacion", "CompensaciónO / CompensaciónE"},
        {"BonoEPCTV", "PagoS / PagoNS / PagoAlimentacionS / PagoAlimentacionNS"},
        {"Comision", "Comisión"},
        {"PagoTercero", "Pago Tercero"},
        {"AnticipoDev", "Anticipo"},
        {"Dotacion", "Dotación"},
        {"ApoyoSost", "Apoyo a Sostenimiento"},
        {"Teletrabajo", "Teletrabajo"},
        {"BonifRetiro", "Bonificación Retiro"},
        {"Indemnizacion", "Indemnización"},
        {"Reintegro", "Reintegro Devengados"}
        }},
        {"Deduccion", New Dictionary(Of String, String) From {
        {"Salud", "Aporte Salud"},
        {"FondoPension", "Aporte Pensión"},
        {"FondoSP", "Fondo de Seguridad Pensional / Subsistencia"},
        {"Sindicato", "Sindicato"},
        {"Sancion", "Sanción Pública / Privada"},
        {"Libranza", "Libranza"},
        {"PagoTercero", "Pago Tercero"},
        {"Anticipo", "Anticipo"},
        {"OtraDeduccion", "Otra Deduccion"},
        {"PensionVoluntaria", "Pensión Voluntaria"},
        {"RetencionFuente", "Retencion Fuente"},
        {"AFC", "Ahorro Fomento a la Construcción"},
        {"Cooperativa", "Cooperativa"},
        {"EmbargoFiscal", "Embargo Fiscal"},
        {"PlanComplementarios", "Plan Complementarios"},
        {"Educacion", "Educación"},
        {"Reintegro", "Reintegro Deducciones"},
        {"Deuda", "Deuda"}
        }}
    }

    ''' <summary>
    ''' Método para convertir todos los conceptos de nómina a una lista de tipo NominaConcepto
    ''' </summary>
    ''' <param name="nomina"></param>
    ''' <returns></returns>
    Public Shared Function FlattenerConcepts(nomina As NominaIndividual) As List(Of NominaConcepto)
        Dim list As New List(Of NominaConcepto)

        ' Devengados
        If nomina.Devengados IsNot Nothing Then
            list.AddRange(FlattenSection(nomina.Devengados, "Devengado"))
        End If

        ' Deducciones
        If nomina.Deducciones IsNot Nothing Then
            list.AddRange(FlattenSection(nomina.Deducciones, "Deduccion"))
        End If

        Return list
    End Function

    ''' <summary>
    ''' Método que recorre y mapea todas las propiedades de las secciones "devengados" y "deducciones" 
    ''' </summary>
    ''' <param name="section"></param>
    ''' <param name="type"></param>
    ''' <returns></returns>
    Private Shared Function FlattenSection(section As Object, type As String) As List(Of NominaConcepto)
        Dim result As New List(Of NominaConcepto)

        For Each prop In section.GetType().GetProperties(Reflection.BindingFlags.Public Or Reflection.BindingFlags.Instance)
            Dim name = prop.Name
            Dim value = prop.GetValue(section)

            If value Is Nothing Then Continue For

            If TypeOf value Is IEnumerable AndAlso Not TypeOf value Is String Then
                For Each item In DirectCast(value, IEnumerable)
                    If item Is Nothing Then Continue For
                    result.Add(MapToConcepto(name, item, type))
                Next

            ElseIf prop.PropertyType.IsClass AndAlso Not TypeOf value Is String Then
                If ContainerConcept.Contains(name) Then
                    For Each subProp In value.GetType().GetProperties(Reflection.BindingFlags.Public Or Reflection.BindingFlags.Instance)
                        Dim subValue = subProp.GetValue(value)
                        If subValue Is Nothing Then Continue For
                        result.Add(MapToConcepto(subProp.Name, subValue, type))
                    Next
                ElseIf name = "Cesantias" Then
                    result.AddRange(MapCesantiasToConceptos(value, type))
                Else
                    result.Add(MapToConcepto(name, value, type))
                End If

            ElseIf prop.PropertyType Is GetType(String) Then
                If Not String.IsNullOrWhiteSpace(value.ToString()) Then
                    result.Add(New NominaConcepto With {
                               .Concepto = name,
                               .Descripcion = GetDescriptionByType(type, name),
                               .Tipo = type,
                               .Devengado = If(type = "Devengado" AndAlso IsNumeric(value), Convert.ToDecimal(value, CultureInfo.InvariantCulture), 0D),
                               .Deduccion = If(type = "Deduccion" AndAlso IsNumeric(value), Convert.ToDecimal(value, CultureInfo.InvariantCulture), 0D)
                    })
                End If
            End If
        Next

        Return result
    End Function

    ''' <summary>
    ''' Método para mapear Cesantías e Intereses de Cesantías como dos conceptos separados
    ''' </summary>
    ''' <param name="cesantiasObj">Objeto Cesantias del XML</param>
    ''' <param name="type">Tipo de concepto (Devengado)</param>
    ''' <returns>Lista con dos conceptos: Cesantías e Intereses Cesantías</returns>
    Private Shared Function MapCesantiasToConceptos(cesantiasObj As Object, type As String) As List(Of NominaConcepto)
        Dim conceptos As New List(Of NominaConcepto)

        Dim pago = cesantiasObj.GetType().GetProperty("Pago")?.GetValue(cesantiasObj)?.ToString()
        Dim pagoIntereses = cesantiasObj.GetType().GetProperty("PagoIntereses")?.GetValue(cesantiasObj)?.ToString()

        'Cesantías 
        If Not String.IsNullOrWhiteSpace(pago) AndAlso IsNumeric(pago) Then
            conceptos.Add(New NominaConcepto With {
                .Concepto = "Cesantias",
                .Descripcion = GetDescriptionByType(type, "Cesantias"),
                .Tipo = type,
                .Devengado = Convert.ToDecimal(pago, CultureInfo.InvariantCulture),
                .Deduccion = 0D
            })
        End If

        'Intereses Cesantías
        If Not String.IsNullOrWhiteSpace(pagoIntereses) AndAlso IsNumeric(pagoIntereses) Then
            conceptos.Add(New NominaConcepto With {
                .Concepto = "InteresesCesantias",
                .Descripcion = GetDescriptionByType(type, "InteresesCesantias"),
                .Tipo = type,
                .Devengado = Convert.ToDecimal(pagoIntereses, CultureInfo.InvariantCulture),
                .Deduccion = 0D
            })
        End If

        Return conceptos
    End Function

    ''' <summary>
    ''' Método que extrae las propiedades principales del xml y se asigna a un objeto de tipo "NominaConcepto"
    ''' </summary>
    ''' <param name="conceptName"></param>
    ''' <param name="obj"></param>
    ''' <param name="type"></param>
    ''' <returns></returns>
    Private Shared Function MapToConcepto(conceptName As String, obj As Object, type As String) As NominaConcepto
        Dim concept As New NominaConcepto With {
        .Concepto = conceptName,
        .Descripcion = GetDescriptionByType(type, conceptName),
        .Tipo = type
        }

        If conceptName = "VacacionesCompensadas" Or conceptName = "VacacionesComunes" Then
            concept.Cantidad = obj.GetType().GetProperty("Cantidad")?.GetValue(obj)?.ToString()
            Dim pago = obj.GetType().GetProperty("Pago")?.GetValue(obj)?.ToString()
            concept.Devengado = If(Not String.IsNullOrWhiteSpace(pago) AndAlso IsNumeric(pago), Convert.ToDecimal(pago, CultureInfo.InvariantCulture), 0D)
            Return concept
        End If

        If conceptName = "OtroConcepto" Then
            concept.Descripcion = obj.GetType().GetProperty("DescripcionConcepto")?.GetValue(obj)?.ToString()
            Dim conceptoS = obj.GetType().GetProperty("ConceptoS")?.GetValue(obj)?.ToString()
            Dim conceptoNS = obj.GetType().GetProperty("ConceptoNS")?.GetValue(obj)?.ToString()
            concept.Devengado = SumarValoresDecimales(conceptoS, conceptoNS)
            Return concept
        End If

        If conceptName = "Bonificacion" Then
            Dim bonificacionS = obj.GetType().GetProperty("BonificacionS")?.GetValue(obj)?.ToString()
            Dim bonificacionNS = obj.GetType().GetProperty("BonificacionNS")?.GetValue(obj)?.ToString()
            concept.Devengado = SumarValoresDecimales(bonificacionS, bonificacionNS)
            Return concept
        End If

        If conceptName = "Auxilio" Then
            Dim auxilioS = obj.GetType().GetProperty("AuxilioS")?.GetValue(obj)?.ToString()
            Dim auxilioNS = obj.GetType().GetProperty("AuxilioNS")?.GetValue(obj)?.ToString()
            concept.Devengado = SumarValoresDecimales(auxilioS, auxilioNS)
            Return concept
        End If

        If conceptName = "Compensacion" Then
            Dim compensacionO = obj.GetType().GetProperty("CompensacionO")?.GetValue(obj)?.ToString()
            Dim compensacionE = obj.GetType().GetProperty("CompensacionE")?.GetValue(obj)?.ToString()
            concept.Devengado = SumarValoresDecimales(compensacionO, compensacionE)
            Return concept
        End If

        If conceptName = "BonoEPCTV" Then
            Dim pagoS = obj.GetType().GetProperty("PagoS")?.GetValue(obj)?.ToString()
            Dim pagoNS = obj.GetType().GetProperty("PagoNS")?.GetValue(obj)?.ToString()
            Dim pagoAlimentacionS = obj.GetType().GetProperty("PagoAlimentacionS")?.GetValue(obj)?.ToString()
            Dim pagoAlimentacionNS = obj.GetType().GetProperty("PagoAlimentacionNS")?.GetValue(obj)?.ToString()
            concept.Devengado = SumarValoresDecimales(pagoS, pagoNS, pagoAlimentacionS, pagoAlimentacionNS)
            Return concept
        End If

        If conceptName = "Primas" Then
            concept.Cantidad = obj.GetType().GetProperty("Cantidad")?.GetValue(obj)?.ToString()
            Dim pago = obj.GetType().GetProperty("Pago")?.GetValue(obj)?.ToString()
            Dim pagoNS = obj.GetType().GetProperty("PagoNS")?.GetValue(obj)?.ToString()
            concept.Devengado = SumarValoresDecimales(pago, pagoNS)
            Return concept
        End If

        If conceptName = "Transporte" Then
            Dim auxilioTransporte = obj.GetType().GetProperty("AuxilioTransporte")?.GetValue(obj)?.ToString()
            Dim viaticoS = obj.GetType().GetProperty("ViaticoManuAlojS")?.GetValue(obj)?.ToString()
            Dim viaticoNS = obj.GetType().GetProperty("ViaticoManuAlojNS")?.GetValue(obj)?.ToString()
            concept.Devengado = SumarValoresDecimales(auxilioTransporte, viaticoS, viaticoNS)
            Return concept
        End If

        For Each prop In obj.GetType().GetProperties(Reflection.BindingFlags.Public Or Reflection.BindingFlags.Instance)
            Dim pname = prop.Name.ToLowerInvariant
            Dim pval = If(prop.GetValue(obj), "").ToString()

            If pname = "descripcionconcepto" Or pname.StartsWith("descripcion") AndAlso String.IsNullOrWhiteSpace(concept.Descripcion) Then
                concept.Descripcion = pval
            End If

            If pname = "diastrabajados" Or pname = "cantidad" Or pname.StartsWith("cantidad") Or pname.Contains("dias") Then
                concept.Cantidad = pval
            End If

            ' Devengado
            If type = "Devengado" Then
                If pname = "sueldotrabajado" Or pname = "pagos" Or pname = "pago" Or pname = "pagons" Or pname = "pagointereses" Or pname = "valor" Or pname = "auxiliotransporte" Or pname = "viaticomanualojs" Or pname = "viaticomanualojns" Or pname = "bonificacions" Or pname = "bonificacionns" Or pname = "auxilios" Or pname = "auxilions" Or pname = "conceptos" Or pname = "conceptons" Or pname = "compensaciono" Or pname = "compensacione" Or pname.StartsWith("pago") Then
                    If Not String.IsNullOrWhiteSpace(pval) AndAlso IsNumeric(pval) Then
                        concept.Devengado = Convert.ToDecimal(pval, CultureInfo.InvariantCulture)
                    End If
                End If
            End If

            ' Deducción
            If type = "Deduccion" Then
                If pname = "deduccion" Or pname = "deduccionsp" Or pname = "deduccionsub" Or pname = "otradeduccion" Or pname = "valor" Or pname = "sancionpublic" Or pname = "sancionpriv" Then
                    If Not String.IsNullOrWhiteSpace(pval) AndAlso IsNumeric(pval) Then
                        concept.Deduccion = Convert.ToDecimal(pval, CultureInfo.InvariantCulture)
                    End If
                End If
            End If
        Next

        Return concept
    End Function

    ''' <summary>
    ''' Devuelve la descripción para cada concepto, según el tipo y nombre del concepto
    ''' </summary>
    ''' <param name="type"></param>
    ''' <param name="conceptName"></param>
    ''' <returns></returns>
    Private Shared Function GetDescriptionByType(type As String, conceptName As String) As String
        If conceptDescription.ContainsKey(type) AndAlso conceptDescription(type).ContainsKey(conceptName) Then
            Return conceptDescription(type)(conceptName)
        End If
        Return conceptName
    End Function

    ''' <summary>
    ''' Suma múltiples valores string que representan decimales.
    ''' Ignora valores nulos, vacíos o no numéricos.
    ''' </summary>
    ''' <param name="valores">Array de valores string a sumar</param>
    ''' <returns>Suma total como Decimal</returns>
    Private Shared Function SumarValoresDecimales(ParamArray valores() As String) As Decimal
        Dim total As Decimal = 0D
        For Each valor In valores
            If Not String.IsNullOrWhiteSpace(valor) AndAlso IsNumeric(valor) Then
                total += Convert.ToDecimal(valor, CultureInfo.InvariantCulture)
            End If
        Next
        Return total
    End Function

    ''' <summary>
    ''' Método para construir el nombre del archibo que va a ser consultado al blobstorage
    ''' </summary>
    ''' <param name="consecutive"></param>
    ''' <param name="year"></param>
    ''' <returns></returns>
    Private Function GetFileName(consecutive As Integer, year As Integer) As String
        Dim format = "{0}{1}{2}.xml"
        Dim documentTypeName = "nie"
        Dim electroniPayrollYear = year.ToString().Substring(2, 2)
        Dim electronicPayrollConsecutive = Hex(consecutive).PadLeft(8, "0")

        Return String.Format(format, documentTypeName, electroniPayrollYear, electronicPayrollConsecutive)
    End Function
#End Region

#End Region

#Region "Private Methods"

#Region "DIAN"

    Private Async Function ExecuteProcessAsync(operatingUnitId As Integer, electronicPayrollIds As List(Of Integer)) As Task(Of ActionResult(Of String))
        Dim errors As New StringBuilder
        Dim messages As New StringBuilder

        'Diccionarios
        Dim dictionarySupplierThirdParty As New Dictionary(Of Integer, Domain.Entities.ThirdParty)()
        Dim dictionaryEmployeeThirdParty As New Dictionary(Of Integer, Domain.Entities.ThirdParty)()

        'Entidades
        Dim settingsAccount As GeneralLedgerSettings
        Dim supplierThirdParty As Domain.Entities.ThirdParty
        Dim employeeThirdParty As Domain.Entities.ThirdParty

        'Cargamos parametros de contabilidad para la unidad operativa
        settingsAccount = _settingsAccountRepository.GetSettingAccountSimple(operatingUnitId)

        For Each electronicPayrollId In electronicPayrollIds
            Dim electronicPayroll = _electronicPayrollRepository.GetElectronicPayrollById(electronicPayrollId)
            If {0, 1, 2, 4, 66, 88, 99}.Contains(electronicPayroll.Status) Then
                'Si es un estado erroneo o invalido cambia a estado registrado
                If {0, 4, 66, 88, 99}.Contains(electronicPayroll.Status) Then
                    electronicPayroll.Status = 1
                End If

                'UnitWorks
                Dim electronicPayrollUnitWork = _electronicPayrollRepository.UnitWork
                Dim electronicPayrollDetailUnitWork = _electronicPayrollDetailRepository.UnitWork
                Dim electronicPayrollNotificationUnitWork = _electronicPayrollNotificationRepository.UnitWork

                Dim currentStatus = IIf({88, 66}.Contains(electronicPayroll.Status), 1, IIf(electronicPayroll.Status = 99, 2, electronicPayroll.Status))
                electronicPayroll.Retry = electronicPayroll.Retry + 1
                electronicPayroll.Status = IIf(electronicPayroll.Status = 1, 88, IIf(electronicPayroll.Status = 2, 99, electronicPayroll.Status)) 'En proceso
                electronicPayrollUnitWork.Commit()

                Try
                    'Cargamos el proveedor
                    If Not dictionarySupplierThirdParty.ContainsKey(settingsAccount.IdDian) Then
                        supplierThirdParty = _thirdPartyRepository.GetThirdPartyById(settingsAccount.IdDian, False)
                        dictionarySupplierThirdParty.Add(settingsAccount.IdDian, supplierThirdParty)
                    Else
                        supplierThirdParty = dictionarySupplierThirdParty(settingsAccount.IdDian)
                    End If

                    'Cargamos el cliente
                    If Not dictionaryEmployeeThirdParty.ContainsKey(electronicPayroll.EmployeePartyId) Then
                        employeeThirdParty = _thirdPartyRepository.GetThirdPartyById(electronicPayroll.EmployeePartyId, False)
                        dictionaryEmployeeThirdParty.Add(electronicPayroll.EmployeePartyId, employeeThirdParty)
                    Else
                        employeeThirdParty = dictionaryEmployeeThirdParty(electronicPayroll.EmployeePartyId)
                    End If

                    'Si esta en estado registrado, generamos XML y Enviamos a la DIAN
                    If currentStatus = 1 Then
                        'Si no se ha definido una ruta de archivo la generamos
                        If String.IsNullOrEmpty(electronicPayroll.FilePath) Then
                            electronicPayroll.FilePath = System.IO.Path.Combine(
                                Utils.GetPathElectronicDocuments(),
                                ServerSessionValues.Current.CurrentContainer,
                                electronicPayroll.Year,
                                electronicPayroll.Month,
                                electronicPayroll.getDocumentTypeName(),
                                electronicPayroll.Prefix,
                                electronicPayroll.DocumentNumber
                            )
                        End If

                        'Validamos la factura antes de realizar el envío
                        currentStatus = Await Me.ValidateDIAN(electronicPayroll, settingsAccount, supplierThirdParty, employeeThirdParty, currentStatus, True)
                        If currentStatus <> 3 Then
                            'Generamos XML
                            Dim response = Me.GenerateXML(electronicPayroll, settingsAccount, supplierThirdParty, employeeThirdParty)
                            If response.StateResult Then
                                currentStatus = Me.SendToDIAN(settingsAccount, supplierThirdParty, employeeThirdParty, electronicPayroll, response.ObjectEmbbeded, currentStatus)
                            Else
                                currentStatus = IIf(response.StateResultAux, currentStatus, 0)

                                'La factura no es valida, deben revisarse los detalles
                                _electronicPayrollDetailRepository.SaveEntity(New ElectronicPayrollDetail With
                                {
                                    .ElectronicPayrollId = electronicPayroll.Id,
                                    .Destination = 0, 'Validate
                                    .CreationDate = DateTime.Now,
                                    .Status = False,
                                    .Response = Enums.ElectronicDocuments.StatusCode.BadRequest,
                                    .Comments = "Errores de validacion",
                                    .ResponseData = response.Message
                                })
                            End If
                        End If
                    End If

                    'Si ya se envio a la DIAN pero no se ha validado
                    If currentStatus = 2 Then
                        'Validamos el envio realizado a la DIAN
                        currentStatus = Await Me.ValidateDIAN(electronicPayroll, settingsAccount, supplierThirdParty, employeeThirdParty, currentStatus)
                    End If
                Catch ex As Exception
                    'Si ocurre un error en el proceso almacenamos el error
                    _electronicPayrollDetailRepository.SaveEntity(New ElectronicPayrollDetail With
                    {
                        .ElectronicPayrollId = electronicPayroll.Id,
                        .Destination = 0, 'Error en el proceso
                        .CreationDate = DateTime.Now,
                        .Status = False,
                        .Response = Enums.ElectronicDocuments.StatusCode.NotImplemented,
                        .Comments = "Error ejecutando el proceso",
                        .ResponseData = Utils.GetInnerExceptionMessageToString(ex)
                    })
                End Try

                If currentStatus = 2 Then
                    errors.AppendLine(String.Format("{0} con código {1} se encuentra en proceso de validación", electronicPayroll.getDocumentTypeName(), electronicPayroll.GetDocumentNumber()))
                ElseIf currentStatus = 3 Then
                    messages.AppendLine(String.Format("{0} con código {1} fue validado exitosamente", electronicPayroll.getDocumentTypeName(), electronicPayroll.GetDocumentNumber()))
                Else
                    errors.AppendLine(String.Format("{0} con código {1} no fue validada", electronicPayroll.getDocumentTypeName(), electronicPayroll.GetDocumentNumber()))
                End If

                electronicPayroll.Status = currentStatus
                _electronicPayrollRepository.SaveEntity(electronicPayroll)

                electronicPayrollUnitWork.Commit()
                electronicPayrollDetailUnitWork.Commit()
                electronicPayrollNotificationUnitWork.Commit()
            End If
        Next

        Return New ActionResult(Of String) With {.StateResult = (messages.Length = 0), .ObjectEmbbeded = messages.ToString(), .Message = errors.ToString()}
    End Function

    Private Function GenerateXML(electronicPayroll As ElectronicPayroll, settingsAccount As GeneralLedgerSettings, supplierThirdParty As Domain.Entities.ThirdParty, employeeThirdParty As Domain.Entities.ThirdParty) As ActionResult(Of String)
        Try
            Dim errors As New StringBuilder

            If supplierThirdParty.Person.IdentificationNumber <> supplierThirdParty.Nit Then
                errors.AppendLine("El numero de documento de tercero '" & supplierThirdParty.Nit & "' no corresponde con el numero de documento de la persona asociada '" & supplierThirdParty.Person.IdentificationNumber & "'.")
            End If

            If employeeThirdParty.Person.IdentificationNumber <> employeeThirdParty.Nit Then
                errors.AppendLine("El numero de documento de tercero '" & employeeThirdParty.Nit & "' no corresponde con el numero de documento de la persona asociada '" & employeeThirdParty.Person.IdentificationNumber & "'.")
            End If

            If (supplierThirdParty.Person.Address Is Nothing OrElse supplierThirdParty.Person.Address.Count = 0) Then
                errors.AppendLine("El tercero '" & supplierThirdParty.Person.IdentificationNumber & "' no tiene parametrizada una dirección.")
            ElseIf (supplierThirdParty.Person.Address.Where(Function(a) a.DepartmentId IsNot Nothing AndAlso a.CityId IsNot Nothing).Count = 0) Then
                errors.AppendLine("El tercero '" & supplierThirdParty.Person.IdentificationNumber & "' no tiene parametrizada una dirección con ciudad y departamento.")
            End If

            If (employeeThirdParty.Person.Address Is Nothing OrElse employeeThirdParty.Person.Address.Count = 0) Then
                errors.AppendLine("El tercero '" & employeeThirdParty.Person.IdentificationNumber & "' no tiene parametrizada una dirección.")
            ElseIf (employeeThirdParty.Person.Address.Where(Function(a) a.DepartmentId IsNot Nothing AndAlso a.CityId IsNot Nothing).Count = 0) Then
                errors.AppendLine("El tercero '" & employeeThirdParty.Person.IdentificationNumber & "' no tiene parametrizada una dirección con ciudad y departamento.")
            End If

            If errors.Length > 0 Then
                Return New ActionResult(Of String) With {.StateResult = False, .Message = errors.ToString()}
            End If

            If electronicPayroll.getDocumentType() = Infrastructure.CrossCutting.Root.TypeElectronicDocument.NominaIndividual Then
                'Obtengo el documento soporte
                electronicPayroll.ElectronicPayrollPaymentSupport = _electronicPayrollPaymentSupportRepository.GetElectronicPayrollPaymentSupportById(electronicPayroll.EntityId)
                If electronicPayroll.ElectronicPayrollPaymentSupport Is Nothing OrElse electronicPayroll.ElectronicPayrollPaymentSupport.Id = 0 Then
                    errors.AppendLine("El Documento Soporte de Nomina Electrónica no fue encontrado")
                    Return New ActionResult(Of String) With {.StateResult = False, .Message = errors.ToString()}
                End If

                'Obtengo mas informacion del documento soporte
                electronicPayroll.ElectronicPayrollPaymentSupport.MoreInformation = _electronicPayrollPaymentSupportRepository.GetElectronicPayrollPaymentSupport(electronicPayroll.EntityId)
                If electronicPayroll.ElectronicPayrollPaymentSupport.MoreInformation Is Nothing Then
                    errors.AppendLine("No se encontró información adicional del Documento Soporte de Nomina Electrónica")
                    Return New ActionResult(Of String) With {.StateResult = False, .Message = errors.ToString()}
                End If
                If electronicPayroll.ElectronicPayrollPaymentSupport.MoreInformation.StateResult = False Then
                    errors.AppendLine(electronicPayroll.ElectronicPayrollPaymentSupport.MoreInformation.MessageResult)
                    Return New ActionResult(Of String) With {.StateResult = False, .Message = errors.ToString()}
                End If

                'Obtengo los detalles del documento soporte
                electronicPayroll.ElectronicPayrollPaymentSupport.Details = _electronicPayrollPaymentSupportRepository.GetElectronicPayrollPaymentSupportDetails(electronicPayroll.EntityId)
                If electronicPayroll.ElectronicPayrollPaymentSupport.Details Is Nothing OrElse electronicPayroll.ElectronicPayrollPaymentSupport.Details.Count = 0 Then
                    errors.AppendLine("No se encontró información adicional del Documento Soporte de Nomina Electrónica")
                    Return New ActionResult(Of String) With {.StateResult = False, .Message = errors.ToString()}
                End If
                If electronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) d.StateResult = False) Then
                    For Each detail In electronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) d.StateResult = False)
                        errors.AppendLine(detail.MessageResult)
                    Next
                    Return New ActionResult(Of String) With {.StateResult = False, .Message = errors.ToString()}
                End If

                electronicPayroll.ValDev = electronicPayroll.ElectronicPayrollPaymentSupport.getAcrualValue()
                electronicPayroll.ValDed = electronicPayroll.ElectronicPayrollPaymentSupport.getDeductionValue()
                electronicPayroll.ValTolNE = electronicPayroll.ElectronicPayrollPaymentSupport.getTotalValue()
                electronicPayroll.NitNE = supplierThirdParty.Person.IdentificationNumber
                electronicPayroll.DocEmp = employeeThirdParty.Person.IdentificationNumber
                electronicPayroll.SoftwarePin = settingsAccount.ElectronicPayrollPin
                electronicPayroll.Environment = settingsAccount.ElectronicPayrollEnvironment

                If electronicPayroll.ElectronicPayrollPaymentSupport.CUNE <> electronicPayroll.getCUNE() Then
                    electronicPayroll.ElectronicPayrollPaymentSupport.CUNE = electronicPayroll.getCUNE()
                    electronicPayroll.ElectronicPayrollPaymentSupport.QR = electronicPayroll.GetQRCode()
                    electronicPayroll.CUNE = electronicPayroll.ElectronicPayrollPaymentSupport.CUNE

                    If electronicPayroll.Status <> 3 Then
                        _electronicPayrollPaymentSupportRepository.SaveEntity(electronicPayroll.ElectronicPayrollPaymentSupport)
                        _electronicPayrollPaymentSupportRepository.UnitWork.Commit()
                    End If
                End If
            ElseIf electronicPayroll.getDocumentType() = Infrastructure.CrossCutting.Root.TypeElectronicDocument.NominaIndividualDeAjuste Then
                'Obtengo el documento soporte
                electronicPayroll.NoteAdjustmentElectronicPayroll = _electronicPayrollRepository.GetElectronicPayrollById(electronicPayroll.EntityId, False)

                electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport = _electronicPayrollPaymentSupportRepository.GetElectronicPayrollPaymentSupportById(electronicPayroll.NoteAdjustmentElectronicPayroll.EntityId)
                If electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport Is Nothing OrElse electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Id = 0 Then
                    errors.AppendLine("El Documento Soporte de Nomina Electrónica no fue encontrado")
                    Return New ActionResult(Of String) With {.StateResult = False, .Message = errors.ToString()}
                End If

                'Obtengo mas informacion del documento soporte
                electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.MoreInformation = _electronicPayrollPaymentSupportRepository.GetElectronicPayrollPaymentSupport(electronicPayroll.NoteAdjustmentElectronicPayroll.EntityId)
                If electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.MoreInformation Is Nothing Then
                    errors.AppendLine("No se encontró información adicional del Documento Soporte de Nomina Electrónica")
                    Return New ActionResult(Of String) With {.StateResult = False, .Message = errors.ToString()}
                End If
                If electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.MoreInformation.StateResult = False Then
                    errors.AppendLine(electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.MoreInformation.MessageResult)
                    Return New ActionResult(Of String) With {.StateResult = False, .Message = errors.ToString()}
                End If

                'Obtengo los detalles del documento soporte
                electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details = _electronicPayrollPaymentSupportRepository.GetElectronicPayrollPaymentSupportDetails(electronicPayroll.NoteAdjustmentElectronicPayroll.EntityId)
                If electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details Is Nothing OrElse electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Count = 0 Then
                    errors.AppendLine("No se encontró información adicional del Documento Soporte de Nomina Electrónica")
                    Return New ActionResult(Of String) With {.StateResult = False, .Message = errors.ToString()}
                End If
                If electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Any(Function(d) d.StateResult = False) Then
                    For Each detail In electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.Details.Where(Function(d) d.StateResult = False)
                        errors.AppendLine(detail.MessageResult)
                    Next
                    Return New ActionResult(Of String) With {.StateResult = False, .Message = errors.ToString()}
                End If

                electronicPayroll.ValDev = electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.getAcrualValue()
                electronicPayroll.ValDed = electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.getDeductionValue()
                electronicPayroll.ValTolNE = electronicPayroll.NoteAdjustmentElectronicPayroll.ElectronicPayrollPaymentSupport.getTotalValue()
                electronicPayroll.NitNE = supplierThirdParty.Person.IdentificationNumber
                electronicPayroll.DocEmp = employeeThirdParty.Person.IdentificationNumber
                electronicPayroll.SoftwarePin = settingsAccount.ElectronicPayrollPin
                electronicPayroll.Environment = settingsAccount.ElectronicPayrollEnvironment

                If electronicPayroll.CUNE <> electronicPayroll.getCUNE() Then
                    electronicPayroll.CUNE = electronicPayroll.getCUNE()
                End If
            End If

            Dim ubl = New DIAN.UBL2_1.UBL2_1(employeeThirdParty, electronicPayroll.getDocumentType(), settingsAccount, supplierThirdParty, _storage)
            ubl.ElectronicPayroll = electronicPayroll
            Return ubl.GenerateXML()
        Catch ex As SqlClient.SqlException
            Return New ActionResult(Of String) With {.StateResult = False, .StateResultAux = Utils.ValidateSqlCodeExceptions(ex, {1205}), .Message = Utils.GetInnerExceptionMessageToString(ex)}
        Catch ex As Exception
            Return New ActionResult(Of String) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Private Function SendToDIAN(settingsAccount As GeneralLedgerSettings, supplierThirdParty As Domain.Entities.ThirdParty, employeeThirdParty As Domain.Entities.ThirdParty, electronicPayroll As ElectronicPayroll, fileName As String, currentStatus As Byte) As Byte
        'Enviamos a la DIAN
        Dim electronicPayrollDetail As New ElectronicPayrollDetail With
        {
            .ElectronicPayrollId = electronicPayroll.Id,
            .CreationDate = DateTime.Now
        }

        electronicPayrollDetail.Destination = If(settingsAccount.ElectronicPayrollEnvironment, 2, 1)
        Using client As New DistributedServices.DIAN.ServiceClient(settingsAccount.GetElectronicDocumentUrl(), settingsAccount.DigitalCertificate, settingsAccount.DigitalCertificateKey, _storage)
            Dim responseSend As ActionResult(Of DianResponse)
            If settingsAccount.ElectronicPayrollEnvironment Then
                responseSend = client.SendNomina(electronicPayroll.FilePath, fileName)
            Else
                responseSend = client.SendTestSet(settingsAccount.ElectronicPayrollTestSetId, electronicPayroll.FilePath, fileName)
            End If

            If responseSend.StateResult Then
                Dim documentResponse = responseSend.ObjectEmbbeded
                If documentResponse IsNot Nothing Then
                    Dim errorMessage As New StringBuilder

                    If settingsAccount.ElectronicPayrollEnvironment Then
                        If documentResponse.ErrorMessage IsNot Nothing AndAlso documentResponse.ErrorMessage.Any() Then
                            For Each message In documentResponse.ErrorMessage
                                errorMessage.AppendLine(message)
                            Next
                        End If

                        currentStatus = If(documentResponse.IsValid, 3, If(errorMessage.Length = 0 OrElse statusCodeErrors.Contains(documentResponse.StatusCode), currentStatus, 4))

                        Dim cuneFromDian As String = String.Empty
                        Dim cuneMatch = System.Text.RegularExpressions.Regex.Match(If(documentResponse.StatusDescription, String.Empty), "CUNE\s+([a-fA-F0-9]+)")
                        If cuneMatch.Success Then
                            cuneFromDian = cuneMatch.Groups(1).Value
                        End If

                        If errorMessage.ToString.Contains("procesado anteriormente.") AndAlso cuneFromDian = electronicPayroll.CUNE Then
                            currentStatus = 3

                            Dim fileNameElectronicDocument = electronicPayroll.GetFileName(electronicPayroll.getDocumentType())
                            _storage.DeleteFile(electronicPayroll.FilePath, fileNameElectronicDocument)
                        End If
                    Else
                        currentStatus = If(documentResponse.IsValid, 2, 4)
                        electronicPayroll.ZipKey = documentResponse.StatusMessage
                    End If

                    electronicPayroll.ShippingDate = DateTime.Now
                    electronicPayroll.ValidationDate = DateTime.Now

                    electronicPayrollDetail.Status = documentResponse.IsValid
                    electronicPayrollDetail.Response = documentResponse.StatusCode
                    electronicPayrollDetail.Comments = String.Concat(documentResponse.StatusDescription, " - ", documentResponse.StatusMessage)
                    electronicPayrollDetail.ResponseData = errorMessage.ToString()
                    _electronicPayrollDetailRepository.SaveEntity(electronicPayrollDetail)

                    Me.PostValidationEvent(settingsAccount, electronicPayroll, supplierThirdParty, employeeThirdParty, documentResponse, currentStatus)
                End If
            Else
                electronicPayroll.ShippingDate = DateTime.Now
                currentStatus = 1

                electronicPayrollDetail.Status = False
                electronicPayrollDetail.Response = Enums.ElectronicDocuments.StatusCode.ServiceUnavailable
                electronicPayrollDetail.Comments = "Error al realizar el envio"
                electronicPayrollDetail.ResponseData = responseSend.Message
                _electronicPayrollDetailRepository.SaveEntity(electronicPayrollDetail)
            End If
        End Using

        Return currentStatus
    End Function

    Private Function ValidateDIAN(electronicPayroll As ElectronicPayroll, settingsAccount As GeneralLedgerSettings, supplierThirdParty As Domain.Entities.ThirdParty, employeeThirdParty As Domain.Entities.ThirdParty, currentStatus As Byte, Optional beforeSend As Boolean = False) As Task(Of Byte)
        Using client As New DistributedServices.DIAN.ServiceClient(settingsAccount.GetElectronicDocumentUrl(), settingsAccount.DigitalCertificate, settingsAccount.DigitalCertificateKey)
            'Validamos el envio realizado a la DIAN
            Dim responseValidate As ActionResult(Of DianResponse)
            If settingsAccount.ElectronicPayrollEnvironment Then
                responseValidate = client.GetStatus(electronicPayroll.CUNE)
            Else
                responseValidate = client.GetStatusZip(electronicPayroll.ZipKey)
            End If
            If responseValidate.StateResult = True Then
                Dim documentResponse = responseValidate.ObjectEmbbeded
                If documentResponse IsNot Nothing Then
                    Dim errorMessage As New StringBuilder
                    If documentResponse.ErrorMessage IsNot Nothing AndAlso documentResponse.ErrorMessage.Any() Then
                        For Each message In documentResponse.ErrorMessage
                            errorMessage.AppendLine(message)
                        Next
                    End If

                    currentStatus = If(documentResponse.IsValid, 3, If(errorMessage.Length = 0 OrElse statusCodeErrors.Contains(documentResponse.StatusCode), currentStatus, 4))
                    If errorMessage.ToString.Contains("procesado anteriormente.") Then
                        currentStatus = 3

                        Dim fileNameElectronicDocument = electronicPayroll.GetFileName(electronicPayroll.getDocumentType())
                        Me._storage.DeleteFile(electronicPayroll.FilePath, fileNameElectronicDocument)
                    End If

                    If String.IsNullOrEmpty(documentResponse.StatusCode) OrElse documentResponse.StatusCode <> "66" Then
                        If beforeSend = False OrElse currentStatus = 3 Then
                            If beforeSend = False OrElse electronicPayroll.Status <> 3 Then
                                electronicPayroll.ValidationDate = DateTime.Now
                                Dim Comments = String.Concat(documentResponse.StatusDescription, " - ", documentResponse.StatusMessage)
                                If Comments.Length > 250 Then
                                    Comments = Comments.Substring(0, 250)
                                End If
                                _electronicPayrollDetailRepository.SaveEntity(New ElectronicPayrollDetail With
                                    {
                                        .ElectronicPayrollId = electronicPayroll.Id,
                                        .Destination = 2, 'Validate To DIAN
                                        .CreationDate = DateTime.Now,
                                        .Status = documentResponse.IsValid,
                                        .Response = documentResponse.StatusCode,
                                        .Comments = Comments,
                                        .ResponseData = errorMessage.ToString()
                                    })
                            End If
                            Me.PostValidationEvent(settingsAccount, electronicPayroll, supplierThirdParty, employeeThirdParty, documentResponse, currentStatus)
                        End If
                    End If
                End If
            End If
        End Using

        Return Task.FromResult(Of Byte)(currentStatus)
    End Function

    Private Sub PostValidationEvent(settingsAccount As GeneralLedgerSettings, electronicPayroll As ElectronicPayroll, supplierThirdParty As Domain.Entities.ThirdParty, customerThirdParty As Domain.Entities.ThirdParty, documentResponse As DianResponse, currentStatus As Byte)
        Try
            If currentStatus = 3 Then
                'Guardamos el ApplicationResponse
                Dim fileNameApplicationResponse = electronicPayroll.GetFileName(Infrastructure.CrossCutting.Root.TypeElectronicDocument.ApplicationResponse)

                If Me._storage.ValidateIfNotExists(electronicPayroll.FilePath, fileNameApplicationResponse) Then
                    Me._storage.WriteFile(electronicPayroll.FilePath, fileNameApplicationResponse, documentResponse.XmlBase64Bytes)
                End If

                If electronicPayroll.Status <> 3 AndAlso settingsAccount.ElectronicPayrollEnvironment Then
                    'Guardamos los registros a los detalles a los cuales se notificaran
                    If customerThirdParty.Person.Email IsNot Nothing AndAlso customerThirdParty.Person.Email.Any(Function(e) e.Type = 2) Then
                        For Each email In customerThirdParty.Person.Email.Where(Function(e) e.Type = 2)
                            If email.IsValidEmailFormat() Then
                                _electronicPayrollNotificationRepository.SaveEntity(New ElectronicPayrollNotification With
                                {
                                    .ElectronicPayrollId = electronicPayroll.Id,
                                    .Email = email.Email1,
                                    .Status = 0,
                                    .CreationDate = DateTime.Now
                                })
                            End If
                        Next
                    End If
                End If
            End If
        Catch ex As Exception
            'Si ocurre un error en el proceso almacenamos el error
            _electronicPayrollDetailRepository.SaveEntity(New ElectronicPayrollDetail With
            {
                .ElectronicPayrollId = electronicPayroll.Id,
                .Destination = 3, 'Generación datos para el envío 
                .CreationDate = DateTime.Now,
                .Status = False,
                .Response = Enums.ElectronicDocuments.StatusCode.NotImplemented,
                .Comments = "Error ejecutando el proceso de postvalidación",
                .ResponseData = Utils.GetInnerExceptionMessageToString(ex)
            })
        End Try
    End Sub

#End Region

#Region "Email"

    Private Function ExecuteSendMailProcessAsync() As Task(Of ActionResult(Of String))
        Return Task.FromResult(Of ActionResult(Of String))(New ActionResult(Of String))
    End Function

#End Region

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            _electronicPayrollRepository = Nothing
            _electronicPayrollDetailRepository = Nothing
            _electronicPayrollNotificationRepository = Nothing
            _thirdPartyRepository = Nothing
            _settingsAccountRepository = Nothing

            _electronicPayrollPaymentSupportRepository = Nothing
        End If
        disposedValue = True
    End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
    End Sub
#End Region

End Class
