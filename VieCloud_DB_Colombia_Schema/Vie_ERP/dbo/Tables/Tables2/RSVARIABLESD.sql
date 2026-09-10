CREATE TABLE [dbo].[RSVARIABLESD] (
    [ID]        INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDPADRE]   INT           NOT NULL,
    [IDHIJO]    INT           NOT NULL,
    [EXPRESION] VARCHAR (250) NOT NULL,
    CONSTRAINT [PK_RSVARIABLESD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_RSVARIABLESD_RSVARIABLES] FOREIGN KEY ([IDPADRE]) REFERENCES [dbo].[RSVARIABLES] ([ID]),
    CONSTRAINT [FK_RSVARIABLESD_RSVARIABLES1] FOREIGN KEY ([IDHIJO]) REFERENCES [dbo].[RSVARIABLES] ([ID])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Expresión de validación o cálculo (VARCHAR 250). Fórmula, regla lógica o condición que se evalúa entre variables padre e hijo en la jerarquía de variables de reportes/reglas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLESD', @level2type = N'COLUMN', @level2name = N'EXPRESION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la Expresion a validar ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLESD', @level2type = N'COLUMN', @level2name = N'EXPRESION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLESD', @level2type = N'COLUMN', @level2name = N'EXPRESION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT FK) de la variable hijo en la relación padre-hijo. Referencia a RSVARIABLES.ID que actúa como variable dependiente o derivada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLESD', @level2type = N'COLUMN', @level2name = N'IDHIJO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la Identidad del hijo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLESD', @level2type = N'COLUMN', @level2name = N'IDHIJO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLESD', @level2type = N'COLUMN', @level2name = N'IDHIJO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT FK) de la variable padre en la relación padre-hijo. Referencia a RSVARIABLES.ID que actúa como variable independiente o base.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLESD', @level2type = N'COLUMN', @level2name = N'IDPADRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la Identidad del padre ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLESD', @level2type = N'COLUMN', @level2name = N'IDPADRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLESD', @level2type = N'COLUMN', @level2name = N'IDPADRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autoincremental (INT, PK). Clave primaria única de la relación entre variables padre e hijo en la tabla de dependencias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLESD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLESD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLESD', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciones jerárquicas entre variables de reportes o plantillas del sistema, donde cada registro define un vínculo padre-hijo con la expresión o fórmula asociada a esa variable dependiente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLESD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLESD';
