CREATE TABLE [Audit].[AuditRulesPCUser] (
    [Id]    INT           IDENTITY (1, 1) NOT NULL,
    [IdPc]  INT           NOT NULL,
    [Users] VARCHAR (200) NOT NULL,
    CONSTRAINT [PK_AuditoriaReglasPCUsuario] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AuditoriaReglasPCUsuario_AuditoriaReglasPC] FOREIGN KEY ([IdPc]) REFERENCES [Audit].[AuditRulesPC] ([Id]),
    CONSTRAINT [IX_AuditoriaReglasPCUsuario] UNIQUE NONCLUSTERED ([IdPc] ASC, [Users] ASC)
);

