CREATE TABLE [dbo].[NTXGRUPOS] (
    [ID]                  INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDNTADMINISTRATIVAS] INT NOT NULL,
    [IDNTGRUPOS]          INT NOT NULL,
    CONSTRAINT [PK_NTXGRUPOS] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_NTXGRUPOS_NTADMINISTRATIVAS] FOREIGN KEY ([IDNTADMINISTRATIVAS]) REFERENCES [dbo].[NTADMINISTRATIVAS] ([ID]),
    CONSTRAINT [FK_NTXGRUPOS_NTXGRUPOS] FOREIGN KEY ([IDNTGRUPOS]) REFERENCES [dbo].[NTGRUPOS] ([ID])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK) que vincula a la tabla NTGRUPOS; identifica el grupo de procedimientos, servicios o categorías administrativas. Tipo: INT. Referencia: [dbo].[NTGRUPOS].[ID]', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTXGRUPOS', @level2type = N'COLUMN', @level2name = N'IDNTGRUPOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relación con la tabla NTGRUPOS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTXGRUPOS', @level2type = N'COLUMN', @level2name = N'IDNTGRUPOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTXGRUPOS', @level2type = N'COLUMN', @level2name = N'IDNTGRUPOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK) que vincula a la tabla NTADMINISTRATIVAS; identifica la unidad administrativa, centro de atención o entidad responsable. Tipo: INT. Referencia: [dbo].[NTADMINISTRATIVAS].[ID]', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTXGRUPOS', @level2type = N'COLUMN', @level2name = N'IDNTADMINISTRATIVAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relación con la tabla NTADMINISTRATIVAS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTXGRUPOS', @level2type = N'COLUMN', @level2name = N'IDNTADMINISTRATIVAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTXGRUPOS', @level2type = N'COLUMN', @level2name = N'IDNTADMINISTRATIVAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único consecutivo (clave primaria, IDENTITY). Tipo: INT. Clave primaria clustered para la tabla de relación NTXGRUPOS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTXGRUPOS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTXGRUPOS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTXGRUPOS', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de relación entre registros administrativos y grupos, que asocia cada nota o registro administrativo con uno o más grupos definidos en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTXGRUPOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTXGRUPOS';
