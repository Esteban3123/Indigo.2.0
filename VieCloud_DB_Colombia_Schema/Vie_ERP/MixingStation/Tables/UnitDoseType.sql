CREATE TABLE [MixingStation].[UnitDoseType] (
    [Id]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                VARCHAR (20)  NOT NULL,
    [Description]         VARCHAR (MAX) NULL,
    [MSClass]             INT           NOT NULL,
    [State]               BIT           NOT NULL,
    [CreationUser]        VARCHAR (20)  CONSTRAINT [DF_UnitDoseType_CreationUser] DEFAULT ('999') NOT NULL,
    [CreationDate]        DATETIME      CONSTRAINT [DF_UnitDoseType_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]    VARCHAR (20)  NULL,
    [ModificationDate]    DATETIME      NULL,
    [TimeStamp]           ROWVERSION    NOT NULL,
    [InventoryGroupId]    INT           NULL,
    [InventorySubGroupId] INT           NULL,
    [IVACodeId]           INT           NULL,
    [BillingGroupId]      INT           NULL,
    [Prefix]              VARCHAR (4)   CONSTRAINT [DF__UnitDoseT__Pefix__1DEBCF31] DEFAULT ('NNNN') NOT NULL,
    CONSTRAINT [PK_UnitDoseType] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UnitDoseType_BillingGroup] FOREIGN KEY ([BillingGroupId]) REFERENCES [Billing].[BillingGroup] ([Id]),
    CONSTRAINT [FK_UnitDoseType_GeneralLedgerIVA] FOREIGN KEY ([IVACodeId]) REFERENCES [GeneralLedger].[GeneralLedgerIVA] ([Id]),
    CONSTRAINT [FK_UnitDoseType_ProductGroup] FOREIGN KEY ([InventoryGroupId]) REFERENCES [Inventory].[ProductGroup] ([Id]),
    CONSTRAINT [FK_UnitDoseType_ProductSubGroup] FOREIGN KEY ([InventorySubGroupId]) REFERENCES [Inventory].[ProductSubGroup] ([Id])
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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Prefijo de 4 caracteres alfanuméricos (VARCHAR 4) usado como código de identificación corta para tipos de dosis unitaria en estación de mezcla; valor por defecto NNNN.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'UnitDoseType', @level2type = N'COLUMN', @level2name = N'Prefix';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Prefijo de  4 caracteres', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'UnitDoseType', @level2type = N'COLUMN', @level2name = N'Prefix';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'UnitDoseType', @level2type = N'COLUMN', @level2name = N'Prefix';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) del grupo de facturación asociado al tipo de dosis unitaria; clave foránea a Billing.BillingGroup para clasificación de facturación y RIPS.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'UnitDoseType', @level2type = N'COLUMN', @level2name = N'BillingGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Id del grupo de facturación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'UnitDoseType', @level2type = N'COLUMN', @level2name = N'BillingGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'UnitDoseType', @level2type = N'COLUMN', @level2name = N'BillingGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) del código IVA aplicable al tipo de dosis unitaria; referencia a GeneralLedger.GeneralLedgerIVA para impuesto de valor agregado y contabilidad.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'UnitDoseType', @level2type = N'COLUMN', @level2name = N'IVACodeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda ID de código IVA', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'UnitDoseType', @level2type = N'COLUMN', @level2name = N'IVACodeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'UnitDoseType', @level2type = N'COLUMN', @level2name = N'IVACodeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) del subgrupo de inventario del tipo de dosis unitaria; clave foránea a Inventory.ProductSubGroup para categorización granular de productos.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'UnitDoseType', @level2type = N'COLUMN', @level2name = N'InventorySubGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda ID de subgrupo de inventario', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'UnitDoseType', @level2type = N'COLUMN', @level2name = N'InventorySubGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'UnitDoseType', @level2type = N'COLUMN', @level2name = N'InventorySubGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) del grupo de inventario del tipo de dosis unitaria; referencia a Inventory.ProductGroup para clasificación y gestión de inventario.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'UnitDoseType', @level2type = N'COLUMN', @level2name = N'InventoryGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el ID de grupo de inventario', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'UnitDoseType', @level2type = N'COLUMN', @level2name = N'InventoryGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'UnitDoseType', @level2type = N'COLUMN', @level2name = N'InventoryGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal automática (TIMESTAMP) de la última modificación, creación o evento en el registro; usado internamente por SQL Server para control de concurrencia y auditoría.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'UnitDoseType', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'UnitDoseType', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'UnitDoseType', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) en que se realizó la última modificación del registro de tipo de dosis unitaria; NULL si nunca fue modificado.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'UnitDoseType', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'UnitDoseType', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'UnitDoseType', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR 20) que realizó la última modificación del tipo de dosis unitaria; valor NULL si no ha habido cambios post-creación.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'UnitDoseType', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de modificación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'UnitDoseType', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'UnitDoseType', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de creación del registro de tipo de dosis unitaria; generada automáticamente por función Common.getdate().', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'UnitDoseType', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'UnitDoseType', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'UnitDoseType', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR 20) que creó el registro del tipo de dosis unitaria; valor por defecto ''''999'''' si no se especifica.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'UnitDoseType', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de creación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'UnitDoseType', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'UnitDoseType', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado activo/inactivo (BIT: 1=activo, 0=inactivo) del tipo de dosis unitaria; controla disponibilidad en estación de mezcla.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'UnitDoseType', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'UnitDoseType', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'UnitDoseType', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación de tipo de dosis en estación de mezcla (INT): 2=NPT, 3=Antibioticoterapia, 4=Citostático, 5=Reempaque, 7=Reenvase, 9=Magistral, 10=Otros estériles; define protocolos de preparación.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'UnitDoseType', @level2type = N'COLUMN', @level2name = N'MSClass';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'2 - NPT , 
3 - Antibioticoterapia,
4 - Citostático, 
5 - Reempaque, 
7 - Reenvase,
9 - Magistral,  
10 - Otros estériles.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'UnitDoseType', @level2type = N'COLUMN', @level2name = N'MSClass';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'UnitDoseType', @level2type = N'COLUMN', @level2name = N'MSClass';




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción detallada (VARCHAR MAX) del tipo de dosis unitaria; información libre para documentación clínica, composición, indicaciones o notas especiales de preparación.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'UnitDoseType', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'UnitDoseType', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'UnitDoseType', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único (VARCHAR 20) identificador del tipo de dosis unitaria en estación de mezcla; usado para búsqueda rápida y auditoría de preparación de medicamentos.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'UnitDoseType', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Codigo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'UnitDoseType', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'UnitDoseType', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (INT IDENTITY) de la tabla UnitDoseType; clave primaria para referencia en otros módulos de mezcla y farmacia.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'UnitDoseType', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'UnitDoseType', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'UnitDoseType', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipos de dosis unitaria utilizados en la estación de mezclas (farmacia). Define las categorías de preparación de medicamentos en dosis individuales, incluyendo su clasificación, grupo de inventario, subgrupo, código de IVA, grupo de facturación y prefijo de identificación.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'UnitDoseType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'UnitDoseType';
