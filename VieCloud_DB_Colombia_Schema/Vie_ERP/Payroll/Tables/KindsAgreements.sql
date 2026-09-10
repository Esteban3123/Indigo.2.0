CREATE TABLE [Payroll].[KindsAgreements] (
    [Id]                           INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                         VARCHAR (20)  NOT NULL,
    [Description]                  VARCHAR (150) NOT NULL,
    [State]                        BIT           NOT NULL,
    [TimeStamp]                    ROWVERSION    NOT NULL,
    [CreationUser]                 VARCHAR (20)  NOT NULL,
    [CreationDate]                 DATETIME      NOT NULL,
    [ModificationUser]             VARCHAR (20)  NULL,
    [ModificationDate]             DATETIME      NULL,
    [IdExpenseConcepts]            INT           NULL,
    [AffectsAccountsReceivable]    BIT           CONSTRAINT [DF__KindsAgre__Affec__33EE883B] DEFAULT ((0)) NOT NULL,
    [ReclassifyAccountsReceivable] BIT           CONSTRAINT [DF__KindsAgre__Recla__34E2AC74] DEFAULT ((0)) NOT NULL,
    [AccountReceivableConceptId]   INT           NULL,
    [AccountId]                    INT           NULL,
    CONSTRAINT [PK_KindsAgreements] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_KindsAgreements_AccountReceivableConcept] FOREIGN KEY ([AccountReceivableConceptId]) REFERENCES [Portfolio].[AccountReceivableConcept] ([Id]),
    CONSTRAINT [FK_KindsAgreements_ExpenseConcepts] FOREIGN KEY ([IdExpenseConcepts]) REFERENCES [Treasury].[ExpenseConcepts] ([Id]),
    CONSTRAINT [FK_KindsAgreements_MainAccounts] FOREIGN KEY ([AccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id])
);




GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK INT a GeneralLedger.MainAccounts. Cuenta contable principal para registrar el impacto del convenio en cuentas por cobrar cuando se reclasifica cartera.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'KindsAgreements', @level2type = N'COLUMN', @level2name = N'AccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable que se usará para relacionar una cuenta contable si reclasifica cuenta por cobrar ', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'KindsAgreements', @level2type = N'COLUMN', @level2name = N'AccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'KindsAgreements', @level2type = N'COLUMN', @level2name = N'AccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK INT a Portfolio.AccountReceivableConcept. Concepto contable para reclasificar cuentas por cobrar cuando ReclassifyAccountsReceivable=1.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'KindsAgreements', @level2type = N'COLUMN', @level2name = N'AccountReceivableConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del concepto de cuenta por cobrar que se usará para reclasificar la cuenta por cobrar', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'KindsAgreements', @level2type = N'COLUMN', @level2name = N'AccountReceivableConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'KindsAgreements', @level2type = N'COLUMN', @level2name = N'AccountReceivableConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT que especifica si el tipo de convenio requiere reclasificación de cuentas por cobrar. Activa la reubicación contable de cartera.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'KindsAgreements', @level2type = N'COLUMN', @level2name = N'ReclassifyAccountsReceivable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo indica si se reclasifica una cuenta por cobrar.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'KindsAgreements', @level2type = N'COLUMN', @level2name = N'ReclassifyAccountsReceivable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'KindsAgreements', @level2type = N'COLUMN', @level2name = N'ReclassifyAccountsReceivable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT (0=No afecta, 1=Afecta) que determina si el tipo de convenio impacta las cuentas por cobrar. Controla si el acuerdo afecta cartera/deudas.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'KindsAgreements', @level2type = N'COLUMN', @level2name = N'AffectsAccountsReceivable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo indica si el convenio afecta o no una cuenta por cobrar:  0 - No afecta  1 - Afecta', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'KindsAgreements', @level2type = N'COLUMN', @level2name = N'AffectsAccountsReceivable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'KindsAgreements', @level2type = N'COLUMN', @level2name = N'AffectsAccountsReceivable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK INT a Treasury.ExpenseConcepts. Concepto de egreso/gasto de tesorería para distribución de gastos de nómina. Típicamente ''''Ninguno'''' en la mayoría de casos.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'KindsAgreements', @level2type = N'COLUMN', @level2name = N'IdExpenseConcepts';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de Concepto de Egreso de Tesoreria para la distribución de Gastos de Nómina. Únicamente se listan los tipos "Ninguno"', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'KindsAgreements', @level2type = N'COLUMN', @level2name = N'IdExpenseConcepts';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'KindsAgreements', @level2type = N'COLUMN', @level2name = N'IdExpenseConcepts';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha DATETIME de última modificación del tipo de convenio. Registra cuándo se actualizó el acuerdo (NULL si no hay cambios posteriores).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'KindsAgreements', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'KindsAgreements', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'KindsAgreements', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario VARCHAR(20) que modificó el registro. Identifica quién editó últimamente el tipo de convenio (NULL si nunca fue modificado).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'KindsAgreements', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'KindsAgreements', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'KindsAgreements', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha DATETIME de creación del tipo de convenio. Marca cuándo se registró por primera vez el acuerdo en el sistema de nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'KindsAgreements', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'KindsAgreements', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'KindsAgreements', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario VARCHAR(20) que creó el registro del tipo de convenio. Identifica quién registró el acuerdo en el sistema.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'KindsAgreements', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'KindsAgreements', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'KindsAgreements', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal TIMESTAMP que registra el instante exacto de creación, modificación o evento en la tabla. Útil para auditoría y sincronización.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'KindsAgreements', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'KindsAgreements', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'KindsAgreements', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado BIT (0=Inactivo, 1=Activo) del tipo de convenio. Indica si el acuerdo está vigente o deshabilitado en el sistema de nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'KindsAgreements', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'KindsAgreements', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'KindsAgreements', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción VARCHAR(150) del tipo de convenio/acuerdo. Detalle textual que especifica la naturaleza y características del acuerdo laboral o convenio.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'KindsAgreements', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'KindsAgreements', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'KindsAgreements', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código VARCHAR(20) del tipo de convenio o acuerdo. Identificador único legible para búsqueda y clasificación de acuerdos laborales en nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'KindsAgreements', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'KindsAgreements', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'KindsAgreements', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental (INT IDENTITY) de la tabla KindsAgreements. Clave primaria para referenciar tipos de convenios/acuerdos laborales.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'KindsAgreements', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'KindsAgreements', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'KindsAgreements', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipos o clases de convenios de nómina (por ejemplo, descuentos por libranza, acuerdos sindicales, embargos u otros pactos laborales), incluyendo si afectan o reclasifican cuentas por cobrar y su concepto contable asociado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'KindsAgreements';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'KindsAgreements';
