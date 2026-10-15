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
ALTER TABLE [Payroll].[Foreclousure] NOCHECK CONSTRAINT [FK_Foreclousure_City];


GO
ALTER TABLE [Payroll].[Foreclousure] NOCHECK CONSTRAINT [FK_Foreclousure_Company];


GO
ALTER TABLE [Payroll].[Foreclousure] NOCHECK CONSTRAINT [FK_Foreclousure_Concept];


GO
ALTER TABLE [Payroll].[Foreclousure] NOCHECK CONSTRAINT [FK_Foreclousure_Contract];


GO
ALTER TABLE [Payroll].[Foreclousure] NOCHECK CONSTRAINT [FK_Foreclousure_Employee];


GO
ALTER TABLE [Payroll].[Foreclousure] NOCHECK CONSTRAINT [FK_Foreclousure_ThirdParty];


GO
ALTER TABLE [Payroll].[Foreclousure] NOCHECK CONSTRAINT [FK_Foreclousure_ThirdParty1];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Comentario de Suspensión', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'SupendComment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de Suspensión', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'SuspendUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Suspensión', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'SuspendDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Comentario de Anulación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'AnnulmentComment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de Anulación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Anulación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de Confirmación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Confirmación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de Proceso', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'ProcessNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Tercero de Beneficiaro', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'IdBeneficiary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del Oficio', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'TradeDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de Oficio', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'TradeNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código Oficina Destino Banco Agrario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'CodeDestinationOffice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de Ciudad', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'IdCity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Juzgado (Empresa)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'IdJudgment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Afecta Valor de Cesantias', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'AffectUnemploymentValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Afecta Prima 2do Semestre', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'Affect2doIncentivePayment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Afecta Prima 1er Semestre', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'Affect1erIncentivePayment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Afecta Vacaciones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'AffectVacation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Inicial', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'InitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'IdConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Saldo del Embargo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'CurrentBalance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Total', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'TotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'Percentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de Cuotas', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'QuoteNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la Cuota', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'QuoteValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clase de Descuento:  1. Porcentaje  2. Valor Fijo  3. Valor Fijo con Saldo  4. Porcentaje con Saldo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'DiscountClass';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clase de Descuento:  1. Porcentaje  2. Valor Fijo  3. Valor Fijo con Saldo  4. Porcentaje con Saldo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'DiscountType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Tercero Demandante', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'IdApplicant';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado:  1. Registrado   - 2. Confirmado   - 3. Suspendido -   4. Anulado - 5. Terminado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'Comment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Embargo  1. Embargos por Obligaciones Comunes  2. Embargos por Obligaciones Alimentarias y/o Cooperativas  3. Embargos Judiciales de Prestaciones Sociales', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'ForeclousureType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Contrato', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'IdContract';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contrato Base', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'InitialContract';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'IdEmployee';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'Id';

