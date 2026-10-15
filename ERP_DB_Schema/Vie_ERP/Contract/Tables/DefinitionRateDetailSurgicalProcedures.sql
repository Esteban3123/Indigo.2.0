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
ALTER TABLE [Contract].[DefinitionRateDetailSurgicalProcedures] NOCHECK CONSTRAINT [FK_DefinitionRateDetailSurgicalProcedures_DefinitionRateDetail];


GO
ALTER TABLE [Contract].[DefinitionRateDetailSurgicalProcedures] NOCHECK CONSTRAINT [FK_DefinitionRateDetailSurgicalProcedures_IPSService];


GO
ALTER TABLE [Contract].[DefinitionRateDetailSurgicalProcedures] NOCHECK CONSTRAINT [FK_DefinitionRateDetailSurgicalProcedures_SurgicalProcedureService];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del procedimiento qx', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailSurgicalProcedures', @level2type = N'COLUMN', @level2name = N'IPSServiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla en donde se relaciona el servicio ips con el procedimiento qx', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailSurgicalProcedures', @level2type = N'COLUMN', @level2name = N'SurgicalProcedureServiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de la definición de tarifas', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailSurgicalProcedures', @level2type = N'COLUMN', @level2name = N'DefinitionRateDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailSurgicalProcedures', @level2type = N'COLUMN', @level2name = N'Id';

