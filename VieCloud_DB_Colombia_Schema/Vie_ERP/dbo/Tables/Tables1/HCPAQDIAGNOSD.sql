CREATE TABLE [dbo].[HCPAQDIAGNOSD] (
    [ID]              INT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCPAQORDENESC] INT      NOT NULL,
    [CODDIAGNO]       CHAR (4) NOT NULL,
    CONSTRAINT [PK_HCPAQDIAGNOSD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCPAQDIAGNOSD_HCPAQORDENESC] FOREIGN KEY ([IDHCPAQORDENESC]) REFERENCES [dbo].[HCPAQORDENESC] ([ID]),
    CONSTRAINT [FK_HCPAQDIAGNOSD_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO])
);


GO
ALTER TABLE [dbo].[HCPAQDIAGNOSD] NOCHECK CONSTRAINT [FK_HCPAQDIAGNOSD_HCPAQORDENESC];




GO
ALTER TABLE [dbo].[HCPAQDIAGNOSD] NOCHECK CONSTRAINT [FK_HCPAQDIAGNOSD_HCPAQORDENESC];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de diagnóstico (CIE-10 u otra clasificación), identificador de 4 caracteres que vincula con la tabla INDIAGNOS para especificar el diagnóstico asociado a la orden.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQDIAGNOSD', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQDIAGNOSD', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQDIAGNOSD', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de relación con la cabecera de paquetes de órdenes (FK a HCPAQORDENESC), conecta el diagnóstico al registro principal de órdenes médicas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQDIAGNOSD', @level2type = N'COLUMN', @level2name = N'IDHCPAQORDENESC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con la tabla de cabecera de los paquetes de ordenes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQDIAGNOSD', @level2type = N'COLUMN', @level2name = N'IDHCPAQORDENESC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQDIAGNOSD', @level2type = N'COLUMN', @level2name = N'IDHCPAQORDENESC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (IDENTITY), clave primaria consecutiva de la tabla de diagnósticos detallados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQDIAGNOSD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQDIAGNOSD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQDIAGNOSD', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Diagnósticos asociados a las órdenes de prescripción o escalonamiento de pacientes en historia clínica. Relaciona cada orden con uno o más códigos de diagnóstico CIE-10.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQDIAGNOSD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQDIAGNOSD';
