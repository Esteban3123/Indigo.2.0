'***********************************************************************
' Assembly         : Domain.Payroll.Entities
' Author           : Jhon Willian Corredor Araujo
' Created          : 03-09-2026
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base.Entities
Imports Domain.Entities

Public Class EmployeeAuditDomain

    Implements IEmployeeAuditDomain

#Region "Descriptor"

    ''' <summary>
    ''' Describe un campo auditable. El valor crudo decide si hubo cambio; el valor visible
    ''' es el que se persiste, resuelto contra el catálogo cuando el campo es una referencia.
    ''' Los códigos sin catálogo se guardan crudos y los traduce quien los muestra.
    ''' </summary>
    Private NotInheritable Class AuditField(Of TEntity)

        Public ReadOnly FieldName As String
        Public ReadOnly Label As String
        Public ReadOnly RawValue As Func(Of TEntity, String)
        Public ReadOnly Catalog As AuditCatalog?
        Public ReadOnly Display As Func(Of TEntity, String)

        Public Sub New(fieldName As String,
                       label As String,
                       rawValue As Func(Of TEntity, String),
                       Optional catalog As AuditCatalog? = Nothing,
                       Optional display As Func(Of TEntity, String) = Nothing)

            Me.FieldName = fieldName
            Me.Label = label
            Me.RawValue = rawValue
            Me.Catalog = catalog
            Me.Display = display

        End Sub

    End Class

#End Region

#Region "Campos auditables"

    Private Shared ReadOnly _contractFields As List(Of AuditField(Of Contract)) = BuildContractFields()
    Private Shared ReadOnly _employeeFields As List(Of AuditField(Of Employee)) = BuildEmployeeFields()
    Private Shared ReadOnly _personFields As List(Of AuditField(Of Person)) = BuildPersonFields()

    ''' <summary>
    ''' Campos del contrato que se modifican sobre el registro vigente sin generar novedad.
    ''' Cargo, salario básico y fechas quedan fuera: producen un contrato nuevo (RowType = 2)
    ''' y se reflejan en el histórico de cambios contractuales.
    ''' </summary>
    Private Shared Function BuildContractFields() As List(Of AuditField(Of Contract))
        Return New List(Of AuditField(Of Contract)) From {
            New AuditField(Of Contract)("FunctionalUnitId", "Unidad Funcional", Function(c) AsText(c.FunctionalUnitId), catalog:=AuditCatalog.FunctionalUnit),
            New AuditField(Of Contract)("GroupId", "Grupo", Function(c) AsText(c.GroupId), catalog:=AuditCatalog.Group),
            New AuditField(Of Contract)("ContractTypeId", "Tipo de Contrato", Function(c) AsText(c.ContractTypeId), catalog:=AuditCatalog.ContractType),
            New AuditField(Of Contract)("OrganizationChartPositionId", "Cargo del Organigrama", Function(c) AsText(c.OrganizationChartPositionId), catalog:=AuditCatalog.OrganizationChartPosition),
            New AuditField(Of Contract)("ContractModificationReasonId", "Motivo de Modificación", Function(c) AsText(c.ContractModificationReasonId), catalog:=AuditCatalog.ContractModificationReason),
            New AuditField(Of Contract)("BankId", "Banco", Function(c) AsText(c.BankId), catalog:=AuditCatalog.Bank),
            New AuditField(Of Contract)("BankAccountNumber", "Número de Cuenta", Function(c) AsText(c.BankAccountNumber)),
            New AuditField(Of Contract)("BankAccountType", "Tipo de Cuenta", Function(c) AsText(c.BankAccountType)),
            New AuditField(Of Contract)("PaymentPeriod", "Periodo de Pago", Function(c) AsText(c.PaymentPeriod)),
            New AuditField(Of Contract)("PaymentType", "Tipo de Pago", Function(c) AsText(c.PaymentType)),
            New AuditField(Of Contract)("HoursDaily", "Horas Diarias", Function(c) AsText(c.HoursDaily)),
            New AuditField(Of Contract)("WorkPlaceId", "Sede de Trabajo", Function(c) AsText(c.WorkPlaceId)),
            New AuditField(Of Contract)("TypeOfPensionContribution", "Tipo de Cotización a Pensión", Function(c) AsText(c.TypeOfPensionContribution)),
            New AuditField(Of Contract)("TrialPeriod", "Periodo de Prueba", Function(c) AsText(c.TrialPeriod)),
            New AuditField(Of Contract)("TrialPeriodTime", "Duración del Periodo de Prueba", Function(c) AsText(c.TrialPeriodTime)),
            New AuditField(Of Contract)("TrialPeriodSalaryPercentage", "Porcentaje de Salario en Periodo de Prueba", Function(c) AsText(c.TrialPeriodSalaryPercentage)),
            New AuditField(Of Contract)("LiquidationPayroll", "Liquida Nómina", Function(c) AsText(c.LiquidationPayroll)),
            New AuditField(Of Contract)("Contingency", "Contingencia", Function(c) AsText(c.Contingency)),
            New AuditField(Of Contract)("BaseIncome", "Ingreso Base", Function(c) AsText(c.BaseIncome)),
            New AuditField(Of Contract)("IncomeDailyBase", "Ingreso Base Diario", Function(c) AsText(c.IncomeDailyBase)),
            New AuditField(Of Contract)("ResolutionNumber", "Número de Resolución", Function(c) AsText(c.ResolutionNumber)),
            New AuditField(Of Contract)("ResolutionDate", "Fecha de Resolución", Function(c) AsText(c.ResolutionDate)),
            New AuditField(Of Contract)("CertificateOfficeNumber", "Número de Acta de Posesión", Function(c) AsText(c.CertificateOfficeNumber)),
            New AuditField(Of Contract)("PosesionDate", "Fecha de Posesión", Function(c) AsText(c.PosesionDate)),
            New AuditField(Of Contract)("ParameterACCAI", "Parámetro ACCAI", Function(c) AsText(c.ParameterACCAI)),
            New AuditField(Of Contract)("InsuredCCSSCode", "Código de Asegurado CCSS", Function(c) AsText(c.InsuredCCSSCode)),
            New AuditField(Of Contract)("Notes", "Observaciones del Contrato", Function(c) AsText(c.Notes))
        }
    End Function

    ''' <summary>
    ''' Campos propios del empleado editables desde el formulario de Talento Humano.
    ''' </summary>
    Private Shared Function BuildEmployeeFields() As List(Of AuditField(Of Employee))
        Return New List(Of AuditField(Of Employee)) From {
            New AuditField(Of Employee)("CostCenterId", "Centro de Costo", Function(e) AsText(e.CostCenterId), catalog:=AuditCatalog.CostCenter),
            New AuditField(Of Employee)("WorkCenterId", "Centro de Trabajo", Function(e) AsText(e.WorkCenterId), catalog:=AuditCatalog.WorkCenter),
            New AuditField(Of Employee)("EmployeeTypeId", "Tipo de Empleado", Function(e) AsText(e.EmployeeTypeId), catalog:=AuditCatalog.EmployeeType),
            New AuditField(Of Employee)("PensionaryTypeId", "Tipo de Pensionado", Function(e) AsText(e.PensionaryTypeId), catalog:=AuditCatalog.PensionaryType),
            New AuditField(Of Employee)("ContributorTypeSubtypeId", "Subtipo de Cotizante", Function(e) AsText(e.ContributorTypeSubtypeId), catalog:=AuditCatalog.ContributorSubtype),
            New AuditField(Of Employee)("AdmissionDate", "Fecha de Ingreso", Function(e) AsText(e.AdmissionDate)),
            New AuditField(Of Employee)("State", "Estado del Empleado", Function(e) AsText(e.State), display:=Function(e) If(e.State, "Activo", "Inactivo")),
            New AuditField(Of Employee)("Pensionary", "Pensionado", Function(e) AsText(e.Pensionary)),
            New AuditField(Of Employee)("PensionaryStatus", "Estado de Pensionado", Function(e) AsText(e.PensionaryStatus)),
            New AuditField(Of Employee)("RetiredForeign", "Pensionado del Exterior", Function(e) AsText(e.RetiredForeign)),
            New AuditField(Of Employee)("ForeignobligedQuotePension", "Extranjero Obligado a Cotizar Pensión", Function(e) AsText(e.ForeignobligedQuotePension)),
            New AuditField(Of Employee)("ContributorAbroad", "Cotizante en el Exterior", Function(e) AsText(e.ContributorAbroad)),
            New AuditField(Of Employee)("DateFilingAbroad", "Fecha de Radicación en el Exterior", Function(e) AsText(e.DateFilingAbroad)),
            New AuditField(Of Employee)("SupplementaryPension", "Pensión Complementaria", Function(e) AsText(e.SupplementaryPension)),
            New AuditField(Of Employee)("TradeUnion", "Sindicato", Function(e) AsText(e.TradeUnion)),
            New AuditField(Of Employee)("HousingDeductionValue", "Deducción de Vivienda", Function(e) AsText(e.HousingDeductionValue)),
            New AuditField(Of Employee)("EducationDeductionValue", "Deducción de Educación", Function(e) AsText(e.EducationDeductionValue)),
            New AuditField(Of Employee)("ProfessionalRiskPercentage", "Porcentaje de Riesgo Profesional", Function(e) AsText(e.ProfessionalRiskPercentage)),
            New AuditField(Of Employee)("AverageYearHealth", "Promedio Anual de Salud", Function(e) AsText(e.AverageYearHealth)),
            New AuditField(Of Employee)("DeclarantType", "Tipo de Declarante", Function(e) AsText(e.DeclarantType)),
            New AuditField(Of Employee)("ProcedureTypeRTF", "Procedimiento de Retención en la Fuente", Function(e) AsText(e.ProcedureTypeRTF)),
            New AuditField(Of Employee)("HealthContributorRTF", "Aporte a Salud para Retención", Function(e) AsText(e.HealthContributorRTF)),
            New AuditField(Of Employee)("PermanentInability", "Incapacidad Permanente", Function(e) AsText(e.PermanentInability)),
            New AuditField(Of Employee)("InitialDatePermanentInability", "Inicio de Incapacidad Permanente", Function(e) AsText(e.InitialDatePermanentInability)),
            New AuditField(Of Employee)("EndDatePermanentInability", "Fin de Incapacidad Permanente", Function(e) AsText(e.EndDatePermanentInability)),
            New AuditField(Of Employee)("Relocation", "Reubicación", Function(e) AsText(e.Relocation)),
            New AuditField(Of Employee)("AllowJobReference", "Permite Referencia Laboral", Function(e) AsText(e.AllowJobReference)),
            New AuditField(Of Employee)("InternalCode", "Código Interno", Function(e) AsText(e.InternalCode)),
            New AuditField(Of Employee)("RemainingVacationDays", "Días de Vacaciones Pendientes", Function(e) AsText(e.RemainingVacationDays)),
            New AuditField(Of Employee)("VacationLastDateLiquidation", "Última Liquidación de Vacaciones", Function(e) AsText(e.VacationLastDateLiquidation))
        }
    End Function

    ''' <summary>
    ''' Datos personales del empleado, editables en las secciones de identificación e información personal.
    ''' </summary>
    Private Shared Function BuildPersonFields() As List(Of AuditField(Of Person))
        Return New List(Of AuditField(Of Person)) From {
            New AuditField(Of Person)("IdentificationTypeId", "Tipo de Identificación", Function(p) AsText(p.IdentificationTypeId)),
            New AuditField(Of Person)("IdentificationNumber", "Número de Identificación", Function(p) AsText(p.IdentificationNumber)),
            New AuditField(Of Person)("IdentificacionCityId", "Ciudad de Expedición del Documento", Function(p) AsText(p.IdentificacionCityId), catalog:=AuditCatalog.City),
            New AuditField(Of Person)("IdentificationExpeditionDate", "Fecha de Expedición del Documento", Function(p) AsText(p.IdentificationExpeditionDate)),
            New AuditField(Of Person)("FirstName", "Primer Nombre", Function(p) AsText(p.FirstName)),
            New AuditField(Of Person)("SecondName", "Segundo Nombre", Function(p) AsText(p.SecondName)),
            New AuditField(Of Person)("FirstLastName", "Primer Apellido", Function(p) AsText(p.FirstLastName)),
            New AuditField(Of Person)("SecondLastName", "Segundo Apellido", Function(p) AsText(p.SecondLastName)),
            New AuditField(Of Person)("BirthDate", "Fecha de Nacimiento", Function(p) AsText(p.BirthDate)),
            New AuditField(Of Person)("BirthCityId", "Ciudad de Nacimiento", Function(p) AsText(p.BirthCityId), catalog:=AuditCatalog.City),
            New AuditField(Of Person)("DeathDate", "Fecha de Fallecimiento", Function(p) AsText(p.DeathDate)),
            New AuditField(Of Person)("Gender", "Género", Function(p) AsText(p.Gender)),
            New AuditField(Of Person)("MaritalStatus", "Estado Civil", Function(p) AsText(p.MaritalStatus)),
            New AuditField(Of Person)("BloodGroup", "Grupo Sanguíneo", Function(p) AsText(p.BloodGroup)),
            New AuditField(Of Person)("RH", "Factor RH", Function(p) AsText(p.RH)),
            New AuditField(Of Person)("SonNumber", "Número de Hijos", Function(p) AsText(p.SonNumber)),
            New AuditField(Of Person)("Dependents", "Personas a Cargo", Function(p) AsText(p.Dependents)),
            New AuditField(Of Person)("MilitaryCardId", "Tipo de Libreta Militar", Function(p) AsText(p.MilitaryCardId)),
            New AuditField(Of Person)("MilitaryCardNumber", "Número de Libreta Militar", Function(p) AsText(p.MilitaryCardNumber)),
            New AuditField(Of Person)("HousingType", "Tipo de Vivienda", Function(p) AsText(p.HousingType)),
            New AuditField(Of Person)("SocioEconomicStatus", "Estrato Socioeconómico", Function(p) AsText(p.SocioEconomicStatus)),
            New AuditField(Of Person)("EthnicGroupId", "Grupo Étnico", Function(p) AsText(p.EthnicGroupId), catalog:=AuditCatalog.EthnicGroups),
            New AuditField(Of Person)("ReligiousBeliefsId", "Creencia Religiosa", Function(p) AsText(p.ReligiousBeliefsId), catalog:=AuditCatalog.ReligiousBeliefs),
            New AuditField(Of Person)("CigaretteConsumption", "Consumo de Cigarrillo", Function(p) AsText(p.CigaretteConsumption)),
            New AuditField(Of Person)("SportPractice", "Práctica Deportiva", Function(p) AsText(p.SportPractice)),
            New AuditField(Of Person)("Weight", "Peso", Function(p) AsText(p.Weight)),
            New AuditField(Of Person)("ShirtSize", "Talla de Camisa", Function(p) AsText(p.ShirtSize)),
            New AuditField(Of Person)("PantSize", "Talla de Pantalón", Function(p) AsText(p.PantSize)),
            New AuditField(Of Person)("ShoeSize", "Talla de Calzado", Function(p) AsText(p.ShoeSize)),
            New AuditField(Of Person)("State", "Estado de la Persona", Function(p) AsText(p.State), display:=Function(p) If(p.State, "Activo", "Inactivo"))
        }
    End Function

