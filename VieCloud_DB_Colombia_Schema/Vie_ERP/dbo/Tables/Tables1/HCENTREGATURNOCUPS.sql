CREATE TABLE [dbo].[HCENTREGATURNOCUPS] (
    [ID]                      INT        IDENTITY (1, 1) NOT NULL,
    [IDHCENTREGATURNOC]       INT        NOT NULL,
    [IDHCENTREGATURNOCPACIEN] INT        NOT NULL,
    [TIPOSERVICIO]            INT        NOT NULL,
    [NUMEFOLIO]               NCHAR (10) NOT NULL,
    [CODSERIPS]               CHAR (20)  NOT NULL,
    [ESTADO]                  INT        NOT NULL,
    CONSTRAINT [PK_HCENTREGATURNOLAB_1] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCENTREGATURNOCUPS_HCENTREGATURNOC] FOREIGN KEY ([IDHCENTREGATURNOC]) REFERENCES [dbo].[HCENTREGATURNOC] ([ID]),
    CONSTRAINT [FK_HCENTREGATURNOCUPS_HCENTREGATURNOCPACIEN] FOREIGN KEY ([IDHCENTREGATURNOCPACIEN]) REFERENCES [dbo].[HCENTREGATURNOCPACIEN] ([ID]),
    CONSTRAINT [FK_HCENTREGATURNOCUPS_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS])
);




GO



GO
CREATE NONCLUSTERED INDEX [IX_HCENTREGATURNOCUPS_IDHCENTREGATURNOC_IDHCENTREGATURNOCPACIEN]
    ON [dbo].[HCENTREGATURNOCUPS]([IDHCENTREGATURNOC] ASC, [IDHCENTREGATURNOCPACIEN] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del servicio CUPS por tipo: Imágenes (Solicitado, Estudio Realizado, Imagen Procesada, Estudio Interpretado, Remitido, Anulado, Extramural); Interconsultas (Solicitado, Solicitud Enviada, Interconsulta Realizada, Extramural, Anulado); Laboratorios (Solicitado, Muestra Recolectada, Resultado Entregado, Examen Interpretado, Remitido, Anulado, Extramural, Muestra Parcial); Patologías (Solicitado, Muestra Recolectada, Resultado Entregado, Examen Interpretado, Remitido, Anulado, Extramural); Procedimientos No Quirúrgicos (Ordenado, Completado, Interpretado, Sin Interfaz, Anulado, Sala Programada); Procedimientos Quirúrgicos (Solicitado, Sala Programada, Cancelado, Resultado Revisado). INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOCUPS', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estados CUPS    --Imagenes--  Estado Servicio IPS  1: Solicitado  2: Estudio Realizado  3: Imagen Procesada  4: Estudio Interpretado  5: Remitido  6: Anulado  7: Extramural    -- Interconsultas --  1: Solicitado  2: Solicitud Enviada  3: Interconsulta Realizada  4: Extramural  5: Anulado    -- Laboratorios --  1: Solicitado  2: Muestra Recolectada  3: Resultado Entregado  4: Examen Interpretado  5: Remitido  6: Anulado  7: Extramural  8:Muestra Recolectada Parcialmente    --Patologias--  1: Solicitado  2: Muestra Recolectada  3: Resultado Entregado  4: Examen Interpretado  5: Remitido  6: Anulado  7: Extramural    --ProcedimientosNoQx--  1: Ordenado  2: Completado  3: Interpretado  4: Sin Interfaz  5: Anulado  6: Sala programada    -- ProcedimientosQx --  1: Solicitado  2: Sala Programada  3: Cancelado  4: Resultado Revisado  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOCUPS', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOCUPS', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único CUPS/RIPS del procedimiento o servicio facturado. CHAR(20), referencia a tabla INCUPSIPS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOCUPS', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Unico de Procedimientos y Servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOCUPS', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOCUPS', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio o comprobante del servicio prestado (diagnóstico, laboratorio, procedimiento, cirugía). NCHAR(10).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOCUPS', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOCUPS', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOCUPS', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación del tipo de servicio CUPS: 1=Imágenes Diagnósticas, 2=Interconsultas, 3=Laboratorios, 4=Patologías, 5=Procedimientos No Quirúrgicos, 6=Procedimientos Quirúrgicos. INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOCUPS', @level2type = N'COLUMN', @level2name = N'TIPOSERVICIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Procedimiento (CUPS)  1 : Imagenes_Dx  2 : Interconsultas   3 : Laboratorios  4 : Patologias   5 : ProcedimientosNoQx   6 : ProcedimientosQx   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOCUPS', @level2type = N'COLUMN', @level2name = N'TIPOSERVICIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOCUPS', @level2type = N'COLUMN', @level2name = N'TIPOSERVICIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador foráneo de paciente en turno de atención. Referencia a tabla HCENTREGATURNOCPACIEN (cabecera paciente-turno).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOCUPS', @level2type = N'COLUMN', @level2name = N'IDHCENTREGATURNOCPACIEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla cabecera HCENTREGATURNOCPACIENTE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOCUPS', @level2type = N'COLUMN', @level2name = N'IDHCENTREGATURNOCPACIEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOCUPS', @level2type = N'COLUMN', @level2name = N'IDHCENTREGATURNOCPACIEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador foráneo de turno de atención en centro. Referencia a tabla HCENTREGATURNOC (cabecera turno centro).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOCUPS', @level2type = N'COLUMN', @level2name = N'IDHCENTREGATURNOC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla cabecera HCENTREGATURNO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOCUPS', @level2type = N'COLUMN', @level2name = N'IDHCENTREGATURNOC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOCUPS', @level2type = N'COLUMN', @level2name = N'IDHCENTREGATURNOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único auto-numérico de la entrega de servicio CUPS en turno. INT IDENTITY, clave primaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOCUPS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Autonumerico ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOCUPS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOCUPS', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de servicios CUPS (procedimientos, exámenes, consultas) incluidos en cada entrega de turno de historia clínica. Registra qué servicios fueron entregados por folio dentro de un proceso de cierre o traspaso de turno.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOCUPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOCUPS';
