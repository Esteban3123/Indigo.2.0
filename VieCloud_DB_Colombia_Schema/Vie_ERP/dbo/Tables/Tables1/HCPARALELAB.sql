CREATE TABLE [dbo].[HCPARALELAB] (
    [CODCENATE]           CHAR (10) NOT NULL,
    [CODSERIPS]           CHAR (20) NOT NULL,
    [LABRECMUE]           TINYINT   NULL,
    [UNITIEREC]           TINYINT   NULL,
    [LABRECMUE2]          TINYINT   NULL,
    [UNITIEREC2]          TINYINT   NULL,
    [LABENTRES]           TINYINT   NULL,
    [UNITIEENT]           TINYINT   NULL,
    [LABENTRES2]          TINYINT   NULL,
    [UNITIEENT2]          TINYINT   NULL,
    [ID]                  INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [USUARIOCREACION]     CHAR (20) NULL,
    [FECHACREACION]       DATETIME  NULL,
    [USUARIOMODIFICACION] CHAR (20) NULL,
    [FECHAMODIFICACION]   DATETIME  NULL,
    CONSTRAINT [PK_HCPARALELAB] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCPARALELAB_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCPARALELAB_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_HCPARALELAB_SEGusuaru_1] FOREIGN KEY ([USUARIOCREACION]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI]),
    CONSTRAINT [FK_HCPARALELAB_SEGusuaru_2] FOREIGN KEY ([USUARIOMODIFICACION]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI]),
    CONSTRAINT [IX_HCPARALELAB] UNIQUE NONCLUSTERED ([CODCENATE] ASC, [CODSERIPS] ASC)
);




GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación del registro de paralelismo laboratorio; permite auditoría temporal (DATETIME, NOT NULL si se modificó).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALELAB', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALELAB', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALELAB', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación del registro; referencia a tabla SEGusuaru para trazabilidad de cambios en configuración de laboratorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALELAB', @level2type = N'COLUMN', @level2name = N'USUARIOMODIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALELAB', @level2type = N'COLUMN', @level2name = N'USUARIOMODIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALELAB', @level2type = N'COLUMN', @level2name = N'USUARIOMODIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación inicial del registro de paralelismo laboratorio; marca origen del vínculo centro-servicio (DATETIME).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALELAB', @level2type = N'COLUMN', @level2name = N'FECHACREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Creación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALELAB', @level2type = N'COLUMN', @level2name = N'FECHACREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALELAB', @level2type = N'COLUMN', @level2name = N'FECHACREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro de paralelismo laboratorio; referencia a tabla SEGusuaru para auditoría de origen (SEGusuaru.CODUSUARI).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALELAB', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALELAB', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALELAB', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) y clave primaria de la tabla HCPARALELAB; autonumérico secuencial para cada configuración de paralelismo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALELAB', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID o Autonumérico de la Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALELAB', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALELAB', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de tiempo máximo de entrega de resultados segunda instancia laboratorio; codificado: 1=Minutos, 2=Horas, 3=Días (TINYINT, SLA paralelo).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALELAB', @level2type = N'COLUMN', @level2name = N'UNITIEENT2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Laboratorios - Unidad de Tiempo Maximo de Entrega de Resultados 2 Instancia (1-Minutos,2-Horas,3-Dias)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALELAB', @level2type = N'COLUMN', @level2name = N'UNITIEENT2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALELAB', @level2type = N'COLUMN', @level2name = N'UNITIEENT2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico de tiempo máximo de entrega de resultados segunda instancia laboratorio; se interpreta con UNITIEENT2 para SLA de turnaround time paralelo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALELAB', @level2type = N'COLUMN', @level2name = N'LABENTRES2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Laboratorios - Tiempo Maximo de Entrega de Resultados 2 Instancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALELAB', @level2type = N'COLUMN', @level2name = N'LABENTRES2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALELAB', @level2type = N'COLUMN', @level2name = N'LABENTRES2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de tiempo máximo de entrega de resultados primera instancia laboratorio; codificado: 1=Minutos, 2=Horas, 3=Días (TINYINT, SLA principal).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALELAB', @level2type = N'COLUMN', @level2name = N'UNITIEENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Laboratorios - Unidad de Tiempo Maximo de Entrega de Resultados (1-Minutos,2-Horas,3-Dias)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALELAB', @level2type = N'COLUMN', @level2name = N'UNITIEENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALELAB', @level2type = N'COLUMN', @level2name = N'UNITIEENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico de tiempo máximo de entrega de resultados primera instancia laboratorio; define SLA turnaround time para exámenes, pruebas diagnósticas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALELAB', @level2type = N'COLUMN', @level2name = N'LABENTRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Laboratorios - Tiempo Maximo de Entrega de Resultados', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALELAB', @level2type = N'COLUMN', @level2name = N'LABENTRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALELAB', @level2type = N'COLUMN', @level2name = N'LABENTRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de tiempo máximo de recolección/toma de muestra segunda instancia laboratorio; codificado: 1=Minutos, 2=Horas, 3=Días (TINYINT, paralelo).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALELAB', @level2type = N'COLUMN', @level2name = N'UNITIEREC2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Laboratorios - Unidad de Tiempo Maximo de Recoleccion de la Muestra 2 Instancia (1-Minutos,2-Horas,3-Dias)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALELAB', @level2type = N'COLUMN', @level2name = N'UNITIEREC2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALELAB', @level2type = N'COLUMN', @level2name = N'UNITIEREC2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico de tiempo máximo de recolección/toma de muestra segunda instancia laboratorio; define ventana recepción muestras en paralelismo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALELAB', @level2type = N'COLUMN', @level2name = N'LABRECMUE2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Laboratorios - Tiempo Maximo de Recoleccion de la Muestra 2 Instancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALELAB', @level2type = N'COLUMN', @level2name = N'LABRECMUE2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALELAB', @level2type = N'COLUMN', @level2name = N'LABRECMUE2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de tiempo máximo de recolección/toma de muestra primera instancia laboratorio; codificado: 1=Minutos, 2=Horas, 3=Días (TINYINT, principal).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALELAB', @level2type = N'COLUMN', @level2name = N'UNITIEREC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Laboratorios - Unidad de Tiempo Maximo de Recoleccion de la Muestra(1-Minutos,2-Horas,3-Dias)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALELAB', @level2type = N'COLUMN', @level2name = N'UNITIEREC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALELAB', @level2type = N'COLUMN', @level2name = N'UNITIEREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico de tiempo máximo de recolección/toma de muestra primera instancia laboratorio; SLA para captura de especímenes biológicas, exámenes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALELAB', @level2type = N'COLUMN', @level2name = N'LABRECMUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Laboratorios - Tiempo Maximo de Recoleccion de la Muestra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALELAB', @level2type = N'COLUMN', @level2name = N'LABRECMUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALELAB', @level2type = N'COLUMN', @level2name = N'LABRECMUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único CUPS/RIPS del procedimiento o servicio de laboratorio; FK a INCUPSIPS para identificar examen, prueba diagnóstica, análisis específico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALELAB', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Unico de Procedimientos y Servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALELAB', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALELAB', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención (unidad funcional, IPS); FK a ADCENATEN; identifica en qué punto asistencial aplican estos SLA de paralelismo laboratorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALELAB', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del centro de atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALELAB', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALELAB', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parámetros de laboratorio clínico por servicio y centro de atención: tiempos de recepción de muestra y entrega de resultados (con sus unidades de tiempo) asociados a cada examen o procedimiento de laboratorio (CUPS/IPS).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALELAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALELAB';
