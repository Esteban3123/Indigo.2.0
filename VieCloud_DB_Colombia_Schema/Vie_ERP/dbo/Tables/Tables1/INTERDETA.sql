CREATE TABLE [dbo].[INTERDETA] (
    [AUTO]         INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCONCEC]    INT       NOT NULL,
    [AUTOLABOR]    INT       NOT NULL,
    [CODSERIPS]    CHAR (20) NOT NULL,
    [NUMEFOLIO]    CHAR (10) NULL,
    [ORDTIP]       CHAR (3)  NULL,
    [TIPOINTERFAZ] TINYINT   NULL,
    CONSTRAINT [PK_INTERDETA] PRIMARY KEY CLUSTERED ([AUTO] ASC)
);




GO
CREATE NONCLUSTERED INDEX [_dta_index_INTERDETA_7_1099919040__K3_K1_K4_K2_K5_6]
    ON [dbo].[INTERDETA]([AUTOLABOR] ASC, [AUTO] ASC, [CODSERIPS] ASC, [CODCONCEC] ASC, [NUMEFOLIO] ASC)
    INCLUDE([ORDTIP]);


GO
CREATE NONCLUSTERED INDEX [IX_INTERDETA_ORDTIP]
    ON [dbo].[INTERDETA]([ORDTIP] ASC)
    INCLUDE([CODCONCEC], [AUTOLABOR]);


GO
ALTER INDEX [IX_INTERDETA_ORDTIP]
    ON [dbo].[INTERDETA] DISABLE;




GO
CREATE NONCLUSTERED INDEX [_dta_index_INTERDETA_7_1099919040__K4_K3_K2_K5_K1_6]
    ON [dbo].[INTERDETA]([CODSERIPS] ASC, [AUTOLABOR] ASC, [CODCONCEC] ASC, [NUMEFOLIO] ASC, [AUTO] ASC)
    INCLUDE([ORDTIP]);


GO
CREATE NONCLUSTERED INDEX [_dta_index_INTERDETA_6_1099919040__K2_K4_K3]
    ON [dbo].[INTERDETA]([CODCONCEC] ASC, [CODSERIPS] ASC, [AUTOLABOR] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de interfaz (TINYINT): 1=Laboratorios, 2=Patologías. Clasificación de origen del análisis/estudio clínico en el detalle de intercambio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERDETA', @level2type = N'COLUMN', @level2name = N'TIPOINTERFAZ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Laboratorios  2 - Patologias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERDETA', @level2type = N'COLUMN', @level2name = N'TIPOINTERFAZ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERDETA', @level2type = N'COLUMN', @level2name = N'TIPOINTERFAZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de orden (CHAR 3): AMB=Ambulatorio, INT=Intrahospitalario. Modalidad de atención asociada a la solicitud de laboratorio o patología.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERDETA', @level2type = N'COLUMN', @level2name = N'ORDTIP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Orden AMB-Ambulatorio    INT-Intrahospitalario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERDETA', @level2type = N'COLUMN', @level2name = N'ORDTIP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERDETA', @level2type = N'COLUMN', @level2name = N'ORDTIP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio (CHAR 10, nullable): Identificador único del documento/comprobante de laboratorio o patología en el sistema externo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERDETA', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERDETA', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERDETA', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del servicio IPS (CHAR 20, NOT NULL): Código de la Institución Prestadora de Salud que ejecutó el análisis, procedimiento o examen clínico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERDETA', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Servicio IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERDETA', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERDETA', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Autonumérico de laboratorio o patología (INT, FK): Referencia cruzada a tablas origen (HCORDLABO, AMBORDLAB para laboratorios; HCORDPATO, AMBORDPAT para patologías). Vincula el detalle al examen específico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERDETA', @level2type = N'COLUMN', @level2name = N'AUTOLABOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla de laboratorios y patologias.    puede ser el Auto de las sig, tablas:     Laboratorios:    HCORDLABO  AMBORDLAB    Patologias:    HCORDPATO  AMBORDPAT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERDETA', @level2type = N'COLUMN', @level2name = N'AUTOLABOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERDETA', @level2type = N'COLUMN', @level2name = N'AUTOLABOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código consecutivo de intercambio (INT, FK INTERCABE): Identificador del encabezado de intercambio padre. Agrupa todos los detalles de laboratorios y patologías de una misma transacción.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERDETA', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del consecutivo de INTERCABE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERDETA', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERDETA', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Autonumérico interno (INT, IDENTITY, PK): Identificador único autoincrementable del registro de detalle en INTERDETA. Clave primaria clustered.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERDETA', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico Interno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERDETA', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERDETA', @level2type = N'COLUMN', @level2name = N'AUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de la interfaz de intercambio entre servicios o laboratorios: relaciona cada servicio (CUPS) con una orden de laboratorio y su concepto, permitiendo identificar el folio, el tipo de orden y el tipo de interfaz para la integración con sistemas externos de diagnóstico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERDETA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERDETA';
