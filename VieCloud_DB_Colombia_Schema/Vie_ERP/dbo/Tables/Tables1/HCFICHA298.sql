CREATE TABLE [dbo].[HCFICHA298] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [VACU1]               INT           NULL,
    [DOSIS1]              INT           NULL,
    [VIA1]                INT           NULL,
    [SITIO1]              INT           NULL,
    [FECHA1]              DATE          NULL,
    [LOTE1]               VARCHAR (50)  NULL,
    [VACU2]               INT           NULL,
    [DOSIS2]              INT           NULL,
    [VIA2]                INT           NULL,
    [SITIO2]              INT           NULL,
    [FECHA2]              DATE          NULL,
    [LOTE2]               VARCHAR (50)  NULL,
    [VACU3]               INT           NULL,
    [DOSIS3]              INT           NULL,
    [VIA3]                INT           NULL,
    [SITIO3]              INT           NULL,
    [FECHA3]              DATE          NULL,
    [LOTE3]               VARCHAR (50)  NULL,
    [VACU4]               INT           NULL,
    [DOSIS4]              INT           NULL,
    [VIA4]                INT           NULL,
    [SITIO4]              INT           NULL,
    [FECHA4]              DATE          NULL,
    [LOTE4]               NCHAR (10)    NULL,
    [ADENITIS]            BIT           NULL,
    [TIEMPOTRANS]         VARCHAR (50)  NULL,
    [UNIMEDI]             INT           NULL,
    [ANTEPAT]             INT           NULL,
    [CUAL1]               VARCHAR (50)  NULL,
    [ANTEALERG]           INT           NULL,
    [CUAL2]               VARCHAR (50)  NULL,
    [ANTEPREV]            INT           NULL,
    [CUAL3]               VARCHAR (50)  NULL,
    [ESTADFINAL]          INT           NULL,
    [CLASCASO]            INT           NULL,
    [ABSCESO]             BIT           NULL,
    [LINFADE]             BIT           NULL,
    [FIEBRE]              BIT           NULL,
    [CONVUFEBR]           BIT           NULL,
    [CONVUSINF]           BIT           NULL,
    [EPIHIPOTO]           BIT           NULL,
    [PARESTESIA]          BIT           NULL,
    [PARALISIS]           BIT           NULL,
    [ENCEFALO]            BIT           NULL,
    [MENINGI]             BIT           NULL,
    [URTICARIA]           BIT           NULL,
    [ECZEMA]              BIT           NULL,
    [CHOQUE]              BIT           NULL,
    [GUILLAIN]            BIT           NULL,
    [CELULITIS]           BIT           NULL,
    [LLANTO]              BIT           NULL,
    [RUMOR]               BIT           NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA298] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA298_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA298_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA298] NOCHECK CONSTRAINT [CK_HCFICHA298_JSON];




