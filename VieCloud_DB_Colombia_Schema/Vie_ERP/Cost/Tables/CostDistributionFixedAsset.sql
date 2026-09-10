CREATE TABLE [Cost].[CostDistributionFixedAsset] (
    [Id]                        INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                      VARCHAR (20)    NOT NULL,
    [FixedAssetPhysicalAssetId] INT             NOT NULL,
    [FixedAssetCode]            VARCHAR (20)    CONSTRAINT [DF_CostDistributionFixedAsset_FixedAssetCode] DEFAULT ('') NOT NULL,
    [DepreciationValue]         DECIMAL (18, 2) NOT NULL,
    [Description]               VARCHAR (300)   NOT NULL,
    [Year]                      INT             NOT NULL,
    [Month]                     INT             NOT NULL,
    [Status]                    BIT             NOT NULL,
    [CreationUser]              VARCHAR (20)    NOT NULL,
    [CreationDate]              DATETIME        NOT NULL,
    [ModificationUser]          VARCHAR (20)    NULL,
    [ModificationDate]          DATETIME        NULL,
    [TimeStamp]                 ROWVERSION      NOT NULL,
    [HoursWorked]               INT             CONSTRAINT [DF_CostDistributionFixedAsset_HoursWorked] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_CostDistributionFixedAsset] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostDistributionFixedAsset_FixedAssetPhysicalAsset] FOREIGN KEY ([FixedAssetPhysicalAssetId]) REFERENCES [FixedAsset].[FixedAssetPhysicalAsset] ([Id])
);




GO



GO
CREATE NONCLUSTERED INDEX [IX_CostDistributionFixedAsset_Year_Month_FixedAssetPhysicalAssetId]
    ON [Cost].[CostDistributionFixedAsset]([Year], [Month], [FixedAssetPhysicalAssetId])
    INCLUDE ([Status], [Id]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. Horas totales trabajadas o utilizadas del activo fijo durante el período de distribución de costos. Cantidad de horas operativas registradas para el cálculo de depreciación.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAsset', @level2type = N'COLUMN', @level2name = N'HoursWorked';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Horas totales usadas del activo fijo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAsset', @level2type = N'COLUMN', @level2name = N'HoursWorked';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAsset', @level2type = N'COLUMN', @level2name = N'HoursWorked';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TIMESTAMP. Marca temporal automática del sistema que registra el instante exacto de creación, modificación o cambio de estado del registro de distribución de costos del activo fijo.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAsset', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAsset', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAsset', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME. Fecha y hora en que se realizó la última modificación del registro de distribución del activo fijo. Nulo si no ha sido editado.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAsset', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAsset', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAsset', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(20). Usuario responsable de la última edición o actualización del registro de distribución de costos. Nulo si no ha habido modificaciones.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAsset', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAsset', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAsset', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME. Fecha y hora de creación del registro de distribución de costos del activo fijo. Trazabilidad de auditoría.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAsset', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAsset', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAsset', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(20). Usuario que generó inicialmente el registro de distribución de costos del activo fijo. Rastro de auditoría.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAsset', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAsset', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAsset', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Estado del registro: 1=Activo (distribución vigente), 0=Inactivo (distribución cancelada o anulada). Control de vigencia.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAsset', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del de la entidad 1 - Activo  0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAsset', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAsset', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. Mes específico (1-12) en el que se realizó la distribución de costos o depreciación del activo fijo.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAsset', @level2type = N'COLUMN', @level2name = N'Month';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mes en el que se hizo la distribucion', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAsset', @level2type = N'COLUMN', @level2name = N'Month';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAsset', @level2type = N'COLUMN', @level2name = N'Month';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. Año (AAAA) al que corresponde la distribución de costos y cálculo de depreciación del activo fijo.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAsset', @level2type = N'COLUMN', @level2name = N'Year';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Año de la distribucion', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAsset', @level2type = N'COLUMN', @level2name = N'Year';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAsset', @level2type = N'COLUMN', @level2name = N'Year';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(300). Descripción detallada del concepto, motivo o justificación de la distribución de costos del activo fijo en el período indicado.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAsset', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de la distribucion', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAsset', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAsset', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DECIMAL(18,2). Valor monetario de la depreciación calculada del activo fijo para el mes y año correspondiente. Referencia: tablas AFNDEPRECI, AFNCALDEP del ERP.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAsset', @level2type = N'COLUMN', @level2name = N'DepreciationValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor de la depreciacion del activo fijo en el mes y año correspondiente    Las tablas de depreciacion son AFNDEPRECI y AFNCALDEP', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAsset', @level2type = N'COLUMN', @level2name = N'DepreciationValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAsset', @level2type = N'COLUMN', @level2name = N'DepreciationValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(20). Código identificador único del activo fijo en el ERP origen (ej: AFNACTIVO). Facilita interfaz y trazabilidad con sistemas externos.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAsset', @level2type = N'COLUMN', @level2name = N'FixedAssetCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del activo fijo del ERP con el que nos estemos interfazando', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAsset', @level2type = N'COLUMN', @level2name = N'FixedAssetCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAsset', @level2type = N'COLUMN', @level2name = N'FixedAssetCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. Identificador numérico único del registro físico del activo fijo en tabla [FixedAsset].[FixedAssetPhysicalAsset]. Clave foránea para vinculación.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAsset', @level2type = N'COLUMN', @level2name = N'FixedAssetPhysicalAssetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del activo fijo del ERP con el que nos estemos interfazando  AFNACTIVO', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAsset', @level2type = N'COLUMN', @level2name = N'FixedAssetPhysicalAssetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAsset', @level2type = N'COLUMN', @level2name = N'FixedAssetPhysicalAssetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(20). Código único de la distribución de costos del activo fijo. Identificador empresarial del registro.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAsset', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la distribucion de activos fijos', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAsset', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAsset', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT IDENTITY. Identificador único autoincrementable del registro de distribución de costos del activo fijo. Clave primaria del sistema.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAsset', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la distribucion de activos fijos', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAsset', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAsset', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Distribución de costos de activos fijos: registra la depreciación mensual de cada activo fijo asignado a un centro de costo o unidad, indicando el valor depreciado, las horas trabajadas y el período (año/mes) correspondiente. Permite distribuir el costo de activos como equipos e inmuebles entre las áreas que los utilizan.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAsset';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAsset';
