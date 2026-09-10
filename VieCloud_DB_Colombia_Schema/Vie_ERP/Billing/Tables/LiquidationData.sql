CREATE TABLE [Billing].[LiquidationData] (
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
    [ApplyGeneralLimits]           BIT             CONSTRAINT [DF__Liquidati__Apply__0C37FEA1] DEFAULT ((0)) NOT NULL,
    [TaxInclude]                   BIT             CONSTRAINT [DF__Liquidati__TaxIn__0D2C22DA] DEFAULT ((0)) NOT NULL,
    [LimitValue]                   NUMERIC (20, 2) CONSTRAINT [DF__Liquidati__Limit__0E204713] DEFAULT ((0)) NOT NULL,
    [InsurerCoveredValue]          NUMERIC (20, 2) CONSTRAINT [DF__Liquidati__Insur__6A6CF682] DEFAULT ((0)) NOT NULL,
    [PatientCoinsuranceLimitValue] NUMERIC (20, 2) CONSTRAINT [DF__Liquidati__Patie__6B611ABB] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_LiquidationData] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor límite máximo de coaseguro del paciente (porcentaje de responsabilidad). NUMERIC(20,2), default 0.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationData', @level2type = N'COLUMN', @level2name = N'PatientCoinsuranceLimitValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor límite de coaseguro del paciente.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationData', @level2type = N'COLUMN', @level2name = N'PatientCoinsuranceLimitValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationData', @level2type = N'COLUMN', @level2name = N'PatientCoinsuranceLimitValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor cubierto por la aseguradora, equivalente al copago de la aseguradora en la liquidación. NUMERIC(20,2), default 0.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationData', @level2type = N'COLUMN', @level2name = N'InsurerCoveredValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor cubierto Aseguradora:                   es como el Copago de la aseguradora', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationData', @level2type = N'COLUMN', @level2name = N'InsurerCoveredValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationData', @level2type = N'COLUMN', @level2name = N'InsurerCoveredValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del límite general máximo aplicable a la aseguradora, distribuido entre todos los ítems/servicios facturados. NUMERIC(20,2), default 0.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationData', @level2type = N'COLUMN', @level2name = N'LimitValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del limite general, limite maximo que puede ser aplicado a la aseguradora. se distribuyen en todo los items', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationData', @level2type = N'COLUMN', @level2name = N'LimitValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationData', @level2type = N'COLUMN', @level2name = N'LimitValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador si se incluye IVA en la liquidación: 0=No incluye, 1=Incluye. BIT, default 0.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationData', @level2type = N'COLUMN', @level2name = N'TaxInclude';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Incluye IVA: 0 false(No); 1 true (si)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationData', @level2type = N'COLUMN', @level2name = N'TaxInclude';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationData', @level2type = N'COLUMN', @level2name = N'TaxInclude';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador para aplicar límites generales en la facturación: 0=No aplica, 1=Aplica límites. BIT, default 0.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationData', @level2type = N'COLUMN', @level2name = N'ApplyGeneralLimits';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Aplica limites Generales:0 false(no); 1 True (si)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationData', @level2type = N'COLUMN', @level2name = N'ApplyGeneralLimits';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationData', @level2type = N'COLUMN', @level2name = N'ApplyGeneralLimits';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de última modificación del registro de liquidación. DATETIME, nullable.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationData', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationData', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationData', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación del registro. VARCHAR(20), nullable, auditoría.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationData', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationData', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationData', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de liquidación. DATETIME, obligatorio, auditoría.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationData', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationData', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationData', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro de liquidación. VARCHAR(20), obligatorio, auditoría.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationData', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationData', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationData', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de coaseguro a cargo del paciente en la facturación. NUMERIC(5,2), respecto al valor neto después deducibles.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationData', @level2type = N'COLUMN', @level2name = N'PatientCoinsurance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'% Coaseguro paciente', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationData', @level2type = N'COLUMN', @level2name = N'PatientCoinsurance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationData', @level2type = N'COLUMN', @level2name = N'PatientCoinsurance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de coaseguro a cargo de la aseguradora en la facturación. NUMERIC(5,2), respecto al valor neto después deducibles.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationData', @level2type = N'COLUMN', @level2name = N'InsuranceCoinsurance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'% Coaseguro aseguradora', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationData', @level2type = N'COLUMN', @level2name = N'InsuranceCoinsurance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationData', @level2type = N'COLUMN', @level2name = N'InsuranceCoinsurance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del copago fijo que debe aportar el paciente por ingreso/atención. NUMERIC(20,2), obligatorio.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationData', @level2type = N'COLUMN', @level2name = N'CopaymentValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor copago', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationData', @level2type = N'COLUMN', @level2name = N'CopaymentValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationData', @level2type = N'COLUMN', @level2name = N'CopaymentValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Momento de aplicación del deducible: 0=Antes de coaseguro, 1=Después de coaseguro. BIT, impacta orden de cálculo.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationData', @level2type = N'COLUMN', @level2name = N'ApplyDeductible';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Aplicar deducible : 0-Antes de coaseguro, 1-Después de coaseguro', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationData', @level2type = N'COLUMN', @level2name = N'ApplyDeductible';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationData', @level2type = N'COLUMN', @level2name = N'ApplyDeductible';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario del deducible (franquicia) que debe pagar el paciente antes de cobertura. NUMERIC(20,2), obligatorio.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationData', @level2type = N'COLUMN', @level2name = N'DeductibleValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del deducible', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationData', @level2type = N'COLUMN', @level2name = N'DeductibleValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationData', @level2type = N'COLUMN', @level2name = N'DeductibleValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico del descuento (monto o porcentaje) según tipo definido en ApplyDiscount. NUMERIC(20,2), nullable.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationData', @level2type = N'COLUMN', @level2name = N'DiscountValueorPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el valor del descuento o el porcentaje en funcion del campo anterior', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationData', @level2type = N'COLUMN', @level2name = N'DiscountValueorPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationData', @level2type = N'COLUMN', @level2name = N'DiscountValueorPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de descuento a aplicar: 1=Porcentual, 2=Valor fijo, 3=Contratado, 4=Ninguno. TINYINT, controla interpretación de DiscountValueorPercentage.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationData', @level2type = N'COLUMN', @level2name = N'ApplyDiscount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Aplicar descuento :                   1. Porcentual                   2. Valor                   3. Contratado                   4. Ninguno', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationData', @level2type = N'COLUMN', @level2name = N'ApplyDiscount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationData', @level2type = N'COLUMN', @level2name = N'ApplyDiscount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso del paciente, identificador de la atención/hospitalización. CHAR(10), obligatorio, FK a tabla de admisiones.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationData', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'# de ingreso del paciente', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationData', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationData', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico del registro de datos de liquidación. INT IDENTITY(1,1), clave primaria.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationData', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationData', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationData', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Datos de liquidación de facturación por ingreso/admisión. Contiene los parámetros financieros aplicados al momento de liquidar una cuenta: descuentos, deducibles, copagos, coaseguros y límites de cobertura pactados entre el asegurador y el paciente.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationData';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationData';

GO
CREATE NONCLUSTERED INDEX [IX_LiquidationData_AdmissionNumber]
    ON [Billing].[LiquidationData]([AdmissionNumber] ASC);
