CREATE TABLE [dbo].[ODONTOPLANTRATAMIENTOPACD] (
    [ID]                         INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDODONTOCONTROL]            INT       NOT NULL,
    [IDODONTOPLANTRATAMIENTOPAC] INT       NOT NULL,
    [IDODOPARTRA]                INT       NOT NULL,
    [ESTADO]                     INT       NOT NULL,
    [FECHAORDENO]                DATETIME  NULL,
    [PROFESIONALORDENO]          CHAR (20) NULL,
    [FECHAREALIZO]               DATETIME  NULL,
    [PROFESIONALREALIZO]         CHAR (20) NULL,
    [FECHACANCELO]               DATETIME  NULL,
    [PROFESIONALCANCELO]         CHAR (20) NULL,
    CONSTRAINT [PK_ODONTOPLANTRATAMIENTOD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ODONTOPLANTRATAMIENTOPACD_INPROFSAL] FOREIGN KEY ([PROFESIONALORDENO]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_ODONTOPLANTRATAMIENTOPACD_INPROFSAL1] FOREIGN KEY ([PROFESIONALREALIZO]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_ODONTOPLANTRATAMIENTOPACD_INPROFSAL2] FOREIGN KEY ([PROFESIONALCANCELO]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_ODONTOPLANTRATAMIENTOPACD_ODONTOCONTROL] FOREIGN KEY ([IDODONTOCONTROL]) REFERENCES [dbo].[ODONTOCONTROL] ([ID]),
    CONSTRAINT [FK_ODONTOPLANTRATAMIENTOPACD_ODONTOPLANTRATAMIENTOPAC] FOREIGN KEY ([IDODONTOPLANTRATAMIENTOPAC]) REFERENCES [dbo].[ODONTOPLANTRATAMIENTOPAC] ([ID]),
    CONSTRAINT [FK_ODONTOPLANTRATAMIENTOPACD_ODOPARTRA] FOREIGN KEY ([IDODOPARTRA]) REFERENCES [dbo].[ODOPARTRA] ([CONSECTRA])
);




GO



GO



GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud (odontólogo/dentista) que canceló el tratamiento odontológico. FK a INPROFSAL. PII: Identification_Ofuscado. Null si no fue cancelado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACD', @level2type = N'COLUMN', @level2name = N'PROFESIONALCANCELO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el profesional quien cancelo ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACD', @level2type = N'COLUMN', @level2name = N'PROFESIONALCANCELO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACD', @level2type = N'COLUMN', @level2name = N'PROFESIONALCANCELO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se canceló el tratamiento odontológico del plan. DATETIME. Null si el tratamiento no fue cancelado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACD', @level2type = N'COLUMN', @level2name = N'FECHACANCELO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda le fecha de la cancelación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACD', @level2type = N'COLUMN', @level2name = N'FECHACANCELO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACD', @level2type = N'COLUMN', @level2name = N'FECHACANCELO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud (odontólogo/dentista) que ejecutó/realizó el tratamiento odontológico al paciente. FK a INPROFSAL. PII: Identification_Ofuscado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACD', @level2type = N'COLUMN', @level2name = N'PROFESIONALREALIZO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'profesional que realizó el tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACD', @level2type = N'COLUMN', @level2name = N'PROFESIONALREALIZO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACD', @level2type = N'COLUMN', @level2name = N'PROFESIONALREALIZO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de ejecución/realización del tratamiento odontológico. DATETIME. Null si aún no se ha realizado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACD', @level2type = N'COLUMN', @level2name = N'FECHAREALIZO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecdha de realización', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACD', @level2type = N'COLUMN', @level2name = N'FECHAREALIZO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACD', @level2type = N'COLUMN', @level2name = N'FECHAREALIZO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional que ordenó/agregó el tratamiento al plan odontológico después de su creación inicial. FK a INPROFSAL. PII: Identification_Ofuscado. Null si fue agregado en la creación del plan.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACD', @level2type = N'COLUMN', @level2name = N'PROFESIONALORDENO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Profesional ordeno esto se llena si los registros son agregados despues de que ya existen tratamientos al Plan.    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACD', @level2type = N'COLUMN', @level2name = N'PROFESIONALORDENO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACD', @level2type = N'COLUMN', @level2name = N'PROFESIONALORDENO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que el profesional ordenó/agregó el tratamiento al plan odontológico existente. DATETIME. Null si fue incluido en la creación inicial del plan.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACD', @level2type = N'COLUMN', @level2name = N'FECHAORDENO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Ordeno el medico, esto se llena si los registros son agregados despues de que ya existen tratamientos al Plan.    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACD', @level2type = N'COLUMN', @level2name = N'FECHAORDENO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACD', @level2type = N'COLUMN', @level2name = N'FECHAORDENO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del tratamiento odontológico en el plan: 1=Sin realizar (ejecución en curso), 2=Realizado (completado), 3=No realizado (automático al incumplir plan), 4=Cancelado (profesional desestimó). INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACD', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Sin realizar (Cuando el paln de tratamiento se encuentra en ejecución y el tratamiento no se ha realizado aún)  2 - Realizado (Cuando se indica que el tratamiendo ya fue realizado)  3 - No realizado (Estado automatico, que se establece cuando un tratamiento no se ha realizado en el momento de indicar que el plan fue Incumplido, los tratamientos en estado sin realizar pasan a No realizados)  4 - Cancelado (Cuando el profesional edita el Plan e indica que el tratamiento no se va a realizar)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACD', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACD', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK) del tratamiento odontológico específico enviado al paciente. FK a ODOPARTRA.CONSECTRA. Referencia a procedimiento/acto odontológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACD', @level2type = N'COLUMN', @level2name = N'IDODOPARTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relación con la tabla ODOPARTRA que es el ID del tratamiento que se le envio al paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACD', @level2type = N'COLUMN', @level2name = N'IDODOPARTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACD', @level2type = N'COLUMN', @level2name = N'IDODOPARTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del plan de tratamiento odontológico del paciente. FK a ODONTOPLANTRATAMIENTOPAC.ID. Agrupa múltiples tratamientos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACD', @level2type = N'COLUMN', @level2name = N'IDODONTOPLANTRATAMIENTOPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con la tabla ODONTOPLANTRATAMIENTOPAC que es el id del plan de tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACD', @level2type = N'COLUMN', @level2name = N'IDODONTOPLANTRATAMIENTOPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACD', @level2type = N'COLUMN', @level2name = N'IDODONTOPLANTRATAMIENTOPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de control odontograma/diagnóstico dental. FK a ODONTOCONTROL.ID. Referencia al control clínico asociado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACD', @level2type = N'COLUMN', @level2name = N'IDODONTOCONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relación con la tabla de control del Odontograma', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACD', @level2type = N'COLUMN', @level2name = N'IDODONTOCONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACD', @level2type = N'COLUMN', @level2name = N'IDODONTOCONTROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo/identificador único (PK IDENTITY) del registro de ejecución de tratamiento en el plan. INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACD', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de los procedimientos o actividades incluidos en el plan de tratamiento odontológico de un paciente, registrando el estado de ejecución de cada ítem del plan y los profesionales que ordenaron, realizaron o cancelaron cada procedimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACD';
