CREATE TABLE [Contract].[CareGroup] (
    [Id]                                        INT             IDENTITY (1, 1) NOT NULL,
    [Code]                                      VARCHAR (20)    NOT NULL,
    [Name]                                      VARCHAR (100)   NOT NULL,
    [CareGroupType]                             TINYINT         NOT NULL,
    [DefaultManual]                             TINYINT         CONSTRAINT [DF_CareGroup_DefaultManual] DEFAULT ((1)) NOT NULL,
    [ContractId]                                INT             NULL,
    [LiquidationType]                           TINYINT         NOT NULL,
    [CostCenterId]                              INT             NOT NULL,
    [BillingPeriod]                             TINYINT         NOT NULL,
    [MaximumIndividualBilling]                  NUMERIC (18, 2) NOT NULL,
    [PeriodMaximumBilling]                      NUMERIC (18, 2) NOT NULL,
    [TypeLiquidationEmergencyStays]             TINYINT         CONSTRAINT [DF_CareGroup_TypeLiquidationEmergencyStays] DEFAULT ((1)) NOT NULL,
    [MinimumObservationTime]                    TINYINT         CONSTRAINT [DF_CareGroup_MinimumObservationTime] DEFAULT ((0)) NOT NULL,
    [MaximumObservationTime]                    TINYINT         CONSTRAINT [DF_CareGroup_MaximumObservationTime] DEFAULT ((0)) NOT NULL,
    [HoursOfRecoveryIncluded]                   TINYINT         CONSTRAINT [DF_CareGroup_HoursOfRecoveryIncluded] DEFAULT ((0)) NOT NULL,
    [RequirementsTemplateId]                    INT             NOT NULL,
    [InvoiceDeadlines]                          INT             NOT NULL,
    [ProcedureTemplateId]                       INT             NOT NULL,
    [ProductRateId]                             INT             NOT NULL,
    [ConceptToBill]                             TINYINT         NOT NULL,
    [EntityType]                                TINYINT         NOT NULL,
    [ContractAccountingStructureId]             INT             NULL,
    [AffectBudget]                              BIT             CONSTRAINT [DF_CareGroup_AffectBudget] DEFAULT ((0)) NOT NULL,
    [BillingBudgetId]                           INT             NULL,
    [OperativeUnitId]                           INT             CONSTRAINT [DF_CareGroup_OperativeUnitId] DEFAULT ((14)) NOT NULL,
    [Status]                                    BIT             CONSTRAINT [DF_CareGroup_Status] DEFAULT ((1)) NOT NULL,
    [CreationUser]                              VARCHAR (20)    NOT NULL,
    [CreationDate]                              DATETIME        NOT NULL,
    [ModificationUser]                          VARCHAR (20)    NULL,
    [ModificationDate]                          DATETIME        NULL,
    [TimeStamp]                                 ROWVERSION      NOT NULL,
    [ExtramuralPharmaceuticalDispensing]        BIT             NULL,
    [ApplyRIAS]                                 BIT             NULL,
    [PromissoryNoteBudgetId]                    INT             NULL,
    [AuthorizationRequired]                     BIT             CONSTRAINT [DF_CareGroup_AuthorizationRequired] DEFAULT ((0)) NOT NULL,
    [AccountReceivablePreviousValidityBudgetId] INT             NULL,
    [PortfolioRecoveryBudgetId]                 INT             NULL,
    [TechnicalNoteId]                           INT             NULL,
    [LiquidateDayDischarge]                     BIT             CONSTRAINT [DF__CareGroup__Liqui__6F3BCD40] DEFAULT ((0)) NOT NULL,
    [MaximumTypeTop]                            BIT             NOT NULL,
    [DiscountContractedCustomer]                DECIMAL (18, 2) CONSTRAINT [DF__CareGroup__Disco__7B427CC9] DEFAULT ((0)) NOT NULL,
    [TypeLiquidationOxygen]                     TINYINT         CONSTRAINT [DF__CareGroup__TypeL__270BF06E] DEFAULT ((1)) NOT NULL,
    [BillingConceptCopayId]                     INT             NULL,
    [ThirdPartyInvoiceCopayFixedAmount]         TINYINT         CONSTRAINT [DF__CareGroup__Third__769E8E94] DEFAULT ((0)) NOT NULL,
    [MOSTRARWEB]                                BIT             NULL,
    [RegisterPolicy]                            BIT             DEFAULT ((0)) NOT NULL,
    [MethodFixedAmountCollectionReport]         INT             CONSTRAINT [DF_CareGroup_MethodFixedAmount] DEFAULT ((2)) NOT NULL,
    CONSTRAINT [PK_CareGroup__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CareGroup_BillingConceptCopay] FOREIGN KEY ([BillingConceptCopayId]) REFERENCES [Billing].[BillingConcept] ([Id]),
    CONSTRAINT [FK_CareGroup_Budget] FOREIGN KEY ([BillingBudgetId]) REFERENCES [Budget].[Budget] ([Id]),
    CONSTRAINT [FK_CareGroup_Budget1] FOREIGN KEY ([PromissoryNoteBudgetId]) REFERENCES [Budget].[Budget] ([Id]),
    CONSTRAINT [FK_CareGroup_Budget2] FOREIGN KEY ([AccountReceivablePreviousValidityBudgetId]) REFERENCES [Budget].[Budget] ([Id]),
    CONSTRAINT [FK_CareGroup_Budget3] FOREIGN KEY ([PortfolioRecoveryBudgetId]) REFERENCES [Budget].[Budget] ([Id]),
    CONSTRAINT [FK_CareGroup_CompanyType] FOREIGN KEY ([EntityType]) REFERENCES [Contract].[CompanyType] ([Id]),
    CONSTRAINT [FK_CareGroup_Contract] FOREIGN KEY ([ContractId]) REFERENCES [Contract].[Contract] ([Id]),
    CONSTRAINT [FK_CareGroup_ContractAccountingStructure] FOREIGN KEY ([ContractAccountingStructureId]) REFERENCES [Contract].[ContractAccountingStructure] ([Id]),
    CONSTRAINT [FK_CareGroup_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_CareGroup_OperatingUnit] FOREIGN KEY ([OperativeUnitId]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_CareGroup_ProcedureTemplate] FOREIGN KEY ([ProcedureTemplateId]) REFERENCES [Contract].[ProcedureTemplate] ([Id]),
    CONSTRAINT [FK_CareGroup_ProductTemplate] FOREIGN KEY ([ProductRateId]) REFERENCES [Inventory].[ProductRate] ([Id]),
    CONSTRAINT [FK_CareGroup_RequirementTemplate] FOREIGN KEY ([RequirementsTemplateId]) REFERENCES [Contract].[RequirementTemplate] ([Id]),
    CONSTRAINT [FK_CareGroup_TechnicalNote] FOREIGN KEY ([TechnicalNoteId]) REFERENCES [Contract].[TechnicalNote] ([Id])
);


