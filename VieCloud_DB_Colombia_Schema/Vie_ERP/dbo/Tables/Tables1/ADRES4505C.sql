CREATE TABLE [dbo].[ADRES4505C] (
    [ID]          INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CONSEC]      VARCHAR (10)  NOT NULL,
    [CONSECREPOR] VARCHAR (10)  NULL,
    [DESCRIPCI]   VARCHAR (100) NOT NULL,
    [EAPB]        CHAR (9)      NULL,
    [FECHINICIAL] DATETIME      NOT NULL,
    [FECHFINAL]   DATETIME      NOT NULL,
    [ESTADO]      TINYINT       NULL,
    [FECREGCRE]   DATETIME      NOT NULL,
    [CODUSUCRE]   CHAR (20)     NOT NULL,
    [FECREGMOD]   DATETIME      NULL,
    [CODUSUMOD]   CHAR (20)     NULL,
    [FECREGANU]   DATETIME      NULL,
    [CODUSUANU]   CHAR (20)     NULL,
    [FECCONFREG]  DATETIME      NULL,
    [CODUSUCONF]  CHAR (20)     NULL,
    [CODCENATE]   CHAR (10)     NULL,
    [IDEAPBVIE]   INT           NULL,
    CONSTRAINT [PK_ADRES4505C] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ADRES4505C_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_ADRES4505C_INENTADM] FOREIGN KEY ([EAPB]) REFERENCES [dbo].[INENTADM] ([CODENTADM])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) de la entidad administradora de prestación VIE; referencia a INENTADM; permite rastrear la EAPB/aseguradora responsable del documento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505C', @level2type = N'COLUMN', @level2name = N'IDEAPBVIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la entidad administradora de VIE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505C', @level2type = N'COLUMN', @level2name = N'IDEAPBVIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505C', @level2type = N'COLUMN', @level2name = N'IDEAPBVIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención (CHAR 10); FK a ADCENATEN; unidad funcional, clínica, hospital o punto de prestación de servicios de salud donde se origina el reporte.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505C', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Centro de  Atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505C', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505C', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario que confirmó/validó el registro (CHAR 20); auditoría de quién autorizó el documento Res. 4505.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505C', @level2type = N'COLUMN', @level2name = N'CODUSUCONF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código Confirmación del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505C', @level2type = N'COLUMN', @level2name = N'CODUSUCONF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505C', @level2type = N'COLUMN', @level2name = N'CODUSUCONF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de confirmación del registro (DATETIME); marca cuándo se validó y selló el documento ante la autoridad sanitaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505C', @level2type = N'COLUMN', @level2name = N'FECCONFREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Confirmación del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505C', @level2type = N'COLUMN', @level2name = N'FECCONFREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505C', @level2type = N'COLUMN', @level2name = N'FECCONFREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario que anuló/revocó el registro (CHAR 20); auditoría de quién canceló el documento Res. 4505.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505C', @level2type = N'COLUMN', @level2name = N'CODUSUANU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de Anulacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505C', @level2type = N'COLUMN', @level2name = N'CODUSUANU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505C', @level2type = N'COLUMN', @level2name = N'CODUSUANU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de anulación del registro (DATETIME); indica cuándo se revocó o canceló el documento Res. 4505.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505C', @level2type = N'COLUMN', @level2name = N'FECREGANU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Anulacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505C', @level2type = N'COLUMN', @level2name = N'FECREGANU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505C', @level2type = N'COLUMN', @level2name = N'FECREGANU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario que modificó/editó el registro (CHAR 20); auditoría de cambios posteriores a la creación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505C', @level2type = N'COLUMN', @level2name = N'CODUSUMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de Modificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505C', @level2type = N'COLUMN', @level2name = N'CODUSUMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505C', @level2type = N'COLUMN', @level2name = N'CODUSUMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de última modificación del registro (DATETIME); rastrea cuándo se realizó la última edición del documento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505C', @level2type = N'COLUMN', @level2name = N'FECREGMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificacion ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505C', @level2type = N'COLUMN', @level2name = N'FECREGMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505C', @level2type = N'COLUMN', @level2name = N'FECREGMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario creador/autor del registro (CHAR 20); auditoría de quién generó originalmente el documento Res. 4505.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505C', @level2type = N'COLUMN', @level2name = N'CODUSUCRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de Usuario de creacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505C', @level2type = N'COLUMN', @level2name = N'CODUSUCRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505C', @level2type = N'COLUMN', @level2name = N'CODUSUCRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro (DATETIME); marca la génesis del documento en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505C', @level2type = N'COLUMN', @level2name = N'FECREGCRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de  Creacion de registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505C', @level2type = N'COLUMN', @level2name = N'FECREGCRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505C', @level2type = N'COLUMN', @level2name = N'FECREGCRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del documento Res. 4505 (TINYINT): 1=Registrado (borrador), 2=Confirmado (válido/aprobado), 3=Anulado (revocado); ciclo de vida del reporte normativo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505C', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del Documento.  1 - Registrado  2 - Confirmado  3 - Anulado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505C', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505C', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha final del período de reporte o cobertura (DATETIME); límite superior de la ventana temporal del documento Res. 4505.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505C', @level2type = N'COLUMN', @level2name = N'FECHFINAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha final Reporte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505C', @level2type = N'COLUMN', @level2name = N'FECHFINAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505C', @level2type = N'COLUMN', @level2name = N'FECHFINAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha inicial del período de reporte o cobertura (DATETIME); límite inferior de la ventana temporal del documento Res. 4505.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505C', @level2type = N'COLUMN', @level2name = N'FECHINICIAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Inicial de reporte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505C', @level2type = N'COLUMN', @level2name = N'FECHINICIAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505C', @level2type = N'COLUMN', @level2name = N'FECHINICIAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la entidad administradora de planes de beneficio (CHAR 9); FK a INENTADM; aseguradora o responsable normativo del reporte.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505C', @level2type = N'COLUMN', @level2name = N'EAPB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Entidad Administradoras', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505C', @level2type = N'COLUMN', @level2name = N'EAPB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505C', @level2type = N'COLUMN', @level2name = N'EAPB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción detallada del contenido o asunto del documento Res. 4505 (VARCHAR 100); propósito, alcance o variante del reporte regulatorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505C', @level2type = N'COLUMN', @level2name = N'DESCRIPCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de Documento Res. 4505', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505C', @level2type = N'COLUMN', @level2name = N'DESCRIPCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505C', @level2type = N'COLUMN', @level2name = N'DESCRIPCI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo o código de archivo a reportar (VARCHAR 10); valores permitidos: 01, 02, 03; indica tipo/versión de reporte exigido por autoridad sanitaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505C', @level2type = N'COLUMN', @level2name = N'CONSECREPOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo o codigo de archivo a reportar, solo pueden seleccionar estos tres.  01  02  03', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505C', @level2type = N'COLUMN', @level2name = N'CONSECREPOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505C', @level2type = N'COLUMN', @level2name = N'CONSECREPOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código consecutivo del documento Res. 4505 (VARCHAR 10); identificador secuencial único dentro del período y centro de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505C', @level2type = N'COLUMN', @level2name = N'CONSEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo consecutivo Documentos Res. 4505.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505C', @level2type = N'COLUMN', @level2name = N'CONSEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505C', @level2type = N'COLUMN', @level2name = N'CONSEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autoincrementable de la tabla (INT IDENTITY); clave primaria única del registro de documento Res. 4505.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505C', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505C', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505C', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registros de períodos o contratos de reporte ante ADRES (Administradora de los Recursos del Sistema General de Seguridad Social en Salud), asociados a una EAPB (EPS/aseguradora) y un centro de atención. Permite gestionar las ventanas de tiempo habilitadas para envío de información a la ADRES, con trazabilidad de creación, modificación, anulación y confirmación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505C';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505C';