GO
ALTER TABLE [dbo].[HCFICHA298] NOCHECK CONSTRAINT [CK_HCFICHA298_JSON];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacenamiento JSON (VARCHAR MAX) con columnas adicionales desde v03_2021-05-23: FATIGA, DOLORCABEZA, MIALGIA, ARTRALGIA, NAUSEAS, OTROS, CUALOTROS, FABRICANTE1-4 (20 caracteres alfanuméricos). Validado con CHECK isjson().', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nuevas columnas en formato JSON     Desde versión "V03_2021-05-23" -->  - FATIGA   - DOLORCABEZA   - MIALGIA   - ARTRALGIA   - NAUSEAS   - OTROS   - CUALOTROS   - FABRICANTE1,  FABRICANTE2,  FABRICANTE3,  FABRICANTE4 : 20 caracteres alfanumericos    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión de la ficha de notificación de evento adverso post-vacunal. NULL = primera versión; formato VARCHAR(20) para control de cambios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Versión de la ficha de notificación ---> Si es valor nulo = primer versión ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hallazgo semiológico: presencia de rumor cardíaco o pulmonar post-vacunación. Tipo BIT (boolean).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'RUMOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Rumor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'RUMOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'RUMOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hallazgo semiológico: llanto persistente mayor a 3 horas post-vacunación. Tipo BIT (boolean).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'LLANTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Llanto Persistente Mayor a 3 Horas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'LLANTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'LLANTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hallazgo semiológico: inflamación de tejidos blandos/celulitis post-vacunación. Tipo BIT (boolean).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'CELULITIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Celulitis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'CELULITIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'CELULITIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hallazgo neurológico: Síndrome de Guillain-Barré post-vacunación. Tipo BIT (boolean).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'GUILLAIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guillain Barre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'GUILLAIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'GUILLAIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hallazgo clínico crítico: choque anafiláctico, reacción alérgica severa post-vacunación. Tipo BIT (boolean).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'CHOQUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Choque Anafilactico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'CHOQUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'CHOQUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hallazgo dermatológico: eczema/dermatitis post-vacunación. Tipo BIT (boolean).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'ECZEMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Eczema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'ECZEMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'ECZEMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hallazgo dermatológico: urticaria (rash/erupciones cutáneas) post-vacunación. Tipo BIT (boolean).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'URTICARIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Urticaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'URTICARIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'URTICARIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hallazgo clínico: meningitis post-vacunación. Tipo BIT (boolean).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'MENINGI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Meningitis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'MENINGI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'MENINGI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hallazgo neurológico: encefalopatía/alteración del estado de conciencia post-vacunación. Tipo BIT (boolean).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'ENCEFALO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Encefalopatía', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'ENCEFALO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'ENCEFALO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hallazgo neurológico: parálisis motora post-vacunación. Tipo BIT (boolean).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'PARALISIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Parálisis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'PARALISIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'PARALISIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hallazgo neurológico: parestesia (hormigueo/entumecimiento) post-vacunación. Tipo BIT (boolean).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'PARESTESIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Parestesia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'PARESTESIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'PARESTESIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hallazgo clínico: episodio hipotónico (pérdida de tono muscular) post-vacunación. Tipo BIT (boolean).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'EPIHIPOTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Episodio Hipotónico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'EPIHIPOTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'EPIHIPOTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hallazgo clínico: convulsión sin fiebre post-vacunación. Tipo BIT (boolean).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'CONVUSINF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Convulsión Sin Fiebre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'CONVUSINF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'CONVUSINF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hallazgo clínico: convulsión febril (con fiebre) post-vacunación. Tipo BIT (boolean).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'CONVUFEBR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Convulsión Febril', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'CONVUFEBR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'CONVUFEBR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hallazgo clínico: fiebre elevada mayor a 38.5°C post-vacunación. Tipo BIT (boolean).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'FIEBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fiebre Mayor a 38.5°C', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'FIEBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'FIEBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hallazgo semiológico: linfadenitis (inflamación de ganglios linfáticos) post-vacunación. Tipo BIT (boolean).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'LINFADE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Linfadenitis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'LINFADE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'LINFADE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hallazgo semiológico: absceso (colección purulenta localizada) en sitio de inyección post-vacunación. Tipo BIT (boolean).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'ABSCESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hallazgos Semiológicos   Absceso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'ABSCESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'ABSCESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación epidemiológica final del caso: 1=Relacionado con vacuna, 2=Relacionado con programa, 3=Coincidente, 4=No concluyente/desconocido, 5=Pendiente. Tipo INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'CLASCASO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clasificación Final del Caso  1=Caso Relacionado con la Vacuna  2=Caso Relacionado con el Programa  3=Caso Coincidente  4=Caso no Concluyente o Desconocido  5=Pendiente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'CLASCASO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'CLASCASO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado final del paciente: 1=Recuperación sin secuelas, 2=Recuperación con secuelas. Tipo INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'ESTADFINAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado Final del Paciente  1 = Recuperación sin Secuelas  2 = Recuperación con Secuelas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'ESTADFINAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'ESTADFINAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción libre del usuario: especificación del antecedente previo de reacción a vacunas reportado en ANTEPREV. Tipo VARCHAR(50).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'CUAL3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuáles 3 lo que escribio usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'CUAL3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'CUAL3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedente médico: presencia de reacción previa a otras vacunas (history of adverse vaccine reaction). True=Sí, False=No. Tipo BIT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'ANTEPREV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiene antecedentes Previos de Reacción a Vacunas  True = si   False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'ANTEPREV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'ANTEPREV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción libre del usuario: especificación del antecedente alérgico reportado en ANTEALERG (alergias previas). Tipo VARCHAR(50).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'CUAL2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuáles 2 lo que escribio usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'CUAL2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'CUAL2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedente médico: presencia de alergias conocidas (medicamentosas, ambientales, alimentarias). True=Sí, False=No. Tipo BIT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'ANTEALERG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiene antecedentes Alérgicos  True = Si   False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'ANTEALERG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'ANTEALERG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción libre del usuario: especificación del antecedente patológico reportado en ANTEPAT (comorbilidades). Tipo VARCHAR(50).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'CUAL1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuáles lo que escribio usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'CUAL1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'CUAL1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedente médico: presencia de enfermedades previas o comorbilidades relevantes del paciente. True=Sí, False=No. Tipo BIT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'ANTEPAT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiene antecedentes Patológicos  True = Si      False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'ANTEPAT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'ANTEPAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de tiempo para TIEMPOTRANS: 1=meses, 2=días, 3=horas, 4=minutos. Tipo INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'UNIMEDI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de Medida  1= meses  2=dias  3=horas  4=Minutos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'UNIMEDI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'UNIMEDI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiempo transcurrido entre la administración de la vacuna y la aparición de síntomas adversos. Texto descriptivo (ej: ''''3 días''''). Tipo VARCHAR(50).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'TIEMPOTRANS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiempo Transcurrido entre la Aplicación y los Síntomas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'TIEMPOTRANS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'TIEMPOTRANS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hallazgo semiológico: adenitis (inflamación de ganglios linfáticos) post-BCG, típica en vacunación BCG. Tipo BIT (boolean).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'ADENITIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hallazgos Semiológicos   Adenitis post BCG', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'ADENITIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'ADENITIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de lote del biológico de la 4ª dosis administrada. PII_Lote. Tipo NCHAR(10).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'LOTE4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Lote 4', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'LOTE4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'LOTE4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de administración de la 4ª dosis vacunal (formato dd/mm/aaaa). Tipo DATE.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'FECHA4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'fecha de administracion(dd/mm/aaaa) 4', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'FECHA4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'FECHA4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sitio anatómico de inyección de la 4ª dosis: 1=Hombro der., 2=Hombro izq., 3=Brazo der., 4=Brazo izq., 5=Glúteo der., 6=Glúteo izq., 7=Muslo der., 8=Muslo izq., 9=Oral. Tipo INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'SITIO4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sitio 4  1=HOMBRO DER.2=HOMBRO IZQ.3=BRAZO DER.4=BRAZO IZQ.5=GLÚTEO DER.6=GLÚTEO IZQ.7=MUSLO DER.8=MUSLO IZQ.9=ORAL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'SITIO4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'SITIO4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vía de administración de la 4ª dosis: 1=Oral, 2=Intradérmica, 3=Subcutánea, 4=Intramuscular. Tipo INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'VIA4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Vía  4  1=ORAL  2=INTRADÉRMICA  3=SUBCUTÁNEA  4=INTRAMUSCULAR', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'VIA4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'VIA4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de dosis de la 4ª aplicación: 1=Primera, 2=Segunda, 3=Tercera, 4=Adicional RN, 5=Única, 6=Refuerzo. Tipo INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'DOSIS4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis 4  1=PRIMERA2=SEGUNDA3=TERCERA4=ADICIONAL RN5=ÚNICA6=REFUERZO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'DOSIS4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'DOSIS4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de vacuna de la 4ª dosis: BCG, DPT, Antipolio, HB, HiB, Pentavalente, Triple Viral, F.A., SR, Td/TD, Influenza, Tdap, Antineumococo, Antivaricela, Antirotavírica, Hepatitis A, Anti-VPH, Antimeningococo, Antirrábica, Antipolio Inyectable. Tipo INT (1-21).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'VACU4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Vacuna 4  1=BCG  2=DPT  3=ANTIPOLIO ORAL  4=HB  5=HiB   6=PENTAVALENTE  7=TRIPLE VIRAL   8=F.A.  9=SR-  10=Td/TD  11=INFLUENZA  12=Tdap  13=ANTINEUMOCOCO  14=ANTIVARICELA  15=ANTIROTAVIRICA  16=OTRA  17=HEPATITIS A  18=Anti VPH  19=ANTIMENINGOCOCO  20=ANTIRRABICA  21=ANTIPOLIO INYECTABLE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'VACU4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'VACU4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de lote del biológico de la 3ª dosis administrada. PII_Lote. Tipo VARCHAR(50).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'LOTE3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Lote 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'LOTE3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'LOTE3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de administración de la 3ª dosis vacunal (formato dd/mm/aaaa). Tipo DATE.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'FECHA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'fecha de administracion(dd/mm/aaaa) 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'FECHA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'FECHA3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sitio anatómico de inyección de la 3ª dosis: 1=Hombro der., 2=Hombro izq., 3=Brazo der., 4=Brazo izq., 5=Glúteo der., 6=Glúteo izq., 7=Muslo der., 8=Muslo izq., 9=Oral. Tipo INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'SITIO3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sitio 3  1=HOMBRO DER.2=HOMBRO IZQ.3=BRAZO DER.4=BRAZO IZQ.5=GLÚTEO DER.6=GLÚTEO IZQ.7=MUSLO DER.8=MUSLO IZQ.9=ORAL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'SITIO3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'SITIO3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vía de administración de la 3ª dosis: 1=Oral, 2=Intradérmica, 3=Subcutánea, 4=Intramuscular. Tipo INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'VIA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Vía  3  1=ORAL  2=INTRADÉRMICA  3=SUBCUTÁNEA  4=INTRAMUSCULAR', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'VIA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'VIA3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de dosis de la 3ª aplicación: 1=Primera, 2=Segunda, 3=Tercera, 4=Adicional RN, 5=Única, 6=Refuerzo. Tipo INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'DOSIS3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis 3  1=PRIMERA2=SEGUNDA3=TERCERA4=ADICIONAL RN5=ÚNICA6=REFUERZO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'DOSIS3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'DOSIS3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de vacuna de la 3ª dosis: BCG, DPT, Antipolio, HB, HiB, Pentavalente, Triple Viral, F.A., SR, Td/TD, Influenza, Tdap, Antineumococo, Antivaricela, Antirotavírica, Hepatitis A, Anti-VPH, Antimeningococo, Antirrábica, Antipolio Inyectable. Tipo INT (1-21).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'VACU3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Vacuna 3  1=BCG  2=DPT  3=ANTIPOLIO ORAL  4=HB  5=HiB   6=PENTAVALENTE  7=TRIPLE VIRAL   8=F.A.  9=SR-  10=Td/TD  11=INFLUENZA  12=Tdap  13=ANTINEUMOCOCO  14=ANTIVARICELA  15=ANTIROTAVIRICA  16=OTRA  17=HEPATITIS A  18=Anti VPH  19=ANTIMENINGOCOCO  20=ANTIRRABICA  21=ANTIPOLIO INYECTABLE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'VACU3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'VACU3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de lote del biológico de la 2ª dosis administrada. PII_Lote. Tipo VARCHAR(50).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'LOTE2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Lote 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'LOTE2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'LOTE2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de administración de la 2ª dosis vacunal (formato dd/mm/aaaa). Tipo DATE.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'FECHA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'fecha de administracion(dd/mm/aaaa) 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'FECHA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'FECHA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sitio anatómico de inyección de la 2ª dosis: 1=Hombro der., 2=Hombro izq., 3=Brazo der., 4=Brazo izq., 5=Glúteo der., 6=Glúteo izq., 7=Muslo der., 8=Muslo izq., 9=Oral. Tipo INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'SITIO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sitio 2  1=HOMBRO DER.  2=HOMBRO IZQ.  3=BRAZO DER.  4=BRAZO IZQ.  5=GLÚTEO DER.  6=GLÚTEO IZQ.  7=MUSLO DER.  8=MUSLO IZQ.  9=ORAL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'SITIO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'SITIO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vía de administración de la 2ª dosis: 1=Oral, 2=Intradérmica, 3=Subcutánea, 4=Intramuscular. Tipo INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'VIA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Vía  2  1=ORAL  2=INTRADÉRMICA  3=SUBCUTÁNEA  4=INTRAMUSCULAR', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'VIA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'VIA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de dosis de la 2ª aplicación: 1=Primera, 2=Segunda, 3=Tercera, 4=Adicional RN, 5=Única, 6=Refuerzo. Tipo INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'DOSIS2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis 2  1=PRIMERA  2=SEGUNDA  3=TERCERA  4=ADICIONAL RN  5=ÚNICA  6=REFUERZO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'DOSIS2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'DOSIS2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de vacuna de la 2ª dosis: BCG, DPT, Antipolio, HB, HiB, Pentavalente, Triple Viral, F.A., SR, Td/TD, Influenza, Tdap, Antineumococo, Antivaricela, Antirotavírica, Hepatitis A, Anti-VPH, Antimeningococo, Antirrábica, Antipolio Inyectable. Tipo INT (1-21).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'VACU2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Vacuna 2  1=BCG  2=DPT  3=ANTIPOLIO ORAL  4=HB  5=HiB   6=PENTAVALENTE  7=TRIPLE VIRAL   8=F.A.  9=SR-  10=Td/TD  11=INFLUENZA  12=Tdap  13=ANTINEUMOCOCO  14=ANTIVARICELA  15=ANTIROTAVIRICA  16=OTRA  17=HEPATITIS A  18=Anti VPH  19=ANTIMENINGOCOCO  20=ANTIRRABICA  21=ANTIPOLIO INYECTABLE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'VACU2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'VACU2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de lote del biológico de la 1ª dosis administrada. PII_Lote, rastreable para investigación farmacológica. Tipo VARCHAR(50).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'LOTE1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'lote', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'LOTE1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'LOTE1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de administración de la 1ª dosis vacunal (formato dd/mm/aaaa). Tipo DATE.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'FECHA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'fecha de administracion(dd/mm/aaaa)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'FECHA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'FECHA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sitio anatómico de inyección de la 1ª dosis: 1=Hombro der., 2=Hombro izq., 3=Brazo der., 4=Brazo izq., 5=Glúteo der., 6=Glúteo izq., 7=Muslo der., 8=Muslo izq., 9=Oral. Tipo INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'SITIO1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sitio  1=HOMBRO DER.  2=HOMBRO IZQ.  3=BRAZO DER.  4=BRAZO IZQ.  5=GLÚTEO DER.  6=GLÚTEO IZQ.  7=MUSLO DER.  8=MUSLO IZQ.  9=ORAL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'SITIO1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'SITIO1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vía de administración de la 1ª dosis: 1=Oral, 2=Intradérmica, 3=Subcutánea, 4=Intramuscular. Tipo INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'VIA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Vía   1=ORAL  2=INTRADÉRMICA  3=SUBCUTÁNEA  4=INTRAMUSCULAR', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'VIA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'VIA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de dosis de la 1ª aplicación: 1=Primera, 2=Segunda, 3=Tercera, 4=Adicional RN, 5=Única, 6=Refuerzo. Tipo INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'DOSIS1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis   1=PRIMERA  2=SEGUNDA  3=TERCERA  4=ADICIONAL RN  5=ÚNICA  6=REFUERZO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'DOSIS1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'DOSIS1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de vacuna de la 1ª dosis: BCG, DPT, Antipolio, HB, HiB, Pentavalente, Triple Viral, F.A., SR, Td/TD, Influenza, Tdap, Antineumococo, Antivaricela, Antirotavírica, Hepatitis A, Anti-VPH, Antimeningococo, Antirrábica, Antipolio Inyectable. Tipo INT (1-21).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'VACU1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Vacuna   1=BCG  2=DPT  3=ANTIPOLIO ORAL  4=HB  5=HiB   6=PENTAVALENTE  7=TRIPLE VIRAL   8=F.A.  9=SR-  10=Td/TD  11=INFLUENZA  12=Tdap  13=ANTINEUMOCOCO  14=ANTIVARICELA  15=ANTIROTAVIRICA  16=OTRA  17=HEPATITIS A  18=Anti VPH  19=ANTIMENINGOCOCO  20=ANTIRRABICA  21=ANTIPOLIO INYECTABLE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'VACU1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'VACU1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de diagnóstico asociado al evento adverso post-vacunación. CHAR(4), probable referencia CIE-10. Tipo CHAR(4).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la ficha de notificación padre. FK→HCFICHANOTIFICACION.ID. Tipo INT NOT NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de ficha Notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (autonumérico/identity) del registro de evento adverso vacunal. PK. Tipo INT IDENTITY(1,1) NOT NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ficha clínica 298 para el reporte de Eventos Supuestamente Atribuibles a la Vacunación e Inmunización (ESAVI). Registra hasta 4 vacunas administradas (con dosis, vía, sitio, fecha y lote), antecedentes del paciente, manifestaciones clínicas adversas presentadas tras la vacunación y la clasificación final del caso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298';
