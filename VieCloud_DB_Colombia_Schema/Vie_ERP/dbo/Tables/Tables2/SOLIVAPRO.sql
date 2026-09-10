CREATE TABLE [dbo].[SOLIVAPRO] (
    [SOAUTOIVA] TINYINT        IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [SODESCIVA] VARCHAR (50)   NOT NULL,
    [SOPORCIVA] NUMERIC (5, 2) NOT NULL,
    [SOCODIVA]  VARCHAR (20)   NOT NULL,
    CONSTRAINT [PK_SOLIVAPRO] PRIMARY KEY CLUSTERED ([SOAUTOIVA] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador del IVA (Impuesto al Valor Agregado); clave única para clasificación tributaria de alícuota; VARCHAR(20)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLIVAPRO', @level2type = N'COLUMN', @level2name = N'SOCODIVA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el codigo del iva', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLIVAPRO', @level2type = N'COLUMN', @level2name = N'SOCODIVA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLIVAPRO', @level2type = N'COLUMN', @level2name = N'SOCODIVA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje o tarifa del IVA aplicable; valor numérico con 2 decimales (ej: 19.00, 5.00); NUMERIC(5,2)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLIVAPRO', @level2type = N'COLUMN', @level2name = N'SOPORCIVA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del iva', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLIVAPRO', @level2type = N'COLUMN', @level2name = N'SOPORCIVA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLIVAPRO', @level2type = N'COLUMN', @level2name = N'SOPORCIVA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción o denominación del tipo/régimen de IVA; texto explicativo de la alícuota tributaria; VARCHAR(50)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLIVAPRO', @level2type = N'COLUMN', @level2name = N'SODESCIVA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Desripcion del iva', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLIVAPRO', @level2type = N'COLUMN', @level2name = N'SODESCIVA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLIVAPRO', @level2type = N'COLUMN', @level2name = N'SODESCIVA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador secuencial único del registro de IVA (autoincrementable); clave primaria; TINYINT IDENTITY', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLIVAPRO', @level2type = N'COLUMN', @level2name = N'SOAUTOIVA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'autonumerico del iva', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLIVAPRO', @level2type = N'COLUMN', @level2name = N'SOAUTOIVA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLIVAPRO', @level2type = N'COLUMN', @level2name = N'SOAUTOIVA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de tipos de IVA (impuesto al valor agregado) aplicables a los servicios o productos. Registra los diferentes porcentajes de IVA disponibles para la facturación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLIVAPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLIVAPRO';
