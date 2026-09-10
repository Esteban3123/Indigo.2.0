CREATE TABLE [dbo].[CHTIPDIET] (
    [CODTIPDIE]            CHAR (3)      NOT NULL,
    [DESTIPDIE]            CHAR (40)     NOT NULL,
    [HORMINSOL]            DATETIME      NOT NULL,
    [HORMAXSOL]            DATETIME      NOT NULL,
    [ESTADOREG]            BIT           NOT NULL,
    [RESTCOMBI]            BIT           NOT NULL,
    [INDAUDFOR]            NUMERIC (18)  NOT NULL,
    [BreastMilk]           BIT           NULL,
    [AdministrationRoutes] VARCHAR (300) NULL,
    CONSTRAINT [PK_CHTIPDIET] PRIMARY KEY CLUSTERED ([CODTIPDIE] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vías de administración de la dieta (VARCHAR 300). Almacena rutas de suministro cuando aplica lactancia materna: oral, sonda nasogástrica, sonda orogástrica, etc.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPDIET', @level2type = N'COLUMN', @level2name = N'AdministrationRoutes';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena las vías de administración si la dieta es de leche materna.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPDIET', @level2type = N'COLUMN', @level2name = N'AdministrationRoutes';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPDIET', @level2type = N'COLUMN', @level2name = N'AdministrationRoutes';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) que señala si el tipo de dieta es compatible con o aplicable a lactancia materna (leche materna exclusiva).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPDIET', @level2type = N'COLUMN', @level2name = N'BreastMilk';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el dato que indica si el tipo de dieta aplica para leche materna', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPDIET', @level2type = N'COLUMN', @level2name = N'BreastMilk';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPDIET', @level2type = N'COLUMN', @level2name = N'BreastMilk';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo numérico (NUMERIC 18) reservado para auditoría y trazabilidad del registro: usuario, fecha, acción en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPDIET', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo Reservado Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPDIET', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPDIET', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera booleana (BIT) que determina si este tipo de dieta restringe o prohíbe combinaciones simultáneas con otros tipos de dieta.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPDIET', @level2type = N'COLUMN', @level2name = N'RESTCOMBI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Define si Restringe combinaciones con otros tipos de dietas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPDIET', @level2type = N'COLUMN', @level2name = N'RESTCOMBI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPDIET', @level2type = N'COLUMN', @level2name = N'RESTCOMBI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del registro (BIT): True = Activo/Vigente, False = Inactivo/Eliminado. Controla visibilidad en órdenes de dieta.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPDIET', @level2type = N'COLUMN', @level2name = N'ESTADOREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del Registro  Activo = True  Inactivo = False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPDIET', @level2type = N'COLUMN', @level2name = N'ESTADOREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPDIET', @level2type = N'COLUMN', @level2name = N'ESTADOREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora máxima (DATETIME) permitida para solicitar este tipo de dieta. Define límite superior del rango horario de pedido.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPDIET', @level2type = N'COLUMN', @level2name = N'HORMAXSOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se estipula el rango de horas para la solicitud de dietas, en este campo se estipula la hora minima', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPDIET', @level2type = N'COLUMN', @level2name = N'HORMAXSOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPDIET', @level2type = N'COLUMN', @level2name = N'HORMAXSOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora mínima (DATETIME) permitida para solicitar este tipo de dieta. Define límite inferior del rango horario de pedido.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPDIET', @level2type = N'COLUMN', @level2name = N'HORMINSOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se estipula el rango de horas para la solicitud de dietas, en este campo se estipula la hora minima', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPDIET', @level2type = N'COLUMN', @level2name = N'HORMINSOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPDIET', @level2type = N'COLUMN', @level2name = N'HORMINSOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del tipo de dieta (CHAR 40): nombre legible del tipo (ej. Dieta Blanda, Dieta Sin Sal, Dieta Hipoprotéica, etc.).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPDIET', @level2type = N'COLUMN', @level2name = N'DESTIPDIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion del Tipo de Dieta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPDIET', @level2type = N'COLUMN', @level2name = N'DESTIPDIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPDIET', @level2type = N'COLUMN', @level2name = N'DESTIPDIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del tipo de dieta (CHAR 3, PK). Identificador alfanumérico usado en órdenes dietéticas y registros clínicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPDIET', @level2type = N'COLUMN', @level2name = N'CODTIPDIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Tipo de Dieta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPDIET', @level2type = N'COLUMN', @level2name = N'CODTIPDIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPDIET', @level2type = N'COLUMN', @level2name = N'CODTIPDIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de tipos de dieta hospitalaria. Define los tipos de alimentación o régimen dietético que se pueden prescribir a los pacientes, incluyendo restricciones de horario, combinaciones permitidas y si aplica leche materna.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPDIET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPDIET';
