CREATE TABLE [Payroll].[Foreclousure] (
    [Id]                        INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                      VARCHAR (20)   NOT NULL,
    [IdEmployee]                INT            NOT NULL,
    [InitialContract]           INT            NOT NULL,
    [IdContract]                INT            NOT NULL,
    [ForeclousureType]          TINYINT        NOT NULL,
    [Comment]                   VARCHAR (500)  NULL,
    [State]                     TINYINT        NOT NULL,
    [IdApplicant]               INT            NOT NULL,
    [DiscountType]              TINYINT        NOT NULL,
    [DiscountClass]             TINYINT        NOT NULL,
    [QuoteValue]                NUMERIC (18)   NULL,
    [QuoteNumber]               INT            NULL,
    [Percentage]                DECIMAL (6, 2) NULL,
    [TotalValue]                NUMERIC (18)   NULL,
    [CurrentBalance]            NUMERIC (18)   NULL,
    [IdConcept]                 INT            NOT NULL,
    [InitialDate]               DATE           NOT NULL,
    [AffectVacation]            BIT            NOT NULL,
    [Affect1erIncentivePayment] BIT            NOT NULL,
    [Affect2doIncentivePayment] BIT            NOT NULL,
    [AffectUnemploymentValue]   BIT            NOT NULL,
    [IdJudgment]                INT            NOT NULL,
    [IdCity]                    INT            NOT NULL,
    [CodeDestinationOffice]     VARCHAR (5)    CONSTRAINT [DF_Foreclousure_CodeDestinationOffice] DEFAULT ((0)) NOT NULL,
    [TradeNumber]               VARCHAR (100)  NOT NULL,
    [TradeDate]                 DATE           NOT NULL,
    [IdBeneficiary]             INT            NOT NULL,
    [ProcessNumber]             VARCHAR (100)  NOT NULL,
    [CreationDate]              DATE           NOT NULL,
    [CreationUser]              VARCHAR (20)   NOT NULL,
    [ConfirmationDate]          DATE           NULL,
    [ConfirmationUser]          VARCHAR (20)   NULL,
    [AnnulmentDate]             DATE           NULL,
    [AnnulmentUser]             VARCHAR (20)   NULL,
    [AnnulmentComment]          VARCHAR (100)  NULL,
    [SuspendDate]               DATE           NULL,
    [SuspendUser]               VARCHAR (20)   NULL,
    [SupendComment]             VARCHAR (100)  NULL,
    [TimeStamp]                 ROWVERSION     NOT NULL,
    CONSTRAINT [PK_Foreclousure] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Foreclousure_City] FOREIGN KEY ([IdCity]) REFERENCES [Common].[City] ([Id]),
    CONSTRAINT [FK_Foreclousure_Company] FOREIGN KEY ([IdJudgment]) REFERENCES [Payroll].[Company] ([Id]),
    CONSTRAINT [FK_Foreclousure_Concept] FOREIGN KEY ([IdConcept]) REFERENCES [Payroll].[Concept] ([Id]),
    CONSTRAINT [FK_Foreclousure_Contract] FOREIGN KEY ([IdContract]) REFERENCES [Payroll].[Contract] ([Id]),
    CONSTRAINT [FK_Foreclousure_Employee] FOREIGN KEY ([IdEmployee]) REFERENCES [Payroll].[Employee] ([Id]),
    CONSTRAINT [FK_Foreclousure_ThirdParty] FOREIGN KEY ([IdApplicant]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [FK_Foreclousure_ThirdParty1] FOREIGN KEY ([IdBeneficiary]) REFERENCES [Common].[ThirdParty] ([Id])
);




GO



GO



GO





GO



GO



GO



GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal TIMESTAMP del evento de embargo (creación, registro, modificación, confirmación, anulación o suspensión). Registra el instante exacto de cambio en la base de datos.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Comentario VARCHAR(100) descriptivo de la causa o motivo de suspensión del embargo, anotación del usuario que suspendió.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'SupendComment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Comentario de Suspensión', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'SupendComment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'SupendComment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario VARCHAR(20) que ejecutó la suspensión del embargo. Auditoría de quién suspendió el trámite.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'SuspendUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de Suspensión', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'SuspendUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'SuspendUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha DATE de suspensión del embargo. Marca cuándo se pausó el descuento de nómina del empleado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'SuspendDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Suspensión', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'SuspendDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'SuspendDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Comentario VARCHAR(100) que justifica la anulación del embargo. Razón de rechazo o cancelación del trámite.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'AnnulmentComment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Comentario de Anulación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'AnnulmentComment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'AnnulmentComment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario VARCHAR(20) que anuló el embargo. Auditoría de autorización de cancelación.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de Anulación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha DATE de anulación del embargo. Marca cuándo se revocó completamente el embargo.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Anulación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario VARCHAR(20) que confirmó y validó el embargo en el sistema. Auditoría de aprobación.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de Confirmación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha DATE de confirmación del embargo. Marca cuándo se validó y activó el descuento en nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Confirmación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario VARCHAR(20) que registró el embargo inicial en el sistema. Auditoría de creación.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha DATE de creación del registro de embargo. Inicio del trámite en el ERP Indigo Vie Cloud.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de proceso judicial VARCHAR(100). Identificador único del caso legal o sentencia de embargo.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'ProcessNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de Proceso', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'ProcessNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'ProcessNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID INT del tercero (ThirdParty) beneficiario del embargo. FK a [Common].[ThirdParty]. Quién recibirá el valor embargado (acreedor/demandante).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'IdBeneficiary';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Tercero de Beneficiaro', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'IdBeneficiary';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'IdBeneficiary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha DATE del oficio judicial que ordena el embargo. Fecha de radicación de la orden legal.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'TradeDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del Oficio', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'TradeDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'TradeDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de oficio VARCHAR(100) emitido por el juzgado/tribunal. Identificador único del documento judicial.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'TradeNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de Oficio', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'TradeNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'TradeNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código VARCHAR(5) de oficina destino en Banco Agrario (DF=0). Ruta bancaria para giro de valores embargados.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'CodeDestinationOffice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código Oficina Destino Banco Agrario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'CodeDestinationOffice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'CodeDestinationOffice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID INT de ciudad donde se radicó el proceso judicial. FK a [Common].[City]. Jurisdicción del juzgado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'IdCity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de Ciudad', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'IdCity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'IdCity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID INT del juzgado o tribunal (empresa) que ordena el embargo. FK a [Payroll].[Company]. Autoridad judicial responsable.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'IdJudgment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Juzgado (Empresa)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'IdJudgment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'IdJudgment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT indicador si el embargo afecta el valor de cesantías/prestación por desempleo del empleado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'AffectUnemploymentValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Afecta Valor de Cesantias', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'AffectUnemploymentValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'AffectUnemploymentValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT indicador si el embargo descuenta de la prima o incentivo del segundo semestre del empleado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'Affect2doIncentivePayment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Afecta Prima 2do Semestre', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'Affect2doIncentivePayment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'Affect2doIncentivePayment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT indicador si el embargo descuenta de la prima o incentivo del primer semestre del empleado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'Affect1erIncentivePayment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Afecta Prima 1er Semestre', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'Affect1erIncentivePayment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'Affect1erIncentivePayment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT indicador si el embargo afecta el pago de vacaciones acumuladas del empleado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'AffectVacation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Afecta Vacaciones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'AffectVacation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'AffectVacation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha DATE de inicio del embargo. Fecha a partir de la cual comienza el descuento en nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'InitialDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Inicial', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'InitialDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'InitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID INT del concepto de nómina que genera el descuento. FK a [Payroll].[Concept]. Tipo de rubro a descontar.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'IdConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'IdConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'IdConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto NUMERIC(18) de saldo pendiente del embargo. Valor aún no cobrado o descontado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'CurrentBalance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Saldo del Embargo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'CurrentBalance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'CurrentBalance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total NUMERIC(18) del embargo ordenado. Monto completo que debe descontarse en nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'TotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Total', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'TotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'TotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje DECIMAL(6,2) de descuento aplicable sobre salario o concepto. Ej: 10%, 15%, etc.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'Percentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'Percentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'Percentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número INT de cuotas en que se divide el embargo. Cantidad de períodos de pago para cobrar el embargo.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'QuoteNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de Cuotas', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'QuoteNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'QuoteNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor NUMERIC(18) de cada cuota del embargo. Monto fijo a descontar por período de pago.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'QuoteValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la Cuota', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'QuoteValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'QuoteValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación TINYINT del tipo de descuento: 1=Porcentaje, 2=Valor Fijo, 3=Valor Fijo con Saldo, 4=Porcentaje con Saldo.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'DiscountClass';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clase de Descuento:  1. Porcentaje  2. Valor Fijo  3. Valor Fijo con Saldo  4. Porcentaje con Saldo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'DiscountClass';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'DiscountClass';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo TINYINT de descuento aplicado al embargo: 1=Porcentaje, 2=Valor Fijo, 3=Valor Fijo con Saldo, 4=Porcentaje con Saldo.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'DiscountType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clase de Descuento:  1. Porcentaje  2. Valor Fijo  3. Valor Fijo con Saldo  4. Porcentaje con Saldo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'DiscountType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'DiscountType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID INT del tercero demandante (acreedor/solicitante). FK a [Common].[ThirdParty]. Quién inicia el embargo.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'IdApplicant';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Tercero Demandante', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'IdApplicant';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'IdApplicant';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado TINYINT del embargo: 1=Registrado, 2=Confirmado, 3=Suspendido, 4=Anulado, 5=Terminado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado:  1. Registrado   - 2. Confirmado   - 3. Suspendido -   4. Anulado - 5. Terminado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observación VARCHAR(500) o nota adicional sobre el embargo. Campo libre para anotaciones del usuario.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'Comment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'Comment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'Comment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo TINYINT de embargo: 1=Obligaciones Comunes, 2=Obligaciones Alimentarias/Cooperativas, 3=Embargos Judiciales de Prestaciones Sociales.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'ForeclousureType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Embargo  1. Embargos por Obligaciones Comunes  2. Embargos por Obligaciones Alimentarias y/o Cooperativas  3. Embargos Judiciales de Prestaciones Sociales', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'ForeclousureType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'ForeclousureType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID INT del contrato del empleado afectado por embargo. FK a [Payroll].[Contract]. Vínculo laboral sujeto a descuento.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'IdContract';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Contrato', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'IdContract';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'IdContract';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID INT del contrato base o inicial del empleado. Contrato origen antes de modificaciones.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'InitialContract';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contrato Base', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'InitialContract';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'InitialContract';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID INT del empleado sujeto a embargo. FK a [Payroll].[Employee]. Trabajador que sufre descuento en nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'IdEmployee';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'IdEmployee';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'IdEmployee';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código VARCHAR(20) único del embargo. Identificador legible del registro de embargo.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID INT autonumérico e identidad única de la tabla [Payroll].[Foreclousure]. Clave primaria IDENTITY(1,1).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Embargos y descuentos judiciales o administrativos aplicados a empleados en nómina. Registra cada embargo (libranza, cuota alimentaria, crédito, etc.) con su tipo, valor, cuotas, saldo vigente, concepto de descuento, beneficiario y el expediente o proceso judicial que lo origina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure';
