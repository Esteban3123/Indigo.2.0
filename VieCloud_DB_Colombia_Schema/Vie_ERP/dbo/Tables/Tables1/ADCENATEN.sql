CREATE TABLE [dbo].[ADCENATEN] (
    [CODCENATE] CHAR (10)       NOT NULL,
    [NOMCENATE] CHAR (100)      NOT NULL,
    [CODIPSSEC] CHAR (13)       NOT NULL,
    [NIVATENCI] INT             NOT NULL,
    [DIRCENATE] CHAR (80)       NOT NULL,
    [INDNUMTEL] CHAR (5)        NOT NULL,
    [NUMTELCEN] CHAR (7)        NOT NULL,
    [NUMEXTTEL] CHAR (6)        NULL,
    [NUMCELCEN] CHAR (10)       NULL,
    [DEPMUNCOD] CHAR (5)        NOT NULL,
    [INDAUDFOR] NUMERIC (18)    NOT NULL,
    [LATITUD]   NUMERIC (18, 6) NULL,
    [LONGITUD]  NUMERIC (18, 6) NULL,
    CONSTRAINT [PK_ADcenaten] PRIMARY KEY CLUSTERED ([CODCENATE] ASC),
    CONSTRAINT [FK_CODCENATE_CODCENATE] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE])
);




GO



GO
CREATE NONCLUSTERED INDEX [IX_ADCENATEN]
    ON [dbo].[ADCENATEN]([CODCENATE] ASC, [NOMCENATE] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Coordenada de longitud geográfica (NUMERIC 18,6) del centro de atención, prestador o unidad funcional; utilizada para geolocalización y mapeo de sedes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCENATEN', @level2type = N'COLUMN', @level2name = N'LONGITUD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la Longitud centro atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCENATEN', @level2type = N'COLUMN', @level2name = N'LONGITUD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCENATEN', @level2type = N'COLUMN', @level2name = N'LONGITUD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Coordenada de latitud geográfica (NUMERIC 18,6) del centro de atención, prestador o unidad funcional; utilizada para geolocalización y mapeo de sedes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCENATEN', @level2type = N'COLUMN', @level2name = N'LATITUD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la  Latitud centro de atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCENATEN', @level2type = N'COLUMN', @level2name = N'LATITUD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCENATEN', @level2type = N'COLUMN', @level2name = N'LATITUD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o indicador de auditoría y formalización (NUMERIC 18); registro de control de conformidad regulatoria del centro de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCENATEN', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCENATEN', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCENATEN', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de identificación del municipio y departamento (CHAR 5) donde está ubicado el centro de atención; geolocalización administrativa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCENATEN', @level2type = N'COLUMN', @level2name = N'DEPMUNCOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Municipio y Departamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCENATEN', @level2type = N'COLUMN', @level2name = N'DEPMUNCOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCENATEN', @level2type = N'COLUMN', @level2name = N'DEPMUNCOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número telefónico móvil o celular (CHAR 10) del centro de atención, prestador o unidad funcional para contacto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCENATEN', @level2type = N'COLUMN', @level2name = N'NUMCELCEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero Telefonico Movil', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCENATEN', @level2type = N'COLUMN', @level2name = N'NUMCELCEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCENATEN', @level2type = N'COLUMN', @level2name = N'NUMCELCEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de extensión telefónica (CHAR 6) asociado al teléfono principal del centro de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCENATEN', @level2type = N'COLUMN', @level2name = N'NUMEXTTEL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Extension', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCENATEN', @level2type = N'COLUMN', @level2name = N'NUMEXTTEL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCENATEN', @level2type = N'COLUMN', @level2name = N'NUMEXTTEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número telefónico fijo (CHAR 10) del prestador, centro de atención o unidad funcional para contacto administrativo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCENATEN', @level2type = N'COLUMN', @level2name = N'NUMTELCEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero Telefonico del Prestador', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCENATEN', @level2type = N'COLUMN', @level2name = N'NUMTELCEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCENATEN', @level2type = N'COLUMN', @level2name = N'NUMTELCEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicativo o prefijo telefónico (CHAR 5) del número telefónico; código de área geográfica del centro de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCENATEN', @level2type = N'COLUMN', @level2name = N'INDNUMTEL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicativo del Numero Telefonico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCENATEN', @level2type = N'COLUMN', @level2name = N'INDNUMTEL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCENATEN', @level2type = N'COLUMN', @level2name = N'INDNUMTEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección postal completa (CHAR 80) del centro de atención, prestador o unidad funcional; ubicación física.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCENATEN', @level2type = N'COLUMN', @level2name = N'DIRCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Direccion del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCENATEN', @level2type = N'COLUMN', @level2name = N'DIRCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCENATEN', @level2type = N'COLUMN', @level2name = N'DIRCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nivel de atención IPS (INT): 1=Primario, 2=Secundario, 3=Terciario; complejidad asistencial del centro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCENATEN', @level2type = N'COLUMN', @level2name = N'NIVATENCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nivel de Atencion IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCENATEN', @level2type = N'COLUMN', @level2name = N'NIVATENCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCENATEN', @level2type = N'COLUMN', @level2name = N'NIVATENCI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único de identificación de la IPS ante la Secretaría Departamental de Salud (CHAR 13); código regulatorio nacional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCENATEN', @level2type = N'COLUMN', @level2name = N'CODIPSSEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la IPS ante la Secretaria Departamental de Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCENATEN', @level2type = N'COLUMN', @level2name = N'CODIPSSEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCENATEN', @level2type = N'COLUMN', @level2name = N'CODIPSSEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o razón social (CHAR 100) del centro de atención, prestador, unidad funcional o institución prestadora de servicios de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCENATEN', @level2type = N'COLUMN', @level2name = N'NOMCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCENATEN', @level2type = N'COLUMN', @level2name = N'NOMCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCENATEN', @level2type = N'COLUMN', @level2name = N'NOMCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único identificador (CHAR 10, PK) del centro de atención, prestador o unidad funcional en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCENATEN', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCENATEN', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCENATEN', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Centros de atención habilitados en el sistema. Guarda la información de cada sede, clínica, hospital o punto de atención donde se prestan servicios de salud, incluyendo datos de contacto, ubicación geográfica y nivel de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCENATEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCENATEN';