#End Region

#Region "Construccion de la bitacora"

    Public Function BuildAuditEntries(originalEmployee As Employee,
                                      modifiedEmployee As Employee,
                                      userCode As String,
                                      anchorContractId As Integer,
                                      catalogResolver As IAuditCatalogResolver) As List(Of ContractAudit) Implements IEmployeeAuditDomain.BuildAuditEntries

        Dim entries As New List(Of ContractAudit)

        If originalEmployee Is Nothing OrElse modifiedEmployee Is Nothing OrElse anchorContractId <= 0 Then
            Return entries
        End If

        Dim changeDate As Date = Date.Now

        If originalEmployee.Contract IsNot Nothing AndAlso modifiedEmployee.Contract IsNot Nothing Then
            For Each originalContract As Contract In originalEmployee.Contract
                Dim modifiedContract = modifiedEmployee.Contract.FirstOrDefault(Function(c) c.Id = originalContract.Id)
                entries.AddRange(Compare(_contractFields, originalContract, modifiedContract, originalContract.Id, userCode, changeDate, catalogResolver))
            Next
        End If

        entries.AddRange(Compare(_employeeFields, originalEmployee, modifiedEmployee, anchorContractId, userCode, changeDate, catalogResolver))
        entries.AddRange(Compare(_personFields, GetPerson(originalEmployee), GetPerson(modifiedEmployee), anchorContractId, userCode, changeDate, catalogResolver))
        entries.AddRange(CompareContactData(modifiedEmployee, anchorContractId, userCode, changeDate, catalogResolver))

        Return entries

    End Function

    Private Shared Function Compare(Of TEntity)(fields As List(Of AuditField(Of TEntity)),
                                                original As TEntity,
                                                modified As TEntity,
                                                contractId As Integer,
                                                userCode As String,
                                                changeDate As Date,
                                                catalogResolver As IAuditCatalogResolver) As List(Of ContractAudit)

        Dim entries As New List(Of ContractAudit)

        If original Is Nothing OrElse modified Is Nothing OrElse contractId <= 0 Then
            Return entries
        End If

        For Each field As AuditField(Of TEntity) In fields

            If String.Equals(field.RawValue(original), field.RawValue(modified), StringComparison.Ordinal) Then
                Continue For
            End If

            entries.Add(NewEntry(contractId,
                                 field.Label,
                                 field.FieldName,
                                 DisplayOf(field, original, catalogResolver),
                                 DisplayOf(field, modified, catalogResolver),
                                 userCode,
                                 changeDate))

        Next

        Return entries

    End Function

    ''' <summary>
    ''' Texto que se persiste: el propio del descriptor, el nombre del catálogo cuando el
    ''' campo es una referencia, o el valor crudo si el catálogo no resuelve.
    ''' </summary>
    Private Shared Function DisplayOf(Of TEntity)(field As AuditField(Of TEntity),
                                                  entity As TEntity,
                                                  catalogResolver As IAuditCatalogResolver) As String

        If field.Display IsNot Nothing Then
            Return field.Display(entity)
        End If

        Dim rawValue As String = field.RawValue(entity)

        If field.Catalog.HasValue AndAlso catalogResolver IsNot Nothing Then

            Dim id As Integer

            If Integer.TryParse(rawValue, id) Then
                Dim catalogName As String = catalogResolver.GetName(field.Catalog.Value, id)
                If Not String.IsNullOrWhiteSpace(catalogName) Then
                    Return catalogName
                End If
            End If

        End If

        Return rawValue

    End Function

