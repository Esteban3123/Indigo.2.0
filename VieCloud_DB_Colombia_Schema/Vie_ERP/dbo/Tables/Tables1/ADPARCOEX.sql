CREATE TABLE [dbo].[ADPARCOEX] (
    [CODCENATE]              CHAR (10) NOT NULL,
    [REALIZAIN]              BIT       NOT NULL,
    [CODSERIPS]              CHAR (20) NULL,
    [ATPACCOEX]              BIT       NOT NULL,
    [DIALISING]              INT       NOT NULL,
    [ID]                     INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AllowPriorConsultation] INT       CONSTRAINT [DF_ADPARCOEX_AllowpriorConsultation] DEFAULT ((2)) NULL,
    CONSTRAINT [PK_ADPARCOEX] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ADPARCOEX_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_ADPARCOEX_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Permitir pre-consulta (BIT, 1=SI/2=NO). Habilita pestaña y formulario de pre-consulta en control de citas de consulta externa y columna en parametrización de examen físico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCOEX', @level2type = N'COLUMN', @level2name = N'AllowPriorConsultation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permitir pre-consulta    Si el parámetro se encuentra en "SI", se realizará lo siguiente:  1 - Se habilitará la pestaña y el formulario "Pre-consulta" en el formulario “Control citas consulta externa”.    2- Se habilitará la columna “Pre-consulta” en el formulario “Parametrización control de examen físico”.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCOEX', @level2type = N'COLUMN', @level2name = N'AllowPriorConsultation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCOEX', @level2type = N'COLUMN', @level2name = N'AllowPriorConsultation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY). Consecutivo/secuencial auto-incrementado de la tabla de parámetros de consulta externa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCOEX', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCOEX', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCOEX', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Días de retroactividad (INT). Rango de días atrás para listar/filtrar ingresos y atenciones de consulta externa en reportes históricos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCOEX', @level2type = N'COLUMN', @level2name = N'DIALISING';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Listar Ingresos de Consulta Externa de X dias atras', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCOEX', @level2type = N'COLUMN', @level2name = N'DIALISING';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCOEX', @level2type = N'COLUMN', @level2name = N'DIALISING';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Atender pacientes no facturados consulta externa (BIT, 1=SI/0=NO). Bandera que permite/restringe atención de pacientes sin factura pendiente en consulta externa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCOEX', @level2type = N'COLUMN', @level2name = N'ATPACCOEX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Atender pacientes no facturados C. Ext', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCOEX', @level2type = N'COLUMN', @level2name = N'ATPACCOEX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCOEX', @level2type = N'COLUMN', @level2name = N'ATPACCOEX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de procedimiento/servicio IPS (CHAR 20, FK→INCUPSIPS). Referencia al código CUPS/IPS del procedimiento o servicio de salud en catálogo nacional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCOEX', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Procedimiento IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCOEX', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCOEX', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Realizar interfaz consulta externa (BIT, 1=SI/0=NO). Especifica si se ejecuta sincronización/integración con módulo de consulta externa en el ERP.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCOEX', @level2type = N'COLUMN', @level2name = N'REALIZAIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si se Realiza Interfaz con el modulo de Consulta Externa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCOEX', @level2type = N'COLUMN', @level2name = N'REALIZAIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCOEX', @level2type = N'COLUMN', @level2name = N'REALIZAIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código centro de atención (CHAR 10, PK→ADCENATEN). Identificador del punto/unidad de servicio, sede u hospital donde ingresa el paciente para consulta externa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCOEX', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Centron de Atencion en donde Ingresa el Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCOEX', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCOEX', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parámetros de configuración para la consulta externa por centro de atención: controla si se realizan atenciones ambulatorias, el servicio IPS asociado, si se atienden pacientes con cobertura especial (como diálisis), y si se permite consulta previa antes del ingreso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCOEX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCOEX';