GO
ALTER TABLE [Contract].[CareGroup] NOCHECK CONSTRAINT [FK_CareGroup_BillingConceptCopay];


GO
ALTER TABLE [Contract].[CareGroup] NOCHECK CONSTRAINT [FK_CareGroup_Budget];


GO
ALTER TABLE [Contract].[CareGroup] NOCHECK CONSTRAINT [FK_CareGroup_Budget1];


GO
ALTER TABLE [Contract].[CareGroup] NOCHECK CONSTRAINT [FK_CareGroup_Budget2];


GO
ALTER TABLE [Contract].[CareGroup] NOCHECK CONSTRAINT [FK_CareGroup_Budget3];


GO
ALTER TABLE [Contract].[CareGroup] NOCHECK CONSTRAINT [FK_CareGroup_CompanyType];


GO
ALTER TABLE [Contract].[CareGroup] NOCHECK CONSTRAINT [FK_CareGroup_Contract];


GO
ALTER TABLE [Contract].[CareGroup] NOCHECK CONSTRAINT [FK_CareGroup_ContractAccountingStructure];


GO
ALTER TABLE [Contract].[CareGroup] NOCHECK CONSTRAINT [FK_CareGroup_CostCenter];


GO
ALTER TABLE [Contract].[CareGroup] NOCHECK CONSTRAINT [FK_CareGroup_OperatingUnit];


GO
ALTER TABLE [Contract].[CareGroup] NOCHECK CONSTRAINT [FK_CareGroup_ProcedureTemplate];


GO
ALTER TABLE [Contract].[CareGroup] NOCHECK CONSTRAINT [FK_CareGroup_ProductTemplate];


GO
ALTER TABLE [Contract].[CareGroup] NOCHECK CONSTRAINT [FK_CareGroup_RequirementTemplate];


GO
ALTER TABLE [Contract].[CareGroup] NOCHECK CONSTRAINT [FK_CareGroup_TechnicalNote];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_CareGroup__Code]
    ON [Contract].[CareGroup]([Code] ASC);


