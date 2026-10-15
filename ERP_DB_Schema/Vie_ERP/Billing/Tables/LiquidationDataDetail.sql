CREATE TABLE [Billing].[LiquidationDataDetail] (
    [Id]                   INT             IDENTITY (1, 1) NOT NULL,
    [LiquidationDataId]    INT             NOT NULL,
    [ConditionType]        TINYINT         NOT NULL,
    [ConditionsItemsIds]   VARCHAR (1000)  NOT NULL,
    [LimitType]            TINYINT         NOT NULL,
    [Quantity]             INT             NOT NULL,
    [TaxInclude]           BIT             NOT NULL,
    [LimitValue]           NUMERIC (20, 2) NOT NULL,
    [InsurerCoveredValue]  NUMERIC (20, 2) CONSTRAINT [DF__Liquidati__Insur__6E2878CD] DEFAULT ((0)) NOT NULL,
    [InsuranceCoinsurance] NUMERIC (5, 2)  CONSTRAINT [DF_LiquidationDataDetail_InsuranceCoinsurance] DEFAULT ((0)) NOT NULL,
    [PatientCoinsurance]   NUMERIC (5, 2)  CONSTRAINT [DF_LiquidationDataDetail_PatientCoinsurance] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_LiquidationDataDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_LiquidationDataDetail_LiquidationData] FOREIGN KEY ([LiquidationDataId]) REFERENCES [Billing].[LiquidationData] ([Id])
);


GO
ALTER TABLE [Billing].[LiquidationDataDetail] NOCHECK CONSTRAINT [FK_LiquidationDataDetail_LiquidationData];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '% Coaseguro paciente,  Este campo solo se llena si el tipo de limite es Coaseguro (LimitType 3)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataDetail', @level2type = N'COLUMN', @level2name = N'PatientCoinsurance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '% Coaseguro aseguradora,  Este campo solo se llena si el tipo de limite es Coaseguro (LimitType 3)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataDetail', @level2type = N'COLUMN', @level2name = N'InsuranceCoinsurance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Limite max a cubrir por la aseguradora antes de coaseguro', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataDetail', @level2type = N'COLUMN', @level2name = N'InsurerCoveredValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Limite max a cubrir por la aseguradora', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataDetail', @level2type = N'COLUMN', @level2name = N'LimitValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Incluye IVA : 1 (TRUE -SI); 0 (False-No)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataDetail', @level2type = N'COLUMN', @level2name = N'TaxInclude';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad max de items a cubir por la aseguradora, 0 es No aplica.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de limite:   1- Valor Unitario,  2- Valor Total,  3 - Coaseguro', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataDetail', @level2type = N'COLUMN', @level2name = N'LimitType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Guarda los Ids, separados por coma, en base a la condicion anterior', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataDetail', @level2type = N'COLUMN', @level2name = N'ConditionsItemsIds';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de condicion :                   1. Grupo Cátalogo de Servicios                   2. SubGrupo Cátalogo de servicio                   3. Cátalogo de Servicios    4. Productos', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataDetail', @level2type = N'COLUMN', @level2name = N'ConditionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera datos de liquidacion', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataDetail', @level2type = N'COLUMN', @level2name = N'LiquidationDataId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataDetail', @level2type = N'COLUMN', @level2name = N'Id';

