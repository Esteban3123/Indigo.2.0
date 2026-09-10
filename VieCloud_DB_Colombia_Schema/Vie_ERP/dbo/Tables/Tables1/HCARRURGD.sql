CREATE TABLE [dbo].[HCARRURGD] (
    [AUTO]      INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCONCEC] NUMERIC (18) NOT NULL,
    [CODARRURG] CHAR (3)     NOT NULL,
    CONSTRAINT [PK_HCARRURGD] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_HCARRURGD_HCARRURGC] FOREIGN KEY ([CODCONCEC]) REFERENCES [dbo].[HCARRURGC] ([AUTOARRIB])
);




GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_HCARRURGD]
    ON [dbo].[HCARRURGD]([CODCONCEC] ASC, [CODARRURG] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de arribo a urgencias, identificador único del evento de llegada del paciente al servicio de emergencia/urgencias (CHAR 3)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCARRURGD', @level2type = N'COLUMN', @level2name = N'CODARRURG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Arribo Urgencias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCARRURGD', @level2type = N'COLUMN', @level2name = N'CODARRURG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCARRURGD', @level2type = N'COLUMN', @level2name = N'CODARRURG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo/referencia a tabla cabecera de arribo urgencias, clave foránea hacia HCARRURGC.AUTOARRIB que vincula el detalle con el registro maestro del arribo (NUMERIC 18)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCARRURGD', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo Tabla Cabecera Arribo Urgencias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCARRURGD', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCARRURGD', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Autonumérico identidad de la tabla, clave primaria secuencial para identificar únicamente cada registro de detalle de arribo urgencias (INT IDENTITY)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCARRURGD', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCARRURGD', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCARRURGD', @level2type = N'COLUMN', @level2name = N'AUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de conceptos o ítems asociados a arreglos o acuerdos de urgencias. Relaciona cada concepto de cobro o clasificación con un tipo de arreglo de urgencia específico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCARRURGD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCARRURGD';
