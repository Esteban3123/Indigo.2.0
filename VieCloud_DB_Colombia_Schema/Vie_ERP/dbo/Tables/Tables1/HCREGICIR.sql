CREATE TABLE [dbo].[HCREGICIR] (
    [IDREGIANES] INT         NOT NULL,
    [CODCIRUGI]  CHAR (10)   NOT NULL,
    [NOMCIRGIA]  NCHAR (500) NOT NULL,
    CONSTRAINT [PK_HCREGICIR] PRIMARY KEY CLUSTERED ([IDREGIANES] ASC, [CODCIRUGI] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o descripción de la cirugía, procedimiento quirúrgico realizado. Texto extenso (hasta 500 caracteres) que identifica el tipo de intervención quirúrgica, acto operatorio o procedimiento anestésico-quirúrgico documentado en la historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGICIR', @level2type = N'COLUMN', @level2name = N'NOMCIRGIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la Cirugia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGICIR', @level2type = N'COLUMN', @level2name = N'NOMCIRGIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGICIR', @level2type = N'COLUMN', @level2name = N'NOMCIRGIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la cirugía, identificador único del procedimiento quirúrgico. Código alfanumérico (10 caracteres) que clasifica y referencia el tipo de intervención quirúrgica, acto operatorio o procedimiento anestésico realizado al paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGICIR', @level2type = N'COLUMN', @level2name = N'CODCIRUGI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Cirugia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGICIR', @level2type = N'COLUMN', @level2name = N'CODCIRUGI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGICIR', @level2type = N'COLUMN', @level2name = N'CODCIRUGI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de registro en la cabecera de anestesia, identificador del evento anestésico. Referencia numérica que vincula el registro quirúrgico con el documento de anestesia, ingreso anestésico o evento de atención quirúrgica en la historia clínica del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGICIR', @level2type = N'COLUMN', @level2name = N'IDREGIANES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero Registrado en la Cabecera de Anestesia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGICIR', @level2type = N'COLUMN', @level2name = N'IDREGIANES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGICIR', @level2type = N'COLUMN', @level2name = N'IDREGIANES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro maestro de cirugías disponibles en el sistema. Contiene el catálogo de procedimientos quirúrgicos con su código y nombre oficial, usado para clasificar y documentar intervenciones quirúrgicas en la historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGICIR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGICIR';
