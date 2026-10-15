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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Registro  Activo = True  Inactivo = False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPDIET', @level2type = N'COLUMN', @level2name = N'ESTADOREG';