#End Region

#Region "Datos de contacto"

    ''' <summary>
    ''' Altas, bajas y modificaciones de direcciones, teléfonos y correos. El elemento que
    ''' no viene marcado como cambiado se ignora: la foto original del empleado no carga
    ''' estas colecciones y compararlas en bloque daría de alta todo en cada guardado.
    ''' </summary>
    Private Shared Function CompareContactData(modifiedEmployee As Employee,
                                               contractId As Integer,
                                               userCode As String,
                                               changeDate As Date,
                                               catalogResolver As IAuditCatalogResolver) As List(Of ContractAudit)

        Dim entries As New List(Of ContractAudit)
        Dim modifiedPerson As Person = GetPerson(modifiedEmployee)

        If modifiedPerson Is Nothing OrElse contractId <= 0 Then
            Return entries
        End If

        If modifiedPerson.Address IsNot Nothing Then
            For Each item As Address In modifiedPerson.Address.ToList()
                entries.AddRange(ContactEntry(item.ChangeTracker, AuditContactKind.Address, "Dirección", "Address",
                                              item.Id, AsText(item.Addresss), contractId, userCode, changeDate, catalogResolver))
            Next
        End If

        If modifiedPerson.Phone IsNot Nothing Then
            For Each item As Phone In modifiedPerson.Phone.ToList()
                entries.AddRange(ContactEntry(item.ChangeTracker, AuditContactKind.Phone, "Teléfono", "Phone",
                                              item.Id, AsText(item.Phone1), contractId, userCode, changeDate, catalogResolver))
            Next
        End If

        If modifiedPerson.Email IsNot Nothing Then
            For Each item As Email In modifiedPerson.Email.ToList()
                entries.AddRange(ContactEntry(item.ChangeTracker, AuditContactKind.Email, "Correo Electrónico", "Email",
                                              item.Id, AsText(item.Email1), contractId, userCode, changeDate, catalogResolver))
            Next
        End If

        Return entries

    End Function

    ''' <summary>
    ''' Entrada correspondiente al estado del elemento de contacto. El valor anterior se lee
    ''' de la base, que aún conserva el dato porque el guardado no se ha confirmado.
    ''' </summary>
    Private Shared Function ContactEntry(tracker As ObjectChangeTracker,
                                         kind As AuditContactKind,
                                         label As String,
                                         fieldName As String,
                                         id As Integer,
                                         currentText As String,
                                         contractId As Integer,
                                         userCode As String,
                                         changeDate As Date,
                                         catalogResolver As IAuditCatalogResolver) As List(Of ContractAudit)

        Dim entries As New List(Of ContractAudit)

        If tracker Is Nothing Then
            Return entries
        End If

        ' El formulario conserva el grafo tras guardar y reenvía los elementos con su último
        ' estado, de modo que la base decide si el cambio está pendiente o ya fue auditado.
        Dim storedText As String = StoredContactText(catalogResolver, kind, id)

        Select Case tracker.State

            Case ObjectState.Added
                If String.IsNullOrWhiteSpace(currentText) Then
                    Exit Select
                End If
                If String.Equals(storedText, currentText, StringComparison.Ordinal) Then
                    Exit Select
                End If
                entries.Add(NewEntry(contractId, label, fieldName, String.Empty, currentText, userCode, changeDate))

            Case ObjectState.Modified
                If Not String.Equals(storedText, currentText, StringComparison.Ordinal) Then
                    entries.Add(NewEntry(contractId, label, fieldName, storedText, currentText, userCode, changeDate))
                End If

            Case ObjectState.Deleted
                If Not String.IsNullOrWhiteSpace(storedText) Then
                    entries.Add(NewEntry(contractId, label, fieldName, storedText, String.Empty, userCode, changeDate))
                End If

        End Select

        Return entries

    End Function

    Private Shared Function StoredContactText(catalogResolver As IAuditCatalogResolver,
                                              kind As AuditContactKind,
                                              id As Integer) As String

        If catalogResolver Is Nothing OrElse id <= 0 Then
            Return String.Empty
        End If

        Return catalogResolver.GetContactText(kind, id)

    End Function

#End Region

#Region "Utilidades"

    Private Shared Function NewEntry(contractId As Integer,
                                     label As String,
                                     fieldName As String,
                                     valueOld As String,
                                     valueNew As String,
                                     userCode As String,
                                     changeDate As Date) As ContractAudit

        Dim entry As New ContractAudit()
        entry.ContractId = contractId
        entry.Type = label
        entry.FieldName = fieldName
        entry.ValueOld = valueOld
        entry.ValueNew = valueNew
        entry.UserCode = userCode
        entry.Date = changeDate
        entry.MarkAsAdded()

        Return entry

    End Function

    Private Shared Function GetPerson(employee As Employee) As Person

        If employee Is Nothing OrElse employee.ThirdParty Is Nothing Then
            Return Nothing
        End If

        Return employee.ThirdParty.Person

    End Function

    Private Shared Function AsText(value As Object) As String

        If value Is Nothing Then
            Return String.Empty
        End If

        If TypeOf value Is Boolean Then
            Return If(CBool(value), "Sí", "No")
        End If

        If TypeOf value Is Date Then
            Return CDate(value).ToString("dd/MM/yyyy")
        End If

        Return value.ToString().Trim()

    End Function

#End Region

End Class
