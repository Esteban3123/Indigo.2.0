CREATE TABLE [dbo].[AGACTMEDD] (
    [CODACTMED]                CHAR (3)      NOT NULL,
    [CODSERIPS]                CHAR (20)     NOT NULL,
    [DURACSERVI]               CHAR (6)      NULL,
    [DURAVARIAB]               BIT           NULL,
    [INDICACI]                 VARCHAR (MAX) NULL,
    [MOSTRARWEB]               BIT           NULL,
    [ID]                       INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDDESCRIPCIONRELACIONADA] INT           NULL,
    [EXIGECONFCITA]            BIT           NULL,
    CONSTRAINT [PK_AGACTMEDD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_AGACTMEDD_AGACTIMED] FOREIGN KEY ([CODACTMED]) REFERENCES [dbo].[AGACTIMED] ([CODACTMED]),
    CONSTRAINT [FK_AGACTMEDD_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS])
);




GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_AGACTMEDD]
    ON [dbo].[AGACTMEDD]([CODACTMED] ASC, [CODSERIPS] ASC, [IDDESCRIPCIONRELACIONADA] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que determina si la actividad médica requiere confirmación obligatoria de cita. Aplica exclusivamente a actividades de tipo apoyo diagnóstico (DX) cuando el CUPS está clasificado como 3-Imágenes diagnósticas o 12-Procedimientos invasivos u otros procedimientos. Valores: 0=No requiere, 1=Requiere confirmación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMEDD', @level2type = N'COLUMN', @level2name = N'EXIGECONFCITA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Determina si exige confirmacion de cita (aplica solo para tipo actividad de apoyo DX) y  si el CUPS está parametrizado en el campo "Listar este servicio en el dashboard de" del formulario de CUPS como "3 -Imágenes diagnosticas" o "12 - Procedimientos invasivos u Otros Procedimientos"', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMEDD', @level2type = N'COLUMN', @level2name = N'EXIGECONFCITA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMEDD', @level2type = N'COLUMN', @level2name = N'EXIGECONFCITA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador entero de la descripción relacionada en VIE ERP (tabla contract.CUPSEntityContractDescriptions). Clave foránea que vincula la actividad médica con descripciones contractuales del servicio en el sistema de contratos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMEDD', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de descripcion de relacion. relacion con  VIE ERP  (contract.CUPSEntityContractDescriptions) ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMEDD', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMEDD', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental (INT IDENTITY) que funciona como clave primaria de la tabla AGACTMEDD. Generado automáticamente por SQL Server para cada registro de actividad médica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMEDD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMEDD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMEDD', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que controla la visibilidad de la actividad médica en canales web y digitales. Valores: 0=No mostrar, 1=Mostrar. Regula disponibilidad en portales pacientes y sistemas de reserva online.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMEDD', @level2type = N'COLUMN', @level2name = N'MOSTRARWEB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mostrar en Web: 0 = No, 1 = Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMEDD', @level2type = N'COLUMN', @level2name = N'MOSTRARWEB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMEDD', @level2type = N'COLUMN', @level2name = N'MOSTRARWEB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo de texto enriquecido (VARCHAR MAX) que contiene las indicaciones médicas, contraindicaciones y protocolos clínicos para actividades de apoyo diagnóstico y tratamientos especiales. Información crítica para guiar al profesional de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMEDD', @level2type = N'COLUMN', @level2name = N'INDICACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicaciones medicas para actividades de tipo apoyo diagnostico y tratamientos especailes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMEDD', @level2type = N'COLUMN', @level2name = N'INDICACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMEDD', @level2type = N'COLUMN', @level2name = N'INDICACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que especifica si la duración del servicio es variable o fija. Valores: 0=Duración fija, 1=Duración variable. Relevante para programación de citas y asignación de recursos en unidades funcionales.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMEDD', @level2type = N'COLUMN', @level2name = N'DURAVARIAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'define si la duracion es variable o no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMEDD', @level2type = N'COLUMN', @level2name = N'DURAVARIAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMEDD', @level2type = N'COLUMN', @level2name = N'DURAVARIAB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Duración estimada del servicio expresada en formato CHAR(6), típicamente en minutos u horas. Utilizado para cálculo de tiempos de atención, bloques quirúrgicos, salas de procedimiento y disponibilidad de agendamiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMEDD', @level2type = N'COLUMN', @level2name = N'DURACSERVI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'duracion del servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMEDD', @level2type = N'COLUMN', @level2name = N'DURACSERVI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMEDD', @level2type = N'COLUMN', @level2name = N'DURACSERVI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del servicio IPS (Institución Prestadora de Servicios) en formato CHAR(20). Identificador del procedimiento, examen o actividad según nomenclatura interna de la institución. Vinculado a CUPS y RIPS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMEDD', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo servicio ips', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMEDD', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMEDD', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código consecutivo de 3 dígitos (CHAR 3) que identifica unívocamente la actividad médica. Referencia a tabla AGACTIMED. Clasifica tipos de actividades: diagnóstico, procedimiento, tratamiento, seguimiento u otra intervención clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMEDD', @level2type = N'COLUMN', @level2name = N'CODACTMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo consecutivo que identifica la actividad medica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMEDD', @level2type = N'COLUMN', @level2name = N'CODACTMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMEDD', @level2type = N'COLUMN', @level2name = N'CODACTMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de actividades o procedimientos médicos habilitados para agendamiento, con su duración, indicaciones clínicas y configuración de visibilidad en el portal web.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMEDD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMEDD';
