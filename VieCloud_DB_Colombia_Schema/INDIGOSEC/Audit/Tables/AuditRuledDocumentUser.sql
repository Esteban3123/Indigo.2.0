CREATE TABLE [Audit].[AuditRuledDocumentUser] (
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    [IdDocument] INT           NOT NULL,
    [Users]      VARCHAR (200) NOT NULL,
    CONSTRAINT [PK_ReglasAuditoriaDocumentoUsuario] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AuditoriaReglasDocumentoUsuario_AuditoriaReglasDocumento] FOREIGN KEY ([IdDocument]) REFERENCES [Audit].[AuditRulesDocument] ([Id]),
    CONSTRAINT [IX_ReglasAuditoriaDocumentoUsuario] UNIQUE NONCLUSTERED ([IdDocument] ASC, [Users] ASC)
);

