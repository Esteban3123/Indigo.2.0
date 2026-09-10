CREATE TABLE [Payroll].[Concept] (
    [Id]                                INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                              VARCHAR (4)   NOT NULL,
    [ConceptClass]                      CHAR (3)      NOT NULL,
    [ConceptType]                       TINYINT       NOT NULL,
    [Name]                              VARCHAR (50)  NOT NULL,
    [AffectIBC]                         BIT           CONSTRAINT [DF_Concept_AffectIBC] DEFAULT ((0)) NULL,
    [AffectIBCRTF]                      BIT           CONSTRAINT [DF_Concept_AffectIBCRTF] DEFAULT ((0)) NULL,
    [AffectIBCSENA]                     BIT           CONSTRAINT [DF_Concept_AffectIBCSENA] DEFAULT ((0)) NULL,
    [AffectIBCICBF]                     BIT           CONSTRAINT [DF_Concept_AffectIBCICBF] DEFAULT ((0)) NULL,
    [AffectIBCCompensationFund]         BIT           CONSTRAINT [DF_Concept_AffectIBCCompensationFund] DEFAULT ((0)) NULL,
    [AffectIBCSeverance]                BIT           CONSTRAINT [DF_Concept_AffectIBCSeverance] DEFAULT ((0)) NULL,
    [AffectIBCHealth]                   BIT           CONSTRAINT [DF_Concept_AffectIBCHealth] DEFAULT ((0)) NULL,
    [AffectIBCPension]                  BIT           CONSTRAINT [DF_Concept_AffectIBCPension] DEFAULT ((0)) NULL,
    [AffectIBCARP]                      BIT           CONSTRAINT [DF_Concept_AffectIBCARP] DEFAULT ((0)) NULL,
    [AffectIBCVacation]                 BIT           CONSTRAINT [DF_Concept_AffectIBCVacation] DEFAULT ((0)) NULL,
    [AffectIBCIncentivePayment]         BIT           CONSTRAINT [DF_Concept_AffectIBCIncentivePayment] DEFAULT ((0)) NULL,
    [AffectRetroactive]                 BIT           CONSTRAINT [DF_Concept_AffectRetroactive] DEFAULT ((0)) NULL,
    [Formulates]                        VARCHAR (MAX) NOT NULL,
    [State]                             BIT           NOT NULL,
    [CreationUser]                      VARCHAR (20)  NOT NULL,
    [CreationDate]                      DATETIME      NOT NULL,
    [ModificationUser]                  VARCHAR (20)  NULL,
    [ModificationDate]                  DATETIME      NULL,
    [TimeStamp]                         ROWVERSION    NOT NULL,
    [IdAdjustmentConcept]               INT           NULL,
    [IdElectronicPayrollConcepts]       INT           NULL,
    [IdElectronicPayrollConceptSubtype] INT           NULL,
    [AffectLimit40Law1393]              BIT           CONSTRAINT [DF_Concept_AffectLimit40Law1393] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Concept__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Concept_Concept] FOREIGN KEY ([IdAdjustmentConcept]) REFERENCES [Payroll].[Concept] ([Id]),
    CONSTRAINT [FK_Concept_ElectronicPayrollConcepts] FOREIGN KEY ([IdElectronicPayrollConcepts]) REFERENCES [Payroll].[ElectronicPayrollConcepts] ([Id]),
    CONSTRAINT [FK_Concept_ElectronicPayrollConceptSubtype] FOREIGN KEY ([IdElectronicPayrollConceptSubtype]) REFERENCES [Payroll].[ElectronicPayrollConceptSubtype] ([Id]) ON UPDATE CASCADE
);




GO





GO


GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_Concept__Code]
    ON [Payroll].[Concept]([Code] ASC);

