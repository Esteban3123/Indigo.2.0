CREATE TABLE [Audit].[AuditRulesUserPC] (
    [Id]     INT           IDENTITY (1, 1) NOT NULL,
    [IdUser] INT           NOT NULL,
    [PcName] VARCHAR (200) NOT NULL,
    CONSTRAINT [PK_AuditoriaReglasUsuarioPC] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AuditoriaReglasUsuarioPC_AuditoriaReglasUsuario] FOREIGN KEY ([IdUser]) REFERENCES [Audit].[AuditRulesUser] ([Id]),
    CONSTRAINT [IX_AuditoriaReglasUsuarioPC] UNIQUE NONCLUSTERED ([IdUser] ASC, [PcName] ASC)
);

