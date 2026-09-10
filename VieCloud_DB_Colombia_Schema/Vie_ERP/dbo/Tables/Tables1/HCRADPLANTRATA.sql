CREATE TABLE [dbo].[HCRADPLANTRATA] (
    [ID]             INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCRADINFOC]   INT            NOT NULL,
    [NUMINGRES]      CHAR (10)      NOT NULL,
    [FECHORDEN]      DATETIME       NOT NULL,
    [NUMREGION]      VARCHAR (3)    NOT NULL,
    [NOMREGION]      VARCHAR (100)  NOT NULL,
    [DOSTOTAL]       INT            NOT NULL,
    [DOSISPORFRACC]  INT            NOT NULL,
    [NUMEFRACC]      INT            NOT NULL,
    [FECCALCULO]     DATETIME       NOT NULL,
    [PORCDOSTOTAUTO] INT            NULL,
    [OBSERVACION]    VARCHAR (2000) NULL,
    [ESTADO]         INT            NULL,
    [FIRMFISICO]     CHAR (20)      NULL,
    [FIRMRADION]     CHAR (20)      NULL,
    [FECFINALIZA]    DATETIME       NULL,
    [DESFINALIZA]    VARCHAR (MAX)  NULL,
    [IDHCRADORD]     INT            NULL,
    [ESTADOCITA]     INT            NULL,
    CONSTRAINT [PK_HCRADPLANTRATA] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCRADPLANTRATA_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCRADPLANTRATA_HCRADINFOC] FOREIGN KEY ([IDHCRADINFOC]) REFERENCES [dbo].[HCRADINFOC] ([ID]),
    CONSTRAINT [FK_HCRADPLANTRATA_HCRADPLANTRATA] FOREIGN KEY ([ID]) REFERENCES [dbo].[HCRADPLANTRATA] ([ID]),
    CONSTRAINT [FK_HCRADPLANTRATA_INPROFSAL] FOREIGN KEY ([FIRMFISICO]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCRADPLANTRATA_INPROFSAL1] FOREIGN KEY ([FIRMRADION]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL])
);




GO



GO





GO



GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la cita de radioterapia (programada, realizada, cancelada, reprogramada); referencia al ciclo de atención del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'ESTADOCITA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la cita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'ESTADOCITA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'ESTADOCITA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la orden de radioterapia (FK a HCRADORD); vincula plan de tratamiento con orden médica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'IDHCRADORD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el id de la orden', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'IDHCRADORD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'IDHCRADORD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción narrativa de la finalización del plan de tratamiento; motivo de cierre (completado, abandonado, transferido, fallecimiento)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'DESFINALIZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la descripción de la finalización', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'DESFINALIZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'DESFINALIZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de finalización del plan de tratamiento de radioterapia; timestamp de cierre del ciclo terapéutico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'FECFINALIZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de finalización', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'FECFINALIZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'FECFINALIZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del radiólogo que firma y autoriza el plan de tratamiento (FK a INPROFSAL); PII profesional de salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'FIRMRADION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la firma nueva del radiologo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'FIRMRADION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'FIRMRADION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del físico médico/radiofísico que valida y firma el plan de tratamiento (FK a INPROFSAL); PII profesional de salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'FIRMFISICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Firma fisica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'FIRMFISICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'FIRMFISICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado actual del registro del plan (activo, inactivo, suspendido, finalizado); INT código de estado del tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Notas clínicas y observaciones sobre el plan de tratamiento, tolerancia del paciente, recomendaciones; VARCHAR(2000)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'OBSERVACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'OBSERVACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'OBSERVACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de dosis total autorizado respecto al cálculo original; INT %, para control de variaciones dodosimétricas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'PORCDOSTOTAUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Iso dosis autorizado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'PORCDOSTOTAUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'PORCDOSTOTAUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de cálculo de la dosis total y fraccionamiento; timestamp de planificación dosimétrica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'FECCALCULO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de calculo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'FECCALCULO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'FECCALCULO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de fracciones de radioterapia prescrito en el plan; INT cantidad de sesiones de tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'NUMEFRACC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de fracciones ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'NUMEFRACC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'NUMEFRACC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis de radiación por fracción (sesión) en Gy (Gray); INT valor dosimétrico unitario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'DOSISPORFRACC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis por fracción ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'DOSISPORFRACC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'DOSISPORFRACC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis total acumulada prescrita en todo el plan de tratamiento; INT valor en Gy, suma de todas las fracciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'DOSTOTAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis total', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'DOSTOTAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'DOSTOTAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo de la región/sitio anatómico tratado (cabeza, tórax, abdomen, pelvis, miembros); VARCHAR(100)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'NOMREGION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la region ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'NOMREGION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'NOMREGION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código numérico de la región/localización anatómica irradiada; VARCHAR(3), clasificación interna del centro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'NUMREGION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de la region ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'NUMREGION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'NUMREGION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de generación de la orden de radioterapia; DATETIME inicio del ciclo prescriptivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'FECHORDEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la orden ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'FECHORDEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'FECHORDEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso/atención del paciente (FK a ADINGRESO); vincula plan al episodio de atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cabecera/maestro del expediente radioncológico (FK a HCRADINFOC); relación padre del plan', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'IDHCRADINFOC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con la cabecera', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'IDHCRADINFOC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'IDHCRADINFOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (INT IDENTITY) del plan de tratamiento de radioterapia; PK de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Planes de tratamiento de radioterapia asociados a pacientes oncológicos. Registra las fracciones de dosis, regiones anatómicas irradiadas, fechas de cálculo y finalización, firmas del físico y radiooncólogo, y el estado de cada plan dentro del proceso de radioterapia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA';
