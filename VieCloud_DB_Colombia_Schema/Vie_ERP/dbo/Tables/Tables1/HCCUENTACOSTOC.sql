CREATE TABLE [dbo].[HCCUENTACOSTOC] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODIGO]              CHAR (4)      NOT NULL,
    [DESCRIPCION]         VARCHAR (100) NOT NULL,
    [IDEAPBVIE]           VARCHAR (MAX) NOT NULL,
    [OBSERVACION]         VARCHAR (500) NULL,
    [FECHACONFIRMACION]   DATETIME      NOT NULL,
    [USUARIOCONFIRMACION] CHAR (20)     NOT NULL,
    [NOMBRENTIDADES]      VARCHAR (MAX) NULL,
    [TIPOCONSULTA]        INT           NULL,
    [FECHAINICIAL]        DATETIME      NULL,
    [FECHAFINAL]          DATETIME      NULL,
    CONSTRAINT [PK_HCCUENTACOSTOC] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha final del período de consulta o rango de atenciones; fecha hasta (DATETIME)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOC', @level2type = N'COLUMN', @level2name = N'FECHAFINAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha final', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOC', @level2type = N'COLUMN', @level2name = N'FECHAFINAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOC', @level2type = N'COLUMN', @level2name = N'FECHAFINAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha inicial del período de consulta o rango de atenciones; fecha desde (DATETIME)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOC', @level2type = N'COLUMN', @level2name = N'FECHAINICIAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha inicial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOC', @level2type = N'COLUMN', @level2name = N'FECHAINICIAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOC', @level2type = N'COLUMN', @level2name = N'FECHAINICIAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de consulta/filtro: 1=Todos los registros, 2=Incidencia, 3=Atenciones realizadas (INT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOC', @level2type = N'COLUMN', @level2name = N'TIPOCONSULTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1. Todos los registros  2. Incidencia  3. Atenciones realizadas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOC', @level2type = N'COLUMN', @level2name = N'TIPOCONSULTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOC', @level2type = N'COLUMN', @level2name = N'TIPOCONSULTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la entidad, EPS, EAPB o prestador de salud asociado; denominación de la organización (VARCHAR MAX)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOC', @level2type = N'COLUMN', @level2name = N'NOMBRENTIDADES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la entidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOC', @level2type = N'COLUMN', @level2name = N'NOMBRENTIDADES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOC', @level2type = N'COLUMN', @level2name = N'NOMBRENTIDADES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que confirmó o validó el registro; login/credencial del operador (CHAR 20)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOC', @level2type = N'COLUMN', @level2name = N'USUARIOCONFIRMACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de confirmación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOC', @level2type = N'COLUMN', @level2name = N'USUARIOCONFIRMACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOC', @level2type = N'COLUMN', @level2name = N'USUARIOCONFIRMACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de confirmación/validación del registro tomada desde servidor; timestamp de aprobación (DATETIME)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOC', @level2type = N'COLUMN', @level2name = N'FECHACONFIRMACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de confirmación tomada desde el servidor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOC', @level2type = N'COLUMN', @level2name = N'FECHACONFIRMACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOC', @level2type = N'COLUMN', @level2name = N'FECHACONFIRMACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones, notas o comentarios adicionales sobre el registro de costo; campo libre de anotaciones (VARCHAR 500)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOC', @level2type = N'COLUMN', @level2name = N'OBSERVACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOC', @level2type = N'COLUMN', @level2name = N'OBSERVACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOC', @level2type = N'COLUMN', @level2name = N'OBSERVACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificadores de EAPB/EPS en Indigo Vie ERP; códigos de aseguradora o entidad promotora (VARCHAR MAX)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOC', @level2type = N'COLUMN', @level2name = N'IDEAPBVIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id''''S EAPB de vie erp', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOC', @level2type = N'COLUMN', @level2name = N'IDEAPBVIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOC', @level2type = N'COLUMN', @level2name = N'IDEAPBVIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del archivo, concepto o categoría de costo contable; nombre descriptivo del registro (VARCHAR 100)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOC', @level2type = N'COLUMN', @level2name = N'DESCRIPCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'descripción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOC', @level2type = N'COLUMN', @level2name = N'DESCRIPCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOC', @level2type = N'COLUMN', @level2name = N'DESCRIPCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único de 4 caracteres del archivo de cuenta costo; identificador corto alfanumérico (CHAR 4)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOC', @level2type = N'COLUMN', @level2name = N'CODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del archivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOC', @level2type = N'COLUMN', @level2name = N'CODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOC', @level2type = N'COLUMN', @level2name = N'CODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo único del registro; clave primaria autoincrementada (INT IDENTITY)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOC', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOC', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOC', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de cuentas de costos para historia clínica. Registra los centros o categorías de costo utilizados en la clasificación contable y financiera de los servicios de salud, incluyendo su vigencia, entidades asociadas y tipo de consulta.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOC';
