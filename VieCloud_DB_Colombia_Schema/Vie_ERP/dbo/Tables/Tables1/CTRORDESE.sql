CREATE TABLE [dbo].[CTRORDESE] (
    [AUTO]      INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NUMINGRES] CHAR (10) NOT NULL,
    [SOSORDSER] CHAR (10) NOT NULL,
    [ORIGEN]    CHAR (1)  NOT NULL,
    CONSTRAINT [PK_CTRORDESE] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_CTRORDESE_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Origen o procedencia desde donde fue creada la orden de servicio: 1=Control Consulta Externa, 2=Liquidador, 3=Control Servicio Ambulatorios. Tipo: CHAR(1). Indica punto de generación de la orden en el flujo de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CTRORDESE', @level2type = N'COLUMN', @level2name = N'ORIGEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Origen desde donde fue creada la orden de servicio 1-Control Consulta Externa 2-  Liquidador 3-Control Servicio Ambulatorios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CTRORDESE', @level2type = N'COLUMN', @level2name = N'ORIGEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CTRORDESE', @level2type = N'COLUMN', @level2name = N'ORIGEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número único de la orden de servicio asociada al ingreso del paciente. Tipo: CHAR(10). Identificador de la orden de atención, servicio o procedimiento solicitado durante la atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CTRORDESE', @level2type = N'COLUMN', @level2name = N'SOSORDSER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de la orden asociada al ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CTRORDESE', @level2type = N'COLUMN', @level2name = N'SOSORDSER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CTRORDESE', @level2type = N'COLUMN', @level2name = N'SOSORDSER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del ingreso o atención en que se generó la orden de servicio. Tipo: CHAR(10). FK a [dbo].[ADINGRESO]. Vincula la orden al registro de admisión, urgencia o consulta externa del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CTRORDESE', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del ingreso en que se genero la orden', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CTRORDESE', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CTRORDESE', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico (ID) único de cada registro de orden de servicio. Tipo: INT IDENTITY(1,1). Clave primaria para control interno de la tabla de órdenes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CTRORDESE', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la orden', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CTRORDESE', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CTRORDESE', @level2type = N'COLUMN', @level2name = N'AUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de órdenes de servicio asociadas a un ingreso o atención, indicando el número de ingreso, el identificador de la orden y su origen dentro del proceso asistencial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CTRORDESE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CTRORDESE';
