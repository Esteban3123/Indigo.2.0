CREATE TABLE [dbo].[UREPPERMISOR] (
    [ID]         INT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ID_REPORTE] INT      NOT NULL,
    [codigorol]  CHAR (4) NOT NULL,
    CONSTRAINT [PK_UREPPERMISOR] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_UREPPERMISOR_SEGrolesu] FOREIGN KEY ([codigorol]) REFERENCES [dbo].[SEGrolesu] ([codigorol]),
    CONSTRAINT [FK_UREPPERMISOR_UREPPERSON] FOREIGN KEY ([ID_REPORTE]) REFERENCES [dbo].[UREPPERSON] ([ID])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del rol de usuario (CHAR 4). Referencia a SEGrolesu que define permisos y niveles de acceso en el sistema. FK que vincula permisos de reporte a roles administrativos, clínicos o de facturación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UREPPERMISOR', @level2type = N'COLUMN', @level2name = N'codigorol';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del rol', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UREPPERMISOR', @level2type = N'COLUMN', @level2name = N'codigorol';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UREPPERMISOR', @level2type = N'COLUMN', @level2name = N'codigorol';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del reporte (INT). Referencia a UREPPERSON que define el reporte específico. FK que asocia permisos de visualización o ejecución del reporte al rol correspondiente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UREPPERMISOR', @level2type = N'COLUMN', @level2name = N'ID_REPORTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del reporte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UREPPERMISOR', @level2type = N'COLUMN', @level2name = N'ID_REPORTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UREPPERMISOR', @level2type = N'COLUMN', @level2name = N'ID_REPORTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la relación permiso-reporte (INT IDENTITY). Clave primaria que registra cada asignación de permiso de un rol sobre un reporte en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UREPPERMISOR', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UREPPERMISOR', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UREPPERMISOR', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Permisos de acceso a reportes por rol de usuario. Indica qué roles tienen autorización para visualizar o ejecutar cada reporte en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UREPPERMISOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UREPPERMISOR';
