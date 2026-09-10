CREATE TABLE [dbo].[AGEPROGQXAYUDANTES] (
    [ID]          INT                                                                           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDAGEPROGQX] INT                                                                           NOT NULL,
    [CODPROSAL]   CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    CONSTRAINT [PK_AGEPROGQXAYUDANTES] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_AGEPROGQXAYUDANTES_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[AGEPROGQXAYUDANTES].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de la salud ayudante o asistente quirúrgico (FK a INPROFSAL). Identificación ofuscada (PII masked). Sinónimos: cédula profesional, documento del asistente, identificación del auxiliar de cirugía.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQXAYUDANTES', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de profesional - Ayudante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQXAYUDANTES', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQXAYUDANTES', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cirugía o procedimiento quirúrgico (FK a AGEPROGQX). Referencia al evento quirúrgico, acto operatorio o intervención quirúrgica principal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQXAYUDANTES', @level2type = N'COLUMN', @level2name = N'IDAGEPROGQX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Cirugia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQXAYUDANTES', @level2type = N'COLUMN', @level2name = N'IDAGEPROGQX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQXAYUDANTES', @level2type = N'COLUMN', @level2name = N'IDAGEPROGQX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental (INT IDENTITY) de la relación entre cirugía y ayudante. Clave primaria de la tabla de asociación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQXAYUDANTES', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autoumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQXAYUDANTES', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQXAYUDANTES', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de ayudantes o profesionales de la salud asistentes asignados a un programa de cirugía agendada. Vincula cada procedimiento quirúrgico programado con los profesionales de apoyo participantes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQXAYUDANTES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQXAYUDANTES';
