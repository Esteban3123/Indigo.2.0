CREATE TABLE [dbo].[INUBICACI] (
    [AUUBICACI]       CHAR (20)    NOT NULL,
    [DEPMUNCOD]       CHAR (5)     NOT NULL,
    [UBICODIGO]       CHAR (7)     NULL,
    [UBINOMBRE]       CHAR (100)   NOT NULL,
    [TIPOUBICA]       INT          NOT NULL,
    [INDAUDFOR]       NUMERIC (18) NOT NULL,
    [ESTADO]          INT          NULL,
    [IDCOMUNA]        INT          NULL,
    [ID]              INT          IDENTITY (1, 1) NOT NULL,
    [UbicationId]     INT          NULL,
    [UbicationTypeId] INT          NULL,
    CONSTRAINT [PK_INUbicaci] PRIMARY KEY CLUSTERED ([AUUBICACI] ASC),
    CONSTRAINT [FK_INUBICACI_ADCOMUNAS] FOREIGN KEY ([IDCOMUNA]) REFERENCES [dbo].[ADCOMUNAS] ([Id]),
    CONSTRAINT [FK_INUbicaci_INMunicip] FOREIGN KEY ([DEPMUNCOD]) REFERENCES [dbo].[INMUNICIP] ([DEPMUNCOD]) ON UPDATE CASCADE
);




GO



GO





GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tipo de ubicación (INT). Clasificación de la naturaleza de la ubicación en el centro de atención o unidad funcional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUBICACI', @level2type = N'COLUMN', @level2name = N'UbicationTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id tipo de ubicación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUBICACI', @level2type = N'COLUMN', @level2name = N'UbicationTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUBICACI', @level2type = N'COLUMN', @level2name = N'UbicationTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la ubicación (INT). Referencia a la ubicación específica del centro de atención o establecimiento de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUBICACI', @level2type = N'COLUMN', @level2name = N'UbicationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la ubicación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUBICACI', @level2type = N'COLUMN', @level2name = N'UbicationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUBICACI', @level2type = N'COLUMN', @level2name = N'UbicationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo autoincrementable (INT IDENTITY). Clave de acceso interno de la tabla INUBICACI.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUBICACI', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUBICACI', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUBICACI', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK) hacia tabla ADCOMUNAS (INT). Relación geográfica con la comuna, municipalidad o división territorial donde se ubica el establecimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUBICACI', @level2type = N'COLUMN', @level2name = N'IDCOMUNA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relación con la tabla ADCOMUNAS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUBICACI', @level2type = N'COLUMN', @level2name = N'IDCOMUNA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUBICACI', @level2type = N'COLUMN', @level2name = N'IDCOMUNA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la ubicación (INT): 1=Activo, 2=Inactivo. Indica si la ubicación está operativa o inhabilitada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUBICACI', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la Ubicacion 1->Acitvo 2->Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUBICACI', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUBICACI', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de auditoría y formato (NUMERIC 18). Campo de control para auditoría interna, verificación de cambios y cumplimiento normativo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUBICACI', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Columna para Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUBICACI', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUBICACI', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de ubicación (INT): 0=Urbano, 1=Rural. Clasifica la ubicación según su contexto geográfico (ciudad/zona o campo/zona rural).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUBICACI', @level2type = N'COLUMN', @level2name = N'TIPOUBICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Ubicacion:  0 = Urbano  1 = Rural', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUBICACI', @level2type = N'COLUMN', @level2name = N'TIPOUBICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUBICACI', @level2type = N'COLUMN', @level2name = N'TIPOUBICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción o nombre de la ubicación (CHAR 100). Denominación completa de la ubicación, unidad funcional, sala, piso o área del centro de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUBICACI', @level2type = N'COLUMN', @level2name = N'UBINOMBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de la Ubicacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUBICACI', @level2type = N'COLUMN', @level2name = N'UBINOMBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUBICACI', @level2type = N'COLUMN', @level2name = N'UBINOMBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la ubicación (CHAR 15, nullable). Identificador codificado de la ubicación para búsquedas y referencias internas del sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUBICACI', @level2type = N'COLUMN', @level2name = N'UBICODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Ubicacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUBICACI', @level2type = N'COLUMN', @level2name = N'UBICODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUBICACI', @level2type = N'COLUMN', @level2name = N'UBICODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del departamento y municipio (CHAR 5). Referencia geográfica administrativa (FK hacia INMUNICIP) que identifica provincia/región y municipio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUBICACI', @level2type = N'COLUMN', @level2name = N'DEPMUNCOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Departamento y municipio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUBICACI', @level2type = N'COLUMN', @level2name = N'DEPMUNCOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUBICACI', @level2type = N'COLUMN', @level2name = N'DEPMUNCOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de auditoría de ubicación y municipio (CHAR 20, PK). Clave primaria que integra código de ubicación con código municipal para identificación única y auditoría.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUBICACI', @level2type = N'COLUMN', @level2name = N'AUUBICACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Ubicacion y municipio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUBICACI', @level2type = N'COLUMN', @level2name = N'AUUBICACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUBICACI', @level2type = N'COLUMN', @level2name = N'AUUBICACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo maestro de ubicaciones geográficas (municipios, barrios, veredas, comunas) utilizadas para identificar la residencia o procedencia del paciente y otros registros del sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUBICACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUBICACI';
