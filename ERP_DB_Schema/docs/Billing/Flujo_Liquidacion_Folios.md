# Flujo de Liquidación de Folios — Módulo de Facturación

> **Módulo:** Billing  
> **Punto de entrada:** `[Billing].[LiquidateFolio]`  
> **Autor original:** Diego A. Roldán (2015/2018)  
> **Refactorización:** Miguel Angel Fonseca Castro (2020)  
> **Fecha de análisis:** 28 de febrero de 2026  

---

## Tabla de Contenidos

1. [Arquitectura General del Flujo](#1-arquitectura-general-del-flujo)
2. [Parámetros de Entrada](#2-parámetros-de-entrada)
3. [Fase 1 — Validaciones Pre-Liquidación](#3-fase-1--validaciones-pre-liquidación)
4. [Fase 2 — Creación de la Factura](#4-fase-2--creación-de-la-factura)
5. [Fase 3 — Cuentas por Cobrar](#5-fase-3--cuentas-por-cobrar)
6. [Fase 4 — Comprobante Contable](#6-fase-4--comprobante-contable)
7. [Fase 5 — Cruce de Anticipos](#7-fase-5--cruce-de-anticipos)
8. [Fase 6 — Actualizaciones Post-Factura](#8-fase-6--actualizaciones-post-factura)
9. [Fase 7 — Cierre de Ingreso](#9-fase-7--cierre-de-ingreso)
10. [Manejo de Multimoneda](#10-manejo-de-multimoneda)
11. [Entidades Creadas y Modificadas](#11-entidades-creadas-y-modificadas)
12. [Observaciones y Riesgos](#12-observaciones-y-riesgos)
13. [Datos de Prueba de Referencia](#13-datos-de-prueba-de-referencia)

---

## 1. Arquitectura General del Flujo

El sistema posee **dos cadenas de ejecución** conviviendo en el codebase:

| Cadena | Orquestador | Estado |
|--------|-------------|--------|
| **Legacy** | `LiquidateFolio_Output` → `CreateInvoice` → `SP_CreateInvoice` | Activo pero en desuso progresivo |
| **Nuevo (2020)** | `SP_LiquidateFolio_Output` → `SP_CreateInvoice_Output` | **Flujo principal activo** |

### Cadena de llamadas del flujo nuevo

```
[Billing].[LiquidateFolio]                              ← Punto de entrada
  └─► [Billing].[SP_LiquidateFolio_Output]               ← Orquestador principal
        │
        ├─► [Billing].[SP_LiquidateFolioValidations_Output]
        │     Validaciones completas pre-guardado
        │
        ├─► FOR EACH folio in XML:
        │     └─► [Billing].[SP_CreateInvoice_Output]
        │           │
        │           ├─► [Billing].[RecalculateFolioDetailsByCurrency]
        │           │     Recálculo multimoneda de valores del folio
        │           │
        │           ├─► INSERT Billing.Invoice
        │           ├─► INSERT Billing.InvoiceDetail
        │           ├─► INSERT Billing.InvoiceDetailSurgical
        │           ├─► INSERT Billing.InvoiceTaxDevolution
        │           ├─► INSERT Billing.ElectronicDocument
        │           ├─► INSERT Billing.InvoiceCustomerRetention
        │           ├─► INSERT Billing.InvoicePortfolioAdvance
        │           ├─► INSERT Billing.ElectronicsProperties
        │           │
        │           ├─► [Billing].[SP_CreateAccountReceivables_Output]
        │           │     └─► [Billing].[SP_CreateAccountReceivable_Output]
        │           │           ├─► INSERT Portfolio.AccountReceivable
        │           │           ├─► INSERT Portfolio.AccountReceivableShare
        │           │           ├─► INSERT Portfolio.AccountReceivableAccounting
        │           │           ├─► [Portfolio].[SP_GenerateRecognitionByAccountReceivableId_Output]
        │           │           ├─► [Billing].[SP_GenerateJournalVoucherDetails]
        │           │           │     ├─► [Billing].[RecalculateFolioDetailsByCurrency]
        │           │           │     └─► [Billing].[fnJournalVoucherIncomeInvoiceDetail]
        │           │           ├─► [GeneralLedger].[SP_CreateAndValidateJournalVoucherMovement]
        │           │           └─► [Portfolio].[SP_GeneratePortfolioTransfer]
        │           │
        │           ├─► UPDATE Billing.RevenueControlDetail (Status → 2)
        │           └─► [Billing].[SP_CreatePackageJournalVoucher_Output]
        │
        ├─► [Billing].[SP_CloseAdmission_Output]          ← Cierre de ingreso
        └─► UPDATE dbo.ADFURIPSU                           ← Afectación FURIPS
```

---

## 2. Parámetros de Entrada

### 2.1 Parámetros del Stored Procedure

| Parámetro | Tipo | Descripción |
|-----------|------|-------------|
| `@PatientCode` | `VARCHAR(20)` | Código/cédula del paciente |
| `@AdmissionNumber` | `VARCHAR(20)` | Número de ingreso/admisión |
| `@ContainerCrystal` | `VARCHAR(10)` | Nombre de la base de datos HIS (Crystal/Indigo) |
| `@BillingAuthorizationId` | `INT` | ID de la autorización de facturación |
| `@OperativeUnitId` | `INT` | Unidad operativa |
| `@ThirdPartyPatientId` | `INT` | ID del tercero asociado al paciente |
| `@UserCode` | `VARCHAR(20)` | Código del usuario que ejecuta |
| `@CompanyType` | `TINYINT` | Tipo de compañía (se obtiene de auditoría) |
| `@RevenueControlDetailCrossingListXml` | `XML` | XML con los folios a liquidar y sus cruces |
| `@SkipAccountControlValidations` | `BIT` | Flag para omitir validaciones de control de cuentas |

### 2.2 Estructura del XML de Entrada

```xml
<RevenueControlDetailCrossingList>
    <FolioOrder>4</FolioOrder>
    <FolioType>3</FolioType>
    <CareGroupId>1</CareGroupId>
    <RevenueControlDetailId>267311</RevenueControlDetailId>
    <TotalPatientDiscount>0</TotalPatientDiscount>
    <RevenueControlId>138680</RevenueControlId>
    <OutputDate>18/02/2026 10:21:44</OutputDate>
    <IsCutAccount>False</IsCutAccount>
    <OutputDiagnosis>Z000</OutputDiagnosis>
    <InitialDate>18/02/2026 10:11:44</InitialDate>
    <CutType>1</CutType>
    <FilePath>C:\ProgramData\...\ElectronicDocuments\INDIGO537</FilePath>

    <ListPortfolioAdvanceCrossing>
        <Id>153332</Id>
        <Code>RC000138998</Code>
        <CrossingValue>73210.08</CrossingValue>
        <CashReceiptDetailIdTmp>0</CashReceiptDetailIdTmp>
        <PortfolioAdvanceType>2</PortfolioAdvanceType>
    </ListPortfolioAdvanceCrossing>

    <CurrencyId>1</CurrencyId>
    <TRMValue>0.00193798449612403</TRMValue>
    <TaxDevolutionValue>0</TaxDevolutionValue>
    <ConditionSalesId>1</ConditionSalesId>
    <EconomicActivityId></EconomicActivityId>
</RevenueControlDetailCrossingList>
```

### 2.3 Campos del XML — Referencia

| Campo | Tipo | Descripción |
|-------|------|-------------|
| `FolioOrder` | `TINYINT` | Orden del folio dentro de la admisión |
| `FolioType` | `TINYINT` | Tipo de folio: 1=EAPB c/contrato, 2=EAPB s/contrato, 3=Particular, 4=Aseguradoras, 5=Capitación |
| `CareGroupId` | `INT` | Grupo de atención asociado |
| `RevenueControlDetailId` | `INT` | ID del detalle del control de ingresos (folio) |
| `TotalPatientDiscount` | `DECIMAL(18,2)` | Descuento global aplicado al paciente |
| `RevenueControlId` | `INT` | ID del control de ingresos padre |
| `OutputDate` | `DATETIME` | Fecha de egreso (formato `dd/MM/yyyy HH:mm:ss`) |
| `IsCutAccount` | `BIT` | Indica si es un corte de cuenta |
| `OutputDiagnosis` | `VARCHAR(10)` | Código CIE-10 del diagnóstico de egreso |
| `InitialDate` | `DATETIME` | Fecha de ingreso |
| `CutType` | `INT` | Tipo de corte |
| `FilePath` | `VARCHAR(MAX)` | Ruta para documentos electrónicos |
| `CurrencyId` | `INT` | Moneda de la transacción |
| `TRMValue` | `DECIMAL(20,5)` | Tasa representativa del mercado |
| `TaxDevolutionValue` | `NUMERIC(20,2)` | Valor de devolución de IVA |
| `ConditionSalesId` | `INT` | Condición de venta (factura electrónica CR) |
| `EconomicActivityId` | `INT` | Actividad económica |
| `ListPortfolioAdvanceCrossing` | Subnodo XML | Anticipos a cruzar (puede haber múltiples) |

### 2.4 Campos del subnodo `ListPortfolioAdvanceCrossing`

| Campo | Tipo | Descripción |
|-------|------|-------------|
| `Id` | `INT` | ID del anticipo (`PortfolioAdvance.Id`) |
| `Code` | `VARCHAR(20)` | Código del recibo de caja |
| `CrossingValue` | `DECIMAL(18,2)` | Valor a cruzar del anticipo |
| `CashReceiptDetailIdTmp` | `INT` | ID temporal del detalle del recibo de caja |
| `PortfolioAdvanceType` | `TINYINT` | Tipo: 1=Anticipo de entidad, 2=Anticipo de paciente |

---

## 3. Fase 1 — Validaciones Pre-Liquidación

**SP:** `[Billing].[SP_LiquidateFolioValidations_Output]`

Todas las validaciones se ejecutan **antes** de crear cualquier dato persistente. Los errores se acumulan en `@ResultMessage` y se retornan al final.

### 3.1 Validaciones de Parámetros de Entrada

| # | Validación | Mensaje de Error |
|---|------------|------------------|
| 1 | Unidad operativa válida (`> 0`) | No se ha enviado el parámetro Unidad Operativa |
| 2 | Existencia de `Billing.SettingsBilling` para la UO | No se encontraron parámetros de Facturación... |
| 3 | `ParticularHealthAdministratorId` parametrizado | No esta parametrizada una entidad administradora para particulares... |
| 4 | Código de paciente no vacío | No se ha enviado el parámetro Código de Paciente |
| 5 | Tercero paciente válido (`> 0`) | No se ha enviado el parámetro Tercero Paciente |
| 6 | Paciente coincide con ingreso en `ADINGRESO` | El paciente no coincide con el del ingreso |
| 7 | Número de ingreso no vacío | No se ha enviado el parámetro Número de Ingreso |
| 8 | Autorización de facturación enviada (`> 0`) | No se ha enviado el parámetro Autorización de Facturación |
| 9 | Al menos un folio en el XML | No hay folios para liquidar |
| 10 | Descuentos globales deshabilitados si `LiquidateMasterAccount = 0` | Los descuentos globales se encuentran deshabilitados |
| 11 | Usuario auditoría no vacío | No se ha enviado el parámetro Usuario Auditoria |

### 3.2 Validaciones de Cabecera / Factura

| # | Validación | Detalle |
|---|------------|---------|
| 12 | Autorización activa (`Status = 1`) | `Billing.BillingAuthorization` |
| 13 | Autorización vigente (fecha actual entre `InitialDate` y `FinalDate`) | Valida `Common.GETDATE()` |
| 14 | No exista factura activa (`Status = 1`) para el mismo folio | `Billing.Invoice` |
| 15 | Descuento global consistente | `TotalPatientSalesPrice = TotalPatientWithDiscount` cuando `IsMasterAccount = 0` |

### 3.3 Validaciones de Configuración Contable

| # | Validación | Detalle |
|---|------------|---------|
| 16 | Grupos de atención activos | `CareGroup.Status <> 0` |
| 17 | Tipo de grupo coincide con tipo de folio | `FolioType = CareGroupType` (excepto `IsMasterAccount = 4`) |
| 18 | Cliente asociado al tercero del folio | Para tipos 1, 2, 4 → `Common.Customer` debe existir |
| 19 | Estructura contable asignada | `ContractAccountingStructureId IS NOT NULL` |
| 20 | Cuenta contable de particulares parametrizada | Si `CareGroupType = 3` → `AccountParticularId IS NOT NULL` |
| 21 | Contrato vigente para EAPB con contrato | `CareGroupType = 1` → `Contract.Status = 1` |
| 22 | Detalle de contrato válido | `ContractDetail.ValidRecord = 1` |
| 23 | Contrato no vencido por fecha | Si `TerminationControl IN (2, 4)` |
| 24 | Contrato no superado por valor | Si `TerminationControl IN (3, 4)` → `ExecuteValue + TotalFolio <= ContractValue` |
| 25 | Entidad administradora no vacía | Si `EntityType <> 99` y `CareGroupType <> 3` |
| 26 | Tercero del folio coincide con HA | `ThirdPartyId = HealthAdministrator.ThirdPartyId` |
| 27 | Cuenta contable copago parametrizada | Si `TotalPatientSalesPrice > 0` y `LiquidateMasterAccount = 0` |

### 3.4 Validaciones de Secuencias

| # | Validación | Detalle |
|---|------------|---------|
| 28 | Secuencia automática de CxC | Formulario `682` en `Portfolio.PortfolioSequence` |
| 29 | Secuencia automática de Pagaré | Formulario `1510` |

### 3.5 Validaciones de Cuentas de Descuento

| # | Validación | Detalle |
|---|------------|---------|
| 30 | Unidades funcionales con cuenta descuento | Conceptos con `AccountingType = 2` → `BillingConceptAccount.DiscountAccountId` |
| 31 | Productos con cuenta descuento | `ProductGroupFunctionalUnit.DiscountAccountId` |
| 32 | Conceptos de facturación con cuenta descuento | Conceptos con `AccountingType = 1` → `BillingConcept.DiscountAccountId` |

### 3.6 Validaciones de Terceros

| # | Validación | Detalle |
|---|------------|---------|
| 33 | Dirección válida del tercero | `Common.Address` con `DepartmentId` y `CityId` |

### 3.7 Validaciones de Detalles / Items

| # | Validación | Detalle |
|---|------------|---------|
| 34 | Autorización MIPRES para NO-POS | Medicamentos `SettlementType = 3` con `NOPOSPROD = 1` |
| 35 | Items incluidos al 100% con valor > 0 | `SettlementType = 3 AND GrandTotalSalesPrice > 0` |
| 36 | Distribución existente para todos los items | `ServiceOrderDetailDistribution.Id IS NOT NULL` |
| 37 | Balance de items (Qty × UnitPrice = Total) | Con y sin IVA incluido según `SalePriceIncludeTax` |
| 38 | Detalles quirúrgicos cuadran | `GrandTotalSalesPrice = SUM(Surgical.TotalSalesPrice)` |
| 39 | Valor del folio = suma de detalles | `TotalFolio - TotalPatientSalesPrice = SUM(ThirdPartySalesPrice)` |

### 3.8 Validaciones de Cruce de Anticipos

| # | Validación | Detalle |
|---|------------|---------|
| 40 | Pagaré: secuencia si `GeneratePromissoryNote = 0` | `SettingsBilling.GeneratePromissoryNote` |
| 41 | Permiso del usuario para generar pagaré | Formulario `756`, Acción `59` |
| 42 | Devolución IVA: métodos de pago consistentes | Anticipos con métodos de pago diferentes cuando aplica devolución |

### 3.9 Validaciones de Control de Cuentas

Se ejecutan **solo cuando todos los folios se liquidan** y `AccountControlValidation = 1`:

| # | Validación | Entidad HIS |
|---|------------|-------------|
| 43 | Valoraciones pendientes | `HCHISPACA` |
| 44 | Procedimientos de enfermería | `HCHOGASIN` |
| 45 | Estancia hospitalaria | `Billing.AccountControlStays` |
| 46 | Consumo de oxígeno | `HCCONOXIG` |
| 47 | Terapias | `HCPROCTER` |
| 48 | Hemocomponentes | `HCORHEMSER` |
| 49 | Interconsultas | `HCORDINTE` |
| 50 | Procedimientos No QX | `HCORDPRON` |
| 51 | Procedimientos QX | `ViewSurgeriesPerformed` |
| 52 | Imágenes diagnósticas | `HCORDIMAG` |
| 53 | Patologías | `HCORDPATO` |
| 54 | Laboratorios | `HCORDLABO` |
| 55 | Medicamentos e insumos | `VMedicinesSupplies` |

> **Nota:** Si alguna de estas validaciones falla, retorna código `-001`, lo que activa en el frontend el modal de justificación/control de cuentas.

---

## 4. Fase 2 — Creación de la Factura

**SP:** `[Billing].[SP_CreateInvoice_Output]`

### 4.1 Recálculo Multimoneda

**Función:** `[Billing].[RecalculateFolioDetailsByCurrency]`

Esta función recalcula los valores de cada distribución del folio según la moneda seleccionada.

#### Proceso:

1. Obtiene la moneda oficial de `GeneralLedger.CompanySettings.OfficialCurrencyId`
2. Obtiene el flag `LiquidateMasterAccount` de `Billing.SettingsBilling`
3. Para cada distribución (`ServiceOrderDetailDistribution`):

```
SubTotalSalesPrice(moneda)  = ROUND(CurrencyConverter(SubTotal_oficial, oficial → moneda), 2)
GrandTotalDiscount(moneda)  = ROUND(CurrencyConverter(Discount_oficial, oficial → moneda), 2)
NetWorth                    = SubTotal(moneda) - Discount(moneda)
GrandTotalTaxes             = ROUND(NetWorth × %IVA / 100, 2)
GrandTotalSalesPrice        = NetWorth + GrandTotalTaxes
UnitGrossValue              = ROUND(SubTotal / Quantity, 2)
NetUnitValue                = ROUND(NetWorth / Quantity, 2)
TotalSalesPrice             = GrandTotalSalesPrice / Quantity
InvoiceThirdPartySalesValue = GrandTotalSalesPrice - SubTotalPatientSalesPrice
```

#### Cálculo de IVA según configuración:

**Si `SalePriceIncludeTax = 1` (IVA incluido en precio — función `SetValueSalesPrice`):**
```
GrossValue = ROUND(SalesPrice / (1 + %IVA/100), 2)
TaxValue   = SalesPrice - GrossValue
```

**Si `SalePriceIncludeTax = 0` (IVA no incluido):**
```
TaxValue   = ROUND(GrossValue × %IVA/100, 2)
SalesPrice = GrossValue + TaxValue
```

### 4.2 Validaciones Pre-Insert

| Validación | Detalle |
|------------|---------|
| Límite máximo de items | `MaxInvoiceItems` en `SettingsBilling` |
| Contrato — valor ejecutado | Incrementa `Contract.ExecuteValue` y valida contra `ContractValue` |
| Autorización — consecutivos | Verifica rango `InitialInvoice..FinalInvoice` |
| Autorización — vigencia | `InvoiceDate` entre `InitialDate` y `FinalDate` |
| Cantidades negativas o en 0 | `sodd.Quantity <= 0 OR sod.InvoicedQuantity < 0` |
| Cantidades consistentes | `sodd.Quantity = sod.InvoicedQuantity` (con excepciones) |
| Categoría de factura | `InvoiceCategoryId IS NOT NULL` |
| Recálculo exitoso | Al menos un registro en `@RecalculateFolioDetails` con `StatusResult = 1` |

### 4.3 Tiquete Electrónico de Venta (Costa Rica)

Si `ApplyElectronicSalesTicket = 1` y el tercero **no** es facturador electrónico (`ElectronicBiller = 0`):
- Se usa la autorización de tiquete desde `SettingsBilling.BillingAuthorizationId`
- Se marca `@IsElectronicTicket = 1`

### 4.4 Generación del Número de Factura

| Tipo de Liquidación | Fuente del Consecutivo | Formato |
|---------------------|------------------------|---------|
| **Pago por servicios** (`LiquidationType = 1`) | `BillingAuthorization.Consecutive` | `Prefix + Consecutive` |
| **Capitación** (`LiquidationType <> 1`) | `SettingsBilling.ConsecutiveControlCapitation` | `PrefixConsecutiveCapitation + Consecutive` |

El consecutivo se incrementa atómicamente con `UPDATE ... SET @var = Column = Column + 1`.

### 4.5 Determinación del Tipo de Folio (DocumentType)

```
Si LiquidationType <> 1 → FolioType = 5 (Capitación)
Si FolioType = 4         → FolioType = 2 (Sin contrato)
Demás                    → Se mantiene el FolioType original
```

### 4.6 INSERT en `Billing.Invoice`

| # | Campo | Fuente / Cálculo |
|---|-------|-------------------|
| 1 | `OperatingUnitId` | Parámetro de entrada |
| 2 | `DocumentType` | Tipo de folio resuelto |
| 3 | `InvoiceNumber` | Prefijo + consecutivo |
| 4 | `RevenueControlDetailId` | ID del folio |
| 5 | `AdmissionNumber` | Parámetro de entrada |
| 6 | `HealthAdministratorId` | Del folio (o particular por defecto) |
| 7 | `ThirdPartyId` | Del folio |
| 8 | `PatientCode` | Parámetro de entrada |
| 9 | `CareGroupId` | Del folio |
| 10 | `InvoiceDate` | `Common.GETDATE()` |
| 11 | `InvoiceExpirationDate` | `InvoiceDate + InvoiceDeadlines días` |
| 12 | `TotalInvoice` | `SUM(GrandTotalSalesPrice)` recalculados |
| 13 | `CapitationPatientValue` | 0 |
| 14 | `ThirdPartySalesValue` | `SUM(InvoiceThirdPartySalesValue)` |
| 15 | `ThirdPartyDiscountValue` | `SUM(GrandTotalDiscount)` |
| 16 | `ResponsibleRecoveryFee` | Del folio |
| 17 | `TotalPatientSalesPrice` | `SUM(SubTotalPatientSalesPrice)` |
| 18 | `PatientDiscount` | Convertido a moneda de transacción |
| 19 | `PatientDiscountPercentage` | Del folio |
| 20 | `TotalPatientWithDiscount` | Convertido a moneda de transacción |
| 21 | `ValueVoucher` | Convertido a moneda de transacción |
| 22 | `PatientPaidValue` | `@TotalCrossingValue` (anticipos cruzados) |
| 23 | `ThirdPartyAccountReceivableValue` | = `ThirdPartySalesValue` |
| 24 | `PatientAccountReceivableValue` | = `SubTotalPatientSalesPrice` |
| 25 | `PatientType` | De `INPACIENT.IPTIPOPAC` |
| 26 | `Observation` | Observación del contrato + observación del folio |
| 27–29 | Datos sociodemográficos | `IPTIPOAFI`, `CAPACIPAG`, `NIVECODIGO` |
| 30–31 | Retención CREE | 0 (valores iniciales) |
| 32 | `Status` | 1 (Activa) |
| 33–34 | Auditoría | `@UserCode`, `Common.GETDATE()` |
| 35 | `InvoiceCategoryId` | Del folio |
| 36–40 | Datos de egreso | `OutputDate`, `IsCutAccount`, `OutputDiagnosis`, `InitialDate`, `CutType` |
| 41 | `BillingAuthorizationId` | ID de autorización (NULL si capitación) |
| 42 | `ContractId` | Del grupo de atención |
| 43 | `InvoiceValue` | Si cuenta madre: `GrandTotal`; sino: `ThirdParty + Patient` |
| 44 | `ValueTax` | `SUM(GrandTotalTaxes)` |
| 45 | `TotalValue` | `InvoiceThirdPartySalesValue` |
| 46 | `CUFE` | `'CUFE'` si factura electrónica (InvoiceType = 3) |
| 47 | `CurrencyId` | Moneda de transacción |
| 48 | `TRMValue` | Tasa de cambio |
| 49 | `TaxDevolutionValue` | Suma de devoluciones de IVA |
| 50 | `IsElectronicTicket` | Flag tiquete electrónico CR |
| 51 | `EconomicActivityId` | Actividad económica |

### 4.7 INSERT en `Billing.InvoiceDetail`

Cada distribución del folio genera un detalle con valores recalculados:

| Campo | Fuente |
|-------|--------|
| `GrandTotalSalesPrice` | Recalculado por moneda |
| `GrandTotalDiscount` | Recalculado por moneda |
| `ThirdPartySalesPrice` | Si cuenta madre: `GrandTotal`; sino: `InvoiceThirdPartySalesValue` |
| `SubTotalPatientSalesPrice` | Si cuenta madre: 0; sino: recalculado |
| `InvoicedQuantity` | Si cuenta madre: `sodd.Quantity`; sino: `sod.InvoicedQuantity` |
| `TotalSalesPrice` | Recalculado por moneda |
| `ThirdPartyDiscount` | Convertido a moneda de transacción |
| `Balance` | = `InvoiceThirdPartySalesValue` |
| `NetWorth` | Recalculado |
| `GrandTotalTaxes` | Recalculado |
| `TaxId` | ID del IVA aplicado |

### 4.8 INSERT en `Billing.InvoiceDetailSurgical`

Para items con componente quirúrgico, se copian datos de `ServiceOrderDetailSurgical`:
- Servicio IPS, cantidad, porcentaje de liquidación, precio manual, profesional, concepto de facturación, centro de costo, recargos, honorarios médicos

### 4.9 Devolución de IVA (`Billing.InvoiceTaxDevolution`)

Se inserta **solo si** `@TaxDevolutionValue > 0` y:
- El IVA tiene `ApplyTaxDevolution = 1`
- El método de pago del anticipo coincide con `PaymentMethodTypes` del IVA

Campos almacenados:
- `InvoiceId`, `TaxId`, `Value` (moneda transacción), `ValueInOfficialCurrency` (moneda oficial)

### 4.10 Documento Electrónico (`Billing.ElectronicDocument`)

Se inserta **solo si**:
- `HandlesElectronicBilling = 1` en `GeneralLedgerSettings`
- `InvoiceType = 3` en la autorización de facturación

| Campo | Valor |
|-------|-------|
| `DianVersion` | De `GeneralLedgerSettings` |
| `DocumentType` | Tipo de factura |
| `Status` | 1 (Pendiente) |
| `FilePath` | Calculado por `GetElectronicDocumentFilePath` |
| `CUFE` | 'CUFE' (se genera posteriormente) |
| `Year` | Año de la factura |

### 4.11 Retenciones del Cliente (`Billing.InvoiceCustomerRetention`)

Se insertan si `FolioType <> 5` (no capitación) y `CalculateTaxAdvance IN (1, 2)`:

```
BaseValue = IIF(RetentionType = 2, 0, InvoiceThirdPartySalesValue)
Value     = ROUND(BaseValue × Rate / 100, 2)
```

Solo se insertan si `Value <> 0`.

### 4.12 Cruce de Anticipos (`Billing.InvoicePortfolioAdvance`)

Registra la relación factura ↔ anticipo:
- `InvoiceId`, `PortfolioAdvanceId`, `CrossingValue`

### 4.13 Propiedades Electrónicas (`Billing.ElectronicsProperties`)

Si `ConditionSalesId > 0`, se inserta un registro con la condición de venta para la factura electrónica de Costa Rica.

---

## 5. Fase 3 — Cuentas por Cobrar

**SPs:** `[Billing].[SP_CreateAccountReceivables_Output]` → `[Billing].[SP_CreateAccountReceivable_Output]`

### 5.1 Tipos de Cuenta por Cobrar

| AccountReceivableType | Nombre | Condición | Tercero | Valor | Saldo |
|----------------------|--------|-----------|---------|-------|-------|
| **2** | CxC a Entidad | Pago por servicios (`LiquidationType = 1`) | Tercero del folio | `ThirdPartySalesValue - TaxDevolution` | `Value - RetentionValue` |
| **4** | Pagaré | Cruce < valor paciente (NO pago por servicios) | Tercero paciente | Valor pagaré | = Value |
| **6** | CxC al Paciente | Cruce de anticipo existente (NO particular) | Tercero paciente | `TotalPatientWithDiscount - PagareValue` | Según tipo liquidación |

> **Nota:** En el flujo nuevo, la CxC tipo 6 está actualmente **deshabilitada** (código comentado).

### 5.2 Determinación de la Cuenta Contable Principal

```
Si FolioType = 3 y CareGroupType <> 3 y IsMasterAccount = 4 → AccountRecoveryFeeId
Si FolioType = 3                                              → AccountParticularId
Si AccountReceivableType IN (4, 6)                            → AccountRecoveryFeeId
Demás                                                         → AccountWithoutRadicateId
```

### 5.3 Entidades Creadas

#### `Portfolio.AccountReceivable`

| Campo | Valor |
|-------|-------|
| `Code` | Secuencia automática (formulario 682 o 1510) |
| `AccountReceivableType` | 2, 4 o 6 |
| `PortfolioStatus` | 3 si particular, 1 demás |
| `Status` | 2 (Activa) |
| `CurrencyId` | Moneda de la factura |
| `TRMValue` | Tasa de cambio |
| `AffectBudget` | Flag del grupo de atención |
| `BudgetId` | `BillingBudgetId` o `PromissoryNoteBudgetId` |

Incluye 15+ cuentas contables para gestión del ciclo de vida:
- Sin radicar, Radicada, Objeción subsanada, Conciliación, Cobro jurídico, Cobro de difícil recaudo
- Provisión (crédito/débito), Deterioro (crédito/débito), Reversión deterioro

#### `Portfolio.AccountReceivableShare`
- Una cuota única con fecha de vencimiento

#### `Portfolio.AccountReceivableAccounting`
- Detalle contable con cuenta principal, tercero y centro de costo

### 5.4 Reconocimiento Presupuestal

Se ejecuta `[Portfolio].[SP_GenerateRecognitionByAccountReceivableId_Output]` para crear el reconocimiento presupuestal automáticamente.

---

## 6. Fase 4 — Comprobante Contable

**SP:** `[Billing].[SP_GenerateJournalVoucherDetails]`

### 6.1 Generación del Comprobante

Solo se genera para CxC tipo 2 (a la entidad). Se usa:
- `InvoiceJournalVoucherTypeId` para facturas normales
- `AccountingVoucherGenerationId` para tiquetes electrónicos de venta

Se persiste via `[GeneralLedger].[SP_CreateAndValidateJournalVoucherMovement]` y se vincula a la factura (`Invoice.JournalVoucherId`).

### 6.2 Estructura de Movimientos Contables

| Concepto | Débito | Crédito | Detalle |
|----------|--------|---------|---------|
| **CxC al Cliente** | ✓ | — | Valor entidad - descuento paciente particular |
| **Retenciones** (si reconocer) | — | ✓ | Anticipos de impuestos |
| **Descuentos a Entidad** | ✓ | — | Cuenta descuento ventas por concepto |
| **Descuentos a Servicios** (cuenta madre) | ✓ | — | Cuenta descuento por unidad funcional |
| **Descuentos a Productos** | ✓ | — | Cuenta grupo producto × unidad funcional |
| **IVA Devuelto** | ✓ | — | Cuenta IVA venta |
| **Copagos / Cuota recuperación** | ✓ | — | Cuenta copago o cuota fija |
| **Descuento cuota recuperación** | ✓ | — | Si hay descuento al paciente |
| **Ingresos Servicios/Productos** | — | ✓ | Cuenta ingreso por concepto facturación |

> Se eliminan movimientos donde `CreditValue = 0 AND DebitValue = 0`.

### 6.3 Comprobante de Pagaré (Capitación)

Para pagarés en liquidación tipo capitación (`LiquidationType <> 1`):

| Movimiento | Débito | Crédito |
|------------|--------|---------|
| Cuenta sin radicar del pagaré | Valor | — |
| Cuenta anticipo paciente capitado | — | Valor |

---

## 7. Fase 5 — Cruce de Anticipos

### 7.1 Flujo de Cruce

```
[Portfolio].[SP_GeneratePortfolioTransfer]
  ├─► Valida saldo del anticipo >= valor a cruzar
  ├─► Reduce PortfolioAdvance.Balance
  ├─► Incrementa PortfolioAdvance.TransferValue
  └─► Genera movimiento de transferencia hacia la CxC
```

### 7.2 Tipos de Anticipo

| PortfolioAdvanceType | Descripción | Cruce contra |
|---------------------|-------------|--------------|
| **1** | Anticipo de Entidad (tercero beneficiario) | CxC de la entidad (se ejecuta por separado) |
| **2** | Anticipo de Paciente | CxC del paciente o particular |

### 7.3 Caso de los datos de prueba

- Anticipo `RC000138998` con valor `73,210.08` y tipo 2 (paciente)
- Se cruza contra la CxC generada para el folio particular

### 7.4 Para capitación (`LiquidationType <> 1`)

En lugar de transferencia, se actualiza directamente:
```sql
UPDATE PortfolioAdvance
  SET Balance = Balance - CrossingValue,
      TransferValue = TransferValue + CrossingValue
```

---

## 8. Fase 6 — Actualizaciones Post-Factura

### 8.1 Historia Clínica Electrónica (EHR)

| Sistema | Tabla | Campo Actualizado |
|---------|-------|-------------------|
| RIAS | `RIASCUPSPACIENTE` | `IDFACTURA = @InvoiceId` |
| Laboratorios | `AMBORDLAB` | `GENINVOICE`, `GENINVOICEID` |
| Patologías | `AMBORDPAT` | `GENINVOICE`, `GENINVOICEID` |
| Imágenes Dx | `AMBORDIMA` | `GENINVOICE`, `GENINVOICEID` |
| Consulta Externa | `ADCONCOEX` | `GENINVOICE`, `GENINVOICEID` |

### 8.2 Topes Anuales del Paciente

Tabla `INPACIENTTOPANU` — acumulados anuales:

| Campo | RecoveryFeeType | Significado |
|-------|----------------|-------------|
| `PAGADOCMO` | 2 | Cuotas moderadoras |
| `PAGADOCOP` | 3 | Copagos |
| `PAGADOCRE` | 5 | Recaudos |

Si no existe registro para el año, se crea uno nuevo.

### 8.3 Autorizaciones

```sql
UPDATE Authorization.TraceabilityPaperwork
  SET PreviousStatus = Status, Status = 10  -- 10 = Facturado
```

### 8.4 Folio (`Billing.RevenueControlDetail`)

```sql
UPDATE Billing.RevenueControlDetail
  SET BillingAuthorizationId = @BillingAuthorizationId,
      OutputDate = @OutputDate,
      IsCutAccount = @IsCutAccount,
      OutputDiagnosis = @OutputDiagnosis,
      Status = 2  -- 2 = Liquidado
```

### 8.5 Propiedades Electrónicas

Si `ConditionSalesId > 0`:
```sql
INSERT INTO Billing.ElectronicsProperties
  (EntityId, EntityName, ConditionSalesId, ...)
```

### 8.6 Paquetes Contables

Si `ValidatePackaging = 1` y `AccountingPackage = 1`:
```
EXEC [Billing].[SP_CreatePackageJournalVoucher_Output]
```

---

## 9. Fase 7 — Cierre de Ingreso

### 9.1 Lógica de Decisión

```
SI existen folios pendientes (Status IN 1, 3, 5) para la admisión:
    → UPDATE ADINGRESO SET IESTADOIN = 'P'  (Parcialmente facturado)

SI NO existen folios pendientes:
    → EXEC SP_CloseAdmission_Output  (Cierre completo del ingreso)
```

### 9.2 FURIPS

Para folios de tipo Aseguradoras (`FolioType = 4`), si no existe factura previa:
```sql
UPDATE ADFURIPSU SET NUMFAC = @InvoiceNumber
```

---

## 10. Manejo de Multimoneda

### 10.1 Función de Conversión

```
Common.CurrencyConverterByModule(valor, monedaOrigen, monedaDestino, UO, modulo, fechaTRM)
```

| Parámetro | Descripción |
|-----------|-------------|
| `valor` | Monto a convertir |
| `monedaOrigen` | ID moneda de origen |
| `monedaDestino` | ID moneda de destino |
| `UO` | Unidad operativa |
| `modulo` | `'Invoice'` para facturación |
| `fechaTRM` | Fecha de la TRM (NULL = más reciente) |

### 10.2 Flujo de Conversión en el Recálculo

```
                    Moneda Oficial (CRC/COP)
                            │
                    CurrencyConverterByModule
                            │
                            ▼
                    Moneda de Transacción
                    (CRC, USD, etc.)
                            │
              ┌─────────────┼─────────────┐
              ▼             ▼             ▼
         SubTotal      Descuento    PatientSales
              │             │
              └─────┬───────┘
                    ▼
              NetWorth = Sub - Desc
                    │
                    ▼
              Taxes = NetWorth × %IVA/100
                    │
                    ▼
              GrandTotal = NetWorth + Taxes
```

### 10.3 Precisión y Redondeo

- Valores monetarios: `NUMERIC(20,2)` — 2 decimales
- TRM: `DECIMAL(20,5)` — 5 decimales
- Redondeos intermedios: `ROUND(..., 2)` en cada paso
- Función `RoundValue`: soporte para redondeo configurable (unidad, decena, centena, millar)

### 10.4 Ejemplo con Datos de Prueba

```
CurrencyId = 1 (moneda oficial, probablemente CRC)
TRMValue   = 0.00193798449612403 (≈ 1/516, tasa CRC → USD)

Si CurrencyId = OfficialCurrencyId → Sin conversión (factor = 1)
Si factura en USD → Cada valor × 0.00194
```

---

## 11. Entidades Creadas y Modificadas

### 11.1 Entidades CREADAS

| Tabla | Cantidad | Descripción |
|-------|----------|-------------|
| `Billing.Invoice` | 1 por folio | Cabecera de factura |
| `Billing.InvoiceDetail` | N por folio | Un detalle por distribución |
| `Billing.InvoiceDetailSurgical` | M | Detalles quirúrgicos |
| `Billing.InvoiceTaxDevolution` | K | Devoluciones de IVA por tipo |
| `Billing.ElectronicDocument` | 0–1 | Si aplica factura electrónica |
| `Billing.InvoiceCustomerRetention` | R | Retenciones del cliente |
| `Billing.InvoicePortfolioAdvance` | P | Cruces de anticipos |
| `Billing.ElectronicsProperties` | 0–1 | Condiciones de venta CR |
| `Portfolio.AccountReceivable` | 1+ | CxC por tipo |
| `Portfolio.AccountReceivableShare` | 1+ | Cuotas |
| `Portfolio.AccountReceivableAccounting` | 1+ | Detalle contable |
| `GeneralLedger.JournalVouchers` | 1 | Comprobante contable |
| `GeneralLedger.JournalVoucherDetails` | N | Movimientos contables |

### 11.2 Entidades MODIFICADAS

| Tabla | Cambio |
|-------|--------|
| `Billing.BillingAuthorization` | Incremento del consecutivo |
| `Billing.SettingsBilling` | Incremento consecutivo capitación |
| `Billing.RevenueControlDetail` | Status → 2, datos de egreso |
| `Billing.Invoice` | JournalVoucherId vinculado |
| `Contract.Contract` | ExecuteValue incrementado |
| `Authorization.TraceabilityPaperwork` | Status → 10 (Facturado) |
| `Portfolio.PortfolioAdvance` | Balance reducido por cruce |
| `dbo.ADINGRESO` | IESTADOIN actualizado |
| `dbo.INPACIENTTOPANU` | Topes anuales acumulados |
| `dbo.AMBORDLAB` | GENINVOICE, GENINVOICEID |
| `dbo.AMBORDPAT` | GENINVOICE, GENINVOICEID |
| `dbo.AMBORDIMA` | GENINVOICE, GENINVOICEID |
| `dbo.ADCONCOEX` | GENINVOICE, GENINVOICEID |
| `dbo.RIASCUPSPACIENTE` | IDFACTURA |
| `dbo.ADFURIPSU` | NUMFAC |

---

## 12. Observaciones y Riesgos

### 12.1 Aspectos Positivos

- **Validaciones centralizadas:** El flujo nuevo separa las validaciones en un SP independiente, ejecutándose antes de cualquier persistencia
- **Multimoneda robusto:** Recálculo por línea con función dedicada y soporte para TRM personalizada
- **Tiquete electrónico de venta:** Soporte para regulación de Costa Rica
- **Devolución de IVA:** Implementación por tipo de IVA y método de pago
- **Reconocimiento presupuestal:** Automático al crear la CxC
- **Estructura contable flexible:** 15+ cuentas configurables por grupo de atención

### 12.2 Riesgos Identificados

| Riesgo | Severidad | Detalle |
|--------|-----------|---------|
| CxC tipo 6 deshabilitada | **Media** | En `SP_CreateAccountReceivables_Output`, la CxC al paciente (tipo 6) está completamente comentada |
| SQL dinámico en flujo legacy | **Alta** | `SP_CreateInvoice` usa `sp_executesql` con concatenación para acceder a la BD HIS |
| NOLOCK masivo | **Media** | Todas las lecturas usan `WITH(NOLOCK)`, posibles lecturas sucias |
| Redondeo acumulativo | **Baja** | IVA se recalcula sobre valores ya redondeados; posible pérdida de centavos |
| Precisión de TRM | **Baja** | `DECIMAL(20,5)` vs precisiones variables en funciones de conversión |
| Dos flujos coexistentes | **Media** | El flujo legacy (`LiquidateFolio_Output`) y el nuevo (`SP_LiquidateFolio_Output`) pueden causar confusión |
| Sin transacción explícita | **Alta** | El SP principal no maneja transacción; la transacción se gestiona externamente |
| Validación -001 en control de cuentas | **Baja** | Código de retorno especial `'-001'` usado como señal para el frontend |

### 12.3 Dependencias Externas

| Módulo | Esquema | Uso |
|--------|---------|-----|
| Contabilidad | `GeneralLedger` | Comprobantes, cuentas contables, IVA, configuración |
| Portafolio/Cartera | `Portfolio` | CxC, anticipos, transferencias, secuencias |
| Contratos | `Contract` | Grupos de atención, contratos, estructura contable, CUPS |
| Tesorería | `Treasury` | Recibos de caja, métodos de pago |
| Inventario | `Inventory` | Productos, grupos, cuentas de descuento |
| Común | `Common` | Terceros, clientes, direcciones, moneda, conversión |
| Seguridad | `Security` | Usuarios, permisos |
| Autorizaciones | `Authorization` | Trazabilidad de autorizaciones |
| Presupuesto | `Budget` | Reconocimiento presupuestal |
| Nómina | `Payroll` | Unidades funcionales |
| HIS (Crystal/Indigo) | `dbo` | Tablas legacy del sistema clínico |

---

## 13. Datos de Prueba de Referencia

### 13.1 Script de Ejecución

```sql
DECLARE @xml XML = '
<RevenueControlDetailCrossingList>
    <FolioOrder>4</FolioOrder>
    <FolioType>3</FolioType>
    <CareGroupId>1</CareGroupId>
    <RevenueControlDetailId>267311</RevenueControlDetailId>
    <TotalPatientDiscount>0</TotalPatientDiscount>
    <RevenueControlId>138680</RevenueControlId>
    <OutputDate>18/02/2026 10:21:44</OutputDate>
    <IsCutAccount>False</IsCutAccount>
    <OutputDiagnosis>Z000</OutputDiagnosis>
    <InitialDate>18/02/2026 10:11:44</InitialDate>
    <CutType>1</CutType>
    <FilePath>C:\ProgramData\Indigo Technologies\ElectronicDocuments\INDIGO537</FilePath>
                                        
    <ListPortfolioAdvanceCrossing>
        <Id>153332</Id>
        <Code>RC000138998</Code>
        <CrossingValue>73210.08</CrossingValue>
        <CashReceiptDetailIdTmp>0</CashReceiptDetailIdTmp>
        <PortfolioAdvanceType>2</PortfolioAdvanceType>
    </ListPortfolioAdvanceCrossing>

    <CurrencyId>1</CurrencyId>
    <TRMValue>0.00193798449612403</TRMValue>
    <TaxDevolutionValue>0</TaxDevolutionValue>
    <ConditionSalesId>1</ConditionSalesId>
    <EconomicActivityId></EconomicActivityId>
</RevenueControlDetailCrossingList>
'

DECLARE @PatientCode VARCHAR(20) = '107290375',
    @AdmissionNumber VARCHAR(20) = '05A68660E5',
    @ContainerCrystal VARCHAR(10) = 'INDIGO537',
    @BillingAuthorizationId INT = 1,
    @OperativeUnitId INT = 1,
    @ThirdPartyPatientId INT = 41150,
    @UserCode VARCHAR(20) = '234',
    @CompanyType TINYINT = 1,
    @SkipAccountControlValidations BIT = 0

BEGIN TRAN t123
BEGIN TRY
    EXECUTE [Billing].[LiquidateFolio] 
        @PatientCode = @PatientCode, 
        @AdmissionNumber = @AdmissionNumber, 
        @ContainerCrystal = @ContainerCrystal, 
        @BillingAuthorizationId = @BillingAuthorizationId, 
        @OperativeUnitId = @OperativeUnitId, 
        @ThirdPartyPatientId = @ThirdPartyPatientId, 
        @UserCode = @UserCode,
        @RevenueControlDetailCrossingListXml = @xml, 
        @CompanyType = @CompanyType,  
        @SkipAccountControlValidations = @SkipAccountControlValidations

    ROLLBACK TRAN t123
END TRY
BEGIN CATCH
    SELECT 999 ErrorCode, ERROR_MESSAGE() ErrorMessage
    ROLLBACK TRAN t123
END CATCH
```

### 13.2 Interpretación de los Datos

| Dato | Valor | Significado para el Flujo |
|------|-------|---------------------------|
| `FolioType = 3` | Particular | Se usa `AccountParticularId`, no se genera CxC separada a entidad y paciente |
| `TotalPatientDiscount = 0` | Sin descuento | No hay ajuste de descuento global |
| `CurrencyId = 1` | Moneda oficial | Si coincide con `OfficialCurrencyId`, no hay conversión de moneda |
| `TRMValue ≈ 0.00194` | ≈ 1/516 | Tasa CRC → USD (≈ 516 colones por dólar) |
| `TaxDevolutionValue = 0` | Sin devolución IVA | No se activa el flujo de `InvoiceTaxDevolution` |
| `PortfolioAdvanceType = 2` | Anticipo paciente | Se cruza contra CxC del particular |
| `CrossingValue = 73,210.08` | Valor del cruce | Se descuenta del anticipo RC000138998 |
| `IsCutAccount = False` | No es corte de cuenta | Liquidación normal |
| `OutputDiagnosis = Z000` | CIE-10: Examen general | Diagnóstico de egreso |

---

> **Generado automáticamente** a partir del análisis del schema de base de datos del ERP.
