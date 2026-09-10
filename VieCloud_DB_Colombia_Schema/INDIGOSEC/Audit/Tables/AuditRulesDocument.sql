CREATE TABLE [Audit].[AuditRulesDocument] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [DocumentCode] VARCHAR (200) NOT NULL,
    [UserFilter]   BIT           NOT NULL,
    [PcFilter]     BIT           NOT NULL,
    CONSTRAINT [PK_AuditoriaReglasDocuementos] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [IX_AuditoriaReglasDocumento] UNIQUE NONCLUSTERED ([DocumentCode] ASC)
);

