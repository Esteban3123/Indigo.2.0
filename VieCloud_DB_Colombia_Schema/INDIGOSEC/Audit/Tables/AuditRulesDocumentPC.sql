CREATE TABLE [Audit].[AuditRulesDocumentPC] (
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    [IdDocument] INT           NOT NULL,
    [Pc]         VARCHAR (200) NOT NULL,
    CONSTRAINT [PK_AuditoriaReglasDocumentoPC] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AuditoriaReglasDocumentoPC_AuditoriaReglasDocumento] FOREIGN KEY ([IdDocument]) REFERENCES [Audit].[AuditRulesDocument] ([Id]),
    CONSTRAINT [IX_AuditoriaReglasDocumentoPC] UNIQUE NONCLUSTERED ([IdDocument] ASC, [Pc] ASC)
);

