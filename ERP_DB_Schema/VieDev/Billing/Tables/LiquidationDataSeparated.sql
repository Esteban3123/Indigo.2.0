CREATE TABLE [Billing].[LiquidationDataSeparated] (
    [Id]                           INT             IDENTITY (1, 1) NOT NULL,
    [AdmissionNumber]              CHAR (10)       NOT NULL,
    [ApplyDiscount]                TINYINT         NOT NULL,
    [DiscountValueorPercentage]    NUMERIC (20, 2) NULL,
    [DeductibleValue]              NUMERIC (20, 2) NOT NULL,
    [ApplyDeductible]              BIT             NOT NULL,
    [CopaymentValue]               NUMERIC (20, 2) NOT NULL,
    [InsuranceCoinsurance]         NUMERIC (5, 2)  NOT NULL,
    [PatientCoinsurance]           NUMERIC (5, 2)  NOT NULL,
    [CreationUser]                 VARCHAR (20)    NOT NULL,
    [CreationDate]                 DATETIME        NOT NULL,
    [ModificationUser]             VARCHAR (20)    NULL,
    [ModificationDate]             DATETIME        NULL,
    [ApplyGeneralLimits]           BIT             CONSTRAINT [DF__Liquidati__Apply__777CD8DD] DEFAULT ((0)) NOT NULL,
    [TaxInclude]                   BIT             CONSTRAINT [DF__Liquidati__TaxIn__7870FD16] DEFAULT ((0)) NOT NULL,
    [LimitValue]                   NUMERIC (20, 2) CONSTRAINT [DF__Liquidati__Limit__7965214F] DEFAULT ((0)) NOT NULL,
    [InsurerCoveredValue]          NUMERIC (20, 2) CONSTRAINT [DF__Liquidati__Insur__7A594588] DEFAULT ((0)) NOT NULL,
    [PatientCoinsuranceLimitValue] NUMERIC (20, 2) CONSTRAINT [DF__Liquidati__Patie__7B4D69C1] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_LiquidationDataSeparated] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor límite del seguro del paciente.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'PatientCoinsuranceLimitValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor cubierto Aseguradora:                   es como el Copago de la aseguradora', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'InsurerCoveredValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del limite general, limite maximo que puede ser aplicado a la aseguradora. se distribuyen en todo los items', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'LimitValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Incluye IVA: 0 false(No); 1 true (si)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'TaxInclude';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Aplica limites Generales:0 false(no); 1 True (si)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'ApplyGeneralLimits';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '% Coaseguro paciente', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'PatientCoinsurance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '% Coaseguro aseguradora', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'InsuranceCoinsurance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor copago', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'CopaymentValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Aplicar deducible : 0-Antes de coaseguro, 1-Después de coaseguro', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'ApplyDeductible';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del deducible', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'DeductibleValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Guarda el valor del descuento o el porcentaje en funcion del campo anterior', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'DiscountValueorPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Aplicar descuento :                   1. Porcentual                   2. Valor                   3. Contratado                   4. Ninguno', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'ApplyDiscount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '# de ingreso del paciente', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'Id';

