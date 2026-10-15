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

