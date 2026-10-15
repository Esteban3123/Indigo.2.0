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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena las vías de administración si la dieta es de leche materna.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPDIET', @level2type = N'COLUMN', @level2name = N'AdministrationRoutes';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena el dato que indica si el tipo de dieta aplica para leche materna', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPDIET', @level2type = N'COLUMN', @level2name = N'BreastMilk';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo Reservado Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPDIET', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Define si Restringe combinaciones con otros tipos de dietas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPDIET', @level2type = N'COLUMN', @level2name = N'RESTCOMBI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Registro  Activo = True  Inactivo = False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPDIET', @level2type = N'COLUMN', @level2name = N'ESTADOREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Se estipula el rango de horas para la solicitud de dietas, en este campo se estipula la hora minima', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPDIET', @level2type = N'COLUMN', @level2name = N'HORMAXSOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Se estipula el rango de horas para la solicitud de dietas, en este campo se estipula la hora minima', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPDIET', @level2type = N'COLUMN', @level2name = N'HORMINSOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion del Tipo de Dieta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPDIET', @level2type = N'COLUMN', @level2name = N'DESTIPDIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Tipo de Dieta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPDIET', @level2type = N'COLUMN', @level2name = N'CODTIPDIE';

