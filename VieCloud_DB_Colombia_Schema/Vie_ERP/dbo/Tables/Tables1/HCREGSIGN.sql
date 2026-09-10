CREATE TABLE [dbo].[HCREGSIGN] (
    [IDREGANES]   INT       NOT NULL,
    [HORAREGIS]   CHAR (5)  NOT NULL,
    [VALORSIGN]   CHAR (20) NOT NULL,
    [OPCIOSIGN]   CHAR (10) NOT NULL,
    [SURGERYDATE] DATETIME  NULL,
    CONSTRAINT [PK_HCREGSIGN_1] PRIMARY KEY CLUSTERED ([IDREGANES] ASC, [HORAREGIS] ASC, [OPCIOSIGN] ASC, [VALORSIGN] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora completa de la cirugía/procedimiento quirúrgico para ordenar y filtrar correctamente registros anestésicos en formularios de seguimiento operatorio. Tipo: DATETIME, nullable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGSIGN', @level2type = N'COLUMN', @level2name = N'SURGERYDATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha completa de la Cirugia para listar de manera correcta el formulario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGSIGN', @level2type = N'COLUMN', @level2name = N'SURGERYDATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGSIGN', @level2type = N'COLUMN', @level2name = N'SURGERYDATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Opción o tipo de convención de signo vital estipulada en rango 210-10; identifica la categoría o escala de medición del parámetro. Tipo: CHAR(10).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGSIGN', @level2type = N'COLUMN', @level2name = N'OPCIOSIGN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de l Convecion estipulada entre el rango (210-10)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGSIGN', @level2type = N'COLUMN', @level2name = N'OPCIOSIGN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGSIGN', @level2type = N'COLUMN', @level2name = N'OPCIOSIGN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico del signo vital registrado (frecuencia cardíaca, presión arterial, saturación, temperatura, etc.) en rango 210-10 según escala convencional. Tipo: CHAR(20).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGSIGN', @level2type = N'COLUMN', @level2name = N'VALORSIGN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del signo Entre el rango 210- 10', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGSIGN', @level2type = N'COLUMN', @level2name = N'VALORSIGN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGSIGN', @level2type = N'COLUMN', @level2name = N'VALORSIGN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora y minuto del registro de signos vitales durante procedimiento anestésico (HH:MM). Tipo: CHAR(5), clave de agrupación temporal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGSIGN', @level2type = N'COLUMN', @level2name = N'HORAREGIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora Registro Signos Vitales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGSIGN', @level2type = N'COLUMN', @level2name = N'HORAREGIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGSIGN', @level2type = N'COLUMN', @level2name = N'HORAREGIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de anestesia en cabecera anestésica; vincula signos vitales a evento quirúrgico específico. Tipo: INT, FK a tabla de anestesia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGSIGN', @level2type = N'COLUMN', @level2name = N'IDREGANES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero Registrado en la Cabecera de Anestesia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGSIGN', @level2type = N'COLUMN', @level2name = N'IDREGANES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGSIGN', @level2type = N'COLUMN', @level2name = N'IDREGANES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de signos vitales y valores de signos clínicos del paciente durante su atención, asociando cada medición a un registro de anestesia o procedimiento quirúrgico con su hora y opción de signo correspondiente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGSIGN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGSIGN';
