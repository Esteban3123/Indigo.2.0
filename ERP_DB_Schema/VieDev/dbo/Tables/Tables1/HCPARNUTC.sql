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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el codigo del producto seleccionado como producto terminado en el formulario de plantillas NPT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTC', @level2type = N'COLUMN', @level2name = N'FinishedProductCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena el mensaje que se muestra cuando el valor esta fuera de rago (MinimumValue - MaximumValue)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTC', @level2type = N'COLUMN', @level2name = N'OutRangeAlertMessage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'indica el valor maximo para la alerta de volumen de nutricion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTC', @level2type = N'COLUMN', @level2name = N'MaximumValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'indica el valor minimo para la alerta de volumen de nutricion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTC', @level2type = N'COLUMN', @level2name = N'MinimumValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si la nutricion tiene activa la alerta por volumen de nutricion ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTC', @level2type = N'COLUMN', @level2name = N'NutritionVolumeAlert';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de modificacion de la nutricion parenteral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTC', @level2type = N'COLUMN', @level2name = N'MODIFYDATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario que modifica la nutricion parenteral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTC', @level2type = N'COLUMN', @level2name = N'USERMODIFY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creacion de la nutricion parenteral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTC', @level2type = N'COLUMN', @level2name = N'CREATEDATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario quien crea la nutricion parenteral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTC', @level2type = N'COLUMN', @level2name = N'USERCREATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones de la nutricion parenteral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTC', @level2type = N'COLUMN', @level2name = N'OBSERVATION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de la nutricion parenteral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTC', @level2type = N'COLUMN', @level2name = N'STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la nutricion parenteral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTC', @level2type = N'COLUMN', @level2name = N'NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la nuticion parenteral (Unico)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTC', @level2type = N'COLUMN', @level2name = N'CODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID cabecera parametrizacion de nutricion parenteral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTC', @level2type = N'COLUMN', @level2name = N'ID';