GO
CREATE INDEX IX_CareGroup_Id_Covering
    ON Contract.CareGroup (Id)
    INCLUDE (Code, Name, EntityType, ContractId);
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si las facturas monto fijo capitación/PGP reportan o no concepto y valor de recaudo. 1 - Recaudo real (Copagos y Cuotas Moderadoras)/Si reporta. 2 - Descuento pactado sobre Monto Fijo (No contable)/No reporta. ', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'MethodFixedAmountCollectionReport';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si debe Registrar Póliza (0 = No, 1 = Sí)', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'RegisterPolicy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de liquidacion  0 - Tercero del grupo de atencion  1 - tercero factura monto fijo', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'ThirdPartyInvoiceCopayFixedAmount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Concepto de facturacion copagos y cuotas moderadoras', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'BillingConceptCopayId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo que determina el tipo para liquidar el Oxigeno: 1 => litros 2 => Horas.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'TypeLiquidationOxygen';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descuento especifico al cliente', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'DiscountContractedCustomer';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica si maneja un tope máximo del tipo, 1 - Si, 0 - No.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'MaximumTypeTop';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo booleano que especifica 0 - No liquida dia de egreso, 1 - Liquida dia de egreso.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'LiquidateDayDischarge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la nota tecnica', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'TechnicalNoteId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id del presupuesto de ingresos que corresponderá al pago de facturas de periodos mucho más antiguos que la anterior vigencia.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'PortfolioRecoveryBudgetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id del presupuesto de ingresos que corresponderá al pago de facturas de la vigencia inmediatamente anterior', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'AccountReceivablePreviousValidityBudgetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permite saber si al realizar una orden de servicio requiere que el no. de autorización sea obligatorio o no', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'AuthorizationRequired';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id del presupuesto de ingresos a usar en el reconocimiento de deudas de paciente', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'PromissoryNoteBudgetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permite saber si aplica a RIAS, este campo se llena siempre y cuando el tipo de grupo de atención es EAPB con Contrato', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'ApplyRIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dispensación Farmacéutica Extramural', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'ExtramuralPharmaceuticalDispensing';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del grupo de atencion  1 - Activo  0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad operativa seleccionada', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'OperativeUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id del presupuesto de ingresos a usar en el reconocimiento de facturas', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'BillingBudgetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el grupo de atencion afecta presupuesto  1 - Si  0- No    Si esta como si, entonces cuando se vaya a radicar la factura este creara un reconocimiento automaticamente', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'AffectBudget';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de estructura contrato de contabilidad ', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'ContractAccountingStructureId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de entidad  Es una llave foranea de CompanyType donde se trae el ID  1 - EPS Contributivo  2 - EPS Subsidiado  3 - ET Vinculados Municipios  4 - ET Vinculados Departamentos  5 - ARL Riesgos Laborales  6 - MP Medicina Prepagada  7 - IPS Privada  8 - IPS Publica  9 - Regimen Especial  10 - Accidentes de transito  11 - Fosyga  12 - Otros  13 - Aseguradoras  99 - Particulares', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'EntityType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concepto a facturar  1 - Plan Obligatorio Salud POS - EPS  2 - Plan Complementario EPS  3 - Plan Subsidiado salud POSS- ARS  4 - Servicios Salud EPS privadas  5 - Empresas medicinas pregagadas  6 - Servicios Salud Compañias Aseguradoras  7 - Servicios Salud Particulares  8 - Servicio Salud IPS publica  9 - Servicio Salud Empresas regimen Especial  10 - Atencion Cargo Subsidio oferta  11 - Riesgos profesionales ARP  12 - Cuota recuperacion vinculados  13 - Atencion Accidentes transito SOAT compañias Seguro  14 - Reclamaciones Fosyga ECAT  15 - Convenios Fosyga Trauma Mayor Desplazados  16 - Min Salud Recursos IVA Social  17 - Otras cuentas por cobrar servicio salud. 18 - Presupuesto Máximo. 19 - Urgencia Población migrante', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'ConceptToBill';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la plantilla de productos', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'ProductRateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la plantilla de procedimientos', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'ProcedureTemplateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Plazo de la factura', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'InvoiceDeadlines';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la plantilla de requerimientos', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'RequirementsTemplateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica las hora de recuperacios que estan incluuidas dentro de una intervencion quirurgica, es decir que despues de que pase este tiempo se podra cobrar la unidad de estancia de lo contrario no se cobrara nada ya que se encuentra incluido dentro de la intervencion', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'HoursOfRecoveryIncluded';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiempo maximo para el cobro de la observacion en estancias, este campo solo se solicita cuando el tipo de liquidacion es SOAT, es decir que solo si las horas de estancas es menor o igual al valor digitado en este campo se podra cobrar la unidad de observacion', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'MaximumObservationTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiempo minimo de liquidacion para la observacion en estancias, este campo solo se solicita si es ISS, es decir que despues de que la estancias supere el valor digitado se va poder liquidar una unidad por concepto de observacion  ', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'MinimumObservationTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de liquidacion de estancias de emergencias  1 - ISS  2 - SOAT', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'TypeLiquidationEmergencyStays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tope maximo de la facturacion por periodo', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'PeriodMaximumBilling';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tope Maximo de facturacion individual', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'MaximumIndividualBilling';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Periodo de facturacion   1 - Semanal  2 - Quincenal  3 - Mensual  4 - BiMensual  5 - Semestral  6 - Anual', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'BillingPeriod';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id del centro de costo, este se pide siempre, ya que si alguna cuenta contable lo requiere se va a obtener de este', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de liquidacion  1 - Pago por Servicios  2 - Capitacion  3 - Factura Global  4 - Capitacion Global  5 - Pago Global Prospectivo - PGP', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'LiquidationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del contrato, este se llena solo si el tipo del grupo de atencion es EAPB con contrato', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'ContractId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si tiene un manual tarifario por defecto  1 - SOAT  2 - ISS  3 - Mixto', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'DefaultManual';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de Grupo de atencion  1 - EAPB Con contrato  2 - EAPB Sin Contrato  3 - Particulares  4 - Aseguradoras', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'CareGroupType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del grupo de atencion', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del grupo de atencion', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del grupo de atencion', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'Id';