GO
CREATE NONCLUSTERED INDEX [IX_Concept__IdElectronicPayrollConcepts]
    ON [Payroll].[Concept] ([IdElectronicPayrollConcepts] ASC)
    INCLUDE ([ConceptClass], [Name], [IdElectronicPayrollConceptSubtype]);

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK (INT) a Concept.Id: referencia el concepto de ajuste para recargos, horas extras ordinarias/nocturnas/dominicales y ajustes retroactivos, nullable.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'IdAdjustmentConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Concepto de Ajuste (Se utilizará para Recargos y Horas Extras)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'IdAdjustmentConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'IdAdjustmentConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal binaria (TIMESTAMP) de auditoría: captura automática del instante de creación, modificación o cambio de estado del registro.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de la última modificación del concepto, nullable.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR 20) que realizó la última modificación del concepto, nullable.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) en que se creó el concepto en base de datos.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR 20) que creó el registro del concepto en el sistema.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del concepto (BIT): 1=Activo (en uso), 0=Inactivo (no aplica en nueva nómina).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado 1-activo 0-inactivo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fórmula de cálculo del concepto (VARCHAR MAX): expresión para determinar el valor dinámico.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'Formulates';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Formula del concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'Formulates';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'Formulates';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Flag (BIT): indica si el concepto aplica retroactivamente (ajustes a períodos anteriores).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'AffectRetroactive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Afecta Retroactivo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'AffectRetroactive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'AffectRetroactive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Flag (BIT): indica si el concepto afecta IBC para Prima de Servicios (bono anual).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'AffectIBCIncentivePayment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Afecta IBC Primas', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'AffectIBCIncentivePayment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'AffectIBCIncentivePayment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Flag (BIT): indica si el concepto afecta IBC para cálculo de Vacaciones (descanso remunerado).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'AffectIBCVacation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Afecta IBC Vacaciones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'AffectIBCVacation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'AffectIBCVacation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Flag (BIT): indica si el concepto afecta IBC para aporte a Riesgos Profesionales (ARP/seguro laboral).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'AffectIBCARP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Afecta IBC ARP', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'AffectIBCARP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'AffectIBCARP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Flag (BIT): indica si el concepto afecta IBC para aporte a Pensión (AFP/fondo pensional).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'AffectIBCPension';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Afecta IBC Pension', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'AffectIBCPension';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'AffectIBCPension';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Flag (BIT): indica si el concepto afecta IBC para aporte a Salud (EPS/Seguro médico).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'AffectIBCHealth';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Affecta IBC Salud', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'AffectIBCHealth';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'AffectIBCHealth';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Flag (BIT): indica si el concepto afecta IBC para cálculo de Cesantías (indemnización por despido).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'AffectIBCSeverance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Afecta IBC Cesantias', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'AffectIBCSeverance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'AffectIBCSeverance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Flag (BIT): indica si el concepto afecta IBC para aporte a Caja de Compensación Familiar.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'AffectIBCCompensationFund';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Afecta IBC Caja de Compensación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'AffectIBCCompensationFund';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'AffectIBCCompensationFund';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Flag (BIT): indica si el concepto afecta IBC para aporte parafiscal ICBF (familias, bienestar).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'AffectIBCICBF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Afecta IBC ICBF', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'AffectIBCICBF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'AffectIBCICBF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Flag (BIT): indica si el concepto afecta IBC para aporte parafiscal al SENA.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'AffectIBCSENA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Afecta IBC SENA', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'AffectIBCSENA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'AffectIBCSENA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Flag (BIT): indica si el concepto afecta IBC para Riesgos Profesionales (RTF/ARP).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'AffectIBCRTF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Afecta IBC RTF', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'AffectIBCRTF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'AffectIBCRTF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Flag (BIT): indica si el concepto afecta la Base de Cotización (IBC) general, base para aportes a seguridad social.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'AffectIBC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Afecta IBC', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'AffectIBC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'AffectIBC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo del concepto de nómina (máx. 50 caracteres), ej: Sueldo, Hora Extra, Prima, Pensión, Salud.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de concepto (TINYINT): 1=Devengado (ingreso), 2=Deducido (descuento), 3=Patronal (aporte empresa).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'ConceptType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de concepto 1-Devengado 2-Deducido 3-Patronal', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'ConceptType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'ConceptType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación del concepto en 3 dígitos (001-073): define tipo de devengado, deducido, patronal, hora extra, prima, cesantía, pensión, salud, ARP, SENA, ICBF, RTF, licencia, vacación, bonificación, provisional, recargo, embargo, calamidad, incapacidad.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'ConceptClass';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Define la Clase de Concepto:  
001 - Hora Extra Ordinaria Diurna  
002 - Primas de Servicios  
003 - Otras Primas  
004 - Bonificación por Servicios  
005 - Sueldo  
006 - Auxilio de Transporte  
007 - Indemnizaciones  
008 - Provisión Cesantías  
009 - Aporte Riesgos Profesionales  
010 - Otros Devengados  
011 - Otros Deducidos  
012 - Hora Extra Ordinaria Nocturna  
013 - Hora Extra Dominical Diurna  
014 - Pensión Empleado  
015 - Pensión Patrono  
016 - Pensión Voluntaria  
017 - Salud Empleado  
018 - Salud Patrono  
019 - Salud Voluntaria  
020 - Retención  
021 - Incapacidad Ambulatoria  
022 - Incapacidad Hospitalaria  
023 - Maternidad  
024 - Licencias  
025 - Licencia No Remunerada  
026 - Sanción  
027 - Incapacidad Riesgos Profesionales  
028 - Permisos  
030 - Vacaciones  
031 - Provisión Vacaciones  
033 - Provisión Primas  
034 - Provisión Interés de Cesantías  
035 - Parafiscal Sena  
036 - Parafiscal Caja  
037 - Parafiscal ICBF  
038 - Aporte Fondo Seguridad Pensional  
041 - Convenios  
042 - Recargo Nocturno Normal  
043 - Recargo Nocturno Festivo  
044 - Sindicato  
045 - Cuentas AFC  
046 - Bonificación por Año de Servicio  
047 - Gastos de Representación  
048 - Ajuste Retención en la Fuente  
049 - Prima de Vacaciones  
050 - Hora Extra Dominical Nocturna  
051 - Recargo Diurno Festivo  
052 - Recargo Diurno Dominical  
053 - Embargos  
054 - Viáticos  
055 - Bonificación Salarial  
056 - Provisión Bonificación Año Servicio  
057 - Provisión Incremento Vacacional  
058 - Provisión Bonificación Especial Recreación  
059 - Provisión Prima Vacaciones  
060 - Provisión Prima Navidad  
061 - Retención de Indemnización  
062 - Auxilio de Alimentos  
063 - Bonificación Especial para Recreación  
064 - Incremento Vacacional  
065 - Bonificación No Salarial  
066 - Apoyo a Sostenimiento
067 - Incapacidad Ambulatoria Patrono
068 - Incapacidad Ambulatoria ERP
069 - Incapacidad Hospitalaria Patrono
070 - Incapacidad Hospitalaria ERP
071 - Calamidad domestica
072 - Licencia Remunerada
073 - Vacaciones en Dinero', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'ConceptClass';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'ConceptClass';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código alfanumérico de 4 caracteres del concepto de nómina, identificador funcional único.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del concepto de nómina en el sistema de nomina Indigo Vie Cloud.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del concepto de nomina', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'Id';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK (INT) a ElectronicPayrollConcepts.Id: relación con conceptos electrónicos RIPS/payroll electrónico (Colombia), nullable.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'IdElectronicPayrollConcepts';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id Relacion Payroll.ElectronicPayrollConcepts', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'IdElectronicPayrollConcepts';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'IdElectronicPayrollConcepts';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Flag (BIT): indica si el concepto afecta o limita el tope del 40% de prestaciones sociales según Ley 1393 de Colombia.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'AffectLimit40Law1393';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Afecta Límite 40% Ley 1393 (Colombia)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'AffectLimit40Law1393';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'AffectLimit40Law1393';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de conceptos de nómina (devengados, deducciones y aportes) que define cómo cada concepto afecta las bases de cotización (IBC) para salud, pensión, ARL, SENA, ICBF, caja de compensación, cesantías y vacaciones, así como su fórmula de cálculo y su equivalencia con los conceptos de nómina electrónica DIAN.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Subtipo de concepto de nómina electrónica DIAN al que pertenece este concepto; permite clasificar el concepto dentro de la estructura de nómina electrónica exigida por la DIAN para la facturación electrónica de nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'IdElectronicPayrollConceptSubtype';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'IdElectronicPayrollConceptSubtype';
