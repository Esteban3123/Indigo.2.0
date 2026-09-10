CREATE TABLE [dbo].[HCLOGSUMC] (
    [ID]        INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [HCFARMEPC] NUMERIC (18)   NOT NULL,
    [FECGENLOG] DATETIME       NOT NULL,
    [USUARIO]   VARCHAR (20)   NOT NULL,
    [MENSAJE]   VARCHAR (2000) NULL,
    [PROESTADO] BIT            NULL,
    [CODCENATE] CHAR (10)      NOT NULL,
    [UFUCODIGO] CHAR (10)      NOT NULL,
    CONSTRAINT [PK_HCLOGSUMC] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCLOGSUMC_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCLOGSUMC_HCFARMEPC] FOREIGN KEY ([HCFARMEPC]) REFERENCES [dbo].[HCFARMEPC] ([CODCONCEC]),
    CONSTRAINT [FK_HCLOGSUMC_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);




GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la Unidad Funcional (CHAR 10), clave foránea a INUNIFUNC, referencia el área o departamento donde se ejecutó el proceso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLOGSUMC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLOGSUMC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLOGSUMC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del Centro de Atención (CHAR 10), clave foránea a ADCENATEN, identifica la institución/sede donde se registra la acción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLOGSUMC', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLOGSUMC', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLOGSUMC', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del proceso (BIT): 1=Exitoso/Completado, 0=Fallido/Error, indica si la operación finalizó correctamente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLOGSUMC', @level2type = N'COLUMN', @level2name = N'PROESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del proceso    1: Exitoso  0: Fallido  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLOGSUMC', @level2type = N'COLUMN', @level2name = N'PROESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLOGSUMC', @level2type = N'COLUMN', @level2name = N'PROESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mensaje informativo (VARCHAR 2000) que detalla el resultado, error o descripción del proceso ejecutado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLOGSUMC', @level2type = N'COLUMN', @level2name = N'MENSAJE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mensaje Informativo Que produce el proceso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLOGSUMC', @level2type = N'COLUMN', @level2name = N'MENSAJE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLOGSUMC', @level2type = N'COLUMN', @level2name = N'MENSAJE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR 20) que realiza la acción o ejecuta el proceso registrado en el log', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLOGSUMC', @level2type = N'COLUMN', @level2name = N'USUARIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que realiza la acción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLOGSUMC', @level2type = N'COLUMN', @level2name = N'USUARIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLOGSUMC', @level2type = N'COLUMN', @level2name = N'USUARIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) en que se genera el registro de log, timestamp de la acción realizada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLOGSUMC', @level2type = N'COLUMN', @level2name = N'FECGENLOG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en que se genera el log', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLOGSUMC', @level2type = N'COLUMN', @level2name = N'FECGENLOG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLOGSUMC', @level2type = N'COLUMN', @level2name = N'FECGENLOG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de consecutivo de la farmacia/prescripción (HCFARMEPC), clave foránea que referencia el documento farmacéutico procesado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLOGSUMC', @level2type = N'COLUMN', @level2name = N'HCFARMEPC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Consecutivo de HCFARMEPC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLOGSUMC', @level2type = N'COLUMN', @level2name = N'HCFARMEPC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLOGSUMC', @level2type = N'COLUMN', @level2name = N'HCFARMEPC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementado (INT IDENTITY) del registro de log en HCLOGSUMC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLOGSUMC', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLOGSUMC', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLOGSUMC', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de auditoría y log de eventos del módulo de farmacia (epicrisis/dispensación), donde se almacenan los mensajes generados por cada acción o inconsistencia detectada, junto con el usuario responsable, la fecha del evento y el centro de atención y unidad funcional involucrados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLOGSUMC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLOGSUMC';
