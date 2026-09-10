CREATE TABLE [dbo].[HCPARALEIMA] (
    [CODCENATE]           CHAR (10) NOT NULL,
    [CODSERIPS]           CHAR (20) NOT NULL,
    [IMAREAEXA]           TINYINT   NULL,
    [UNITIEREA]           TINYINT   NULL,
    [IMAREAEXA2]          TINYINT   NULL,
    [UNITIEREA2]          TINYINT   NULL,
    [IMAENTRES]           TINYINT   NULL,
    [UNITIERES]           TINYINT   NULL,
    [IMAENTRES2]          TINYINT   NULL,
    [UNITIERES2]          TINYINT   NULL,
    [ID]                  INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [USUARIOCREACION]     CHAR (20) NULL,
    [FECHACREACION]       DATETIME  NULL,
    [USUARIOMODIFICACION] CHAR (20) NULL,
    [FECHAMODIFICACION]   DATETIME  NULL,
    CONSTRAINT [PK_HCPARALEIMA] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCPARALEIMA_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCPARALEIMA_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_HCPARALEIMA_SEGusuaru_1] FOREIGN KEY ([USUARIOCREACION]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI]),
    CONSTRAINT [FK_HCPARALEIMA_SEGusuaru_2] FOREIGN KEY ([USUARIOMODIFICACION]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI]),
    CONSTRAINT [IX_HCPARALEIMA] UNIQUE NONCLUSTERED ([CODCENATE] ASC, [CODSERIPS] ASC)
);




GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación del registro de parámetros de imagenología (DATETIME, auditoría)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEIMA', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEIMA', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEIMA', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación del registro (FK SEGusuaru, auditoría, PII)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEIMA', @level2type = N'COLUMN', @level2name = N'USUARIOMODIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEIMA', @level2type = N'COLUMN', @level2name = N'USUARIOMODIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEIMA', @level2type = N'COLUMN', @level2name = N'USUARIOMODIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de parámetros de imagenología (DATETIME, auditoría)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEIMA', @level2type = N'COLUMN', @level2name = N'FECHACREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Creación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEIMA', @level2type = N'COLUMN', @level2name = N'FECHACREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEIMA', @level2type = N'COLUMN', @level2name = N'FECHACREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro (FK SEGusuaru, auditoría, PII)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEIMA', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEIMA', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEIMA', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental del registro de parámetros de imagenología (INT IDENTITY, clave primaria)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEIMA', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID o Autonumérico de la Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEIMA', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEIMA', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de tiempo máximo de entrega de resultado segunda instancia imagenología (TINYINT: 1=Minutos, 2=Horas, 3=Días)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEIMA', @level2type = N'COLUMN', @level2name = N'UNITIERES2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Imagenologia - Unidad de Tiempo Maximo de Entrega de Resultado 2 Instancia (1-Minutos,2-Horas,3-Dias)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEIMA', @level2type = N'COLUMN', @level2name = N'UNITIERES2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEIMA', @level2type = N'COLUMN', @level2name = N'UNITIERES2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiempo máximo de entrega de resultado en segunda instancia para procedimientos imagenológicos (TINYINT, valores numéricos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEIMA', @level2type = N'COLUMN', @level2name = N'IMAENTRES2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Imagenologia - Tiempo Maximo de Entrega de Resultado 2 Instancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEIMA', @level2type = N'COLUMN', @level2name = N'IMAENTRES2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEIMA', @level2type = N'COLUMN', @level2name = N'IMAENTRES2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de tiempo máximo de entrega de resultado imagenología (TINYINT: 1=Minutos, 2=Horas, 3=Días, SLA)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEIMA', @level2type = N'COLUMN', @level2name = N'UNITIERES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Imagenologia - Unidad de Tiempo Maximo de Entrega de Resultado (1-Minutos,2-Horas,3-Dias)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEIMA', @level2type = N'COLUMN', @level2name = N'UNITIERES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEIMA', @level2type = N'COLUMN', @level2name = N'UNITIERES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiempo máximo de entrega de resultado para exámenes imagenológicos (TINYINT, valores numéricos, SLA)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEIMA', @level2type = N'COLUMN', @level2name = N'IMAENTRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Imagenologia - Tiempo Maximo de Entrega de Resultado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEIMA', @level2type = N'COLUMN', @level2name = N'IMAENTRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEIMA', @level2type = N'COLUMN', @level2name = N'IMAENTRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de tiempo máximo de realización del examen segunda instancia imagenología (TINYINT: 1=Minutos, 2=Horas, 3=Días)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEIMA', @level2type = N'COLUMN', @level2name = N'UNITIEREA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Imagenologia - Unidad de Tiempo Maximo de Realizacion del Examen 2 Instancia (1-Minutos,2-Horas,3-Dias)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEIMA', @level2type = N'COLUMN', @level2name = N'UNITIEREA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEIMA', @level2type = N'COLUMN', @level2name = N'UNITIEREA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiempo máximo de realización del examen en segunda instancia imagenología (TINYINT, valores numéricos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEIMA', @level2type = N'COLUMN', @level2name = N'IMAREAEXA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Imagenologia - Tiempo Maximo de Realizacion del Examen 2 Instancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEIMA', @level2type = N'COLUMN', @level2name = N'IMAREAEXA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEIMA', @level2type = N'COLUMN', @level2name = N'IMAREAEXA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de tiempo máximo de realización del examen imagenología (TINYINT: 1=Minutos, 2=Horas, 3=Días, SLA)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEIMA', @level2type = N'COLUMN', @level2name = N'UNITIEREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Imagenologia - Unidad de Tiempo Maximo de Realizacion del Examen (1-Minutos,2-Horas,3-Dias)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEIMA', @level2type = N'COLUMN', @level2name = N'UNITIEREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEIMA', @level2type = N'COLUMN', @level2name = N'UNITIEREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiempo máximo de realización del examen imagenológico (TINYINT, valores numéricos, SLA)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEIMA', @level2type = N'COLUMN', @level2name = N'IMAREAEXA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Imagenologia - Tiempo Maximo de Realizacion del Examen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEIMA', @level2type = N'COLUMN', @level2name = N'IMAREAEXA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEIMA', @level2type = N'COLUMN', @level2name = N'IMAREAEXA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único CUPS de procedimiento o servicio imagenológico (CHAR 20, FK INCUPSIPS, identificador servicio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEIMA', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Unico de Procedimientos y Servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEIMA', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEIMA', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención que realiza imagenología (CHAR 10, FK ADCENATEN, unidad funcional)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEIMA', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del centro de atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEIMA', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEIMA', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parámetros de límites de área y entrega para imágenes diagnósticas por servicio (CUPS) y centro de atención. Define los valores máximos permitidos de área de examen y área de entrega de resultados de imágenes, usados para validar o configurar umbrales en el proceso de imagenología.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEIMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEIMA';
