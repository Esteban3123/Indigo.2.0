CREATE TABLE [dbo].[HCCENLABUD] (
    [ID]          INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCCENLABU] INT NOT NULL,
    [TIPSERIPS]   INT NOT NULL,
    CONSTRAINT [PK_HCCENLABUD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCCENLABUD_HCCENLABU] FOREIGN KEY ([IDHCCENLABU]) REFERENCES [dbo].[HCCENLABU] ([ID])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de Servicio RIPS según clasificación de prestación: 1=Laboratorios clínicos, 2=Patologías/Anatomía patológica, 3=Imágenes diagnósticas/Radiología, 4=Procedimientos no quirúrgicos, 5=Procedimientos quirúrgicos, 6=Interconsultas con especialistas. INT, utilizado para reportes RIPS y facturación de servicios de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCENLABUD', @level2type = N'COLUMN', @level2name = N'TIPSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Servicio 1: Laboratorios 2: Patologias 3: Imagenes Diagnosticas 4:   Procedimeintos no Qx 5: Procedimientos Qx 6: Interconsultas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCENLABUD', @level2type = N'COLUMN', @level2name = N'TIPSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCENLABUD', @level2type = N'COLUMN', @level2name = N'TIPSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (FK) de la tabla HCCENLABU que referencia el centro de atención/unidad funcional donde se prestó el servicio de laboratorio, patología, imagen diagnóstica o procedimiento. INT NOT NULL, clave foránea para rastrabilidad de prestaciones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCENLABUD', @level2type = N'COLUMN', @level2name = N'IDHCCENLABU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la tabla HCCENLABU ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCENLABUD', @level2type = N'COLUMN', @level2name = N'IDHCCENLABU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCENLABUD', @level2type = N'COLUMN', @level2name = N'IDHCCENLABU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico único (INT IDENTITY) de la línea de detalle de servicios en el centro de atención. Clave primaria de la tabla HCCENLABUD, para vincular cada servicio prestado al registro de centro/unidad funcional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCENLABUD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCENLABUD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCENLABUD', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de tipos de servicio (CUPS/RIPS) asociados a las órdenes de laboratorio clínico dentro de la historia clínica. Relaciona cada orden de laboratorio con los servicios específicos solicitados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCENLABUD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCENLABUD';
