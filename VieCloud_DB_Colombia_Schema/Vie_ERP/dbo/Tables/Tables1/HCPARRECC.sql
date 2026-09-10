CREATE TABLE [dbo].[HCPARRECC] (
    [ID]          INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCENATE]   CHAR (10) NULL,
    [UFUCODIGO]   CHAR (10) NULL,
    [RECOESCRITA] BIT       NOT NULL,
    [TIPOUF]      INT       NULL,
    CONSTRAINT [PK_HCPARRECC] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCPARRECC_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCPARRECC_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de Unidad Funcional (INT): clasificación o categoría de la unidad funcional del centro de atención (ej: urgencias, consulta, hospitalización, laboratorio, imagenología). Referencia a estructura organizacional del EHR.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARRECC', @level2type = N'COLUMN', @level2name = N'TIPOUF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de unidad funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARRECC', @level2type = N'COLUMN', @level2name = N'TIPOUF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARRECC', @level2type = N'COLUMN', @level2name = N'TIPOUF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recomendación Escrita Permitida (BIT): indicador booleano que habilita (1=sí) o deshabilita (0=no) la emisión de recomendaciones en formato escrito en la historia clínica de la unidad funcional. Aplica a recetas, órdenes y prescripciones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARRECC', @level2type = N'COLUMN', @level2name = N'RECOESCRITA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece si permtite recomendación escrita 0-> no, 1-> si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARRECC', @level2type = N'COLUMN', @level2name = N'RECOESCRITA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARRECC', @level2type = N'COLUMN', @level2name = N'RECOESCRITA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de Unidad Funcional (CHAR 10): identificador único de la unidad funcional (consulta, urgencia, hospitalización, laboratorio, etc.) del centro de atención. Foreign Key a tabla INUNIFUNC. Búsquedas: UF, departamento clínico, servicio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARRECC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARRECC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARRECC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del Centro de Atención (CHAR 10): identificador del centro u sede donde opera la unidad funcional (hospital, clínica, puesto de salud). Foreign Key a tabla ADCENATEN. Sinónimos: código de sede, institución, establecimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARRECC', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARRECC', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARRECC', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador Autonumérico (INT IDENTITY): clave primaria secuencial e irrepetible de la tabla de parámetros de recomendaciones por unidad funcional. Cabecera de configuración del EHR.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARRECC', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id autonumerico cabecera de parametros', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARRECC', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARRECC', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de parámetros de reconocimiento o recepción de historia clínica por unidad funcional y centro de atención, indicando si la recepción se realiza de forma escrita y el tipo de unidad funcional asociada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARRECC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARRECC';
