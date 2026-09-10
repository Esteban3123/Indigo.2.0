CREATE TABLE [dbo].[HCRADSIMULACION] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCRADORDEN]        INT           NOT NULL,
    [PROTOCOLO]           INT           NOT NULL,
    [OTROPROTOCOLO]       VARCHAR (100) NULL,
    [SOPPOPLITEO]         BIT           NULL,
    [SOPDUALLEG]          BIT           NULL,
    [SOPOVERLAY]          BIT           NULL,
    [SOPCARAPRONO]        BIT           NULL,
    [SOPCOJIN]            BIT           NULL,
    [SOPWING]             BIT           NULL,
    [SOPRETRACTOR]        BIT           NULL,
    [SOPHOLDER]           BIT           NULL,
    [SOPMARCO]            BIT           NULL,
    [SOPPIES]             BIT           NULL,
    [SOPBELLY]            BIT           NULL,
    [SOPPLANO]            BIT           NULL,
    [SOPVAC]              BIT           NULL,
    [SOPTIMO]             BIT           NULL,
    [SOPMASCARA]          BIT           NULL,
    [SOPOTROS]            BIT           NULL,
    [OTROSSOPORTE]        VARCHAR (100) NULL,
    [POSICION]            INT           NOT NULL,
    [OTROSPOSICION]       VARCHAR (100) NULL,
    [BRAZOS]              INT           NULL,
    [OTROSBRAZOS]         VARCHAR (100) NULL,
    [PIES]                INT           NULL,
    [CABEZA]              INT           NULL,
    [OTROSCABEZA]         VARCHAR (100) NULL,
    [BOLUScm]             INT           NULL,
    [PARAFINA]            BIT           NULL,
    [WALTURA]             VARCHAR (15)  NULL,
    [WLONGITUD]           INT           NULL,
    [PINCLINAGENERAL]     VARCHAR (30)  NULL,
    [PCABEZA]             VARCHAR (30)  NULL,
    [PTATTO]              VARCHAR (30)  NULL,
    [PGLUTEOS]            VARCHAR (30)  NULL,
    [BDERECHOBASE]        VARCHAR (30)  NULL,
    [BDERECHOPOSICION]    VARCHAR (30)  NULL,
    [BDERECHOALTURA]      VARCHAR (30)  NULL,
    [BIZQUIBASE]          VARCHAR (30)  NULL,
    [BIZQUIPOSICION]      VARCHAR (30)  NULL,
    [BIZQUIALTURA]        VARCHAR (30)  NULL,
    [MPOSCION]            VARCHAR (30)  NULL,
    [MFIX]                VARCHAR (30)  NULL,
    [MANGULO]             VARCHAR (30)  NULL,
    [OBSERVACION]         VARCHAR (MAX) NULL,
    [CODCENATESIMULACION] CHAR (10)     NULL,
    CONSTRAINT [PK_HCRADSIMULACION] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCRADSIMULACION_ADCENATEN] FOREIGN KEY ([CODCENATESIMULACION]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCRADSIMULACION_HCRADORD] FOREIGN KEY ([IDHCRADORDEN]) REFERENCES [dbo].[HCRADORDEN] ([ID])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención (FK→ADCENATEN) seleccionado al crear la simulación radiológica; CHAR(10), búsqueda por unidad funcional, institución prestadora.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'CODCENATESIMULACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Centro de Atencion que seleccionar al crear la simulacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'CODCENATESIMULACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'CODCENATESIMULACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones generales de la simulación radiológica; VARCHAR(MAX), notas clínicas, hallazgos técnicos, recomendaciones del simulador.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'OBSERVACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la Observacion ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'OBSERVACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'OBSERVACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ángulo de la muñeca registrado en simulación; VARCHAR(30), posicionamiento de miembro superior para radioterapia/imagen diagnóstica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'MANGULO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el angulo muñeca', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'MANGULO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'MANGULO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fijación (fix) de la muñeca en simulación; VARCHAR(30), restricción de movimiento, estabilización de extremidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'MFIX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el fix muñeca', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'MFIX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'MFIX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Posición de la muñeca durante simulación radiológica; VARCHAR(30), alineación anatómica para protocolo de tratamiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'MPOSCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda Posicion Muñeca', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'MPOSCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'MPOSCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Altura del brazo izquierdo en simulación; VARCHAR(30), medida vertical, posicionamiento de extremidad superior izquierda.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'BIZQUIALTURA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda Altura Brazo Izquierdo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'BIZQUIALTURA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'BIZQUIALTURA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Posición del brazo izquierdo en simulación radiológica; VARCHAR(30), alineación, colocación anatómica izquierda.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'BIZQUIPOSICION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'GuardaPosicion Brazo Izquierdo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'BIZQUIPOSICION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'BIZQUIPOSICION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Base de apoyo del brazo izquierdo en simulación; VARCHAR(30), punto de anclaje, soporte estructural extremidad izquierda.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'BIZQUIBASE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda Base BrazoIz quierdo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'BIZQUIBASE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'BIZQUIBASE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Altura del brazo derecho en simulación; VARCHAR(30), medida vertical, posicionamiento de extremidad superior derecha.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'BDERECHOALTURA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda Altura Brazo Derecho', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'BDERECHOALTURA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'BDERECHOALTURA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Posición del brazo derecho en simulación radiológica; VARCHAR(30), alineación, colocación anatómica derecha.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'BDERECHOPOSICION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda Posicion Brazo Derecho', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'BDERECHOPOSICION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'BDERECHOPOSICION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Base de apoyo del brazo derecho en simulación; VARCHAR(30), punto de anclaje, soporte estructural extremidad derecha.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'BDERECHOBASE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda base Brazo derecho', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'BDERECHOBASE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'BDERECHOBASE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Posición de glúteos en simulación radiológica; VARCHAR(30), posicionamiento región glútea, pelvis.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'PGLUTEOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda posicion Gluteos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'PGLUTEOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'PGLUTEOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Posición del tronco/tatto en simulación; VARCHAR(30), alineación torácica, posicionamiento región anterior/posterior.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'PTATTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda posicion Tatto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'PTATTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'PTATTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Posición de la cabeza en simulación radiológica; VARCHAR(30), alineación craneal, posicionamiento región cefálica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'PCABEZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda posicion cabeza', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'PCABEZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'PCABEZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Inclinación general del paciente en simulación; VARCHAR(30), ángulo de posición corporal, postura global.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'PINCLINAGENERAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la inclinacion general', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'PINCLINAGENERAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'PINCLINAGENERAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Longitud del wedge/cuña de posicionamiento; INT, dimensión linear de soporte complementario en cm.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'WLONGITUD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la longitud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'WLONGITUD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'WLONGITUD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Altura del wedge/cuña de posicionamiento; VARCHAR(15), dimensión vertical de soporte complementario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'WALTURA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda Altura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'WALTURA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'WALTURA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si se usa parafina como material de protección/inmovilización; BIT (1=sí, 0=no), técnica de confección de moldes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'PARAFINA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda si tiene Check Parafina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'PARAFINA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'PARAFINA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de bolus (material de compensación de dosis) en centímetros; INT, grosor material radiosensible en radioterapia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'BOLUScm';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la cantida de Bolus cm', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'BOLUScm';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'BOLUScm';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de otros dispositivos/posiciones de cabeza no listados; VARCHAR(100), extensiones, rotación, opciones adicionales.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'OTROSCABEZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guard Otros Cabeza', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'OTROSCABEZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'OTROSCABEZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Posición de cabeza en simulación; INT (1=Neutra, 2=Rotación, 3=Extensión, 4=Otros), alineación cefálica para protocolo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'CABEZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda Brazos   1 = Neutra    2 = Rotacion      3 =  extension     4 = Otros', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'CABEZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'CABEZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Posición de pies en simulación radiológica; INT (1=Hacia Gantry, 2=Rana), orientación de extremidades inferiores.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'PIES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda posicion 1 = Hacia Gantry      2 = Rana', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'PIES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'PIES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de otras posiciones de brazos no listadas; VARCHAR(100), combinaciones, opciones personalizadas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'OTROSBRAZOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda Otros Brazos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'OTROSBRAZOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'OTROSBRAZOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Posición de brazos en simulación; INT (1=Largo cuerpo, 2=Cabeza, 3=Pecho, 4=Otros), alineación extremidades superiores.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'BRAZOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda Brazos 1 =  alo largo del cuerpo    2 = Sobre la cabeza    3 = sobre el pechco     4 = Otros', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'BRAZOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'BRAZOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de otra posición corporal no listada; VARCHAR(100), postura alternativa, configuración personalizada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'OTROSPOSICION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda Otros Posicion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'OTROSPOSICION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'OTROSPOSICION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Posición principal del paciente en simulación; INT (1=Prono, 2=Supina, 3=Otros), decúbito para protocolo radiológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'POSICION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda posicion   1 = Prono    2 = Supina    3 = Otros', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'POSICION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'POSICION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de otros soportes/accesorios no listados; VARCHAR(100), dispositivos complementarios, inmovilización adicional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'OTROSSOPORTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda Otros Soporte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'OTROSSOPORTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'OTROSSOPORTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si se utiliza soporte Otros (genérico); BIT (1=sí, 0=no), dispositivo complementario no clasificado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'SOPOTROS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda si tiene el soporte Otros   1 = true  0 = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'SOPOTROS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'SOPOTROS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si se utiliza máscara de inmovilización; BIT (1=sí, 0=no), dispositivo termoplástico región craneal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'SOPMASCARA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda si tiene el soporte Mascara  1 = true  0 = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'SOPMASCARA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'SOPMASCARA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si se utiliza soporte Timo para región mediastinal; BIT (1=sí, 0=no), protección tisular torácica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'SOPTIMO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda si tiene el soporte Timo   1 = true  0 = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'SOPTIMO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'SOPTIMO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si se utiliza colchón Vac Lok (vacío); BIT (1=sí, 0=no), inmovilización por presión negativa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'SOPVAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda si tiene el soporte Vac Lok  1 = true  0 = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'SOPVAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'SOPVAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si se utiliza plano inclinado como soporte; BIT (1=sí, 0=no), cuña angular para posicionamiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'SOPPLANO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda si tiene el soporte Plano inclinado  1 = true  0 = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'SOPPLANO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'SOPPLANO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si se utiliza Belly Board (tablilla abdominal); BIT (1=sí, 0=no), protección/desplazamiento vísceras.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'SOPBELLY';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda si tiene el soporte Belly Board 1 = true  0 = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'SOPBELLY';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'SOPBELLY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si se utiliza soporte de pies; BIT (1=sí, 0=no), dispositivo inmovilización extremidades inferiores.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'SOPPIES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda si tiene el soporte Pies 1 = true  0 = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'SOPPIES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'SOPPIES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si se utiliza marco estereotáctico; BIT (1=sí, 0=no), sistema precisión radiocirugía/radiología.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'SOPMARCO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda si tiene el soporte Marco estereotaxico  1 = true  0 = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'SOPMARCO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'SOPMARCO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si se utiliza Holder Prono para posición prona; BIT (1=sí, 0=no), dispositivo sujeción rostro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'SOPHOLDER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda si tiene el soporte Holder Prono  1 = true  0 = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'SOPHOLDER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'SOPHOLDER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si se utiliza retractor de hombros; BIT (1=sí, 0=no), desplazamiento tisular región axilar.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'SOPRETRACTOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda si tiene el soporte Retractor de Hombros   1 = true  0 = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'SOPRETRACTOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'SOPRETRACTOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si se utiliza soporte WING (ala); BIT (1=sí, 0=no), dispositivo lateral posicionamiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'SOPWING';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda si tiene el soporte WING  1 = true  0 = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'SOPWING';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'SOPWING';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si se utiliza cojín de posicionamiento; BIT (1=sí, 0=no), almohada anatómica inmovilización.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'SOPCOJIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda si tiene el soporte Cojin 1 = true  0 = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'SOPCOJIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'SOPCOJIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si se utiliza soporte Cara Prono; BIT (1=sí, 0=no), dispositivo apoyo rostro en decúbito prono.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'SOPCARAPRONO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda si tiene el soporte Cara Prono  1 = true  0 = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'SOPCARAPRONO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'SOPCARAPRONO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si se utiliza OverLay (superposición); BIT (1=sí, 0=no), lámina acrílica/material transparente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'SOPOVERLAY';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda si tiene  el soporte OverLay  1 = true  0 = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'SOPOVERLAY';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'SOPOVERLAY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si se utiliza soporte LEG (extremidades); BIT (1=sí, 0=no), inmovilización dual de miembros inferiores.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'SOPDUALLEG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda si tiene  el soporte  LEG 1 = true  0 = false  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'SOPDUALLEG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'SOPDUALLEG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si se utiliza soporte poplíteo (detrás rodilla); BIT (1=sí, 0=no), apoyo bajo rodillas en supina.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'SOPPOPLITEO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda si tiene  el soporte popliteo 1 = true  0 = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'SOPPOPLITEO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'SOPPOPLITEO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de protocolo radiológico personalizado; VARCHAR(100), clasificación alternativa, especificación adicional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'OTROPROTOCOLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda otros protocolo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'OTROPROTOCOLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'OTROPROTOCOLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Protocolo radiológico/radioterapia; INT (1=Cráneo, 2=Cabeza-cuello, 3=Tórax, 4=Abdomen, 5=Pelvis, 6=Extremidades, 7=Radiocirugía, 8=Otros), tipo de simulación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'PROTOCOLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda protocolo   1= Craneo 2 = cabeza y cuello 3= torax 4= abdomen 5 =pelvis 6= extramidades 7 =radio cirugia 8 = otros', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'PROTOCOLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'PROTOCOLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de orden radiológica/radioterapia (FK→HCRADORDEN); INT, referencia a orden médica de imagen/tratamiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'IDHCRADORDEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el id de la orden medica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'IDHCRADORDEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'IDHCRADORDEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK) de registro de simulación radiológica; INT IDENTITY, consecutivo tabla HCRADSIMULACION.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de simulación de radioterapia asociado a una orden de radiación. Guarda la configuración del protocolo, posicionamiento del paciente, soportes utilizados, parámetros de bolus y parafina, referencias de tatuajes y marcas corporales, y datos del equipo (brazo, mesa, colimador) para reproducir con exactitud el setup en cada sesión de tratamiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION';
