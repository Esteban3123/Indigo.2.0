CREATE TABLE [dbo].[HCPARNUTC] (
    [ID]                   INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODE]                 CHAR (3)       NOT NULL,
    [NAME]                 CHAR (200)     NULL,
    [STATUS]               BIT            NOT NULL,
    [OBSERVATION]          VARCHAR (MAX)  NULL,
    [USERCREATE]           CHAR (20)      NOT NULL,
    [CREATEDATE]           DATETIME       NOT NULL,
    [USERMODIFY]           CHAR (20)      NULL,
    [MODIFYDATE]           DATETIME       NULL,
    [NutritionVolumeAlert] BIT            NULL,
    [MinimumValue]         DECIMAL (7, 2) NULL,
    [MaximumValue]         DECIMAL (7, 2) NULL,
    [OutRangeAlertMessage] VARCHAR (500)  NULL,
    [FinishedProductCode]  VARCHAR (20)   NULL,
    CONSTRAINT [PK__HCPARNUT__3214EC075373FF73] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del producto terminado seleccionado en plantilla de nutrición parenteral total (NPT); referencia al producto farmacéutico final preparado. VARCHAR(20)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTC', @level2type = N'COLUMN', @level2name = N'FinishedProductCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el codigo del producto seleccionado como producto terminado en el formulario de plantillas NPT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTC', @level2type = N'COLUMN', @level2name = N'FinishedProductCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTC', @level2type = N'COLUMN', @level2name = N'FinishedProductCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mensaje de alerta mostrado cuando el volumen de nutrición excede rango permitido (MinimumValue-MaximumValue); notificación al profesional de salud. VARCHAR(500)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTC', @level2type = N'COLUMN', @level2name = N'OutRangeAlertMessage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el mensaje que se muestra cuando el valor esta fuera de rago (MinimumValue - MaximumValue)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTC', @level2type = N'COLUMN', @level2name = N'OutRangeAlertMessage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTC', @level2type = N'COLUMN', @level2name = N'OutRangeAlertMessage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor máximo permitido para volumen de nutrición parenteral; umbral superior de alerta; DECIMAL(7,2)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTC', @level2type = N'COLUMN', @level2name = N'MaximumValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'indica el valor maximo para la alerta de volumen de nutricion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTC', @level2type = N'COLUMN', @level2name = N'MaximumValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTC', @level2type = N'COLUMN', @level2name = N'MaximumValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor mínimo permitido para volumen de nutrición parenteral; umbral inferior de alerta; DECIMAL(7,2)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTC', @level2type = N'COLUMN', @level2name = N'MinimumValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'indica el valor minimo para la alerta de volumen de nutricion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTC', @level2type = N'COLUMN', @level2name = N'MinimumValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTC', @level2type = N'COLUMN', @level2name = N'MinimumValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador activo/inactivo de alerta por volumen en nutrición parenteral; BIT (1=habilitado, 0=deshabilitado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTC', @level2type = N'COLUMN', @level2name = N'NutritionVolumeAlert';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si la nutricion tiene activa la alerta por volumen de nutricion ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTC', @level2type = N'COLUMN', @level2name = N'NutritionVolumeAlert';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTC', @level2type = N'COLUMN', @level2name = N'NutritionVolumeAlert';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora última modificación del parámetro de nutrición parenteral; DATETIME', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTC', @level2type = N'COLUMN', @level2name = N'MODIFYDATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificacion de la nutricion parenteral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTC', @level2type = N'COLUMN', @level2name = N'MODIFYDATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTC', @level2type = N'COLUMN', @level2name = N'MODIFYDATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación del parámetro NPT; CHAR(20); auditoría', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTC', @level2type = N'COLUMN', @level2name = N'USERMODIFY';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que modifica la nutricion parenteral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTC', @level2type = N'COLUMN', @level2name = N'USERMODIFY';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTC', @level2type = N'COLUMN', @level2name = N'USERMODIFY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora creación inicial del parámetro de nutrición parenteral; DATETIME', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTC', @level2type = N'COLUMN', @level2name = N'CREATEDATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creacion de la nutricion parenteral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTC', @level2type = N'COLUMN', @level2name = N'CREATEDATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTC', @level2type = N'COLUMN', @level2name = N'CREATEDATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el parámetro de nutrición parenteral; CHAR(20); auditoría', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTC', @level2type = N'COLUMN', @level2name = N'USERCREATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario quien crea la nutricion parenteral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTC', @level2type = N'COLUMN', @level2name = N'USERCREATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTC', @level2type = N'COLUMN', @level2name = N'USERCREATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones, notas clínicas o comentarios adicionales sobre la nutrición parenteral; VARCHAR(MAX)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTC', @level2type = N'COLUMN', @level2name = N'OBSERVATION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones de la nutricion parenteral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTC', @level2type = N'COLUMN', @level2name = N'OBSERVATION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTC', @level2type = N'COLUMN', @level2name = N'OBSERVATION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de activación del parámetro NPT (1=activo, 0=inactivo); BIT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTC', @level2type = N'COLUMN', @level2name = N'STATUS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la nutricion parenteral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTC', @level2type = N'COLUMN', @level2name = N'STATUS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTC', @level2type = N'COLUMN', @level2name = N'STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o descripción del parámetro de nutrición parenteral total (NPT); CHAR(200)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTC', @level2type = N'COLUMN', @level2name = N'NAME';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la nutricion parenteral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTC', @level2type = N'COLUMN', @level2name = N'NAME';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTC', @level2type = N'COLUMN', @level2name = N'NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único identificador del parámetro de nutrición parenteral; CHAR(3); clave natural', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTC', @level2type = N'COLUMN', @level2name = N'CODE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la nuticion parenteral (Unico)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTC', @level2type = N'COLUMN', @level2name = N'CODE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTC', @level2type = N'COLUMN', @level2name = N'CODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador primario (Identity) de cabecera parametrización nutrición parenteral; INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTC', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID cabecera parametrizacion de nutricion parenteral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTC', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTC', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de parámetros o nutrientes utilizados en nutrición clínica (nutrición parenteral/enteral). Registra cada componente nutricional con su código, nombre, estado, alertas de volumen y rangos mínimos/máximos aceptables para la preparación de fórmulas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTC';
