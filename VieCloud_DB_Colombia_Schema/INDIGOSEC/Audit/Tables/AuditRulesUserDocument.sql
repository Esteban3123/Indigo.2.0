CREATE TABLE [Audit].[AuditRulesUserDocument] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [IdUser]    INT           NOT NULL,
    [Documents] VARCHAR (200) NOT NULL,
    CONSTRAINT [PK_AuditoriaReglasUsuarioDocumento] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AuditoriaReglasUsuarioDocumento_AuditoriaReglasUsuario] FOREIGN KEY ([IdUser]) REFERENCES [Audit].[AuditRulesUser] ([Id]),
    CONSTRAINT [IX_AuditoriaReglasUsuarioDocumento] UNIQUE NONCLUSTERED ([IdUser] ASC, [Documents] ASC)
);

