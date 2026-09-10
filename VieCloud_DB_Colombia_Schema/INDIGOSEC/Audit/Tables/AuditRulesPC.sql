CREATE TABLE [Audit].[AuditRulesPC] (
    [Id]             INT           IDENTITY (1, 1) NOT NULL,
    [PcName]         VARCHAR (200) NOT NULL,
    [UserFiler]      BIT           NOT NULL,
    [DocumentFilter] BIT           NOT NULL,
    CONSTRAINT [PK_AuditoriaReglasPC] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [IX_AuditoriaReglasPC] UNIQUE NONCLUSTERED ([PcName] ASC)
);

