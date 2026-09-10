CREATE TABLE [Contract].[DefinitionRateDetailSurgicalProcedures] (
    [Id]                         INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DefinitionRateDetailId]     INT NOT NULL,
    [SurgicalProcedureServiceId] INT NOT NULL,
    [IPSServiceId]               INT NOT NULL,
    CONSTRAINT [PK_DefinitionRateDetailSurgicalProcedures] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DefinitionRateDetailSurgicalProcedures_DefinitionRateDetail] FOREIGN KEY ([DefinitionRateDetailId]) REFERENCES [Contract].[DefinitionRateDetail] ([Id]),
    CONSTRAINT [FK_DefinitionRateDetailSurgicalProcedures_IPSService] FOREIGN KEY ([IPSServiceId]) REFERENCES [Contract].[IPSService] ([Id]),
    CONSTRAINT [FK_DefinitionRateDetailSurgicalProcedures_SurgicalProcedureService] FOREIGN KEY ([SurgicalProcedureServiceId]) REFERENCES [Contract].[SurgicalProcedureService] ([Id])
);




GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del servicio quirúrgico de la IPS (Institución Prestadora de Salud). Referencia a Contract.IPSService. Vincula el procedimiento quirúrgico con la unidad funcional o centro de atención que lo ofrece.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailSurgicalProcedures', @level2type = N'COLUMN', @level2name = N'IPSServiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del procedimiento qx', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailSurgicalProcedures', @level2type = N'COLUMN', @level2name = N'IPSServiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailSurgicalProcedures', @level2type = N'COLUMN', @level2name = N'IPSServiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la relación entre servicio IPS y procedimiento quirúrgico. Referencia a Contract.SurgicalProcedureService. Asocia el procedimiento qx (cirugía, intervención) con el catálogo de servicios disponibles.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailSurgicalProcedures', @level2type = N'COLUMN', @level2name = N'SurgicalProcedureServiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla en donde se relaciona el servicio ips con el procedimiento qx', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailSurgicalProcedures', @level2type = N'COLUMN', @level2name = N'SurgicalProcedureServiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailSurgicalProcedures', @level2type = N'COLUMN', @level2name = N'SurgicalProcedureServiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle de definición de tarifas. Referencia a Contract.DefinitionRateDetail. Vincula el procedimiento quirúrgico con su tarifa, valor, contrato y términos de facturación/RIPS.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailSurgicalProcedures', @level2type = N'COLUMN', @level2name = N'DefinitionRateDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la definición de tarifas', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailSurgicalProcedures', @level2type = N'COLUMN', @level2name = N'DefinitionRateDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailSurgicalProcedures', @level2type = N'COLUMN', @level2name = N'DefinitionRateDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave primaria, identificador único del registro de relación entre procedimiento quirúrgico y detalle de tarifa. Autonumérico, no replicable.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailSurgicalProcedures', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailSurgicalProcedures', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailSurgicalProcedures', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona los detalles de tarifas contractuales con los procedimientos quirúrgicos y servicios IPS asociados, permitiendo definir qué procedimientos quirúrgicos aplican a cada tarifa dentro de un contrato.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailSurgicalProcedures';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailSurgicalProcedures';
