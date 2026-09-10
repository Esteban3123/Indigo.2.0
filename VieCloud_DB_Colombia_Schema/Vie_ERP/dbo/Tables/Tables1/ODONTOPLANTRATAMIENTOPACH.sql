CREATE TABLE [dbo].[ODONTOPLANTRATAMIENTOPACH] (
    [ID]                         INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDODONTOCONTROL]            INT       NOT NULL,
    [IDODONTOPLANTRATAMIENTOPAC] INT       NOT NULL,
    [IDODOPARTRA]                INT       NOT NULL,
    [FECHAORDENO]                DATETIME  NOT NULL,
    [PROFESIONALORDENO]          CHAR (20) NULL,
    [ESTADO]                     INT       NULL,
    [PROFESIONALREALIZO]         CHAR (20) NULL,
    [FECHAREALIZO]               DATETIME  NULL,
    [PROFESIONALCANCELO]         CHAR (20) NULL,
    [FECHACANCELO]               DATETIME  NULL,
    CONSTRAINT [PK_ODONTOPLANTRATAMIENTOPACH] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ODONTOPLANTRATAMIENTOPACH_INPROFSAL] FOREIGN KEY ([PROFESIONALCANCELO]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_ODONTOPLANTRATAMIENTOPACH_INPROFSAL1] FOREIGN KEY ([PROFESIONALORDENO]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_ODONTOPLANTRATAMIENTOPACH_INPROFSAL2] FOREIGN KEY ([PROFESIONALREALIZO]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_ODONTOPLANTRATAMIENTOPACH_ODONTOCONTROL] FOREIGN KEY ([IDODONTOCONTROL]) REFERENCES [dbo].[ODONTOCONTROL] ([ID]),
    CONSTRAINT [FK_ODONTOPLANTRATAMIENTOPACH_ODONTOPLANTRATAMIENTOPAC] FOREIGN KEY ([IDODONTOPLANTRATAMIENTOPAC]) REFERENCES [dbo].[ODONTOPLANTRATAMIENTOPAC] ([ID]),
    CONSTRAINT [FK_ODONTOPLANTRATAMIENTOPACH_ODONTOPLANTRATAMIENTOPACH] FOREIGN KEY ([ID]) REFERENCES [dbo].[ODONTOPLANTRATAMIENTOPACH] ([ID]),
    CONSTRAINT [FK_ODONTOPLANTRATAMIENTOPACH_ODOPARTRA] FOREIGN KEY ([IDODOPARTRA]) REFERENCES [dbo].[ODOPARTRA] ([CONSECTRA])
);




GO



GO



GO



GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) en que se canceló la actividad odontológica planificada; PII temporal. Permite rastrear cuándo se descartó el tratamiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACH', @level2type = N'COLUMN', @level2name = N'FECHACANCELO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la fecha de la cancelación ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACH', @level2type = N'COLUMN', @level2name = N'FECHACANCELO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACH', @level2type = N'COLUMN', @level2name = N'FECHACANCELO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud (odontólogo/especialista) que canceló el tratamiento; FK a INPROFSAL.CODPROSAL. Identificación PII del profesional responsable de la cancelación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACH', @level2type = N'COLUMN', @level2name = N'PROFESIONALCANCELO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el profesional quien cancelo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACH', @level2type = N'COLUMN', @level2name = N'PROFESIONALCANCELO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACH', @level2type = N'COLUMN', @level2name = N'PROFESIONALCANCELO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de ejecución/realización del procedimiento odontológico planificado. Diferencia entre orden y ejecución real.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACH', @level2type = N'COLUMN', @level2name = N'FECHAREALIZO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la fecha que se realizo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACH', @level2type = N'COLUMN', @level2name = N'FECHAREALIZO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACH', @level2type = N'COLUMN', @level2name = N'FECHAREALIZO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud (odontólogo/cirujano dental) que ejecutó/realizó el procedimiento; FK a INPROFSAL.CODPROSAL. Profesional PII responsable de la atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACH', @level2type = N'COLUMN', @level2name = N'PROFESIONALREALIZO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el profesional quien realizo ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACH', @level2type = N'COLUMN', @level2name = N'PROFESIONALREALIZO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACH', @level2type = N'COLUMN', @level2name = N'PROFESIONALREALIZO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado numérico del tratamiento odontológico: 1=Activo/Vigente; 0=Inactivo/Suspendido. Bandera booleana que indica disponibilidad de la actividad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACH', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el estado  1 =  true = activo        0 = false = inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACH', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACH', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud (odontólogo) que ordenó/prescribió el tratamiento; FK a INPROFSAL.CODPROSAL. Identificación PII del profesional que emitió la orden.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACH', @level2type = N'COLUMN', @level2name = N'PROFESIONALORDENO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el profesional que ordeno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACH', @level2type = N'COLUMN', @level2name = N'PROFESIONALORDENO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACH', @level2type = N'COLUMN', @level2name = N'PROFESIONALORDENO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) en que se ordenó/prescribió la actividad odontológica. Marca temporal de inicio del ciclo de vida del tratamiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACH', @level2type = N'COLUMN', @level2name = N'FECHAORDENO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la fecha orden', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACH', @level2type = N'COLUMN', @level2name = N'FECHAORDENO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACH', @level2type = N'COLUMN', @level2name = N'FECHAORDENO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la tabla ODOPARTRA (CONSECTRA); FK que vincula con tipo/parámetro específico de procedimiento/actividad odontológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACH', @level2type = N'COLUMN', @level2name = N'IDODOPARTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relación de la tabla id odontograma tratamientos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACH', @level2type = N'COLUMN', @level2name = N'IDODOPARTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACH', @level2type = N'COLUMN', @level2name = N'IDODOPARTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (ID) del plan de tratamiento odontológico del paciente; FK a ODONTOPLANTRATAMIENTOPAC. Nivel jerárquico superior del plan.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACH', @level2type = N'COLUMN', @level2name = N'IDODONTOPLANTRATAMIENTOPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con la tabla ODONTOPLANTRATAMIENTOPAC que es el id del plan de tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACH', @level2type = N'COLUMN', @level2name = N'IDODONTOPLANTRATAMIENTOPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACH', @level2type = N'COLUMN', @level2name = N'IDODONTOPLANTRATAMIENTOPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (ID) del control/seguimiento odontográfico del paciente; FK a ODONTOCONTROL. Vinculación con evaluación periódica del odontograma.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACH', @level2type = N'COLUMN', @level2name = N'IDODONTOCONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de relacion con la tabla de control de odontograma ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACH', @level2type = N'COLUMN', @level2name = N'IDODONTOCONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACH', @level2type = N'COLUMN', @level2name = N'IDODONTOCONTROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del registro de actividad de tratamiento odontológico planificado para el paciente. Clave primaria secuencial de la tabla.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACH', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACH', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACH', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Historial de ejecución de los procedimientos incluidos en los planes de tratamiento odontológico por paciente: registra quién ordenó cada procedimiento, quién lo realizó o canceló, y las fechas correspondientes a cada evento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACH';
