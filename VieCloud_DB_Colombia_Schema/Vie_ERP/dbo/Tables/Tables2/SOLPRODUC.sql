CREATE TABLE [dbo].[SOLPRODUC] (
    [PRODAUTON]  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PRODCODIGO] VARCHAR (20)  NOT NULL,
    [PRODNOMBRE] VARCHAR (255) NOT NULL,
    [PRODESTADO] BIT           NULL,
    [PRODOBSER]  VARCHAR (500) NULL,
    [PRODCODCAT] INT           NOT NULL,
    [PRODUCIVA]  TINYINT       NULL,
    [PROUNIMED]  INT           CONSTRAINT [DFX_0B2D2080] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_SOLPRODUC] PRIMARY KEY CLUSTERED ([PRODAUTON] ASC),
    CONSTRAINT [FK_SOLPRODUC_SOLCATEGO] FOREIGN KEY ([PRODCODCAT]) REFERENCES [dbo].[SOLCATEGO] ([CATEAUTON]),
    CONSTRAINT [FK_SOLPRODUC_SOLIVAPRO] FOREIGN KEY ([PRODUCIVA]) REFERENCES [dbo].[SOLIVAPRO] ([SOAUTOIVA]),
    CONSTRAINT [FK_SOLPRODUC_SOLPRODUC] FOREIGN KEY ([PRODAUTON]) REFERENCES [dbo].[SOLPRODUC] ([PRODAUTON]),
    CONSTRAINT [FK_SOLPRODUC_SOLUNIMED] FOREIGN KEY ([PROUNIMED]) REFERENCES [dbo].[SOLUNIMED] ([UNIMEDAUT])
);




GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la unidad de medida del producto (FK a SOLUNIMED). Ejemplo: unidad, mililitro, gramo, caja, blíster. Permite cuantificar productos en solicitudes y recetas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPRODUC', @level2type = N'COLUMN', @level2name = N'PROUNIMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene la unidad de medida del producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPRODUC', @level2type = N'COLUMN', @level2name = N'PROUNIMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPRODUC', @level2type = N'COLUMN', @level2name = N'PROUNIMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del impuesto IVA aplicable al producto (FK a SOLIVAPRO). Referencia el porcentaje o tarifa de IVA configurada para este producto en facturación y RIPS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPRODUC', @level2type = N'COLUMN', @level2name = N'PRODUCIVA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el autonumerico del iva del producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPRODUC', @level2type = N'COLUMN', @level2name = N'PRODUCIVA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPRODUC', @level2type = N'COLUMN', @level2name = N'PRODUCIVA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la categoría del producto (FK a SOLCATEGO). Clasifica el producto en grupos funcionales: medicamentos, dispositivos, insumos, servicios. Usado en búsquedas y reportes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPRODUC', @level2type = N'COLUMN', @level2name = N'PRODCODCAT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el autonumerico de la categoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPRODUC', @level2type = N'COLUMN', @level2name = N'PRODCODCAT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPRODUC', @level2type = N'COLUMN', @level2name = N'PRODCODCAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones o notas adicionales del producto. Incluye contraindicaciones, precauciones, presentación, o información relevante para prescripción y dispensación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPRODUC', @level2type = N'COLUMN', @level2name = N'PRODOBSER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene las observaciondes del producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPRODUC', @level2type = N'COLUMN', @level2name = N'PRODOBSER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPRODUC', @level2type = N'COLUMN', @level2name = N'PRODOBSER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado activo/inactivo del producto (BIT: 1=Activo, 0=Inactivo). Controla disponibilidad en catálogos de solicitud, recetas y facturación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPRODUC', @level2type = N'COLUMN', @level2name = N'PRODESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el estado del producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPRODUC', @level2type = N'COLUMN', @level2name = N'PRODESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPRODUC', @level2type = N'COLUMN', @level2name = N'PRODESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o descripción comercial/genérica del producto. Utilizado en etiquetas de recetas, órdenes, RIPS, facturas e inventarios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPRODUC', @level2type = N'COLUMN', @level2name = N'PRODNOMBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el nombre del producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPRODUC', @level2type = N'COLUMN', @level2name = N'PRODNOMBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPRODUC', @level2type = N'COLUMN', @level2name = N'PRODNOMBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del producto. Identificador funcional para búsqueda rápida en solicitudes, recetas, facturas y reportes de farmacia y almacén.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPRODUC', @level2type = N'COLUMN', @level2name = N'PRODCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el codigo del producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPRODUC', @level2type = N'COLUMN', @level2name = N'PRODCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPRODUC', @level2type = N'COLUMN', @level2name = N'PRODCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador primario (autoincremental) de la tabla SOLPRODUC. Llave única del registro del producto en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPRODUC', @level2type = N'COLUMN', @level2name = N'PRODAUTON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPRODUC', @level2type = N'COLUMN', @level2name = N'PRODAUTON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPRODUC', @level2type = N'COLUMN', @level2name = N'PRODAUTON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo maestro de productos o insumos del sistema de solicitudes. Registra cada producto con su nombre, categoría, estado, IVA aplicable y unidad de medida, usado para gestionar solicitudes de productos o suministros.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPRODUC';
