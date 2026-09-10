CREATE TABLE [Audit].[AuditRulesUser] (
    [Id]             INT          IDENTITY (1, 1) NOT NULL,
    [UserCode]       VARCHAR (50) NOT NULL,
    [PcFilter]       BIT          NOT NULL,
    [DocumentFilter] BIT          NOT NULL,
    CONSTRAINT [PK_AuditoriaReglasUsuario] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [IX_AuditoriaReglasUsuario] UNIQUE NONCLUSTERED ([UserCode] ASC)
);

