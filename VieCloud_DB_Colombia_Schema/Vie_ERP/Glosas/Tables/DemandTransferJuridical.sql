CREATE TABLE [Glosas].[DemandTransferJuridical] (
    [Id]                                 INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [TransferJuridicalDebtCollectionCId] INT          NOT NULL,
    [FilingUnitSourceId]                 INT          NULL,
    [FilingUnitTargetId]                 INT          NULL,
    [LawyerId]                           INT          NULL,
    [DemandStatusId]                     INT          NULL,
    [CreationUser]                       VARCHAR (20) NOT NULL,
    [CreationDate]                       DATETIME     NOT NULL,
    CONSTRAINT [PK_DemandTransferJuridical] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de registro o creación del traslado jurídico de la demanda en el sistema (DATETIME)', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'DemandTransferJuridical', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de registro', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'DemandTransferJuridical', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'DemandTransferJuridical', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario o profesional que registró o editó el traslado jurídico de la glosa, cobro o demanda (VARCHAR 20, auditoría)', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'DemandTransferJuridical', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario quien edito el traslado juridico', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'DemandTransferJuridical', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'DemandTransferJuridical', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del estado actual de la demanda: pendiente, radicada, en proceso, fallada, cobrada, etc. (INT, FK)', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'DemandTransferJuridical', @level2type = N'COLUMN', @level2name = N'DemandStatusId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de estado de demanda', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'DemandTransferJuridical', @level2type = N'COLUMN', @level2name = N'DemandStatusId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'DemandTransferJuridical', @level2type = N'COLUMN', @level2name = N'DemandStatusId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del abogado o profesional jurídico responsable del traslado y gestión de la demanda (INT, FK)', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'DemandTransferJuridical', @level2type = N'COLUMN', @level2name = N'LawyerId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de abogado', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'DemandTransferJuridical', @level2type = N'COLUMN', @level2name = N'LawyerId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'DemandTransferJuridical', @level2type = N'COLUMN', @level2name = N'LawyerId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la unidad de radicación, juzgado o entidad jurídica destino que recibe la demanda (INT, FK nullable)', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'DemandTransferJuridical', @level2type = N'COLUMN', @level2name = N'FilingUnitTargetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad de radicación destino', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'DemandTransferJuridical', @level2type = N'COLUMN', @level2name = N'FilingUnitTargetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'DemandTransferJuridical', @level2type = N'COLUMN', @level2name = N'FilingUnitTargetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la unidad de radicación, juzgado u oficina origen que remite la demanda (INT, FK nullable)', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'DemandTransferJuridical', @level2type = N'COLUMN', @level2name = N'FilingUnitSourceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad de radicación origen', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'DemandTransferJuridical', @level2type = N'COLUMN', @level2name = N'FilingUnitSourceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'DemandTransferJuridical', @level2type = N'COLUMN', @level2name = N'FilingUnitSourceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del traslado jurídico o proceso de cobro por demanda relacionado con glosa (INT, FK, obligatorio)', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'DemandTransferJuridical', @level2type = N'COLUMN', @level2name = N'TransferJuridicalDebtCollectionCId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del traslado juridico', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'DemandTransferJuridical', @level2type = N'COLUMN', @level2name = N'TransferJuridicalDebtCollectionCId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'DemandTransferJuridical', @level2type = N'COLUMN', @level2name = N'TransferJuridicalDebtCollectionCId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave primaria, identificador único de cada registro de traslado jurídico de demanda (INT IDENTITY, PK)', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'DemandTransferJuridical', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador de la tabla', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'DemandTransferJuridical', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'DemandTransferJuridical', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra los traslados jurídicos de demandas en el proceso de cobro coactivo de glosas, indicando la unidad de radicación origen y destino, el abogado asignado y el estado de la demanda en cada transferencia.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'DemandTransferJuridical';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'DemandTransferJuridical';
