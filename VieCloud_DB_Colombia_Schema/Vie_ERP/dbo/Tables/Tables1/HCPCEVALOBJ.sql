CREATE TABLE [dbo].[HCPCEVALOBJ] (
    [IDVALOBJ]       INT IDENTITY (1, 1) NOT NULL,
    [IDPLANVAL]      INT NOT NULL,
    [CODOBJPCE]      INT NOT NULL,
    [IDHCPCECONTROL] INT NOT NULL,
    CONSTRAINT [PK_HCPCEVALOBJ] PRIMARY KEY CLUSTERED ([IDVALOBJ] ASC),
    CONSTRAINT [FK_HCPCEVALOBJ_HCOBJPCE] FOREIGN KEY ([CODOBJPCE]) REFERENCES [dbo].[HCOBJPCE] ([CODOBJPCE]),
    CONSTRAINT [FK_HCPCEVALOBJ_HCPCEPLANVAL] FOREIGN KEY ([IDPLANVAL]) REFERENCES [dbo].[HCPCEPLANVAL] ([IDPLANVAL])
);




GO



GO





GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_HCPCEVALOBJ_IDHCPCECONTROL]
    ON [dbo].[HCPCEVALOBJ]([IDHCPCECONTROL] ASC);


GO
ALTER INDEX [IX_HCPCEVALOBJ_IDHCPCECONTROL]
    ON [dbo].[HCPCEVALOBJ] DISABLE;




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del control/seguimiento del plan de cuidado de enfermería. Referencia a registro de monitoreo de intervenciones enfermeras.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEVALOBJ', @level2type = N'COLUMN', @level2name = N'IDHCPCECONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el id del control del plan de cuidado enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEVALOBJ', @level2type = N'COLUMN', @level2name = N'IDHCPCECONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEVALOBJ', @level2type = N'COLUMN', @level2name = N'IDHCPCECONTROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del objetivo de plan de cuidado de enfermería. Identifica metas clínicas y cuidados definidos en protocolos de atención enfermera.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEVALOBJ', @level2type = N'COLUMN', @level2name = N'CODOBJPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el codigo objetos de planes  de cuidado de enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEVALOBJ', @level2type = N'COLUMN', @level2name = N'CODOBJPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEVALOBJ', @level2type = N'COLUMN', @level2name = N'CODOBJPCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del plan de valoración/evaluación de enfermería. Referencia a registro maestro de planes de cuidado (FK HCPCEPLANVAL).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEVALOBJ', @level2type = N'COLUMN', @level2name = N'IDPLANVAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Id  del Identificador del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEVALOBJ', @level2type = N'COLUMN', @level2name = N'IDPLANVAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEVALOBJ', @level2type = N'COLUMN', @level2name = N'IDPLANVAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de evaluación de objetivo. Clave primaria que vincula objetivos de cuidado con evaluaciones en control enfermero.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEVALOBJ', @level2type = N'COLUMN', @level2name = N'IDVALOBJ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Id valores objetos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEVALOBJ', @level2type = N'COLUMN', @level2name = N'IDVALOBJ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEVALOBJ', @level2type = N'COLUMN', @level2name = N'IDVALOBJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de objetivos evaluados dentro de un plan de valoración de enfermería (PCE). Asocia cada objetivo clínico con su plan de valoración y el control de historia clínica correspondiente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEVALOBJ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEVALOBJ';
