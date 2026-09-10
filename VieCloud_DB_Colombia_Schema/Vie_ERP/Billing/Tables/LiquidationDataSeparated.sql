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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor límite máximo de coaseguro del paciente, tope de participación en costos compartidos (NUMERIC 20,2)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'PatientCoinsuranceLimitValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor límite del seguro del paciente.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'PatientCoinsuranceLimitValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'PatientCoinsuranceLimitValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor cubierto por la aseguradora, equivalente a copago de la aseguradora o participación del asegurador (NUMERIC 20,2)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'InsurerCoveredValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor cubierto Aseguradora:                   es como el Copago de la aseguradora', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'InsurerCoveredValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'InsurerCoveredValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del límite general máximo aplicable a la factura, distribuido entre todos los servicios del ingreso (NUMERIC 20,2)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'LimitValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del limite general, limite maximo que puede ser aplicado a la aseguradora. se distribuyen en todo los items', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'LimitValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'LimitValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador si incluye IVA/impuestos: 0=No incluye, 1=Incluye (BIT, default 0)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'TaxInclude';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Incluye IVA: 0 false(No); 1 true (si)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'TaxInclude';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'TaxInclude';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de aplicación de límites generales: 0=No aplica, 1=Aplica (BIT, default 0)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'ApplyGeneralLimits';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Aplica limites Generales:0 false(no); 1 True (si)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'ApplyGeneralLimits';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'ApplyGeneralLimits';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de última modificación del registro de liquidación separada (DATETIME)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación del registro (VARCHAR 20, auditoria)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de liquidación separada (DATETIME)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro de liquidación separada (VARCHAR 20, auditoria)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de coaseguro a cargo del paciente, participación compartida del usuario (NUMERIC 5,2)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'PatientCoinsurance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'% Coaseguro paciente', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'PatientCoinsurance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'PatientCoinsurance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de coaseguro a cargo de la aseguradora, participación del asegurador (NUMERIC 5,2)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'InsuranceCoinsurance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'% Coaseguro aseguradora', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'InsuranceCoinsurance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'InsuranceCoinsurance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del copago, monto fijo que debe abonar el paciente por la atención/servicio (NUMERIC 20,2)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'CopaymentValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor copago', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'CopaymentValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'CopaymentValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de aplicación del deducible: 0=Antes de coaseguro, 1=Después de coaseguro (BIT)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'ApplyDeductible';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Aplicar deducible : 0-Antes de coaseguro, 1-Después de coaseguro', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'ApplyDeductible';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'ApplyDeductible';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del deducible, cantidad que paciente paga antes de cobertura del asegurador (NUMERIC 20,2)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'DeductibleValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del deducible', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'DeductibleValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'DeductibleValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del descuento o porcentaje según tipo definido en ApplyDiscount (NUMERIC 20,2, NULL)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'DiscountValueorPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el valor del descuento o el porcentaje en funcion del campo anterior', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'DiscountValueorPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'DiscountValueorPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de descuento a aplicar: 1=Porcentual, 2=Valor absoluto, 3=Contratado, 4=Ninguno (TINYINT)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'ApplyDiscount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Aplicar descuento :                   1. Porcentual                   2. Valor                   3. Contratado                   4. Ninguno', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'ApplyDiscount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'ApplyDiscount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso del paciente, identificador de la atención/admisión (CHAR 10, FK)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'# de ingreso del paciente', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la tabla, clave primaria de liquidación separada (INT IDENTITY)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parámetros de liquidación separada por admisión: descuentos, deducibles, copagos, coaseguros y límites de cobertura aplicados a cada ingreso del paciente para la facturación y conciliación con aseguradoras.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataSeparated';

GO
CREATE NONCLUSTERED INDEX [IX_LiquidationDataSeparated_AdmissionNumber]
    ON [Billing].[LiquidationDataSeparated]([AdmissionNumber] ASC);
