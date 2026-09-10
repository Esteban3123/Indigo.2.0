CREATE TABLE [dbo].[HCPARNUTDUF] (
    [ID]          INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [HCPARNUTCId] INT       NULL,
    [UFUCODIGO]   CHAR (10) NULL,
    CONSTRAINT [PK__HCPARNUT__3214EC074BA10146] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_IDNUTCINUNIFUNC] FOREIGN KEY ([HCPARNUTCId]) REFERENCES [dbo].[HCPARNUTC] ([ID]),
    CONSTRAINT [FK_NUTDUFINUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCPARNUTDUF] NOCHECK CONSTRAINT [FK_IDNUTCINUNIFUNC];




GO
ALTER TABLE [dbo].[HCPARNUTDUF] NOCHECK CONSTRAINT [FK_IDNUTCINUNIFUNC];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional (centro de atención, servicio, departamento) donde se parametriza, configura o aplica la nutrición parenteral. Referencia FK a INUNIFUNC. Type: CHAR(10).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTDUF', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la unidad funcional donde se esta parametrizando la nutricion parenteral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTDUF', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTDUF', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de la cabecera/registro maestro de nutrición parenteral. Referencia FK a HCPARNUTC. Agrupa detalles de parametrización por unidad funcional. Type: INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTDUF', @level2type = N'COLUMN', @level2name = N'HCPARNUTCId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la cabecera de nutricion parenteral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTDUF', @level2type = N'COLUMN', @level2name = N'HCPARNUTCId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTDUF', @level2type = N'COLUMN', @level2name = N'HCPARNUTCId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único, clave primaria del registro de asociación entre nutrición parenteral y unidad funcional. Type: INT IDENTITY.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTDUF', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTDUF', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTDUF', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona los parámetros nutricionales de historia clínica con las unidades funcionales (servicios o áreas de atención) donde aplican. Permite configurar qué unidades funcionales están asociadas a cada parámetro de nutrición clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTDUF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTDUF';
