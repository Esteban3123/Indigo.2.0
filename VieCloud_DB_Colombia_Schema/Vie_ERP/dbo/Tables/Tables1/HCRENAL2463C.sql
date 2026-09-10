CREATE TABLE [dbo].[HCRENAL2463C] (
    [ID]          INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CONSEC]      VARCHAR (10)  NOT NULL,
    [CODCENATE]   CHAR (10)     NOT NULL,
    [DESCRIPCI]   VARCHAR (100) NOT NULL,
    [EAPB]        CHAR (9)      NULL,
    [FECHINICIAL] DATE          NOT NULL,
    [FECHFINAL]   DATE          NOT NULL,
    [ESTADO]      TINYINT       NULL,
    [FECREGCRE]   DATETIME      NOT NULL,
    [CODUSUCRE]   CHAR (20)     NOT NULL,
    [FECREGMOD]   DATETIME      NULL,
    [CODUSUMOD]   CHAR (20)     NULL,
    [FECREGANU]   DATETIME      NULL,
    [CODUSUANU]   CHAR (20)     NULL,
    [FECCONFREG]  DATETIME      NULL,
    [CODUSUCONF]  CHAR (20)     NULL,
    [IDEAPBVIE]   INT           NULL,
    CONSTRAINT [PK_HCRENAL2463C] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la entidad administradora de prestaciones (EAPBVie); clave foránea para vincular la EAPB responsable del reporte normativo 2463', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'IDEAPBVIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la entidad administradora de VIE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'IDEAPBVIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'IDEAPBVIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario que confirma el registro; usuario operador responsable de la validación y aprobación del documento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'CODUSUCONF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código Confirmación del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'CODUSUCONF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'CODUSUCONF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de confirmación del registro; marca de tiempo cuando se valida y aprueba el documento Res. 2463', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'FECCONFREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Confirmación del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'FECCONFREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'FECCONFREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario que anula o cancela el registro; identificación del operador responsable de la anulación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'CODUSUANU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'CODUSUANU', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'CODUSUANU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'CODUSUANU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de anulación del registro; marca de tiempo cuando se cancela o invalida el documento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'FECREGANU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Anulacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'FECREGANU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'FECREGANU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario que modifica el registro; identificación del operador que realiza cambios posteriores a la creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'CODUSUMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de Modificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'CODUSUMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'CODUSUMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de modificación del registro; marca de tiempo de la última actualización del documento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'FECREGMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'FECREGMOD', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'FECREGMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'FECREGMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario que crea el registro; identificación del operador responsable de la generación inicial del documento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'CODUSUCRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de Usuario de creacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'CODUSUCRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'CODUSUCRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro; marca de tiempo de la generación inicial del documento en el sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'FECREGCRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de  Creacion de registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'FECREGCRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'FECREGCRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del documento Res. 2463: 1=Registrado, 2=Confirmado, 3=Anulado; controla el ciclo de vida del reporte normativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del Documento.  1 - Registrado  2 - Confirmado  3 - Anulado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha final del período cubierto en el reporte Res. 2463; fecha de cierre o término de la información reportada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'FECHFINAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha final Reporte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'FECHFINAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'FECHFINAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha inicial del período cubierto en el reporte Res. 2463; fecha de inicio o apertura de la información reportada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'FECHINICIAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Inicial de reporte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'FECHINICIAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'FECHINICIAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la entidad administradora de prestaciones de salud (EAPB); identificación de la aseguradora responsable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'EAPB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Entidad Administradoras', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'EAPB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'EAPB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción o título del documento según Resolución 2463; campo que detalla el contenido o clasificación del reporte normativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'DESCRIPCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de Documento Res. 2463', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'DESCRIPCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'DESCRIPCI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención; identificación de la unidad funcional o sede que genera el reporte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Centro de  Atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo del registro; número secuencial único para rastrear y ordenar documentos dentro del período', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'CONSEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'CONSEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'CONSEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único auto-incremental de la tabla; clave primaria para integridad referencial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de contratos o programas de atención renal (nefrología) por centro de atención y EAPB, con vigencia por fechas de inicio y fin, incluyendo trazabilidad de creación, modificación, anulación y confirmación del registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C';
