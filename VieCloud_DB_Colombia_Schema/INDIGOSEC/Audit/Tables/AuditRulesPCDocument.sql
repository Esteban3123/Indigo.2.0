CREATE TABLE [Audit].[AuditRulesPCDocument] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [IdPc]      INT           NOT NULL,
    [Documents] VARCHAR (200) NOT NULL,
    CONSTRAINT [PK_AuditoriaReglasPCDocumento] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AuditoriaReglasPCDocumento_AuditoriaReglasPC] FOREIGN KEY ([IdPc]) REFERENCES [Audit].[AuditRulesPC] ([Id]),
    CONSTRAINT [IX_AuditoriaReglasPCDocumento] UNIQUE NONCLUSTERED ([IdPc] ASC, [Documents] ASC)
);

