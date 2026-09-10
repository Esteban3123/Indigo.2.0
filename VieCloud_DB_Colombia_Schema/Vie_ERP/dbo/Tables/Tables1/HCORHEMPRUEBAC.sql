CREATE TABLE [dbo].[HCORHEMPRUEBAC] (
    [ID]                 INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [HCORHEMCOID]        INT           NOT NULL,
    [CODIGOBACTERIOLOGO] CHAR (20)     NOT NULL,
    [FECHAPRUEBA]        DATETIME      NOT NULL,
    [ESTADO]             INT           NOT NULL,
    [FECHAREGISTRO]      DATETIME      NOT NULL,
    [CODIGOCUPS]         VARCHAR (20)  NULL,
    [NOMBREBACTERIOLOGO] VARCHAR (150) NULL,
    [NUMEROBOLSA]        VARCHAR (20)  NULL,
    [SELLOCALIDAD]       VARCHAR (20)  NULL,
    CONSTRAINT [PK_HCORHEMPRUEBAC] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCORHEMPRUEBAC_HCORHEMCO] FOREIGN KEY ([HCORHEMCOID]) REFERENCES [dbo].[HCORHEMCO] ([ID]),
    CONSTRAINT [FK_HCORHEMPRUEBAC_INPROFSAL] FOREIGN KEY ([CODIGOBACTERIOLOGO]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sello de calidad, certificación o validación de la muestra de sangre en prueba cruzada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMPRUEBAC', @level2type = N'COLUMN', @level2name = N'SELLOCALIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sello de calidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMPRUEBAC', @level2type = N'COLUMN', @level2name = N'SELLOCALIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMPRUEBAC', @level2type = N'COLUMN', @level2name = N'SELLOCALIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de identificación de la bolsa o unidad de sangre utilizada en la prueba cruzada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMPRUEBAC', @level2type = N'COLUMN', @level2name = N'NUMEROBOLSA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de la bolsa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMPRUEBAC', @level2type = N'COLUMN', @level2name = N'NUMEROBOLSA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMPRUEBAC', @level2type = N'COLUMN', @level2name = N'NUMEROBOLSA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo del bacteriólogo o profesional de salud responsable de realizar la prueba cruzada (FK: INPROFSAL)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMPRUEBAC', @level2type = N'COLUMN', @level2name = N'NOMBREBACTERIOLOGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del bacteriologo que realiza prueba cruzada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMPRUEBAC', @level2type = N'COLUMN', @level2name = N'NOMBREBACTERIOLOGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMPRUEBAC', @level2type = N'COLUMN', @level2name = N'NOMBREBACTERIOLOGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CUPS del procedimiento de prueba cruzada, clasificación de servicios de salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMPRUEBAC', @level2type = N'COLUMN', @level2name = N'CODIGOCUPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del CUPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMPRUEBAC', @level2type = N'COLUMN', @level2name = N'CODIGOCUPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMPRUEBAC', @level2type = N'COLUMN', @level2name = N'CODIGOCUPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de la prueba cruzada en el sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMPRUEBAC', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación de registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMPRUEBAC', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMPRUEBAC', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de compatibilidad de la prueba cruzada: 1=Compatible, 2=Incompatible', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMPRUEBAC', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado: 1 - Compatible     2- Incompatible', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMPRUEBAC', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMPRUEBAC', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se realizó efectivamente la prueba cruzada de sangre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMPRUEBAC', @level2type = N'COLUMN', @level2name = N'FECHAPRUEBA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha que se realizo la prueba cruzada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMPRUEBAC', @level2type = N'COLUMN', @level2name = N'FECHAPRUEBA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMPRUEBAC', @level2type = N'COLUMN', @level2name = N'FECHAPRUEBA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador del bacteriólogo o profesional que ejecuta la prueba cruzada (FK: INPROFSAL.CODPROSAL)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMPRUEBAC', @level2type = N'COLUMN', @level2name = N'CODIGOBACTERIOLOGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del bacteriologo que realiza prueba cruzada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMPRUEBAC', @level2type = N'COLUMN', @level2name = N'CODIGOBACTERIOLOGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMPRUEBAC', @level2type = N'COLUMN', @level2name = N'CODIGOBACTERIOLOGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la orden de hemocompatibilidad o transfusión relacionada (FK: HCORHEMCO.ID)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMPRUEBAC', @level2type = N'COLUMN', @level2name = N'HCORHEMCOID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la orden (HCRODHEMCO)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMPRUEBAC', @level2type = N'COLUMN', @level2name = N'HCORHEMCOID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMPRUEBAC', @level2type = N'COLUMN', @level2name = N'HCORHEMCOID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable del registro de prueba cruzada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMPRUEBAC', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMPRUEBAC', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMPRUEBAC', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de pruebas de hemocultivo realizadas a pacientes hospitalizados o en urgencias. Guarda el detalle de cada prueba bacteriológica: quién la realizó, cuándo, el código de servicio CUPS asociado, el número de bolsa o muestra y el sello de localidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMPRUEBAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMPRUEBAC';
