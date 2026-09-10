CREATE TABLE [dbo].[ODOPARTRA] (
    [CONSECTRA]            INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODIGOTRA]            NCHAR (10)      NOT NULL,
    [DESCRITRA]            NCHAR (150)     NOT NULL,
    [CODSERIPS]            CHAR (20)       NULL,
    [INDICECPO]            CHAR (1)        NULL,
    [INDICECEO]            CHAR (1)        NULL,
    [IMAGENTRA]            VARBINARY (MAX) NULL,
    [APLICATRA]            CHAR (1)        NULL,
    [COLORSTRA]            NCHAR (40)      NULL,
    [UBICACION]            CHAR (1)        NOT NULL,
    [OBSERVTRA]            NVARCHAR (200)  NULL,
    [ESTADOTRA]            BIT             NOT NULL,
    [APLICAOTROSTRATAMIEN] BIT             NULL,
    CONSTRAINT [PK_ODOPARTRA] PRIMARY KEY CLUSTERED ([CONSECTRA] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que determina si aplican otros tratamientos odontológicos adicionales (1=Sí/True, 0=No/False)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARTRA', @level2type = N'COLUMN', @level2name = N'APLICAOTROSTRATAMIEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda aplica otros tratamientos  1 = true', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARTRA', @level2type = N'COLUMN', @level2name = N'APLICAOTROSTRATAMIEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARTRA', @level2type = N'COLUMN', @level2name = N'APLICAOTROSTRATAMIEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del tratamiento odontológico: True=Activo/Vigente, False=Inactivo/Descontinuado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARTRA', @level2type = N'COLUMN', @level2name = N'ESTADOTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del tratamiento    True: Activo  False: Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARTRA', @level2type = N'COLUMN', @level2name = N'ESTADOTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARTRA', @level2type = N'COLUMN', @level2name = N'ESTADOTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Notas u observaciones clínicas asociadas al diagnóstico odontológico parametrizado (NVARCHAR 200)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARTRA', @level2type = N'COLUMN', @level2name = N'OBSERVTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observacion del diagnostico parametrizado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARTRA', @level2type = N'COLUMN', @level2name = N'OBSERVTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARTRA', @level2type = N'COLUMN', @level2name = N'OBSERVTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ubicación anatómica del tratamiento odontológico: 0=No Aplica, 1=Diente específico, 2=Sextante, 3=Cuadrante, 4=Maxilar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARTRA', @level2type = N'COLUMN', @level2name = N'UBICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ubicacion:  0: No Aplica  1: Diente  2: Sextante  3: Cuadrante  4: Maxilar   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARTRA', @level2type = N'COLUMN', @level2name = N'UBICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARTRA', @level2type = N'COLUMN', @level2name = N'UBICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Color o característica visible de la superficie dental o zona tratada (NCHAR 40)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARTRA', @level2type = N'COLUMN', @level2name = N'COLORSTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Color de la Superficie', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARTRA', @level2type = N'COLUMN', @level2name = N'COLORSTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARTRA', @level2type = N'COLUMN', @level2name = N'COLORSTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nivel de aplicabilidad del tratamiento: 1=A nivel de Diente, 2=A nivel de Superficie dental', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARTRA', @level2type = N'COLUMN', @level2name = N'APLICATRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Para determinar si aplica a  1: Diente  2: Superficie', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARTRA', @level2type = N'COLUMN', @level2name = N'APLICATRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARTRA', @level2type = N'COLUMN', @level2name = N'APLICATRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Archivo de imagen digital (VARBINARY MAX) asociado al diagnóstico u procedimiento odontológico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARTRA', @level2type = N'COLUMN', @level2name = N'IMAGENTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Imagen relacionada al diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARTRA', @level2type = N'COLUMN', @level2name = N'IMAGENTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARTRA', @level2type = N'COLUMN', @level2name = N'IMAGENTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Índice CEO (Cariado-Extraído-Obturado) para dientes permanentes: N=No Aplica, C=Cariado, E=Extraído, O=Obturado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARTRA', @level2type = N'COLUMN', @level2name = N'INDICECEO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indice CEO    N: No Aplica  C: Cariado  E: Extraido  O: Obturado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARTRA', @level2type = N'COLUMN', @level2name = N'INDICECEO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARTRA', @level2type = N'COLUMN', @level2name = N'INDICECEO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Índice CPO (Cariado-Perdido-Obturado) para dentición mixta/temporal: N=No Aplica, C=Cariado, P=Perdido, O=Obturado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARTRA', @level2type = N'COLUMN', @level2name = N'INDICECPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indice CPO    N: No Aplica  C: Cariado  P: Perdido  O: Obturado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARTRA', @level2type = N'COLUMN', @level2name = N'INDICECPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARTRA', @level2type = N'COLUMN', @level2name = N'INDICECPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del servicio odontológico en IPS (OBSOLETO desde 08-03-2019; migrar a tabla ODOPARTRACUPSD para códigos CUPS)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARTRA', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del servicio IPS    Campo que queda obsoleto desde el día 08-03-2019 ya que se crea otra tabla para alamacenar los CUPS llamada ODOPARTRACUPSD.    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARTRA', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARTRA', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción detallada o nombre del tratamiento odontológico (NCHAR 150)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARTRA', @level2type = N'COLUMN', @level2name = N'DESCRITRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion del tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARTRA', @level2type = N'COLUMN', @level2name = N'DESCRITRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARTRA', @level2type = N'COLUMN', @level2name = N'DESCRITRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del tratamiento odontológico (NCHAR 10, identificador de búsqueda)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARTRA', @level2type = N'COLUMN', @level2name = N'CODIGOTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARTRA', @level2type = N'COLUMN', @level2name = N'CODIGOTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARTRA', @level2type = N'COLUMN', @level2name = N'CODIGOTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador secuencial auto-incremental (INT IDENTITY) del registro de tratamiento en la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARTRA', @level2type = N'COLUMN', @level2name = N'CONSECTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de los tratamientos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARTRA', @level2type = N'COLUMN', @level2name = N'CONSECTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARTRA', @level2type = N'COLUMN', @level2name = N'CONSECTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de tratamientos odontológicos disponibles en el sistema. Registra cada tipo de tratamiento dental con su código, descripción, servicio CUPS asociado, índices de salud oral (CPO/CEO), imagen de referencia y configuración de aplicación por ubicación dental.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARTRA';
