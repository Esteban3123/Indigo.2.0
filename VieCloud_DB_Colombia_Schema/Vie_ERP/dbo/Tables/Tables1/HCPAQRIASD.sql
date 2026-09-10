CREATE TABLE [dbo].[HCPAQRIASD] (
    [ID]              INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCPAQORDENESC] INT NOT NULL,
    [IDRIAS]          INT NOT NULL,
    CONSTRAINT [PK_HCPAQRIASD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCPAQRIASD_HCPAQORDENESC] FOREIGN KEY ([IDHCPAQORDENESC]) REFERENCES [dbo].[HCPAQORDENESC] ([ID]),
    CONSTRAINT [FK_HCPAQRIASD_PRMODELOHC] FOREIGN KEY ([IDRIAS]) REFERENCES [dbo].[RIAS] ([ID])
);


GO
ALTER TABLE [dbo].[HCPAQRIASD] NOCHECK CONSTRAINT [FK_HCPAQRIASD_HCPAQORDENESC];




GO
ALTER TABLE [dbo].[HCPAQRIASD] NOCHECK CONSTRAINT [FK_HCPAQRIASD_HCPAQORDENESC];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la Ruta Integral de Atención en Salud (RIAS). Relación con tabla RIAS que define rutas de atención, protocolos clínicos y flujos de procedimientos para pacientes. FK a RIAS.ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQRIASD', @level2type = N'COLUMN', @level2name = N'IDRIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con la tabla de las RIAS o rutas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQRIASD', @level2type = N'COLUMN', @level2name = N'IDRIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQRIASD', @level2type = N'COLUMN', @level2name = N'IDRIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la orden de paquete de servicios de salud. Relación con tabla HCPAQORDENESC (cabecera de órdenes) que agrupa servicios, procedimientos y atenciones facturables. FK a HCPAQORDENESC.ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQRIASD', @level2type = N'COLUMN', @level2name = N'IDHCPAQORDENESC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con la tabla cabecera de las ordenes de paquetes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQRIASD', @level2type = N'COLUMN', @level2name = N'IDHCPAQORDENESC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQRIASD', @level2type = N'COLUMN', @level2name = N'IDHCPAQORDENESC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único y consecutivo (PK) de la asociación entre RIAS y orden de paquete. INT IDENTITY(1,1)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQRIASD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQRIASD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQRIASD', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona las órdenes de escáner (radiología o imágenes diagnósticas) de la historia clínica del paciente con los registros RIAS (Rutas Integrales de Atención en Salud), vinculando cada orden de imagen con su correspondiente atención o actividad de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQRIASD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQRIASD';
